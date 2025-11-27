---
layout: "post"
title: "Get ComponentDefinition of suppressed component in BOM"
date: "2012-08-28 08:11:40"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/get-componentdefinition-of-suppressed-component-in-bom.html "
typepad_basename: "get-componentdefinition-of-suppressed-component-in-bom"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue</strong>    <br />My code accesses BOM of the assembly and check the ComponentDefinition of each BOM row. It works well with the&#160; Master view, but in an LOD view, it failed to check the BOM row whose component is suppressed . </p>  <p><strong>Solution     <br /></strong>The suppressed component will not have the ComponentDefinitions value on the BOMRow object. This is as designed. The statement in Inventor API document says:     <br /><em>Suppression</em>     <br />An instance in an assembly can now be suppressed for capacity gains. This means that the connection to the referenced document from the particular instance is ignored, and the referenced document is available to be closed if otherwise unreferenced. Any APIs that traverse across that instance to the referenced document will not be able to succeed. This would include operations on ComponentOccurrence such as the Definition, SubOccurrences, and SurfaceBodies properties. Binding reference keys or obtaining the attached entities of an Attribute that reside across the suppressed connection will likewise be impossible to satisfy.     <br />A suppressed link will behave much as an unresolved link did in previous releases. An application would usually want to treat a suppressed connection much as they would have treated an unresolved reference in previous releases. The issue is that many applications likely did not pay much attention to unresolved dependency situations, but now with suppression it will be much more common, and may occur during the working lifetime of the referencing document instead of only at open time. </p>  <p>   <br />The workaround is to obtain the BOMRow.ReferencedFileDescriptor.ReferencedFile.AvailableDocuments(1).ComponentDefinition instead when failing to get the ComponentDefinitions from the BOMRow object. </p>
