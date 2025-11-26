---
layout: "post"
title: "Migrating the ADN Training Labs to Revit 2014"
date: "2013-06-08 04:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Getting Started"
  - "Migration"
  - "SDK Samples"
  - "Training"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/06/migrating-the-adn-training-labs-to-revit-2014.html "
typepad_basename: "migrating-the-adn-training-labs-to-revit-2014"
typepad_status: "Publish"
---

<a class="asset-img-link"  style="float: right;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103141a96970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103141a96970c" alt="Gopinath Taget" title="Gopinath Taget" src="/assets/image_b96a89.jpg" border="0" style="margin: 0px 0px 5px 5px;" /></a>

<p>My colleague

<a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">
Gopinath Taget</a> has

started supporting the Revit API as well, and we are very happy he is joining our workgroup.</p>

<p>He migrated the ADN Training Labs to Revit 2014, and I cleaned up and documented the results a little.

<p>Just like for the

<a href="http://thebuildingcoder.typepad.com/blog/2013/04/migrating-the-building-coder-samples-to-revit-2014.html">
migration of The Building Coder samples</a>,

the changes are quite small this time around and the basic procedure is the same, so you should refer back to that post to see how to update the Revit API assembly references and other basic stuff.</p>

<p>Here is a snapshot of the error list once the code already was in a compilable state, which at this point of time reports

<span class="asset  asset-generic at-xid-6a00e553e16897883301910313e839970c"><a href="http://thebuildingcoder.typepad.com/files/adn_migr_2014_a.txt">zero errors and 157 warnings</a></span> about

deprecated API usage.</p>

<p>These are the two main culprits in our code:</p>

<ul>
<li>In VB the Curve.EndPoint property is obsolete; in C#, it is represented as Curve.get_EndPoint; use Curve.GetEndPoint instead, for both.</li>
<li>The NewLineBound method is obsolete; use Line.CreateBound instead.</li>
</ul>

<p>Some others are:</p>

<ul>
<li>Document.Delete(Element) is obsolete; use Delete(ElementId) instead.</li>
<li>Element.Level is obsolete; use Element.LevelId instead.</li>
</ul>

<p>Removing occurrences of these main offenders reduced the error list to

<span class="asset  asset-generic at-xid-6a00e553e1689788330192aadc2a97970d"><a href="http://thebuildingcoder.typepad.com/files/adn_migr_2014_b.txt">zero errors and 35 warnings</a></span>,

most of which are complaining about the processor architecture mismatch that I cannot do anything about:</p>

<ul>
<li>There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "RevitAPI, Version=2012.0.0.0, Culture=neutral, processorArchitecture=x86", "AMD64"...</li>
</ul>

<p>So I will leave it at this for the moment to provide you here with the very first

<span class="asset  asset-generic at-xid-6a00e553e16897883301910313e561970c"><a href="http://thebuildingcoder.typepad.com/files/adn_labs_2014_0.zip">version 2014.0.0.0</a></span> of

the ADN Training Labs for Revit 2014.

<p>If you would like to compare it with the

<a href="http://thebuildingcoder.typepad.com/blog/2013/02/adn-revit-api-training-material-update-and-vacation.html">
last version for Revit 2013</a>,

look at

<!-- /a/doc/revit/blog/zip/adn_src_2013-02-15.zip -->

<span class="asset  asset-generic at-xid-6a00e553e168978833017d4121fb8f970c"><a href="http://thebuildingcoder.typepad.com/files/adn_src_2013-02-15.zip">adn_src_2013-02-15.zip</a></span>,

containing the following solutions and projects:</p>

<ul>
<li>lab
<ul>
<li>1_Revit_API_Intro
</li><li>2_Revit_UI_API
</li><li>3_Revit_Family_API
</li><li>XtraCs
</li><li>XtraVb
</li>
</ul>

</li><li>rme
<ul>
<li>AdnRme
</li>
</ul>

</li><li>rst
<ul>
<li>labs
</li><li>link
</li><li>NewRstLab
</li><li>RstAvfDmu
</li>
</ul>
</ul>

<p>Note that the Revit 2014 version presented above only includes the labs, and the Revit 2014 MEP and Structure lab migration is still pending.</p>
