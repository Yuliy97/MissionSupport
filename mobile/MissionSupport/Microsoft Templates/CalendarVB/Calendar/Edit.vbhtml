@Code
   If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
  End If

    Dim calendarId = UrlData(0).AsInt()

    Dim calObj = New With {Key .CalendarId = calendarId,
                           Key .Name = Request("name"),
                           Key .Color = Request("color")}

    @RenderPage(Themes.GetResourcePath("Partials", "CalendarForm.vbhtml"), calObj)
End Code