---
layout: "post"
title: "Signing the WebServices DLL"
date: "2010-10-11 10:32:48"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/10/signing-the-webservices-dll.html "
typepad_basename: "signing-the-webservices-dll"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p><strong>Update:</strong>&#0160; These steps only work for the Vault 2011 version of the DLL.&#0160; <a href="http://justonesandzeros.typepad.com/blog/2011/08/strong-named-dlls.html" target="_self">Go here if you are using Vault 2012</a>.</p>
<p>A couple of people have had issues with the fact that Autodesk.Connectivity.WebServices.dll is unsigned.&#0160; This is a problem if you want to sign your project.&#0160; Visual Studio won&#39;t let you sign something if it references an unsigned DLL.&#0160;</p>
<p>Fortunately, there is a trick you can use to sign the WebServices DLL.&#0160; The trick involves SN.exe, ILASM and ILDASM.exe, which are .NET tools provided by Microsoft. You basically de-compile the DLL to IL code, then you re-compile with a key.</p>
<p>Example BAT file:</p>
<table border="1" cellpadding="2" cellspacing="0" width="450">
<tbody>
<tr>
<td valign="top" width="450">
<p><span style="font-size: xx-small;">set assembly=Autodesk.Connectivity.WebServices              <br />set WinSDK=C:\Program Files\Microsoft SDKs\Windows\v6.1\Bin               <br />set NetDir=C:\Windows\Microsoft.NET\Framework\v2.0.50727</span></p>
<p><span style="font-size: xx-small;">&quot;%WinSDK%\sn.exe&quot; -k keyfile.snk              <br />&quot;%WinSDK%\ILDASM.exe&quot; %assembly%.dll /out=%assembly%.il               <br />move %assembly%.dll %assembly%.1.dll               <br />&quot;%NetDir%\ILASM.exe&quot; %assembly%.il /dll /out=%assembly%.dll /key=keyfile.snk</span></p>
</td>
</tr>
</tbody>
</table>
<p>If all goes well, you will end up with a signed WebServices DLL.&#0160; The original DLL is still there, but it&#39;s been renamed with a &quot;.1.dll&quot; extension.</p>
<p>This trick will work with any .NET assembly, so don&#39;t let unsigned references prevent you from signing your project.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
