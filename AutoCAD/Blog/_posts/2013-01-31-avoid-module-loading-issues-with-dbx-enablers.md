---
layout: "post"
title: "avoid module loading issues with DBX enablers"
date: "2013-01-31 01:30:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/avoid-module-loading-issues-with-dbx-enablers.html "
typepad_basename: "avoid-module-loading-issues-with-dbx-enablers"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>I have an ObjectARX User Interface (.ARX) module that is dependent on an ObjectDBX Object Enabler (.DBX) module. I link the .ARX module with the import library (.LIB) for the .DBX, but even though the .DBX demand-loads correctly on proxy detection, the .ARX will not demand-load on command invocation and the following error occurs:</p>
<p>&quot;MyApplication.arx cannot find a DLL or other file that it needs.&quot;</p>
<p>This works correctly if I add the location of the .DBX to the Windows system path or install it into C:\Program Files\Common Files\Autodesk Shared, but I don&#39;t want to do this.</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>There is a mechanism available in Visual C++ called &quot;delay loading&quot;. This mechanism allows you to link to a library via the standard DLL linking mechanism, but delays its initialization until required.</p>
<p>To specify that your .ARX delay-loads your .DBX, edit your .ARX project&#39;s linker settings as follows:</p>
<p>1. Add the delayimp.lib library file to the &quot;Object/library modules&quot; list.   <br />2. Manually specify the setting, /delayload:&quot;MyDbxModule.dbx, in the Project Options text box.</p>
<p>You can demand-load both the .ARX module and the .DBX without being constrained by the Windows system path. You will need to programmatically loadthe DBX application from the ARX application for this technique to work, therefore, use either acrxLoadMoudle() or acexDynamicLinker-&gt;loadModule()..</p>
<p>For more information, please refer to MSDN such as :</p>
<p><a href="http://msdn.microsoft.com/en-us/library/hf3f62bz.aspx">Specifying DLLs to Delay Load</a></p>
<p><a href="http://msdn.microsoft.com/en-us/library/yx9zd12s(v=vs.80).aspx">/DELAYLOAD</a></p>
