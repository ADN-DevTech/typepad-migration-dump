---
layout: "post"
title: "AccoreConsole and Object enabler"
date: "2015-03-17 04:05:06"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/accoreconsole-and-object-enabler.html "
typepad_basename: "accoreconsole-and-object-enabler"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>For AccoreConsole to recognize a custom entity in your drawing, it requires to load the object enabler just as AutoCAD does. If you already know the full path of your object enabler (.dbx) you can have it configured for loading at startup by modifying the registry. Here is a sample registry file that would register the object enabler for AsdkPoly custom entity&nbsp;from the ObjectARX SDK :</p>
<p style="color:blue">Windows Registry Editor Version 5.00<br /> <br />[HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\ObjectDBX\R20.0\Applications\AsdkPolyObj2.0]<br />"DESCRIPTION"="AsdkPolyObj2.0"<br />"LOADER"="D:\\Temp\\asdkpolyobj.dbx"<br />"LOADCTRLS"=dword:00000009</p>
<p>if you do not know the full path to the dbx, you can have the "LOADER" path set as simply "asdkpolyobj.dbx". In that case, AccoreConsole would try searching for it alongside the AccoreConsole exe path, the drawing path or one of the support paths. An easy way to have the support path configured for AccoreConsole is to launch AutoCAD and add the custom path to its "support file search path" using the Options command. AccoreConsole uses the same set of search paths that is already setup in AutoCAD.</p>
<p>It is important to have the right application name of your object enabler written to the registry. A simple way to determine this is to list the entities in the drawing. AccoreConsole will display the details of the proxy object and its Application name as shown here :</p>
<a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7649b07970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7649b07970b img-responsive" alt="1" title="1" src="/assets/image_217263.jpg" style="margin: 0px 5px 5px 0px;" /></a>
</br>
<p>For some reason, if you do not want AccoreConsole to load the object enablers, start AutoCAD and set the DEMANDLOAD to 0 and close it. Further invocation of AccoreConsole will stop loading the object enablers even when the dbx is configured for loading at startup using the registry. Please do not use this unless you have a reason to do so. This may cause other issues as object enablers that AutoCAD requires for its working can also get ignored.</p>
<p></p>
