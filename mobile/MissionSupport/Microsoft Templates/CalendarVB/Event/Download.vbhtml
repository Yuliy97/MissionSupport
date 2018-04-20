@Code
    Layout = Nothing
    If Not UrlData.Any() Then
        @:No Event Specified
         Return
    End If

Dim eventId = UrlData(0).AsInt()

Dim eventInfo = [Event].GetUserEvent(WebSecurity.CurrentUserId, eventId)

    If eventInfo Is Nothing Then
        @:Invalid Event Id 
    Return
    End If

    If eventInfo.Permissions < CInt(Fix(Permission.Edit)) Then
        @:"No Permissions to Delete Event "eventId 
    Return
    End If

    Dim file = New StringWriter
    file.Write(IcsHelper.CalendarHeaders)
    file.WriteLine("METHOD:REQUEST")
    file.Write(IcsHelper.BuildEvent(eventInfo))

    file.Write(IcsHelper.CalendarFooters)

    Response.ClearContent()
    Response.AppendHeader("Content-Type", "text/calendar")
    Response.AppendHeader("Content-Disposition:", String.Format("attachment; filename={0}", HttpUtility.UrlPathEncode(eventInfo.Name & ".ics")))
    Response.Write(file.ToString())
    Return
End Code