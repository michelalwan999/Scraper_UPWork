Public Class preData

    Private mDir As String
    Private mfile_Raw As String
    Private mFile_Fix As String

    Public Sub New()
        Initilaise()
    End Sub
    Private Sub Initilaise()
        mDir = "C:\Users\Lenovo z50\Desktop\Software and files\Expert23\Scrapers\2021\Lebanese Working Abbroad\dir\pre\"
        mfile_Raw = mDir & "names.txt"
        mFile_Fix = mDir & "1fix.txt"
    End Sub



    Public Sub Fix_1()

        Dim file As String = My.Computer.FileSystem.ReadAllText(mfile_Raw)
        Dim lines As String() = file.Split(vbNewLine)
        For i = 0 To lines.Count - 1

            Clean_Line(lines(i))
            Dim parts As String() = lines(i).Split(vbTab)
            Dim fName As String = parts(0)
            Dim lName As String = parts(1)
            Dim tempFname As String() = {}
            Dim tempLname As String() = {}
            If fName.Contains("(") Then


                tempFname = fName.Split("(")

                For l = 0 To tempFname.Count - 1
                    tempFname(l) = tempFname(l).Replace("(", "")
                    tempFname(l) = tempFname(l).Replace(")", "")


                Next

            End If

            If fName.Contains("/") Then



                tempFname = fName.Split("/")
                For l = 0 To tempFname.Count - 1

                    tempFname(l) = tempFname(l).Replace("/", "")

                Next

            End If

            If lName.Contains("(") Then


                tempLname = lName.Split("(")

                For l = 0 To tempLname.Count - 1
                    tempLname(l) = tempLname(l).Replace("(", "")
                    tempLname(l) = tempLname(l).Replace(")", "")

                Next

            End If

            If lName.Contains("/") Then

                tempLname = lName.Split("/")
                For l = 0 To tempLname.Count - 1

                    tempLname(l) = tempLname(l).Replace("/", "")
                Next

            End If
            If tempFname.Count > 1 Then

                For m = 0 To tempFname.Count - 1

                    If tempLname.Count = 0 Then

                        My.Computer.FileSystem.WriteAllText(mFile_Fix, tempFname(m) & vbTab & lName & vbNewLine, True)
                    Else
                        My.Computer.FileSystem.WriteAllText(mFile_Fix, tempFname(m) & vbTab & tempLname(0) & vbNewLine, True)
                    End If





                Next

            ElseIf tempLname.Count > 0 Then

                For m = 0 To tempLname.Count - 1


                    If tempFname.Count = 0 Then

                        My.Computer.FileSystem.WriteAllText(mFile_Fix, fName & vbTab & tempLname(m) & vbNewLine, True)
                    Else
                        My.Computer.FileSystem.WriteAllText(mFile_Fix, tempFname(0) & vbTab & tempLname(m) & vbNewLine, True)
                    End If


                Next

            Else
                My.Computer.FileSystem.WriteAllText(mFile_Fix, fName & vbTab & lName & vbNewLine, True)
            End If



        Next

    End Sub



    Private Sub Clean_Line(ByRef line As String)
        line = line.Replace(vbLf, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
        line = line.Replace(vbNewLine, "")
    End Sub

End Class
