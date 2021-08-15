Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Remote
Imports OpenQA.Selenium.Support.UI
Public Class Scraper
    Private mDriver As IWebDriver
    Private mDirChrome As String
    Private mChrome_App As String
    Private mVPN_App As String
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        mDirChrome = "C:\Users\pc\Desktop\Software and Files\Expert 23\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mChrome_App = mDirChrome & "chrome.exe"
        mVPN_App = mDirChrome & "2.0.0_0.crx"
    End Sub
    Public Sub Scrape()
        Initialise_Driver()
        Go_To_URl("https://www.google.com")
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
