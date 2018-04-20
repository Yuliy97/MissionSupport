@Code
    If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Event Id")
        Return
    End If

    Dim eventId = UrlData(0).AsInt()

    Dim eventInfo = [Event].GetUserEvent(WebSecurity.CurrentUserId, eventId)

    If eventInfo Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Event Id")
        Return
    End If

    If eventInfo.Permissions < CInt(Fix(Permission.Edit)) Then
        RequestData.SetValue("InfoPaneTitle", "No Permissions to Delete Event " & eventId)
        Return
    End If

    RequestData.SetValue("InfoPaneTitle", "Delete Event: " & eventInfo.Name)

    ' We have confirmed that we want to delete
    If IsPost Then
        [Event].DeleteEvent(eventId)
        Response.Redirect("~/")
    End If
End Code
<div class="confirm">
    <div class="message center">
        Are you sure that you want to delete the event "@eventInfo.Name"?
    </div>
    <form action="" method="post">
        <div class="actions center">
            <input type="submit" value="Confirm" />
        </div>
    </form>
</div>