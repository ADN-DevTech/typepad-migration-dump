---
layout: "post"
title: "Multiline text in ribbon button"
date: "2012-05-07 19:49:41"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/multiline-text-in-ribbon-button.html "
typepad_basename: "multiline-text-in-ribbon-button"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<ul>
<li>To create a ribbon button with multi-line text using the CUI editor :</li>
</ul>
<p style="margin: 0in 0in 0pt 0.5in;"></p>
<p style="margin: 0in 0in 0pt 0.5in;">Open the CUI editor and select the ribbon item</p>
<p style="margin: 0in 0in 0pt 0.5in;">In the “Name” field, enter the words separated by “\r”. For ex : Insert\rBlock</p>
<p style="margin: 0in 0in 0pt 0.5in;">&nbsp;</p>
<p style="margin: 0in 0in 0pt 0.5in;">Please note that control characters such as “\r” do not appear in the edit box after it is saved, but the ribbon item will display the multi-line correctly.</p>
<p style="margin: 0in 0in 0pt 0.5in;"></p>
<ul>
<li>To create a multi-line ribbon button using code, here is a sample code snippet :</li>
</ul>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Autodesk.Windows.</span><span style="color: #2b91af; line-height: 140%;">RibbonButton</span><span style="line-height: 140%;"> ribButton = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">RibbonButton</span><span style="line-height: 140%;">(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ribButton.Text = </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;My{0}Polyline&quot;</span><span style="line-height: 140%;">, </span><span style="color: #2b91af; line-height: 140%;">Environment</span><span style="line-height: 140%;">.NewLine); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ribButton.ShowText = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ribButton.Size = Autodesk.Windows.</span><span style="color: #2b91af; line-height: 140%;">RibbonItemSize</span><span style="line-height: 140%;">.Large; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ribButton.CommandParameter = </span><span style="color: #a31515; line-height: 140%;">&quot;\x1b\x1b_PLINE &quot;</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ribButton.CommandHandler = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AdskCommandHandler();</span></p>
</div>
