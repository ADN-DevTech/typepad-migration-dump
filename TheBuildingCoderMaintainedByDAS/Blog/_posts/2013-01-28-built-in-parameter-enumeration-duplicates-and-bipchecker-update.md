---
layout: "post"
title: "Duplicate Built-in Parameter Values and BipChecker Update"
date: "2013-01-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Algorithm"
  - "Data Access"
  - "Parameters"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/01/built-in-parameter-enumeration-duplicates-and-bipchecker-update.html "
typepad_basename: "built-in-parameter-enumeration-duplicates-and-bipchecker-update"
typepad_status: "Publish"
---

<p>BipChecker is a tool that I would recommend every Revit developer to install, just like RevitLookup.
It simply lists the values of all parameter data stored on a selected element.</p>

<p>Similar access is also offered within RevitLookup by the option Parameters &gt; Built-in Enums Snoop...
BipChecker offers much more functionality, though, and is a really powerful Revit database exploration and debugging tool.</p>

<p>The built-in parameter checker has been part of the ADN sample labs for ages now, and the original version is still provided in the

<a href="http://thebuildingcoder.typepad.com/blog/2012/04/xtra-adn-revit-2013-api-training-labs.html">
Xtra materials</a>.

The time has come to finally remove it from there, though.

<p>First, since I find it useful on its own, I extracted it into a separate add-in and baptised it

<a href="http://thebuildingcoder.typepad.com/blog/2011/09/unofficial-parameters-and-bipchecker.html">
BipChecker</a>.</p>

<p>Secondly,

<a href="http://www.facebook.com/profile.php?id=100003616852588">
Victor Chekalin</a> enhanced

it by implementing grouping of the long list by parameter group, data type, built-in versus standard Parameters collection, read-write, or none.</p>

<p>This tool always suffered one serious problem, though, due to repetitions in the definitions and assignment of some of the built-in parameter enumeration integer values.</p>

<p>Victor now tracked down and fixed that issue as well.</p>

<p>Here is his explanation:</p>

<p>Now I work closely with Materials and use our BiP checker.
I noticed two strange things when I check material parameters using BiP Checker.
The first one, the material cost stored as BuiltInParameter.DOOR_COST.
The second one &ndash; some parameters appear twice or even triple:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3654b54a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3654b54a970b image-full" alt="BipChecker duplicate and missing values" title="BipChecker duplicate and missing values" src="/assets/image_a79b98.jpg" border="0" /></a><br />

</center>

<p>I researched this and found the reason.</p>

<p>To iterate all built-in parameters, we used the Enum.GetValues method:</p>

<pre class="code">
  var allParameters = Enum.GetValues(
    typeof ( BuiltInParameter ) );
</pre>

<p>This makes use of the integer values of the Enum.
If different Enum values have the same integer value the GetValues method doesn’t work properly.

<p>For example, BuiltInParameter.ALL_MODEL_COST and BuiltInParameter.DOOR_COST both have the integer value -1001205:</p>

<ul>
<li>DOOR_FIRE_RATING = -1001206
<li>ALL_MODEL_COST = -1001205
<li>DOOR_COST = -1001205
<li>ALL_MODEL_MARK = -1001203
</ul>

<p>In this case, BiPChecker shows DOOR_COST twice and doesn’t show ALL_MODEL_COST parameter at all.


<a name="2"></a>

<h4>The Solution</h4>

<p>Use Enum.GetValues and Enum.TryParse instead of Enum.GetValues to get the BuiltInParameter value, and use the BuiltInParameter name from Enum.GetValues.
This requires the .NET framework 4.

<p>Now it looks much better:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c3654b7fc970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c3654b7fc970b image-full" alt="BipChecker corrected values" title="BipChecker corrected values" src="/assets/image_135a24.jpg" border="0" /></a><br />

</center>

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e168978833017c3654cd11970b"><a href="http://thebuildingcoder.typepad.com/files/bipchecker05.zip">BipChecker05.zip</a></span> containing

the full source code, Visual Studio solution and add-in manifest of the updated BipChecker version 2013.0.5.0.</p>

<p>С уважением from Чекалин Виктор.</p>

<p>Many thanks to Victor for his research and important enhancement!</p>

<p>To wrap it up, here is a snapshot listing all built-in parameters on a wall with no grouping enabled, so that the results can be sorted across all groups by any desired criterion:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d4083bebe970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d4083bebe970c image-full" alt="BipChecker on my old XP machine running in Parallels on the Mac" title="BipChecker on my old XP machine running in Parallels on the Mac" src="/assets/image_9c9af4.jpg" border="0" /></a><br />

</center>
