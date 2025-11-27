---
layout: "post"
title: "Fusion API: Manage Participant Bodies"
date: "2017-01-23 02:17:57"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/01/fusion-api-manage-participant-bodies.html "
typepad_basename: "fusion-api-manage-participant-bodies"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>This is mainly quoted from Fusion 360 help document:</p>
<p>In the recent update in Jan, Fusion added the ability to choose which bodies will be affected when creating certain features; extrude, hole, loft, revolve, and sweep. The input objects for each of those features and the feature objects themselves all now support a &quot;participantBodies&quot; property that lets you specify the set of bodies that will participate in the feature. The default is that all visible bodies that intersect the feature will be used, which was also the previous default so existing programs shouldn&#39;t see any change of behavior.</p>
<p>The codes below is a simple sample on how this new property works. It creates some bodies, then creates a cut feature. With&#0160;participantBodies, only some of the bodies are cut.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ccea7d970b-pi" style="display: inline;"><img alt="Screen Shot 2017-01-23 at 6.14.14 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ccea7d970b image-full img-responsive" src="/assets/image_0c4624.jpg" title="Screen Shot 2017-01-23 at 6.14.14 PM" /></a></p>
<p>&#0160;</p>
<p><strong>Python</strong>:</p>
<p>&#0160;</p>
<pre><code>
#Author-
#Description-

import adsk.core, adsk.fusion, adsk.cam, traceback

