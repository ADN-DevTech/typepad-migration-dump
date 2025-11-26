---
layout: "post"
title: "Parameter Modification Performance"
date: "2008-12-04 14:00:00"
author: "Jeremy Tammik"
categories:
  - "AU 2008"
  - "Parameters"
  - "Performance"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/12/parameter-modification-performance.html "
typepad_basename: "parameter-modification-performance"
typepad_status: "Publish"
---

<p>I just finished the third and final of my 

<a href="http://au.autodesk.com/sessions/?speaker=Jeremy+Tammik&year=2008">
presentations at AU 2008</a>

and had a great time doing so, with enthusiastic and friendly audiences. 
For a live impression of the atmosphere here, don't miss having a look at Kean's posts on his 

<a href="http://through-the-interface.typepad.com/through_the_interface/2008/12/the-calm-after.html">
arrival</a>,

<a href="http://through-the-interface.typepad.com/through_the_interface/2008/12/first-day-of-au.html">
first day</a>, 

and 

<a href="http://through-the-interface.typepad.com/through_the_interface/2008/12/in-the-exhibit.html">
the exhibit hall</a>

at AU.</p>

<p>Meanwhile I noticed the following interesting issue on parameter modification performance raised by Michael Raps of the

<a href="www.i4Ds.ch">Institut 4D-Technologies and DataSpaces</a>

at the

<a href="www.fhnw.ch">Fachhochschule Nordwestschweiz</a>

and being explored by Saikat Bhattacharya of our Bangalore office.</p>

<p>Modifying multiple parameters sometimes takes an inordinate amount of time. I noticed something similar myself once running a command which modified parameters on HVAC air terminals. Executing this command in Revit Architecture worked fine and fast, but it took an order of magnitude longer to execute in Revit MEP, because Revit MEP was continuously recalculating and updating the entire duct system every time one of the parameters changed.</p>

<p>Michael's issue is simpler and does not involve any complex regeneration of the entire system by Revit. The problem was that executing the following simple code in a model using a specific custom family took extremely long:</p>

<pre class="code">
<span class="green">// Start measurement</span>
<span class="blue">int</span> millis = <span class="teal">Environment</span>.TickCount;
&nbsp;
<span class="green">// Set some values</span>
<span class="teal">Parameter</span> p = instance.get_Parameter(<span class="maroon">"Breite"</span>);
p.Set(500);
&nbsp;
<span class="green">// Set some Booleans</span>
p = instance.get_Parameter(<span class="maroon">"F&#252;sse"</span>);
p.Set(1);
&nbsp;
p = instance.get_Parameter(<span class="maroon">"Sockel"</span>);
p.Set(1);
&nbsp;
p = instance.get_Parameter(<span class="maroon">"Rille"</span>);
p.Set(1);
&nbsp;
<span class="green">// Set some strings</span>
p = instance.get_Parameter(<span class="maroon">"Bezeichnung Deutsch"</span>);
p.Set(<span class="maroon">"Deutsche Bezeichnung"</span>);
&nbsp;
p = instance.get_Parameter(<span class="maroon">"Bezeichnung Englisch"</span>);
p.Set(<span class="maroon">"English description"</span>);
&nbsp;
p = instance.get_Parameter(<span class="maroon">"Comments"</span>);
p.Set(<span class="maroon">"A comment"</span>);
&nbsp;
millis = <span class="teal">Environment</span>.TickCount - millis;
<span class="teal">MessageBox</span>.Show( 
&nbsp; millis + <span class="maroon">"Millis to set 9 parameters. "</span> +
&nbsp; <span class="maroon">" (about "</span> + millis / 9.0 + <span class="maroon">" millis per param)"</span> );
</pre>

<p>Michael made use of the profiling functionality provided by the 

<a href="http://www.jetbrains.com/profiler">
dotTrace profiler for .NET</a>

to create the following execution time analysis:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301053636a7cc970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e16897883301053636a7cc970c image-full" alt="dotTrace execution time profiling" title="dotTrace execution time profiling" src="/assets/image_52fee9.jpg" border="0"  /></a>

<p>One issue involved here is a known behaviour with parameter bindings and was fixed in the web update version of Revit 2009, so the first step is to ensure that you have the

<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=11017599">latest build</a>.

Even after this update, the performance was still pretty slow:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330105362ede7e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330105362ede7e970b image-full" alt="dotTrace execution time profiling" title="dotTrace execution time profiling" src="/assets/image_8fe4b5.jpg" border="0"  /></a>

<p>Saikat then discovered that
replacing the custom family by a simpler RFA file such as a rectangular column family caused it to work like a flash. Thus, the delay is partially due to the dataset being used, in this case the family file, and not entirely caused by the Parameter.Set() API call. This is one detail to keep in mind when you run into performance issues setting parameters.</p>
