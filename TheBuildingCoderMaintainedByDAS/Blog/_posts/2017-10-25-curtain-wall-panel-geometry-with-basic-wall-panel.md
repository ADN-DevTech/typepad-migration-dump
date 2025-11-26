---
layout: "post"
title: "Curtain Wall Panel Geometry with Basic Wall Panel"
date: "2017-10-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Data Access"
  - "Element Relationships"
  - "Filters"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/10/curtain-wall-panel-geometry-with-basic-wall-panel.html "
typepad_basename: "curtain-wall-panel-geometry-with-basic-wall-panel"
typepad_status: "Publish"
---

<p>A quick geometrical question on retrieving geometry from a basic wall being used as a panel in a curtain wall:</p>

<p><strong>Question:</strong> I am struggling to retrieve the geometry data from a curtain wall that contains a Basic wall in one of the curtain wall panels. </p>

<p>My example curtain wall has two panels. With one of the panels, a basic wall type is associated. I need to get the geometry data (i.e., the faces) for the entire curtain wall. When I reach the second panel in my code, the <code>SymbolGeometry</code> contains zero objects, so my code cannot retrieve any geometry for it. As a result, it is not able to handle the basic wall to retrieve its geometry face data. </p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb09d09c7a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb09d09c7a970d img-responsive" style="width: 151px; " alt="Curtain wall panels" title="Curtain wall panels" src="/assets/image_386cfd.jpg" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> Here is a code snippet that handles this situation correctly.</p>

<p>The main point is this: we need to find the panel-wall which corresponds to the curtain wall and then retrieve its geometry in a second step:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;First,&nbsp;find&nbsp;solid&nbsp;geometry&nbsp;from&nbsp;panel&nbsp;ids.</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Note&nbsp;that&nbsp;the&nbsp;panel&nbsp;which&nbsp;contains&nbsp;a&nbsp;basic</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;wall&nbsp;has&nbsp;NO&nbsp;geometry!</span>

&nbsp;&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;=&nbsp;doc.GetElement(&nbsp;curtainWallId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Wall</span>;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;grid&nbsp;=&nbsp;wall.CurtainGrid;

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;id&nbsp;<span style="color:blue;">in</span>&nbsp;grid.GetPanelIds()&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;e&nbsp;=&nbsp;doc.GetElement(&nbsp;id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;solids.AddRange(&nbsp;GetElementSolids(&nbsp;e&nbsp;)&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Secondly,&nbsp;find&nbsp;corresponding&nbsp;panel&nbsp;wall</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;for&nbsp;the&nbsp;curtain&nbsp;wall&nbsp;and&nbsp;retrieve&nbsp;the&nbsp;actual</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;geometry&nbsp;from&nbsp;that.</span>

&nbsp;&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>&nbsp;cwPanels
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfCategory(&nbsp;<span style="color:#2b91af;">BuiltInCategory</span>.OST_CurtainWallPanels&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;cwp&nbsp;<span style="color:blue;">in</span>&nbsp;cwPanels&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Find&nbsp;panel&nbsp;wall&nbsp;belonging&nbsp;to&nbsp;this&nbsp;curtain&nbsp;wall</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;and&nbsp;retrieve&nbsp;its&nbsp;geometry</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;cwp.StackedWallOwnerId&nbsp;==&nbsp;curtainWallId&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;solids.AddRange(&nbsp;GetElementSolids(&nbsp;cwp&nbsp;)&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<p>I added this code in the method <code>GetCurtainWallPanelGeometry</code> 
to <a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2018.0.134.6">The Building Coder samples release 2018.0.134.6</a>
module <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/CmdCurtainWallGeom.cs#L35-L66">CmdCurtainWallGeom.cs L35-L66</a>.</p>
