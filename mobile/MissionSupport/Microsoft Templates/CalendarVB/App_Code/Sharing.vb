Public NotInheritable Class Sharing
    ' Get all people with access
    ' Add access
    ' Edit access

    Private Sub New()
    End Sub

    Public Shared Function GetUsersWithAccess(ByVal calendarId As Integer,
                                              Optional ByVal minPermission As Permission = Permission.View) As Object
        Dim db = UserHelper.DatabaseInstance

        Return db.Query("SELECT u.*, cu.Permissions " &
                        "FROM Users AS u " &
                        "JOIN CalendarsUsers AS cu ON u.Id = cu.UserId " &
                        "JOIN Calendars AS c ON cu.CalendarId = c.Id " &
                        "WHERE c.Id = @0 AND cu.Permissions >= @1 " &
                        "ORDER BY cu.Permissions DESC, " &
                        "u.Email ASC ",
      calendarId,
      minPermission)
    End Function

    ' Give user access to calendar 
    Public Shared Sub ShareCalendar(ByVal calendarId As Integer,
                                    ByVal userId As Integer,
                                    ByVal permission As Permission,
         Optional ByVal color As String = Nothing)
        color = If(color, ColorHelper.GetRandomColor())

        Dim db = UserHelper.DatabaseInstance
        db.Execute("INSERT INTO CalendarsUsers (CalendarId, UserId, Permissions, Color) VALUES (@0, @1, @2, @3)",
     calendarId,
     userId,
     permission,
     color)
    End Sub

    Private Shared Sub RemoveAccess(ByVal calendarId As Integer, ByVal userId As Integer)
        Dim db = UserHelper.DatabaseInstance

        db.Execute("DELETE FROM CalendarsUsers WHERE CalendarId = @0 AND UserId = @1",
  calendarId,
  userId)
    End Sub

    Public Shared Sub EditShareCalendar(ByVal calendarId As Integer,
                                        ByVal userId As Integer,
                                        ByVal permission As Permission)
        Dim db = UserHelper.DatabaseInstance

        If permission = permission.NoAccess Then
            RemoveAccess(calendarId, userId)
        Else
            db.Execute("UPDATE CalendarsUsers SET Permissions = @0 WHERE CalendarId = @1 AND UserId = @2",
                       permission,
                       calendarId,
                       userId)
        End If
    End Sub
End Class