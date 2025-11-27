---
layout: "post"
title: "Fusion API: Add Simple Extrude Feature and Add by Input"
date: "2017-01-24 06:04:00"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/01/fusion-api-add-simple-extrude-feature-and-add-by-input.html "
typepad_basename: "fusion-api-add-simple-extrude-feature-and-add-by-input"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>In the past, when creating the extrude feature, we will need to create an ExtrudeInput firstly, then add the extrude feature with the input. The input is like a definition of the feature, with which, some parameters of the feature can be defined. One of the benefits is the ExtrudeFeatures.Add method would not be changed for long term. When any new parameter of creating feature is appended, the API specifies can only add more properties withExtrudeInput.&#0160;</p>
<p>However, quoted from the latest news of Fusion 360 API:&#0160;<em>With the recent changes to the Extrude feature the API to create extrusions got quite a bit more complicated. However, we recognized that even though the feature is more powerful and provides a lot more capabilities, the majority of the time people still create simple finite extrusions. To make this more common workflow easier, we&#39;ve added a new <a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/ExtrudeFeatures_addSimple.htm">addSimple</a>. method to the ExtrudeFeatures collection. This makes creating an extrusion much simpler than it ever was before with a single API call.</em></p>
<p>So, you could decide to use the previous way or the simple way based on your requirement. The following is a code snippets from the <a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-CB1A2357-C8CD-474D-921E-992CA3621D04">API sample&#0160;</a>on the two scenarios.&#0160;</p>
<p><strong>Python</strong></p>
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
        
        # Get extrude features
        extrudes = rootComp.features.extrudeFeatures  
        
        # create a sketch
        sketches = rootComp.sketches
        sketch1 = sketches.add(rootComp.xZConstructionPlane)
        sketchCircles = sketch1.sketchCurves.sketchCircles
        centerPoint = adsk.core.Point3D.create(0, 0, 0)
        sketchCircles.addByCenterRadius(centerPoint, 3.0)

        # Get the profile defined by the circle.
        prof_simple = sketch1.profiles.item(0) 
        
        # Extrude Sample 1: A simple way of creating typical extrusions (extrusion that goes from the profile plane the specified distance).
        # Define a distance extent of 5 cm
        distance = adsk.core.ValueInput.createByReal(5) 
        extrude_simple = extrudes.addSimple(prof_simple, distance, adsk.fusion.FeatureOperations.NewBodyFeatureOperation) 
        # Get the extrusion body
        body_simple = extrude_simple.bodies.item(0) 
        body_simple.name = &quot;simple extrude feature&quot; 
        
        #Create another sketch
        sketch2 = sketches.add(rootComp.xZConstructionPlane) 
        sketchCircles2 = sketch2.sketchCurves.sketchCircles 
        sketchCircles2.addByCenterRadius(centerPoint, 13.0) 
        sketchCircles2.addByCenterRadius(centerPoint, 15.0) 
        prof_for_input = sketch2.profiles.item(1) 
        
        # Create taper angle value inputs
        deg0 = adsk.core.ValueInput.createByString(&#39;0 deg&#39;) 
        deg2 = adsk.core.ValueInput.createByString(&#39;2 deg&#39;) 
        deg5 = adsk.core.ValueInput.createByString(&#39;5 deg&#39;) 
        
        #Create distance value inputs
        mm10 = adsk.core.ValueInput.createByString(&#39;10 mm&#39;) 
        mm100 = adsk.core.ValueInput.createByString(&#39;100 mm&#39;) 

        # Extrude Sample 2: Create an extrusion that goes from the profile plane with one side distance extent
        extrudeInput = extrudes.createInput(prof_for_input, adsk.fusion.FeatureOperations.NewBodyFeatureOperation) 
        # Create a distance extent definition
        extent_distance = adsk.fusion.DistanceExtentDefinition.create(mm100) 
        extrudeInput.setOneSideExtent(extent_distance, adsk.fusion.ExtentDirections.PositiveExtentDirection) 
        # Create the extrusion
        extrude_by_input = extrudes.add(extrudeInput) 
        # Get the body of extrusion
        body_by_input = extrude_by_input.bodies.item(0) 
        body_by_input.name = &quot;extrude feature by input&quot; 
        

    except:
        if ui:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))

</code></pre>
<p>&#0160;</p>
<p><strong>JavaScript</strong></p>
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

        // Get the root component of the active design
        var rootComp = design.rootComponent;
        
        // Get extrude features
        var extrudes = rootComp.features.extrudeFeatures; 
        
        // Create sketch
        var sketches = rootComp.sketches;
        
        var sketch1 = sketches.add(rootComp.xZConstructionPlane);
        var sketchCircles = sketch1.sketchCurves.sketchCircles;
        var centerPoint = adsk.core.Point3D.create(0, 0, 0);
        var circle = sketchCircles.addByCenterRadius(centerPoint, 5.0);
        
        // Get the profile defined by the circle
        var prof_simple = sketch1.profiles.item(0);
        
        // Extrude Sample 1: A simple way of creating typical extrusions (extrusion that goes from the profile plane the specified distance).
        // Define a distance extent of 5 cm
        var distance = adsk.core.ValueInput.createByReal(5);
        var extrude_simple = extrudes.addSimple(prof_simple, distance, adsk.fusion.FeatureOperations.NewBodyFeatureOperation);
        // Get the extrusion body
        var body_simple = extrude_simple.bodies.item(0);
        body_simple.name = &quot;simple extrude feature&quot;;
        
       // Create another sketch
        var sketch2 = sketches.add(rootComp.xZConstructionPlane);
        var sketchCircles2 = sketch2.sketchCurves.sketchCircles;
        sketchCircles2.addByCenterRadius(centerPoint, 13.0);
        sketchCircles2.addByCenterRadius(centerPoint, 15.0);
        var prof_for_input = sketch2.profiles.item(1);
        
        // Create taper angle value inputs
        var deg0 = adsk.core.ValueInput.createByString(&#39;0 deg&#39;);
        var deg2 = adsk.core.ValueInput.createByString(&#39;2 deg&#39;);
        var deg5 = adsk.core.ValueInput.createByString(&#39;5 deg&#39;);
        // Create distance value inputs
        var mm10 = adsk.core.ValueInput.createByString(&#39;10 mm&#39;);
        var mm100 = adsk.core.ValueInput.createByString(&#39;100 mm&#39;);

        // Extrude Sample 2: Create an extrusion that goes from the profile plane with one side distance extent
        var extrudeInput = extrudes.createInput(prof_for_input, adsk.fusion.FeatureOperations.NewBodyFeatureOperation);
        // Create a distance extent definition
        var extent_distance = adsk.fusion.DistanceExtentDefinition.create(mm100);
        extrudeInput.setOneSideExtent(extent_distance, adsk.fusion.ExtentDirections.PositiveExtentDirection);
        // Create the extrusion
        var extrude_by_input = extrudes.add(extrudeInput);
        // Get the body of extrusion
        var body_by_input = extrude_by_input.bodies.item(0);
        body_by_input.name = &quot;extrude feature by input&quot;;
        

        
    } 
    catch (e) {
        if (ui) {
            ui.messageBox(&#39;Failed : &#39; + (e.description ? e.description : e));
        }
    }

    adsk.terminate(); 
}
 
</code></pre>
<p>&#0160;</p>
<p>&#0160;</p>
