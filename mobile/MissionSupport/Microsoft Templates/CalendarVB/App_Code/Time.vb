'
'The goal of this class is to provide an easy interface for a web
'developer to make a web application that uses timezones that are
'user specific that might be different from the server. The server
'might not even be sitting at UTC.
'
Public NotInheritable Class Time

    Private Sub New()
    End Sub

    ' The user's timezone
    Public Shared Property LocalTimeZoneId As String
        Get
            Dim timeZone = RequestData.GetValue(Of String)("LocalTimeZone")
            If timeZone Is Nothing Then
                Throw New InvalidOperationException("The Request Local Time Zone has not been set")
            End If

            Return timeZone
        End Get
        Set(ByVal value As String)
            ' If an invalid id is passed, it will throw an InvalidOperationException
            RequestData.SetValue("LocalTimeZone", value)
        End Set
    End Property

    Public Shared ReadOnly Property NowInLocal As Date
        Get
            Return UtcToLocal(Date.UtcNow)
        End Get
    End Property

    Public Shared Function LocalToUtc(ByVal localTime As Date) As Date
        Return TimeZoneInfo.ConvertTimeToUtc(localTime, TimeZoneInfo.FindSystemTimeZoneById(LocalTimeZoneId))
    End Function

    Public Shared Function UtcToLocal(ByVal utcTime As Date) As Date
        Return TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.FindSystemTimeZoneById(LocalTimeZoneId))
    End Function
End Class