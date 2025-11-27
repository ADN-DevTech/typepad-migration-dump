---
layout: "post"
title: "Cannot load or reload add-in"
date: "2015-12-18 04:20:01"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/12/cannot-load-or-reload-add-in.html "
typepad_basename: "cannot-load-or-reload-add-in"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you have issues with loading or reloading your <strong>add-in</strong> these might be the most likely issues.&#0160;</p>
<p><strong>1) Issues with loading the add-in</strong></p>
<p>This is the scenario when your <strong>add-in</strong> never gets loaded.&#0160;</p>
<p><strong>Is your add-in found by Inventor?</strong><br />- if your <strong>add-in</strong> is not listed in the <strong>Add-In Manager</strong> dialog then <strong>Inventor</strong> cannot even find your <strong>add-in</strong>. You should check that you placed your <strong>*.addin</strong> file in the right folder and that all the information in it is correct:&#0160;<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=20143212">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=20143212</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d185ea5d970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Addinloading" class="asset  asset-image at-xid-6a0167607c2431970b01b8d185ea5d970c img-responsive" src="/assets/image_dd08a8.jpg" title="Addinloading" /></a></p>
<p><strong>Does your add-in have a unique id?<br /></strong>- maybe your <strong>add-in</strong> was based on an existing <strong>add-in</strong> and so is using the same <strong>GUID</strong> as another one which gets loaded into <strong>Inventor</strong>. You can always list all the add-in <strong>GUIDs</strong> which are being loaded into <strong>Inventor</strong> and compare that to your <strong>add-in</strong>&#39;s <strong>GUID</strong>. &#0160; &#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7fc1629970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Guid" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7fc1629970b img-responsive" src="/assets/image_6444a8.jpg" title="Guid" /></a></p>
<p><strong>Is the bitness of your add-in the same as Inventor&#39;s?<br /></strong>- check the project setting that it&#39;s <strong>32 bit</strong> on a <strong>32 bit OS</strong> and <strong>64 bit</strong> on a <strong>64 bit</strong> <strong>OS</strong>. In case of a <strong>.NET add-in</strong> the easiest is to use the &quot;<strong>Any CPU</strong>&quot; setting</p>
<p><strong> <a class="asset-img-link" href="http://a5.typepad.com/6a0112791b8fe628a401bb08a09efd970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Bitness" class="asset  asset-image at-xid-6a0112791b8fe628a401bb08a09efd970d img-responsive" src="/assets/image_9c5934.jpg" title="Bitness" /></a></strong></p>
<p><strong>Is your add-in COM Visible?<br /></strong>- make sure that &quot;<strong>Make assembly COM-Visible</strong>&quot; setting is checked. Note: do not confuse it with the &quot;<strong>Register for COM interop</strong>&quot; setting which should be switched off as that would make the <strong>add-in</strong> use the old registration mechanism.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7fc1703970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Comvisible" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7fc1703970b img-responsive" src="/assets/image_b0a498.jpg" title="Comvisible" /></a></p>
<p><strong>Still cannot load it?</strong><br />- if you still cannot even step into the code inside the <strong>Activate</strong>() method then you might have some unresolved dependencies or you might have some issues inside the constructor of your <strong>ApplicationAddInServer</strong> implementation or maybe some global variables that error out. If there are any issues before reaching the <strong>Activate()</strong> method or inside that, your<strong> add-in</strong> will not get loaded.&#0160;</p>
<p><strong>2) Issues with reloading add-in </strong></p>
<p>This is the scenario when you could already load your <strong>add-in</strong> successfully, but within the same <strong>Inventor</strong> session, once you unloaded your <strong>add-in</strong> you could not get it loaded again.</p>
<p><strong>Are you removing the controls you are creating?</strong><br />- the most likely problem is that you are creating some control definitions and <strong>UI</strong> components, but you do not remove them inside the <strong>Deactivate()</strong> method when your <strong>add-in</strong> gets unloaded. So&#0160;when you try to load your <strong>add-in</strong> again, since those components already exist, your code will error out and the loading of the <strong>add-in</strong> will be cancelled. &#0160;&#0160;</p>
