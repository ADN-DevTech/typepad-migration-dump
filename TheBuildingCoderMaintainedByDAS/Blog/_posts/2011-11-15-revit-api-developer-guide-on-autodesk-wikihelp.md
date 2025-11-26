---
layout: "post"
title: "Revit API Developer Guide on Autodesk WikiHelp"
date: "2011-11-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Getting Started"
  - "SDK Samples"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/11/revit-api-developer-guide-on-autodesk-wikihelp.html "
typepad_basename: "revit-api-developer-guide-on-autodesk-wikihelp"
typepad_status: "Publish"
---

<p>Step by step, online help is ever more prevalent.

<!--

First the Revit 2010 product was provided with 

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/online-revit-help.html">
online help</a>.

<p>Rod Howarth did what he could at the time using a custom Google search to provide an entry point into all available 

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/online-revit-api-help.html">
Revit API help material</a> including 

some blogs, AUGI and the Autodesk discussion forums.

-->

A huge step forwarded was achieved with the Revit 2012 product 

<a href="http://thebuildingcoder.typepad.com/blog/2011/04/revit-2012-wikihelp-overview.html">
WikiHelp system</a> with 

its support for community building and contribution, and the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/05/wiki-api-help-view-event-and-structural-material-type.html#1">
Revit API documentation</a> soon followed suit.

<p>The Content Wrangler recently published an interesting 

<a href="http://thecontentwrangler.com/2011/10/25/in-search-of-operational-efficiency-a-discussion-with-victor-solano-autodesk">
article and interview</a> discussing the topic in more depth.

<p>Following up on this, here is some more detailed information and suggestions specifically targeted at the Revit API help by Mikako Harada, ADN AEC Technical Lead:


<a name="1"></a>

<h4>Autodesk Revit API Developer Guide on Autodesk WikiHelp</h4>

<p>As some of you are already aware, we now have an Autodesk Revit API Developer Guide on Autodesk WikiHelp.  
You can access it from <a href="http://www.autodesk.com/revitapi-help">www.autodesk.com/revitapi-help</a>. 
This is the place where you will find the latest, up-to-date Revit developer guide. 
Of course, Wiki means this is the place where we all can contribute and become a part of the community.  

<p>To get you motivated to look at our Revit API Developer Guide on WikiHelp, and to encourage you to start contributing, we have picked up the following two topics to share with you:

<ul>
<li><a href="#2">'My page'</a>
<li><a href="#3">Formatting Code Samples</a>
</ul>

<p>More to follow...

 

<a name="2"></a>

<h4>'My page' &ndash; It's For You</h4>

<p>Did you know that you can create content under your own page?  Every user has their own location in the WikiHelp, in which you can add pages and edit content. Here is how you can access it:

<ol>
<li>Go to <a href="http://wikihelp.autodesk.com">wikihelp.autodesk.com</a>
<li>Sign in using Autodesk login ID ('Sign in' button is at the top right):</li>

<br/>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015436e052b6970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015436e052b6970c image-full" alt="Wikihelp sign-in" title="Wikihelp sign-in" src="/assets/image_040d7a.jpg" border="0" /></a><br />

</center>

<br/>

<li>Go to your <user name> at the top right corner of the page, and click 'My page'. This will bring you to your page:</li>

<br/>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330162fc622eed970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330162fc622eed970d image-full" alt="My page" title="My page" src="/assets/image_bd7010.jpg" border="0" /></a><br />

</center>

<br/>

<li>Once you are in 'My page', you can Create a new page and Edit page using various edit functions:</li>

<br/>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015436e05570970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015436e05570970c image-full" alt="Create new page" title="Create new page" src="/assets/image_15e6a4.jpg" border="0" /></a><br />

</center>

</ol>

<p>The image below shows an example of how it looks like when you go to 'Edit page' &gt; 'Edit content' mode. You can easily figure out what each basic function does from icons.</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833015436e0567c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833015436e0567c970c image-full" alt="Editing" title="Editing" src="/assets/image_4fa2ab.jpg" border="0" /></a><br />

</center>

<p>As mentioned earlier, all users have the 'My page' location and it is intended as a public page. 
For example, on the landing page of the WikiHelp, you can click on any of the user names of the 'Top Contributors' to see their My page profile. 

<p>If you are not yet familiar with the WikiHelp environment, this is a good place to start playing with WikiHelp and learn how to write a page until you feel comfortable modifying the existing content. 



<a name="3"></a>

<h4>Tips for Formatting Code Samples</h4>


<p>If you are a programmer thinking about contributing to any of the Developer Guide section of WikiHelp, you will most likely find formatting in WikiHelp not as straightforward as you wish. 
Soon you will start wondering what the best way to add a code sample is. 
(In fact, that's what we struggled with when we started!) 

<p>Here is one suggestion that we want to share with you to make formatting code less painful:

<ol>
<li>Copy and Paste a code snippet to the Edit window. 
For example, you can copy and paste directly from your Visual Studio IDE to the Wiki page:</li>

<br/>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330153930cc1c0970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330153930cc1c0970b image-full" alt="Copy and paste code" title="Copy and paste code" src="/assets/image_040bd5.jpg" border="0" /></a><br />

</center>

<br/>

<li>Select the whole code area, and then set the [Formatting Styles] to 'Formatted'.</li>

<br/>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330153930cc29e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330153930cc29e970b image-full" alt="Formatted code" title="Formatted code" src="/assets/image_737789.jpg" border="0" /></a><br />

</center>

<br/>

<li>Place a cursor anywhere within the formatted text. Once a block of text is formatted, [Transformations] button becomes active.  ([Transformations] icon is rather shy looking. It's a tiny grayish icon at lower left corner of the tool bar area.) Choose the language of the code from the pull-down menu (i.e.,  'syntax.CSharp' for C# in our case. Choose 'syntax.Vb' for VB.NET):</li>

<br/>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330153930cc4d5970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330153930cc4d5970b image-full" alt="Select source code language" title="Select source code language" src="/assets/image_876a24.jpg" border="0" /></a><br />

</center>

<p>At this point, the transformation you set does not have any visual effect. 
You will see it only after you save it and exit the edit mode. 

<li>[Save] the modification. Your code should look like this after save:</li>

<br/>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330153930cc575970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330153930cc575970b image-full" alt="Formatted code" title="Formatted code" src="/assets/image_6fab93.jpg" border="0" /></a><br />

</center>

<p>You still lose the exact colour coding that you had in your Visual Studio IDE. 
But with the current functionality of WikiHelp editing, the above seems to be the easiest. 
We, including the Revit API team, have decided to stick to following this way from now on.

</ol> 

<p>We look forward to seeing your contribution in our Revit 'API Dev Guide' section very soon.

<p>Acknowledgement: many thanks to Elizabeth Shulok of Structural Integrators, LL C, ADN member, for her valuable feedback about editing in WikiHelp, and many thanks to Mikako for this helpful summary!
