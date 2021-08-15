Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Public Class Scraper_L1

    Private mDir As String
    Private mFile_In As String
    Private mFile_Out As String
    Private mScraperLst As List(Of String)
    Private mDto_ID As Integer
    Private mdirRef As String
    Private mdirChrome As String
    Private mdirVPN As String
    Private mfileUnnique As String
    Private mDriver As IWebDriver
    Private mURL_Base As String
    Private mfile_Fail As String
    Private mVPN_Rotation_Index As Integer
    Private mIP As List(Of Integer)
    Private countAut As Integer
    Public Sub New()
        Initialise()
        Initialise_Driver()
    End Sub
    Private Sub Initialise()
        mDir = "C:\Users\TOSHIBA\Desktop\Software and Files\Expert 23\Task1\Lebanese Working Abbroad\dir\output\March4\"
        mdirRef = "C:\Users\TOSHIBA\Desktop\Software and Files\Expert 23\Task1\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mdirChrome = mdirRef & "Chrome.exe"
        mdirVPN = mdirRef & "2.0.0_0.crx"
        '     mURL_Base = "https://lb.linkedin.com/jobs/search?keywords=People%20Search&location=%D8%B9%D8%A7%D9%84%D9%85%D9%8A%D9%8B%D8%A7&geoId=92000000&trk=organization_guest_jobs-search-bar_search-submit&redirect=false&position=1&pageNum=0"
        mURL_Base = "https://www.linkedin.com/jobs/search?keywords=People%20Search&location=worldwide&geoId=92000000&trk=organization_guest_jobs-search-bar_search-submit&redirect=false&position=1&pageNum=0"
        mVPN_Rotation_Index = 0
        mFile_In = mDir & "ALL.txt"
        mfile_Fail = mDir & "S2Fail.txt"
        mFile_Out = mDir & "S123Clean.txt"
        mfileUnnique = mDir & "S2Unique.txt"
        mScraperLst = New List(Of String)
        '   mIP = New List(Of Integer) From {7, 9, 11, 12, 14, 16, 17, 4, 0, 3}
        mIP = New List(Of Integer) From {3, 4, 12, 9, 0, 7, 11}
        mDto_ID = 1050
        countAut = 0

    End Sub
    Public Sub Scrape_L1()



        Read_Ref_File()
        Utilities.Rotate_VPN(0, True, mDriver)
        Utilities.Rotate_VPN(11, False, mDriver)

        Dim countIps As Integer = 0
        '6888 till 9260 are done
        For i = 7000 To 11230

            Debug.Print(i)
            Dim names As String() = mScraperLst(i).Split(vbTab)
            ' mURL_Base = "https://www.linkedin.com/pub/dir?firstName=" & names(0) & "&lastName=" & names(1) & "&trk=homepage-jobseeker_people-search-bar_search-submit"

            countIps = countIps + 1

            If countIps = 7 Then
                countIps = 0

                Try
                    Utilities.Rotate_VPN(0, False, mDriver)
                    Utilities.Rotate_VPN(mIP(mVPN_Rotation_Index), False, mDriver)
                Catch ex As Exception
                    Thread.Sleep(10000)
                    Utilities.Rotate_VPN(0, False, mDriver)
                    Thread.Sleep(5000)
                    Utilities.Rotate_VPN(mIP(mVPN_Rotation_Index), False, mDriver)
                End Try
                mVPN_Rotation_Index = mVPN_Rotation_Index + 1
                If mVPN_Rotation_Index = 7 Then

                    mVPN_Rotation_Index = 0
                End If

                Thread.Sleep(2000)
                Try
                    Go_To_URl(mURL_Base)

                    Scrape_Page(mScraperLst(i))
                Catch ex As Exception
                    My.Computer.FileSystem.WriteAllText(mfile_Fail, mScraperLst(i) & vbNewLine, True)

                End Try



            Else
                Try
                    Go_To_URl(mURL_Base)
                    Scrape_Page(mScraperLst(i))

                Catch ex As Exception
                    My.Computer.FileSystem.WriteAllText(mfile_Fail, mScraperLst(i) & vbNewLine, True)

                End Try



            End If
        Next

    End Sub

    Private Sub Google_By_Pass()

        Dim cards = mDriver.FindElements(By.CssSelector(".g"))

        For Each card In cards

            Dim h3 = card.FindElement(By.TagName("h3")).Text
            If h3.Contains("+") Then
                card.FindElement(By.TagName("a")).Click()

            End If

        Next


    End Sub

    Private Sub Scrape_Page(ByVal nameLname As String)

        Dim dto As New DTO_L1
        Dim parts As String() = nameLname.Split(vbTab)
        Dim nomatch
        Try
            Search(parts)

        Catch ex As Exception
            My.Computer.FileSystem.WriteAllText(mfile_Fail, nameLname & vbNewLine, True)
            Exit Sub
        End Try

        Try
            nomatch = mDriver.FindElement(By.TagName("h1"))

        Catch ex As Exception
            My.Computer.FileSystem.WriteAllText(mfile_Fail, nameLname & vbNewLine, True)
            Exit Sub
        End Try


        Try

            Dim auth = mDriver.FindElement(By.CssSelector(".flip-card "))
            My.Computer.FileSystem.WriteAllText(mfile_Fail, nameLname & vbNewLine, True)
            countAut = countAut + 1


            If countAut = 30 Then
                countAut = 0
                mDriver.Quit()

                Debug.Print("WAITING 45 sec")
                Thread.Sleep(45000)
                Initialise_Driver()



                Try

                    Utilities.Rotate_VPN(0, True, mDriver)

                    Thread.Sleep(5000)
                Catch ex As Exception

                    Thread.Sleep(5000)

                    Utilities.Rotate_VPN(0, True, mDriver)


                End Try

                Try

                    Utilities.Rotate_VPN(mIP(mVPN_Rotation_Index), False, mDriver)
                    mVPN_Rotation_Index = mVPN_Rotation_Index + 1
                    If mVPN_Rotation_Index = 7 Then
                        mVPN_Rotation_Index = 0
                    End If
                    Thread.Sleep(5000)
                Catch ex As Exception

                    Thread.Sleep(5000)

                    Utilities.Rotate_VPN(mIP(mVPN_Rotation_Index), False, mDriver)
                    mVPN_Rotation_Index = mVPN_Rotation_Index + 1
                    If mVPN_Rotation_Index = 7 Then
                        mVPN_Rotation_Index = 0
                    End If

                End Try

            End If

            Exit Sub
        Catch ex As Exception

        End Try

        If nomatch.Text.Contains("We couldn't find a match") Then
            Exit Sub
        Else
            Try
                Dim mainContent = mDriver.FindElement(By.Id("main-content"))

            Catch ex As Exception

                mDto_ID = mDto_ID + 1
                My.Computer.FileSystem.WriteAllText(mfileUnnique, mDto_ID.ToString() & vbTab & parts(0) & vbTab & parts(1) & vbTab & mDriver.Url & vbNewLine, True)
                My.Computer.FileSystem.WriteAllText(mfile_Fail, parts(0) & vbTab & parts(1) & vbNewLine, True)
                Exit Sub

            End Try


            Try
                Dim mainContent = mDriver.FindElement(By.Id("main-content"))
                Dim liTag = mainContent.FindElements(By.TagName("li"))

                For i = 0 To liTag.Count - 1

                    Scrape_Card(liTag(i), dto)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    mDriver.FindElement(By.TagName("body")).SendKeys(Keys.ArrowDown)
                    dto.ID = mDto_ID.ToString()
                    mDto_ID = mDto_ID + 1


                    My.Computer.FileSystem.WriteAllText(mFile_Out, dto.ToString(), True)
                    Debug.Print("Lines Written:" & mDto_ID.ToString)

                Next
            Catch ex As Exception

            End Try


        End If



    End Sub


    Private Sub Scrape_Card(ByVal li As IWebElement, ByRef dto As DTO_L1)
        Scrape_Url(li, dto)

        Scrape_Name_LName(li, dto)


        Try
            Scrape_Title(li, dto)
        Catch ex As Exception
            dto.Title = "unspecified"
        End Try

        Try
            Scrape_Location(li, dto)
        Catch ex As Exception
            dto.Country = "unspecified"
        End Try


        Scrape_Company(li, dto)



    End Sub

    Private Sub Search(pars As String())


        mDriver.FindElement(By.CssSelector(".switcher-tabs__trigger-and-tabs")).FindElement(By.TagName("button")).Click()
        Thread.Sleep(1000)
        Dim people = mDriver.FindElements(By.CssSelector(".switcher-tabs__button"))
        people(1).Click()
        Dim inputs = mDriver.FindElements(By.CssSelector(".dismissable-input__input"))
        inputs(0).SendKeys(pars(0))
        inputs(1).SendKeys(pars(1))
        mDriver.FindElement(By.CssSelector(".base-search-bar__submit-btn")).Click()

    End Sub



    Private Sub Scrape_Company(ByVal li As IWebElement, ByRef dto As DTO_L1)





        Try
            Dim wrapperSchoolWork = li.FindElements(By.CssSelector(".entity-list-meta"))
            Dim temp = wrapperSchoolWork(0).FindElement(By.TagName("icon")).FindElement(By.Id("briefcase-icon"))

            dto.Company = wrapperSchoolWork(0).FindElement(By.TagName("span")).Text
            Scrape_School(li, dto)
            Exit Sub
        Catch ex As Exception
            dto.Company = "unspecified"
            Try
                Dim wrapperSchoolWork = li.FindElements(By.CssSelector(".entity-list-meta"))
                Dim temp = wrapperSchoolWork(0).FindElement(By.TagName("icon")).FindElement(By.Id("school-icon"))
                dto.Education = wrapperSchoolWork(0).FindElement(By.TagName("span")).Text
                Exit Sub
            Catch ex2 As Exception
                Scrape_School(li, dto)
                Exit Sub
            End Try




        End Try

    End Sub

    Private Sub Scrape_School(ByVal li As IWebElement, ByRef dto As DTO_L1)

        Try

            Dim wrapperSchoolWork = li.FindElements(By.CssSelector(".entity-list-meta"))
            Dim temp = wrapperSchoolWork(1).FindElement(By.TagName("icon")).FindElement(By.Id("school-icon"))
            dto.Education = wrapperSchoolWork(1).FindElement(By.TagName("span")).Text
        Catch ex As Exception
            dto.Education = "unspecified"
            End Try





    End Sub

    Private Sub Scrape_Location(ByVal li As IWebElement, ByRef dto As DTO_L1)
        dto.Country = li.FindElement(By.CssSelector(".people-search-card__location")).Text
        dto.Country = dto.Country.Trim()
    End Sub
    Private Sub Scrape_Name_LName(ByVal li As IWebElement, ByRef dto As DTO_L1)
        Dim fLname = li.FindElement(By.CssSelector(".base-search-card__title")).Text
        fLname = fLname.Trim()
        Dim names As String() = fLname.Split(" ")
        If names.Count > 2 Then
            dto.First_Name = names(0)
            Dim lastname = fLname.Replace(names(0), "")
            lastname = lastname.Trim()
            dto.Last_Name = lastname

        Else

            dto.First_Name = names(0)
            dto.Last_Name = names(1)
        End If

    End Sub

    Private Sub Scrape_Title(ByVal li As IWebElement, ByRef dto As DTO_L1)
        dto.Title = li.FindElement(By.CssSelector(".base-search-card__subtitle")).Text
        dto.Title = dto.Title.Trim()
    End Sub

    Private Sub Scrape_Url(ByVal li As IWebElement, ByRef dto As DTO_L1)
        dto.URL = li.FindElement(By.TagName("a")).GetAttribute("href")
    End Sub

    Private Sub Read_Ref_File()
        Dim file As String = My.Computer.FileSystem.ReadAllText(mFile_In)
        Dim lines As String() = file.Split(vbNewLine)
        For i = 0 To lines.Count - 1
            Clean_Line(lines(i))
            mScraperLst.Add(lines(i))
        Next
    End Sub

    Private Sub Go_To_URl(ByVal url As String)
        mDriver.Navigate.GoToUrl(url)
    End Sub
    Private Sub Initialise_Driver()
        Dim options As New ChromeOptions
        options.BinaryLocation = mdirChrome
        options.AddExtension(mdirVPN)
        mDriver = New ChromeDriver(options)
        mDriver.Manage.Timeouts.PageLoad = New TimeSpan(0, 0, 60)
        mDriver.Manage.Timeouts.ImplicitWait = New TimeSpan(0, 0, 1)
        mDriver.Manage.Window.Maximize()
    End Sub
    Private Sub Clean_Line(ByRef line As String)
        line = line.Replace(vbLf, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
        line = line.Replace(vbNewLine, "")
    End Sub
End Class
