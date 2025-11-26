---
layout: "post"
title: "Difference between Block and xRef in AutoLISP"
date: "2013-01-15 11:27:54"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/difference-between-block-and-xref-in-autolisp.html "
typepad_basename: "difference-between-block-and-xref-in-autolisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>After you know the name of the block, For example:</p>  <p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt"><span style="color: #ff0000">(</span><span style="color: #0000ff">setq</span> blkname <span style="color: #ff0000">(</span><span style="color: #0000ff">cdr</span>&#160;<span style="color: #ff0000">(</span><span style="color: #0000ff">assoc</span>&#160;<span style="color: #008000">2</span>&#160;<span style="color: #ff0000">(</span><span style="color: #0000ff">entget</span>&#160;<span style="color: #ff0000">(</span><span style="color: #0000ff">car</span>&#160;<span style="color: #ff0000">(</span><span style="color: #0000ff">entsel</span><span style="color: #ff0000">)</span><span style="color: #ff0000">)</span><span style="color: #ff0000">)</span><span style="color: #ff0000">)</span><span style="color: #ff0000">)</span><span style="color: #ff0000">)</span></span></p>  <p>you can pass it into (tblsearch), get the details of the block table record (whether for a normal block or an xref) using:</p>  <p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt"><span style="color: #ff0000">(</span>tblsearch <span style="color: #ff00ff">&quot;BLOCK&quot;</span> blkname<span style="color: #ff0000">)</span></span></p>  <p>This returns information from the block table, including the path of the xref (if applicable). The 70 group code differentiates blocks from xrefs, and the 1 group code gives you the path information.</p>
