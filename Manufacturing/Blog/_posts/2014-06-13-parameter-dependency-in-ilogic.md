---
layout: "post"
title: "Parameter dependency in iLogic form"
date: "2014-06-13 07:30:59"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/parameter-dependency-in-ilogic.html "
typepad_basename: "parameter-dependency-in-ilogic"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may want to show the possible parameter values of a part in an iLogic form where the parameters depend on each other, like in case of a Content Center part: if x is 2, then y can ony be 3 or 4, etc</p>
<p>Let's say this is the table that contains the possible values:</p>
<table style="border: 1px solid;">
<tbody>
<tr><th>Width</th><th>Depth</th><th>Height</th></tr>
<tr>
<td style="border: 1px solid;">1</td>
<td style="border: 1px solid;">1</td>
<td style="border: 1px solid;">1</td>
</tr>
<tr>
<td style="border: 1px solid;">1</td>
<td style="border: 1px solid;">1</td>
<td style="border: 1px solid;">2</td>
</tr>
<tr>
<td style="border: 1px solid;">1</td>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">1</td>
</tr>
<tr>
<td style="border: 1px solid;">1</td>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">2</td>
</tr>
<tr>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">2</td>
</tr>
<tr>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">3</td>
</tr>
<tr>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">3</td>
<td style="border: 1px solid;">3</td>
</tr>
<tr>
<td style="border: 1px solid;">2</td>
<td style="border: 1px solid;">3</td>
<td style="border: 1px solid;">4</td>
</tr>
</tbody>
</table>
<p>We can create multi-value parameters for each parameter to store their possible values: WidthValues, DepthValues, HeightValues</p>
<p>We can now place these parameters on a form and then create a rule that is using those parameters, e.g. "Update":</p>
<pre>' Need this for List(Of Double) which has a Contains function
Imports System.Collections.Generic

'MsgBox(Str(WidthValues) + "; " + 
'  Str(DepthValues) + "; " + 
'  Str(HeightValues))

' Let's say these are the possible values (pv) for the parameters
' that we got from the Content Center
' Width, Depth, Height
Dim pv = New List(Of Double)
pv.AddRange(New Double(){
  1, 1, 1,
  1, 1, 2,
  1, 2, 1,
  1, 2, 2,
  2, 2, 2,
  2, 2, 3,
  2, 3, 3,
  2, 3, 4})

' WIDTH -------------------------------------------------------------- 
' Width is the main param so all its possible values are listed
Dim widths = New List(Of Double) 
widths.AddRange(New Double(){1, 2})
MultiValue.List("WidthValues") = widths

' If the current width value is not in the list then use the first one
If Not widths.Contains(WidthValues) Then WidthValues = widths(0)

' DEPTH -------------------------------------------------------------- 
' Depending on the width value we set the DepthValues
Dim depths = New List(Of Double)
For i As Integer = 0 To pv.Count - 1 Step 3
  If pv(i) = WidthValues Then
    If Not depths.Contains(pv(i + 1)) Then depths.Add(pv(i + 1))
  End If
Next

MultiValue.List("DepthValues") = depths

' If the current depth value is not in the list then use the first one
If Not depths.Contains(DepthValues) Then DepthValues = depths(0)

' HEIGHT ------------------------------------------------------------- 
' Depending on the width and depth value we set the HeightValues
Dim heights = New List(Of Double)
For i As Integer = 0 To pv.Count - 1 Step 3
  If pv(i) = WidthValues And pv(i + 1) = DepthValues Then
    If Not heights.Contains(pv(i + 2)) Then heights.Add(pv(i + 2))
  End If
Next

MultiValue.List("HeightValues") = heights

' If the current height value is not in the list then use the first one
If Not heights.Contains(HeightValues) Then HeightValues = heights(0)

' Let's change the model based on the values
Width = WidthValues
Depth = DepthValues
Height = HeightValues

InventorVb.DocumentUpdate()
</pre>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd1d6c4b970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fd1d6c4b970b img-responsive" title="Dependantvalues" src="/assets/image_fed30c.jpg" alt="Dependantvalues" border="0" /></a></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01a73dd827e8970d img-responsive">Inventor 2015 sample part file:&nbsp;<a href="http://adndevblog.typepad.com/files/ilogictest.ipt">Download ILogicTest</a></span></p>
