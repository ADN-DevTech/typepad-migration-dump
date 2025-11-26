---
layout: "post"
title: "Forge Formats Python Scripts"
date: "2016-12-14 02:00:00"
author: "Jeremy Tammik"
categories:
  - "Forge"
  - "Jeremy Tammik"
  - "Script"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/forge-formats-python-scripts.html "
typepad_basename: "forge-formats-python-scripts"
typepad_status: "Publish"
---

<script src="https://google-code-prettify.googlecode.com/svn/loader/run_prettify.js" type="text/javascript"></script>

<p>By <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html">Jeremy Tammik</a>.</p>

<p>I just published the beginning of a
new <a href="https://github.com/jeremytammik/forge_python_script">collection of Forge Python scripts</a>,
currently with a count of one:</p>

<p><a href="https://github.com/jeremytammik/forge_python_script/blob/master/py_forge_formats.py">py_forge_formats.py</a>
implements a Python wrapper around two basic RESTful Forge web service calls:</p>

<ul>
<li>Authenticate an app &ndash; <a href="#6">forge_authenticate_app</a></li>
<li>Query the file formats currently supported by the translation processes &ndash; <a href="#7">forge_formats</a></li>
</ul>

<p>The result is prettified using the <a href="#8">jprettyprint</a> helper function.</p>

<p>The <a href="#9">mainline</a> ties it all together and presents the final result, which looks like this at the time of writing:</p>

<pre class="prettyprint">
$ ./py_forge_formats.py
9 Forge output formats:
  dwg: f2d, f3d, rvt
  fbx: f3d
  ifc: rvt
  iges: f3d, fbx, iam, ipt, wire
  obj: asm, f3d, fbx, iam, ipt, neu, prt, sldasm, sldprt, step, stp, stpz,
    wire, x_b, x_t, asm.NNN, neu.NNN, prt.NNN
  step: f3d, fbx, iam, ipt, wire
  stl: f3d, fbx, iam, ipt, wire
  svf: 3dm, 3ds, asm, catpart, catproduct, cgr, collaboration, dae, dgn,
    dlv3, dwf, dwfx, dwg, dwt, dxf, exp, f3d, fbx, g, gbxml, iam, idw,
    ifc, ige, iges, igs, ipt, jt, max, model, neu, nwc, nwd, obj, pdf,
    prt, rcp, rvt, sab, sat, session, skp, sldasm, sldprt, smb, smt,
    ste, step, stl, stla, stlb, stp, stpz, wire, x_b, x_t, xas, xpr,
    zip, asm.NNN, neu.NNN, prt.NNN
  thumbnail: 3dm, 3ds, asm, catpart, catproduct, cgr, collaboration, dae, dgn,
    dlv3, dwf, dwfx, dwg, dwt, dxf, exp, f3d, fbx, g, gbxml, iam, idw,
    ifc, ige, iges, igs, ipt, jt, max, model, neu, nwc, nwd, obj, pdf,
    prt, rcp, rvt, sab, sat, session, skp, sldasm, sldprt, smb, smt,
    ste, step, stl, stla, stlb, stp, stpz, wire, x_b, x_t, xas, xpr,
    zip, asm.NNN, neu.NNN, prt.NNN
</pre>

<p>This script replaces and improves on the
previous <a href="https://github.com/jeremytammik/forge_python_script/blob/master/forgeauth">forgeauth</a>
and <a href="https://github.com/jeremytammik/forge_python_script/blob/master/forgeformats">forgeformats</a> Unix
shell cURL scripts documented in the discussion of
the <a href="http://thebuildingcoder.typepad.com/blog/2016/10/forge-intro-formats-webinars-and-fusion-360-client-api.html#3"><code>cURL</code> wrapper scripts to list Forge file formats</a>.</p>

<p>For the sake of completeness, those two scripts have been added to this repository as well.</p>

<h3><a name="5"></a>Setup and Usage</h3>

<p>Two aspects need to be prepared: Forge and Python.</p>

<p>Before you can make any use of the Forge web services, you will need to register an app and request the API client id and client secret for it
at <a href="https://developer.autodesk.com">developer.autodesk.com</a>
&gt; <a href="https://developer.autodesk.com/myapps">my apps</a>.</p>

<p>These scripts assume that you have stored these credentials in the environment variables <code>FORGE_CLIENT_ID</code> and <code>FORGE_CLIENT_SECRET</code>.</p>

<p>Regarding the Python components:</p>

<ul>
<li>Install <a href="https://www.python.org">Python</a>.</li>
<li>Install the <a href="http://docs.python-requests.org">requests Python library</a>.</li>
</ul>

<p>Now you should be all set to run as shown above.</p>

<h3><a name="6"></a>forge_authenticate_app</h3>

<script src="https://gist.github.com/jeremytammik/819084fdc8bc52965b7ce8f3d64cc18b.js"></script>

<h3><a name="7"></a>forge_formats</h3>

<script src="https://gist.github.com/jeremytammik/4e8df567c15f8fab1fa40e17962045b9.js"></script>

<h3><a name="8"></a>jprettyprint</h3>

<script src="https://gist.github.com/jeremytammik/d3c3b02b5fe2636436cc6acc7173bef2.js"></script>

<h3><a name="9"></a>Mainline</h3>

<script src="https://gist.github.com/jeremytammik/9a9caddec09a44ddceaab677abcc9887.js"></script>
