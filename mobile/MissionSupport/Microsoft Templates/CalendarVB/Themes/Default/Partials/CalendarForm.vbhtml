@Code
 Dim calendarId? As Integer = Page.CalendarId
    Dim calendarInfo As Object = Nothing

    If calendarId.HasValue Then
        ' View information about the calendar
        calendarInfo = Calendar.GetUserCalendar(WebSecurity.CurrentUserId, calendarId.Value)

        ' If the calendar doesn't exist
        If calendarInfo Is Nothing Then
            RequestData.SetValue("InfoPaneTitle", "Invalid Calendar Id")
            Return
        End If

        RequestData.SetValue("InfoPaneTitle", "Calendar: " & calendarInfo.Name)
    End If

    If IsPost Then
        Dim name As String = Page.Name

        ' We may only be updating the color, in which case name == null
        If name IsNot Nothing AndAlso (name.Length < 4 OrElse name.Length > 15) Then
            ModelState.AddError("name", "The calendar name must be within 4 and 15 characters.")
        End If

        Dim _color As String = Page.Color
        If String.IsNullOrEmpty(_color) Then
            ModelState.AddError("color", "You must specify a calendar color.")
        ElseIf Not ColorHelper.IsValidColor(_color) Then
            ModelState.AddError("color", "You must specify a brighter / valid color.")
        End If

        If ModelState.IsValid Then
            If calendarId.HasValue Then
                ' Update the calendar color
                If name Is Nothing Then
                    Calendar.UpdateCalendarColor(WebSecurity.CurrentUserId, calendarId.Value, _color)
                Else
                    ' Update the calendar name and color
                    Calendar.UpdateCalendar(WebSecurity.CurrentUserId, calendarId.Value, name, _color)
                End If
            Else
                calendarId = Calendar.CreateCalendar(WebSecurity.CurrentUserId, name, _color)
            End If
            Response.Redirect("~/Calendar/Info/" & calendarId)
        End If
    End If
End Code

@Html.ValidationSummary()
<form method="post" action="">
    <table class="info">
        <tr>
            <td><label for="name">Calendar Name</label></td>
            <td>
                @If calendarId.HasValue Then
                    If calendarInfo.Permissions = CInt(Permission.Own) Then
                        @<input type="text" id="name" name="name" value="@calendarInfo.Name" />
                    Else
                        @calendarInfo.Name
                    End If
                Else
                    @<input type="text" id="name" name="name" value="" />
                End If
            </td>
        </tr>
        <tr>
            <td><label for="jqueryColorField">Calendar Color</label></td>
            <td>
                <input type="text" id="jqueryColorField" name="color" value="@(If(calendarId.HasValue ,calendarInfo.Color , ""))" /> 
                <span id="jqueryColorSwatch" class="swatch width100 rounded-top rounded-bottom" style="@(If(calendarId.HasValue , "background-color: @(calendarInfo.Color);" , "")) border-color: black;">&nbsp;</span>
            </td>
        </tr>
        @If calendarId.HasValue Then
            @<tr>
                <td>Created By</td>
                <td>@UserHelper.GetUser(calendarInfo.Creator).Email</td>
            </tr>
        End If
        <tr>
            <td colspan="2" id="submit"><input type="submit" name="submit" value="@(If(calendarId.HasValue, "Save", "Create"))" /></td>
        </tr>
    </table>
</form>