---
layout: "post"
title: "Is point on face"
date: "2016-05-27 18:02:37"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/05/is-point-on-face.html "
typepad_basename: "is-point-on-face"
typepad_status: "Publish"
---

<p>To be more precise, the question should be &quot;Is Point3D on BRepFace?&quot; :)</p>
<p>There is no direct function to determine that but you can get to the answer by using other functions like the <span class="s1"><strong>isParameterOnFace&#0160;</strong>function of&#0160;<strong>SurfaceEvaluator</strong>.</span></p>
<p><span class="s1">In order to use that though we need to first get the parameters based on the point. Turns out the function we need to use,&#0160;</span><span class="s1"><strong>getParameterAtPoint</strong>, actually works like this: &quot;</span><span class="s1">If the point does not lie on the surface, the parameter of the nearest point on the surface will generally be returned.</span><span class="s1">&quot;<br />So we have to check for that too.</span></p>
<p><span class="s1">Here is the&#0160;<strong>Python</strong> code we can use:</span></p>
<pre>def isPointOnFace():
    app = adsk.core.Application.get()
    ui  = app.userInterface
    
    selections = app.userInterface.activeSelections
    sketchPoint = selections.item(0).entity
    face = selections.item(1).entity
    
    evaluator = face.evaluator
    point = sketchPoint.worldGeometry
    ui.messageBox(
        &quot;point, x=&quot; + str(point.x) + 
        &quot;; y=&quot; + str(point.y) + 
        &quot;; z=&quot; + str(point.z))
    (returnValue, parameter) = evaluator.getParameterAtPoint(point)
    ui.messageBox(
        &quot;parameter, u=&quot; + str(parameter.x) + 
        &quot;; v=&quot; + str(parameter.y))
    
    if not returnValue:
        # could not get the parameter for it so
        # it&#39;s probably not on the face
        ui.messageBox(&quot;Point not on face\n(Could not get parameter)&quot;)
        return
        
    (returnValue, projectedPoint) = evaluator.getPointAtParameter(parameter)
    ui.messageBox(
        &quot;projectedPoint, x=&quot; + str(projectedPoint.x) + 
        &quot;; y=&quot; + str(projectedPoint.y) + 
        &quot;; z=&quot; + str(projectedPoint.z))
    if not projectedPoint.isEqualTo(point):
        # the point has been projected in order to get 
        # a parameter so it&#39;s not on the face
        ui.messageBox(
            &quot;Point not on face\n(Point was projected in order to get parameter)&quot;)
        return
    
    returnValue = evaluator.isParameterOnFace(parameter)
    if not returnValue:
        ui.messageBox(&quot;Point not on face\n(isParameterOnFace says so)&quot;)
        return
    
    ui.messageBox(&quot;Point on face&quot;)</pre>
<p><span class="s1">You just have to select a <strong>SketchPoint</strong> and a <strong>BRepFace</strong> in the <strong>UI</strong> before running it:</span></p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb0907fbac970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PointOnFace" class="asset  asset-image at-xid-6a00e553fcbfc6883401bb0907fbac970d img-responsive" src="/assets/image_323252.jpg" title="PointOnFace" /></a></p>
<p><span class="s1">-Adam &#0160;</span></p>
