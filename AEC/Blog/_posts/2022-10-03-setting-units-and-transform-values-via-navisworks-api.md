---
layout: "post"
title: "Setting \"Units and Transform\" values via Navisworks API"
date: "2022-10-03 23:58:27"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2022/10/setting-units-and-transform-values-via-navisworks-api.html "
typepad_basename: "setting-units-and-transform-values-via-navisworks-api"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p><strong>Question</strong>: I&#39;m trying to modify the Model Units, Origin, Rotation, and scale values of each appended 3D model. Is it possible to change these values via Navisworks API?</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02acc60e872c200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="float: left;"><img alt="NW" class="asset  asset-image at-xid-6a0167607c2431970b02acc60e872c200b img-responsive" height="448" src="/assets/image_839102.jpg" style="margin: 0px 5px 5px 0px;" title="NW" width="308" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p><strong>Answer</strong>: Yes, it is possible to change these values using SetModelUnitsAndTransform Method. Please take a look at the below sample code</p>
<pre class="prettyprint"><span style="font-size: 10pt; font-family: &#39;courier new&#39;, courier;">  
  DocumentModels models = doc.Models;
//Get the required model from DocumentModels
  Model model;

  Transform3D oldTransform3d=model.Transform;
  Transform3DComponents transform3dComponents = oldTransform3d.Factor();

//Get Values
  Vector3D originVector3D = transform3dComponents.Translation;
  Vector3D scaleVector3D = transform3dComponents.Scale;
  Rotation3D rotationVector3D = transform3dComponents.Rotation;

//Set Values
  transform3dComponents.Translation = new Vector3D(origin_X, origin_Y, origin_Z);<br />  //Eg: new Vector3D(10,10,10);
  <br />  transform3dComponents.Rotation = new Rotation3D(new UnitVector3D(0, 0, 1), 0.872665);<br />  //Here 50 degree=0.872665 radian
  <br />  transform3dComponents.Scale = new Vector3D(scale_X, scale_Y, scale_Z);<br />  //Eg: new Vector3D(2,2,2);
  <br />  Transform3D newTransform3D = transform3dComponents.Combine();

//Change model units value
  Units units = Units.Meters;

  models.SetModelUnitsAndTransform(model, units , newTransform3D, true);

</span></pre>
