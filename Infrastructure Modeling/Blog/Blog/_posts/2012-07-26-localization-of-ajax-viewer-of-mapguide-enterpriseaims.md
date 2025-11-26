---
layout: "post"
title: "Localization of Ajax viewer of MapGuide Enterprise/AIMS"
date: "2012-07-26 00:18:29"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/localization-of-ajax-viewer-of-mapguide-enterpriseaims.html "
typepad_basename: "localization-of-ajax-viewer-of-mapguide-enterpriseaims"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>Many developers are trying to localize viewer of MapGuide or AIMS to make it more user-friendly. Here are the basic steps to localize MapGuide Enterprise, This blog is based on MapGuide Enterprise 2011, it applies to AIMS as well with minor changes.</p>  <p>1. Prepare the resource file, which locates at C:\Program Files\Autodesk\MapGuideEnterprise2011\WebServerExtensions\www\localized by default.</p>  <p>2. Copy the file en to a new one named as zh for Chinese, fr for France, for example. Put it into the same directory. Please be note that this is NO extension for this file.</p>  <p>3. Open the copied resource file and translate the message into local language.</p>  <p>4. Save the file, please save it using Unicode if needed, especially for East Asian language.</p>  <p>5. Pass the locale parameter to the Ajax viewer.</p>  <p>&lt;frameset rows=&quot;0,*&quot; border=&quot;0&quot; framespacing=&quot;0&quot;&gt;    <br />&lt;frame /&gt;     <br />&lt;frame src=&quot;/mapguide2011/mapviewernet/ajaxviewer.aspx?SESSION=&lt;%= sessionId %&gt;&amp;WEBLAYOUT=&lt;%= webLayout %<b>&gt;&amp;Locale=zh</b>&quot; name=&quot;ViewerFrame&quot; /&gt;     <br />&lt;/frameset&gt;</p>  <p>6. For these build-in commands, it can be localized by substitute the built-in commands by “Add –&gt; Built-In Command” and localizing the display name and tooltip in MapGuide Studio.</p>  <p>Add Build-in Commands in MapGuide Studio</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177439f87cf970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_219219.jpg" width="444" height="319" /></a></p>  <p>Localize display name and tooltip</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616b93184970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_e0763b.jpg" width="438" height="457" /></a></p>  <p>That's it, now your interface is localized!</p>
