---
layout: "post"
title: "January 2017 Fusion Update"
date: "2017-01-20 04:53:03"
author: "Adam Nagy"
categories:
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2017/01/january-2017-fusion-update.html "
typepad_basename: "january-2017-fusion-update"
typepad_status: "Publish"
---

<p>There are several new enhancements in this update and a couple that open some new doors on what can be done with the Fusion API.</p>
<ol>
<li>
<p>The first of these is the ability to customize the context menu, or as it&#39;s called in Fusion, the <em>marking menu</em>. You do this by responding to the MarkingMenuDisplaying event which is fired just before the marking menu is shown to the user. Here, you have a chance to find out what&#39;s currently selected in the UI (which is what defines the context) and then add and remove commands from the linear and radial marking menus. Below is an example where a new custom &quot;Design Notes&quot; command is being added to the marking menu when the user right-clicks on the root component.</p>
<div style="text-align: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb096ee3fe970d-pi"><img alt="ContextMenu_DesignNotes" border="0" height="453" src="/assets/image_770098.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="ContextMenu_DesignNotes" width="310" /></a></div>
</li>
<li>
<p>There are a couple of breaking changes in this release that we decided to go ahead and make because the functionality being changed is new and/or we believe the likelihood of impact on existing code to be very low. These are:</p>
<ul>
<li>
<p>The various add methods to create new sketch dimensions have a new &quot;isDriving&quot; argument to allow you to create driving or driven dimensions. This is an optional argument with the default value being True to create a driving dimension, which is the same as before. Because it&#39;s an optional argument that defaults to the same result as before, there is no impact to Python or JavaScript programs because they&#39;re interpreted and will automatically use the optional argument. C++ code will need to be recompiled.</p>
</li>
<li>
<p>In the last update we added some changes to bring the API in sync with the user-interface for creating extrusions. There were a couple of mistakes that we need to clean up in this update. The first is that the taperAngleOne and taperAngleTwo properties were typed to return a base.Core object. This has been corrected so they&#39;re typed to return a ModelParameter object.</p>
<p>The second issue is that there were two settings to control the direction of a through-all extent extrusion which was confusing. We&#39;ve removed the &quot;isPositiveDirection&quot; argument from the create method of the ThroughAllExtentDefinition class. If you&#39;ve used this you&#39;ll need to update your code to remove setting that argument and continue to use the &quot;direction&quot; argument on the setOneSideExtent method of the ExtrudeFeatureInput object.</p>
</li>
</ul>
</li>
<li>
<p>With the recent changes to the Extrude feature the API to create extrusions got quite a bit more complicated. However, we recognized that even though the feature is more powerful and provides a lot more capabilities, the majority of the time people still create simple finite extrusions. To make this more common workflow easier, we&#39;ve added a new <a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-0F875632-0DCB-4F18-9176-9617783AE794">addSimple</a>. method to the ExtrudeFeatures collection. This makes creating an extrusion much simpler than it ever was before with a single API call.</p>
</li>
<li>
<p>Fusion recently added the ability to choose which bodies will be affected when creating certain features; extrude, hole, loft, revolve, and sweep. The input objects for each of those features and the feature objects themselves all now support a &quot;participantBodies&quot; property that lets you specify the set of bodies that will participate in the feature. The default is that all visible bodies that intersect the feature will be used, which was also the previous default so existing programs shouldn&#39;t see any change of behavior.</p>
</li>
<li>
<p>An entirely new feature in this update is the ability for an add-in to fire an event to itself. This may seem like a strange thing to do but it opens up some new capabilities for some add-ins. Because all add-ins run in the main thread of Fusion, when the add-in code is actively executing everything else is blocked. This behavior is typical of most applications that have a user-interface. Changes to Fusion data, (both interactively and through the API), is done by code running in the main thread. However, there are cases where an add-in wants to do some separate work outside of Fusion and doesn&#39;t want to block the user interacting with Fusion while this work occurs. It&#39;s possible to create a new thread where this separate work can happen so the main thread isn&#39;t blocked but it hasn&#39;t been possible for that worker thread to communicate back to the add-in in the main thread. This new event provides that capability. See the new <a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-F9FD4A6D-C59F-4176-9003-CE04F7558CCC">Working in a Separate Thread topic</a> in the user manual for more information.</p>
</li>
<li>
<p>A new command input to allow the user to enter an angle, both in a command dialog and in the graphics window is now supported. The AngleValueCommandInput will allow you to create an input like that used to define the angle in the Revolve feature command, like shown below.</p>
<div style="text-align: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d255edae970c-pi"><img alt="AngleInputExample" border="0" height="347" src="/assets/image_547631.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="AngleInputExample" width="570" /></a></div>
</li>
<li>
<p>In response to <a href="http://forums.autodesk.com/t5/api-and-scripts/is-there-a-way-to-batch-delete-curves/m-p/6582414#M2193">ross.korsky&#39;s question</a> in the forum we&#39;ve added a new deleteEntities method on the Design object. This takes in an array of entities and deletes them all at once which is much simpler and faster than deleting them one at a time, which is what you had to do before. This is similar to selecting several entities and pressing the Delete key in the user-interface.</p>
</li>
<li>
<p>We&#39;ve also fixed several issues we found in our testing and the following problems that were reported on the forum.</p>
<ul>
<li>
<p><a href="http://forums.autodesk.com/t5/api-and-scripts/patch-with-splines/td-p/6756827" rel="noopener noreferrer" target="_blank">darinAZY43 reported a problem</a> where the creation of Patch features was failing when the input curves were non-planar. This has now been fixed.</p>
</li>
<li>
<p><a href="http://forums.autodesk.com/t5/api-and-scripts/divide-path-into-equal-segments/td-p/6753005" rel="noopener noreferrer" target="_blank">GeoffPacker7777 reported a problem</a> where the setByDistanceOnPath method of the ConstructionPlaneInput object was not allowing a Path as input but only single entities. This has now been fixed.</p>
</li>
<li>
<p>It was reported that TableCommandInputs don&#39;t work correctly when your command dialog has tabs with tables on each tab. This has now been fixed.</p>
</li>
</ul>
</li>
</ol>
<p>-Brian</p>
