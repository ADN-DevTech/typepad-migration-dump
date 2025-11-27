---
layout: "post"
title: "Get ActiveX/COM class properties and methods from .NET"
date: "2013-02-07 10:23:45"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/02/get-activexcom-class-properties-and-methods-from-net.html "
typepad_basename: "get-activexcom-class-properties-and-methods-from-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you are using the COM interface of e.g. AutoCAD or Inventor from a .NET AddIn or application and you want to be able to find out what properties and methods are available for the object, then here is a nice post which shows excactly that. It works even if your project is not referencing the relevant type library/interop assembly and so you are using late binding:<br /><a href="http://limbioliong.wordpress.com/2011/10/18/obtain-type-information-of-idispatch-based-com-objects-from-managed-code/" target="_self">Obtain Type Information of IDispatch-Based COM Objects from Managed Code</a></p>
<p>I tested it with both Inventor and AutoCAD and it seemed to work fine - I got back all the items shown under&#0160;<strong>Dynamic View</strong> inside the <strong>Watch</strong> window:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee84f15a3970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="float: left;"><img alt="Com" class="asset  asset-image at-xid-6a0167607c2431970b017ee84f15a3970d" src="/assets/image_170d74.jpg" style="width: 450px; margin: 0px 5px 5px 0px;" title="Com" /></a>
&#0160;</p>
