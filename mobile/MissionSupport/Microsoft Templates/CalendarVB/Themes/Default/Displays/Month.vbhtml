@Code
    ' Store some time variables for our timezone
    Dim now = Time.NowInLocal

    ' Get the request variables (query string)
    ' If they were set, store them in the session

    If Not Request("year").IsEmpty() Then
        Session("Year") = Request("year").AsInt()
    End If
    If Not Request("month").IsEmpty() Then
        Session("Month") = Request("month").AsInt()
    End If

    ' Get the year, if there is nothing in the session, we want the current stuff
    Dim year = If(Session("Year") IsNot Nothing, CInt(Fix(Session("Year"))), now.Year)
    Dim month = If(Session("Month") IsNot Nothing, CInt(Fix(Session("Month"))), now.Month)

    ' Get the first day of the month
    Dim firstDayOfMonth = New Date(year, month, 1)

    Dim previousMonth = firstDayOfMonth.AddDays(-1)
    Dim daysInPreviousMonth = Date.DaysInMonth(previousMonth.Year, previousMonth.Month)

    Dim numberOfDaysInMonth = Date.DaysInMonth(year, month)

    ' This is midnight morning of the first day of next month: 4/1/2010 12:00:00 AM
    Dim lastDayOfMonth = firstDayOfMonth.AddDays(numberOfDaysInMonth)

    Dim dayOfWeek = firstDayOfMonth.DayOfWeek

    ' Number of blank cells we need
    Dim numBlankCells = CInt(dayOfWeek)

    ' We use this to know when to make a new row (%7)
    Dim dayCounter = 1

    ' What day of the month are we on.
    Dim dayNumber = 1

    Dim db = UserHelper.DatabaseInstance

    Dim events = db.Query("SELECT " &
                          "e.Id, e.Name, e.AllDay, e.Start, e.[End], cu.Color " &
                          "FROM Events AS e " & 
                          "JOIN Calendars AS c ON e.CalendarID = c.Id " & 
                          "JOIN CalendarsUsers AS cu ON c.Id = cu.CalendarId " & 
                          "JOIN Users AS u ON cu.UserId = u.Id " & 
                          "WHERE u.Id = @0 " & 
                          "AND e.Start >= @1 " & 
                          "AND e.[End] <= @2 ",
                          WebSecurity.CurrentUserId,
                          firstDayOfMonth, 
                          lastDayOfMonth)

    Dim dayGroups = (
        From e In CType(events, IEnumerable(Of Object))
        Group e By GroupKey = Time.UtcToLocal(e.Start).Date Into g = Group
        Order By GroupKey
        Select New With {Key .Date = GroupKey,
            Key .Events = g.OrderByDescending(
            Function(e) e.AllDay).ThenBy(Function(e) e.Start)}).ToDictionary(Function(g) g.Date.Day)

    Dim dayGroupIndex = 0
End Code

<table class="month-view">
    <caption>
        <a href="?year=@(previousMonth.Year)&amp;month=@(previousMonth.Month)" title="Previous Month">&laquo;</a>
        @firstDayOfMonth.ToString("MMMM")
        <a href="?year=@(lastDayOfMonth.Year)&amp;month=@(lastDayOfMonth.Month)" title="Next Month">&raquo;</a>
        <span style="float: right"><a href="?year=@(now.Year)&amp;month=@(now.Month)">Today</a></span>
    </caption>
    <thead>
        <tr>
            <th scope="col">Sun</th>
            <th scope="col">Mon</th>
            <th scope="col">Tue</th>
            <th scope="col">Wed</th>
            <th scope="col">Thu</th>
            <th scope="col">Fri</th>
            <th scope="col">Sat</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            @* Print out all of the blanks so we start on the right day*@
            @For i = numBlankCells - 1 To 0 Step -1
                @<td class="inactive">
                    <span class="day-number">
                        @(daysInPreviousMonth-i)
                    </span>
                </td>
                dayCounter+=1
            Next i

            @* Go through every day in this month *@
            @Do While dayNumber <= numberOfDaysInMonth
                Dim className = ""
                If dayNumber = now.Day AndAlso year = now.Year AndAlso month = now.Month Then
                    className = "today"
                End If
                @<td class="@className">
                    <span class="day-number">@dayNumber</span>                

                    @* If our set of days contains events for the current day *@
                    @If dayGroups.ContainsKey(dayNumber) Then
                    @RenderPage(Themes.GetResourcePath("Partials", "DayList.vbhtml"), dayGroups(dayNumber).Events, False)
                    dayGroupIndex += 1
                        End If
                </td>
                dayCounter+=1
                dayNumber+=1
                @* If we are at the end of the week, go to the next row *@
                If dayCounter Mod 8 = 0 AndAlso dayNumber -1 <> numberOfDaysInMonth Then
                    @:</tr><tr>
                    dayCounter += 1
                    End If
            Loop
            @* Finish this week display *@
            @Code
                dayNumber = 1
                Do While dayCounter Mod 8 > 1 AndAlso dayCounter Mod 8 <= 7
                    @<td class="inactive">
                       <span class="day-number">@dayNumber</span>
                    </td>
                    dayNumber += 1
                    dayCounter += 1
                Loop
            End Code
        </tr>
    </tbody>
</table>