<div id="themeList" class="nav-section rounded-top">
    <h3 class="rounded-top border-bottom">
        Themes
    </h3>
    <ul>
        @For Each theme In Themes.AvailableThemes
        @<li class="calendar-name rounded-top rounded-bottom @(If(theme = Themes.CurrentTheme, " theme-selected" , " themeUnselected"))">
            <a href="@Href("~/Calendar/ChangeTheme", theme)" title="Change Theme">@theme</a>
        </li>
        Next theme        
    </ul>
</div>