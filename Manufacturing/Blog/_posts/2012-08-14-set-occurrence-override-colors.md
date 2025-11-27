---
layout: "post"
title: "Set occurrence override colors"
date: "2012-08-14 06:08:42"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/set-occurrence-override-colors.html "
typepad_basename: "set-occurrence-override-colors"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The <a href="http://modthemachine.typepad.com/my_weblog/2009/02/controlling-part-colors.html">post</a> on modthemachine introduces how to control colors of part and the occurrence of assembly with an existing render style. But it does not affect the active render style of the document of the occurrence. The code below sets&#0160; occurrence render style with a new one. It also shows how to do if you want change the active render style of the occurrence’s document.</p>
<p>&#0160;</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; applies render style to defintion of component</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#39; occurrence</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> OverrideRenderStyleToOccurrence()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Const</span><span style="line-height: 140%;"> kStyleName = </span><span style="line-height: 140%; color: #a31515;">&quot;MyRS&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160; &#39; assume we have had Inventor application</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; Dim</span><span style="line-height: 140%;"> oDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AssemblyDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; On</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Error</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Resume</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oDoc = _InvApplication.ActiveDocument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160; If</span><span style="line-height: 140%;"> (oDoc.DocumentType &lt;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">&#0160;&#0160;&#0160;&#0160;&#0160; DocumentTypeEnum</span><span style="line-height: 140%;">.kAssemblyDocumentObject) </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(</span><span style="line-height: 140%; color: #a31515;">&quot;There must be an active assembly&quot;</span><span style="line-height: 140%;"> + _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; “document. Exiting ...&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160; End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160; &#39; get occurrence from selection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160; Dim</span><span style="line-height: 140%;"> oSelectSet </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SelectSet</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; oSelectSet = oDoc.SelectSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160; If</span><span style="line-height: 140%;"> oSelectSet.Count &lt;&gt; 1 </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox(</span><span style="line-height: 140%; color: #a31515;">&quot;A component must be selected.&quot;</span><span style="line-height: 140%;"> + _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;Exiting ...&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">Exit Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; Dim</span><span style="line-height: 140%;"> oOccurrence </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ComponentOccurrence</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oOccurrence = oSelectSet.Item(1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; &#39; get source doc of occurrence</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; Dim</span><span style="line-height: 140%;"> oOccDoc </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oOccDoc = oOccurrence.Definition.Document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; &#39; create new render style</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; Dim</span><span style="line-height: 140%;"> oStyle </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">RenderStyle</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oStyle = oDoc.RenderStyles(kStyleName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; If</span><span style="line-height: 140%;"> oStyle </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oStyle = oDoc.RenderStyles.Add(kStyleName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;&#0160;&#0160; End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oStyle.SetAmbientColor(50, 100, 200)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oStyle.SetDiffuseColor(50, 100, 200)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oStyle.SetEmissiveColor(50, 100, 200)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oStyle.SetSpecularColor(50, 100, 200)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; &#39; this does not affect the active render style </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; &#39; (real style) of the occurrence&#39;s document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oOccurrence.SetRenderStyle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">&#0160;&#0160;&#0160;&#0160; StyleSourceTypeEnum</span><span style="line-height: 140%;">.kOverrideRenderStyle,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oStyle)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; &#39; you can change the active renderstyle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; &#39; if this occurrence is from a part</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160; &#39;If (TypeOf oOccDoc Is PartDocument) Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;&#0160;&#0160;&#0160; Dim oPartDoc As PartDocument = oOccDoc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;&#0160;&#0160;&#0160; oPartDoc.ActiveRenderStyle = oStyle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;&#0160;&#0160;&#0160; &#39;End If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160; &#39; comment following line, if you do not want <br />&#0160; ‘to save document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">&#39;oPartDoc.Save()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
</div>
