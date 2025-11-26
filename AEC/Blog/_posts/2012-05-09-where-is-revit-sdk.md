---
layout: "post"
title: "Where is Revit SDK?"
date: "2012-05-09 06:33:52"
author: "Mikako Harada"
categories:
  - ".NET"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/05/where-is-revit-sdk.html "
typepad_basename: "where-is-revit-sdk"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p>The next few posts are&#0160;from my &quot;Getting Started with Revit API&quot;&#0160;document which&#0160;I have been keeping and updating since Revit 2009. As a person who is leading AEC workgroup, I get inquiries about&#0160;how to get started&#0160;with Revit API occasionally.The doc became quite handy in such an occasion.&#0160;</p>
<p>The first is:&#0160;where is Revit SDK? This is the&#0160;basic question. Yet, we still get this inquiry once in a while.&#0160;You can get the SDK from two location: from the product installation and <a href="http://www.autodesk.com/developrevit" target="_self" title="Revit Developer Center">Revit Developer Center</a>.</p>
<p>You can find Revit SDK in every Revit install. From the main install dialog while you are installing Revit, go to “Install Tools and Utilities” menu. (Note: there is another menu called “Utilities” you see right after the product installation, which contains only the link Content Batch utilities. Click on “Back to First Page” button to move back to the main page of the installer to install SDK.)</p>
<p>Alternatively, you can also find the SDK in the extraction folder. It will be&#0160;under:<br />&lt;extraction folder&gt;\support\SDK\RevitSDK.exe<br />If you have accepted the default location, which typically looks like this: <br />C:\Program Files\Autodesk\Revit XXX 201x\support\SDK\RevitSDK.exe</p>
<p>The second location&#0160;is Revit&#0160;Developer Center. The SDK may be updated.&#0160; Make sure you check the latest at either ADN site (member only) or Revit Developer Center (available to public): <br /><a href="http://www.autodesk.com/developrevit">http://www.autodesk.com/developrevit</a></p>
<p>RevitAPI.dll and Revit APIUI.dll, which are needed to build&#0160;a Revit add-in application,&#0160;are in every Revit installation under &lt;Revit install&gt;\Program\</p>
