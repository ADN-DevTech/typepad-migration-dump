---
layout: "post"
title: "C# Help Examples for Drawing Views"
date: "2012-11-29 21:30:32"
author: "Wayne Brill"
categories:
  - "C#"
  - "Drawings"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/11/c-help-examples-for-drawing-views.html "
typepad_basename: "c-help-examples-for-drawing-views"
typepad_status: "Publish"
---

<p>Here is another section of VBA procedures from the help file converted to C#. These examples are related to drawing views. There was one example that already had a C# version.&#0160;&#0160;&#0160;</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d3e4aa827970c-pi"><img alt="image" border="0" height="275" src="/assets/image_974015.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="368" /></a></p>
<p>You can find details about how the C# projects can be used in this <a href="http://modthemachine.typepad.com/my_weblog/2012/08/c-help-examples-for-sketch-part-1.html" target="_blank">post</a>. This project has the following functions:</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc68834017d3e4aab33970c"><a href="http://modthemachine.typepad.com/files/inventorhelpexamples_drawing_views.zip">Download InventorHelpExamples_Drawing_Views</a></span></p>
<p>CreateBreakoperationInDrawingView <br />ModifyDrawingSketchEntityProperties <br />DrawingSketchFill <br />CreateDrawingSketchInView <br />AddBaseViewWithRepresentations <br />AddFlatPatternDrawingView <br />CreateDetailView <br />CreateDraftView <br />Layer <br />BreakAlignment <br />AddUsingSheetFormat</p>
<p><strong>Here is CreateBreakoperationInDrawingView <br /></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">//Creation of a break operation in a drawing view </span></p>
<p style="margin: 0px;"><span style="color: green;">//Demonstrates the creation of a break operation.</span></p>
<p style="margin: 0px;"><span style="color: green;">//Before running this sample, select a drawing </span></p>
<p style="margin: 0px;"><span style="color: green;">//view in the active drawing.</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> CreateBreakoperationInDrawingView()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the drawing document.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// This assumes a drawing document is active.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DrawingDocument</span> oDrawDoc =</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">DrawingDocument</span>)ThisApplication.ActiveDocument;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//Set a reference to the active sheet.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Sheet</span> oSheet = (<span style="color: #2b91af;">Sheet</span>)oDrawDoc.ActiveSheet;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">DrawingView</span> oDrawingView = <span style="color: blue;">default</span>(<span style="color: #2b91af;">DrawingView</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Check to make sure a drawing view is selected.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the selected drawing.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// This assumes that the selected view is</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// not a draft view.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDrawingView =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DrawingView</span>)oDrawDoc.SelectSet[1];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">catch</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;A drawing view must be selected.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//center of the base view.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point2d</span> oCenter =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">Point2d</span>)oDrawingView.Center;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Define the start point of the break</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point2d</span> oStartPoint = ThisApplication.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TransientGeometry.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreatePoint2d(oCenter.X - 1, oCenter.Y);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Define the end point of the break</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point2d</span> oEndPoint = <span style="color: blue;">default</span>(<span style="color: #2b91af;">Point2d</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oEndPoint = ThisApplication.TransientGeometry.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreatePoint2d(oCenter.X + 1, oCenter.Y);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">BreakOperation</span> oBreakOperation =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">BreakOperation</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oBreakOperation = oDrawingView.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; BreakOperations.Add</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">BreakOrientationEnum</span>.kHorizontalBreakOrientation,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oStartPoint, oEndPoint,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">BreakStyleEnum</span>.kRectangularBreakStyle, 5);</p>
<p style="margin: 0px;">}</p>
</div>
