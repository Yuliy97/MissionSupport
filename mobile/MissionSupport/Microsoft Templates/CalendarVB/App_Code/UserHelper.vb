Imports WebMatrix.Data
Imports WebMatrix.WebData

Public NotInheritable Class UserHelper

    Private Shared _ConnectionStringName As String
    Public Shared Property ConnectionStringName As String
        Get
            Return _ConnectionStringName
        End Get
        Private Set(ByVal value As String)
            _ConnectionStringName = value
        End Set
    End Property

    Private Shared _UserTableName As String
    Public Shared Property UserTableName As String
        Get
            Return _UserTableName
        End Get
        Private Set(ByVal value As String)
            _UserTableName = value
        End Set
    End Property

    Private Shared _UserNameField As String
    Public Shared Property UserNameField As String
        Get
            Return _UserNameField
        End Get
        Private Set(ByVal value As String)
            _UserNameField = value
        End Set
    End Property

    Private Shared _UserIdField As String
    Public Shared Property UserIdField As String
        Get
            Return _UserIdField
        End Get
        Private Set(ByVal value As String)
            _UserIdField = value
        End Set
    End Property

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property CurrentUser As Object
        Get
            ' HTTPContext could be null
            Dim current = RequestData.GetValue("CurrentUser")
            If current Is Nothing Then
                current = GetUser(WebSecurity.CurrentUserId)
                RequestData.SetValue("CurrentUser", current)
            End If
            Return current
        End Get
    End Property

    ' Can we name this Database since there is also the Database.Open thing?
    Public Shared ReadOnly Property DatabaseInstance As Object
        Get

            ' HTTPContext could be null
            Dim db = RequestData.GetValue(Of Database)("Database")
            If db Is Nothing Then
                db = Database.Open(ConnectionStringName)
                RequestData.SetValue("Database", db)
            End If

            Return db
        End Get
    End Property

    Public Shared Sub Initialize(ByVal connectionStringName As String,
                                 ByVal userTableName As String,
                                 ByVal userIdColumn As String,
                                 ByVal userNameColumn As String,
                                 ByVal autoCreateTables As Boolean)

        WebSecurity.InitializeDatabaseConnection(connectionStringName,
                                                 userTableName,
                                                 userIdColumn,
                                                 userNameColumn,
                                                 autoCreateTables)

        UserHelper.ConnectionStringName = connectionStringName
        UserHelper.UserTableName = userTableName
        UserNameField = userNameColumn
        UserIdField = userIdColumn
    End Sub

    Public Shared Function GetUser(ByVal userId As Integer) As Object
        Dim query = String.Format("SELECT * FROM [{0}] WHERE [{1}] = @0",
                                            UserTableName,
                                            UserIdField)
        Return DatabaseInstance.QuerySingle(query, userId)
    End Function

    Public Shared Function UserExists(ByVal userNameValue As String) As Boolean
        Return WebSecurity.GetUserId(userNameValue) <> -1
    End Function

    Public Shared Function AddUser(ByVal name As String,
                                   ByVal email As String,
                                   ByVal timeZone As String) As Integer
        Dim query = String.Format("INSERT INTO [{0}] (Name, Email, TimeZone) VALUES (@0, @1, @2)",
                                            UserTableName)

        DatabaseInstance.QuerySingle(query, name, email, timeZone)

        Return Convert.ToInt32(DatabaseInstance.GetLastInsertId())
    End Function
End Class