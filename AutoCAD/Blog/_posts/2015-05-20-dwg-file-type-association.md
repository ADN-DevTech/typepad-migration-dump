---
layout: "post"
title: "DWG file type association"
date: "2015-05-20 05:27:34"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/05/dwg-file-type-association.html "
typepad_basename: "dwg-file-type-association"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is some information on .dwg file type association that you may find helpful especially if you multiple softwares that can open .dwg files installed in your system.</p>
<p>The applications that can open .dwg files in a system are listed under the following registry key :</p>
<p>HKEY_CURRENT_USER\Software\Autodesk\DwgCommon\shellex\apps</p>
<p>For example, AutoCAD could be listed under :</p>
<p>HKEY_CURRENT_USER\Software\Autodesk\DwgCommon\shellex\apps\{F29F85E0-4FF9-1068-AB91-08002B27B3D9}:AutoCAD</p>
<p>The default application that will be used for opening .dwg file is under&nbsp;</p>
<p>HKEY_CURRENT_USER\Software\Autodesk\DwgCommon\shellex\apps -&gt; (Default) key</p>
<p>AutoCAD reads the following registry key at startup to know if .dwg file type is associated with AutoCAD.</p>
<p>HKEY_CURRENT_USER\Software\Classes\.dwg</p>
<p>If this key is not found as would be expected when running AutoCAD for the very first time, AutoCAD would display the DWG Association dialog as shown in this screenshot.</p>
<p></p>
<a class="asset-img-link"   href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78d7f05970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c78d7f05970b img-responsive" alt="DwgReassociateDialog" title="DwgReassociateDialog" src="/assets/image_386067.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
<p>In case you want AutoCAD to show this dialog in any of its subsequent invocations, simply try renaming the ".dwg" registry key under "HKEY_CURRENT_USER\Software\Classes"</p>
<p>To always automatically reassociate the .dwg without prompting the user, the following registry key was required is to be set :</p>
<p>HKCU\Software\Autodesk\AutoCAD\R20.0\ACAD-E001:409\Profiles\&lt;&lt;Unnamed Profile&gt;&gt;\General</p>
<p>DwgAlwaysAssociate : 1</p>
<p>To never associate .dwg automatically and also prevent the reassociate dialog from appearing, the following&nbsp; registry key is to be set :</p>
<p>HKCU\Software\Autodesk\AutoCAD\R20.0\ACAD-E001:409\Profiles\&lt;&lt;Unnamed Profile&gt;&gt;\General</p>
<p>DwgAlwaysAssociate : 0</p>
<p>If you do not find the DwgAlwaysAssociate key in the registry, you can create it as a DWORD.</p>
<p>Please ensure that you make the registry changes with care and keep a backup of the registry keys that you are changing by first exporting them as .reg files. This is important to undo the changes if you face any problems later.&nbsp;</p>
