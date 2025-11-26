---
layout: "post"
title: "Point Cloud Unit Conversion"
date: "2012-01-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Climbing"
  - "Cloud"
  - "Element Creation"
  - "External"
  - "Geometry"
  - "Units"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/01/point-cloud-unit-conversion.html "
typepad_basename: "point-cloud-unit-conversion"
typepad_status: "Publish"
---

<p>Happy New Year of the Dragon!</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330168e5e3e56b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330168e5e3e56b970c image-full" alt="Happy New Year of the Dragon" title="Happy New Year of the Dragon" src="/assets/image_28c0d7.jpg" border="0" /></a><br />

</center>

<p>Top: Happy New Year!

<p>Bottom: Lucky Peace!

<p>The Chinese dragon is the symbol of emperor and power.  

<p>Thanks to Joe Ye and <a href="http://www.nipic.com">nipic.com</a> for the image and translation!

<p>Besides programming Revit and climbing mountains, I also like climbing trees:</p>

<iframe width="480" height="274" src="http://www.youtube.com/embed/gbs4sb-wcJo" frameborder="0" 

allowfullscreen></iframe>

<p>Returning to the Revit API, the topic of 

<a href="http://thebuildingcoder.typepad.com/blog/units">
units</a> is

a recurring theme, and here it is again rearing its beautiful head in the context of point cloud data access:

<p><strong>Question:</strong> I am working on a point cloud engine implementation for Revit. 
In the IPointCloudAccess interface, the GetUnitsToFeetConversionFactor method seems to define the factor between the point cloud units and feet. 
It is not working properly for me, though. 
The only way I am able to use it so far is to convert all my points into feet myself and have this method return a constant factor of 1.0.

<p>My point clouds always have a unit of measure, which is currently defined in meters, so an 
internal data item of 1 means 1 meter. 
If the Revit model's internal data also uses a fixed built-in length unit and not the unit preference to visible in the user interface, then the conversion between the point cloud data and the Revit model is fixed. 
Otherwise, if the Revit model's internal data is unitless (like AutoCAD DWG), the customer has to determine what unit is to be used to map the points into the model.

<p>In the Revit user interface, I experimented with creating a circle and saving it. 
When it is selected, it displays the radius in the current user interface unit preference. 
If I change the unit preference, it displays a new value, showing that the actual radius did not change. 
This makes me believe that the internal data has a unit of measure. 
What is it? 
Is there a difference between the US English and German versions of Revit?

<p><strong>Answer:</strong> The explorations you describe toward the end of your query are leading in the absolutely right direction, and the solution is very simple, but not obvious.

<p>Revit does indeed use a fixed internal database unit for all length measurements, and that unit is the imperial foot.
There is thus no difference between the different versions and languages of Revit; they all use the same internal database unit.
This fact is special enough to deserve a 

<a href="http://thebuildingcoder.typepad.com/blog/2011/03/internal-imperial-units.html">
separate discussion</a> of its own.

<p>A test very similar to the one you describe can easily be expanded to reliably determine the conversion factor between any given user interface unit and the internal Revit database one, as explained in the discussion of 

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/unit-conversion-and-new-blogs.html">
unit conversion</a> and

preceding posts.

You can also look at the entire collection of 

<a href="http://thebuildingcoder.typepad.com/blog/units">
unit topics</a>.

<p>Here is a suggestion on how to implement the IPointCloudAccess interface GetUnitsToFeetConversionFactor method, assuming the point cloud data is given in millimetres:

<pre class="code">
&nbsp; <span class="blue">class</span> <span class="teal">PointCloudAccess</span> : <span class="teal">IPointCloudAccess</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">const</span> <span class="blue">double</span> _mm_per_inch = 25.4;
&nbsp; &nbsp; <span class="blue">const</span> <span class="blue">double</span> _mm_per_foot = 12 * _mm_per_inch;
&nbsp; &nbsp; <span class="blue">const</span> <span class="blue">double</span> _mm_to_feet = 1.0 / _mm_per_foot;
&nbsp;
&nbsp; &nbsp; <span class="blue">public</span> <span class="blue">double</span> GetUnitsToFeetConversionFactor()
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> _mm_to_feet;
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="green">// . . .</span>
&nbsp;
&nbsp; }
</pre>
