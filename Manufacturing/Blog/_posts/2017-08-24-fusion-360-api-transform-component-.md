---
layout: "post"
title: "Fusion 360 API: Transform Component "
date: "2017-08-24 04:02:06"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/08/fusion-360-api-transform-component-.html "
typepad_basename: "fusion-360-api-transform-component-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>There are some posts on Autodesk forum about translating or rotating component in the assembly. This blog is mainly for sort them out.&#0160;</p>
<p>The object Occurrence.transform manages the transformation this&#0160;component locates relatively to the space of assembly. It is &#0160;Matrix3D which is&#0160;a standard math object. you could either make static method to build the translation, transformBy a matrix (Matrix3D.transformBy, or edit the cells (Matrix3D.setCell). Calling the transform property of the Occurrence object creates a new matrix that contains the current transform information of the occurrence and returns this matrix.&#0160; The matrix doesn&#39;t have any relationship back to the occurrence so changing information in the matrix has not affect on the occurrence. &#0160;So, the correct way to update the transformation is to get the existing transform as a matrix, change data within the matrix and then assign it back to the occurrence.&#0160; The matrix being assigned back to the occurrence can come from anywhere.&#0160; The assignment then update the internal transform of the occurrence.</p>
<p>The following is a code snippet on translating one&#0160;component, and rotate another&#0160;component. It shows a couple ways to update transformation. The comments enclosed explain the corresponding methods.&#0160;</p>
<p>Another trick in this demo is: to make an animation of the transforming, you will need to call doEvents after each transformation updates. &#0160;<strong>doEvents</strong>&#0160;function temporarily halts the execution of the add-in or script and gives Fusion 360 a chance to handle any queued up messages. &#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<pre><code>
#Author-
#Description-


import adsk.core, adsk.fusion, adsk.cam, traceback,threading
import math
import time



times = 0


def eachMove():
    ui = None
    try:
        app = adsk.core.Application.get()
        ui  = app.userInterface


        #get product        
        product = app.activeProduct
        design = adsk.fusion.Design.cast(product)
        if not design:
            ui.messageBox(&#39;No active Fusion design&#39;, &#39;No Design&#39;)
            return


        # Get the root component of the active design.
        rootComp = design.rootComponent
        #get array of occurrences
        occurrences = rootComp.occurrences.asList
        
        #get first component
        occurrence_first = occurrences.item(0)
        
         #get second component
        occurrence_second= occurrences.item(1)
        
        #get thrid component
        occurrence_third= occurrences.item(2)
        
       
        while (times &lt; 20):           
            
            #translate the first component by updating [translation] property

            #wrong way: occurrence.transform will clone and return a matrix
            #so any change on this cloned matrix does not take effect on the 
            #update of component
            #offsetVector = adsk.core.Vector3D.create(5.0, 0, 0)
            #occurrence.transform.translation.add( offsetVector )
        
            # Get the current transform of the first occurrence
            transform = occurrence_first.transform
            
            if times &lt;10:
                # Change the transform data by moving 5.0cm on X+ axis
                transform.translation = adsk.core.Vector3D.create( transform.translation.x +  5.0, 0, 0)
            else:
                # Change the transform data by moving 5.0cm on X- axis
                transform.translation = adsk.core.Vector3D.create( transform.translation.x -  5.0, 0, 0)
                
            # Set the tranform data back to the occurrence
            occurrence_first.transform = transform
            
            # Get the current transform of the second occurrence
            transform = occurrence_second.transform
            
            
            rotX = adsk.core.Matrix3D.create()
            # Change the transform data by rotating around Z+ axis

            rotX.setToRotation(math.pi/4, adsk.core.Vector3D.create(0,0,1), adsk.core.Point3D.create(0,0,0))
            transform.transformBy(rotX)
            
             # Set the tranform data back to the occurrence
            occurrence_second.transform = transform
            
             # Get the current transform of the third occurrence
            transform = occurrence_third.transform
            
            if times &lt;10:
                # Change the transform data by moving 5.0cm on Y+ axis
                transform.setCell(1,3,transform.getCell(1,3) + 5.0)
            else:
                # Change the transform data by moving 5.0cm on Y- axis
                transform.setCell(1,3,transform.getCell(1,3) - 5.0)
                 

            #rotate around Z+ axis
            rotZSin = math.sin(math.pi/4 * times)
            rotZCos = math.cos(math.pi/4 * times)
            
            #change the cells value 
            transform.setCell(0,0,rotZCos)
            transform.setCell(0,1,rotZSin)
            transform.setCell(1,0,-rotZSin)
            transform.setCell(1,1,rotZCos) 
            
             # Set the tranform data back to the occurrence
            occurrence_third.transform = transform
            
            
            global times
            times =times +1 
            time.sleep( 0.1 )

            #Calling doEvents gives it a chance to catch up each time the components are transformed. 
            adsk.doEvents() 

       
        
       
    except:
        if ui:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))


def run(context): 
    global times
    times = 0
    eachMove() 
   
<br /></code></pre>
<p><strong>click the picture to see the gif.</strong></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a3400a970c-pi" style="display: inline;"><img alt="Transform" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2a3400a970c img-responsive" src="/assets/image_74fd32.jpg" title="Transform" /></a></p>
