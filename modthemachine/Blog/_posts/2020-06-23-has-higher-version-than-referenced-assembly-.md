---
layout: "post"
title: "Has higher version than referenced assembly "
date: "2020-06-23 12:04:06"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/06/has-higher-version-than-referenced-assembly-.html "
typepad_basename: "has-higher-version-than-referenced-assembly-"
typepad_status: "Publish"
---

<p>Some assemblies might be relying on a specific higher version of <strong>Autodesk.Inventor.Interop.dll</strong> than the one your project is referencing. That&#39;s the problem I ran into with <strong>Autodesk.iLogic.Core.dll</strong> in case of the project talked about here: <a href="https://forge.autodesk.com/blog/get-ilogic-form-information-inventor-documents">Get iLogic Form information from Inventor documents</a></p>
<p>My project was working fine, but when I installed the latest <strong>Inventor 2020 update</strong> and tried to compile my project, I got this error:</p>
<pre>Assembly &#39;Autodesk.iLogic.Core&#39; with identity &#39;Autodesk.iLogic.Core, Version=24.30.37300.0, 
Culture=neutral, PublicKeyToken=null&#39; uses &#39;Autodesk.Inventor.Interop, Version=24.2.0.0, 
Culture=neutral, PublicKeyToken=d84147f8b4276564&#39; which has a higher version than referenced assembly 
&#39;Autodesk.Inventor.Interop&#39; with identity &#39;Autodesk.Inventor.Interop, Version=24.1.0.0, 
Culture=neutral, PublicKeyToken=d84147f8b4276564&#39;</pre>
<p>You can find the various versions of <strong>Autodesk.Inventor.Interop.dll</strong> in the &quot;<strong>C:\Windows\Microsoft.NET\assembly\GAC_MSIL\Autodesk.Inventor.Interop</strong>&quot; folder including version <strong>24.2</strong> (after installing the relevant Inventor update):</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340263ec23e4ab200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Autodesk.Inventor.Interop_2020.2" class="asset  asset-image at-xid-6a00e553fcbfc688340263ec23e4ab200c img-responsive" src="/assets/image_62199.jpg" title="Autodesk.Inventor.Interop_2020.2" /></a></p>
<p>Once I added that reference, the project compiled fine. 😀&#0160;</p>
<p>-Adam</p>
