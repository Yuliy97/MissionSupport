@Code
    Dim calendar = Page.Calendar
    RequestData.SetValue("InfoPaneTitle", "Manage Sharing for: " & calendar.Name)
    Dim users = Sharing.GetUsersWithAccess(calendar.CalendarId)

    If IsPost Then
        ' We are adding a user
        Dim email = Request("email")
        If email.IsEmpty() Then
            ModelState.AddError("email", "You must specify a user name.")
        ElseIf Not UserHelper.UserExists(email) Then
            ' check that the user exist
            ModelState.AddError("email", "No user exists with that username.")
        ElseIf email = UserHelper.CurrentUser.Email Then
            ModelState.AddError("email", "You can't share a calendar with yourself.")
        End If

        For Each _User In users
            If _User.Email = email Then
                ModelState.AddError("email", "That user is already able to access the calendar")
            End If
        Next _User

        Dim _permission = Request("permission")
        Dim result = Permission.View

        If _permission.IsEmpty() Then
            ModelState.AddError("permission", "You must specify a permission level")
        ElseIf Not System.Enum.TryParse(Of Permission)(_permission, result) Then
            ' validate the specified permission level
            ModelState.AddError("permission", "You must specify a valid permission level")
        End If

        If ModelState.IsValid Then
            Sharing.ShareCalendar(calendar.CalendarId, WebSecurity.GetUserId(email), result)
            Response.Redirect("~/Calendar/Share/View/" & calendar.CalendarId)
        End If
    End If
End code
@Html.ValidationSummary()
<form method="post" action="">
    <table class="info">
        <tr>
            <th scope="col">Email</th>
            <th scope="col">Permission Level</th>
            <th></th>
        </tr>
        <tr>
            <td>
                <label>
                    <span style="margin-right: 1em">Add A User:</span>
                    <input type="text" name="email" value="" />
                </label>
            </td>
            <td>
                <select name="permission">
                    @For Each level In System.Enum.GetValues(GetType(Permission))
                        @<option value="@(CInt(level))">@level</option>
                    Next level
                </select>
            </td>
            <td><input type="submit" value="Add" /></td>
        </tr>
        @For Each user As Object In users
            ' Don't show the user who is sharing. This keeps them from removing themselves
            If user.Email = Page.User.Email Then
                Continue For
            End If
        @<tr>
            <td>@user.Email</td>
            <td>@(CType(user.Permissions, Permission))</td>
            <td> <a href="@Href("~/Calendar/Share/Edit", calendar.CalendarId, user.Id)" title="Edit Calendar">Change Permission</a></td>
        </tr>
        Next user
    </table>
</form>