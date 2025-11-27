---
layout: "post"
title: "Several tips for ETO"
date: "2013-05-31 22:59:34"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/05/several-tips-for-eto.html "
typepad_basename: "several-tips-for-eto"
typepad_status: "Publish"
---

<p><strong>DirectoryNotFoundException using ETO Server R2</strong></p>
<p>1. If you get a System.IO.DirectoryNotFoundException when using&#0160; ETO Server 2013 R2. Check to see if this directory exists: </p>
<p><em>C:\Users\InventorETOServices\Documents\Inventor Server x64 Inventor ETO Server      <br /></em>&#0160;&#0160;&#0160; <br /><strong>Note:</strong> If the InventorETOServices user has been deleted and recreated, there may be multiple user directories for it, such as:     <br />&#0160; <br /><em>C:\users\InventorETOServices.&lt;MachineName&gt;.000\Doc​uments\Inventor Server x64 Inventor ETO Server</em>     <br />&#0160; <br /><em>C:\users\InventorETOServices.&lt;MachineName&gt;.001\Doc​uments\Inventor Server x64 Inventor ETO Server      <br /></em>&#0160;&#0160; <br />In this case, add the directory in the newest one. This issue has been reported to Autodesk Engineering.     </p>
<p><strong>After installing ETO 2013 Server R2 - cannot establish a client connection.</strong> </p>
<p>The start of the error message is:    <br /><em>”The message with Action &#39;</em><a href="http://www.autodesk.com/IntentServices/2012/02/IIntentServices/SetInteger&#39;"><em>http://www.autodesk.com/IntentServices/2012/02/IIntentServices/SetInteger&#39;</em></a><em> cannot be processed at the receiver, due to a ContractFilter mismatch at the EndpointDispatcher”.</em>     </p>
<p>This happens when there is a mismatch between the contract used by the client and the contract used by the server. In this case the server is R2 but the client is using the R1 contracts (&#39;<a href="http://www.autodesk.com/IntentServices/2012/02/IIntentServices&#39;)">http://www.autodesk.com/IntentServices/2012/02/IIntentServices&#39;)</a>. </p>
<p>This can happen when the client project is being built with ETO Series 2013 R1 but trying to target ETO Server 2013 R2. Generally the version of Series used when building the client project should match the version of Server targeted. The solution is to update the reference to Autodesk.Intent.Services.dll in the client project so it is the same version the server is using. This can be done by installing ETO Series 2013 R2 if it’s not already installed, removing the reference, and re-adding the reference by browsing to the “&lt;Series 2013 R2 install&gt;\Bin\Public Assemblies” directory.    <br />An alternative, in this case, since ETO Server is on the same machine as the client is to update the reference to the Autodesk.Intent.Services.dll to point to the one in the ETO Server Bin directory.     </p>
<p><strong>Quickly narrow down the location of a crash.</strong> </p>
<p>In one case a crash was occurring in a custom dll. It looked like it was an ETO specific problem. By using IntentTracer to determine the Part/Design and “%%iv_message()” we were able to narrow down the location of the crash which was occurring somewhere in the custom dll. The help toipic for IntentTracer is here:</p>
<p><a href="http://wikihelp.autodesk.com/Inventor_ETO/enu/2013/Help/0001-Inventor1/0160-Inventor160/0177-Tools_fo177/0182-Intent_T182/0183-Intent_T183">http://wikihelp.autodesk.com/Inventor_ETO/enu/2013/Help/0001-Inventor1/0160-Inventor160/0177-Tools_fo177/0182-Intent_T182/0183-Intent_T183</a></p>
<p>After finding the design you can use a message box style technique to further narrow down the place of a crash.</p>
<p>%%iv_message(&quot;Tracing777Before&quot;, show := true)   <br /> Call to custom dll was here.    <br /> The crash occurred before this message was displayed.    <br />%%iv_message(&quot;Tracing777After&quot;, show := true)</p>
<p>Note: ETO 2013 R2 allows you to debug rules in Visual Studio</p>
<p><strong>Sphere Design fails after install R2 Hotfix (build 202)</strong></p>
<p>To resolve this problem download this iks file and put it in this directory.&#0160; (Back up the existing xxhostSphere.iks)</p>
<p><em>“C:\Program Files\Autodesk\Inventor ETO Components 2013\Library\Inventor\ivhostlib”</em></p>
<p>&#0160;
<span class="asset  asset-generic at-xid-6a00e553fcbfc6883401901cd238ac970b"><a href="http://modthemachine.typepad.com/files/xxhostsphere.iks">Download XxhostSphere</a></span></p>
<p>-Wayne</p>
