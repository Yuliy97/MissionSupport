@Code
    ' Grab the calendar from the id
    If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    Dim calendarId = UrlData(0).AsInt()

    ' Check that the current user "owns" the calendar
    Dim _calendar = Calendar.GetUserCalendar(WebSecurity.CurrentUserId, calendarId)

    If _calendar Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    ' If not, redirect to home
    If _calendar.Permissions < CInt(Fix(Permission.Own)) Then
        Response.Redirect("~/")
    End If

    Page.Calendar = _calendar
End Code