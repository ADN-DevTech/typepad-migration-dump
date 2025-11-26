---
layout: "post"
title: "Can I sign my AutoCAD .NET plug-in with a strong name?"
date: "2012-07-12 10:41:09"
author: "Marat Mirgaleev"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Marat Mirgaleev"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/can-i-sign-my-autocad-net-plug-in-with-a-strong-name.html "
typepad_basename: "can-i-sign-my-autocad-net-plug-in-with-a-strong-name"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>I am trying to create a strong name for my assembly, but it raises the following error message: &quot;Referenced assembly 'acmgd' does not have a strong name&quot;. Is there any way to solve this?</em></p>  <p><em></em></p>  <p><strong>Solution</strong></p>  <p>Unfortunately, we do not provide strong named versions of AutoCAD .NET assemblies. There are some reasons for this:</p>  <p>- A strongly named assembly adheres to a stricter versioning policy: patching the dll is trickier;</p>  <p>- A strongly named assembly must be installed into the GAC to avoid the performance hit related to extra validation. The GAC itself further complicates deployment.</p>  <p>By the way, we signed our dlls in AutoCAD 2005 and we got rid of the signatures because of the complications.</p>  <p>Thus, your assembly cannot have a strong name if it depends on AutoCAD assemblies.</p>  <p>However there are workarounds available:</p>  <p>- Instead of setting security policy for file you can set it for entire folder:    <br />&#160;&#160; CasPol.exe -m -ag 1.2 -url &quot;\\YourNetworkDrive\Path\*&quot; FullTrust -name MYNAME</p>  <p>or</p>  <p>- Sign your assembly with Authenticode signature and configure to trust the 'publisher'.</p>
