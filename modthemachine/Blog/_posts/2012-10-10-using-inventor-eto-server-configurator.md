---
layout: "post"
title: "Using Inventor ETO Server Configurator"
date: "2012-10-10 00:21:15"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/10/using-inventor-eto-server-configurator.html "
typepad_basename: "using-inventor-eto-server-configurator"
typepad_status: "Publish"
---

<p>One of the tools you use when setting up an ETO application to use with ETO server is the “Inventor ETO Server Configurator”. The user interface for this utility is under review by engineering. One thing that may not be obvious is that you need to hit Enter after filling out the four fields required to add the application.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c326cc438970b-pi"><img alt="image" border="0" height="299" src="/assets/image_380451.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="483" /></a></p>
<p>If you don’t Enter and click on “Test Application Services” you will probably get this error:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee4109228970d-pi"><img alt="image" border="0" height="227" src="/assets/image_669255.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="500" /></a></p>
<p>You can get more information by following the directions in the error:&#0160;</p>
<p>“turn on IncludeExceptionDetailInFaults from the &lt;serviceDebug&gt; configuration behavior on the server in order to send the exception information back to the client”</p>
<p>To do this, see line 125 in {install location}\Inventor ETO Server 2013\Bin\IntentSessionManager.exe.config.</p>
<p>From the config file:</p>
<p>&lt;!-- To receive exception details in faults for debugging purposes, set the value below to true. Set to false before deployment to avoid disclosing exception information –&gt;</p>
<p>&lt;serviceDebug includeExceptionDetailInFaults=&quot;False&quot;/&gt;</p>
<p>You will have to restart the service for the change to take effect. Either reboot the machine or restart the service from the Services control panel.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee4109240970d-pi"><img alt="image" border="0" height="333" src="/assets/image_705981.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="453" /></a></p>
<p>After making this change, the error when clicking “Test Application Services” will change to this error:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee410925c970d-pi"><img alt="image" border="0" height="197" src="/assets/image_525036.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="406" /></a></p>
<p>This error occurs because even though you have added the four required fields for the application, the configuration doesn’t actually get committed to the configuration file unless you select the row in the grid and hit the “Enter” key. You can check this config file to see if the application was committed.</p>
<p>%ProgramData%\Autodesk\Inventor ETO Server 2013 R1\IntentServices.config</p>
<p>This was a problem I recently worked on. In the end the solution was so simple – “just hit enter”!</p>
<p>Here is the link to the help topic on the Inventor ETO Server Configurator:</p>
<p><a href="http://wikihelp.autodesk.com/Inventor_ETO/enu/2013/Help/1240-Inventor1240/1253-Configur1253" title="http://wikihelp.autodesk.com/Inventor_ETO/enu/2013/Help/1240-Inventor1240/1253-Configur1253">http://wikihelp.autodesk.com/Inventor_ETO/enu/2013/Help/1240-Inventor1240/1253-Configur1253</a></p>
<p>-Wayne</p>
