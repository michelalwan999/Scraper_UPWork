Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Remote
Imports OpenQA.Selenium.Support.UI
Public Class Scraper_Flex_Jobs
    Private mDir As String
    Private ref0 As String
    Private mDriver As IWebDriver
    Private mDirChrome As String
    Private mChrome_App As String
    Private mVPN_App As String
    Private mDirL1 As String
    Private mURL_List As List(Of String)
    Private mFile_Out As String
    Private mFile_Fail As String
    Private mID As Integer = 0
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        mDir = "C:\Users\pc\Desktop\Software and Files\Expert 23\Scrapers\Flex Jobs\DIR\Flex Jobs\"
        mDirChrome = "C:\Users\pc\Desktop\Software and Files\Expert 23\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mChrome_App = mDirChrome & "chrome.exe"
        mVPN_App = mDirChrome & "2.0.0_0.crx"
        ref0 = mDir & "ref0.txt"
        mDirL1 = mDir & "L1.txt"
        mURL_List = New List(Of String)
        mFile_Out = mDir & "output.txt"
        mFile_Fail = mDir & "fail.txt"
    End Sub
    Public Sub Scrape()
        'Read_Build_File()

        'Dim utl As New Utilities
        'utl.Shuffle_Links(mDir & "L0.txt", mDirL1)

        Read_Ref_File()
        Initialise_Driver()
        Scrape_Pages()

    End Sub

    Private Sub Scrape_Pages()
        For i = 0 To mURL_List.Count - 1
            Debug.Print(i.ToString)
            Go_To_URl(mURL_List(i))
            Try
                Scrape_Page()
            Catch ex As Exception
                My.Computer.FileSystem.WriteAllText(mFile_Fail, i.ToString & vbTab & mURL_List(i), True)
            End Try

        Next
    End Sub
    Private Sub Scrape_Page()

        Dim dt As New DTO_Flex_Jobs
        Dim title = mDriver.FindElement(By.TagName("h1")).Text
        Dim titletemp As String() = title.Split("-")
        dt.Category = titletemp(0).Trim
        Dim jobs = mDriver.FindElements(By.CssSelector(".col-md-12.col-12"))
        For Each job In jobs
            dt.ID = mID.ToString
            dt.Title = job.FindElement(By.CssSelector(".job-title.job-link")).Text.Trim
            dt.URL = job.FindElement(By.CssSelector(".job-title.job-link")).GetAttribute("href")
            dt.Location = job.FindElement(By.CssSelector(".col.pr-0.job-locations.text-truncate")).Text
            dt.Location = dt.Location.Trim
            dt.Location = dt.Location.Replace("&nbsp;", "")
            dt.Desription = job.FindElement(By.CssSelector(".job-description")).Text
            Dim tags = job.FindElements(By.CssSelector(".job-tag.d-inline-block.mr-2.mb-1"))
            Try
                dt.Job_Type = tags(1).Text

            Catch ex As Exception
                dt.Job_Type = "unspecified"
            End Try
            Try
                dt.Job_Type_Remote = tags(0).Text
            Catch ex As Exception
                dt.Job_Type_Remote = "unspecified"
            End Try

            My.Computer.FileSystem.WriteAllText(mFile_Out, dt.ToString, True)
            mID = mID + 1

        Next

    End Sub

    Private Sub Read_Ref_File()
        Dim file = My.Computer.FileSystem.ReadAllText(mDirL1)
        Dim lines As String() = file.Split(vbNewLine)
        For Each line In lines
            Clean_Line(line)
            Dim parts As String() = line.Split(vbTab)
            mURL_List.Add(parts(1))
        Next
    End Sub
    Private Sub Read_Build_File()

        Dim file = My.Computer.FileSystem.ReadAllText(ref0)
        Dim lines As String() = file.Split(vbNewLine)
        Dim counter As Integer = 1
        For Each line In lines
            Clean_Line(line)
            Dim parts As String() = line.Split(vbTab)
            Dim index = Integer.Parse(parts(0))
            For i = 1 To index

                Dim ln As String = parts(1).Replace("page=1", "page=" & (i).ToString)
                ln = counter.ToString & vbTab & ln & vbNewLine
                My.Computer.FileSystem.WriteAllText(mDir & "L0.txt", ln, True)
                counter = counter + 1
            Next
        Next

    End Sub

    Private Sub Go_To_URl(ByVal url As String)
        mDriver.Navigate.GoToUrl(url)
    End Sub
    '
    Private Sub Clean_Line(ByRef line As String)
        line = line.Replace(vbLf, "")
        line = line.Replace(vbNewLine, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
    End Sub
    Private Sub Initialise_Driver()
        Dim options As New ChromeOptions
        options.BinaryLocation = mChrome_App
        options.AddExtension(mVPN_App)
        mDriver = New ChromeDriver(options)
        mDriver.Manage.Timeouts.PageLoad = New TimeSpan(0, 0, 60)
    End Sub
End Class
