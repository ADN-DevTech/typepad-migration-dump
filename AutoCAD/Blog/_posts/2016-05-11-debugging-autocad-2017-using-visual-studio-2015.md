---
layout: "post"
title: "Debugging AutoCAD 2017 using Visual Studio 2015"
date: "2016-05-11 02:31:46"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/05/debugging-autocad-2017-using-visual-studio-2015.html "
typepad_basename: "debugging-autocad-2017-using-visual-studio-2015"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>At present, developers are unable to debug the .NET modules in AutoCAD 2017 with VS 2015. As explained in Kean’s article for VS 2013 <a href="http://through-the-interface.typepad.com/through_the_interface/2013/11/debugging-autocad-using-visual-studio-2013.html">http://through-the-interface.typepad.com/through_the_interface/2013/11/debugging-autocad-using-visual-studio-2013.html</a> , the workaround for this issue is to either enable the native debugging or using the Compatibility mode in VS 2015.</p>
<ol>
<li>Turn on “Use Managed Compatibility Mode” via Tools –&gt; Options –&gt; Debugging.</li>
<li>Turn on “Enable native code debugging” from Project –&gt; Properties –&gt; Debug.</li>
</ol>
