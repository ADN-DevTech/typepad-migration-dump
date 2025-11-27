---
layout: "post"
title: "Get a notification when a flat pattern changes?"
date: "2012-05-27 20:57:03"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/by-xiaodong-liang-issuewe-need-to-get-a-notification-when-a-flat-pattern-changes-by-an-internal-update-of-inventor-for-exam.html "
typepad_basename: "by-xiaodong-liang-issuewe-need-to-get-a-notification-when-a-flat-pattern-changes-by-an-internal-update-of-inventor-for-exam"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue</strong><br />We need to get a notification when a flat pattern changes by an internal update of Inventor. For example when we change a sketch geometries in a model with an existing flat pattern, the flat pattern automatically gets updated by Inventor. DocumentEvents::OnChange fires, but how can we know that the flat pattern has been really changed when we only switch from flat pattern to folded model, without any change in the real flat pattern.</p>
<p><strong>Solution</strong><br /> You could check Context if it is switching between folder mode and flatten mode. This code snippet below assumes you have had an event class which implements OnChange.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oDocument </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Document </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oDocumentEvents </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> DocumentEvents </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> startEvents()</span><span style="line-height: 140%;">&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oDocument = m_invApp.ActiveDocument </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oDocumentEvents = oDocument.DocumentEvents</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">AddHandler</span><span style="line-height: 140%;"> oDocumentEvents.OnChange,</span><span style="color: blue; line-height: 140%;">AddressOf</span><span style="line-height: 140%;"> oDocumentEvents_OnChange</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;"><br /></span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> printContext(Context </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> NameValueMap)&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> j = 1 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> Context.count&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oEachContext </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">&#0160; = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Context.Item(j)&#0160;&#0160;&#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; has sub context</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;"> oEachContext </span><span style="color: blue; line-height: 140%;">is</span><span style="line-height: 140%;"> Array </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> k = 0 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> oEachContext.Length -1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; Debug.Print ( </span><span style="color: #a31515; line-height: 140%;">&quot;[SubContext Name]&quot;</span><span style="line-height: 140%;"> &amp; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; oEachContext(k))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Debug.Print( </span><span style="color: #a31515; line-height: 140%;">&quot;[Context Name] &quot;</span><span style="line-height: 140%;">&#0160; &amp;&#0160;&#0160; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Context.Name(j)&#0160; &amp; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Value:&quot;</span><span style="line-height: 140%;">&#0160; &amp;&#0160; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Context.Value(Context.Name(j) ) ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> ex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Exception</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Next</span><span style="line-height: 140%;">&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;"><br /></span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> oDocumentEvents_OnChange(</span><span style="color: blue; line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> ReasonsForChange </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Inventor.CommandTypesEnum, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> BeforeOrAfter </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Inventor.EventTimingEnum, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> Context </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Inventor.NameValueMap, _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">ByRef</span><span style="line-height: 140%;"> HandlingCode </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Inventor.HandlingCodeEnum)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">TypeOf</span><span style="line-height: 140%;"> m_invApp.ActiveEditObject </span><span style="color: blue; line-height: 140%;">Is</span><span style="line-height: 140%;"> FlatPattern </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> isSwitching </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Boolean</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; isSwitching = </span><span style="color: blue; line-height: 140%;">False</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> Context.count = 2 </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> I = 1 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> Context.count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> Context.Value(Context.Name(I)) =&#0160; </span><span style="color: #a31515; line-height: 140%;">_</span></p>
<p style="margin: 0px;"><span style="color: #a31515; line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;Switch Part Representation&quot;</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; is swtiching</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; isSwitching = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Exit</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">For</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> isSwitching </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Debug.Print(</span><span style="color: #a31515; line-height: 140%;">&quot;isSwitching&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Debug.Print(</span><span style="color: #a31515; line-height: 140%;">&quot;flat pattern is changed&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Call</span><span style="line-height: 140%;"> printContext(Context)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<br />
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
