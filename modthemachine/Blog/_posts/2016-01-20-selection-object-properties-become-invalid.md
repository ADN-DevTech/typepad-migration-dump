---
layout: "post"
title: "Selection object properties become invalid"
date: "2016-01-20 15:02:50"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/01/selection-object-properties-become-invalid.html "
typepad_basename: "selection-object-properties-become-invalid"
typepad_status: "Publish"
---

<p>In your commands you can use <a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/SelectionCommandInput.htm">SelectionCommandInput</a>&#0160;objects and they work fine. However, if you do certain modifications on the model, e.g. create a new sketch, then the&#0160;<strong>Selection</strong> object of the last used&#0160;<strong>SelectionCommandInput</strong> will become invalid:</p>
<pre>var onCommandExecuted = function(args) {
    try {
        var command = adsk.core.Command(args.firingEvent.sender);
        var inputs = command.commandInputs;

        // both are filtered for vertex selection
        var selection1 = inputs.itemById(&#39;selection1&#39;).selection(0);
        var selection2 = inputs.itemById(&#39;selection2&#39;).selection(0);

        var design = app.activeProduct;
        var root = design.rootComponent;
        var sketches = root.sketches;
        var xyPlane = root.xYConstructionPlane;

        // Monitor selection1 and selection2 in the object 
        // window. As soon as we create a new sketch the 
        // variable to the last selected entity will become
        // unavailable &gt;&gt; &lt;not available&gt;
        sketches.add(xyPlane);
        ui.messageBox(&quot;selection1 &gt;&gt; &quot; + selection1.<strong>entity</strong>.edges.count.toString()); // this succeeds
        ui.messageBox(&quot;selection2 &gt;&gt; &quot; + selection2.<strong>entity</strong>.edges.count.toString()); // this does not
     } 
     catch (e) {        
        ui.messageBox(&#39;Failed to run command : &#39; + (e.description ? e.description : e));
    }
};</pre>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c80a38f1970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SelectionIssue" class="asset  asset-image at-xid-6a00e553fcbfc6883401b7c80a38f1970b img-responsive" src="/assets/image_305193.jpg" title="SelectionIssue" /></a></p>
<p>This should not really affect you though, since you can get all the information out of the <strong>Selection</strong> objects <span style="text-decoration: underline;">before</span> doing any manipulation on the model:</p>
<pre>var onCommandExecuted = function(args) {
    try {
        var command = adsk.core.Command(args.firingEvent.sender);
        var inputs = command.commandInputs;

        // both are filtered for vertex selection
        var selectedEntity1 = inputs.itemById(&#39;selection1&#39;).selection(0).<strong>entity</strong>;
        var selectedEntity2 = inputs.itemById(&#39;selection2&#39;).selection(0).<strong>entity</strong>;

        var design = app.activeProduct;
        var root = design.rootComponent;
        var sketches = root.sketches;
        var xyPlane = root.xYConstructionPlane;

        sketches.add(xyPlane);
        ui.messageBox(&quot;selection1 &gt;&gt; &quot; + selectedEntity1.edges.count.toString()); 
        ui.messageBox(&quot;selection2 &gt;&gt; &quot; + selectedEntity2.edges.count.toString()); 
     } 
     catch (e) {        
        ui.messageBox(&#39;Failed to run command : &#39; + (e.description ? e.description : e));
    }
};</pre>
<p>-Adam</p>
