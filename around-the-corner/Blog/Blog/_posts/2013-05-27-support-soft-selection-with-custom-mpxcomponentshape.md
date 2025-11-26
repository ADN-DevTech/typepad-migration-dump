---
layout: "post"
title: "Support Soft Selection with custom MPxComponentShape"
date: "2013-05-27 01:33:52"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Geometry"
  - "Maya"
  - "Modeling"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/05/support-soft-selection-with-custom-mpxcomponentshape.html "
typepad_basename: "support-soft-selection-with-custom-mpxcomponentshape"
typepad_status: "Publish"
---

<p>MPxComponentShape allows the implementation of new user-defined shapes using components, however all the UI related feature (such as selection/drawing) has to be implemented with another class MPxSurfaceShapeUI.</p>
<p>In the MPxSurfaceShapeUI class, there are select() and draw() functions you need to implement for your custom shape. The Maya devkit has 2 examples that demonstrates how to use this class: apiMeshShape and apiSimpleShape. The apiSimpleShape shows to how to use MPxSurfaceShapeUI to work with MPxComponentShape to achieve component selection and drawing. But this is not Soft Selection!</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0192aa5ee55d970d-pi" style="display: inline;"><img alt="SoftSelectIntro" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d0192aa5ee55d970d image-full" src="/assets/image_f052ef.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="SoftSelectIntro" /></a><br />Unfortunately there is no direct way to achieve Soft Selection in Maya by API. However there is a workaround in an in-direct way. The pseudo-code below shows how to get soft selection in component mode.</p>
<pre class="brush: python; toolbar: false;">softSelection = OpenMaya.MRichSelection()
OpenMaya.MGlobal.getRichSelection(softSelection)
selection = OpenMaya.MSelectionList()
softSelection.getSelection(selection)
pathDag = OpenMaya.MDagPath()
oComp = OpenMaya.MObject()
selection.getDagPath(0, pathDag, oComp)
fnComp = OpenMaya.MFnSingleIndexedComponent(oComp)
for i in range(fnComp.elementCount()):
  print fnComp.element(i), fnComp.weight(i).influence()
</pre>
<h3>
  
C++ API:</h3>
<p>The below enum and function are there in</p>
<ol>
<li>To set the component mode<br /> 
   In &quot;enum MSelectionMode&quot; set &quot;kSelectComponentMode&quot;</li>
<li>To get soft selection<br />static MStatus getRichSelection (MRichSelection &amp;dest, bool defaultToActiveSelection=true)</li>
</ol>
<p>&#0160;</p>
