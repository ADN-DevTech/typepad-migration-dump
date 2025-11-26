---
layout: "post"
title: "Setting the Compound Structure Core and Shell Layers"
date: "2013-08-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Climbing"
  - "Element Relationships"
  - "Geometry"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/08/setting-the-compound-structure-core-and-shell-layers.html "
typepad_basename: "setting-the-compound-structure-core-and-shell-layers"
typepad_status: "Publish"
---

<p>Modifying the compound layer structure of a wall or floor type is pretty well documented, and we already discussed the use of the SetCompoundStructure method to

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/updating-wall-compound-layer-structure.html">update the compound layer structure</a>.</p>

<p>However, how to define which layers are and are not part of the core is less obvious, which lead to the developer query below.</p>

<p>Before I get to that, here is a snapshot of the north-east ridge and summit of the

<a href="http://en.wikipedia.org/wiki/Spillgerte">Hinderi Spillgerte</a>

(<a href="http://de.wikipedia.org/wiki/Spillgerte">German</a>) that

I recently climbed:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019104a5b9d6970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019104a5b9d6970c image-full" alt="Hinderi Spillgerte NE ridge and summit" title="Hinderi Spillgerte NE ridge and summit" src="/assets/image_b9a1a1.jpg" border="0" /></a><br />

</center>

<p>The climb is easier than it looks, grade II-III+, includes a number of abseil sections, both ascending the north-east ridge and descending the south one, and became a bit risky at the end due to getting caught in a thunderstorm

(<a href="https://www.facebook.com/media/set/?set=a.10201093030740807.1073741828.1019863650&type=3">more photos</a>)...</p>

<p>Anyway, back safe and sound at our desks, let's return to setting the Revit core compound structure layers:</p>


<p><strong>Question:</strong> I found several methods in the CompoundStructure class to check the position of the core, e.g. GetCoreBoundaryLayerIndex, GetFirstCoreLayerIndex, GetLastCoreLayerIndex and IsCoreLayer, but no method to change it.</p>

<p>How can I achieve that, please?</p>


<p><strong>Answer:</strong> As mentioned above, you can change the compound layer structure a wall or floor type by creating a new collection of layers and setting them on the type using the

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/updating-wall-compound-layer-structure.html">
SetCompoundStructure method</a>.

<p>While this sets the layers, it does not define which of them are parts of the core.
In fact, the sample code uses the GetFirstCoreLayerIndex method to determine the first layer in the core, but presents no method to change it.</p>

<p>Defining which CompoundStructure layers are part of the core is done using the CompoundStructure.SetNumberOfShellLayers method.</p>

<p>For example, if you have 9 layers and want layers 0-3 in the exterior shell, 4-5 in the core, and 6-8 in the interior shell, you could use these two calls to achieve that:</p>

<pre class="code">
&nbsp; Cs.SetNumberOfShellLayers(
&nbsp; &nbsp; <span class="teal">ShellLayerType</span>.Exterior, 4 );
&nbsp;
&nbsp; Cs.SetNumberOfShellLayers(
&nbsp; &nbsp; <span class="teal">ShellLayerType</span>.Interior, 3 );
</pre>


<p><strong>Response:</strong> Thank you very much, that is just what I was looking for.</p>

<p>I modified my application and the two-line code example you provided works perfectly.</p>

<p>I had in fact seen the SetNumberOfShellLayers method, but its use was not obvious to me, nor the fact that the so-called 'shell' defines and changes the contents of the core.</p>
