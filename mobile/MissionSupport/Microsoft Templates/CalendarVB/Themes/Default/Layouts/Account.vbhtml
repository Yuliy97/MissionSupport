@Code
    Layout = Themes.GetResourcePath("Layouts", "Main.vbhtml")
End Code

<div id="accountBox">
    <h2 class="border-bottom">@Page.Title</h2>
    <div>
        @RenderBody()
    </div>
</div>