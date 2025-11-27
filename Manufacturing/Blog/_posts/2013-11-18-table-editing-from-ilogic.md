---
layout: "post"
title: "Table editing from iLogic"
date: "2013-11-18 15:57:20"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/table-editing-from-ilogic.html "
typepad_basename: "table-editing-from-ilogic"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may have lots of data that you would like to make editable for the user. If it can be organized into a table then you could simply use a <strong>Form</strong> with a <strong>DataGridView</strong>&nbsp;on it. There is an article on providing custom dialogs from iLogic: <a title="" href="http://adndevblog.typepad.com/manufacturing/2013/06/use-vbnet-dialog-in-ilogic.html" target="_self">http://adndevblog.typepad.com/manufacturing/2013/06/use-vbnet-dialog-in-ilogic.html</a></p>
<p>This will be the same, but here we will expose the <strong>DataGridView</strong> which is placed on the <strong>Form</strong> so you can take advantage of all its functionality: <a title="" href="http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview(v=vs.100).aspx" target="_self">http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview(v=vs.100).aspx</a>&nbsp;&nbsp;</p>
<p>This is how you can use it from an <strong>iLogic Rule</strong>:&nbsp;</p>
<pre>Imports System.Windows.Forms
AddReference "C:\temp\MyTableForm.dll" 

Using mf As New MyTableForm.MyForm
  mf.Text = "Get Data"	
  Dim dgv As DataGridView = mf.dgvTable
  ' set columns
  Dim c1 As Integer = _
    dgv.Columns.Add("MyColumn1", "MyColumnHeader1")
  Dim c2 As Integer = _
    dgv.Columns.Add("MyColumn2", "MyColumnHeader2")
  ' add two rows
  Dim r1 As Integer = dgv.Rows.Add()
  Dim r2 As Integer = dgv.Rows.Add()
  ' disable sorting
  dgv.Columns(c1).SortMode = DataGridViewColumnSortMode.NotSortable 
  dgv.Columns(c2).SortMode = DataGridViewColumnSortMode.NotSortable
  ' set cell data
  dgv.Rows(r1).Cells(1).Value = "test1"
  dgv.Rows(r2).Cells(1).Value = "test2"
  ' make one of them read-only
  dgv.Rows(r2).Cells(1).ReadOnly = True
  ' show the dialog
  If mf.ShowDialog() = DialogResult.OK Then
    MsgBox(dgv.Rows(0).Cells(0).Value, MsgBoxStyle.Information, 
	  "Results")
  End If
End Using</pre>
<p>The result:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b014da695970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019b014da695970b image-full img-responsive" title="MyTableForm" src="/assets/image_7b97b2.jpg" alt="MyTableForm" border="0" /></a></p>
<p>Here is the source code of the VB.NET 2010 project:&nbsp;<span class="asset  asset-generic at-xid-6a0167607c2431970b019b014db72c970b"><a href="http://adndevblog.typepad.com/files/mytableform_2013-11-18.zip">Download MyTableForm_2013-11-18</a></span><br />The <strong>compiled dll</strong> is also part of the zip file in folder "<strong>MyTableForm\MyTableForm\bin\Release</strong>".</p>
