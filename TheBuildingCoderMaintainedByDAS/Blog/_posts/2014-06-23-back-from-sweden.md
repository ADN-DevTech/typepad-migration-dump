---
layout: "post"
title: "Back from Sweden"
date: "2014-06-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Geometry"
  - "Photo"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/06/back-from-sweden.html "
typepad_basename: "back-from-sweden"
typepad_status: "Publish"
---

<p>I returned from my wonderful relaxing outdoor vacation in the vast nature of Sweden and had numerous simple Revit API questions waiting for me on my return, e.g.</p>

<ul>
<li><a href="#2">Annotation Location property</a></li>
<li><a href="#3">Placing ElementType instances</a></li>
<li><a href="#4">Commercial use of the Revit API</a></li>
</ul>

<p>Before looking at those, here is an impression of the huge open space offered by heaven and earth in the islands in the Stockholm

<a href="http://en.wikipedia.org/wiki/Stockholm_archipelago">
Skärgården</a> archipelago,

e.g.

<a href="http://en.wikipedia.org/wiki/Ut%C3%B6,_Sweden">
Utö</a> and

Ålö:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73dde7296970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73dde7296970d image-full img-responsive" alt="Ålö strand" title="Ålö strand" src="/assets/image_1a3b9e.jpg" border="0" /></a><br />

</center>

<p>For something more tranquil, here is a sunset meditation on some reeds in the inland lake

<a href="">
Kvarnsjön</a>:

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fd23a48f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fd23a48f970b image-full img-responsive" alt="Reeds in Kvarnsjön" title="Reeds in Kvarnsjön" src="/assets/image_100c1a.jpg" border="0" /></a><br />

</center>

<p>Here they are in context:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511d326e7970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511d326e7970c image-full img-responsive" alt="Reeds in Kvarnsjön" title="Reeds in Kvarnsjön" src="/assets/image_48b0c1.jpg" border="0" /></a><br />

</center>

<p>Here is an

<a href="https://www.facebook.com/media/set/?set=a.10203275797548613.1073741831.1019863650">
album</a> with more pictures.</p>

<p>Back to the Revit API...</p>


<a name="2"></a>

<h4>Annotation Location Property</h4>

<p><strong>Question:</strong> I’ve been following your website for a few weeks now, Great Stuff!</p>

<p>It’s been really helpful in figuring out Dynamo and how to fill in the gaps with Python components.</p>

<p>I’ve run into an issue that I haven’t seen documented anywhere online and wondered if you have any thoughts (maybe your next blog topic).</p>

<p>I’m trying to set up a dynamo script to replace updated images.</p>

<p>I can’t find anything related to the location of an annotation element in a view.</p>

<p>Obviously it doesn’t have a world XYZ, but somehow Revit is tracking where it is located within the view/sheet because it does have a location parameter.</p>

<p>I just can’t find a way to extract it.</p>

<p><strong>Answer:</strong> Thank you for the appreciation!
I am glad you find it useful.</p>

<p>Every Revit element has a Location property.</p>

<p>You retrieve it and cast it to either a LocationPoint or a LocationCurve instance.</p>

<p>If the cast fails, i.e. returns null, or if the Location property is null to start with, there is no API access to this information.</p>

<p>You can explore the contents of the Location property interactively using RevitLookup or other

<a href="http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html">
more powerful scripting</a>  approaches.</p>





<a name="3"></a>

<h4>Placing ElementType Instances</h4>

<p><strong>Question:</strong> I am trying to place new instances of an ElementType via the API.</p>

<p>I can right-click on the type in the Project Browser and select ‘Create Instance’, but need to launch this from the API.</p>

<p>I know I’ve seen in your blog the idea of using shortcut keys like ‘WA’ to begin placing a wall, but was wondering if there had been any new developments allowing directly placing new instances of an ElementType directly instead of having to set it as the default type and launching the wall command via the aforementioned shortcut.</p>

<p><strong>Answer:</strong> You can place instances in a model using the

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.25">
NewFamilyInstance</a> method.</p>

<p>This has been possible for ages.</p>

<p>You can also programmatically prompt the user to place an instance, where your add-in triggers the placement and asks the user to select the location interactively using the UIDocument.PromptForFamilyInstancePlacement method.</p>

<p>Finally, yes, there have indeed been new developments in this area in Revit 2015 API, e.g. the introduction of the default type API and the new PostRequestForElementTypePlacement method:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#3.02">Default Type API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#4.02">Document API additions</a> &gt; UIDocument operations and additions</li>
</ul>

<p>These enhancements improve the usability of both the fully automated and the interactive placement methods.</p>




<a name="4"></a>

<h4>Commercial Use of the Revit API</h4>

<p><strong>Question:</strong> We are building a Revit plugin that exports the model to another software.</p>

<p>Is any license required to use it for commercial purposes?</p>


<p><strong>Answer:</strong> Of course you may use the Revit API to create custom applications, both for your own use and for commercial purposes.</p>

<p>There is no need for any additional license.</p>

<p>The user of your add-in simply needs to have a standard Revit license to run it.</p>
