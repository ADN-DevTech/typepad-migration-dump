---
layout: "post"
title: "delete and add rows to existing CustomTable"
date: "2015-09-16 02:07:04"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/09/delete-and-add-rows-to-existing-customtable.html "
typepad_basename: "delete-and-add-rows-to-existing-customtable"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Question:</strong></p>
<p>Given there is a drawing with at least one custom table，and the existing table is not empty. Can I delete the existing rows and add the new data I need?</p>
<p><strong>Solution:</strong></p>
<p>Each Row object of CustomTable.Rows provides Delete method that can remove this row. And CustomTable.Rows also provides Add method to add a new row. So the first way is to delete all old rows one by one, and add the new row one by one. e.g. this is a test drawing with a custom table as below.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d158c6df970c img-responsive"><a href="http://adndevblog.typepad.com/files/test_dynamic_load-1.zip"> <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7cedf17970b img-responsive"><a href="http://adndevblog.typepad.com/files/testdrawing-2016.idw">Download Testdrawing-2016</a></span></a></span></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0873217c970d-pi"><img alt="image" border="0" height="134" src="/assets/image_56ae40.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="410" /></a></p>
<p>The code below will delete all old rows and add new rows.</p>
<table border="1" cellpadding="2" cellspacing="0" width="400">
<tbody>
<tr>
<td valign="top" width="398">Sub modifyTable_Way1() <br />&#0160; <br />&#0160;&#0160;&#0160; Dim oDrawDoc As DrawingDocument <br />&#0160;&#0160;&#0160; Set oDrawDoc = ThisApplication.ActiveDocument <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; &#39; Set a reference to the active sheet. <br />&#0160;&#0160;&#0160; Dim oSheet As Sheet <br />&#0160;&#0160;&#0160; Set oSheet = oDrawDoc.ActiveSheet <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim oTB As CustomTable <br />&#0160;&#0160;&#0160; Set oTB = oSheet.CustomTables(1) <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; &#39;delete the old rows <br />&#0160;&#0160;&#0160; Dim oRow As Row <br />&#0160;&#0160;&#0160; For Each oRow In oTB.Rows&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oRow.Delete&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Next <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; &#39;assume the new data is available <br />&#0160;&#0160;&#0160; Dim oContents(1 To 3) As String <br />&#0160;&#0160;&#0160; oContents(1) = &quot;1 - new&quot; <br />&#0160;&#0160;&#0160; oContents(2) = &quot;1 - new&quot; <br />&#0160;&#0160;&#0160; oContents(3) = &quot;Brass- new&quot;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Call oTB.Rows.Add(0, False, oContents)&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; oContents(1) = &quot;2 - new&quot; <br />&#0160;&#0160;&#0160; oContents(2) = &quot;2 - new&quot; <br />&#0160;&#0160;&#0160; oContents(3) = &quot;Aluminium- new&quot;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Call oTB.Rows.Add(0, False, oContents) <br />&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; oContents(1) = &quot;3 - new&quot; <br />&#0160;&#0160;&#0160; oContents(2) = &quot;3 - new&quot; <br />&#0160;&#0160;&#0160; oContents(3) = &quot;Steel- new&quot;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Call oTB.Rows.Add(0, False, oContents) <br />&#0160;&#0160;&#0160;&#0160; <br />End Sub</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p>After the code, the table becomes:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d158c656970c-pi"><img alt="image" border="0" height="137" src="/assets/image_adcb74.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="412" /></a></p>
<p>&#0160;</p>
<p>The second way is to make a note on the columns definitions of the CustomTable, call CustomTable.Delete to delete the table, add the table again with the new data.</p>
<table border="1" cellpadding="2" cellspacing="0" width="400">
<tbody>
<tr>
<td valign="top" width="400">
<p>Sub modifyTable_Way2()</p>
<p><br /> &#39; Set a reference to the drawing document. <br />&#0160;&#0160;&#0160; &#39; This assumes a drawing document is active. <br />&#0160;&#0160;&#0160; Dim oDrawDoc As DrawingDocument <br />&#0160;&#0160;&#0160; Set oDrawDoc = ThisApplication.ActiveDocument <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; &#39; Set a reference to the active sheet. <br />&#0160;&#0160;&#0160; Dim oSheet As Sheet <br />&#0160;&#0160;&#0160; Set oSheet = oDrawDoc.ActiveSheet <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim oTB As CustomTable <br />&#0160;&#0160;&#0160; Set oTB = oSheet.CustomTables(1) <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; Dim oTitles() As String <br />&#0160;&#0160;&#0160; ReDim Preserve oTitles(1 To oTB.Columns.Count) <br />&#0160;&#0160;&#0160; Dim i As Integer <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; For i = 1 To oTB.Columns.Count <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTitles(i) = oTB.Columns(i).Title <br />&#0160;&#0160;&#0160; Next</p>
<p>&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; &#39; Set the contents of the custom table (contents are set row-wise) <br />&#0160;&#0160;&#0160; Dim oContents(1 To 9) As String <br />&#0160;&#0160;&#0160; oContents(1) = &quot;1 - New&quot; <br />&#0160;&#0160;&#0160; oContents(2) = &quot;1- New&quot; <br />&#0160;&#0160;&#0160; oContents(3) = &quot;Brass- New&quot; <br />&#0160;&#0160;&#0160; oContents(4) = &quot;2- New&quot; <br />&#0160;&#0160;&#0160; oContents(5) = &quot;2- New&quot; <br />&#0160;&#0160;&#0160; oContents(6) = &quot;Aluminium- New&quot; <br />&#0160;&#0160;&#0160; oContents(7) = &quot;3- New&quot; <br />&#0160;&#0160;&#0160; oContents(8) = &quot;1- New&quot; <br />&#0160;&#0160;&#0160; oContents(9) = &quot;Steel- New&quot; <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160; &#39; Create new custom table <br />&#0160;&#0160;&#0160; Dim oTB_New As CustomTable <br />&#0160;&#0160;&#0160; Set oTB_New = oSheet.CustomTables.Add</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oTB.Title, oTB.Position, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTB.Columns.Count,&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTB.Rows.Count, oTitles, oContents)&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160; &#39;delete the old table <br />&#0160;&#0160;&#0160; oTB.Delete <br />&#0160;&#0160;&#0160;&#0160; <br />End Sub</p>
</td>
</tr>
</tbody>
</table>
