---
layout: "post"
title: "Counting UP and DOWN bends"
date: "2012-09-26 02:31:00"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/09/counting-up-and-down-bends.html "
typepad_basename: "counting-up-and-down-bends"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The bend information is stored in FlatBendResult. The property IsDirectionUp tells if the bend is up or down. The following code dumps the information of FlatBendResult collection thus we could know how many bends are UP / Down.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> GetBendResults()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> inventorAppType </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Type</span><span style="line-height: 140%;"> = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.</span><span style="line-height: 140%; color: #2b91af;">Type</span><span style="line-height: 140%;">.GetTypeFromProgID(</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> _InvApplication </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> Inventor.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;"> = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">System.Runtime.InteropServices.</span><span style="line-height: 140%; color: #2b91af;">Marshal</span><span style="line-height: 140%;">.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">GetActiveObject(</span><span style="line-height: 140%; color: #a31515;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; Set a reference to the sheet metal document.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; This assumes a part document is active.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oPartDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oPartDoc = _InvApplication.ActiveDocument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; Make sure the document is a sheet metal document.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oPartDoc.SubType &lt;&gt;&#0160; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(</span><span style="line-height: 140%; color: #a31515;">&quot;A sheet metal document must be open.&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oSheetMetalCompDef </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SheetMetalComponentDefinition</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oSheetMetalCompDef = oPartDoc.ComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">Not</span><span style="line-height: 140%;"> oSheetMetalCompDef.HasFlatPattern) </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSheetMetalCompDef.Unfold()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oFlatPattern </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">FlatPattern</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oFlatPattern = oSheetMetalCompDef.FlatPattern&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> oBendResult </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">FlatBendResult</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">For</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Each</span><span style="line-height: 140%;"> oBendResult </span><span style="line-height: 140%; color: blue;">In</span><span style="line-height: 140%;"> oFlatPattern.FlatBendResults</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; Internal Name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> strResult </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">String</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult = </span><span style="line-height: 140%; color: #a31515;">&quot;Internal Name: &quot;</span><span style="line-height: 140%;"> &amp; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oBendResult.InternalName &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;, &quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; top or bottom</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oBendResult.IsOnBottomFace </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult = strResult &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;On Bottom, &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult = strResult &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;On Top, &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; angle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult = strResult &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;Angle: &quot;</span><span style="line-height: 140%;"> &amp; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _InvApplication.ActiveDocument.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UnitsOfMeasure.GetStringFromValue(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oBendResult.Angle, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">UnitsTypeEnum</span><span style="line-height: 140%;">.kDefaultDisplayAngleUnits) &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;, &quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; Inner Radius</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult = strResult &amp;&#0160; _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Inner Radius: &quot;</span><span style="line-height: 140%;"> &amp;&#0160; _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _InvApplication.ActiveDocument.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UnitsOfMeasure.GetStringFromValue(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oBendResult.InnerRadius, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">UnitsTypeEnum</span><span style="line-height: 140%;">.kDefaultDisplayLengthUnits) &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;, &quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39; bend direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> oBendResult.IsDirectionUp </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;Bend Direction: &quot;</span><span style="line-height: 140%;"> &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;Bend Up&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strResult &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;Bend Direction: &quot;</span><span style="line-height: 140%;"> &amp; </span><span style="line-height: 140%; color: #a31515;">&quot;Bend Down&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;">&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Debug</span><span style="line-height: 140%;">.Print(strResult)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Next</span><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
</div>
