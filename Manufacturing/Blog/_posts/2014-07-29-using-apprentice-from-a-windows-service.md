---
layout: "post"
title: "Using Apprentice from a Windows Service"
date: "2014-07-29 14:25:43"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/using-apprentice-from-a-windows-service.html "
typepad_basename: "using-apprentice-from-a-windows-service"
typepad_status: "Publish"
---

<p>By&#0160;<a href="https://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>First of all, we have to clarify that using <strong>Apprentice</strong> or <strong>Inventor</strong> from a <strong>Windows Service is not supported</strong>. So if you really get into issues with it for one reason or another then we cannot help you.</p>
<p>If you still decide to give it a try then keep on reading.&#0160;</p>
<p>Some info on creating a <strong>.NET Windows Service</strong> and debugging it:<br />-&#0160;<a href="https://msdn.microsoft.com/en-us/library/zt39148a(v=vs.110).aspx" target="_self" title="">Walkthrough: Creating a Windows Service Application in the Component Designer<br /></a>- <a href="https://msdn.microsoft.com/en-us/library/ddhy0byf(v=vs.110).aspx" target="_self" title="">How to: Add Installers to Your Service Application</a>&#0160;<br />-&#0160;<a href="https://msdn.microsoft.com/en-us/library/7a50syb3(v=vs.110).aspx" target="_self" title="">How to: Debug Windows Service Applications</a> &#0160;&#0160;</p>
<p>For testing purposes I created a service that monitors a given folder for <strong>ipt</strong> files being moved to it. If an ipt file shows up then the service retrieves the <strong>Summary Information</strong> iProperties from the document and appends that to a log file residing in the same folder that is being monitored:&#0160;<a href="https://github.com/adamenagy/ApprenticeFromWindowsService" target="_self" title="">https://github.com/adamenagy/ApprenticeFromWindowsService</a></p>
<p>If you created a <strong>Windows Service</strong> and it uses <strong>Local System account</strong> then it will try to get the registry keys from the <strong>HKLM</strong> (Local Machine) part of the registry which does not include information about all the Inventor COM objects. Therefore creating the <strong>ApprenticeServerComponent</strong> will fail with message &quot;<strong>Retrieving the COM class factory for component with CLSID {C343ED84-A129-11D3-B799-0060B0F159EF} failed due to the following error: 80040154 Class not registered (Exception from HRESULT: 0x80040154 (REGDB_E_CLASSNOTREG)).</strong>&quot;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b01a73df71eec970d-pi" style="display: inline;"><img alt="Error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73df71eec970d image-full img-responsive" src="/assets/image_a7e6ef.jpg" title="Error" /></a>&#0160;</p>
<p>There could be a couple of solutions:<br /><strong>1)</strong> Run the service under a <strong>user account</strong>:<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd3c24cd970b-pi" style="display: inline;"><img alt="UserAccount" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd3c24cd970b image-full img-responsive" src="/assets/image_b3031c.jpg" title="UserAccount" /></a><br /><br />In the video you can see that after starting the service from the admin account the service is functional even from a <strong>Guest</strong> account: <a href="https://www.youtube.com/watch?v=Zh9pY0u3dsQ" target="_self" title="">https://www.youtube.com/watch?v=Zh9pY0u3dsQ</a>&#0160;<a href="https://www.youtube.com/watch?v=Zh9pY0u3dsQ&amp;list=UUDGVCCCfod3yhELCbLcqilQ&amp;index=1" target="_self" title=""><br /></a></p>
<p><strong>2)</strong> Copy all the Inventor COM object entries from the <strong>HKCU</strong> (Current User) section of the registry to the <strong>HKLM</strong> (Local Machine) one. This might be risky as modifying registry entries usually can be. So make sure you know what you are doing: <a href="https://github.com/adamenagy/RegistryCopy" target="_self" title="">https://github.com/adamenagy/RegistryCopy</a>&#0160;</p>
<p>Here is also a video showing how copying the registry entries from <strong>HKCU</strong> to <strong>HKLM</strong> solves the problem:&#0160;<a href="https://www.youtube.com/watch?v=bqcIcspnwuQ" target="_self" title="">https://www.youtube.com/watch?v=bqcIcspnwuQ</a><a href="https://www.youtube.com/watch?v=bqcIcspnwuQ&amp;feature=youtu.be" target="_self" title=""><br /></a></p>
<p><strong>3)</strong> Don&#39;t use a Windows Service for this. :)</p>
<p>&#0160;</p>
