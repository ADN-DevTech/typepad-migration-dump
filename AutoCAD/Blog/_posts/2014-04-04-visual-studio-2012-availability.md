---
layout: "post"
title: "Visual Studio 2012 availability"
date: "2014-04-04 00:59:15"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2015"
  - "AutoCAD"
  - "ObjectARX"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2014/04/visual-studio-2012-availability.html "
typepad_basename: "visual-studio-2012-availability"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html" target="_blank">Stephen Preston</a></p>
<p>As explained in other blog posts both here and on <a href="http://through-the-interface.typepad.com/through_the_interface/2014/03/autocad-2015-calling-commands.html" target="_blank">Kean Walmsley&#39;s excellent blog</a> (and of course in the ObjectARX 2015 documentation), AutoCAD 2015 is not binary (API) compatible with previous AutoCAD versions. This means that the Visual Studio version required to build your plug-ins has also changed. What this actually means to you depends on which API you&#39;re using.</p>
<p>For .NET they key requirement is .NET Framework 4.5. Both Visual Studio 2012 and 2013 support .NET Framework 4.5, so you should be able to use whichever of those two IDEs you prefer. Many .NET developers will likely go for Visual Studio 2013 because of the new 64-bit edit-and-continue feature (which is available for .NET debugging only - not native C++).</p>
<p>Requirements for ObjectARX (native C++) developers are a little more stringent. Because an OjectARX plug-in is a C++ DLL, it has to be compiled using the same Visual C++ compiler version as AutoCAD itself. This means that you must build your ObjectARX plug-ins using the C++ compiler that ships with Visual Studio 2012 (update 4). If you have Visual Studio 2013 installed as well, then you can use that IDE - but you must still install Visual Studio 2012 (update 4) and target the correct C++ compiler using the Platform Toolset feature.</p>
<p>Why is AutoCAD built using Visual Studio 2012 instead of Visual Studio 2013? Quite simply because Visual Studio 2013 was released by Microsoft too late in our development cycle to make the switch. (AutoCAD is a complex beastie, and its not trivial to convert such a large codebase to a new compiler).</p>
<p>This has led to a few questions to our ADN team about how to obtain a copy of Visual Studio 2012 - which is the reason for this blog post. <strong>I asked one of the Microsoft team who manages our Autodesk corporate account for a definitive statement on Visual Studio 2012 availability, which I&#39;m posting below verbatim.</strong> Remember as you read it that this is written from a US perspective, so prices are in US dollars and programs mentioned may differ in other countries. Contact your local Microsoft reseller for full information on your local situation:</p>
<p>&gt;&gt;&gt;</p>
<p><strong>Options to acquire VS 2012</strong></p>
<p>Due to the release of VS 2013 many customers need to still acquire VS 2012 for compiler compatibility.</p>
<p>We are currently offering an upgrade price for customers who purchased VS 2012 to VS 2013 but there is no upgrade available for older products like VS 2010.</p>
<p>While you can target the VS 2012 tool set in VS 2013 by setting this up in the project properties, you must already have VS 2012 to do this.</p>
<p>Volume License customers can purchase VS 2013 and download VS 2012 from their Volume License site.</p>
<p>Any customer that own VS 2013 with MSDN can download and use VS 2012.</p>
<p>For large customers with Volume License agreements this is probably not an issue.&#0160; Prices are approx. $450 for VS Pro license only and approx. $400/yr for VS Pro with MSDN</p>
<p>For smaller customers here are their choices:</p>
<ul type="disc">
<li>If the customer does less than 2M/yr and has been in business less than 3 years they can sign up for our BizSpark program and acquire as much VS with MSDN as they need for free.&#0160; To sign up customers just need to go to <a href="http://www.microsoft.com/bizspark"><strong>www.microsoft.com</strong>/<strong>bizspark</strong></a><strong>.</strong></li>
<li>Customers customer can buy VS Pro with MSDN on Open Licensing (not a Volume licensing program but provides some discounts) for approx. $900/yr</li>
<li>Customers can purchase VS Pro with MSDN directly from Microsoft for approx. $1200/yr</li>
<li>Customers can acquire VS Pro 2012 box copy from sites like eBay for approx. $500</li>
</ul>
<p>&lt;&lt;&lt;</p>
<p><strong>Update 4/24/14:</strong></p>
<p>Following a smallish thread on this in the Twittersphere, I&#39;ve updated the text above to bold an important sentence about the source of the quoted information - to make that more obvious. I&#39;ll also take this opportunity to draw your attention to the Disclaimer contained in the &#39;About this blog&#39; page (link at the top of this page - just below the banner).</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
