---
layout: "post"
title: "The latest on Windows Azure"
date: "2012-06-15 06:25:00"
author: "Kean Walmsley"
categories:
  - "Azure"
  - "SaaS"
  - "Visual Studio"
  - "WinRT"
original_url: "https://www.keanw.com/2012/06/the-latest-on-windows-azure.html "
typepad_basename: "the-latest-on-windows-azure"
typepad_status: "Publish"
---

<p>A few things have happened over the last week or so that have got me looking, once again, at Windows Azure:</p>
<p>Firstly, there was <a href="http://weblogs.asp.net/scottgu/archive/2012/06/07/meet-the-new-windows-azure.aspx" target="_blank">an exciting release announced last week</a> (I attended <a href="http://www.meetwindowsazure.com/Conversations#ScottGuthrieMeet" target="_blank">the webcast</a> online from San Francisco, where the event happened to be taking place), prior to this week’s <a href="http://channel9.msdn.com/Events/TechEd/NorthAmerica/2012" target="_blank">TechEd</a>. Among the key features were:</p>
<ul>
<li>Support for Linux instances       
<ul>
<li>These will presumably be cheaper, given the lack of OS licensing costs </li>
</ul>
</li>
<li>Virtual Machine capability       
<ul>
<li>IaaS for those that prefer it over PaaS (something no doubt learned from the popularity of <a href="http://aws.amazon.com" target="_blank">AWS</a>) </li>
<li>Select from the online Image Gallery or upload a <a href="http://en.wikipedia.org/wiki/VHD_(file_format)" target="_blank">Virtual Hard Disk (.vhd)</a> </li>
</ul>
</li>
<li>Web-site hosting that scales flexibly </li>
<li>VPN support for private virtual network creation </li>
<li>New cache clustering capability </li>
<li>A nice new management portal (image below) and command-line admin tools</li>
</ul>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201761577513f970c-pi" target="_blank"><img alt="The preview Windows Azure management portal" border="0" height="401" src="/assets/image_850783.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="The preview Windows Azure management portal" width="470" /></a></p>
<p>One additional piece of information I noticed that looked quite helpful was the ability to update web-sites by just uploading the modified files. A seemingly small detail, but one that will be appreciated by many, I suspect.</p>
<p>The other event that had me looking at Azure was the fact my 3-month trial period was coming to an end. I had less that two weeks left on it, and so if I wanted <a href="http://apollonian.cloudapp.net" target="_blank">our Apollonian service</a> to stay up, I needed to work out the best way to make this happen.</p>
<p>Thankfully, MSDN license benefits allowed me more than enough hosting capability that I didn’t (or shouldn’t) need to pay anything additional to host the service. The first hurdle was getting my MSDN subscription to be recognised by the system – I ended up finding out it had lapsed (!), so it needed to be recreated – but after that it was plain sailing to get my subscription added to the portal.</p>
<p>The second major hurdle – and the reason I ended up looking at Azure 1.7 (at least that’s <a href="http://www.microsoft.com/en-us/download/details.aspx?id=29988" target="_blank">the SDK version</a>) more closely – was that I couldn’t just transfer my existing service across to be under the new subscription: I actually had to remove the existing service and redeploy the distribution to Azure to make that happen. And rather than just taking the existing project and redeploying it, I thought I might as well get some practice re-enabling an ASP.NET app to work with Azure.</p>
<p>Which meant I inadvertently got exposed to the fact that caching – something I mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/using-windows-azure-caching-with-our-aspnet-web-api-project.html" target="_blank">this previous post</a>, which I’d apparently left off (but have now added to) <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/cloud-mobile-series-summary.html" target="_blank">the mobile &amp; cloud series summary</a> – has had a serious make-over in this release of Azure. Rather than relying on a centralised cache – which can apparently work out to be quite expensive – it’s possible to carve out a percentage of each instance’s resources to devote to caching. This seemed interesting, but as my service runs on extra-small instances (I know – what a cheap-skate ;-) there wasn’t much by way of resources to carve out and dedicate to caching.</p>
<p>So I ended up sticking to my existing caching approach – the basic 128MB cache that comes for free with my MSDN subscription – rather than increasing the instance size and sharing the load across them. What’s a little worrying is that the new preview management portal doesn’t show information on the former cache implementation at all, which presumably means at some point I’ll have to shift across to the new distributed cache model.</p>
<p>Moving the deployment across from one subscription to another did involve a somewhat scary moment: I had to stop and delete the existing deployment before I could create an equivalent deployment with the same DNS name under the new subscription. This resulted in about 10 minutes of down-time, but also a few nail-biting moments before I knew whether I’d even be able to claim “apollonian.cloudapp.net” under the new subscription (to avoid having to change the various client samples I’d written). Luckily it all worked out… the moral of the story is probably to avoid switching subscriptions, if at all possible (i.e. if you have MSDN, use that from the start rather than using the free trial, first).</p>
<p>I know I’ll need to overhaul the implementation prior to AU – if nothing else because there’s now a Release Candidate available for <a href="http://www.asp.net/whitepapers/mvc4-release-notes" target="_blank">ASP.NET MVC 4</a> – but I’m figuring I’ll see if I can wait for VS 2012 to be released and update the project to use that at the same time.</p>
<p>Speaking of which, I’ve just installed the <a href="http://windows.microsoft.com/en-US/windows-8/release-preview" target="_blank">Windows 8 Release Preview</a> and put the <a href="http://www.microsoft.com/visualstudio/11/en-us" target="_blank">VS 2012 Release Candidate</a> on it. I had to update the project for the Apollonian Viewer for WinRT to use a newer version of <a href="http://sharpdx.org" target="_blank">SharpDX</a> (version 2.2.0) for it to work on this new version of the OS, but the source-code changes from my side were trivial (I had to remove one event handler and update the <em>SystemStyles.xaml</em>). I’ve posted more details – including the updated project – on <a href="http://through-the-interface.typepad.com/through_the_interface/2012/05/creating-a-3d-viewer-for-our-apollonian-service-using-winrt-part-2.html" target="_blank">the last post devoted to this topic</a>.</p>
