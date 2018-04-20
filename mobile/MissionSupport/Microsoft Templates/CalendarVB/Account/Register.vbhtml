@Code
    Page.Title = "Account Creation"

    If IsPost Then
        Dim name = Request("name")
        If name.IsEmpty() Then
            ModelState.AddError("name", "You must specify a display name.")
        End If

        Dim email = Request("email")
        If email.IsEmpty() Then
            ModelState.AddError("email", "You must specify an email address.")
        End If

        Dim timeZoneId = Request("timezone")

        If timeZoneId.IsEmpty() Then
            ModelState.AddError("timezone", "You must specify a time zone")
        End If

        ' only accept the timezone if it is in our list
        Try
            TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)
        Catch
            ModelState.AddError("timezone", "You must specify a valid time zone")
        End Try

        Dim password = Request("password")
        Dim confirmPassword = Request("confirmPassword")

        If password.IsEmpty() Then
            ModelState.AddError("password", "You must specify a password.")
        End If

        If confirmPassword.IsEmpty() Then
            ModelState.AddError("confirmPassword", "You must specify a confirm password.")
        End If

        If password <> confirmPassword Then
            ModelState.AddFormError("The new password and confirmation password do not match.")
        End If

        If ModelState.IsValid Then
            If UserHelper.UserExists(email) Then
                ModelState.AddFormError("Email address is already in use.")
            Else
                ' This and the contents of the try catch should probably be done
                ' in a transaction. What happens if we add a user, but membership
                ' isn't cool with it?
                Dim userId = UserHelper.AddUser(name, email, timeZoneId)

                Calendar.CreateCalendar(userId)

                ' Create and associate a new entry in the membership database.
                ' If successful, continue processing the request
                Try
                    WebSecurity.CreateAccount(email, password)
                    WebSecurity.Login(email, password)
                    ' It seems like there should be a "congratulations on registering"
                    ' page that would come in here.

                    Response.Redirect("~/")
                Catch e As System.Web.Security.MembershipCreateUserException
                    ModelState.AddFormError(e.ToString())
                End Try
            End If
        End If
    End If
End Code
<p>
    Use the form below to create a new account. 
</p>
@Html.ValidationSummary()
<form method="post" action="" id="accountForm">
    <div>
        <table>
            <tr>
                <td><label for="name">Display Name</label></td>
                <td>
                    @Html.TextBox("name")
                </td>
            </tr>
            <tr>
                <td><label for="email">Email</label></td>
                <td>
                    @Html.TextBox("email")
                </td>
            </tr>
            <tr>
                <td><label for="timezone">TimeZone</label></td>
                <td>
                    <select id="timezone" name="timezone">
                        @For Each timeZone In TimeZoneInfo.GetSystemTimeZones()
                            @<option value="@timeZone.Id">@timeZone.DisplayName</option>
                        Next timeZone
                    </select>
                </td>
            </tr>
            <tr>
                <td><label for="password">Password</label></td>
                <td>
                    @Html.Password("password")
                </td>
            </tr>
            <tr>
                <td><label for="confirmPassword">Confirm password</label></td>
                <td>
                    @Html.Password("confirmPassword")
                </td>
            </tr>
            <tr>
                <td colspan="2" id="submit"><input type="submit" value="Register" /></td>
            </tr>
        </table>
    </div>
</form>