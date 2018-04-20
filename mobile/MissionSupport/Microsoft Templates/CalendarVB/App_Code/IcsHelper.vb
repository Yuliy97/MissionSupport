Imports System.IO

Public NotInheritable Class IcsHelper

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property CalendarHeaders As String
        Get
            Dim file = New StringWriter
            file.WriteLine("BEGIN:VCALENDAR")
            file.WriteLine("PRODID:-//ASP//SharedCalendar//EN")
            file.WriteLine("VERSION:2.0")
            Return file.ToString()
        End Get
    End Property

    Public Shared ReadOnly Property CalendarFooters As String
        Get
            Dim file = New StringWriter
            file.WriteLine("END:VCALENDAR")
            Return file.ToString()
        End Get
    End Property

    Public Shared Function Encode(ByVal input As String) As String
        input = input.Replace("\", "\\")
        input = input.Replace(",", "\,")
        input = input.Replace(";", "\;")

        Return input
    End Function

    Public Shared Function BuildEvent(ByVal eventInfo As Object) As String

        Dim file = New StringWriter

        file.WriteLine("BEGIN:VEVENT")

        If eventInfo.AllDay Then
            file.WriteLine("DTSTART;VALUE=DATE:{0}", CDate(eventInfo.Start).ToString("yyyyMMdd"))
        Else
            file.WriteLine("DTSTART: {0}", CDate(eventInfo.Start).ToString("yyyyMMdd\THHmmss\Z"))
            file.WriteLine("DTEND: {0}", CDate(eventInfo.End).ToString("yyyyMMdd\THHmmss\Z"))
        End If
        file.WriteLine("LOCATION: {0}", IcsHelper.Encode(eventInfo.Location))
        file.WriteLine("DESCRIPTION: {0}", IcsHelper.Encode(eventInfo.Description))
        file.WriteLine("SUMMARY;LANGUAGE=en-us: {0}", IcsHelper.Encode(eventInfo.Name))

        Dim user = UserHelper.GetUser(eventInfo.OrganizerId)

        file.WriteLine("ORGANIZER;CN=""{0}"":mailto:{1}", IcsHelper.Encode(user.Name), user.Email)
        file.WriteLine("UID:{0}", eventInfo.Id)
        file.WriteLine("PRIORITY:3")
        file.WriteLine("END:VEVENT")

        Return file.ToString()
    End Function

    Public Shared Function BuildCalendar(ByVal calendarId As Integer) As String

        Dim file = New StringWriter
        Dim _calendar = Calendar.GetCalendar(calendarId)

        file.Write(CalendarHeaders)
        file.WriteLine("METHOD:PUBLISH")
        file.WriteLine("X-WR-RELCALID:{0}", calendarId)
        file.WriteLine("X-WR-CALNAME:{0}", _calendar.Name)
        Dim events = [Event].GetCalendarEvents(calendarId)
        For Each e In events
            file.Write(BuildEvent(e))
        Next e

        file.Write(CalendarFooters)
        Return file.ToString()
    End Function
End Class