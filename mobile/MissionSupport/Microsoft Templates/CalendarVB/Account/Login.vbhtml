@Code
    Page.Title = "Login"

    If IsPost Then
        Dim email = Request("email")
        If email.IsEmpty() Then
            ModelState.AddError("email", "You must specify an email address.")
        End If

        Dim password = Request("password")
        If password.IsEmpty() Then
            ModelState.AddError("password", "You must specify a password.")
        End If

        If ModelState.IsValid Then
            Dim rememberMe = Request("rememberMe").AsBool()
            If WebMatrix.WebData.WebSecurity.Login(email, password, rememberMe) Then
                Dim returnUrl = Request("returnUrl")
                If Not returnUrl.IsEmpty() Then
                    Context.RedirectLocal(returnUrl)
                Else
                    Response.Redirect("~/")
                End If
            Else
                ModelState.AddFormError("The email or password provided is incorrect.")
            End If
        End If
    End If
End Code
<p>
    Please enter your email and password below. If you don't have an account,
    please <a href="@Href("Register")">Register</a>.
</p>
@Html.ValidationSummary()
<form method="post" action="" id="accountForm">
    <div>
        <table>
            <tr>
                <td><label for="email">Email</label></td>
                <td>
                    @Html.TextBox("email")
                </td>
            </tr>
            <tr>
                <td><label for="password">Password</label></td>
                <td>
                    @Html.Password("password")
                </td>
            </tr>
            <tr>
                <td><label for="rememberMe">Remember me?</label></td>
                <td>@Html.CheckBox("rememberMe")</td>
            </tr>
            <tr>
                <td colspan="2" id="submit"><input type="submit" value="Login" /></td>
            </tr>
        </table>
    </div>
</form>