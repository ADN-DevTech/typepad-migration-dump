---
layout: "post"
title: "Using DIMPOST in a Lisp routine"
date: "2014-03-18 17:41:18"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2014/03/using-dimpost-in-a-lisp-routine.html "
typepad_basename: "using-dimpost-in-a-lisp-routine"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>    <br />I'm trying to change DIMPOST to \X in a lisp routine. The program will not accept \X in a lsp program. I'm want to change Dimstyle using the set up; Method set to NONE for both primary(Metric) and alternate units(English) and alternate unit placement under the primary units. I can do this manually but not in the lisp. </p>  <p><strong>Solution     <br /></strong>With these Lisp statements, the dimension formatting were as requested by the developer :</p>  <p>&lt;&lt;&lt; </p>  <p>; To reset the alternative dimension prefix and suffix</p>  <p>(command-s &quot;DIMAPOST&quot; &quot;.&quot;)</p>  <p>; To reset the primary dimension prefix and suffix</p>  <p>(command-s &quot;DIMPOST&quot; &quot;.&quot;)</p>  <p>; To set the orientation of the alternative dimension over the primary dimension </p>  <p>(setvar &quot;DIMPOST&quot; &quot;<a href="file:///\\X">\\X</a>&quot;)</p>  <p>&gt;&gt;&gt; </p>
