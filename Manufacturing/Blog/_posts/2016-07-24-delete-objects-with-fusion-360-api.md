---
layout: "post"
title: "Delete Objects with Fusion 360 API"
date: "2016-07-24 23:36:08"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/07/delete-objects-with-fusion-360-api.html "
typepad_basename: "delete-objects-with-fusion-360-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>All objects that can be deleted support the the <strong>deleteMe</strong> method. The usage is&#160; very straightforward. Get the object and call deleteMe. </p>  <p>I found a forum post that one customer failed to delete the object which is within the generic object collection. </p>  <p><a href="http://forums.autodesk.com/t5/api-and-scripts/how-do-i-delete-a-sketch-curve-line-arc-etc/m-p/6394526#U6394526">http://forums.autodesk.com/t5/api-and-scripts/how-do-i-delete-a-sketch-curve-line-arc-etc/m-p/6394526#U6394526</a></p>  <p>but, it looks working well at my side. The following code demo three scenarios:</p>  <p>1. delete selected objects</p>  <p>2. delete the specific object in the collection created by UI</p>  <p>3. delete the specific object in the generic collection (e.g. in this code, the collection is the offset curves collection)</p>  <pre><code>
def deleteObjectsOfSelection():

    app = adsk.core.Application.get()
    try:      
        #assume some entities are selected
        for eachObj in app.userInterface.activeSelections:
            if eachObj.isValid:
                #get the selected entity of the selection.                
                #call deleteMe
                eachObj.entity.deleteMe()
    except:
        ui = app.userInterface
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))  
            

def deleteItemOfObjCollectionItem():

    app = adsk.core.Application.get()
    
    try:
        #current product            
        product = app.activeProduct
        #current component
        rootComp = product.rootComponent 
        #get sketches collection
        sketches = rootComp.sketches
        #get one sketch 
        if sketches.count &gt; 0:
            oneSketch = sketches.item(0)
            oneSketch.deleteMe()
       
    except:
        ui = app.userInterface
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))             


def deleteItemOfGenericCollection():
    app = adsk.core.Application.get()
    ui = app.userInterface
    
    try:
      
        
        #select a sketch        
        selectedObjs = app.userInterface.activeSelections
        if selectedObjs.count == 0:
           ui.messageBox('no object is selected in the document!')
           return
        
        oneObj = selectedObjs.item(0)
        if oneObj.entity.objectType != 'adsk::fusion::Sketch':
             print( oneObj.entity.objectType)
             ui.messageBox('not a sketch!')
             return        
        sketchIn = oneObj.entity 
        
        sketchIn.isComputeDeferred = True
        
        curvesIn = sketchIn.sketchCurves
        if curvesIn.count == 0:
             ui.messageBox('no curve in the sketch!')
             return
             
        curvesForOffset = adsk.core.ObjectCollection.create() 
        offsetCurves = adsk.core.ObjectCollection.create()
        
        curvesForOffset = sketchIn.findConnectedCurves(curvesIn.item(0));
        offsetCurves = sketchIn.offset(curvesForOffset, adsk.core.Point3D.create(100,100,100), 2.0);
        
        if offsetCurves == None or offsetCurves.count ==0:
             ui.messageBox('no offset curves are created!')
             return
        
        transform = adsk.core.Matrix3D.create();
        
        #current product            
        product = app.activeProduct
        #current component
        rootComp = product.rootComponent 
        #get sketches collection
        sketches = rootComp.sketches
        
        #create a new sketch 
        sketchOut = sketches.add(rootComp.xYConstructionPlane)
        sketchIn.copy(offsetCurves, transform, sketchOut);
        
        #delete the offseted curves in sketchIn
        for eachCurve in offsetCurves:
            eachCurve.deleteMe()
        
        sketchIn.isComputeDeferred =False 
       
    except:
        ui = app.userInterface
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))  
     
</code></pre>
