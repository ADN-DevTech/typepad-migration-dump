---
layout: "post"
title: "Clear &ldquo;Show My Home on startup&rdquo; option"
date: "2018-04-19 05:13:46"
author: "Sajith Subramanian"
categories:
  - "Inventor"
  - "Sajith Subramanian"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/04/clear-show-my-home-on-startup-option.html "
typepad_basename: "clear-show-my-home-on-startup-option"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p>We had a query to clear the&nbsp; “Show My Home on startup”&nbsp; option under Inventor Application Options, General Tab, using the Inventor API.</p><p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0a054132970d-pi"><img width="524" height="746" title="clip_image001" style="display: inline; background-image: none;" alt="clip_image001" src="/assets/image_0432b3.jpg" border="0"></a></p><p><br></p><p>Currently, there is no API to achieve this directly. </p><p>However, since Inventor 2018, the application options are no longer in the registry.&nbsp; They are now in an XML file that’s located in the user’s roaming profile:&nbsp; <br><em>
C:\Users\&lt;&lt;USER&gt;&gt;\AppData\Roaming\Autodesk\Inventor 2019\UserApplicationOptions.xml</em><br>&nbsp; <br>
This file contains changes from the default Application option settings.&nbsp; <br>
The “Show My Home” section is part of the “&lt;General&gt;” element:&nbsp; <br>
&lt;General ShowHomeBaseOnStartup="0"/&gt;<br>&nbsp; <br>
Additionally, It will also contain other attributes, if other settings in the “General” Application Options tab differ from the default. For e.g.:<br>&nbsp;&nbsp; &lt;General MruSize="45" ShowHomeBaseOnStartup="0"/&gt;</p>
