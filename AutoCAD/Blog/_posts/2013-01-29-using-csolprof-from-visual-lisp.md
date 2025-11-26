---
layout: "post"
title: "Using c:solprof from Visual LISP"
date: "2013-01-29 12:45:27"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/using-csolprof-from-visual-lisp.html "
typepad_basename: "using-csolprof-from-visual-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>For a properly selection set <strong>ss1</strong>, the next information required is the data for the prompt (&quot;Display hidden profile lines on separate layer?&quot;). This results in the 2nd (&quot;Project profile lines onto a plane?&quot;) and 3rd (&quot;Delete tangential edges?&quot;) prompts being answered with &quot;Y&quot;. We can substitute 1 and 0 to represent &quot;Y&quot; and &quot;N&quot;, respectively, the tangential edges are retained:</p>  <p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt"><span style="color: #ff0000">(</span><span style="color: #0000ff">setq</span> err <span style="color: #ff0000">(</span><span style="color: #0000ff">vl-catch-all-apply</span> 'c:solprof <span style="color: #ff0000">(</span>list ss1 <span style="color: #008000">1</span>&#160;<span style="color: #008000">1</span>&#160;<span style="color: #008000">0</span><span style="color: #ff0000">)</span><span style="color: #ff0000">)</span><span style="color: #ff0000">)</span></span></p>
