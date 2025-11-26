---
layout: "post"
title: "Installed version of RealDWG application crashes"
date: "2013-04-05 03:29:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/installed-version-of-realdwg-application-crashes.html "
typepad_basename: "installed-version-of-realdwg-application-crashes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Issue</strong></p>
<p>My application runs fine on the development machine but the installed version crashes on a clean test machine.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c38562a7b970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RDWG_missing_policies" class="asset  asset-image at-xid-6a0167607c2431970b017c38562a7b970b" src="/assets/image_331888.jpg" style="width: 450px;" title="RDWG_missing_policies" /></a></p>
<p>If I only reference my version of HostApplicationServices inside a&#0160;try-catch (Autodesk.AutoCAD.Runtime.Exception), then the installed version pops up this error:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d42854588970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RDWG_missing_policies2" class="asset  asset-image at-xid-6a0167607c2431970b017d42854588970c" src="/assets/image_977519.jpg" style="width: 450px;" title="RDWG_missing_policies2" /></a></p>
<p>So it seems that some components are missing. When I checked the&#0160;sample that comes with the Introduction to <a href="http://download.autodesk.com/media/adn/DevTV-Introduction-to-RealDWG-Programming/" target="_self">RealDWG Programming DevTV</a> presentation, that had the same problem.&#0160;</p>
<p><strong>Solution</strong></p>
<p>The sample is written in VS 2005. When you migrate it to VS 2008, then for some reason Visual Studio does not automatically pull in the policy merge modules that come with the included merge modules (*.msm)</p>
<p>As you&#39;ll find in many articles on the net, most Visual Studio merge modules require the corresponding policy merge modules.</p>
<p>So just make sure that those are also added to your merge module or installer project:</p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9f987e9970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RDWG_policy_files" class="asset  asset-image at-xid-6a0167607c2431970b017ee9f987e9970d" src="/assets/image_553209.jpg" style="width: 450px;" title="RDWG_policy_files" /></a><br />
<p>&#0160;</p>
