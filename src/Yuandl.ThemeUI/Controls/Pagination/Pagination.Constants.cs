// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright(C) Yuandl ThemeUI. All Rights Reserved.
namespace Yuandl.ThemeUI.Controls;

public partial class Pagination
{
    private const string C_numberBoxVisibleVisualState = "NumberBoxVisible";
    private const string C_comboBoxVisibleVisualState = "ComboBoxVisible";
    private const string C_numberPanelVisibleVisualState = "NumberPanelVisible";

    private const string C_firstPageButtonVisibleVisualState = "FirstPageButtonVisible";
    private const string C_firstPageButtonNotVisibleVisualState = "FirstPageButtonCollapsed";
    private const string C_firstPageButtonHiddenVisualState = "FirstPageButtonHidden";
    private const string C_firstPageButtonEnabledVisualState = "FirstPageButtonEnabled";
    private const string C_firstPageButtonDisabledVisualState = "FirstPageButtonDisabled";

    private const string C_previousPageButtonVisibleVisualState = "PreviousPageButtonVisible";
    private const string C_previousPageButtonNotVisibleVisualState = "PreviousPageButtonCollapsed";
    private const string C_previousPageButtonHiddenVisualState = "PreviousPageButtonHidden";
    private const string C_previousPageButtonEnabledVisualState = "PreviousPageButtonEnabled";
    private const string C_previousPageButtonDisabledVisualState = "PreviousPageButtonDisabled";

    private const string C_nextPageButtonVisibleVisualState = "NextPageButtonVisible";
    private const string C_nextPageButtonNotVisibleVisualState = "NextPageButtonCollapsed";
    private const string C_nextPageButtonHiddenVisualState = "NextPageButtonHidden";
    private const string C_nextPageButtonEnabledVisualState = "NextPageButtonEnabled";
    private const string C_nextPageButtonDisabledVisualState = "NextPageButtonDisabled";

    private const string C_lastPageButtonVisibleVisualState = "LastPageButtonVisible";
    private const string C_lastPageButtonNotVisibleVisualState = "LastPageButtonCollapsed";
    private const string C_lastPageButtonHiddenVisualState = "LastPageButtonHidden";
    private const string C_lastPageButtonEnabledVisualState = "LastPageButtonEnabled";
    private const string C_lastPageButtonDisabledVisualState = "LastPageButtonDisabled";

    private const string C_finiteItemsModeState = "FiniteItems";
    private const string C_infiniteItemsModeState = "InfiniteItems";

    private const string C_comboBoxName = "ComboBoxDisplay";
    private const string C_numberBoxName = "NumberBoxDisplay";

    private const string C_numberPanelRepeaterName = "NumberPanelItemsRepeater";
    private const string C_numberPanelIndicatorName = "NumberPanelCurrentPageIndicator";
    private const string C_firstPageButtonName = "FirstPageButton";
    private const string C_previousPageButtonName = "PreviousPageButton";
    private const string C_nextPageButtonName = "NextPageButton";
    private const string C_lastPageButtonName = "LastPageButton";

    private const string C_numberPanelButtonStyleName = "DefaultUiButtonStyle";
    private const int C_AutoDisplayModePageCountThreshold = 10;
    private const int C_infiniteModeComboBoxItemsIncrement = 100;
}