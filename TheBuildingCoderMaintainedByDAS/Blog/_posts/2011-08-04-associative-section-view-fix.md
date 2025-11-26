---
layout: "post"
title: "Associative Section View Fix and Greece"
date: "2011-08-04 04:00:00"
author: "Jeremy Tammik"
categories:
  - "SDK Samples"
  - "Training"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/08/associative-section-view-fix.html "
typepad_basename: "associative-section-view-fix"
typepad_status: "Publish"
---

<p>Here I am back from a very pleasant break.

<h4>Break in Greece</h4>

<p>As mentioned, I gave a Revit API training in Athens last week.
I was able to extend my stay to visit the 

<a href="http://en.wikipedia.org/wiki/Acropolis_of_Athens">Acropolis</a> and 

spend a wonderful extended weekend on the beaches of 

<a href="http://en.wikipedia.org/wiki/Limni,_Euboea">
Limni</a> or 

<a href="http://en.wikipedia.org/wiki/Limni,_Euboea">
&Lambda;&iota;&mu;&nu;&iota;</a>, on the island of 

<a href="http://en.wikipedia.org/wiki/Euboea">
Euboea</a>.

Very impressive, how the ancient Greeks built things that last for thousands of years, even though some renovation is required...</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154343f28f9970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330154343f28f9970c image-full" alt="Acropolis renovation" title="Acropolis renovation" src="/assets/image_cc0a87.jpg" border="0" /></a> <br />

</center>

<h4>Mastic Liqueur and Masticate</h4>

<p>One interesting thing that I learned and was able to explore through first-hand experience during the training is the etymology of the English word 

<a href="http://en.wiktionary.org/wiki/masticate">masticate</a>.

I went out for a meal with my mainly Lebanese training participants, so we were speaking English and they spoke Arabic in this Greek environment.
We had fish, salad,

<a href="http://en.wikipedia.org/wiki/Tzatziki">
tzatsiki</a>, 

water and 

<a href="http://en.wikipedia.org/wiki/Ouzo">
ouzo</a> (&tau;&zeta;&alpha;&&tau;&zeta;&iota;&kappa;&iota;, &omicron;&upsilon;&zeta;&omicron;)

all very typical Greek, or rather Eastern Mediterranean.

For dessert, as a present from the Egyptian chef, came a 

<a href="http://en.wikipedia.org/wiki/Mastika">
mastica liqueur</a>,

&mu;&alpha;&sigma;&tau;&iota;&chi;&alpha;.

This is produced from 

<a href="http://en.wiktionary.org/wiki/mastic">
mastic</a>,

the resin of the 

<a href="http://en.wiktionary.org/w/index.php?title=Pistacia_lentiscus&action=edit&redlink=1">
Pistacia lentiscus</a> shrub native to the Mediterranean.

The really interesting point for me was that the same word 'mastic' is used in Arabic as well.
I was surprised.

<a name="2"></a>

<h4>Dynamic Model Update Associative Section View SDK Sample</h4>

<p>The training was unconventional, since these guys were all unusually experienced programmers, so I never needed to explain anything, just point out briefly how things works and they would immediately say 'ok, fine; next, please'.
Mostly, in my trainings, I would like to go faster, and spend much too much time explaining .NET programming basics instead of the Revit API.
This time, as soon as I got into any .NET related details, they got impatient and urged me to get on with the next topic.
Great fun!

<p>One thing we ended up looking at was the DynamicModelUpdate sample, which demonstrates an example of making use of the DMU or Dynamic Model Update framework.
The version in the SDK does not work out of the box right now due to a mismatch between the namespace prefix of the class implementing the external application loaded by Revit.
The add-in manifest file specifies the full class name 'Revit.SDK.Samples.DynamicModelUpdate.CS.AssociativeSectionUpdater'.

<p>The two source files Application.cs and SectionUpdater.cs define the namespace 'DynamicModelUpdate' instead.

<p>To fix the problem, simply replace the line 

<pre class="code">
namespace DynamicModelUpdate
</pre>

<p>The correct specification in both files should be 

<pre class="code">
namespace Revit.SDK.Samples.DynamicModelUpdate.CS
</pre>

<p>Once that is done, the sample works fine.

<h4>More Pictures of Beaches</h4>

<p>To return to the unbelievably unpopulated beaches of Limni, obviously mainly due to the current economic crisis, here are some final pictures; here is Limni itself and its beach:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154343f271c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330154343f271c970c image-full" alt="Limni and its beach" title="Limni and its beach" src="/assets/image_c0ab13.jpg" border="0" /></a> <br />

</center>

<p>A pier and beach all to ourselves:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330154343f265d970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330154343f265d970c image-full" alt="A pier and beach all to ourselves" title="A pier and beach all to ourselves" src="/assets/image_92a40c.jpg" border="0" /></a> <br />

</center>

<p>Totally clear water:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330153906bd728970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330153906bd728970b image-full" alt="Totally clear water" title="Totally clear water" src="/assets/image_c3db00.jpg" border="0" /></a> <br />

</center>

<p>Another nice lonesome beach to sleep on:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8a5f139d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8a5f139d970d image-full" alt="Another nice lonesome beach to sleep on" title="Another nice lonesome beach to sleep on" src="/assets/image_e5862f.jpg" border="0" /></a> <br />

</center>

<p>Here are <a href="http://www.facebook.com/media/set/?set=a.2162481814488.122318.1019863650&l=7167da5cad&type=1">yet more pictures</a>.

<p>A wonderful place to be, I must say.
I really am overwhelmed by the beauty and friendliness of Greece!
