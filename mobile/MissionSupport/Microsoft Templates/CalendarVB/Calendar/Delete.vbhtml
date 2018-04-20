@Code
    If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    Dim calId = UrlData(0).AsInt()

    Dim _calendar = Calendar.GetUserCalendar(WebSecurity.CurrentUserId, calId)

    If _calendar Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    If _calendar.Permissions < CInt(Permission.Own) Then
        RequestData.SetValue("InfoPaneTitle", "No Permissions to Delete Calendar " & calId)
        Return
    End If

    RequestData.SetValue("InfoPaneTitle", "Delete Calendar: " & _calendar.Name)

    ' We have confirmed that we want to delete
    If IsPost Then
        Dim numCalendars = calendar.GetUserCalendars(WebSecurity.CurrentUserId).Count
        If numCalendars <= 1 Then
            ModelState.AddFormError("You can't delete your only calendar")
        End If

        If ModelState.IsValid Then
            calendar.DeleteCalendar(calId)
            Response.Redirect("~/")
        End If
    End If
End Code
<div class="confirm">
    <div class="message center">
        Are you sure that you want to delete the calendar "@_calendar.Name"?<br />
        This will delete all the events that exist on the calendar.
    </div>
    <form method="post" action="">
        <div class="actions center">
            <input type="submit" value="Confirm" />
        </div>
    </form>
</div>