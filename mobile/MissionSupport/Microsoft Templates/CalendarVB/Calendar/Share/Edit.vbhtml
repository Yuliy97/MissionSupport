@Code
    ' The user we are going to share with
    Dim userId = UrlData(1).AsInt()

    ' The user object we are going to share with
    Dim user = UserHelper.GetUser(userId)

    Dim _calendar = Page.Calendar

    If user Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "No User Exists by that Name")
    ElseIf user.Email = UserHelper.CurrentUser.Email Then
        ModelState.AddError("username", "You can't modify your own share settings.")
    End If

    ' We need to make sure that the user already has some sort of permissions on this calendar
    Dim userCalendarInfo = Calendar.GetUserCalendar(userId, _calendar.CalendarId)
    If userCalendarInfo Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar User Pair")
        Return
    End If

    RequestData.SetValue("InfoPaneTitle", "Editing User '" & user.Email & "' for Calendar '" & userCalendarInfo.Name & "'")

    If IsPost Then
        ' We are changing the permissions. Special case is if
        ' permission is 0, that means remove them. Default to -1
        ' so it doesn't count as a 0 (no permission)
        Dim permission? = Request("permission").As(Of Permission?)()

        If Not permission.HasValue Then
            ModelState.AddError("permission", "You must specify a valid permission level")
        End If

        If ModelState.IsValid Then
            Sharing.EditShareCalendar(_calendar.CalendarId, WebSecurity.GetUserId(user.Email), permission.Value)
            Response.Redirect("~/Calendar/Share/View/" & _calendar.CalendarId)
        End If
    End If
End Code
@Html.ValidationSummary()
<form method="post" action="">
    <table class="info">
        <tr>
            <th scope="col">Username</th>
            <th scope="col">Permission Level</th>
        </tr>
        <tr>
            <td>
                <label>
                    <span style="margin-right: 10px;">Add A User:</span>
                    <input type="text" name="username" value="@user.Email" />
                </label>
            </td>
            <td>
                <select name="permission">
                    @For Each level In System.Enum.GetValues(GetType(Permission))
                        @<option value="@(CInt(level))" @(If(CInt(level) = userCalendarInfo.Permissions, " selected=""selected""", ""))>@level</option>
                    Next level
                </select>
            </td>
        </tr>
        <tr>
            <td class="center" colspan="2">
                <input type="submit" value="Save" />
            </td>
        </tr>
    </table>
</form>