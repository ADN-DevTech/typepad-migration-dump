---
layout: "post"
title: "Fusion API: Change Appearance of Body"
date: "2016-04-18 07:01:49"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/04/fusion-api-change-appearance-of-body.html "
typepad_basename: "fusion-api-change-appearance-of-body"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Various objects in Fusion 360 can be applied with material and appearance. Appearance is how the object looks like. While Material is the physical property of an object. It has a default appearance. e.g. an object with “Leather, weathered” would look like this.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1c663dd970c-pi"><img style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="clip_image001" src="/assets/image_163ea1.jpg" alt="clip_image001" width="244" height="192" border="0" /></a></p>
<p>However we can override its appearance with other type.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08e040e4970d-pi"><img style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="clip_image002" src="/assets/image_795821.jpg" alt="clip_image002" width="244" height="188" border="0" /></a></p>
<p>By this mean, the appearance of a material is not changed. Only the appearance of body is changed.</p>
<p>The corresponding APIs are:</p>
<ul>
<li>BRepBody.material which is a Material object.</li>
</ul>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c83c3bd5970b-pi"><img style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" src="/assets/image_becd4d.jpg" alt="image" width="188" height="244" border="0" /></a></p>
<ul>
<li>BrepBody.appearance which is an Appearance object.</li>
</ul>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c83c3bd9970b-pi"><img style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" src="/assets/image_1cba46.jpg" alt="image" width="182" height="244" border="0" /></a></p>
<p>So, if we test with the following JavaScript code before overriding the appearance, it will tell the material is “Leather, weathered” and material’s appearance and body’s appearance are both “Leather, weathered”. After overriding, material is “Leather, weathered”, material’s appearance are not either changed, but the body’s appearance is “Glass - Heavy Color (Blue)” now.</p>
<pre>function run(context) {

&nbsp;&nbsp;&nbsp; "use strict"; 
&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp; if (adsk.debug === true) { 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; /*jslint debug: true*/ 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; debugger; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; /*jslint debug: false*/ 
&nbsp;&nbsp;&nbsp; }

&nbsp;&nbsp;&nbsp; var ui; 
&nbsp;&nbsp;&nbsp; try { 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var app = adsk.core.Application.get(); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ui = app.userInterface;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var product = app.activeProduct; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var design = adsk.fusion.Design(product); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; //get the first body of the root component 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var rootComp = design.rootComponent; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var body = rootComp.bRepBodies.item(0); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; //body material 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var bodyM = body.material; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ui.messageBox('body material is: ' + bodyM.name);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; //appearance with the material 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var MaterialA =bodyM.appearance; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ui.messageBox('material appearance is: ' + MaterialA.name); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; //body appearance 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; var BodyA = body.appearance; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ui.messageBox('body appearance is: ' + BodyA.name); 
&nbsp;&nbsp;&nbsp; } 
&nbsp;&nbsp;&nbsp; catch (e) { 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; if (ui) { 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ui.messageBox('Failed : ' + (e.description ? e.description : e)); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; } 
&nbsp;&nbsp;&nbsp; }&nbsp;&nbsp;&nbsp;&nbsp;

&nbsp;&nbsp;&nbsp; adsk.terminate(); 
} 
</pre>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c83c3be0970b-pi"><br /><img style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" src="/assets/image_eff569.jpg" alt="image" width="244" height="170" border="0" /></a>To change the material /appearance, you need to firstly get the material/appearance from the libraries, and set them to object.material or object.appearance.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1c66404970c-pi"><img style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" src="/assets/image_c8350a.jpg" alt="image" width="244" height="139" border="0" /></a></p>
<p>&nbsp;</p>
<pre>
function run(context) {

    "use strict"; 
    
    if (adsk.debug === true) { 
        /*jslint debug: true*/ 
        debugger; 
        /*jslint debug: false*/ 
    }

    var ui; 
    try { 
        var app = adsk.core.Application.get(); 
        ui = app.userInterface;

        var product = app.activeProduct; 
        var design = adsk.fusion.Design(product); 
        
        //get the first body of the root component 
        var rootComp = design.rootComponent; 
        var body = rootComp.bRepBodies.item(0); 
        
        //body material 
        var bodyM = body.material; 
        ui.messageBox('body material is: ' + bodyM.name);        
        //appearance with the material 
        var MaterialA =bodyM.appearance; 
        ui.messageBox('material appearance is: ' + MaterialA.name); 
        
        //body appearance 
        var BodyA = body.appearance; 
        ui.messageBox('body appearance is: ' + BodyA.name);         
        
        //get material lib 
        var mLibs = app.materialLibraries;                
        var mLib = mLibs.itemByName("Fusion 360 Material Library");         
        //get one material from this lib 
        var mOne = mLib.materials.itemByName("ABS Plastic"); 
        //change body material 
        body.material = mOne; 
        app.activeViewport.refresh(); 
        ui.messageBox('take a look at the screen after material is changed: ');        
        
        //get appearance lib 
        var appearanceLib1 = mLibs.itemByName("Fusion 360 Appearance Library");            
         //get one material from this lib 
        var aOne = appearanceLib1.appearances.itemByName("Fabric (Green)"); 
        //change body appearance 
        body.appearance = aOne; 
        app.activeViewport.refresh();  
        ui.messageBox('take a look at the screen after appearance is changed: ');        
       
    } 
    catch (e) { 
        if (ui) { 
            ui.messageBox('Failed : ' + (e.description ? e.description : e)); 
        } 
    }    
  

    adsk.terminate(); 
} 
</pre>
