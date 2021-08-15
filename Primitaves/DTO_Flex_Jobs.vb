Public Class DTO_Flex_Jobs
    Public ID As String
    Public Category As String
    Public refURL As String
    Public Title As String
    Public Desription As String
    Public URL As String
    Public Job_Type_Remote As String
    Public Job_Type As String
    Public Location As String
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        ID = String.Empty
        Category = String.Empty
        refURL = String.Empty
        Title = String.Empty
        Desription = String.Empty
        URL = String.Empty
        Job_Type_Remote = String.Empty
        Job_Type = String.Empty
        Location = String.Empty
    End Sub
    Public Overrides Function ToString() As String
        Return ID & vbTab & Category & vbTab & Title & vbTab & Desription & vbTab & URL & vbTab & Job_Type_Remote & vbTab & Job_Type & vbTab & Location & vbNewLine
    End Function
End Class
