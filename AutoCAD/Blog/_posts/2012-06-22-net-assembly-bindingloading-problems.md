---
layout: "post"
title: ".NET Assembly Binding/Loading problems"
date: "2012-06-22 11:39:09"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/net-assembly-bindingloading-problems.html "
typepad_basename: "net-assembly-bindingloading-problems"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html" target="_blank">Augusto Goncalves</a></p>  <p>More often than not, the problem is due to a missing assembly which causes the assembly binding mechanism to fail.</p>  <p>The best way to detect issues of this nature is by running the fuslogvw.exe on the problematic machine. To use it, simply copy fuslogvw.exe from your Visual Studio install folder <em>C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin</em> onto the problematic machine and then run it. If you have an assembly binding problem then it will be listed in the fuslogvw.exe dialog.</p>  <p>For the viewer to log all the results, you need to add certain keys in the registry. Please add a DWORD key at this location</p>  <p>HKLM\Software\Microsoft\Fusion\ForceLog</p>  <p>If your machine has it, then simply change its value to something greater than 0, otherwise please create this new DWORD key and set the value of this key greater than 0. Then it will display all the binding messages.</p>  <p>For more information on the .NET SDK tool, Assembly Binding Log Viewer (Fuslogvw.exe), please refer to <a href="http://msdn.microsoft.com/en-us/library/e74a18c4.aspx" target="_blank">this document</a>.</p>
