---
layout: "post"
title: "Find elements in Revit based on the elements exported in DWF"
date: "2012-07-31 03:55:15"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/07/find-elements-in-revit-based-on-the-elements-exported-in-dwf.html "
typepad_basename: "find-elements-in-revit-based-on-the-elements-exported-in-dwf"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p><b>Issue</b></p>  <p>We are trying to find elements in&#160; Revit based on the exported DWF via the GUID in the DWF export. I cannot find any elements using this, how can we find the elements using the GUID from the exported DWF ?</p>  <p>&#160;</p>  <p><b>Solution</b></p>  <p>The element GUID in DWF is different from the same element’s GUID in Revit. So it is not possible to retrieve the element in Revit according to the GUID obtained from DWF file.    <br />However, each element in the exported DWF file has the property Id. We can use this Id value to retrieve element in Revit.Here is the pseudo-code.</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// the argument idFromDWF is obtained from dwf file.</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">ElementId</span><span style="line-height: 140%"> id = </span><span style="line-height: 140%; color: blue">new</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">ElementId</span><span style="line-height: 140%">(idFromDWF);</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: #2b91af">Element</span><span style="line-height: 140%"> elem = doc.get_Element(id); </span></p> </div>
