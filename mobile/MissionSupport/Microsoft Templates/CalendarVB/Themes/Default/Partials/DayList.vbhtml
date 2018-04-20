@Code
    Dim events = PageData(0)
    Dim showTime = PageData(1)
End Code
<ul>
    @For Each e In events
         If e.AllDay Then
            @<li class="calendar-name rounded-top rounded-bottom" style="background-color: @(e.Color); border-color: @(ColorHelper.GetBorderFromHtml(e.Color));">
                <a href="@Href("~/Event/Info", e.Id)" title="Event Info">@e.Name</a>
            </li>
         Else 
            @<li>
                @If showTime IsNot Nothing AndAlso CBool(showTime) Then
                @<label>@Time.UtcToLocal(e.Start).ToString("t") - @Time.UtcToLocal(e.End).ToString("t")</label> 
                End If
                <span class="swatch rounded-top rounded-bottom" style="background-color: @(e.Color); border-color: @(ColorHelper.GetBorderFromHtml(e.Color));">&nbsp;</span>
                <span><a href="@Href("~/Event/Info", e.Id)" title="Event Info">@e.Name</a></span>
            </li>
        End If
    Next e
</ul>