---
layout: "post"
title: "Load and run VBA macro through .NET command"
date: "2016-07-22 01:45:15"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "ActiveX"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/load-and-run-vba-macro-through-net-command.html "
typepad_basename: "load-and-run-vba-macro-through-net-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Below code shows the procedure to load and run a VBA macro through .NET command. Here AutoCAD ActiveX API LoadDVB &amp; RunMacro is used. As the code uses dynamic keyword, to use this code no need to refer the AutoCAD ActiveX (interop) references.</p>
<pre>[CommandMethod(&quot;LoadRunVBAcOMMAND&quot;)]
public static void LoadDVBFile()
{
    dynamic acadApplication = Application.AcadApplication;
    acadApplication.LoadDVB(@&quot;C:\cases\area.dvb&quot;);
    acadApplication.RunMacro(&quot;mytest&quot;);
} 
</pre>
