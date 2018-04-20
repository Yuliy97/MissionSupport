@Code
    If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Event Id")
        Return
    End If

    Dim eventId = UrlData(0).AsInt()

    Dim eventObj = New With {Key .EventId = eventId,
                             Key .Name = Request("name"),
                             Key .Description = Request("description"),
                             Key .AllDay = Request("allDay"),
                             Key .StartDate = Request("sDate"),
                             Key .StartTime = Request("sTime"),
                             Key .EndTime = Request("eTime"),
                             Key .Location = Request("location"),
                             Key .CalendarId = Request("calendar")}

    @RenderPage(Themes.GetResourcePath("Partials", "EventForm.vbhtml"), eventObj)
End Code