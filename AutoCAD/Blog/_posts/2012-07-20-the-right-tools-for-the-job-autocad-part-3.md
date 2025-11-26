---
layout: "post"
title: "The Right Tools for the Job &ndash; AutoCAD Part 3"
date: "2012-07-20 15:25:45"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-3.html "
typepad_basename: "the-right-tools-for-the-job-autocad-part-3"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Leading on from <a href="http://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-2.html">Part 2</a> – it’s time to start looking at performance (finally).</p>
<p>Just so you understand where I’m going with this ‘Performance’ section – I’m going to start by giving you an overview of the different language performance by looking into what I consider a good generic test scenario for all of the languages, namely “Storing Data inside of AutoCAD”. I’ll be using Xrecords, Xdata and ObjectARX Custom Objects.</p>
<p>Then, in some following posts, I’m going to drill into ObjectARX and .NET specifically to provide you with all of the performance techniques that I think you should know. I’ll show you how to optimize your ObjectARX and .NET code so it runs the fastest way I know how. I won’t show any techniques for optimizing VBA or LISP as I figure it’s now common knowledge that if you use those languages you don’t really mind that much about performance.</p>
<p>Let’s first start by looking at the different languages API support that comes inside of AutoCAD which all related to “Storing Data inside of AutoCAD” topic…</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616975e6a970c-pi"><img alt="image" border="0" height="238" src="/assets/image_97294.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="495" /></a></p>
<p>As you can see, ObjectARX is the most versatile of them all providing the developer with all of the above. As you would expect with such a common API task, all of the other languages compare very nicely if you exclude Custom Objects and Persistent Reactor referencing.</p>
<p>If you are wondering what Persistent Reactor referencing is, let me explain. So in ObjectARX you can create Reactors (Events) that are persisted with the DWG file. These Persistent Reactors are implemented via a Custom Object which has special event functions that are specifically called when an event happens. You need to use Persistent Reactor referencing to setup the reactor link between the object that you want to receive the events from and to your Custom Reactor Object. A cheap trick I like to use when programming ObjectARX is to setup a hard pointer reference to some other object inside of AutoCAD using the Persistent Reactor referencing functionality. I mention it because you often want to link a data object to an entity using a hard pointer, so this is a cheap way to do it (rather than creating your own links internally to your objects), but it can cost a few extra CPU cycles when accessing the linked objects because AutoCAD sends wasted notifications to the linked object.</p>
<p>Now before you look below at the results, Stephen did mention that this was an old presentation. Looking at the old results I decided to go through and rerun all the tests so I could remember some more of the details. Here are some points you should be aware of:</p>
<ol>
<li>Back then, the 64bit version of AutoCAD was not available so I had to create my own VB6 Out of Process test harness, whereas now days, simply running the same VBA code on 64bit AutoCAD gives the same results as the old VB6 exe. </li>
<li>I didn’t take care to fully optimize all of the code I wrote, however, I did tweak the Release mode ObjectARX compiler settings. I wanted to be fair to all of the languages, so I created fairly generic code that performs the same task in each language. Nevertheless, it works and I feel that it does give a good measure of relative performance between all of the languages. </li>
<li>If I remember right, all of the code was written for AutoCAD 2007. Rather than spend all my time migrating to AutoCAD 2013, I decided that the easiest path to rerunning the tests was to migrate the .NET and ObjectARX Visual Studio projects to run on AutoCAD 2011 64bit – AutoCAD 2011 64bit was the test platform on my HP Elitebook 8540w.</li>
<li>I’m not posting the code used for these tests now, because I don’t want people to focus on the code here, given that I will be discussing that in later posts.</li>
<li>Lastly, as with all computer programs, performance really depends what you are doing in your code, how you are doing it, which parts of your execution run as part of an API or actually inside of your own code and the computer you are running on. What I’m trying to say is that the actual speed of any computer program will vary – therefore you should take these results as a rough indicator, not as a definitive benchmark.</li>
</ol>
<p>Enough details, let’s look at some data.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616975e74970c-pi"><img alt="image" border="0" height="258" src="/assets/image_992242.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="491" /></a></p>
<p>As I mentioned, there were three data storage categories that I ran my tests on:</p>
<ol>
<li>Creating a Dictionary entry and storing an XRecord to it </li>
<li>Creating a Dictionary entry and and storing a Custom Object to it</li>
<li>Creating a Dictionary entry and storing XData to it</li>
</ol>
<p>I created sample code for each language and ran them for 1 million cycles (performing the same operation 1 million times) with a timer routine which timed the whole process.</p>
<p>You’ll notice in the above slide that I have missed out the VB6/VBA (Out of Process) results – this is because they are *so* much slower than ObjectARX, .NET, VBA (In process) and LISP that if I were to apply the data to this slide you wouldn’t even see ObjectARX or .NET data showing up on the scale!</p>
<p>Check out why VBA developers needed to migrate their applications to VB.NET using COM Interop if they want to run on 64bit!</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616976738970c-pi" style="display: inline;"><img alt="20-07-2012 15-35-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017616976738970c image-full" src="/assets/image_162318.jpg" title="20-07-2012 15-35-01" /></a></p>
<p>Of course, all of my tests showed that ObjectARX was significantly faster than all the other languages, no surprise there. That said, you can now clearly see why I don’t use Transactions in any of my sample code, it’s slower than Open/Close for both .NET and ObjectARX. I’ll dig into why this is in a later post.</p>
<p>What I found really surprising, is how well VBA (In Process 32bit) and LISP performed. Yes, .NET is about 5 times faster for this specific test benchmark, but considering that the time is only 75-80 seconds for 1,000,000 records, that’s pretty impressive. Looking into why this is happening, I can see that the reason is that most of the work is actually done under the hood by AutoCAD (C++ code), reinforcing my earlier point that it really depends on what you are doing whether you see dramatic (or not) resulting time differences.</p>
<p>The specific Xdata test I ran though, in my opinion, is not really a good real life benchmark because generally speaking Xdata will not be attached to a Dictionary entry that we just created. So I decided to run a pure test against attaching Xdata to an existing object. Surprisingly in my tests, attaching Xdata to an existing entity only gave slightly better results for .NET and ObjectARX 8 seconds instead of 9 for ObjectARX and 14 seconds instead of 15.&#0160;</p>
<p>Next I’m going to look into ObjectARX compiler settings, and show you what a difference the correct settings can make… I’ll address all of your performance questions as best I can in future posts, so keep them coming.</p>
<p>Read Part 4 <a href="http://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-4.html">here</a></p>
