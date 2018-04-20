@Code
    Dim eventId? As Integer = Page.EventId

    ' Gross variable that stores the current event (IF we are on an edit page)
    Dim eventInfo = Nothing
    Dim calendars = Calendar.GetUserCalendars(WebSecurity.CurrentUserId, Permission.Edit)

    ' These are the times to use for the form.
    Dim start As Date
    Dim [end] As Date

    ' We are editing a page
    If eventId.HasValue Then
        ' Check if the event id is tied to the current user
        eventInfo = [Event].GetUserEvent(WebSecurity.CurrentUserId, eventId.Value)

        If eventInfo Is Nothing Then
            RequestData.SetValue("InfoPaneTitle", "Invalid Event Id")
            Return
        End If
        RequestData.SetValue("InfoPaneTitle", "Edit Event: " & eventInfo.Name)

        start = Time.UtcToLocal(eventInfo.Start)
        [end] = Time.UtcToLocal(eventInfo.End)
    Else
        RequestData.SetValue("InfoPaneTitle", "Add an Event")

        start = Time.NowInLocal
        Dim halfHour = TimeSpan.FromMinutes(30)
        [end] = start.Add(halfHour)
    End If

    If IsPost Then
        Dim name = Page.Name
        If String.IsNullOrEmpty(name) Then
            ModelState.AddError("name", "You must specify an event name.")
        End If

        Dim description = Page.Description

        Dim allDay = If(Page.AllDay = "on", True, False)

        start = Time.NowInLocal
        [end] = Time.NowInLocal

        ' Validate the start and end date/times.

        If (Not Date.TryParse(Page.StartDate & " " & Page.StartTime, start)) OrElse
            (Not Date.TryParse(Page.StartDate & " " & Page.EndTime, [end])) Then
            ModelState.AddFormError("One of the date / time strings were unreadable.")
        End If

        Dim location = Page.Location
        If (Not String.IsNullOrEmpty(location)) AndAlso (location.Length > 100) Then
            ModelState.AddError("location", "If you specify a location, it must be less than or equal to 100 chars.")
        End If

        Dim calendarId = Int32.Parse(Page.CalendarId)
        ' Find out if we can make an event on the given calendars
        Dim canMakeEvent = False
        Dim editableCalendars = Calendar.GetUserCalendars(WebSecurity.CurrentUserId, Permission.Edit)

        For Each calendar In editableCalendars
            If calendar.CalendarId = calendarId Then
                canMakeEvent = True
                Exit For
            End If
        Next calendar

        If Not canMakeEvent Then
            ModelState.AddError("calendar", "You can't add events to the specified calendar")
        End If

        If ModelState.IsValid Then
            If eventId.HasValue Then
                [Event].EditEvent(eventId.Value,
                                  calendarId,
                                  name,
                                  description,
                                  location,
                                  Time.LocalToUtc(start),
                                  Time.LocalToUtc([end]),
                                  Convert.ToBoolean(allDay))
            Else
                eventId = [Event].AddEvent(WebSecurity.CurrentUserId,
                                           calendarId,
                                           name,
                                           description,
                                           location,
                                           Time.LocalToUtc(start),
                                           Time.LocalToUtc([end]),
                                           Convert.ToBoolean(allDay))
            End If
            Response.Redirect("~/Event/Info/" & eventId)
        End If
    End If
End Code
@Html.ValidationSummary()
<form method="post" action="">
    <table class="info">
        <tr>
            <td><label for="name">Event Name</label></td>
            <td><input type="text" id="name" name="name" value="@(If(eventId.HasValue , eventInfo.Name , ""))" /></td>
        </tr>
        <tr>
            <td><label for="description">Event Description</label></td>
            <td><textarea id="description" name="description" rows="3" cols="15">@(If(eventId.HasValue , eventInfo.Description , ""))</textarea></td>
        </tr>
        <tr>
            <td><label for="location">Event Location</label></td>
            <td><input type="text" id="location" name="location" value="@(If(eventId.HasValue , eventInfo.Location , ""))" /></td>
        </tr>
        <tr>
            <td><label for="allDay">All Day Event</label></td>
            <td><input type="checkbox" id="allDay" name="allDay" @(If(eventId.HasValue AndAlso eventInfo.AllDay , " checked=""checked""" , "")) /></td>
        </tr>
        <tr>
            <td>Start</td>
            <td>
                <label for="sDate">Date:</label> <input type="text" class="datepicker" id="sDate" name="sDate" value="@start.ToString("d")" /> <label for="sTime">Time:</label> <input type="text" id="sTime" name="sTime" value="@start.ToString("t")" />
            </td>
        </tr>
        <tr>
            <td>End</td>
            <td>
                <label for="eTime">Time:</label> <input type="text" id="eTime" name="eTime" value="@([end].ToString("t"))" />
            </td>
        </tr>
        <tr>
            <td><label for="calendar">Calendar Name</label></td>
            <td>
                <select id="calendar" name="calendar">
                    @For Each _calendar In calendars
                        @<option style="background-color: @_calendar.Color" value="@_calendar.CalendarId" @(If(eventId.HasValue AndAlso (_calendar.CalendarId = eventInfo.CalendarID), " selected=""selected""" , ""))>
                            @_calendar.Name
                        </option>
                    Next _calendar
                </select>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="center"><input type="submit" name="submit" value="@(If(eventId.HasValue , "Save" , "Create"))" /></td>
        </tr>
    </table>
</form>