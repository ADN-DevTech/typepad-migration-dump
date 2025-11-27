---
layout: "post"
title: "My first attempt with Cloud and Mobile programming"
date: "2012-09-28 01:31:58"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/09/my-first-attempt-with-cloud-and-mobile-programming.html "
typepad_basename: "my-first-attempt-with-cloud-and-mobile-programming"
typepad_status: "Publish"
---

<p>The DevTech team here at Autodesk is working on becoming experts with cloud and mobile technologies. I am happy to say that recently I was able to spend some time working towards this goal. (For many years we have been mostly supporting desktop applications).&#0160; As a training exercise I got an <a href="http://aws.amazon.com/console/" target="_blank">account on Amazon</a>&#0160; and set up a Windows 2008 server with IIS installed. (Other DevTech engineers have used Azure).</p>
<p>The idea was to get ETO Server running on this Amazon server and to access it using a WCF Service from a Windows phone application. Along the way I would publish the <a href="http://etosamples.autodesk.com/" target="_blank">ETO Web Samples</a> and make it a web site on my cloud server. (I also used the ETO Staircase application with the WCF service).</p>
<p>This screenshot shows the different parts of this experiment:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee3d2ca2f970d-pi"><img alt="clip_image002" border="0" height="378" src="/assets/image_982301.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image002" width="583" /></a></p>
<p>I am happy to say that it is all working now. Here are some of the training resources I used:</p>
<p><a href="http://www.iis.net/learn">http://www.iis.net/learn</a></p>
<p><a href="http://msdn.microsoft.com/en-us/library/ms735119(v=vs.90).aspx">http://msdn.microsoft.com/en-us/library/ms735119(v=vs.90).aspx</a></p>
<p><a href="http://channel9.msdn.com/Series/Windows-Phone-7-Development-for-Absolute-Beginners">http://channel9.msdn.com/Series/Windows-Phone-7-Development-for-Absolute-Beginners</a></p>
<p><a href="http://www.asp.net/mvc">http://www.asp.net/mvc</a></p>
<p>I got the <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=18361292" target="_blank">ETO Web sample source</a> from Autodesk com, built and published it. To get the published files to the amazon server I used Dropbox. After I moved the folder to my Dropbox folder on my local machine it appeared on the server. (of course I needed to install Dropbox on the server).</p>
<p>A couple of other things I needed to do on the server was to use IIS to enable the Web site and the Web service. Accessing the server was easy using Remote Desktop. Using IIS was straight forward as well after I completed a tutorial on it. I also needed to use the <a href="http://wikihelp.autodesk.com/Inventor_ETO/enu/2013/Help/1240-Inventor1240/1253-Configur1253" target="_blank">Intent Services Configurator</a> to make the ETO applications&#0160; on that cloud system available to ETO server.</p>
<p>I used the WCF Application wizard in Visual Studio to create my project that would be the Service that I could use from a Windows phone application. I spent some time learning the basics of WCF and then added a function that connects to the ETO server using the ETO Server API. Here is the code:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">string</span> GetNumberOfSteps</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: blue;">double</span> newFlToFlrHt, <span style="color: blue;">double</span> newTreadRise)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">IntentServicesClient</span> client = <span style="color: blue;">new</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">IntentServicesClient</span>(<span style="color: blue;">new</span>&#0160; <span style="color: #2b91af;">Uri</span></p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;net.pipe://localhost/Autodesk/Intent/Services&quot;</span>));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; client.SetApplication(<span style="color: #a31515;">&quot;stairCase&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">string</span> prtRefChain = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//Change the distance setting between </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//the floors</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; client.SetValue(prtRefChain,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;FloorToFloorHeight&quot;</span>, newFlToFlrHt);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// set the new value for new tread rise</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; client.SetValue</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (prtRefChain, <span style="color: #a31515;">&quot;TreadRise&quot;</span>, newTreadRise);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">string</span> sTreadCountReport =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: blue;">string</span>)client.GetValue</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (prtRefChain, <span style="color: #a31515;">&quot;TreadCountReport&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> sTreadCountReport;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> (<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(ex.ToString());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> ex.ToString();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>After building and publishing the WCF Application I copied the published files to the Amazon server. I was able to add a reference to my service in a Windows phone application. When I tested it in the Win phone emulator I am able to get back the number of stairs that would be created for the new floor to floor height and the new tread rise. Here is a screen shot of Inventor and ETO to help explain what the code in the WCF service is doing:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d3c5d6419970c-pi"><img alt="image" border="0" height="400" src="/assets/image_499063.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="440" /></a></p>
<p>Below you can see the rule that is used to get this string. This is in Inventor with ETO. The WCF service is accessing ETO server (does not have a UI) to get this string:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c322f333c970b-pi"><img alt="image" border="0" height="236" src="/assets/image_309340.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="450" /></a></p>
<p>Here it is in the Windows Phone emulator:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c322f335e970b-pi"><img alt="image" border="0" height="617" src="/assets/image_576909.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="377" /></a></p>
<p>-Wayne</p>
