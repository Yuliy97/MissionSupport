@Code
    Dim cookie = Request.Cookies.Get("CalendarView")
    Dim view = If(cookie IsNot Nothing, cookie.Value, "Month")
    Dim page As String = Nothing
End Code
@If view = "List" Then
    page = Themes.GetResourcePath("Displays", "List.vbhtml")
Else
    page = Themes.GetResourcePath("Displays", "Month.vbhtml")
End If
@RenderPage(page)