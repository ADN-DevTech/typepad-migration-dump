---
layout: "post"
title: "Drive AutoCAD and ARX from C++ app"
date: "2015-12-14 01:53:36"
author: "Adam Nagy"
categories:
  - "ActiveX"
  - "Adam Nagy"
  - "AutoCAD"
  - "MFC"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/12/drive-autocad-and-arx-from-c-app.html "
typepad_basename: "drive-autocad-and-arx-from-c-app"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html">Adam Nagy</a></p>
<p>Just like in case of a <strong>.NET</strong> application, in a <strong>C++</strong> application as well you&#39;ll need to handle certain <strong>COM</strong> messages like&#0160;<span class="s1"><strong>0x80010001 (RPC_E_CALL_REJECTED)</strong> in order to keep things running</span>:<br /><a href="http://through-the-interface.typepad.com/through_the_interface/2010/02/handling-com-calls-rejected-by-autocad-from-an-external-net-application.html">Handling COM calls rejected by AutoCAD from an external .NET application</a></p>
<p>You also need to take care of making sure your add-in will be allowed to load otherwise a security dialog will pop up:<br /><a href="http://adndevblog.typepad.com/autocad/2015/05/autocad-2016-trusted-paths-and-autoloader.html">AutoCAD 2016: Trusted paths and AutoLoader</a></p>
<p>For testing purposes, the easiest thing might be to add the path of your <strong>arx</strong>/<strong>dll</strong> to the trusted paths in the <strong>Options</strong> dialog: <strong>Options</strong> &gt;&gt; <strong>Files</strong> &gt;&gt; <strong>Trusted</strong> <strong>Locations</strong></p>
<p><a class="asset-img-link" href="http://a5.typepad.com/6a0112791b8fe628a401b7c7f9e4d5970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="TrustedPaths" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c7f9e4d5970b img-responsive" src="/assets/image_311785.jpg" title="TrustedPaths" /></a></p>
<p>The rest should be relatively straight-forward.</p>
<p>Here is a sample app that demonstrates this. It includes and <strong>exe</strong> and an <strong>ARX</strong> project. In the <strong>exe</strong> you just have to modify the path of the <strong>arx</strong> and <strong>dwg</strong> files which are being used by the sample:<br /><a href="https://github.com/adamenagy/AutoCAD-CppExeAndARX">https://github.com/adamenagy/AutoCAD-CppExeAndARX</a></p>
<p>&#0160;</p>
