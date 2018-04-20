@Code
    Dim myCalendars = PageData(0)
End Code
<div id="myCalendars" class="nav-section rounded-top">
    <h3 class="rounded-top border-bottom">
        My Calendars - <a href="@Href("~/Calendar/Add")">Add</a>
    </h3>
    <ul>
        @* Show all of the ones that the user owns first *@
        @For Each _calendar In myCalendars
            @<li class="calendar-name rounded-top rounded-bottom" style="background-color: @(_calendar.Color); border-color: @ColorHelper.GetBorderFromHtml(_calendar.Color)">
                <a href="@Href("~/Calendar/Info", _calendar.CalendarId)" title="Calendar Info">@_calendar.Name </a>
            </li>
        Next _calendar
    </ul>
</div>