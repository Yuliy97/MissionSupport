@Code
    If UrlData.Any() Then
        ' We have set a theme
        Dim theme = UrlData(0)

        Dim cookie = New HttpCookie("CalendarTheme")
        cookie.Value = theme
        cookie.Expires = Date.Now.AddMonths(2)
        Response.Cookies.Add(cookie)
    End If

    Response.Redirect("~/")
End Code