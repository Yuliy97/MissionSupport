@Code

    If Not UrlData.Any() Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    Dim calId = UrlData(0).AsInt()

    Dim calInfo = Calendar.GetUserCalendar(WebSecurity.CurrentUserId, calId)

    If calInfo Is Nothing Then
        RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
        Return
    End If

    ' Check that the current user has the ability to share the calendar
    If calInfo.Permissions < CInt(Permission.Own) Then
        RequestData.SetValue("InfoPaneTitle", "No Permissions to Delete Calendar " & calId)
        Return
    End If

    RequestData.SetValue("InfoPaneTitle", "Calendar: " & calInfo.Name)

    If IsPost Then
        Dim email = Request("email")
        If email.IsEmpty() Then
            ModelState.AddError("email", "You must specify a user name.")
        ElseIf Not UserHelper.UserExists(email) Then
            ' check that the user exist
            ModelState.AddError("email", "No user exists with that email.")
        ElseIf UserHelper.CurrentUser.Email.Equals(email, StringComparison.OrdinalIgnoreCase) Then
            ModelState.AddError("email", "You can't share a calendar with yourself.")
        End If

        Dim _permission = Request("permission")
        Dim result = Permission.View

        If _permission.IsEmpty() Then
            ModelState.AddError("permission", "You must specify a permission level")
        ElseIf Not System.Enum.TryParse(Of Permission)(_permission, result) Then
            ' validate the specified permission level
            ModelState.AddError("permission", "You must specify a valid permission level")
        End If

        If ModelState.IsValid Then
            Sharing.ShareCalendar(calId, WebSecurity.GetUserId(email), result)
            Response.Redirect("~/Calendar/Info/" & calId)
        End If
    End If
End Code

@Html.ValidationSummary()
    <form method="post" action="">
        <table class="info">
            <tr>
                <td><label for="email">Share With</label></td>
                <td>@Html.TextBox("email")</td>
            </tr>
            <tr>
                <td><label for="permission">Permission Level</label></td>
                <td>
                    <select id="permission" name="permission">
                        @For Each level In System.Enum.GetValues(GetType(Permission))
                            @<option value="@(CInt(level))">@level</option>
                        Next level
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2" id="submit"><input type="submit" name="submit" value="Share" /></td>
            </tr>
        </table>
    </form>