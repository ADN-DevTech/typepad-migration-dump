---
layout: "post"
title: "Error netloading plugin from network location"
date: "2012-09-01 20:53:36"
author: "Balaji"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/error-netloading-plugin-from-network-location.html "
typepad_basename: "error-netloading-plugin-from-network-location"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>Iam trying to netload a plugin from a network location. But it gives me an error indicating "Please enable the loadFromRemoteSources" switch. How do I resolve this ?</p>
<div><strong>Solution</strong></div>
<p>This message is due to a change related to administering CAS policy in the .Net Framework 4.0 as compared to .Net Framework 3.5.</p>
<p>More information on this is available in the following links :</p>
<p><a href="http://blogs.msdn.com/b/shawnfa/archive/2009/06/08/more-implicit-uses-of-cas-policy-loadfromremotesources.aspx">http://blogs.msdn.com/b/shawnfa/archive/2009/06/08/more-implicit-uses-of-cas-policy-loadfromremotesources.aspx</a></p>
<p><a href="http://msdn.microsoft.com/en-us/library/dd409252(VS.100).aspx">http://msdn.microsoft.com/en-us/library/dd409252(VS.100).aspx</a></p>
<p>To fix this problem, you can modify the "acad.exe.config" by inserting the following line in it. </p>
<p>The "acad.exe.config" can be found in the AutoCAD installation folder.</p>
<p>&lt;runtime&gt; </p>
<p>　　　 &lt;loadFromRemoteSources enabled="true"/&gt; </p>
<p>&lt;/runtime&gt;</p>
<p>The &lt;loadFromRemoteSources&gt; element lets you specify that the assemblies that run partially trusted in earlier versions of the .NET Framework should be run fully trusted in the .NET Framework 4.</p>
