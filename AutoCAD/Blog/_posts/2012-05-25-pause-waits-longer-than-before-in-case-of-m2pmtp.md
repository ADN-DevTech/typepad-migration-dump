---
layout: "post"
title: "PAUSE \\ waits longer than before in case of m2p/mtp"
date: "2012-05-25 10:43:47"
author: "Wayne Brill"
categories:
  - "AutoCAD"
  - "LISP"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/pause-waits-longer-than-before-in-case-of-m2pmtp.html "
typepad_basename: "pause-waits-longer-than-before-in-case-of-m2pmtp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p><b>Issue</b></p>  <p>I have a Macro for one of my commands and it worked fine before, but in AutoCAD 2010 the PAUSE seems to wait longer, not only for a single point aquisition as in AutoCAD 2009.</p>  <p>I have this Macro:</p>  <pre>^C^C_line m2p;endp;\endp;</pre>

<p>In AutoCAD 2010 if Object Snap is turned off and you start the above macro then you can see that the first end point acquisition of m2p is going well (the end point of the entities under the cursor are shown), but during the second end point acquisition of m2p the end points are not highlighted. It seems that the PAUSE is still on, and so the second endp has not been reached. </p>

<p>How could I make this work as before, so that \ would only wait for a single point acquisition?</p>

<p><a name="section2"></a></p>

<p><b>Solution</b></p>

<p>In case of the new behaviour PAUSE \ waits for the whole m2p to finish, instead of waiting for just a single intermediate point acquisition. This is more consistent with other similar functions like tan/appint.</p>

<p>If you use the following Lisp code then the result will always be predictable in AutoCAD 2010 and above - PAUSE will wait for the first point acqusition of the LINE command to finish no matter how the user aquires it:</p>

<pre>(command &quot;_.line&quot; pause &quot;5,5&quot; &quot;&quot;)</pre>

<ol>
  <li>Run the above Lisp code </li>

  <li>Type in m2p </li>

  <li>Pick the first point for m2p &gt;&gt; in AutoCAD 2009 the LINE command will exit without creating a line entity, whereas in the newer releases the LINE command will continue by passing in &quot;5,5&quot; as the second point of the line, and the entity will be created successfully </li>
</ol>

<p>As a workaround your Macro could set the OSMODE value directly:</p>

<pre>^C^C(setq osm (getvar &quot;OSMODE&quot;));(setvar &quot;OSMODE&quot; 1);_line m2p;\\;(setvar &quot;OSMODE&quot; osm);</pre>

<p>This switches on End Point Snap temporarily, instead of using endp. </p>
