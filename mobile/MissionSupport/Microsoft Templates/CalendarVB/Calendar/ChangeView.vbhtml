@Code
    If UrlData.Any() Then
        ' We have set a view
        Dim view = UrlData(0)

        Dim cookie = New HttpCookie("CalendarView")
        cookie.Value = view
        cookie.Expires = Date.Now.AddMonths(2)
        Response.Cookies.Add(cookie)
    End If

    Response.Redirect("~/")
End Code