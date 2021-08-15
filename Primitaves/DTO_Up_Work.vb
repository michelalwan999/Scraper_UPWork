Public Class DTO_Up_Work
    Public ID As String
    Public Category As String
    Public Sub_Category As String
    Public Job_Title As String
    Public Description As String
    Public Time As String
    Public Duration As String
    Public Experience_Level As String
    Public Tags As String
    Public URL As String
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        ID = String.Empty
        Category = String.Empty
        Sub_Category = String.Empty
        Job_Title = String.Empty
        Description = String.Empty
        Time = String.Empty
        Duration = String.Empty
        Experience_Level = String.Empty
        Tags = String.Empty
        URL = String.Empty
    End Sub
    Public Overrides Function ToString() As String
        Return ID & vbTab & Category & vbTab & Sub_Category & vbTab & Job_Title & vbTab & Description & vbTab & Duration & vbTab & Tags & vbTab & URL & vbNewLine
    End Function
End Class
