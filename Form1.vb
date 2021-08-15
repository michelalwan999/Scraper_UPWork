Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim scrp As New Scraper_Up_Work_L2_0()
        scrp.Scrape()
    End Sub
End Class
