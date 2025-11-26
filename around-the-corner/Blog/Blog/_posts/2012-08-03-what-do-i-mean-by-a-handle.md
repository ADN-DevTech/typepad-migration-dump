---
layout: "post"
title: "What do I mean by a “handle”…?"
date: "2012-08-03 02:08:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "FBX"
  - "Kristine Middlemiss"
  - "Maya"
  - "MotionBuilder"
  - "Mudbox"
  - "Python"
  - "SoftImage"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/what-do-i-mean-by-a-handle.html "
typepad_basename: "what-do-i-mean-by-a-handle"
typepad_status: "Publish"
---

<p>Through my years at Autodesk/Alias, I have done a lot of teaching and education (some is recorded and some is not), but some things you might catch me saying during training are… “Now you get the handle to the object”, or “this now returns the handle to the object”, but what the heck does that really mean? I will know try to explain my colloquialism below using MotionBuilder Python SDK code as an example.</p>
<h2>A Handle in Programming Terms</h2>
<p>What I mean by a handle is what the constructor of a class returns when you create an object.</p>
<p>For example:</p>
<p><span style="color: #008000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; from</span> <span style="color: #0000ff;">pyfbsdk</span> <span style="color: #008000;">import</span> <span style="color: #666666;">*</span></p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lModel <span style="color: #666666;">=</span> FBModelCube(<span style="color: #ba2121;">&quot;Kristine&quot;</span>)</p>
<p>Here lModel is&#0160;the &quot;handle&quot; to&#0160;the FBModelCube object instance/copy. In this specific case you have your class definition of FBModelCube which would has been originally created by the MotionBuilder developers (we only let you create copies of this class, so you do not mess with the original version).</p>
<p>Once you have a &quot;handle&quot; to the object you want to have in your scene/file,&#0160;you can use the class functions and attributes on your new copy to manipulate your copy.</p>
<p>The concept &quot;handle&quot; is more of a visual naming convention than anything else, think of it like a door handle to hold onto the copy/instance of your FBModelCube object so that you can work with it.&#0160;</p>
<p>See this will still create an object:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FBModelCube(<span style="color: #ba2121;">&quot;Kristine&quot;</span>)</p>
<p>But since you didn’t assign a variable to hold the handle to your object you would not be able to access it for further manipulation.</p>
<p>But if you choose to use a function from the instance of the copy such as GetSelectedPointsCount (), like this:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pointCount = lModel.GetSelectedPointCount()</p>
<p>pointCount is not a handle to the instance, it is just an integer holding the SelectedPointCount, where lModel is the handle to the object.</p>
<p>Just to clarify, handles can only be created when you create an instance of the class in the Python Reference Guide, such as FBModelCube. The documentation called these ‘constructors’.</p>
<p>This concept is applicable to any object oriented programming language, this would fall into MotionBuilder SDK, Maya API, Mudbox SDK, FBX SDK, and Softimage SDK (and 3ds Max SDK)!</p>
<p>Enjoy,</p>
<p>Kristine</p>
