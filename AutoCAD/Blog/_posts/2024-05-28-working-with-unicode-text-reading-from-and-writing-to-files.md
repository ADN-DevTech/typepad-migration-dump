---
layout: "post"
title: "Working with Unicode Text: Reading from and Writing to Files"
date: "2024-05-28 18:26:00"
author: "Madhukar Moogala"
categories:
  - "LISP"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/05/working-with-unicode-text-reading-from-and-writing-to-files.html "
typepad_basename: "working-with-unicode-text-reading-from-and-writing-to-files"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst">
    </script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>Before AutoCAD 2021, AutoLisp/VLisp was not fully Unicode compliant.</p>
  <p>AutoCAD 2021 introduced a new system variable,
    <code><a href="https://help.autodesk.com/view/ACD/2024/ENU/?guid=GUID-1853092D-6E6D-4A06-8956-AD2C3DF203A3">LISPSYS</a></code>,
    which fully supports AutoLisp functions with Unicode
    characters.
  </p>
  <p>Here's how to read a text file with Unicode characters and create an MTEXT entity with the parsed contents:</p>
  <p>Sample text file with Unicode characters:</p>
  <textarea>    こんにちは、良い一日を
  </textarea>
  <p>The following AutoLisp code reads the text file and creates an MTEXT entity</p>
  <pre class="prettyprint lang-lisp">  (defun c:drawtext()
   (setq f (open "d:/temp/r.txt" "r" "utf8"))
   (setq mytext (read-line f))
   (setq mypoint (list 1.0 1.0 0.0)
         myotherpoint (list -0.5 2.0 0.0)
         mycolor "2" ; red
         myrot 90.0
         currcolor (getvar "CECOLOR")
   )
   (setvar "CECOLOR" mycolor)
   (command "mtext" mypoint "R" myrot "@" mytext "")
   (setvar "CECOLOR" currcolor)
   (close f)
  )
  </pre>
  <p><b>Note:</b>We use <mark>utf8</mark>encoding, which tells AutoLISP to open a Unicode stream for reading and
    writing.</p>
  <p>Now, similarly, we can write the Unicode contents of an MTEXT entity to a file:</p>
  <pre class="prettyprint lang-lisp">  (defun c:writetext()
  (setq f (open "d:/temp/w.txt" "w" "utf8"))
  (setq txt (vla-get-textstring (vlax-ename-&gt;vla-object (car (entsel "Pick mtext")))))
  (write-line txt f)
  (close f)
  )</pre>
  <p>Here's the result:</p>
  <pre class="prettyprint lang-lisp"><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b0ddbc200c-pi"><img width="157" height="356" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_188772.jpg" border="0"></a>
  </pre>
