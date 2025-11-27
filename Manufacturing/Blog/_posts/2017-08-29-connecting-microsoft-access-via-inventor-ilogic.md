---
layout: "post"
title: "Connecting Microsoft Access via Inventor iLogic"
date: "2017-08-29 22:02:19"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/08/connecting-microsoft-access-via-inventor-ilogic.html "
typepad_basename: "connecting-microsoft-access-via-inventor-ilogic"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener noreferrer" target="_blank">Chandra shekar Gopal</a></p>
<p>If you want to connect <strong>Microsoft Access</strong> Database through Inventor iLogic, a data connectivity driver called <strong>“2007 Office System Driver : Data Connectivity Components”</strong> is required to install in system. It can be downloaded from this link(<span class="asset  asset-generic at-xid-6a0167607c2431970b0240a494dbcd200c img-responsive"><a href="https://adndevblog.typepad.com/files/employeeinfo-1.zip">Download Employeeinfo</a>)</span></p>
<p>After successful installation of driver,the following iLogic code can be used to access <strong>Microsoft Access</strong> database in <strong>Inventor 2017</strong></p>
<blockquote>
<pre>AddReference &quot;System.Data&quot;
AddReference &quot;System.Core&quot;
AddReference &quot;System.Xml&quot;

Imports System.Data.OleDb
Imports System.Data
Imports System.Xml


Sub Main()

	Dim Table_ As String = &quot;EmployeeInfo&quot;
	Dim query As String = &quot;SELECT * FROM &quot; &amp; Table_
	Dim MDBConnString_ As String = &quot;Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Temp\EmployeeInfo.accdb;Persist Security Info=False;&quot;
	Dim ds As New DataSet
	Dim cnn As OleDbConnection = New OleDbConnection(MDBConnString_)
	cnn.Open()
	Dim cmd As New OleDbCommand(query, cnn)
	Dim da As New OleDbDataAdapter(cmd)
	da.Fill(ds, Table_)
	cnn.Close()
	Dim t1 As DataTable = ds.Tables(Table_)
	Dim row As DataRow
	Dim Item(2) As String
	For Each row In t1.Rows
		MessageBox.Show(&quot;EID : &quot; &amp; row(0) &amp; &quot; and Employee Name : &quot; &amp; row(1)
	Next
End Sub</pre>
</blockquote>
<p>To demonstrate code, a sample database <strong>(EmployeeInfo.accdb)</strong> is created and that would like as shown below.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09bdebf9970d-pi"><img alt="Sample_Database" border="0" height="284" src="/assets/image_2b8a8c.jpg" style="display: inline; background-image: none;" title="Sample_Database" width="750" /></a></p>
<p>Sample database can be downloaded from<a href="http://adndevblog.typepad.com/files/employeeinfo.zip">&#0160;here.</a> After downloading database to desire location, path of database should be updated in the above iLogic code.</p>
<p><strong>Result:</strong></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09bdebfd970d-pi"><img alt="1" border="0" height="147" src="/assets/image_0c1c9b.jpg" style="margin: 0px; display: inline; background-image: none;" title="1" width="244" /></a><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c91aae3e970b-pi"><img alt="2" border="0" height="148" src="/assets/image_eacff6.jpg" style="margin: 0px; display: inline; background-image: none;" title="2" width="244" /></a><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a51d63970c-pi"><img alt="3" border="0" height="151" src="/assets/image_213177.jpg" style="display: inline; background-image: none;" title="3" width="230" /></a><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a51d74970c-pi"><img alt="4" border="0" height="148" src="/assets/image_a45f4f.jpg" style="display: inline; background-image: none;" title="4" width="244" /></a><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a51d7e970c-pi"><img alt="5" border="0" height="162" src="/assets/image_c2cf0e.jpg" style="display: inline; background-image: none;" title="5" width="242" /></a><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09bdec09970d-pi"><img alt="6" border="0" height="157" src="/assets/image_52ee35.jpg" style="display: inline; background-image: none;" title="6" width="233" /></a></p>
