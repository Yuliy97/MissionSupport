@Code
    If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    Dim calId = UrlData(0).AsInt()

    ' View information about the calendar
    Dim _calendar = Calendar.GetUserCalendar(WebSecurity.CurrentUserId, calId)

    If _calendar Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    RequestData.SetValue("InfoPaneTitle", "Calendar: " & _calendar.Name)
End Code
<fieldset>
    <table class="info">
        <tr>
            <td>Calendar Name</td>
            <td>@_calendar.Name</td>
        </tr>
        <tr>
            <td>Calendar Color</td>
            <td>@_calendar.Color <span class="swatch width100 rounded-top rounded-bottom" style="background-color: @(_calendar.Color); border-color: @(ColorHelper.GetBorderFromHtml(_calendar.Color));">&nbsp;</span></td>
        </tr>
        <tr>
            <td>Created By</td>
            <td>@UserHelper.GetUser(_calendar.Creator).Email</td>
        </tr>
        <tr>
            <td colspan="2" class="center">
                 <a href="@Href("~/Calendar/Edit", _calendar.CalendarId)">Edit</a>  
                 @If _calendar.Permissions = CInt(Fix(Permission.Own)) Then
                    @<text> -
                     <a href="@Href("~/Calendar/Delete", _calendar.CalendarId)">Delete</a> -
                     <a href="@Href("~/Calendar/Share/View", _calendar.CalendarId)">Share</a>
                    </text>
                End If
                - <a href="@Href("~/Download", _calendar.CalendarId, _calendar.Guid)">Download</a>
            </td>
        </tr>
    </table>
</fieldset>