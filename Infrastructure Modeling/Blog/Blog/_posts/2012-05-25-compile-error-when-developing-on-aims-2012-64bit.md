---
layout: "post"
title: "Compile error when developing on AIMS 2012 64bit"
date: "2012-05-25 01:06:00"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/compile-error-when-developing-on-aims-2012-64bit.html "
typepad_basename: "compile-error-when-developing-on-aims-2012-64bit"
typepad_status: "Publish"
---

<p>I am always asked questions like below:</p>
<p><br />I am working with Autodesk Infrastructure Map Server 2012 64bit, but I always get a compile error “could not load file or assembly”, there is not problem when running.</p>
<p>The error message could be:</p>
<p>Could not load file or assembly &#39;OSGeo.MapGuide.Foundation, Version=2.3.0.4202, Culture=neutral, PublicKeyToken=null&#39; or one of its dependencies. An attempt was made to load a program with an incorrect format.</p>
<p>Could not load file or assembly &#39;MapGuideDotNetApi&#39; or one of its dependencies.</p>
<p>&#0160;</p>
<p>How could we solve this problem?</p>
<p>Firstly, we should copy *all* the files from MapGuide WebExtension folder(C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\mapviewernet\bin) to &lt;yourApp&gt;\bin folder.</p>
<p>Secondly, for AIMS2012 64bit, we can use WebApplication instead of Website when creating project in Visual Studio, and create web application in local IIS, then use “attach to process” to debug. For more information, please refer to the webcast, we have a live demonstration about how to create a AIMS2012 64bit based web application and how to debug.</p>
<p><strong>Video : Autodesk® Infrastructure Map Server 2012 API Webcast      <br /></strong>Recorded version of the Autodesk® Infrastructure Map Server 2012 API webcast     <br /><a href="http://download.autodesk.com/media/adn/AIMS2012_Webcast_English/AIMS2012_Webcast_English.html">View online</a> | <a href="http://download.autodesk.com/media/adn/AIMS2012_Webcast_English.zip">Download</a></p>
<p>&#0160;</p>
<p>Hope this helps.</p>
