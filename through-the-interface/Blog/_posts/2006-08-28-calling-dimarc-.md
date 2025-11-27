---
layout: "post"
title: "Calling DIMARC from Visual LISP"
date: "2006-08-28 15:51:12"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoLISP / Visual LISP"
  - "Commands"
  - "Dimensions"
original_url: "https://www.keanw.com/2006/08/calling_dimarc_.html "
typepad_basename: "calling_dimarc_"
typepad_status: "Publish"
---

<p>This is an interesting little problem that came in from Japan last week. Basically the issue is understanding how to call the DIMARC command programmatically from LISP using (command).</p>

<p>The DIMARC command was introduced in AutoCAD 2006 to dimension the length of arcs or polyline arc segments. What's interesting about this problem is that it highlights different aspects of entity selection in LISP.</p>

<p>Firstly, let's take a look at the DIMARC command (I've put the keyboard-entered text in <span style="COLOR: red">red</span> below):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">DIMARC</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select arc or polyline arc segment:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify arc length dimension location, or [Mtext/Text/Angle/Partial/Leader]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Dimension text = 10.6887</p></div>

<p>The first prompt is for the arc or polyline arc segment, the second is for the dimension location (we're not going to worry about using other options).</p>

<p>The classic way to select an entity is to use (entsel):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">(entsel)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select object: (&lt;Entity name: 7ef8e050&gt; (22.1694 32.2269 0.0))</p></div>

<p>This returns a list containing both the entity-name and the point picked when selecting the entity. This is going to be particularly useful in this problem, as the DIMARC command needs to know where the point was picked: this is because it has to support picking of a &quot;complex&quot; entity, as it works not only on arcs but on arc segments of polylines. If you use the classic technique of using (car)stripping off the point, to just pass in the entity name, it fails:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">DIMARC</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select arc or polyline arc segment: <span style="COLOR: red">(car (entsel))</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select object: &lt;Entity name: 7ef8e050&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:</p></div>

<p>Whereas just passing the results of (entsel) works fine:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">DIMARC</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select arc or polyline arc segment: <span style="COLOR: red">(entsel)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select object: (&lt;Entity name: 7ef8e050&gt; (22.2696 32.2269 0.0))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify arc length dimension location, or [Mtext/Text/Angle/Partial/Leader]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Dimension text = 24.3844</p></div>

<p>So it's clear we need to keep the point in there. So we know what we have to do to select arc entities - we can pass in the results of (entsel).</p>

<p>Polyline entities are trickier. You can pass in the entity name, with or without the point, and the command won't work:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">DIMARC</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select arc or polyline arc segment: <span style="COLOR: red">(entsel)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select object: (&lt;Entity name: 7ef8e058&gt; (51.0773 29.5726 0.0))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Object selected is not an arc or polyline arc segment.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select arc or polyline arc segment:</p></div>

<p>But we can pass in the point picked by (entsel), and let the DIMARC command do the work of determining whether the segment picked was actually an arc or not:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">DIMARC</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select arc or polyline arc segment: <span style="COLOR: red">(cadr (entsel))</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select object: (52.6305 29.5726 0.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify arc length dimension location, or [Mtext/Text/Angle/Partial/Leader]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Dimension text = 9.4106</p></div>

<p>So now we're ready to put it all together.</p>

<p>[ <strong>Note:</strong> The below example does not take into consideration the user cancelling from (getpoint) or (entsel) - that is left as an exercise for the reader. ]</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">(defun c:myDimArc(/ en pt ed)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (setq en (entsel &quot;\nSelect arc or polyline arc segment: &quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pt (getpoint &quot;\nSpecify arc length dimension location: &quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed (entget (car en))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ; If the object selected is a 2D polyline (old or new)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ; then pass the point instead of the entity name</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (if (member (cdr (assoc 0 ed)) '(&quot;POLYLINE&quot; &quot;LWPOLYLINE&quot;))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (setq en (cadr en))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (command &quot;_.DIMARC&quot; en pt)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (princ)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">)</p></div>

<p>This is just an example of how you need to call the DIMARC command programmatically - with this technique you clearly lose the visual feedback from the dragging of the entity during selection (what we call the &quot;jig&quot; in ObjectARX). And this is ultimately an unrealistic example - you're more likely to need to call DIMARC automatically for a set of entities in a drawing than just define a simple command to request user input and pass it through.</p>

<p>One thing this example does demonstrate is that different commands have different needs in terms of entity selection information (some just need a selection set or an entity name, some need more that than).</p>
