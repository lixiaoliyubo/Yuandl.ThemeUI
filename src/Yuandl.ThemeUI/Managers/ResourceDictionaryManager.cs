// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Yuandl.ThemeUI.Managers;

/// <summary>
/// 用于管理应用程序字典的类。
/// </summary>
internal class ResourceDictionaryManager
{
    /// <summary>
    /// Gets 获取命名空间，例如正在搜索资源的库。
    /// </summary>
    public string SearchNamespace { get; }

    // 缓存已查找的资源字典，提高性能
    private readonly Dictionary<string, ResourceDictionary> _resourceCache = new();

    public ResourceDictionaryManager(string searchNamespace)
    {
        SearchNamespace = searchNamespace ?? string.Empty;
    }

    /// <summary>
    /// 显示应用程序是否包含 <see cref="ResourceDictionary"/>。
    /// </summary>
    /// <param name="resourceLookup">资源名称的任何部分。</param>
    /// <returns>如果不存在，则返回 <see langword="false"/>。</returns>
    public bool HasDictionary(string resourceLookup)
    {
        return GetDictionary(resourceLookup) != null;
    }

    /// <summary>
    /// 获取存在的 <see cref="ResourceDictionary"/>。
    /// </summary>
    /// <param name="resourceLookup">资源名称的任何部分。</param>
    /// <returns><see cref="ResourceDictionary"/>，如果不存在则返回 <see langword="null"/>。</returns>
    public ResourceDictionary GetDictionary(string resourceLookup)
    {
        // 检查缓存
        if (!string.IsNullOrEmpty(resourceLookup) && _resourceCache.TryGetValue(resourceLookup, out var cachedDictionary))
        {
            return cachedDictionary;
        }

        // 验证参数和应用程序
        if (UiApplication.Current is null || string.IsNullOrEmpty(resourceLookup))
        {
            return null;
        }

        var applicationDictionaries = GetApplicationMergedDictionaries();
        if (applicationDictionaries.Count == 0)
        {
            return null;
        }

        // 标准化查找条件
        resourceLookup = resourceLookup.ToLower().Trim();
        var searchNamespace = SearchNamespace.ToLower();

        // 查找资源字典
        foreach (var dictionary in applicationDictionaries.Where(d => d != null))
        {
            // 检查直接源
            var foundDictionary = CheckDictionarySource(dictionary, resourceLookup, searchNamespace);
            if (foundDictionary != null)
            {
                // 更新缓存
                _resourceCache[resourceLookup] = foundDictionary;
                return foundDictionary;
            }

            // 检查嵌套的MergedDictionaries
            if (dictionary.MergedDictionaries != null && dictionary.MergedDictionaries.Count > 0)
            {
                foreach (var nestedDictionary in dictionary.MergedDictionaries.Where(d => d != null))
                {
                    foundDictionary = CheckDictionarySource(nestedDictionary, resourceLookup, searchNamespace);
                    if (foundDictionary != null)
                    {
                        // 更新缓存
                        _resourceCache[resourceLookup] = foundDictionary;
                        return foundDictionary;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 检查单个ResourceDictionary的源是否匹配查找条件
    /// </summary>
    private ResourceDictionary CheckDictionarySource(ResourceDictionary dictionary, string resourceLookup, string searchNamespace)
    {
        if (dictionary.Source != null)
        {
            try
            {
                var sourceUri = dictionary.Source.ToString().ToLower().Trim();
                if (sourceUri.Contains(searchNamespace) && sourceUri.Contains(resourceLookup))
                {
                    return dictionary;
                }
            }
            catch { }
        }

        return null;
    }

    /// <summary>
    /// 更新资源字典
    /// </summary>
    /// <param name="resourceLookup">资源名称的任何部分。</param>
    /// <param name="newResourceUri">替换资源的有效 <see cref="Uri"/>。</param>
    /// <returns>如果字典 <see cref="Uri"/> 已更新，则返回 <see langword="true"/>。否则返回 <see langword="false"/>。</returns>
    public bool UpdateDictionary(string resourceLookup, Uri newResourceUri)
    {
        // 验证参数和应用程序
        if (UiApplication.Current is null || string.IsNullOrEmpty(resourceLookup) || newResourceUri is null)
        {
            return false;
        }

        var applicationDictionaries = UiApplication.Current.Resources.MergedDictionaries;
        if (applicationDictionaries.Count == 0)
        {
            return false;
        }

        // 标准化查找条件
        resourceLookup = resourceLookup.ToLower().Trim();
        var searchNamespace = SearchNamespace.ToLower();

        // 清除缓存
        _resourceCache.Remove(resourceLookup);

        // 查找并更新资源字典
        for (var i = 0; i < applicationDictionaries.Count; i++)
        {
            var dictionary = applicationDictionaries[i];
            if (dictionary == null)
                continue;

            // 检查直接源
            if (dictionary.Source != null)
            {
                var sourceUri = dictionary.Source.ToString().ToLower().Trim();
                if (sourceUri.Contains(searchNamespace) && sourceUri.Contains(resourceLookup))
                {
                    try
                    {
                        applicationDictionaries[i] = new() { Source = newResourceUri };
                        return true;
                    }
                    catch { }
                }
            }

            // 检查嵌套的MergedDictionaries
            if (dictionary.MergedDictionaries != null)
            {
                for (var j = 0; j < dictionary.MergedDictionaries.Count; j++)
                {
                    var nestedDictionary = dictionary.MergedDictionaries[j];
                    if (nestedDictionary == null || nestedDictionary.Source == null)
                        continue;

                    var sourceUri = nestedDictionary.Source.ToString().ToLower().Trim();
                    if (sourceUri.Contains(searchNamespace) && sourceUri.Contains(resourceLookup))
                    {
                        try
                        {
                            dictionary.MergedDictionaries[j] = new() { Source = newResourceUri };
                            return true;
                        }
                        catch { }
                    }
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 获取应用程序的合并字典集合
    /// </summary>
    private Collection<ResourceDictionary> GetApplicationMergedDictionaries()
    {
        return UiApplication.Current?.Resources?.MergedDictionaries ?? new Collection<ResourceDictionary>();
    }

    /// <summary>
    /// 清除资源缓存，当应用程序主题更改时使用
    /// </summary>
    public void ClearCache()
    {
        _resourceCache.Clear();
    }
}