@Code
    Dim eventObj = New With {Key .Name = Request("name"),
                             Key .Description = Request("description"),
                             Key .AllDay = Request("allDay"),
                             Key .StartDate = Request("sDate"),
                             Key .StartTime = Request("sTime"),
                             Key .EndTime = Request("eTime"),
                             Key .Location = Request("location"),
                             Key .CalendarId = Request("calendar")}

    @RenderPage(Microsoft.Web.Helpers.Themes.GetResourcePath("Partials", "EventForm.vbhtml"), eventObj)
End Code