<div id="userInfo" class="nav-section rounded-top">
    <h3 class="rounded-top border-bottom">
        User
    </h3>
    <ul>
        <li>Welcome <strong>@UserHelper.CurrentUser.Email</strong></li>
        <li>@Time.NowInLocal.ToString("MMM dd, yyy h:mm tt")</li>
        <li><a href="@Href("~/Account/Logout")">Logout</a></li>
    </ul>
</div>