---
layout: "post"
title: "Material Assets and FBX"
date: "2011-01-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "Data Access"
  - "FBX"
  - "Parameters"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/01/material-assets-and-fbx.html "
typepad_basename: "material-assets-and-fbx"
typepad_status: "Publish"
---

<p>Last year, we discussed analysing the FBX files exported by Revit to access the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/02/texture-data-uv-coordinates-and-fbx.html">
texture data UV coordinates</a> and 

other material and rendering information.

However, even something as simple as retrieving the material name from the FBX file is apparently no longer possible.
This has led to several questions such as the following:

<p><strong>Question:</strong> When using either the FBX SDK version 2010.2 or 2011.3.1, I can no longer read the material attached to an object from an FBX file exported by Revit 2011.  
Specifically, the call to node->GetMaterial(mat_id) returns NULL, and node->GetMaterialCount() returns 0.  

<p>However, this works perfectly well for FBX files exported by Revit 2010! 

<p>Therefore, this looks like a bug in the way Revit 2011 is exporting FBX files.  
It is no longer linking the materials with the objects as before.  
Regardless of what FBX SDK I use, either one encounters this problem.  

<p>The problem appears to be in the Revit FBX export code in Revit 2011, which is not exposed to us.
Can you help with this? 

<p><strong>Answer:</strong> I have some bad news and some good news for you on this topic. 
Let's start with the bad:

<p>The problem in FBX (and Revit and Max) is that the materials are now encrypted. 
This prevents anyone from reading the information in the FBX file. 
Autodesk applications having certain libraries can 'convert' the material, but third parties will not be able to get information on materials without going through the Autodesk applications and their APIs.

<p>That means that since you do not have the API to read the data embedded in the FBX file, you are stuck.

<p>This will be fixed eventually on the FBX side, but we are not expecting anything in the near future.

<p>On the other hand, since FBX is a completely separate product and context, I do not expect any FBX-specific features to be added to the Revit API.

<p>So far for the main cause and issue why the materials are not accessible from the Revit FBX export.

<p>Now for the good news, I hope:

<p>Although the access to the materials in the FBX file is probably not going to be resolved in the immediate future, there is some Revit API access to materials.

<p>As you probably know, the Revit SDK Materials sample shows how to read some material information from the Revit model.

<p>It makes use of the .NET TypeDescriptor technology, which is somewhat similar to Reflection, but different. 
Here is the explanation from MSDN:

<span style="color:darkblue">

<p>The .NET Framework provides two ways to access metadata on a type: the reflection API provided in the System.Reflection namespace, and the TypeDescriptor class. Reflection is a general mechanism available to all types because its foundation is established in the GetType method of the root Object class. The information it returns for a type is not extensible, in that it cannot be modified after compilation of the target type. For more information, see the topics in Reflection.

<p>In contrast, TypeDescriptor is an extensible inspection mechanism for components: those classes that implement the IComponent interface. Unlike reflection, it does not inspect for methods. TypeDescriptor can be dynamically extended by several services available through the target component's Site. The following table shows these services.

</span>

<p>The original Materials SDK sample uses the System.ComponentModel namespace, implements a custom type descriptor class RenderAppearanceDescriptor, derived from ICustomTypeDescriptor, and uses that to access certain material properties.
It demonstrates 4 main features:

<ol>
<li>How to get and set the parameters of materials in the current document.
<li>How to duplicate a material.
<li>How to change the material of a structural element.
<li>How to get and set a material's Render Appearance.
</ol>

<p>Here is an enhanced version of that sample, 

<span class="asset  asset-generic at-xid-6a00e553e1689788330147e20d6564970b"><a href="http://thebuildingcoder.typepad.com/files/materials2.zip">Materials2</a></span>,

which implements the following functionality:

<ul>
<li>Create a new pure red RGB material.
<li>Create a new metal chrome "bitmap" material from an existing one and change some qualities.
    You cannot change the bitmap or other "RenderApearance" aspects as they are stored generically as "Assets".
<li>Curved wall creation and compound structure layer material assignment.
<li>Furniture type creation and material assignment.
<li>Floor creation and material assignment.
</ul>

<p>It demonstrates some additional possibilities to access individual elements,
creates some new materials and building elements, lists the material properties, and assigns materials to the elements and individual sub-element parts. 
Here is a screen snapshot of the model created:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e20d6626970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e20d6626970b" alt="Materials2 model" title="Materials2 model" src="/assets/image_2a5ca8.jpg" border="0" /></a> <br />

</center>

<p>It contains a wall and a floor whose compound layers have been assigned a new red material, and a desk whose subcomponents have been assigned a new chrome metal material.

<p>The sample also displays a dialogue box listing some of the material asset properties that can be accessed:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c8168775970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c8168775970c" alt="Materials2 asset properties" title="Materials2 asset properties" src="/assets/image_152375.jpg" border="0" /></a> <br />

</center>

<p>The Revit API does not provide the geometry level access like mapping coordinates, etc., but it does allow you to read some additional material data within Revit.  
This access is not officially supported with direct Revit API calls. 

<p>Of course this does not resolve anything for the FBX exporter, but it might still be useful when used in conjunction with other tools.

<p>For example, you might be able to output the materials assigned to each object, then in FBX SDK remap them to the elements. 
Not being familiar with FBX SDK, I am not sure how easy that would be, e.g. what identifier to use when mapping the materials, and whether the mapping coordinates would be lost, etc. 
This should at least provide a starting point, however.
