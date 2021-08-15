Imports System.Threading
Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports System.Net
Imports Chilkat

Public Class Dummy




    Private mDir As String
    Private mFile_In As String
    Private mFile_Out As String
    Private mScraperLst As List(Of String)
    Private mDto_ID As Integer
    Private mdirRef As String
    Private mdirChrome As String
    Private mdirVPN As String

    Private mDriver As IWebDriver
    Private mURL_Base As String

    Private mVPN_Rotation_Index As Integer







    Public Sub New()
        Initialise()
        ' Initialise_Driver()
    End Sub
    Private Sub Initialise()
        mDir = "C:\Users\SONY\Desktop\Software And Files\Files\Expert23\SCRAPERS\Task1\Lebanese Working Abbroad\dir\output\L1\"
        mdirRef = "C:\Users\SONY\Desktop\Software And Files\Files\Expert23\SCRAPERS\Task1\Google_For_Selenium_78.0.3904.7000\Chrome\Application\"
        mdirChrome = mdirRef & "Chrome.exe"
        mdirVPN = mdirRef & "2.0.0_0.crx"
        mURL_Base = "https://lb.linkedin.com/jobs/search?keywords=People%20Search&location=%D8%B9%D8%A7%D9%84%D9%85%D9%8A%D9%8B%D8%A7&geoId=92000000&trk=organization_guest_jobs-search-bar_search-submit&redirect=false&position=1&pageNum=0"
        mVPN_Rotation_Index = 0
        mFile_In = mDir & "1combination.txt"
        mFile_Out = mDir & "L1Demo1.txt"
        mScraperLst = New List(Of String)
        mDto_ID = 0

    End Sub



    Public Sub Scrape()


        '    Go_To_URl("https://www.linkedin.com/")
        Dim coookies As New Dictionary(Of String, String)

        Dim Mht As New Chilkat.Mht
        Dim mhtDoc As String = Mht.GetMHT("https://www.linkedin.com")
        Dim unpackDir As String = mDir
        Dim html As String = "linkedin.html"
        Dim success As Boolean = Mht.UnpackMHTString(mhtDoc, unpackDir, html, "obj2")

        Dim x = 1 + 1
        'Dim req As New HttpRequest
        ' Dim http As New Http

        '    For Each cookie In mDriver.Manage.Cookies.AllCookies
        '  Dim ccc = New CookieCollection()
        '
        '
        ' coookies.Add(cookie.Name, cookie.Value)
        ' webClient.Headers.Add(cookie.Name, cookie.Value)
        '    Next




        ' req.SetFromUrl("https://lb.linkedin.com/jobs/search?keywords=People%20Search&location=%D8%B9%D8%A7%D9%84%D9%85%D9%8A%D9%8B%D8%A7&geoId=92000000&trk=organization_guest_jobs-search-bar_search-submit&redirect=false&position=1&pageNum=0")
        '  req.HttpVerb = "Get"

        ' Dim resp As Chilkat.HttpResponse
        '  Dim domain As String = "www..linkedin.com"
        '  Dim port As Integer = 80
        '   Dim ssl As Boolean = False
        ' 'resp = http.SynchronousRequest(domain, port, ssl, req)
        '  If (http.LastMethodSuccess <> True) Then
        '     '       Debug.WriteLine(http.LastErrorText)
        '    Exit Sub
        '      '    End If


        ' Display the HTML body of the response.
        '   If (resp.StatusCode = 200) Then
        '   ' Show the last HTTP request header sent, which should include
        ' our cookies...
        ' Debug.WriteLine(http.LastHeader)
        'Else
        'Debug.WriteLine("HTTP Response Status = " & resp.StatusCode)
        ' End If




    End Sub


    Public Sub test()

        Dim http As New Chilkat.Http

        ' The Cookie header field has this format:
        ' Cookie: name1=value1 [; name2=value2] ...

        ' Build an HTTP POST request:
        Dim req As New Chilkat.HttpRequest
        req.SetFromUrl("http://www.chilkatsoft.com/echoPost.asp")
        req.HttpVerb = "POST"

        req.AddParam("param1", "value1")
        req.AddParam("param2", "value2")

        ' To add cookies to any HTTP request sent by a Chilkat HTTP method
        ' that uses an HTTP request object, add the cookies to the
        ' request object by calling AddHeader.  

        ' Add two cookies:
        req.AddHeader("Cookie", "user=""mary""; city=""Chicago""")

        ' Send the HTTP POST.  
        ' (The cookies are sent as part of the HTTP header.)

        Dim resp As Chilkat.HttpResponse
        Dim domain As String = "www.chilkatsoft.com"
        Dim port As Integer = 80
        Dim ssl As Boolean = False
        resp = http.SynchronousRequest(domain, port, ssl, req)
        If (http.LastMethodSuccess <> True) Then
            Debug.WriteLine(http.LastErrorText)
            Exit Sub
        End If


        ' Display the HTML body of the response.
        If (resp.StatusCode = 200) Then
            ' Show the last HTTP request header sent, which should include
            ' our cookies...
            Debug.WriteLine(http.LastHeader)
        Else
            Debug.WriteLine("HTTP Response Status = " & resp.StatusCode)
        End If




        Debug.WriteLine("---------------------")

        ' Some Chilkat HTTP methods do not use an HTTP request object. 
        ' For these methods, such as for QuickGetStr, cookies (or any HTTP request header) 
        ' are added by calling AddQuickHeader.  


        Dim html As String = http.QuickGetStr("http://www.w3.org/")
        If (http.LastMethodSuccess <> True) Then
            Debug.WriteLine(http.LastErrorText)
        Else
            ' Show the last HTTP request header sent, which should include
            ' our cookies...
            Debug.WriteLine(http.LastHeader)
        End If




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
    End Sub
    Private Sub Clean_Line(ByRef line As String)
        line = line.Replace(vbLf, "")
        line = line.Replace(vbCr, "")
        line = line.Replace(vbCrLf, "")
        line = line.Replace(vbNewLine, "")
    End Sub



End Class
