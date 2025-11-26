---
layout: "post"
title: "For Revit IFC newbies"
date: "2012-07-06 00:08:00"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/07/for-revit-ifc-newbies.html "
typepad_basename: "for-revit-ifc-newbies"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>  <p>I never had chance to really look into IFC and therefore did not know much about Revit IFC. I had an opportunity to look into it recently when my colleague asked me how to read IFC files exported from Revit.</p>  <p>Thanks to Angel Velez in Revit engineering team for explaining me an answer as following. </p>  <p>The IFC format is text, but not intended to be human-readable and to understand without IFC knowledge. However, there are lines like </p>  <p>#13774=IFCRELCONTAINEDINSPATIALSTRUCTURE('1ItGejZxT16AtCzq9KgA$h',#14,$,    <br />$,(#64263,#64264,#64265,#12236,#64266,#12237,#64267,#64268,#64269,#64270)</p>  <p>That contains a list of all of the top-level entities in the file, usually contained in a base level. You can then track down those line numbers to get the top level IFCWALL, IFCDOOR, whatever. There is a tag for each of these that if it came from a Revit file, will be the element id.</p>  <p>A non-programmer could not be expected to write IFC code, other than very simple things.</p>  <p>For Revit IFC export feature in Revit products, Revit engineering decides how Revit elements are written out in the IFC format in combination with buildingSMART that publishes the requirements of how elements are supposed to be written out.&#160; </p>  <p>Here are the links of buildingSMART if you are interested.</p>  <p><a href="http://www.buildingsmart.com">http://www.buildingsmart.com</a>     <br /><a href="http://www.buildingsmart-tech.org/specifications/ifc-overview">http://www.buildingsmart-tech.org/specifications/ifc-overview</a></p>  <p>It is never a simple one-line for any element; there are many lines for a Revit element.</p>  <p>Autodesk Revit IFC Exporter code is available as Open Source Software at <a href="http://sourceforge.net/projects/ifcexporter">sourceforge.net/projects/ifcexporter</a>. Anyone who wanted to add to the Open Source would really have to:</p>  <p>1.&#160;&#160;&#160; Know Revit    <br />2.&#160;&#160;&#160; Know Revit API     <br />3.&#160;&#160;&#160; Know IFC     <br />4.&#160;&#160;&#160; Know how to program in C# .NET</p>  <p>Please visit Jeremy’s <a href="http://thebuildingcoder.typepad.com/blog/2011/09/revit-ifc-exporter-released-as-open-source.html">blog post for Revit IFC</a> for futher reading. </p>  <p>Well, I am glad if you now know what takes to become an expert. </p>
