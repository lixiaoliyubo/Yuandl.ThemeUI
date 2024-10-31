// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
using System.Collections.ObjectModel;

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

    public ResourceDictionaryManager(string searchNamespace)
    {
        SearchNamespace = searchNamespace;
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
        Collection<ResourceDictionary> applicationDictionaries = GetApplicationMergedDictionaries();

        if (applicationDictionaries.Count == 0)
        {
            return null;
        }

        resourceLookup = resourceLookup.ToLower().Trim();

        foreach (ResourceDictionary t in applicationDictionaries)
        {
            string resourceDictionaryUri;

            if (t?.Source != null)
            {
                resourceDictionaryUri = t.Source.ToString().ToLower().Trim();

                if (
                    resourceDictionaryUri.Contains(SearchNamespace)
                    && resourceDictionaryUri.Contains(resourceLookup)
                )
                {
                    return t;
                }
            }

            foreach (ResourceDictionary t1 in t!.MergedDictionaries)
            {
                if (t1?.Source == null)
                {
                    continue;
                }

                resourceDictionaryUri = t1.Source.ToString().ToLower().Trim();

                if (
                   !resourceDictionaryUri.Contains(SearchNamespace)
                    || !resourceDictionaryUri.Contains(resourceLookup)
                )
                {
                    continue;
                }

                return t1;
            }
        }

        return null;
    }

    /// <summary>
    /// 显示应用程序是否包含 <see cref="ResourceDictionary"/>。
    /// </summary>
    /// <param name="resourceLookup">资源名称的任何部分。</param>
    /// <param name="newResourceUri">替换资源的有效 <see cref="Uri"/>。</param>
    /// <returns>如果字典 <see cref="Uri"/> 已更新，则返回 <see langword="true"/>。否则返回 <see langword="false"/>。</returns>
    public bool UpdateDictionary(string resourceLookup, Uri newResourceUri)
    {
        Collection<ResourceDictionary> applicationDictionaries = UiApplication
          .Current
          .Resources
          .MergedDictionaries;

        if (applicationDictionaries.Count == 0 || newResourceUri is null)
        {
            return false;
        }

        resourceLookup = resourceLookup.ToLower().Trim();

        for (var i = 0; i < applicationDictionaries.Count; i++)
        {
            string sourceUri;

            if (applicationDictionaries[i]?.Source != null)
            {
                sourceUri = applicationDictionaries[i].Source.ToString().ToLower().Trim();

                if (sourceUri.Contains(SearchNamespace) && sourceUri.Contains(resourceLookup))
                {
                    applicationDictionaries[i] = new() { Source = newResourceUri };

                    return true;
                }
            }

            for (var j = 0; j < applicationDictionaries[i].MergedDictionaries.Count; j++)
            {
                if (applicationDictionaries[i].MergedDictionaries[j]?.Source == null)
                {
                    continue;
                }

                sourceUri = applicationDictionaries[i]
                   .MergedDictionaries[j]
                   .Source.ToString()
                   .ToLower()
                   .Trim();

                if (!sourceUri.Contains(SearchNamespace) || !sourceUri.Contains(resourceLookup))
                {
                    continue;
                }

                applicationDictionaries[i].MergedDictionaries[j] = new() { Source = newResourceUri };

                return true;
            }
        }

        return false;
    }

    private Collection<ResourceDictionary> GetApplicationMergedDictionaries()
    {
        return UiApplication.Current.Resources.MergedDictionaries;
    }
}