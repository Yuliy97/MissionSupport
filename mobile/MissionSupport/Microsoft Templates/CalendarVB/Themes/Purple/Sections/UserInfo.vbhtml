<div id="userInfo">
    <ul>
        <li>
            Welcome <strong>@UserHelper.CurrentUser.Email</strong>
        </li>
        <li>The current time is @Time.NowInLocal.ToString("MMM dd, yyy h:mm tt")</li>
        <li><a href="@Href("~/Account/Logout")">Logout</a></li>
    </ul>
</div>