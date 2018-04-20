@Code

    Dim db = UserHelper.DatabaseInstance

    ' The beggining of the local day
    Dim startDay = Time.LocalToUtc(Time.NowInLocal.Date)

    Dim events = db.Query("SELECT TOP (30) " &
                          "e.Id, e.Name, e.AllDay, e.Start, e.[End], cu.Color " & 
                          "FROM Events AS e " & 
                          "JOIN Calendars AS c ON e.CalendarID = c.Id " &
                          "JOIN CalendarsUsers AS cu ON c.Id = cu.CalendarId " &
                          "JOIN Users AS u ON cu.UserId = u.Id " &
                          "WHERE u.Id = @0 " &
                          "AND e.Start >= @1 ", 
                          WebSecurity.CurrentUserId,
                          startDay)

    Dim dayGroups =
        From e In CType(events, IEnumerable(Of Object))
        Group e By GroupKey = Time.UtcToLocal(e.Start).Date Into g = Group
        Order By GroupKey
        Select New With {Key .Date = GroupKey, Key .Events = g.OrderByDescending(Function(e) e.AllDay).ThenBy(Function(e) e.Start)}
End Code

@For Each g In dayGroups
    @<fieldset class="list-day">
        <legend>@g.Date.ToString("d")</legend>
        @Code
            @RenderPage(Themes.GetResourcePath("Partials", "DayList.vbhtml"), g.Events, True)
        End Code
    </fieldset>
Next g