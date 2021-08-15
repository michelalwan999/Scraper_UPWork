Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Remote
Imports OpenQA.Selenium.Support.UI
Public Class Scraper_Up_Work_L2_0
    Private mDir As String
    Private mFile_In As String
    Private mFile_In_Suffle As String
    Private mfileFail As String
    Private mFile_Out As String
    Private mDriver As IWebDriver
    Private mDirChrome As String
    Private mChrome_App As String
    Private mVPN_App As String
    Public DTO_L1 As List(Of DTO_Up_Work_L1)
    Public DTO_L2 As List(Of DTO_Up_Work)
    Public mListIps As List(Of Integer) = New List(Of Integer) From {3, 4, 12, 9, 0, 7, 11}
    Public monitor As Integer = 0
    Public vpnIndex = 1
    Public mId As Integer = 1000
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        mDir = "C:\Users\TOSHIBA\Desktop\Software And Files\Expert 23\Scrapers\UPWORK\"
        mFile_In = mDir & "UPWORKL1.txt"
        mFile_Out = mDir & "0.txt"
        mfileFail = mDir & "0fail.txt"
        mFile_In_Suffle = mDir & "L1Shuffle.txt"
        mDirChrome = "C:\Users\TOSHIBA\Desktop\Software And Files\Expert 23\Social Blade\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mChrome_App = mDirChrome & "chrome.exe"
        mVPN_App = mDirChrome & "2.0.0_0.crx"
        DTO_L1 = New List(Of DTO_Up_Work_L1)
        DTO_L2 = New List(Of DTO_Up_Work)
    End Sub
    Private Sub Scrape_Pages()
        For i = 0 To 194
            Debug.Print(i)
            Try
                Relod_Driver()
                Utilities.Rotate_VPN(mListIps(CInt(Math.Ceiling(Rnd() * 6))) + 1, True, mDriver)
                Go_To_URl(DTO_L1(i).URL)
                Dim dt As New DTO_Up_Work
                Utilities.Click_Elelment_No_Wait(mDriver.FindElement(By.CssSelector(".up-btn.up-btn-secondary.up-btn-block-sm.mb-0")), mDriver)
                Scroll()
                Scrape_Page(dt, DTO_L1(i))
                monitor = monitor + 1
            Catch ex As Exception
                My.Computer.FileSystem.WriteAllText(mfileFail, DTO_L1(i).ToString, True)
            End Try

        Next
    End Sub
    Public Sub Scrape()
        Read_Ref_File()
        Initialise_Driver()
        Scrape_Pages()
    End Sub
    Private Sub Relod_Driver()
        mDriver.Quit()

        Initialise_Driver()
    End Sub
    Private Sub Scrape_Page(ByRef dt As DTO_Up_Work, ByVal dt2 As DTO_Up_Work_L1)
        Dim jobs = mDriver.FindElements(By.CssSelector(".job-tile-wrapper"))
        For Each job In jobs
            dt.ID = mId.ToString
            dt.Category = dt2.Category
            dt.Sub_Category = dt2.SubCategory
            mId = mId + 1
            Try
                dt.Job_Title = job.FindElement(By.CssSelector(".d-block.display-u2u.job-title.h5.mb-10")).Text.Trim
            Catch ex As Exception
                Continue For
                Debug.Print("Fail To Scrape Title")
            End Try

            Try
                dt.URL = job.FindElement(By.CssSelector(".d-block.display-u2u.job-title.h5.mb-10")).GetAttribute("href").Trim
            Catch ex As Exception
                Debug.Print("Fail To Scrape URL")
            End Try

            Try
                dt.Description = job.FindElement(By.CssSelector(".mt-0.pt-0.mb-20.job-description")).Text.Trim
            Catch ex As Exception
                Debug.Print("Fail To Scrape Description")
            End Try

            Try
                Scrape_duration(job, dt)
            Catch ex As Exception
                Debug.Print("Fail To Scrape Duration")
            End Try
            Try
                Scrape_Tags(job, dt)
            Catch ex As Exception
                Debug.Print("Fail To Scrape Tag")
            End Try
            My.Computer.FileSystem.WriteAllText(mFile_Out, dt.ToString, True)
        Next
    End Sub

    Private Sub Scrape_Tags(ByVal job As IWebElement, ByRef dt As DTO_Up_Work)

        Dim tags = job.FindElement(By.CssSelector(".skills-list.mb-0"))
        Try
            Dim span = tags.FindElements(By.TagName("span"))
            For Each s In span
                dt.Tags = dt.Tags & s.Text & "|"
            Next
        Catch ex As Exception

        End Try

        Try
            Dim a = tags.FindElements(By.TagName("a"))
            For Each Aa In a
                dt.Tags = dt.Tags & Aa.Text & "|"
            Next
        Catch ex As Exception

        End Try


    End Sub
    Private Sub Scrape_duration(ByVal job As IWebElement, ByRef dt As DTO_Up_Work)
        Dim spec = job.FindElement(By.CssSelector(".row"))
        Dim paragraphs = spec.FindElements(By.TagName("p"))
        For Each p In paragraphs
            Dim key
            Try
                key = p.FindElement(By.TagName("span")).Text.Trim
            Catch ex As Exception
                key = p.FindElement(By.TagName("small")).Text.Trim
            End Try

            Dim value = p.FindElement(By.TagName("strong")).Text.Trim
            dt.Duration = dt.Duration & key & ": " & value & "|"
        Next
    End Sub

    Private Sub Scroll()
        For i = 0 To 20
            mDriver.FindElement(By.TagName("body")).SendKeys(Keys.PageDown)
        Next
    End Sub
    Private Sub Go_To_URl(ByVal url As String)
        Try
            mDriver.Navigate.GoToUrl(url)
        Catch ex As Exception
            Thread.Sleep(4000)
        End Try


    End Sub

    Private Sub Read_Ref_File()
        Dim file As String = My.Computer.FileSystem.ReadAllText(mFile_In_Suffle)
        Dim lines As String() = file.Split(vbNewLine)
        For Each line In lines
            Clean_Line(line)
            Dim parts As String() = line.Split(vbTab)
            Dim dtL1 = New DTO_Up_Work_L1
            With dtL1
                .ID = parts(0)
                .Category = parts(1)
                .SubCategory = parts(2)
                .URL = parts(3)
            End With
            DTO_L1.Add(dtL1)
        Next
    End Sub

    Private Sub Clean_Line(ByRef line As String)
        line = line.Replace(vbLf, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
        line = line.Replace(vbNewLine, "")
    End Sub
    Private Sub Initialise_Driver()
        Dim options As New ChromeOptions
        options.BinaryLocation = mChrome_App
        options.AddExtension(mVPN_App)
        mDriver = New ChromeDriver(options)
        mDriver.Manage.Window.Maximize()
        mDriver.Manage.Timeouts.PageLoad = New TimeSpan(0, 0, 60)
    End Sub
End Class
