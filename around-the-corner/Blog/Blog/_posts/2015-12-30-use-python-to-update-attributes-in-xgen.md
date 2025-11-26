---
layout: "post"
title: "Use python to update attributes in XGen"
date: "2015-12-30 17:48:51"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2015/12/use-python-to-update-attributes-in-xgen.html "
typepad_basename: "use-python-to-update-attributes-in-xgen"
typepad_status: "Publish"
---

<p>Last week, somebody asked me how to update attributes in XGen. I found that there isn’t many XGen Python API examples.</p>
<p>Some of XGen’ source code is included in your Maya folder &lt;maya&gt;\plug-ins\XGen\scripts\xgenm. It is always a good idea to take a look there for reference.</p>
<p>The XGen Python module is called xgenm. We’ll use the functions inside this package to update the XGen descriptors.</p>
<p>If you read the <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=GUID-33ECC43B-5CF6-4BE1-8EAE-8C6C0D698020">document</a> in our website, you can find xgenm.xgGlobal is mentioned. It can be used to check whether XGen is present and get the singleton reference of the description editor: xgenm.xgGlobal.DescriptionEditor.</p>
<p>There is a naming convention between XGen and Python API. The palettes in the Python refers to the collections of XGen.</p>
<p>The biggest challenge here is there is no detailed documentation for the user to get the attribute name in XGen. For example, for the Spline Primitives, attribute “Control Using” which determines using attribute or guides to create primitives in XGen becomes an enumerate attribute “iMethod” and accepts 0(attribute) and 1(guides).</p>
<p>I wrote a small piece of script to print active attributes and their values in a XGen descriptor. So we can find the attribute name we want by comparing the values.</p>
<blockquote>
<p>import xgenm as xg <br />import xgenm.xgGlobal as xgg<br />import xgenm.XgExternalAPI as xge</p>
<p><br /> <br />if xgg.Maya: <br /> <br />&#0160;&#0160;&#0160; #palette is collection, use palettes to get collections first. <br />&#0160;&#0160;&#0160; palettes = xg.palettes() <br />&#0160;&#0160;&#0160; for palette in palettes: <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print &quot;Collection:&quot; + palette <br /> <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; #Use descriptions to get description of each collection <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; descriptions = xg.descriptions(palette) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; for description in descriptions: <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print &quot; Description:&quot; + description <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objects = xg.objects(palette, description, True) <br /> <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; #Get active objects,e.g. SplinePrimtives <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; for object in objects: <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print &quot; Object:&quot; + object <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; attrs = xg.allAttrs(palette, description, object) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; for attr in attrs: <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print &quot; Attribute:&quot; + attr + &quot;, Value:&quot; + xg.getAttr(attr, palette, description, object) </p>
</blockquote>
<p>To modify an attribute, we need to provide palette(collection) name, descriptor name, object name and the attribute name in string.</p>
<p>For example, if we want to change the “Control Using” attribute of Spline Primitive object, we should write like this.</p>
<blockquote>
<p><br />#iMethod = Control Using, 0:Attribute, 1:Guides <br />xg.setAttr(&quot;iMethod&quot;,xge.prepForAttribute(&quot;1&quot;),&quot;collection&quot;, &quot;description&quot;, &quot;SplinePrimitive&quot;) </p>
</blockquote>
<p>You&#39;ll need to call xgenm.XgExternalAPI.prepFroAttribute to escape &quot;\\n&quot;, &#39;\n&#39;, &quot;\\t&quot; and &#39;\t&#39; to &quot;\\\\n&quot;, &quot;\\n&quot;, &quot;\\\\t&quot; and &quot;\\t&quot; for file path and expressions.</p>
<p>However, you cannot change object by using this function. For example, if I am using to switch from Card Primitive to Spline Primitive, we should use the setActive function.</p>
<blockquote>
<p><br />#if you don&#39;t want to override the previewing in viewport, please use False by default <br />xg.setActive(&quot;collection&quot;, &quot;description&quot;, &quot;SplinePrimitive&quot;, False) </p>
</blockquote>
<p>If you tried to run the code above, you may find that nothing happened in the description editor. The XGen core and the UI are separated; but renderman for example will use the updated values. To make things more clear. We still need to update the description editor to see the update in the UI.</p>
<blockquote>
<p><br />#Get the description editor first. <br />de = xgg.DescriptionEditor <br /> <br />#Do a full UI refresh <br />de.refresh(&quot;Full&quot;) </p>
</blockquote>
<p>It will refresh the description editor with the latest updates.</p>
<p>This is it :)</p>
