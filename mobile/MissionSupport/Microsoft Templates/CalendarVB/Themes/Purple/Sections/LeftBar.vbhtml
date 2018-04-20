@Code
    Dim calendarGroups = Calendar.GetCalendarGroups(WebSecurity.CurrentUserId)

    Dim sharedCalendars As Object = String.Empty
    If calendarGroups.ContainsKey("Shared") Then
        sharedCalendars = calendarGroups("Shared")
    End If
End Code

@RenderPage(Themes.GetResourcePath("Sections", "MyCalendars.vbhtml"), calendarGroups("Mine"))
        
@RenderPage(Themes.GetResourcePath("Sections", "SharedCalendars.vbhtml"), sharedCalendars)
        
@RenderPage(Themes.GetResourcePath("Sections", "ThemeList.vbhtml"))