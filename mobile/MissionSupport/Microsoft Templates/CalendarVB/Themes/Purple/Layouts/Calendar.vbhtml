@Code
    Layout = Themes.GetResourcePath("Layouts", "Main.vbhtml")
End Code
<div id="mainBody">
    <div id="mainSection">
        <div id="calendarBar" class="border-bottom">
            <div id="calendarLinks">
                <span>
                    <a href="@Href("~/Event/Add")" title="Add Event">
                        <img src="@Themes.GetResourcePath("Images", "AddEvent16x16.png")" alt="Add Event" title="Add Event" />
                    </a>
                </span>
            </div>
            <div id="calendarViews">
                <a href="@Href("~/Calendar/ChangeView/List")">List</a> -
                <a href="@Href("~/Calendar/ChangeView/Month")">Month</a>
            </div>
        </div>

        @If RequestData.GetValue("InfoPaneTitle") IsNot Nothing Then
        @<div id="information">
            <h2 class="border-bottom">@RequestData.GetValue("InfoPaneTitle")</h2>
            <div id="informationPane">
                @RenderBody()
            </div>
        </div>
        Else
            @RenderBody()
        End If
        
        <div id="calendarPane">
            @RenderPage(Themes.GetResourcePath("Sections", "CalendarView.vbhtml"))
        </div>
    </div>
    <div id="leftBar">
        @RenderPage(Themes.GetResourcePath("Sections", "LeftBar.vbhtml"))
    </div>
</div>