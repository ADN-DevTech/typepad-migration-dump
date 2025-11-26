---
layout: "post"
title: "What is &lsquo;&lt;&lt;&rsquo; in AutoCAD Angle Inputs? A Forgotten Tip from the Past"
date: "2025-02-10 15:17:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2025/02/what-is-in-autocad-angle-inputs-a-forgotten-tip-from-the-past.html "
typepad_basename: "what-is-in-autocad-angle-inputs-a-forgotten-tip-from-the-past"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>We have received a query from an A D N partner about usage of <code>&lt;&lt;</code> in acedCommand</p>
  <pre class="prettyprint">    <code>
      acedCommand (RTSTR, ACRX_T ("_.UCS"), 
      RTSTR, ACRX_T ("_Z"), 
      RTSTR, ACRX_T ("&lt;&lt;90d"), 
    0);
    </code>
  </pre>
  <p> This post attempts to explain this mysterious quirk from the past!</p>
  <p>The double left angle brackets denote clockwise or counterclockwise rotation when you rotate an entity. They make
    an important role when you set the angle measurement units to DMS (Degree, Minute, Second).</p>
  <p><strong>When the direction of the angle is set to South, 6 o'clock:</strong></p>
  <p><strong>"&lt;&lt;" will override to North, 12 o'clock.</strong></p>
  <p>Direction for angle 0°0'0":</p>
  <ul>
    <li>East (3 o'clock) = 0°0'0"</li>
    <li>North (12 o'clock) = 90°0'0"</li>
    <li>West (9 o'clock) = 180°0'0"</li>
    <li>South (6 o'clock) = 270°0'0"</li>
  </ul>
  <p>Let's see an example:</p>
  <p>First, let's set up the AutoCAD units system to measure angles in DMS and set South to 6 o'clock.</p>
  <pre class="prettyprint lang-lisp"><code>(command "_units" "" "" "2" "" "270" "_Y")</code></pre>
  <p><br></p>
  <p>Draw a line. // NOTE: The magenta circle is for reference.</p>
  <p><br></p>
  <pre class="prettyprint lang-lisp"><code>
    (setq StartPt '(2 2 0))
    (setq EndPt '(10 2 0))
    (command "_line" StartPt EndPt "")
  </code>
  </pre>
  <p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f9477d200d-pi"><img width="244" height="111" title="dms_angle_override_1" style="display: inline; background-image: none;" alt="dms_angle_override_1" src="/assets/image_297416.jpg" border="0"></a></p>
  <p><br></p>
  <p><br></p>
  <p><br clear="none">First Rotate with "&lt;90"<br clear="none"></p><pre class="prettyprint lang-lisp">    <code>
 (command "_rotate" (entlast) "" StartPt "&lt;90")
    </code>
  <a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cba1cc200c-pi"><img width="138" height="244" title="dms_angle_override_2" style="display: inline; background-image: none;" alt="dms_angle_override_2" src="/assets/image_403208.jpg" border="0"></a></pre>
  <p><br></p>
  <p> Undo, and Rotate with &lt;&lt;90</p>&nbsp;&nbsp; <p>    <code>(command "_rotate" (entlast) "" StartPt "&lt;&lt;90")</code></p><pre class="prettyprint lang-lisp"><code></code>
  <a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cba1d0200c-pi"><img width="125" height="244" title="dms_angle_override_3" style="display: inline; background-image: none;" alt="dms_angle_override_3" src="/assets/image_460689.jpg" border="0"></a></pre>
