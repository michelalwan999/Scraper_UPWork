Public Class DTO_L1

    Public ID As String
    Public First_Name As String
    Public Last_Name As String
    Public Title As String
    Public Company As String
    Public Education As String

    Public Country As String
    Public URL As String
    Public Sub New()
        Initialise()
    End Sub
    Private Sub Initialise()
        ID = 0
        First_Name = String.Empty
        Last_Name = String.Empty
        Title = String.Empty
        Company = String.Empty
        Education = String.Empty

        Country = String.Empty
        URL = String.Empty
    End Sub
    Public Overrides Function ToString() As String
        Return ID & vbTab & First_Name & vbTab & Last_Name & vbTab & Title & vbTab & Company & vbTab & Education & vbTab & Country & vbTab & URL & vbNewLine
    End Function

End Class
