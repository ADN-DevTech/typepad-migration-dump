---
layout: "post"
title: "Using Global Interpreter Locks (GIL) when developing Python code in C++"
date: "2013-05-03 08:54:59"
author: "Cyrille Fauvel"
categories:
  - "3ds Max"
  - "C++"
  - "Cyrille Fauvel"
  - "FBX"
  - "Maya"
  - "MotionBuilder"
  - "Python"
  - "SoftImage"
original_url: "https://around-the-corner.typepad.com/adn/2013/05/using-global-interpreter-locks-gil-when-developing-python-code-in-c.html "
typepad_basename: "using-global-interpreter-locks-gil-when-developing-python-code-in-c"
typepad_status: "Publish"
---

<p>Python uses a Global Interpreter Lock (GIL) to ensures that Python code in only executing in a single thread at a time.</p>
<p>When you use the Python C API and/or expose C++ code to Python, you have to re-acquire the GIL each time you call in a C Python API. This is also true when writing Python extensions for Maya or when using the Python C API from a Maya plug-ins.</p>
<p>You will need to add these calls in a few high-level places in your code, like this:</p>
<pre class="brush: cpp; toolbar: false;">PyGILState_STATE state = PyGILState_Ensure();
// call your python code here:
PyGILState_Release(state);</pre>
