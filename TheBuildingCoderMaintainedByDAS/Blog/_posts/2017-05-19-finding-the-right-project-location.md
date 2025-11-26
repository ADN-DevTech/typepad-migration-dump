---
layout: "post"
title: "Finding the Right Project Location"
date: "2017-05-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Element Relationships"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/05/finding-the-right-project-location.html "
typepad_basename: "finding-the-right-project-location"
typepad_status: "Publish"
---

<p>Here is an explanation of the Revit project location elements and selecting the right one to use.</p>

<h4><a name="2"></a>Question</h4>

<p>The sample project 'rac_basic_sample_project.rvt' for Revit 2016 defines project location elements: 'Internal 21748' and 'Project 111428'.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2847916970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2847916970c image-full img-responsive" alt="Project locations" title="Project locations" src="/assets/image_d44481.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>The active Project Location is 'Internal 21748'.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d284791a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d284791a970c image-full img-responsive" alt="Active project location" title="Active project location" src="/assets/image_0dd17a.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>However, the values in 'Project 111428' are correct and can be used to transform the coordinates, whereas the values in 'Internal 21748' are nonsense.</p>

<p>That leads to the following questions:</p>

<ol>
<li>Why is the active project location 'Internal 21748' and not 'Project 111428'?</li>
<li>How can I determine which of the two to use when retrieving all project location elements, given that I cannot rely on the active project location?</li>
<li>What is the difference between these two? Why do they exist? Are there always exactly two project locations in a Revit project?</li>
</ol>

<!----
<center>
<img src="img/project_location.png" alt="Project locations" width="846">
</center>
---->

<h4><a name="3"></a>Answer</h4>

<p>In the case you describe, the <code>Document.ProjectLocations</code> property will return only one single <code>ProjectLocation</code> element &ndash; it will hide the location named "Project'.</p>

<p>I have to explain a little bit about how Revit works internally here. A normal Revit model has two SiteLocations &ndash; one tied to the shared coordinates, and one tied to the project coordinates. Each one starts with one <code>ProjectLocation</code>. Those are the two ProjectLocations you mention &ndash; 'Internal' for the shared coordinates, and 'Project' for the project coordinates.</p>

<p>The API does not expose the project ones directly, because they are confusing and not particularly helpful for normal use, as you've seen.</p>

<p>If you created more ProjectLocations via the Location/Weather/Site dialog on the Manage tab, they'd share the same SiteLocation as the Internal location.</p>

<p>As far as coordinates are concerned &ndash; assuming you have a ProjectLocation <code>projLoc</code> and you execute the following:</p>

<pre class="code">
<span style="color:#2b91af;">ProjectPosition</span>&nbsp;pos&nbsp;=&nbsp;projLoc.GetProjectPosition(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero&nbsp;);
<span style="color:#2b91af;">TaskDialog</span>.Show(&nbsp;<span style="color:#a31515;">&quot;Revit&quot;</span>,&nbsp;pos.EastWest&nbsp;);
</pre>

<p>This will tell you the relationship between the project coordinates and shared coordinates when that location is active. For example, in the little test I did to check my understanding, it says 18, and my project base point is 18 feet east of my survey point.</p>

<p>I don't know what the 'Project' ProjectLocation's coordinates mean in this circumstance, because it would be comparing to itself. Possibly it's a transform between Revit's internal origin and the project coordinates.</p>

<p>If <code>Document.ProjectLocations</code> does actually return both ProjectLocations in Revit 2016, you should simply ignore the 'Project' one. It's part of the project coordinates and isn't actually a ProjectLocation in the sense of the locations you see in Location/Weather/Site.</p>

<p>This information is given in terms of the 2018 API. It should mostly apply to Revit 2016 as well. I think the code internals are the same. You might need to make small trivial changes, such as use <code>get_ProjectPosition()</code> instead of <code>GetProjectPosition()</code> or similar.</p>

<p>Many thanks to Diane Christoforo for this illuminating explanation!</p>
