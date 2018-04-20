@Code
    Dim sharedCalendars = PageData(0)
End Code
<div id="sharedCalendars" class="nav-section rounded-top">
    <h3 class="rounded-top border-bottom">
        Shared Calendars
    </h3>
    <ul>
        @If String.IsNullOrEmpty(sharedCalendars.ToString()) Then
            @<li class="center">No Shared Calendars</li>
       Else
            @* Show all of the ones that the user owns first *@
            For Each _calendar In sharedCalendars
                @<li class="calendar-name rounded-top rounded-bottom" style="background-color: @(_calendar.Color); border-color: @ColorHelper.GetBorderFromHtml(_calendar.Color)">
                    <a href="@Href("~/Calendar/Info", _calendar.CalendarId)" title="Calendar Info">@_calendar.Name</a>
                </li>
            Next _calendar
        End If
    </ul>
</div>