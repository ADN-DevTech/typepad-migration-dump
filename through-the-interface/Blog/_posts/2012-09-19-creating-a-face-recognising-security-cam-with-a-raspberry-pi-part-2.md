---
layout: "post"
title: "Creating a face-recognising security cam with a Raspberry Pi &ndash; Part 2"
date: "2012-09-19 08:59:00"
author: "Kean Walmsley"
categories:
  - "Facebook"
  - "Raspberry Pi"
  - "Social media"
original_url: "https://www.keanw.com/2012/09/creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-2.html "
typepad_basename: "creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-2"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2012/09/creating-a-face-recognising-security-cam-with-a-raspberry-pi-part-1.html" target="_blank">the first post in this series</a>, we introduced the idea behind the Facecam, a Facebook-enabled security camera running on a Raspberry Pi device.</p>
<p>Over the next three posts we’re going to look in more detail at the system’s components. In this post, we’re going to start by looking at the component – the only one that doesn’t actually run on the Raspberry Pi – that downloads the information from Facebook to build the facial recognition database to be used by the device.</p>
<p>As mentioned last time, I’ve coded this as a .NET application – actually by extending <a href="https://github.com/facebook-csharp-sdk/facebook-winforms-sample" target="_blank">the WinForms sample</a> provided with <a href="http://csharpsdk.org" target="_blank">the Facebook C# SDK</a> – in order to dedicate the Raspberry Pi to performing the security cam function. I’m not actually going to share the code for this application, as it’s really a work in progress. The code itself isn’t very complicated – the Facebook piece is mostly executing <a href="https://developers.facebook.com/docs/reference/fql/" target="_blank">FQL</a>, which allows you to use a SQL-like syntax to query <a href="https://developers.facebook.com/docs/reference/api/" target="_blank">the Facebook Graph API</a> – and I’ll be describing the techniques it uses should people want to go ahead and roll their own.</p>
<p>Before looking at the behaviour of the application itself, it’s important to understand more about this “database” that it’s going to help build.</p>
<p>The facial recognition code I’ve adopted is based on <a href="http://www.shervinemami.info/faceRecognition.html" target="_blank">work by Shervin Emami</a> and uses the <a href="http://en.wikipedia.org/wiki/Eigenface" target="_blank">Eigenface</a> algorithm to compare detected faces against a database. To understand more about Eigenface, <a href="http://www.cognotics.com/opencv/servo_2007_series/part_4/index.html" target="_blank">this article</a> provides a thorough explanation.</p>
<p>Shervin’s OnlineFaceRec sample makes use of OpenCV for this process. I’ve borrowed code from this sample for use on the Raspberry Pi, but today’s application simply builds a file and folder structure that can be used by the OnlineFaceRec application to train the database (and essentially build the <em>facedata.xml</em> file we’ll transfer across to the Raspberry Pi).</p>
<p>There are two main parts of this process: downloading the friend data from Facebook and then parsing that data on disk in order to create a training script to be passed to OnlineFaceRec.</p>
<p>These two functions are reflected by the basic UI of the .NET application:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3bff2450970c-pi" target="_blank"><img alt="The simple primary UI for the friend downloader app" border="0" height="260" src="/assets/image_132454.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="The simple primary UI for the friend downloader app" width="394" /></a></p>
<p>Clicking on the first button lets us log in to Facebook and accept the permissions requested by the .NET application. Which reminds me: to create an application that makes use of the Facebook API, you’ll need to <a href="http://facebook.com/developers" target="_blank">register it with Facebook</a> and include the AppId in your code. As you can see below, I called my app “FriendExtractor”:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c31d0a3da970b-pi" target="_blank"><img alt="Facebook login" border="0" height="235" src="/assets/image_424740.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Facebook login" width="454" /></a></p>
<p>Once we’re logged in, we see the main dialog which will help us drive the download process:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017744ae6a65970d-pi" target="_blank"><img alt="The dialog we can use to download the friend information" border="0" height="420" src="/assets/image_959586.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="The dialog we can use to download the friend information" width="354" /></a></p>
<p>Clicking on the “Populate Friends” button pulls down the list of the current user’s friends from Facebook and populates the list on the left:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c31d0a3fa970b-pi" target="_blank"><img alt="The populated friend list" border="0" height="420" src="/assets/image_461756.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="The populated friend list" width="354" /></a></p>
<p>You can see the list has checkboxes associated with each item. Originally the implementation didn’t allow selection of friends to include, which ended in a huge amount of data being downloaded (it turns out I have friends who seem to do nothing other than upload their pictures to Facebook and have 1000+ tagged images up there). Running OnlineFaceRec to train the database with the results ended up in a 400MB facedata.xml file being created. Given the fact the R-Pi has 256MB of RAM&lt; I figured that was a non-starter. :-)</p>
<p>I therefore enabled selection of friends that are actually likely to visit our home – or that I may end up demoing this to at AU – which greatly reduces the amount of probably redundant data that gets downloaded and processed. The list of selected friends gets saved in a simple text file and reloaded automatically.</p>
<p>I also added the ability to start downloading from a particular friend, to allow incremental additions/updates. The first time it’s run, it’s certainly worth starting from the beginning, of course, but you may want to restart from a friend further down the list a later point.</p>
<p>When you select a friend on the left, the “Start Download from Selected” button should come alive. Pushing it does what’s written on the box:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017744ae6a93970d-pi" target="_blank"><img alt="The photos being downloaded" border="0" height="420" src="/assets/image_140220.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="The photos being downloaded" width="354" /></a></p>
<p>Once completed, you’ll have a folder full of photos of you and your friends. The folder structure contains one level of indirection to use the user ID – as friends can very well have the same name (there’s a teenager named Kean Walmsley who lives in Canada and friended me on Facebook some time ago, for instance) – and inside that folder you’ll find a collection of images:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3bff24c3970c-pi" target="_blank"><img alt="My photos from Facebook on my local drive" border="0" height="409" src="/assets/image_790836.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="My photos from Facebook on my local drive" width="404" /></a></p>
<p>Looking closely, above, you’ll see there are both .jpg and .pgm images. The .jpg files are those downloaded directly from Facebook – nothing surprising there. The .pgm files are created by the .NET application using following algorithm:</p>
<ul>
<li>For each tagged photo of a user      
<ul>
<li>Use OpenCV face detection to find the faces in the image          
<ul>
<li>For this I used <a href="http://www.emgu.com" target="_blank">Emgu CV</a> – a .NET wrapper for OpenCV – along with <a href="http://opencv.willowgarage.com/wiki/FaceDetection/" target="_blank">OpenCV’s face detection feature that looks for haar-like features</a> </li>
</ul>
</li>
<li>Compare the tag location with the faces returned. For the best match…          
<ul>
<li>Take a greyscale copy of the cropped area </li>
<li>Resize it to 50 x 50 </li>
<li>Equalize the histogram to get consistent brightness and contrast </li>
<li>Save that file to the .pgm format </li>
</ul>
</li>
</ul>
</li>
</ul>
<p>Regarding the quality of the photos downloaded from Facebook: frankly, it varies. Some photos are poorly tagged, and some are tagged well enough but the face cannot easily be extracted (which sometimes means someone else’s gets picked up in its place). And then some photos simply aren’t well suited to face detection/extraction, so the results end up being just plain strange.</p>
<p>I’ve found that some kind of manual scrubbing process between the download and the script creation/database training helps the quality of the data a great deal. I’ve used a tool called <a href="http://www.irfanview.net" target="_blank">IrfanView</a> to do this: you can search a set of sub-folders for *.pgm files and transfer the results across into “Thumbnail View”, and can then proceed to delete the ugly pics from there. Of course you could do the same thing in Explorer if you had a shell extension that can preview .pgm files, but I wasn’t able to find one. And IrfanView seems to work pretty well.</p>
<p>Here’s a view of some shots of Scott Sheppard and Shaan Hurley, for instance:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3bff24f6970c-pi" target="_blank"><img alt="Scrubbing Scott and Shaan&#39;s Facebook photos" border="0" height="346" src="/assets/image_487566.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Scrubbing Scott and Shaan&#39;s Facebook photos" width="464" /></a></p>
<p>In time I expect a bespoke tool – one that remembers the pics you’ve deleted before, to save you having to repeat that task each time – is the way to go. But that’s for another time.</p>
<p>Once this is done, you should have a fairly clean set of normalised (i.e. of the same size and with similar brightness/contrast) files that can be used to generate a training script for the OnlineFaceRec tool.</p>
<p>The second button on our main dialog runs some code that copies the .pgm files to a separate folder and creates a training script listing them all:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> btnCreateScript_Click(</span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span><span style="line-height: 140%; color: #2b91af;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> root = </span><span style="line-height: 140%; color: #a31515;">&quot;Z:\\fb_photos\\&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> destRoot = </span><span style="line-height: 140%; color: #a31515;">&quot;Z:\\rp_photos\\&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #2b91af;">SortedList</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;">&gt; checkedFriends =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">InfoDialog</span><span style="line-height: 140%;">.LoadCheckedFriends();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Get the list of PGM files in our source directory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #2b91af;">DirectoryInfo</span><span style="line-height: 140%;"> di = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DirectoryInfo</span><span style="line-height: 140%;">(root);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> files = di.GetFiles(</span><span style="line-height: 140%; color: #a31515;">&quot;*.pgm&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #2b91af;">SearchOption</span><span style="line-height: 140%;">.AllDirectories);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #2b91af;">StringBuilder</span><span style="line-height: 140%;"> sb = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">StringBuilder</span><span style="line-height: 140%;">(),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cur = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">StringBuilder</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// We&#39;ll maintain an index for the persons and an</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// old user ID, so we can tell when we have changed</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// to a new person&#39;s data</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> person = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> ouid = </span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Maintain a counter of the number of photos for</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// the current person (we only write out 2+ photos,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// as otherwise they don&#39;t train the database),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// as well as a list of their paths</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> photosForCurrent = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">&gt; filesForCurrent = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">&gt;();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">FileInfo</span><span style="line-height: 140%;"> fi </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> files)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> path = fi.DirectoryName;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> info = path.Split(</span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;">[] { </span><span style="line-height: 140%; color: #a31515;">&#39;\\&#39;</span><span style="line-height: 140%;"> });</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (info.Length == 4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> name = info[2], uid = info[3];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ouid != uid)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We have a new user</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ouid = uid;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If the last user had more than 2 photos...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (photosForCurrent &gt; 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Let&#39;s add them to the training script and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// copy the files to the destination</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; person++;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sb.Append(cur.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> file </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> filesForCurrent)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> destFile =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; destRoot +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Utils</span><span style="line-height: 140%;">.RemoveAccents(file.Substring(root.Length));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Directory</span><span style="line-height: 140%;">.CreateDirectory(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Path</span><span style="line-height: 140%;">.GetDirectoryName(destFile)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Copy(file, destFile, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Reset the string, photos and image file info</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// for the current user</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cur.Clear();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; photosForCurrent = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; filesForCurrent.Clear();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> id = </span><span style="line-height: 140%; color: #2b91af;">Int64</span><span style="line-height: 140%;">.Parse(uid);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (checkedFriends.ContainsKey(id))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Add the information on the current photo to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the current user&#39;s string</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cur.Append(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;">.Format(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;{0} {1} {2}\r\n&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; person, </span><span style="line-height: 140%; color: #2b91af;">Utils</span><span style="line-height: 140%;">.RemoveAccents(name),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;./&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Utils</span><span style="line-height: 140%;">.RemoveAccents(fi.FullName).</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Substring(root.Length).Replace(</span><span style="line-height: 140%; color: #a31515;">&#39;\\&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;/&#39;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Do the same for the photos to copy across to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the destination</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; photosForCurrent++;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; filesForCurrent.Add(fi.FullName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Don&#39;t forget to tidy up and copy the last user&#39;s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// training string and files across</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; sb.Append(cur.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> file </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> filesForCurrent)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> destFile =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; destRoot + file.Substring(root.Length);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Directory</span><span style="line-height: 140%;">.CreateDirectory(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Path</span><span style="line-height: 140%;">.GetDirectoryName(destFile)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Copy(file, destFile, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Finally we write out the string to the training file</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.WriteAllText(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; destRoot + </span><span style="line-height: 140%; color: #a31515;">&quot;training.txt&quot;</span><span style="line-height: 140%;">, sb.ToString()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>In case you’re wondering, I’ve kept the two main pieces of the process separate primarily so I can combine my wife’s friends with my own before training the database. :-)</p>
<p>Now it’s a simple matter of running OnlineFaceRec with the training script:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017744ae6af1970d-pi" target="_blank"><img alt="Calling OnlineFaceRec with the trainng script" border="0" height="264" src="/assets/image_193465.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Calling OnlineFaceRec with the trainng script" width="454" /></a></p>
<p>The output of this tool is pretty interesting: aside from the very important <em>facedata.xml</em> file (the database we’ll use on the Raspberry Pi), we also two files that are created mainly to demonstrate the principle at work.</p>
<p>Firstly we have the average face – which is the absolute average of all the pictures of your friends, and tends to be a bland, slightly androgynous and some might say idealised image of a human face:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d3bff2526970c-pi" target="_blank"><img alt="out_averageImage.bmp" border="0" height="61" src="/assets/image_956027.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="out_averageImage.bmp" width="61" /></a></p>
<p>And seondly we have an image of the Eigenfaces themselves, which map the differences from this average image:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c31d0a4c9970b-pi" target="_blank"><img alt="out_eigenfaces.bmp" border="0" height="2322" src="/assets/image_902153.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="out_eigenfaces.bmp" width="285" /></a></p>
<p>Neither of these images needs to be transferred across to the Raspberry Pi – their data is captured in the XML database (which stands at around 28 MB for the selected subset of my friends).</p>
<p>In the next post, we’ll look at the implementation of a component that makes use of this XML data to perform facial recognition on images captured by our motion detector.</p>
