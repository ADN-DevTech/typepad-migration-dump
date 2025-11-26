---
layout: "post"
title: "Handling Duplicate Types on CopyElements"
date: "2014-08-19 14:27:43"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2014/08/handling-duplicate-types-on-copyelements.html "
typepad_basename: "handling-duplicate-types-on-copyelements"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>When using the <strong>ElementTransformUtils.CopyElements</strong> you may have the ‘Duplicate Types’ warning, as shown below.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73e04d6e5970d-pi"><img title="duplicated_types" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="duplicated_types" src="/assets/image_868119.jpg" width="388" height="231" /></a> </p>  <p>In fact this method have one override option to pass a CopyPasteOptions parameter that enable us to control how Revit will react when this happens. We can return one of the following: </p>  <ul>   <li><strong>UseDestinationTypes</strong>: Proceed with the paste operation and use the types with the same name in the destination document.&#160; </li>    <li><strong>Abort</strong>: Cancel the paste operation.&#160; </li> </ul>  <p>To implement this, first create a new class that implements the interface IDuplicateTypeNamesHandler. This class should have one method that returns the action. In the sample below, it will use destination types.</p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: blue">public</span>&#160;<span style="color: blue">class</span>&#160;<span style="color: #2b91af">CustomCopyHandler</span> : <span style="color: #2b91af">IDuplicateTypeNamesHandler</span><br />{<br />&#160; <span style="color: blue">public</span>&#160;<span style="color: #2b91af">DuplicateTypeAction</span> OnDuplicateTypeNamesFound(<br />&#160;&#160;&#160; <span style="color: #2b91af">DuplicateTypeNamesHandlerArgs</span> args)<br />&#160; {<br />&#160;&#160;&#160; <span style="color: blue">return</span>&#160;<span style="color: #2b91af">DuplicateTypeAction</span>.UseDestinationTypes;<br />&#160; }<br />}</pre>

<p>Then, at the CopyElements call, create a object of the type CopyPasteOptions and set the handler.</p>

<pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: green">// create an instance</span><br /><span style="color: #2b91af">CopyPasteOptions</span> copyOptions = <span style="color: blue">new</span>&#160;<span style="color: #2b91af">CopyPasteOptions</span>();<br />copyOptions.SetDuplicateTypeNamesHandler(<span style="color: blue">new</span>&#160;<span style="color: #2b91af">CustomCopyHandler</span>());<br /> <br /><span style="color: green">// now the copy</span><br /><span style="color: #2b91af">ElementTransformUtils</span>.CopyElements(doc1, ids, doc2, <span style="color: #2b91af">Transform</span>.Identity, copyOptions);</pre>

<p>&#160;</p>

<p>Thanks <a href="http://forums.autodesk.com/t5/Revit-API/How-to-implement-IDuplicateTypesNameHandler-for-the/m-p/4926792#M5995">Arnošt Löbel for his post at our forums</a>.</p>
