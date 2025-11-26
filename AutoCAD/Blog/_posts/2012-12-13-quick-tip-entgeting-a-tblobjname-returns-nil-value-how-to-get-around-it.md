---
layout: "post"
title: "Quick tip: entget'ing a tblobjname returns nil value. How to get around it?"
date: "2012-12-13 17:02:24"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/quick-tip-entgeting-a-tblobjname-returns-nil-value-how-to-get-around-it.html "
typepad_basename: "quick-tip-entgeting-a-tblobjname-returns-nil-value-how-to-get-around-it"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Why is the return value of the (entget) function nil after updating dimension entities?</p>  <p>This happens because of a known problem with tlbobjname lisp function. Consider the following code:</p>  <pre>(setq ent (entget (tblobjname &quot;block&quot; &quot;*D1&quot;))<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; det (entget (cdr (assoc -2 ent)))<br />)<br />(print det)<br /></pre>

<p>At the command prompt type the following:
  <br /><font color="#800080"> Command: Dim
    <br /> Dim: upd

    <br /> Select objects: all

    <br /> Select objects:</font></p>

<p>Now execute the above lisp code again and you will notice a nil return value.&#160; This is due to the known problem with 'tblobjname' function.&#160; The workaround is to use 'tblsearch' instead: </p>

<pre>(setq ent (tblsearch &quot;block&quot;&#160; &quot;*D1&quot;)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; det (entget (cdr (assoc -2 ent)))<br />)<br />(print det)<br /></pre>
