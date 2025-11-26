---
layout: "post"
title: "The Revit 2014 Pre-release API and SDK"
date: "2013-04-02 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Fun"
  - "Migration"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/04/the-revit-2014-pre-release-api-and-sdk.html "
typepad_basename: "the-revit-2014-pre-release-api-and-sdk"
typepad_status: "Publish"
---

<p>Below, we look at how ADN members can obtain the Revit 2014 pre-release SDK, address migration issues, and one or two interesting new aspects of the API.</p>

<p>First however, a few words on yesterdays post touting the idea of a

<a href="http://thebuildingcoder.typepad.com/blog/2013/04/cloud-based-restaurant-seating-arrangement-and-cleaning.html">
cloud-based restaurant seating arrangement and cleaning</a> application.

As you probably noticed, the main new ideas I discussed there were in honour of

<a href="http://en.wikipedia.org/wiki/April_Fools%27_Day">April fool's Day</a>.


<a class="asset-img-link"  style="float: right;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c38483c56970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c38483c56970b" alt="P1000504_water_box" title="P1000504_water_box" src="/assets/image_4e43d9.jpg" style="margin: 0px 0px 5px 5px;" /></a>

<p>Many of my colleagues joined in with other similar hoax blog posts.
<a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a> presents an

<a href="http://adndevblog.typepad.com/autocad/2013/04/april-1st-1.html">April fool's Day blog post overview</a> listing

them all.</p>

<p>I hope you enjoy them.
They might even provide you with a useful real-world idea or two  :-)

<p>As always, think outside the box!</p>

<p>Back to more serious business, the Revit API, and the Revit 2014 pre-release.</p>


<a name="1"></a>

<h4>The Revit 2014 Pre-release SDK</h4>

<p>As you can guess, ADN member have already been working with Revit 2014 and its API to get their products ready for the new release.</p>

<p>Here are a couple of questions and interesting observations that came up and are still coming in:</p>

<ul>
<li><a href="#2">Where is the Revit 2014 SDK?</a></li>
<li><a href="#3">Do we need to rewrite our apps?</a></li>
<li><a href="#4">Add to an existing command</a></li>
<li><a href="#5">Full circle creation</a></li>
</ul>



<!-- 08227326 [Revit MEP 2014 SDK?] -->

<a name="2"></a>

<h4>Where is the Revit 2014 SDK?</h4>

<p><strong>Question:</strong> Is there an SDK available for Revit MEP 2014?

<p>The <a href="http://www.autodesk.com/developrevit">Revit Developer Center</a> still only lists the 2013 SDK...</p>


<p><strong>Answer:</strong> ADN members can obtain the Revit 2014 SDK from the ADN web site.
It is included in the preview version of Revit:

<ul>
<li><a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&id=472012">Autodesk Developer Network</a> &gt; Login</li>
<li>Downloads by Product  <a href="http://adn.autodesk.com/adn/servlet/index?siteID=4814862&id=5017413&linkID=4901650">Autodesk Revit</a></li>
<li>Autodesk Revit Architecture 2014 available in <a href="http://adn.autodesk.com/adn/servlet/item?siteID=4814862&id=21376501&linkID=4901650">English</a></li>
<li><a href="http://otwdownloads.autodesk.com/SWDLDNET3/2014/REVIT/DLM/Autodesk_Revit_Architecture_2014_English_Win_32-64bit_dlm.sfx.exe">Autodesk Revit Architecture 2014 English Win 32-64bit</a> (exe - 2.59 Gb)</li>
</ul>

<p>The installer extracts its content to a temporary installation folder, which by default is located in C:\Autodesk.

<p>Within this folder, you can navigate to the Utilities\SDK subfolder, which contains the installer:

<ul>
<li>C:\Autodesk
<li>&nbsp;\Autodesk_Revit_Architecture_2014_English_Win_32-64bit_dlm
<li>&nbsp;\Utilities
<li>&nbsp;\SDK
<li>&nbsp;&gt; 03/09/2013  12:27 AM  201,066,482 RevitSDK.exe
</ul>

<p>This executable extracts and installs the Revit SDK, which is identical for all flavours of Revit including MEP.

<p>Please note that this is a pre-release version of the SDK.
There definitely will be changes and additions made before the final release, so plan on updating it as soon as we get there.
As always, the final version will obviously be posted to the

<a href="http://www.autodesk.com/developrevit">Revit Developer Center</a> as

soon as possible.</p>



<a name="3"></a>

<h4>Do we need to rewrite our apps?</h4>

<p><strong>Question:</strong> Sorry if my question is stupid, I am not a programmer and it is hard for me by myself to get to the problem details.</p>

<p>Our programmer reports that our apps do not function under the Revit 2014.
I understand it happens because many methods were replaced with new calls.
Does it mean that we should rewrite our apps and replace old methods with new ones?
Is there a way to upgrade apps automatically?</p>


<p><strong>Answer:</strong> Thanks for posting this question.
Revit does not provide any tools to automatically upgrade lower version code to higher version.
However, Revit does provide plenty of information on how to upgrade the code yourself.
Here is the procedure:</p>

<ul>
<li>Open the lower version code in Visual Studio.</li>
<li>Add the Revit 2014 API references.</li>
<li>In the output window, each error includes a detailed instruction on how to change the code to the higher version.</li>
<li>Follow these steps one by one.</li>
<li>Compile the project.</li>
</ul>

<p>That is easy to follow.
Actually, the migration of an add-in from Revit 2013 to 2014 is much easier and faster than in the past few releases.

<p>For more information, please look at the numerous examples in The Building Coder

<a href="http://thebuildingcoder.typepad.com/blog/migration">migration</a> category.</p>

<p>Good luck!</p>



<a name="4"></a>

<h4>Add to an Existing Command</h4>

<p><strong>Question:</strong> I read your post about disabling commands through the API and it’s sort of along the lines of what I’m trying to do.
I’m looking to capture a specific CommandId ID_SETTINGS_REVISIONS, run it, and, when the command finishes, run a custom add-in.
I don’t want to replace the command, just add some custom background content at the end of it.</p>

<p>Is that possible?</p>

<p><strong>Answer:</strong> In 2013, all you can do is replace the existing command completely with your own implementation.</p>

<p>In 2014, you can also launch an existing Revit command.</p>

<p>Combining these two functionalities should enable what you wish.</p>


<a name="5"></a>

<h4>Full Circle Creation</h4>

<p>The Revit 2014 API enables the creation of a real full circle.
That was is not possible in Revit 2013.
There, we had to resort to creating two separate 180-degree arcs.</p>


<p>That was just a starter.
Lots more to come!</p>
