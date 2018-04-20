@Code
    Dim cookie = Request.Cookies.Get("CalendarTheme")
    Dim theme = If(cookie IsNot Nothing, cookie.Value, "Default")

    Themes.CurrentTheme = theme

    Dim user = UserHelper.CurrentUser

    ' If someone is logged in, set the timezone.
    If user IsNot Nothing Then
        Time.LocalTimeZoneId = user.TimeZone
    End If

    Page.User = user
End Code