@Code
    Layout = Nothing
    If Not UrlData.Any() Then
        @:No Calendar Specified
        Return
    End If
    If UrlData.Count < 2 Then
        @:Calendar Id or Hash not specified
    End If

    Dim calendarId = System.IO.Path.GetFileNameWithoutExtension(UrlData(0)).AsInt()

    Dim _calendar = Calendar.GetCalendarByHash(calendarId, UrlData(1))

    If _calendar Is Nothing Then
        @:Invalid Calendar Id or Hash
         Return
    End If

Dim icsCalendar = IcsHelper.BuildCalendar(_calendar.Id)

    Response.ClearContent()
    Response.AppendHeader("Content-Type", "application/octet-stream") ' "text/calendar");
Response.AppendHeader("Content-Disposition:", String.Format("attachment; filename={0}", HttpUtility.UrlPathEncode(_calendar.Name & ".ics")))
    Response.Write(icsCalendar.ToString())
    Return
End Code