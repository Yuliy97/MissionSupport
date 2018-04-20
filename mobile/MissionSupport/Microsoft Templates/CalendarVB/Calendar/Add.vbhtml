@Code
   RequestData.SetValue("InfoPaneTitle", "Add a Calendar")

    Dim calObj = New With {Key .Name = Request("name"),
                           Key .Color = Request("color")}

    @RenderPage(Themes.GetResourcePath("Partials", "CalendarForm.vbhtml"), calObj)
End Code