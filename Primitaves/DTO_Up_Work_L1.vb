Public Class DTO_Up_Work_L1

    Public ID As String
    Public Category As String
    Public SubCategory As String
    Public URL As String
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        ID = String.Empty
        Category = String.Empty
        SubCategory = String.Empty
        URL = String.Empty
    End Sub
    Public Overrides Function ToString() As String
        Return ID & vbTab & Category & vbTab & SubCategory & vbTab & URL & vbNewLine
    End Function

End Class
