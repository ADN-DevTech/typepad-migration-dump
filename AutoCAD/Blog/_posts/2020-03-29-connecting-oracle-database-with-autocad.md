---
layout: "post"
title: "Connecting Oracle Database with AutoCAD"
date: "2020-03-29 16:52:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2020/03/connecting-oracle-database-with-autocad.html "
typepad_basename: "connecting-oracle-database-with-autocad"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script><p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p><br></p><p>Q. <strong>Problems connecting AutoCAD to Oracle database, is Oracle database is supported. How to use to API to retrieve information from my Oracle database to AutoCAD.</strong></p><p>A. From AutoCAD 2020 there is no 32 bit application, AutoCAD comes only in 64 bit application.</p><p>If you want to connect to Oracle, the Oracle client OLE Db drivers need to in 64 bit.</p><p>You need to install these based on your Oracle database installation, at my end I have <a href="https://www.oracle.com/database/technologies/oracle19c-windows-downloads.html">Oracle 19c database</a>, accordingly I need to install associated <a href="https://www.oracle.com/database/technologies/dotnet-odacdeploy-downloads.html">Oracle 19c OLE Db</a> drivers.</p><p><br></p><p><strong>Oracle Db</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51bad58200b-pi"><img width="532" height="171" title="OracleDB-19" style="display: inline; background-image: none;" alt="OracleDB-19" src="/assets/image_523004.jpg" border="0"></a></p><p><strong>Oracle Ole Db</strong></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f6f959200d-pi"><img width="532" height="129" title="Oracle-Ole-Db" style="display: inline; background-image: none;" alt="Oracle-Ole-Db" src="/assets/image_512625.jpg" border="0"></a></p><p><br></p><p>Now connecting to Oracle Db from AutoCAD VBAIDE</p><p><strong>Step1: Setting necessary reference files.</strong></p>
<pre><ul>
<li>AutoCAD 2021 Type Library</li>
<li>Ole Automation</li>
<li>Microsoft Activex Data Objects</li>
<li>Microsoft Activex Data Objects Recordset </li>
<li>OraOLEDb 1.0 Type Library</li>
</ul>
</pre><p>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b41915c200c-pi"><img width="355" height="317" title="VBA-References" style="display: inline; background-image: none;" alt="VBA-References" src="/assets/image_911809.jpg" border="0"></a></p><p><strong>Step2 : Preparing Connection String</strong></p><p><strong><br></strong></p><p>"Provider=OraOLEDB.Oracle;User ID=&lt;yourUserId&gt;;Password=&lt;yourPassword;Data Source=&lt;yourDatabase&gt;;"</p><p>For example:</p><p>"<em>Provider=OraOLEDB.Oracle;User ID=SYSTEM;Password=Abcdef!23;Data Source=moogalm19</em>;"</p><p><strong>UserId</strong> : The User you have created for your Database</p><p><strong>Password</strong>: The Password required to log in to Database</p><p><strong>DataSource</strong>: The Database to which you would like to connect.</p><p><strong>Step 3: Code to make a connection and open the Database.</strong></p><p><strong><br></strong></p>
<pre class="prettyprint lang-vb"> 
Sub ReadOracleDB()
    Dim adoDbConn   As New ADODB.Connection
    Dim adoDbRs     As New ADODB.Recordset
    Dim selectCmd   As New ADODB.Command
    
    Dim strCon      As String
    Dim RC, CC      As Long
    
    ' connection string, same userId and password, which used to logon to with sqlplus'
    ' Data Source = name of your Database'
    
    strCon = "Provider=OraOLEDB.Oracle;User ID=SYSTEM;Password=Aut0desk!23;Data Source=moogalm19;"
    adoDbConn.Open (strCon)
    
    ' open the table with adOpenStatic, so we traverse to end of all recordses'
    adoDbRs.Open "SELECT * FROM EMPLOYEES", adoDbConn, adOpenStatic
    
    If IsNull(adoDbRs.RecordCount) Or (adoDbRs.RecordCount = 0) Then
        MsgBox "No Records Found!"
        Exit Sub
    End If
    
    RC = adoDbRs.RecordCount
    CC = adoDbRs.Fields.Count
    
    Dim MyModelSpace As AcadModelSpace
    Set MyModelSpace = ThisDrawing.ModelSpace
    Dim pt(2)       As Double
    Dim MyTable     As AcadTable
    
    ' RC+2 accounts for Title and Header rows'
    
    Set MyTable = MyModelSpace.AddTable(pt, RC + 2, CC, 10, 60)
    
    Dim i           As Integer
    Dim j           As Integer
    
    
    With MyTable
        .RegenerateTableSuppressed = True
        .RecomputeTableBlock False
        .TitleSuppressed = False
        .HeaderSuppressed = False
        .SetTextStyle AcRowType.acTitleRow, "Standard"
        .SetTextStyle AcRowType.acHeaderRow, "Standard"
        .SetTextStyle AcRowType.acDataRow, "Standard"
        
        Dim col         As New AcadAcCmColor
        col.SetRGB 255, 0, 255
        
        ' title'
        col.SetRGB 194, 212, 235
        .SetCellBackgroundColor 0, 0, col
        col.SetRGB 127, 0, 0
        .SetCellContentColor 0, 0, col
        .SetCellType 0, 0, acTextCell
        .SetText 0, 0, "MOOGALM19"
        
        ' headers'
        
        i = i + 1
        For j = 0 To .Columns - 1
            .SetCellType i, j, acTextCell
            .SetText i, j, CStr(adoDbRs.Fields(j).Name)
            
        Next
        
        ' dataRows'
        
        For i = 2 To .Rows - 1
            For j = 0 To .Columns - 1
                .SetCellType i, j, acTextCell
                .SetText i, j, adoDbRs.Fields(j).Value
                
            Next j
            adoDbRs.MoveNext
        Next i
        
        .RegenerateTableSuppressed = False
        .RecomputeTableBlock True
        .Update
        .GetBoundingBox minp, maxp
        ZoomWindow minp, maxp
        ZoomScaled 0.9, acZoomScaledRelative
        
    End With
    
    ' Close the connection and free the memory'
    
    adoDbRs.Close
    Set adoDbRs = Nothing
    Set selectCmd = Nothing
    adoDbConn.Close
    Set adoDbConn = Nothing
       
    ThisDrawing.SetVariable "LWDISPLAY", 1
    
End Sub

</pre>
<p>Demo </p>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f6f96c200d-pi"><img width="417" height="205" title="Oracle-Server-Working" style="display: inline;" alt="Oracle-Server-Working" src="/assets/image_373688.jpg"></a>
