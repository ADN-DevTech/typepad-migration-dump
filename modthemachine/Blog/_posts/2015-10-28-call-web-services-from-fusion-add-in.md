---
layout: "post"
title: "Call web services from a Fusion add-in "
date: "2015-10-28 12:06:36"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/10/call-web-services-from-fusion-add-in.html "
typepad_basename: "call-web-services-from-fusion-add-in"
typepad_status: "Publish"
---

<p>There are more and more web services available out there on the net and integration with those can bring real value to your product. Sometimes the <strong>web service</strong>&#0160;in itself is the real value provided by the add-in in which case you do not really need to protect the <a href="https://en.wikipedia.org/wiki/Intellectual_property" target="_self"><strong>IP</strong></a> of your add-in and might as well use open&#0160;<strong>JavaScript</strong> or <strong>Python</strong> instead of a compiled <strong>C++</strong> project.</p>
<p>The following sample script is written in <strong>Python</strong> and it simply lists all the repositories inside our <strong>Fusion 360 GitHub</strong> account:&#0160;<a href="https://github.com/AutodeskFusion360" target="_self" title="">https://github.com/AutodeskFusion360</a></p>
<p>When the user selects a repository from the drop-down combo box then the description field will get updated inside the command dialog. If the user clicks <strong>OK</strong> then the browser will jump to the <strong>URL</strong> of the selected repository.</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb0887ec5b970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="HttpSample" border="0" class="asset  asset-image at-xid-6a00e553fcbfc6883401bb0887ec5b970d image-full img-responsive" src="/assets/image_395629.jpg" title="HttpSample" /></a></p>
<p>&#0160;</p>
<p>It&#39;s fairly easy to implement such a thing in <strong>Python</strong>. I could use the <strong>http.client</strong> library for the <strong>HTTP</strong> communication, <strong>json</strong> library to turn the <strong>JSON</strong> response strings into arrays and dictionaries which make it much easier to get the data out of them, and the&#0160;<strong>webbrowser</strong> library to start the default web browser on the system and show a given <strong>URL</strong>.</p>
<p>You can find the source code here:&#0160;<a href="https://github.com/AutodeskFusion360/HttpSample" target="_self" title="">https://github.com/AutodeskFusion360/HttpSample</a></p>
<p>-Adam</p>
