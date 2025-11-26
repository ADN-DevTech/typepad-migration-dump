---
layout: "post"
title: "Performance Issues When Copying/Arraying Text Entities in a Stamped OEM Application"
date: "2013-01-03 17:32:03"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/performance-issues-when-copyingarraying-text-entities-in-a-stamped-oem-application.html "
typepad_basename: "performance-issues-when-copyingarraying-text-entities-in-a-stamped-oem-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might find that when you create an array of a block reference with some preset attributes your Stamped OEM Application takes more time to array the objects than AutoCAD or AOEM.EXE.</p>  <p>The most common reason for the difference in performance is the missing font file that the ATTDEF's text style is currently using. AutoCAD tries to search for the missing fonts (in the defined search paths) whenever Attribute Reference or TEXT entities are created, so that it can display them with the specified font. By default your Stamped OEM Application does not have the font files installed with it. So, the solution is to place the required font files in the font directory (or in the support directory) and modify the product's search path to include the font directory. This can be done while creating the Stamped OEM Application using the Make Wizard.</p>  <p>To test the effect of this on the performance of your application, try the following:</p>  <p>1) Add the following LISP code in aoem.lsp and compile the aoem.lsp.</p>  <p>2) Add the command &quot;_arraytest&quot; in the Make Wizard while creating your stamped application.</p>  <p>3) Invoke the Stamped OEM Application and open a drawing that has a block with a preset Attribute that references a font that is not installed.</p>  <p>4) Type the command &quot;arraytest&quot; and select the newly created block reference</p>  <p>(defun c:arraytest( / secEnd secStart)   <br /> (prompt &quot;\nSelect a entity to array 100 X 100&quot;)</p>  <p>&#160;&#160; (princ)   <br />&#160;&#160;&#160;&#160;&#160;&#160; (setq pss(ssget))    <br />&#160;&#160;&#160;&#160;&#160;&#160; (setq s (getvar &quot;DATE&quot;))    <br />&#160;&#160;&#160;&#160;&#160;&#160; (setq secStart(* 86400.0 (- s (fix s))))</p>  <p>&#160;&#160;&#160;&#160;&#160;&#160; (setenv &quot;MaxArray&quot; (itoa (* 100 100)))   <br />&#160;&#160;&#160;&#160;&#160;&#160; (command &quot;-array&quot; pss &quot;&quot; &quot;r&quot; 100 100 100 100)</p>  <p>&#160;&#160;&#160;&#160;&#160;&#160; (setq s (getvar &quot;DATE&quot;))   <br />&#160;&#160;&#160;&#160;&#160;&#160; (setq secEnd (* 86400.0 (- s (fix s))))</p>  <p>&#160;&#160;&#160;&#160;&#160;&#160; (princ &quot;\nTime taken :&quot;)   <br />&#160;&#160;&#160;&#160;&#160;&#160; (princ (- secEnd secStart))    <br />&#160;&#160;&#160;&#160;&#160;&#160; (princ)    <br />)</p>
