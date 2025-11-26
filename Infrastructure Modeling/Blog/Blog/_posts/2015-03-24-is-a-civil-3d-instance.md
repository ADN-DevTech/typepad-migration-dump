---
layout: "post"
title: "Is a Civil 3D instance?"
date: "2015-03-24 13:31:00"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2015"
original_url: "https://adndevblog.typepad.com/infrastructure/2015/03/is-a-civil-3d-instance.html "
typepad_basename: "is-a-civil-3d-instance"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>This is a .NET version of the code posted by a colleague, <a href="http://adndevblog.typepad.com/autocad/2012/09/determine-product-of-autocad-family.html">see original here</a>, redesigned to Civil 3D naming.</p>  <p>When we get a running instance of AutoCAD, we don’t know which vertical is actually running as all of them are basically the same root object. To determine which version we have, we need to try. And that’s what this code is doing.</p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: green">' get the running instance of AutoCAD (or any vertical)</span><br /><span style="color: blue">Dim</span> acadApp <span style="color: blue">As</span>&#160;<span style="color: blue">Object</span> = GetObject(, <span style="color: #a31515">&quot;Autocad.Application&quot;</span>)<br /><span style="color: green">' is it a Civil 3D? let's try!</span><br /><span style="color: blue">Dim</span> civilLandApp <span style="color: blue">As</span>&#160;<span style="color: blue">Object</span><br /><span style="color: blue">Try</span><br />&#160; <span style="color: green">' the 10.4 stands for 2015 release</span><br />&#160; civilLandApp = acadApp.GetInterfaceObject<br />                 (<span style="color: #a31515">&quot;AeccXUiLand.AeccApplication.10.4&quot;</span>)<br /><span style="color: blue">Catch</span> ex <span style="color: blue">As</span>&#160;<span style="color: #2b91af">Exception</span><br />&#160; <span style="color: blue">Return</span>&#160;<span style="color: green">' not Civil 3D</span><br /><span style="color: blue">End</span>&#160;<span style="color: blue">Try</span><br /> <br /><span style="color: green">' now we know is Civil 3D</span><br /><span style="color: green">' do something</span><br /></pre>
And in C# it’s basically the same, except that you need to decide whenever to use <strong><em>var </em></strong>or <strong><em>dynamic.</em></strong> The above code uses <strong><em>Object</em></strong>, that enables late-binding. If that is your option, then use the C# equivalent: <a href="https://msdn.microsoft.com/library/dd264736.aspx">dynamic</a>
