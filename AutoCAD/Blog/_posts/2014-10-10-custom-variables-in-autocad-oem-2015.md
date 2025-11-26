---
layout: "post"
title: "Custom variables in AutoCAD OEM 2015"
date: "2014-10-10 00:00:58"
author: "Balaji"
categories:
  - "2015"
  - "AutoCAD OEM"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/10/custom-variables-in-autocad-oem-2015.html "
typepad_basename: "custom-variables-in-autocad-oem-2015"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>One of the enhancements in AutoCAD OEM 2015 is the support for custom system variables.&#0160;</p>
<p>To create a custom system variable, the following registry key is to be created. Here is a sample reg file for adding a custom system variable named &quot;LevelOfDetail&quot; for a OEM product that I named GOCAD.</p>
<p>Windows Registry Editor Version 5.00</p>
<p>[HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\GOCAD\R20\GOCAD-E001:409\Variables\LevelOfDetail]<br />&quot;StorageType&quot;=dword:00000002<br />&quot;LowerBound&quot;=dword:00000000<br />&quot;UpperBound&quot;=dword:00000064<br />&quot;PrimaryType&quot;=dword:0000138b<br />@=&quot;3&quot;</p>
<p>For the custom variable to be accessible in your OEM product, add the name of the custom variable in &quot;Your Module Settings&quot; page with a &quot;##&quot; prefix. Here is a screenshot from the OEM MakeWizard :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6f0c48f970b-pi" style="float: left;"><img alt="CustomSysVar" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6f0c48f970b img-responsive" src="/assets/image_989636.jpg" style="margin: 0px 5px 5px 0px;" title="CustomSysVar" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Build the OEM product and before running the exe, remember to run the .reg file to update the registry. Note that if you rebuild your product and testing it using the MakeWizard, the registry keys will need to be created again by running the .reg file.</p>
<p>The custom system variable can be accessed using acedSetVar / acedGetVar in your ObjectARX code or using the&#0160;Application.SetSystemVariable / Application.GetSystemVariable&#0160;from your .Net code. Users of your OEM product can change the variable using the &quot;SetVar&quot; command.</p>
