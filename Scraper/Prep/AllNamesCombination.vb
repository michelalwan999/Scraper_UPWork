Public Class AllNamesCombination

    Private mDir As String
    Private mfile_Raw As String
    Private mFile_Fix As String
    Private mFnameLit As List(Of Tuple(Of String, String))
    Private mLnameLit As List(Of Tuple(Of String, String))

    Public Sub New()
        Initilaise()
    End Sub
    Private Sub Initilaise()
        mDir = "C:\Users\SONY\Desktop\Software And Files\Files\Expert23\SCRAPERS\Task1\Lebanese Working Abbroad\dir\pre\"
        mfile_Raw = mDir & "round1raw.txt"
        mFile_Fix = mDir & "round1.txt"
        mFnameLit = New List(Of Tuple(Of String, String))
        mLnameLit = New List(Of Tuple(Of String, String))
    End Sub

    Public Sub fixFile()


        Read_file()
        Populate_File()


    End Sub


    Private Sub Populate_File()


        For i = 0 To mFnameLit.Count - 1


            For j = 0 To mLnameLit.Count - 1



                If mFnameLit(i).Item1 = "0" And mLnameLit(j).Item1 = "0" Then My.Computer.FileSystem.WriteAllText(mFile_Fix, mFnameLit(i).Item2 & vbTab & mLnameLit(j).Item2 & vbNewLine, True)

                If mFnameLit(i).Item1 = "1" And mLnameLit(j).Item1 = "1" Then My.Computer.FileSystem.WriteAllText(mFile_Fix, mFnameLit(i).Item2 & vbTab & mLnameLit(j).Item2 & vbNewLine, True)

                If mFnameLit(i).Item1 = "10" And mLnameLit(j).Item1 = "0" Then My.Computer.FileSystem.WriteAllText(mFile_Fix, mFnameLit(i).Item2 & vbTab & mLnameLit(j).Item2 & vbNewLine, True)

                If mFnameLit(i).Item1 = "10" And mLnameLit(j).Item1 = "1" Then My.Computer.FileSystem.WriteAllText(mFile_Fix, mFnameLit(i).Item2 & vbTab & mLnameLit(j).Item2 & vbNewLine, True)

                If mFnameLit(i).Item1 = "0" And mLnameLit(j).Item1 = "10" Then My.Computer.FileSystem.WriteAllText(mFile_Fix, mFnameLit(i).Item2 & vbTab & mLnameLit(j).Item2 & vbNewLine, True)

                If mFnameLit(i).Item1 = "1" And mLnameLit(j).Item1 = "10" Then My.Computer.FileSystem.WriteAllText(mFile_Fix, mFnameLit(i).Item2 & vbTab & mLnameLit(j).Item2 & vbNewLine, True)

            Next


        Next



    End Sub
    Private Sub Read_file()


        Dim file As String = My.Computer.FileSystem.ReadAllText(mfile_Raw)
        Dim lines As String() = file.Split(vbNewLine)

        For i = 1 To lines.Count - 1

            Clean_Line(lines(i))
            Dim parts As String() = lines(i).Split(vbTab)

            Dim tuple1 As Tuple(Of String, String) =
            New Tuple(Of String, String)(parts(1), parts(0))
            mFnameLit.Add(tuple1)

            Dim tuple2 As Tuple(Of String, String) =
            New Tuple(Of String, String)(parts(3), parts(2))
            mLnameLit.Add(tuple2)

        Next

    End Sub

    Private Sub Clean_Line(ByRef line As String)
        line = line.Replace(vbLf, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
        line = line.Replace(vbNewLine, "")
    End Sub

End Class