def run(context):
    ui = None
    try:
        app = adsk.core.Application.get()
        ui  = app.userInterface
        
        #crreate a document
        doc = app.documents.add(adsk.core.DocumentTypes.FusionDesignDocumentType)
        
        product = app.activeProduct
        design = adsk.fusion.Design.cast(product)
        
        #get the root component of the active design
        rootComp = design.rootComponent
        
        # create a sketch
        sketches = rootComp.sketches
        sketch = sketches.add(rootComp.xZConstructionPlane)
        sketchCircles = sketch.sketchCurves.sketchCircles
        centerPoint = adsk.core.Point3D.create(0, 0, 0)
        sketchCircles.addByCenterRadius(centerPoint, 3.0)

        # Get the profile defined by the circle.
        prof = sketch.profiles.item(0)

	  # Create sketch for Cut
        sketchForCut = sketches.add(rootComp.xZConstructionPlane)
        sketchForCutCircles = sketchForCut.sketchCurves.sketchCircles
        sketchForCutCircles.addByCenterRadius(centerPoint, 1.5)

        # Get the profile defined by the circle.
        profForCut = sketchForCut.profiles.item(0)

        # Create an extrusion input
        extrudes = rootComp.features.extrudeFeatures
        extInput = extrudes.createInput(prof, adsk.fusion.FeatureOperations.NewBodyFeatureOperation)

        # Define that the extent is a distance extent of 5 cm.
        distance = adsk.core.ValueInput.createByReal(5)
        extInput.setDistanceExtent(False, distance)

        # Create the extrusion.
        ext = extrudes.add(extInput)

        # Get the body created by extrusion
        body = ext.bodies.item(0)

        # Create input entities for rectangular pattern
        inputEntites = adsk.core.ObjectCollection.create()
        inputEntites.add(body)

        # Get x and y axes for rectangular pattern
        xAxis = rootComp.xConstructionAxis
        yAxis = rootComp.yConstructionAxis

        # Quantity and distance
        quantityOne = adsk.core.ValueInput.createByString(&#39;0&#39;)
        distanceOne = adsk.core.ValueInput.createByString(&#39;0 cm&#39;)
        quantityTwo = adsk.core.ValueInput.createByString(&#39;6&#39;)
        distanceTwo = adsk.core.ValueInput.createByString(&#39;15 cm&#39;)

        # Create the input for rectangular pattern
        rectangularPatterns = rootComp.features.rectangularPatternFeatures
        rectangularPatternInput = rectangularPatterns.createInput(inputEntites, xAxis, \
            quantityOne, distanceOne, adsk.fusion.PatternDistanceType.SpacingPatternDistanceType)

        # Set the data for second direction
        rectangularPatternInput.setDirectionTwo(yAxis, quantityTwo, distanceTwo)

        # Create the rectangular pattern
        rectangularFeature = rectangularPatterns.add(rectangularPatternInput)

        patBodies = rectangularFeature.bodies

        body0 = patBodies.item(0)
        face0 = body0.faces.item(0)

        extCutInput = extrudes.createInput(profForCut, adsk.fusion.FeatureOperations.CutFeatureOperation)

	  # Set the extrude input
        distanceForCut = adsk.core.ValueInput.createByString(&#39;90 cm&#39;)
        extCutInput.setDistanceExtent(False, distanceForCut)

        # set bodies to participate
        extCutInput.participantBodies = [patBodies.item(0), patBodies.item(2), patBodies.item(4)]

        extrudes.add(extCutInput)        
        

    except:
        if ui:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))

</code></pre>
<p><strong>JavaScipt</strong>:</p>
<p>&#0160;</p>
<pre><code>
function run(context) {

    &quot;use strict&quot;;
    if (adsk.debug === true) {
        /*jslint debug: true*/
        debugger;
        /*jslint debug: false*/
    }

    var ui;
    try {
        var app = adsk.core.Application.get();
        ui = app.userInterface;

        // Create a document.
        var doc = app.documents.add(adsk.core.DocumentTypes.FusionDesignDocumentType);

        var product = app.activeProduct;
        var design = adsk.fusion.Design(product);

        // Get the root component of the active design.
        var rootComp = design.rootComponent;

        // Create sketch
        var sketches = rootComp.sketches;
        var sketch = sketches.add(rootComp.xZConstructionPlane);
        var sketchCircles = sketch.sketchCurves.sketchCircles;
        var centerPoint = adsk.core.Point3D.create(0, 0, 0);
        sketchCircles.addByCenterRadius(centerPoint, 3.0);

        // Get the profile defined by the circle.
        var prof = sketch.profiles.item(0);

		// Create sketch for Cut
        var sketchForCut = sketches.add(rootComp.xZConstructionPlane);
        var sketchForCutCircles = sketchForCut.sketchCurves.sketchCircles;
        sketchForCutCircles.addByCenterRadius(centerPoint, 1.5);

        // Get the profile defined by the circle.
        var profForCut = sketchForCut.profiles.item(0);

        // Create an extrusion input
        var extrudes = rootComp.features.extrudeFeatures;
        var extInput = extrudes.createInput(prof, adsk.fusion.FeatureOperations.NewBodyFeatureOperation);

        // Define that the extent is a distance extent of 5 cm.
        var distance = adsk.core.ValueInput.createByReal(5);
        extInput.setDistanceExtent(false, distance);

        // Create the extrusion.
        var ext = extrudes.add(extInput);

        // Get the body created by extrusion
        var body = ext.bodies.item(0);

        // Create input entities for rectangular pattern
        var inputEntites = adsk.core.ObjectCollection.create();
        inputEntites.add(body);

        // Get x and y axes for rectangular pattern
        var xAxis = rootComp.xConstructionAxis;
        var yAxis = rootComp.yConstructionAxis;

        // Quantity and distance
        var quantityOne = adsk.core.ValueInput.createByString(&#39;0&#39;);
        var distanceOne = adsk.core.ValueInput.createByString(&#39;0 cm&#39;);
        var quantityTwo = adsk.core.ValueInput.createByString(&#39;6&#39;);
        var distanceTwo = adsk.core.ValueInput.createByString(&#39;15 cm&#39;);

        // Create the input for rectangular pattern
        var rectangularPatterns = rootComp.features.rectangularPatternFeatures;
        var rectangularPatternInput = rectangularPatterns.createInput(inputEntites, xAxis,
            quantityOne, distanceOne, adsk.fusion.PatternDistanceType.SpacingPatternDistanceType);

        // Set the data for second direction
        rectangularPatternInput.setDirectionTwo(yAxis, quantityTwo, distanceTwo);

        // Create the rectangular pattern
        var rectangularFeature = rectangularPatterns.add(rectangularPatternInput);

		var patBodies = rectangularFeature.bodies;

        var body0 = patBodies.item(0);
        var face0 = body0.faces.item(0);

		var extCutInput = extrudes.createInput(profForCut, adsk.fusion.FeatureOperations.CutFeatureOperation);

		// Set the extrude input
        var distanceForCut = adsk.core.ValueInput.createByString(&#39;90 cm&#39;);
        extCutInput.setDistanceExtent(false, distanceForCut);

        //set bodies to participate
        extCutInput.participantBodies = [patBodies.item(0), patBodies.item(2), patBodies.item(4)];

		extrudes.add(extCutInput);

    }
    catch (e) {
        if (ui) {
            ui.messageBox(&#39;Failed : &#39; + (e.description ? e.description : e));
        }
    }

    adsk.terminate();
}
</code></pre>
