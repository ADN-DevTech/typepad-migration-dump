---
layout: "post"
title: "Quit AutoCAD"
date: "2016-05-20 02:24:32"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/05/quit-autocad.html "
typepad_basename: "quit-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>AutoCAD.NET API exposes “Application.Quit()” to exit the AutoCAD. However, “Application.Quit()” at present is unable to save all the user profile setting (like active ribbon tab ).&#0160;One alternative approach for the issue is to use quit AutoCAD command through “SendStringToExecute” &#0160;&#0160;</p>
<pre>[CommandMethod(&quot;quitACAD&quot;, CommandFlags.Session)]
static public void quitACAD()
{
    //Quit AutoCAD 
    Application.DocumentManager.MdiActiveDocument.SendStringToExecute(&quot;quit &quot;, true, false, true);
} 
</pre>
