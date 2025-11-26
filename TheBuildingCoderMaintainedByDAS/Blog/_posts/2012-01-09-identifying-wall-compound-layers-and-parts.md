---
layout: "post"
title: "Identifying Wall Compound Layers and Parts"
date: "2012-01-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Data Access"
  - "Element Relationships"
  - "Geometry"
  - "Parts"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/01/identifying-wall-compound-layers-and-parts.html "
typepad_basename: "identifying-wall-compound-layers-and-parts"
typepad_status: "Publish"
---

<p>We recently looked at how to determine the exact geometry and wall 

<a href="http://thebuildingcoder.typepad.com/blog/2011/10/retrieving-detailed-wall-layer-geometry.html">
join points of compound layer geometry</a> subcomponents by temporarily creating separate parts from the wall. 

Here is another interesting question the correlation between the wall compound layers and its ensuing parts:

<p><strong>Question:</strong> I have split my wall into parts, and now I would like to determine which of the resulting parts correspond to the component structure layers with certain functions such as Structure or Membrane.

<p>Once the parts have been created, the CompoundStructureLayer information is apparently lost. As a workaround, I was thinking of creating a list of the compound structure layers before converting the wall to parts and storing their materials, width etc.
After the parts are created, I hoped to access each part, search for their material and width, and map these to the list I saved in order to know which part originally had which function.

<p>I am hoping that there is a better way to achieve this, since this approach has multiple limitations.

<p>As a simple example, let's assume we have a wall with two layers, layer 1 and layer 2. 
After creating parts from it, we have parts named 1 and 2. 
From part 1, I can access the wall that it was created from, and the wall contains all the original layers. 
So far, so good. 
Now the task here is to find which part corresponds to the layer whose function parameter is say Membrane before it was converted into parts. 
How do I know which part was created from which layer?
I was thinking of using material, width etc. to do the check, but if we multiple layers have the same material and width, and possibly even volume, how can I make this mapping foolproof?

Furthermore, some parts may be further subdivided into say horizontal parts, which will add another level of complexity to the problem.

<p><strong>Answer:</strong> It should be possible to find the location of any layer.

<ol>
<li>Remember that the wall location line is always the centre of the wall, regardless of the layer structure,
<li>Use CompoundStructure.GetOffsetForLocationLine to find other locations such as CoreBoundary, FinishBoundary, etc., and/or
<li>Use CompoundStructure.GetLayerWidth to walk the layers in order, and/or
<li>Use the FindReferencesByDirectionWithContext ray tracing method to find the wall parts in order.
</ol>

<p>For vertically compound structures, there is more work to be done, as you pointed out.  

<p>It is important to remember that zero width membrane layers do not generate parts.
