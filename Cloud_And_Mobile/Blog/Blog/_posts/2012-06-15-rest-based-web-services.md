---
layout: "post"
title: "REST based Web services"
date: "2012-06-15 14:24:44"
author: "Gopinath Taget"
categories:
  - "Client"
  - "Cloud"
  - "HTML"
  - "Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2012/06/rest-based-web-services.html "
typepad_basename: "rest-based-web-services"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/gopinath-taget.html" target="_blank">Gopinath Taget</a></p>
<p><a href="http://en.wikipedia.org/wiki/Representational_state_transfer" target="_blank">REST</a> stands for Representational State Transfer. It is popular method for designing distributed systems. Competing architectures are <a href="http://en.wikipedia.org/wiki/SOAP" target="_blank">SOAP</a> and <a href="http://en.wikipedia.org/wiki/WSDL" target="_blank">WSDL</a>. REST mandates designing distributed systems based on a set of simple principles:</p>
<p>1) Identify data objects using Unique IDs</p>
<p>2) Provide data objects in the form the client applications need (like <a href="http://en.wikipedia.org/wiki/XML" target="_blank">XML</a> and <a href="http://en.wikipedia.org/wiki/JSON" target="_blank">JSON</a>) and allow manipulations of the data objects just using these representations without needing the original object definitions</p>
<p>3) Data is exchanged using “messages” and each message contains information on how to read the message. I.e., the message is self describing</p>
<p>4) Clients and servers interact only through <a href="http://en.wikipedia.org/wiki/HATEOAS" target="_blank">hypermedia</a>, for instance using <a href="http://en.wikipedia.org/wiki/Uniform_resource_locator" target="_blank">URL</a>s</p>
<p>These principles make it very easy to build Web Services and a variety of clients (Eg: Desktop applications, scripts, other web services and web sites etc) using those web services.</p>
<p>On Windows platforms, it is easy to create REST based Web services using the .NET Windows Communication Foundation (<a href="http://en.wikipedia.org/wiki/Windows_Communication_Foundation" target="_blank">WCF</a>) platform. Here are a few suggestions to get started:</p>
<p><a href="http://msdn.microsoft.com/en-us/magazine/dd315413.aspx" target="_blank">An Introduction To RESTful Services With WCF</a></p>
<p><a href="http://www.codeproject.com/Articles/327420/WCF-REST-Service-with-JSON" target="_blank">WCF REST Service with JSON</a></p>
<p><a href="http://techsavygal.wordpress.com/2009/03/10/getting-started-with-rest-in-wcf/" target="_blank">Getting Started with REST in WCF</a></p>
