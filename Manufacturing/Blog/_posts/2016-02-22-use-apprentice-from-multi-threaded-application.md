---
layout: "post"
title: "Use Apprentice from multi-threaded application"
date: "2016-02-22 06:01:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/use-apprentice-from-multi-threaded-application.html "
typepad_basename: "use-apprentice-from-multi-threaded-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I wrote about it already that you can use <strong>Apprentice</strong> in a <strong>side-thread</strong>, but that sample only showed a single instance doing it:<br /><a href="http://adndevblog.typepad.com/manufacturing/2014/09/apprentice-in-side-thread.html">http://adndevblog.typepad.com/manufacturing/2014/09/apprentice-in-side-thread.html</a></p>
<p>Now here is a sample running multiple <strong>Apprentice</strong> instances in the same application on different threads. Each thread creates&#0160;its own <strong>ApprenticeServerComponent</strong>&#0160;- note that this object can only be used from the thread that created it, since it&#39;s <strong>not thread-safe</strong>.</p>
<p>Depending on how much calculation you&#39;re doing and how your app is structured, it could also be a good idea to migrate&#0160;the <strong>Apprentice</strong> usage into a separate app which could be used from the main application.</p>
<p>This sample project shows both ways of doing things and allows you to switch between them through the &quot;<strong>Use Apprentice in-process</strong>&quot; check box.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c819ae25970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ApprenticeMultiThread" class="asset  asset-image at-xid-6a0167607c2431970b01b7c819ae25970b img-responsive" src="/assets/image_79b8cd.jpg" title="ApprenticeMultiThread" /></a></p>
<p>Source:&#0160;<br /><a href="https://github.com/adamenagy/Apprentice-MultiThread">https://github.com/adamenagy/Apprentice-MultiThread</a></p>
<p>By the way, if you want to set the <strong>Output</strong> directory of a <strong>.NET</strong> project relative to the <strong>Solution</strong> directory, you can do that by editing the project file in a text editor:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c819af47970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SolutionDir" class="asset  asset-image at-xid-6a0167607c2431970b01b7c819af47970b img-responsive" src="/assets/image_7f71b8.jpg" title="SolutionDir" /></a></p>
<p class="p1"><a href="https://social.msdn.microsoft.com/Forums/vstudio/en-US/b0eb3746-285f-4ec6-9658-154f427cdb80/c-project-setting-output-directory-to-solution-dir?forum=msbuild">C# project : setting output directory to solution dir</a></p>
