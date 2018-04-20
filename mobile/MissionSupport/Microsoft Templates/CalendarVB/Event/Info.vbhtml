@Code
   If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Event Id")
        Return
  End If

    Dim eventId = UrlData(0).AsInt()

    ' Check if the event id is tied to the current user
    Dim eventInfo = [Event].GetUserEvent(WebSecurity.CurrentUserId, eventId)

    If eventInfo Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Event Id")
        Return
    End If

    RequestData.SetValue("InfoPaneTitle", "Event: " & eventInfo.Name)

    Dim start = Time.UtcToLocal(eventInfo.Start)
    Dim [end] = Time.UtcToLocal(eventInfo.End)
End Code
<fieldset>
    <table class="info">
        <tr>
            <td>Event Name</td>
            <td>@eventInfo.Name</td>
        </tr>
        <tr>
            <td>Event Description</td>
            <td>@eventInfo.Description</td>
        </tr>
        <tr>
            <td>Event Location</td>
            <td>@eventInfo.Location</td>
        </tr>
        <tr>
            <td>All Day Event</td>
            <td>@eventInfo.AllDay</td>
        </tr>
        @If eventInfo.AllDay
            @<tr>
                <td>Date</td>
                <td>
                    @start.ToString("d")
                </td>
            </tr>
        Else
        @<tr>
            <td>Start</td>
            <td>
                @start
            </td>
        </tr>
        @<tr>
            <td>End</td>
            <td>
                @([end])
            </td>
        </tr>
        End If
        <tr>
            <td>Calendar Name</td>
            <td><span class="swatch rounded-top rounded-bottom" style="background-color: @(eventInfo.Color); border-color: @(ColorHelper.GetBorderFromHtml(eventInfo.Color));">&nbsp;</span>@eventInfo.CalendarName</td>
        </tr>
        <tr>
            <td colspan="2" class="center">
                @If eventInfo.Permissions >= CInt(Permission.Edit) Then
                    @<text>
                        <a href="@Href("~/Event/Edit", eventInfo.Id)">Edit</a> - 
                        <a href="@Href("~/Event/Delete", eventInfo.Id)">Delete</a> - 
                    </text>
                End If
                <a href="@Href("~/Event/Download", eventInfo.Id)">Download</a>
            </td>
        </tr>
    </table>
</fieldset>