---
layout: "post"
title: "Attribute Utility"
date: "2009-09-23 21:56:40"
author: "Adam Nagy"
categories:
  - "Attributes"
  - "Brian"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2009/09/attribute-utility.html "
typepad_basename: "attribute-utility"
typepad_status: "Publish"
---

<p>Working with attributes can sometimes be a bit frustrating because the only access you have to attributes is through the API.&#0160; You can’t quickly create, edit, or view an attribute without writing a program to do it.&#0160; Because of this I became a bit frustrated when working on my first program that made extensive use of attributes.&#0160; For the program to function correctly the attributes it was using all had to be correct and it was difficult to debug problems to determine if the problem was my program or the attributes it was trying to access.</p>
<p>For my own sanity I wrote a little utility that would let me view all of the attributes in a document.&#0160; That helped a lot but I never felt it was finished because it only allowed me to view existing attributes and I still couldn’t easily create or edit an existing attribute without writing code.&#0160; As part of the series of blog posts I’ve I’m writing about <a href="http://modthemachine.typepad.com/my_weblog/2009/07/introduction-to-attributes.html">attributes</a>, I’ve completely re-written the utility so that it provides full creation, edit, and viewing of attributes.&#0160; This new version of the utility is an add-in.&#0160; To use it just <a href="http://modthemachine.typepad.com/files/AttributeHelperSetup.zip">download</a> and install the add-in and look at the readme that’s installed for instructions on how to use it.&#0160; When installing you have the option of also installing the source code, if you’re interested in the internals of how it works.&#0160; It was written using the Visual Basic language in Visual Studio 2008.</p>
<p>Let me know if you have suggestions or find any problems.</p>
<p>---------------------------</p>
<p>Update (Aug. 10, 2015) - I&#39;ve updated this utility.&#0160; The update can be found <a href="http://modthemachine.typepad.com/my_weblog/2015/08/attribute-helper-update.html" target="_self" title="here">here</a>.</p>
