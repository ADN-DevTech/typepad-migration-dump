---
layout: "post"
title: "Converting C# to Java – An Experiment (Part 6)"
date: "2012-06-29 14:45:25"
author: "Madhukar Moogala"
categories:
  - "Mobile"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/converting-c-to-java-an-experiment-part-6.html "
typepad_basename: "converting-c-to-java-an-experiment-part-6"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>(Links to previous installments:&#0160;<a href="http://adndevblog.typepad.com/autocad/2012/05/converting-c-to-java-an-experiment-part-1.html" target="_self">Part 1</a>;&#0160;<a href="http://adndevblog.typepad.com/autocad/2012/05/converting-c-to-java-an-experiment-part-1-1.html" target="_self">Part 2</a>;&#0160;<a href="http://adndevblog.typepad.com/autocad/2012/05/converting-c-to-java-an-experiment-part-3.html" target="_self">Part 3</a>;&#0160;<a href="http://adndevblog.typepad.com/autocad/2012/06/converting-c-to-java-an-experiment-part-4.html" target="_self">Part 4</a>; <a href="http://adndevblog.typepad.com/autocad/2012/06/converting-c-to-java-an-experiment-part-5.html" target="_self">Part 5</a>).</p>
<p>I left this series hanging because I was busy with our <a href="http://modthemachine.typepad.com/my_weblog/2012/06/mfg-devcamp-2012-material-zip-file.html" target="_self">ADN DevCamps</a> for a while, and then I had some frustration finishing off my app. I had intended to add functionality to allow the user to save/load as many prioritization lists as they wanted (as I did for my AutoCAD version), but after much searching I discovered that the Android SDK doesn&#39;t include a file picker (open/save) activity or dialog. This seems to be a fairly common question among new Android developers, and it took me a long time to find out because I refused to believe it was true :-). (Please let me know if I overlooked something in a newer Android SDK. Its always a problem googling for information on an SDK that has been around for a while - you find lots of old comments and samples that no longer apply to the latest SDK version).</p>
<p>The options to use a file picker in your app seem to be either to rely on one already being installed on the device that will respond to an intent you broadcast, or to&#0160;&#39;roll your own&#39;. I found a promising looking <a href="https://github.com/iPaulPro/aFileChooser" target="_blank">project on GitHub</a> for the &#39;roll your own&#39; option, and <a href="http://code.google.com/p/android-file-dialog/" target="_blank">another one</a> on <a href="http://code.google.com" target="_blank">code.google.com</a>.</p>
<p>To avoid going off on a tangent, I decided to abandon the multiple file option, and instead use the serialization interfaces I&#39;d already implemented to persist the same list between application sessions, and also to handle the Android OS destroying and later restoring my App Activities in the background if it needed to free up resources. <a href="http://developer.android.com/guide/components/activities.html#Lifecycle" target="_blank">Handling your Activities being destroyed and restored</a> is a very important aspect of Android app development.</p>
<p>To cut a long story short, I&#39;ve posted a new version of the Prioritizer app for you to <a href="http://adndevblog.typepad.com/autocad/Prioritizer.zip" target="_self">download here</a>. It extends the previous version I walked through by adding:</p>
<ul>
<li>A new Activity to perform the binary comparison between activities.</li>
<li>A button to sort Items by priority.</li>
<li>A button to clear the list.</li>
<li>The ability to delete an Item (from the edit activity).</li>
<li>Persistence of the list between states and also better handling of the Activity lifecycle.&#0160;</li>
<li>Use of string resources in layouts instead of hardcoded strings.</li>
</ul>
<p>The app is far from optimized - saving the entire Item list every time anything is updated is particularly klunky - but it works. Hopefully the comments in the code are self-explanatory. The additions are really more of the same, which is why I&#39;ve not documented the changes step-by-step.</p>
<p>To summarize what we&#39;ve found during this exercise:</p>
<ul>
<li>Converting C# to Java is pretty straightforward. Their syntax is very similar, as are the class libraries they provide. This <a href="http://www.harding.edu/fmccown/java_csharp_comparison.html" target="_blank">syntax comparison sheet</a>&#0160;is a great help when you&#39;re trying your first few migrations.</li>
<li>The big differences when porting between platforms are the interfaces to the specific platforms - for example, the UI and the file system. This is why its important to separate your business logic from platform specific code whenever you write an app you plan to port between platforms. This was the reason for the AutoCAD Big Split project (<a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/the-autocad-2013-core-console.html" target="_blank">which Kean has described on his blog</a>).</li>
<li>Android apps consist of Activities. Information is passed between Activities using an Intent. Once you know how to do that you&#39;re up and running.</li>
<li>Android programming is much easier to setup on MacOS than on Windows or Linux ;-), with the added advantage that you can learn iOS programming as well if you buy a Mac :-).</li>
</ul>
<p>I&#39;m off to play around with Windows Azure now to see what all the fuss is about. Some of the guys on our <a href="http://adndevblog.typepad.com/cloud_and_mobile/" target="_self">C&amp;M DevBlog</a> are well ahead of me there, so I may not get to blog about it. I&#39;m also looking forward to coming back to Android and trying the <a href="http://www.rozengain.com/blog/2011/08/23/announcing-rajawali-an-opengl-es-2-0-based-3d-framework-for-android/" target="_blank">Rajawali SDK</a> that <a href="http://through-the-interface.typepad.com/through_the_interface/2012/04/creating-a-3d-viewer-for-our-apollonian-service-using-android-part-1.html" target="_blank">Kean told me about</a>.</p>
<p>&#0160;</p>
