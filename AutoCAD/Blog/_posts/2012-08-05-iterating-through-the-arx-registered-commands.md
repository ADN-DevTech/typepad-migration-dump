---
layout: "post"
title: "Iterating through the ARX-registered commands"
date: "2012-08-05 02:34:17"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/iterating-through-the-arx-registered-commands.html "
typepad_basename: "iterating-through-the-arx-registered-commands"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>This can achieved by using AcEdCommandIterator to iterate through all the AcEd-registered commands.</p>
<p>Here is a sample code fragment :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ACHAR cmdLocalName[133];&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ACHAR cmdGlobalName[133];&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ACHAR cmdGroupName[133];</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcEdCommandIterator *pCmdItr = acedRegCmds-&gt;iterator();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(NULL == pCmdItr)&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(;! pCmdItr-&gt;done(); pCmdItr-&gt;next())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; wcscpy(cmdGlobalName, pCmdItr-&gt;command()-&gt;globalName()); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; wcscpy(cmdLocalName, pCmdItr-&gt;command()-&gt;localName()); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; wcscpy(cmdGroupName, pCmdItr-&gt;commandGroup());&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\n&nbsp;&nbsp;&nbsp; Group name = %s, Local name = %s, Global name = %s&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cmdGroupName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cmdLocalName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cmdGlobalName</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pCmdItr; </span></p>
</div>
