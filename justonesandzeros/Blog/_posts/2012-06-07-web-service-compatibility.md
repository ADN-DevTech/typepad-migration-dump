---
layout: "post"
title: "Web Service Compatibility"
date: "2012-06-07 10:54:00"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/06/web-service-compatibility.html "
typepad_basename: "web-service-compatibility"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Hopefully you already know that Vault 2012 clients can access the Vault 2013 server without the need for any sort of software update.&#0160; This compatibility feature works at the web service API level.&#0160; Basically, anything that uses the Vault 2012 APIs can connect to Vault 2013.</p>
<p>What is compatible with Vault 2013 server:</p>
<ul>
<li>2013 applications. </li>
<li>2012 CAD clients such as AutoCAD, Inventor and Civil 3D.</li>
<li>Vault 2012 clients such as Vault Explorer, Autoloader and JobProcessor.</li>
<li>Any standalone application that calls the 2012 web services. </li>
</ul>
<p>What is not compatible:</p>
<ul>
<li>2013 clients are not compatible with the Vault 2012 server.&#0160; The compatibility only works one way. </li>
<li>Anything that is a 2012 plug-in will not be compatible with the 2013 clients.&#0160; This includes custom commands, job handlers, and event handlers. </li>
<li>Anything that uses Vault 2012 API components outside the Autodesk.Connectivity.WebServices assembly may not be compatible in Vault 2013. </li>
</ul>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Architecture:</strong></p>
<p>An API is basically an interface to an application’s functionality.&#0160; For Vault 2013, the server exposes two interfaces: the Vault 2012 set of web services and the Vault 2013 set of services.</p>
<p><img alt="" src="/assets/diagram.png" /></p>
<p>It’s pretty straightforward.&#0160; The services are distinguished by the service URL.&#0160; Starting in 2013, there is a version component to the service URL.&#0160; <br />For example:     <br />2012 URL:&#0160; http://localhost/AutodeskDM/Services/DocumentService.asmx     <br />2013 URL:&#0160; http://localhost/AutodeskDM/Services/<strong>v17</strong>/DocumentService.asmx</p>
<p>If you are using Autodesk.Connectivity.WebServices.dll, it will only communicate with the web services interface for the corresponding version.&#0160; So the 2012 DLL (16.0.x.x) will communicate only with the 2012 web services, and the 2013 DLL(17.0.x.x) will communicate only with the 2013 web services.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Best Practices:</strong></p>
<ul>
<li><strong>Testing is still needed!</strong>&#0160; <br /><em>Don’t assume that everything works perfectly.        <br /></em></li>
<li><strong>Code to only 1 API version.</strong>&#0160; <br /><em>For example, don’t check out with the 2012 API and check in with the 2013 API.        <br /></em></li>
<li><strong>If you are building a 2013 client, use the 2013 services.        <br /></strong></li>
<li><strong>Don’t hard-code data that may be version specific. </strong>&#0160; <br /><em>For example, don’t write logic around the server’s version number.        <br /></em></li>
<li><strong>Assume that your client may be used with a future Vault server.</strong>&#0160; <em>However, Autodesk makes no guarantee that Vault 2014 will still have this compatibility feature.</em> </li>
</ul>
<p><em><img alt="" src="/assets/Concepts3-1.png" /></em></p>
