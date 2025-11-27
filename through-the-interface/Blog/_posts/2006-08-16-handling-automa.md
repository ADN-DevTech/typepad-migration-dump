---
layout: "post"
title: "Handling Automation errors in Visual LISP"
date: "2006-08-16 14:10:57"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoLISP / Visual LISP"
original_url: "https://www.keanw.com/2006/08/handling_automa.html "
typepad_basename: "handling_automa"
typepad_status: "Publish"
---

<p>Since Visual LISP was introduced, developers have taken advantage of its ability to call COM Automation interfaces (whether AutoCAD's or other applications'). The addition of this functionality to the LISP platform created many new development possibilities - previously you were able to call through to ObjectARX applications defining LISP functions, but enabling Automation access from LISP suddenly allowed developers to access any other application adopting the COM standard its API, such as Microsoft Excel.</p>

<p>A quick note on error handling in LISP...</p>

<p>Traditionally LISP applications have defined their own (*error*) function to trap errors during execution. During this function they often report the value of ERRNO - used by AutoCAD to tell the LISP app what kind of error has occurred - which in turn can help the user or developer pin down the cause of the problem. This is fine, but this kind of global error-trap doesn't make it easy to resume execution after the error.</p>

<p>When using Automation interfaces things are different. Automation clients generally need to trap exceptions as they occur, rather than defining a global error-handling function. Visual LISP enables this with a very helpful function named (vl-catch-all-apply).</p>

<p>(vl-catch-all-apply) is like the (apply) function, in that it takes a symbol representing the function name to be called as the first argument, followed by the various arguments to be passed to that function in the form of a list. (vl-catch-all-apply) executes the function call, and does its best to trap any errors that occur during it. The main difference between the function signatures of (apply) and (vl-catch-all-apply) is with the return value, which will either be the return value of the function call, if all works well, or an error object that can then be queried for additional information. </p>

<p>Let's take a simple example that doesn't involve Automation, which I've basically stolen from the Visual LISP online help. The following code asks the user to enter two numbers using my favourite LISP function, (getreal) :-), and then tries to perform a division. We check result of the division with (vl-catch-all-error-p), to see whether it succeeded or not: if it didn't we then use (vl-catch-all-error-message) to get an error string telling us what happened.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">(defun c:div (/ first second result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (setq first (getreal &quot;\nEnter the first number: &quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; second (getreal &quot;\nEnter the second number: &quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; result (vl-catch-all-apply '/ (list first second))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (if (vl-catch-all-error-p result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (princ (strcat &quot;\nCaught an exception: &quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (vl-catch-all-error-message result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (princ (strcat &quot;\nSuccess - the result is &quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (rtos result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (princ)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">)</p></div></div>

<p>Here's what happens when you run the command:</p><blockquote dir="ltr"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: div</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Enter the first number: 50</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Enter the second number: 2</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Success - the result is 25.0000</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: div</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Enter the first number: 50</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Enter the second number: 0</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Caught an exception: divide by zero</p></div></blockquote><p>So how does this technique apply to Automation calls? Let's take a look at another piece of code, this time calling a function to check the interference between two solids. This code defines a CHECKINT command that asks for two solids to be selected. Assuming two solids are selected, it will call the CheckInterference Automation method, specifying that any resulting intersection should be created as its own solid.</p>

<p>A lot of work was done to enhance the solids capabilities in AutoCAD 2007, so although this function works find in 2007, in prior releases it would often return modelling errors. This code allows it to fail gracefully even in prior releases.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">; Helper function to check whether an entity is a solid</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">(defun is-solid-p (ename)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (= (cdr (assoc 0 (entget ename))) &quot;3DSOLID&quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">; The CHECKINT command</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">(defun c:checkint (/ first second e1 e2 result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (vl-load-com)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (setq first&nbsp; (entsel &quot;\nSelect the first solid: &quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; second (entsel &quot;\nSelect the second solid: &quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (if (and first</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; second</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (is-solid-p (setq e1 (car first)))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (is-solid-p (setq e2 (car second)))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (progn</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(setq result (vl-catch-all-apply</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 'vla-CheckInterference</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (list (vlax-ename-&gt;vla-object e1)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (vlax-ename-&gt;vla-object e2)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; :vlax-true</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(if (vl-catch-all-error-p result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (princ (strcat &quot;\nCaught an exception: &quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (vl-catch-all-error-message result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (progn</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (princ &quot;\nSuccess!&quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ; Highlight the newly created intersection solid</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (vla-Highlight result :vlax-true)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (vlax-release-object result)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (princ &quot;\nMust select two solids.&quot;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; (princ)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">)</p></div>
