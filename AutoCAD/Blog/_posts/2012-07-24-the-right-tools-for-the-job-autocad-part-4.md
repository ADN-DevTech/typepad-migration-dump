---
layout: "post"
title: "The Right Tools for the Job &ndash; AutoCAD Part 4"
date: "2012-07-24 10:58:31"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-4.html "
typepad_basename: "the-right-tools-for-the-job-autocad-part-4"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Leading on from <a href="http://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-3.html">Part 3</a> I wanted to talk to you about the importance of ObjectARX compiler settings.</p>
<p>As I mentioned before, the code I used to test the performance of the various languages was all written a while ago and that I had decided to port them to AutoCAD 2011 in order to rerun the tests.</p>
<p>When I ported the Visual Studio 2005 ObjectARX and .NET projects to Visual Studio 2008 I simply recompiled in Release mode and started testing.</p>
<p>What I found very strange was that the Release mode .NET tests were running about 35% *faster* than the Release mode ObjectARX tests?!?!? Obviously, this could never be the case because .NET wraps ObjectARX so ObjectARX will always be faster that .NET in the AutoCAD world.</p>
<p>I found that the problem was that the Release mode ObjectARX Compiler Optimization settings were way off, probably due to some Visual Studio project migration issue or maybe even a bug in the old ObjectARX wizard that created the project settings. Once I updated the settings, everything started to work as expected, as you can see in the results that I posted previously.</p>
<p>At this point, I decided to take some time to try and see which settings affected the performance best.</p>
<p>What I found was that, for my test harness code, the Visual Studio Project Property page settings “Optimization=Max Speed” and “Favor Size or Speed=Favor Fast Code” settings displayed in Red below in made the greatest *difference* in performance time, without those set correctly .NET ran faster! Also, you should set the other settings as you can see below - Let’s call is Stage 1 - It got me to around 8.51 seconds for 1,000,000 Xrecords, taken as an average from 3 different test runs…</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016768b7d07e970b-pi"><img alt="SNAGHTML40ed2be" border="0" height="348" src="/assets/image_287206.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="SNAGHTML40ed2be" width="495" /></a></p>
<p>Notice that I’m not using “Whole Program Optimization” yet. With that setting turned on (and also inside of the Linker Optimization page “Link Time Code Generation” (see just below)),&#0160; let’s call this Stage 2, the time went down again to 8.047 seconds, taken as an average from 3 different test runs…</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616accc75970c-pi"><img alt="SNAGHTML41d1dd1" border="0" height="352" src="/assets/image_530015.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="SNAGHTML41d1dd1" width="501" /></a></p>
<p>You could be confused by this given that my test harness code is in one .cpp file, but remember that we are linking many ObjectARX modules, so that’s where the optimization comes in.</p>
<p>Next, setting “Enable String pooling=Yes” and “Buffer Security Check=No” surprisingly slowed things down to 8.96. I say surprisingly because I considered that my loop contained items which I figured turning off the Buffer Security Check switch would affect, must be the String pooling that is affecting it…</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616accc8d970c-pi"><img alt="SNAGHTML43a3ac4" border="0" height="357" src="/assets/image_774909.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="SNAGHTML43a3ac4" width="508" /></a></p>
<p>Changing back “Enable String pooling=No” and keeping “Buffer Security Check=No” did make things run slightly quicker with a time of 8.54, but still not quicker than its default setting, I find this strange…</p>
<p>So it seems that Stage 2 is the best settings for performance on AutoCAD 2011 with Visual Studio 2008, running my specific test harness.</p>
<p>Now to talk a little bit. I’m sure a lot of you out there have read the results of the settings and have not been too surprised about the results, am I right? But how many of your ObjectARX programmers have assumed that the your ObjectARX Release mode settings are optimal as I speak? Do you take time with every release to examine what new settings are and consider what each one might do (or not do) for your application? Did you double check the settings that the Visual Studio Project migration wizard created for you? I know that I don’t <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_453619.jpg" style="border-style: none;" /></p>
<p>So here’s some basic advice for all you ObjectARX programmers with regards AutoCAD ObjectARX Visual Studio Release mode project settings…</p>
<ol>
<li>Never assume you have the best Visual Studio Release mode performance settings for your ObjectARX application. </li>
<li>Never trust the migration wizards, always check what they have done. <ol>
<li>Visual Studio is always being enhanced, make sure your migrated project has all of the latest, relevant project settings setup – especially for Release mode.</li>
</ol></li>
<li>We take a lot of time creating the best Visual Studio project settings for your ObjectARX applications, you should check the ObjectARX samples to see what performance settings you should use.</li>
</ol>
<p>Next I’m going to look at .NET performance and reveal some performance coding techniques that I use. See Part 5 <a href="http://adndevblog.typepad.com/autocad/2012/08/the-right-tools-for-the-job-autocad-part-5.html" target="_self">here</a></p>
