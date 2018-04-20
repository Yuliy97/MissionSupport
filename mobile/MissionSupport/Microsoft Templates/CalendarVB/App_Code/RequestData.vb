''' <summary>
''' Helper class to store information in context
''' that is very unlikely to collide with other values
''' 
''' The data stored in this helper live only throughout the
''' request.
''' </summary>
Public NotInheritable Class RequestData
    ' The key to use as a faux namespace in context
    Private Shared ReadOnly _key As New Object

    Private Sub New()
    End Sub

    ''' <summary>
    ''' Get the entire collection of information stored
    ''' in our namespace
    ''' </summary>
    ''' <return>A dictionary of settings</return>
    Private Shared Function GetSettings() As IDictionary(Of String, Object)
        Return GetSettings(New HttpContextWrapper(HttpContext.Current))
    End Function

    Friend Shared Function GetSettings(ByVal context As HttpContextBase) As IDictionary(Of String, Object)
        Dim settings = TryCast(context.Items(_key), IDictionary(Of String, Object))

        If settings Is Nothing Then
            ' We will create it
            settings = New Dictionary(Of String, Object)
            context.Items(_key) = settings
        End If

        Return settings
    End Function

    ' Get the generic object of the item stored in the context
    Public Shared Function GetValue(ByVal key As String) As Object
        Return GetValue(Of Object)(New HttpContextWrapper(HttpContext.Current), key)
    End Function

    Public Shared Function GetValue(Of TValue)(ByVal key As String) As TValue
        Return GetValue(Of TValue)(New HttpContextWrapper(HttpContext.Current), key)
    End Function

    Friend Shared Function GetValue(Of TValue)(ByVal context As HttpContextBase,
                                               ByVal key As String) As TValue
        Dim settings = GetSettings(context)
        Dim value As Object = New Object()

        If settings.TryGetValue(key, value) Then
            Return CType(value, TValue)
        End If

        Return Nothing
    End Function

    Public Shared Sub SetValue(ByVal key As String, ByVal value As Object)
        SetValue(New HttpContextWrapper(HttpContext.Current), key, value)
    End Sub

    Friend Shared Sub SetValue(ByVal context As HttpContextBase,
                               ByVal key As String,
                               ByVal value As Object)
        Dim settings = GetSettings(context)
        settings(key) = value
    End Sub
End Class