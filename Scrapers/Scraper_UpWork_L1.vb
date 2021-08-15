Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Remote
Imports OpenQA.Selenium.Support.UI
Public Class Scraper_UpWork_L1
    Private mDriver As IWebDriver
    Private mDirChrome As String
    Private mChrome_App As String
    Private mVPN_App As String
    Private mDTO_L1 As List(Of DTO_Up_Work_L1)
    Private mFile_Out As String
    Private mID = 1
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        mDirChrome = "C:\Users\pc\Desktop\Software and Files\Expert 23\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mChrome_App = mDirChrome & "chrome.exe"
        mVPN_App = mDirChrome & "2.0.0_0.crx"
        mDTO_L1 = New List(Of DTO_Up_Work_L1)
        mFile_Out = "C:\Users\pc\Desktop\Software and Files\Expert 23\Scrapers\Flex Jobs\DIR\UPWORKL1.txt"
    End Sub
    Public Sub Scrape()
        Initialise_Driver()

        Go_To_URl("https://www.upwork.com/freelance-jobs/")
        Click_On_Radios()
    End Sub

    Private Sub Click_On_Radios()
        Dim radios = mDriver.FindElements(By.CssSelector(".mb-10.up-radio.up-radio"))
        For Each radio In radios
            Dim dt = New DTO_Up_Work_L1
            dt.Category = radio.Text.Trim()
            Utilities.Click_Elelment_No_Wait(radio.FindElement(By.TagName("input")), mDriver)

            Scrape_Links(dt)
        Next
    End Sub
    Private Sub Scrape_Links(ByRef dt As DTO_Up_Work_L1)
        Dim hyperlinks = mDriver.FindElements(By.CssSelector(".col-md-6.col-lg-4.mb-10"))
        For Each hyperlink In hyperlinks
            dt.SubCategory = hyperlink.Text.Trim
            dt.ID = mID.ToString
            dt.URL = hyperlink.FindElement(By.TagName("a")).GetAttribute("href")
            Debug.Print(dt.ToString)
            My.Computer.FileSystem.WriteAllText(mFile_Out, dt.ToString(), True)
            mID = mID + 1
        Next
    End Sub
    Private Sub Go_To_URl(ByVal url As String)
        mDriver.Navigate.GoToUrl(url)
    End Sub
    Private Sub Initialise_Driver()
        Dim options As New ChromeOptions
        options.BinaryLocation = mChrome_App
        options.AddExtension(mVPN_App)
        mDriver = New ChromeDriver(options)
        mDriver.Manage.Timeouts.PageLoad = New TimeSpan(0, 0, 60)
    End Sub
End Class
