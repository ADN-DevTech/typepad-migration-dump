---
layout: "post"
title: "Install Autodesk Infrastructure Map Server 2013 step by step"
date: "2012-04-01 23:40:06"
author: "Daniel Du"
categories:
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/install-autodesk-infrastructure-map-server-2013-step-by-step.html "
typepad_basename: "install-autodesk-infrastructure-map-server-2013-step-by-step"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html" target="_self">Daniel Du</a></p>
<p>Autodesk Infrastructure Map Server 2013 has been released, it can be downloaded from ADN website if you are an ADN member. Trail version will also be published to Autodesk.com website very soon.</p>
<p>In this article, I would like to share with you how to install AIMS 2013 on windows machine.&#0160;</p>
<h2>Before Installation</h2>
<p>Before you start, to avoid unexpected errors, please do read the System Requirement to ensure your computer meets the minimum system requirements of AIMS. Documentation is accessible from links at the lower left corner of the installer.</p>
<p>Core Server &amp; Web Extension &amp; Infrastructure Application Extensions need Microsoft<sup>®</sup> Windows<sup>®</sup> Server 2008 Enterprise SP2. It is not officially supported to install AIMS2013 on Windows 7, but it does run without problem.Since many developers are developing AIMS application from a Win7 machine, I will use Windows 7 as demonstration here. To install on windows with IIS+.NET binding, please make sure IIS is installed/configured correctly, make sure you can run common ASP.NET application first.</p>
<p>Autodesk Infrastructure Map Server Web Extension uses CGI to communicate with core Server, please make sure check “CGI” in IIS configuration. For Windows 7, click Control Panel—&gt; Program and Features –&gt; Turn Windows features on or off:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676480f052970b-pi"><img alt="image" border="0" height="334" src="/assets/image_0662df.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="381" /></a></p>
<p>Another reminder is to ensure that your user name has Administrator permissions to install AIMS 2013.</p>
<p>&#0160;</p>
<h2>Start Installation</h2>
<p>Now let’s begin, you will see this screen after the setup is launched, please do take some time reading the Installation Help, System Requirement and Readme. Click “Install” to continue.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9822ebd970c-pi"><img alt="image" border="0" height="363" src="/assets/image_b72a04.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="441" /></a></p>
<p>Read and accept the license agreement:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163038c1702970d-pi"><img alt="image" border="0" height="369" src="/assets/image_7ee60a.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="448" /></a></p>
<p>Input your serial number and product key if you have one, otherwise you can select the first option to try this product for 30 days.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163038c171f970d-pi"><img alt="image" border="0" height="369" src="/assets/image_9810de.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="448" /></a></p>
<p><strong>Caution</strong>! Do not go too fast in this page. You need to click each item to open the configuration page, otherwise it will install with default configuration.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9822f42970c-pi"><img alt="image" border="0" height="377" src="/assets/image_975f84.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="459" /></a></p>
<p>Click “Autodesk Infrastructure Map Server 2013” to open the server configuration page as below. You can install Map Server and Web Extension on same machine or separate machines.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163038c177d970d-pi"><img alt="image" border="0" height="383" src="/assets/image_c2ff2e.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="466" /></a></p>
<p>For “Autodesk Infrastructure Web Server Extension 2013”, it is a little complicated. As you know, Autodesk Infrastructure Map Server runs on both Windows and Linux, provides APIs in 3 languages: .net, php and Java. Depending on the choices you made to develop AIMS applications, you have different choice to install Infrastructure Map Server Web Extension.</p>
<p>You should decide which language you are planning to use before installing AIMS Web Extension.</p>
<p>Language&#0160;&#0160;&#0160; OS&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Web Server</p>
<p>-----------------------------------------------------------------------------</p>
<p>.net&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Windows&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IIS Configuration with .net</p>
<p>php&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Windows&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IIS Configuration with php</p>
<p>php&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Linux&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Apache with php</p>
<p>Java&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Windows&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Apache/Tomcat with Java</p>
<p>Java&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Linux&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Apache/Tomcat with Java</p>
<p>------------------------------------------------------------------------------</p>
<p>I am going to use .NET to develop AIMS application, so I select IIS configuration with .NET.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163038c1799970d-pi"><img alt="image" border="0" height="359" src="/assets/image_2d80fd.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="437" /></a></p>
<p>&#0160;</p>
<p>I need to install “Autodesk Infrastructure Studio 2013” in next page. You can also install “Autodesk Infrastructure Application Extension 2013” and “Autodesk Infrastructure Administrator 2013” if you are going to use industry model.</p>
<p>Please note that IIS with .NET configuration is the only option if you want to install “Autodesk Infrastructure Application Extension 2013” .</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9822fa8970c-pi"><img alt="image" border="0" height="372" src="/assets/image_e347fb.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="452" /></a></p>
<p>Now it is all set, you can sit back to have a cup of coffee now.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163038c1803970d-pi"><img alt="image" border="0" height="381" src="/assets/image_2bb47a.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="463" /></a></p>
<p>A few minutes latter, the product will be installed successfully.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676480f1ea970b-pi"><img alt="image" border="0" height="389" src="/assets/image_8cf272.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="473" /></a></p>
<p>&#0160;</p>
<h2>License Activation</h2>
<p>Unlike other Autodesk products, Autodesk Infrastructure Map Server 2013 does not start the activation process when you launch the program for the first time. You must manually activate Infrastructure Map Server by clicking Start <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163038c1863970d-pi"><img alt="ac.menuaro" border="0" height="11" src="/assets/image_54f251.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="ac.menuaro" width="17" /></a> Programs (or All Programs) <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9823033970c-pi"><img alt="ac.menuaro[1]" border="0" height="11" src="/assets/image_e04495.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="ac.menuaro[1]" width="17" /></a> Autodesk <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9823038970c-pi"><img alt="ac.menuaro[2]" border="0" height="11" src="/assets/image_369aa6.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="ac.menuaro[2]" width="17" /></a> Autodesk Infrastructure Map Server 2013 <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676480f221970b-pi"><img alt="ac.menuaro[3]" border="0" height="11" src="/assets/image_4cd3eb.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="ac.menuaro[3]" width="17" /></a> Autodesk Infrastructure Map Server 2013 Licensing Activator.</p>
<p><a name="GUID-5B0CE3B1-1733-4C55-BD0B-FCBDF39957C0"></a>The Autodesk Licensing wizard is started where you can follow the on-screen instructions to activate your Autodesk Infrastructure Map Server 2013 license.</p>
<p>&#0160;</p>
<h2>Verifying the Configuration</h2>
<p>You can run some simple tests to ensure that the Map Agent and Web Server Extensions are configured properly.</p>
<h4>Testing the Map Agent</h4>
<p>Ensure that the Infrastructure Map Server is running. The installer configures Infrastructure Map Server as a service. Open Services Management Console by typing “Services.msc” in Start—&gt;Run, check that the service status is Started.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9823050970c-pi"><img alt="image" border="0" height="324" src="/assets/image_90817f.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="487" /></a></p>
<p>&#0160;</p>
<p><a name="WS73099CC142F487551259E26112442A7ABB521"></a>Open a web browser and go to the following URL. If the Web Server Extensions are not running on your local machine, replace localhost with the name or IP address of your server.</p>
<p><a name="GUID-6DA14D45-775E-4AAF-B10F-9891DEB26C43"></a></p>
<p><a name="WS73099CC142F4875535A241551166AC8792F147D"></a><a href="file:///C:/Users/duda/AppData/Local/Temp/_AIEB8C.tmp/en-us/Help/files/">http://localhost:8008/mapserver2013/mapagent/index.html </a>(Apache) or <a href="file:///C:/Users/duda/AppData/Local/Temp/_AIEB8C.tmp/en-us/Help/files/">http://localhost/mapserver2013/mapagent/index.html</a>(IIS)</p>
<p><a name="WS73099CC142F487551259E26112442A7ABB523"></a>Note that if your web server is listening on a different port you must include the port number in the HTTP request. For example, if Apache is listening on port 8700, you must go to the following URL:</p>
<p><a name="GUID-2C593634-1E70-4BCC-A92C-98D5B0BF5D2E"></a></p>
<p><a name="WS73099CC142F4875535A241551166AC8792F147E"></a><a href="file:///C:/Users/duda/AppData/Local/Temp/_AIEB8C.tmp/en-us/Help/files/">http://localhost:8700/mapserver2013/mapagent/index.html</a></p>
<p><a name="WS73099CC142F487551259E26112442A7ABB525"></a>Click one test method, for example, “Features” in Service API, then “GetFeatureProviders”, it will get all register FDO providers. Enter Administrator for the user id and admin for the password. Both are case-sensitive.</p>
<p><a name="WS73099CC142F487551259E26112442A7ABB526"></a>If the Map Agent is running properly, you will get an XML document describing the resources in the repository.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676480f276970b-pi"><img alt="image" border="0" height="323" src="/assets/image_93a821.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="525" /></a></p>
<p>&#0160;</p>
<h4>Testing the Site Administrator</h4>
<p>Ensure that the Map Server is running. Open a web browser and go to the following URL.</p>
<p><a name="GUID-EA57710D-ABFF-4729-A524-A031730AFC9E"></a></p>
<p><a name="WS73099CC142F4875535A241551166AC8792F147F"></a><a href="file:///C:/Users/duda/AppData/Local/Temp/_AIEB8C.tmp/en-us/Help/files/">http://localhost:8008/mapserver2013/mapadmin/login.php</a> (Apache) or <a href="file:///C:/Users/duda/AppData/Local/Temp/_AIEB8C.tmp/en-us/Help/files/">http://localhost/mapserver2013/mapadmin/login.php</a>(IIS)</p>
<p><a name="WS73099CC142F487551259E26112442A7ABB529"></a>Note that if your web server is listening on a different port you must include the port number in the HTTP request. For example, if Apache is listening on port 8700, you must go to the following URL:</p>
<p><a name="GUID-DF56BDB9-5F81-4783-8EB7-B0E7724DF060"></a></p>
<p><a name="WS73099CC142F487551259E26112442A7ABB52A"></a><a href="file:///C:/Users/duda/AppData/Local/Temp/_AIEB8C.tmp/en-us/Help/files/">http://localhost:8700/mapserver2013/mapadmin/login.php</a></p>
<p><a name="WS73099CC142F487551259E26112442A7ABB52B"></a>Enter Administrator for the user id and admin for the password. Both are case-sensitive.</p>
<p><a name="WS73099CC142F487551259E26112442A7ABB52C"></a>If the Site Administrator is configured properly, you will get a Manage Servers page which lists all the servers configured in the Map Server Site.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9823077970c-pi"><img alt="image" border="0" height="359" src="/assets/image_b030d9.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="452" /></a></p>
<p>&#0160;</p>
<h2>Common Errors/Problems:</h2>
<p>Here are some solutions for common problems for your reference:</p>
<h4>1. cannot login from Infrastructure Studio,</h4>
<p>Error message:</p>
<p>---------------------------</p>
<p>Unable to complete request to Site</p>
<p>The User ID or Password is invalid. Please try again.</p>
<p>---------------------------</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163038c18ca970d-pi"><img alt="image" border="0" height="136" src="/assets/image_5aa3b2.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="289" /></a></p>
<p>&#0160;</p>
<p>Solution:</p>
<blockquote>
<p><strong>Enable </strong>“<strong>Anonymous Authentication</strong>” and<strong> Disable</strong> “<strong>Basic Authentication</strong>” in IIS for MapServer2013 web application.</p>
</blockquote>
<blockquote>
<p>Start—&gt;Run, type “inetmgr” to open Internet Information Service Manager. Click “MapServer 2013” web application from let, then Authentication from right.</p>
</blockquote>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676480f2dc970b-pi"><img alt="image" border="0" height="424" src="/assets/image_f1a031.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="496" /></a></p>
<p>&#0160;</p>
<p>2. Get following error message in Infrastructure Studio:</p>
<p>---------------------------    <br />Unable to complete request to Site     <br />FastCGI must be properly configured before attempting to connect to Map Server.     <br />---------------------------</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e98230c7970c-pi"><img alt="image" border="0" height="165" src="/assets/image_dd6614.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="358" /></a></p>
<p>&#0160;</p>
<p>Solution:</p>
<ul>
<li>Check whether your configure CGI successfully in IIS(see Before Starting) </li>
<li>Disable “ASP.NET Impersonation” </li>
</ul>
<p>&#0160;</p>
<p>3. Cannot login from mapagent, <a href="http://localhost/mapserver2013/mapagent/index.html">http://localhost/mapserver2013/mapagent/index.html</a>, always get this authentication dialogue even input the correct user name “Administrator” and password “admin”:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676480f31f970b-pi"><img alt="image" border="0" height="166" src="/assets/image_c5da67.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="292" /></a></p>
<p>&#0160;</p>
<p>Solution:</p>
<blockquote>
<p><strong>Disable “Digest Authentication ” </strong>in&#0160; IIS for MapServer2013 web application.</p>
</blockquote>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e98230f0970c-pi"><img alt="image" border="0" height="267" src="/assets/image_e403a5.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="485" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
