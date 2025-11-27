---
layout: "post"
title: "Inventor debugging slowed down by error messages"
date: "2015-05-22 08:21:41"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/inventor-debugging-slowed-down-by-error-messages.html "
typepad_basename: "inventor-debugging-slowed-down-by-error-messages"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Many <strong>Autodesk</strong> products are using the same .<strong>NET</strong> components or the <strong>UI</strong>, and when debugging <strong>Inventor</strong> from <strong>Visual Studio</strong> they can make the <strong>Output</strong>/<strong>Debug</strong> window quite noisy, and can also slow down things considerably.</p>
<p>Same inside <strong>AutoCAD</strong>:<br />- <a href="http://through-the-interface.typepad.com/through_the_interface/2011/03/making-autocad-less-noisy-when-debugging.html" target="_self" title="">Making AutoCAD less noisy when debugging</a>&#0160;<br />-&#0160;<a href="http://adndevblog.typepad.com/autocad/2012/03/solution-for-many-errors-when-debugging-my-first-plug-in-with-vbnet-express-2010.html" target="_self" title="">Solution for many errors when debugging my first plug-in with VB.net Express 2010</a></p>
<p>And the solution is also the same here too.&#0160;<br />In my <strong>Output</strong> window I saw that most entries were created by these two components:&#0160;<strong>System.Windows.Data</strong> and&#0160;<strong>System.Windows.Media.Animation</strong>.</p>
<p>So I removed them by adding this section in the <strong>Inventor.exe.config</strong> file:&#0160;</p>
<pre>  &lt;system.diagnostics&gt;
    &lt;sources&gt;
      &lt;source name=&quot;System.Windows.Data&quot;
              switchName=&quot;SourceSwitch&quot;&gt;
        &lt;listeners&gt;
          &lt;remove name=&quot;Default&quot; /&gt;
        &lt;/listeners&gt;
      &lt;/source&gt;
      &lt;source name=&quot;System.Windows.Media.Animation&quot;
              switchName=&quot;SourceSwitch&quot;&gt;
        &lt;listeners&gt;
          &lt;remove name=&quot;Default&quot; /&gt;
        &lt;/listeners&gt;
      &lt;/source&gt;
    &lt;/sources&gt;
  &lt;/system.diagnostics&gt;</pre>
<p>&#0160;</p>
