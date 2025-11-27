---
layout: "post"
title: "Add one custom row to an existing content centre family"
date: "2012-10-17 01:09:34"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/10/add-one-custom-row-to-an-existing-content-centre-family.html "
typepad_basename: "add-one-custom-row-to-an-existing-content-centre-family"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a>&#160;</p>  <p><strong>Issue</strong>&#160; <br />If I place the part from content centre, Inventor lists the available rows of a family, and I select one of them to insert to the assembly. But not all options I need are there. For example one family has a diameter and a length . Inventor would provide:</p>  <p>Diameter&#160;&#160;&#160;&#160; Length    <br />10&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 1000    <br />20&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 1000    <br />30&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 1000</p>  <p>But I need a part with diameter =&#160; 10 and length = 2000. </p>  <p>How to add the required row?</p>  <p><strong>Solution</strong></p>  <p>ContentFamily<strong>.TableRows.Add</strong> provides you with the ability to add a new row. <font color="#ff0000">This applies to custom library only</font>. </p>  <p>The VB definition is:</p>  <pre><strong>Sub</strong> <strong>Add</strong>(<strong>ByRef</strong> <strong>RowData</strong> <strong>As</strong> SAFEARRAY(BSTR), <br />        <strong>ByRef</strong> <strong>Position</strong> <strong>As</strong> [defaultvalue(-1)] <strong>long</strong>, <br />        <strong>Result</strong> <strong>As</strong> [out, retval] <a href="Inventor__ContentTableRow.html">ContentTableRow</a>*)</pre>

<p>RowData:&#160; Input String array that contains the new values for the row. The array should be the same size as the number of columns and the values are defined in the same order as the column order.&#160; <strong>Position</strong>:&#160; Optional input Long that defines the position within the table where the new row should be created. If this argument is not supplied or is out of range the row will be created at the end of the table.&#160; </p>

<p>And please note: any changes to the table are not actually applied until the ContentFamily.Save method is called.</p>

<p>Following is a small VB.NET code demo.</p>

<p>&#160;</p>
<font face="Courier New"></font>

<div style="font-family: courier new; background: white; color: black; font-size: 9pt">
  <p style="margin: 0px"><span style="line-height: 140%; color: blue">Sub</span><span style="line-height: 140%"> addCCFamilyRow()</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' assume Inventor application is available</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> m_inventorApp </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> Inventor.</span><span style="line-height: 140%; color: #2b91af">Application</span><span style="line-height: 140%"> = </span><span style="line-height: 140%; color: blue">Nothing</span><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; m_inventorApp = System.Runtime.InteropServices.</span><span style="line-height: 140%; color: #2b91af">Marshal</span><span style="line-height: 140%">.GetActiveObject(</span><span style="line-height: 140%; color: #a31515">&quot;Inventor.Application&quot;</span><span style="line-height: 140%">)</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get Content Center</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> oContentCenter </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">ContentCenter</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; oContentCenter = m_inventorApp.ContentCenter</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' Get the content node (category) &quot;Fasteners:Bolts:Hex Head&quot;</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> oContentNode </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">ContentTreeViewNode</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; oContentNode = </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; oContentCenter.TreeViewTopNode.ChildNodes.Item(</span><span style="line-height: 140%; color: #a31515">&quot;Fasteners&quot;</span><span style="line-height: 140%">).</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; ChildNodes.Item(</span><span style="line-height: 140%; color: #a31515">&quot;Bolts&quot;</span><span style="line-height: 140%">).</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; ChildNodes.Item​(</span><span style="line-height: 140%; color: #a31515">&quot;Hex Head&quot;</span><span style="line-height: 140%">)</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' Get the &quot;ISO 4015&quot; Family object.</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> oFamily </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">ContentFamily</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">For</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Each</span><span style="line-height: 140%"> oFamily </span><span style="line-height: 140%; color: blue">In</span><span style="line-height: 140%"> oContentNode.Families</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">If</span><span style="line-height: 140%"> oFamily.DisplayName = </span><span style="line-height: 140%; color: #a31515">&quot;ISO 4015&quot;</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Then</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Exit For</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">End</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">If</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Next</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get the first row</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> oOneRow </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">ContentTableRow</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; oOneRow = oFamily.TableRows(1)</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">'just a demo. we reuse the values of first row</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' and change the value of one</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' a dynamic array </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> oNewRow()&#160; </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">String</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' each cell</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> oCell </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">ContentTableCell</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Dim</span><span style="line-height: 140%"> i </span><span style="line-height: 140%; color: blue">As</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Integer</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; i = 0</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">For</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Each</span><span style="line-height: 140%"> oCell </span><span style="line-height: 140%; color: blue">In</span><span style="line-height: 140%"> oOneRow</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' get the value of each cell</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">ReDim</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Preserve</span><span style="line-height: 140%"> oNewRow(i)</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; oNewRow(i) = oCell.Value </span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' In this case</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">'Fa​steners &gt;&gt;Bolts&gt;&gt;Hex Head</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">'we will modify [Bolt Length]</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' which is number 10 column</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' Note: the array bases from 0 </span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">If</span><span style="line-height: 140%"> i = 10 </span><span style="line-height: 140%; color: blue">Then</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' set the new value</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">' e.g. set a special value</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oNewRow(i) = </span><span style="line-height: 140%; color: #a31515">&quot;10.11111&quot;</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">End</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">If</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; i = i + 1</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">Next</span></p>

  <p style="margin: 0px">&#160;</p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">'add a new row</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; oFamily.TableRows.Add( oNewRow)</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">'save family to save the change</span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; oFamily.Save</span></p>

  <p style="margin: 0px"><span style="line-height: 140%; color: blue">End</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">Sub</span></p>
</div>
