---
layout: "post"
title: "Open a particular sheet in a DWF file using Design Review API"
date: "2013-07-10 22:30:00"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "ADR API"
  - "DWF"
  - "JavaScript"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/open-a-particular-sheet-in-a-dwf-file-using-design-review-api.html "
typepad_basename: "open-a-particular-sheet-in-a-dwf-file-using-design-review-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you want
to open a particular sheet in a DWF file which has many sheets in it (e.g. -
Model, A1, A2, A3 etc) and you want to show only the A2 sheet when the DWF file
is loaded using the Design Review API, in that situation we can use the
<strong>AdCommon.IAdCollection</strong> Sections collection to get and set the named section as demonstrated in the code snippet below :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;"> compvwr = (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdECompositeViewer</span><span style="line-height: 140%;">)axCExpressViewerControl1.ECompositeViewer;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;"> Sections = (AdCommon.</span><span style="color: #2b91af; line-height: 140%;">IAdCollection</span><span style="line-height: 140%;">)compvwr.Sections;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Loop through the section collections and set the current</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (ECompositeViewer.</span><span style="color: #2b91af; line-height: 140%;">IAdSection</span><span style="line-height: 140%;"> sec </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> Sections)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//Set section</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (sec.Title.ToString() == </span><span style="color: #a31515; line-height: 140%;">&quot;A2&quot;</span><span style="line-height: 140%;">) </span><span style="color: green; line-height: 140%;">// Get the specific section of the DWF file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; compvwr.Section = sec;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//MessageBox.Show(sec.Title);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
