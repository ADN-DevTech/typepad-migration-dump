---
layout: "post"
title: "Migrating .NET applications to work with AutoCAD 2013"
date: "2012-03-02 09:35:56"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2012/03/migrating-net-applications-to-work-with-autocad-2013.html "
typepad_basename: "migrating-net-applications-to-work-with-autocad-2013"
typepad_status: "Publish"
---

<p>I’m on my way to San Francisco for some internal meetings (including a <a href="http://en.wikipedia.org/wiki/Hackathon" target="_blank">Hackathon</a>, over the weekend, which should be fun), but have unfortunately been held up at Heathrow by fog (it delayed my inbound flight, along with many others’, but somehow didn’t stop my outbound flight from leaving, which always seems to be the case… I then had to queue for 2.5 hours to get a new flight, the only highlight of which was standing behind members of the junior Kuwaiti ice hockey team [at least that’s what they said they were – they may have been pulling my leg {it was that kind of day :-}]). And so, having spent the night at the Holiday Inn on junction 4 of the M4, I’m now at the airport waiting to catch my Virgin Atlantic flight (a change from BA) to SFO.</p>
<p>I’ve decided to skip talking about the other new APIs in AutoCAD 2013, for now – I’ll look at that more closely next week – but did want to get back to the issue of migrating .NET applications to work with AutoCAD 2013.</p>
<p>Yes, there is a break in binary compatibility for .NET applications in this coming release. ObjectARX developers have been used to see application compatibility (only) <a href="http://through-the-interface.typepad.com/through_the_interface/2006/06/compatibility_o.html" target="_blank">broken every 3 releases since AutoCAD R14</a>, but this is the first time that .NET applications will have needed to be rebuilt since AutoCAD 2007 – at least for developers relying on the core API capabilities made available in that release. Which means there are many applications out there with .NET modules that work for AutoCAD 2007-2012.</p>
<p>Mainly due to The Big Split mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/the-autocad-2013-core-console.html" target="_blank">this previous post</a>, we now have an additional DLL dependency in .NET projects for AutoCAD 2013: you need to include a project reference to <em>AcCoreMgd.dll</em> (in addition to <em>AcMgd.dll</em> and <em>AcDbMgd.dll</em>). If you use the opportunity to write “core” apps (which can, for instance, be loaded in the Core Console) then you will end up replacing your reference to <em>AcMgd.dll</em> with one for <em>AcCoreMgd.dll</em>.</p>
<p>That’s the big change, and the reason why it will be impossible to load older applications in AutoCAD 2013 – the binary code implementing API functionality has physically moved – but for good reasons.</p>
<p>Along with API implementations moving from acad.exe to <em>AcCore.dll</em> (and their public declarations moving from <em>AcMgd.dll</em> to <em>AcCoreMgd.dll</em>), we’ve also added some namespaces to house extension methods for API capabilities that need to be used in full (rather than core) applications: DocumentExtension, DocumentCollectionExtension, EditorExtension, WindowExtension. You will find that certain methods can’t be found when you reference the AutoCAD 2013 DLLs – in most cases it’s a simple matter of adding the right namespace to your project, but in a few cases you’ll find properties/methods have been renamed – mostly to make them more descriptive.</p>
<p>For instance, the existing property to get AutoCAD’s status bar is:</p>
<p>Autodesk.AutoCAD.ApplicationServices.Document.StatusBar</p>
<p>In AutoCAD 2013, this has been changed to:</p>
<p>Autodesk.AutoCAD.ApplicationServices.DocumentExtension.GetStatusBar()</p>
<p>So a little different, but hopefully not to find to track down and manage. The ObjectARX SDK contains migration information that will help identify and make these changes.</p>
<p>An important point to note is that you’ll also need to target .NET 4 for your applications to work with AutoCAD 2013.</p>
<p>Those are the main migrations steps, then:</p>
<ol>
<li>Change your project’s settings to target .NET 4 (if it doesn’t already)</li>
<li>Add an assembly reference to AcCoreMgd.dll</li>
<li>Fix your code to compile properly, which may mean:
<ul>
<li>Changing property and method names, as per the migration guide</li>
<li>Adding namespace references (Imports or using statements) to a few new namespaces</li>
</ul>
</li>
</ol>
<p>Stephen Preston tells me it took him about 10 minutes to migrate the MgdDbg sample project – which exercises a huge amount of the AutoCAD API, so is a pretty good reference for a real-world application.</p>
<p>A few additional points to note…</p>
<p>If you’re using COM Interop, you’ll find the interop assemblies are no longer registered in the GAC (i.e. you won’t find them on the COM tab in the Add Project References dialog). You’ll now need to browse to <em>Autodesk.AutoCAD.Interop.dll</em> and <em>Autodesk.AutoCAD.Interop.Common.dll</em> (I believe) in the AutoCAD Program Files folder or on the ObjectARX SDK. Hopefully Visual Studio will know <a href="http://msdn.microsoft.com/en-us/library/dd409610.aspx" target="_blank">to embed types from these assemblies</a>, which will make deployment more straightforward.</p>
<p>Speaking of deployment… yes, you will need to have (at least one) different module(s) to support AutoCAD 2013 along with AutoCAD 2007-2012 (the number will depend on your application and how it is structured), but using the <a href="http://through-the-interface.typepad.com/through_the_interface/2011/05/adn-devcast-episode-6-autoloader.html" target="_blank">Autoloader</a> will definitely simplify your deployment of these modules for different releases.</p>
<p>It should also be possible to have the same source files build to different modules (you may need to add some conditional compilation settings – something I should address in a blog post, at some point).</p>
<p>My flight’s gate is about to open, so I’d better sign off. I don’t want to miss this one!</p>
<p><em><strong>Update:</strong></em></p>
<p>Stephen Preston just pointed me to <a href="http://www.theswamp.org/index.php?topic=41602.0" target="_blank">a thread on The Swamp</a> where the question of the terminology used to describe the containers housing these extension methods (namespaces vs. static classes) has come up. Stephen has addressed the question in <a href="http://adndevblog.typepad.com/autocad/2012/05/extension-methods-in-autocad-2013.html" target="_blank">a post on the AutoCAD DevBlog</a>, where he explains the reason for the confusion. It seems I&#39;d unwittingly propagated this confusion in this blog post, so be sure to check Stephen&#39;s post if you&#39;re in any doubt on this issue.</p>
