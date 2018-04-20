' This class does calendar based actions, add, edit, remove, fetch, etc.
Public NotInheritable Class Calendar

    Private Sub New()
    End Sub
    Public Shared Sub UpdateCalendarColor(ByVal userId As Integer,
                                          ByVal calendarId As Integer,
                                          ByVal color As String)
        Dim db = UserHelper.DatabaseInstance
        db.Execute("UPDATE CalendarsUsers SET Color = @0 WHERE CalendarId = @1 AND UserId = @2",
                   color,
                   calendarId,
                   userId)
    End Sub

    Public Shared Sub UpdateCalendar(ByVal userId As Integer,
                                     ByVal calendarId As Integer,
                                     Optional ByVal calendarName As String = Nothing,
                                     Optional ByVal color As String = Nothing)
        Dim db = UserHelper.DatabaseInstance

        color = If(color, ColorHelper.GetRandomColor())
        calendarName = If(calendarName, "Default")

        ' We are editing a calendar
        db.Execute("UPDATE Calendars SET Name = @0 WHERE Id = @1",
                   calendarName,
                   calendarId)
        UpdateCalendarColor(userId, calendarId, color)
    End Sub

    Public Shared Function CreateCalendar(ByVal userId As Integer,
                                          Optional ByVal calendarName As String = Nothing,
                                          Optional ByVal color As String = Nothing) As Integer
        Dim db = UserHelper.DatabaseInstance

        color = If(color, ColorHelper.GetRandomColor())
        calendarName = If(calendarName, "Default")

        ' Create a new calendar
        db.Execute("INSERT INTO Calendars (Name, Creator, Guid) Values (@0, @1, @2)",
                   calendarName,
                   userId,
                   GenerateUniqueId())

        Dim calendarId = Convert.ToInt32(db.GetLastInsertId())

        ' Add the calendar id to CalendarUsers
        db.Execute("INSERT INTO CalendarsUsers (CalendarId, UserId, Permissions, Color) VALUES (@0, @1, @2, @3)",
                   calendarId,
                   userId,
                   Permission.Own,
                   color)

        Return calendarId
    End Function

    Private Shared Function GenerateUniqueId() As String
        Return Guid.NewGuid().ToString()

    End Function

    ''' <summary>
    ''' Deletes a calendar and all events on that calendar
    ''' </summary>
    ''' <param name="calendarId">The id of the calendar to delete</param>
    Public Shared Sub DeleteCalendar(ByVal calendarId As Integer)
        Dim db = UserHelper.DatabaseInstance

        db.Execute("DELETE FROM Events WHERE CalendarId = @0", calendarId)
        db.Execute("DELETE FROM CalendarsUsers WHERE CalendarId = @0", calendarId)
        db.Execute("DELETE FROM Calendars WHERE Id = @0", calendarId)
    End Sub

    ''' <summary>
    ''' Get calendars that userId has permissions on.
    ''' </summary>
    ''' <param name="userId">The id of the user to look for</param>
    ''' <param name="minimumPermission">The minimum value of permission the user must have on the calendar</param>
    ''' <returns>Set of rows ordered first by permissions(desc) then by name (asc)</returns>
    Public Shared Function GetUserCalendars(ByVal userId As Integer,
                                            Optional ByVal minimumPermission As Permission = Permission.View) As Object
        Dim db = UserHelper.DatabaseInstance

        Return db.Query("SELECT c.Name, c.Creator, c.Guid, cu.* " &
                        "FROM Calendars AS c " &
                        "JOIN CalendarsUsers AS cu ON c.Id = cu.CalendarId " &
                        "WHERE cu.UserId = @0 " &
                        "AND cu.Permissions >= @1 " &
                        "ORDER BY cu.Permissions DESC, c.name ASC ",
                        userId,
                        minimumPermission)
    End Function

    ''' <summary>
    ''' Get calendar information specified by a user calendar pair
    ''' </summary>
    ''' <param name="userId">The id of the user</param>
    ''' <param name="calendarId">The id of the calendar</param>
    ''' <returns>null if no calendar exists, otherwise returns calendar information</returns>
    Public Shared Function GetUserCalendar(ByVal userId As Integer, ByVal calendarId As Integer) As Object
        Dim db = UserHelper.DatabaseInstance

        Return db.QuerySingle("SELECT c.Name, c.Creator, c.Guid, cu.* " &
                              "FROM Calendars AS c " &
                              "JOIN CalendarsUsers AS cu ON c.Id = cu.CalendarId " &
                              "WHERE cu.UserId = @0 " &
                              "AND cu.CalendarId = @1 ",
                              userId,
                              calendarId)
    End Function

    Public Shared Function GetCalendarGroups(ByVal userId As Integer) As Object
        ' I should maintain a cache of all of the calendars loaded on the page
        ' to reduce calendar queries / joins
        Dim calendars = Calendar.GetUserCalendars(userId)

        Dim calendarGroups = (
            From c In CType(calendars, IEnumerable(Of Object))
            Group c By GroupKey = c.Permissions >= CInt(Permission.Own) Into g = Group
            Order By GroupKey
            Select New With {
                Key .Own = If(GroupKey, "Mine", "Shared"),
                Key .Calendars = g.OrderBy(Function(c) c.Name)}).ToDictionary(Function(g) g.Own, Function(g) g.Calendars)

        Return calendarGroups
    End Function

    Public Shared Function GetCalendar(ByVal calendarId As Integer) As Object
        Dim db = UserHelper.DatabaseInstance
        Return db.QuerySingle("SELECT * FROM Calendars WHERE Id = @0",
                              calendarId)
    End Function

    Public Shared Function GetCalendarByHash(ByVal calendarId As Integer,
                                             ByVal hash As String) As Object
        Dim db = UserHelper.DatabaseInstance
        Return db.QuerySingle("SELECT * FROM Calendars WHERE Id = @0 AND Guid = @1",
                              calendarId,
                              hash)
    End Function
End Class