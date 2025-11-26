---
layout: "post"
title: "Expose your code to Python - the Python way"
date: "2012-06-15 02:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/expose-your-code-to-python-the-python-way.html "
typepad_basename: "expose-your-code-to-python-the-python-way"
typepad_status: "Publish"
---

<p>In a previous <a title="Maya and Python a love story?" href="http://around-the-corner.typepad.com/adn/2012/06/maya-and-python-a-love-story.html" target="_self">post</a>, I quickly explained why Python isn't secure and may be a bad guy performance wise. One option to solve the two issues at the same time is to write your code in C++ for example. And in case you still want to access the C++ functionality, you expose it to Python. There is several way to do this, you either expose your code to Python using:</p>
<ul>
<li>the Python SDK</li>
<li>Swig</li>
<li>Boost</li>
</ul>
<p>SWIG is a software development tool that connects programs written in C and C++ with a variety of high-level programming languages. SWIG is used with different types of languages including common scripting languages such as Perl, PHP, Python, Tcl and Ruby. <a href="http://www.swig.org/">http://www.swig.org/</a><br />To use SWIG you construct an interface file which defines classes, functions, variables, and constants. Then pass the interface file to a command line utility which produces an additional source file with the bindings that make your C++ code accessible to the script language.</p>
<p>While Swig is very popular and often used by programmers, you need to teach Swig how to handle the different types. Otherwise it is fairly easy to use and do mistakes ;) like in the first Maya Python implementation (called Python 1.0). The Python SDK and Boost are more interesting and we will quickly look how to do this today and the following post.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d016767963a7b970b-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d016767963a7b970b" style="display: block; margin-left: auto; margin-right: auto;" title="Python-logo" src="/assets/image_44ba8d.jpg" border="0" alt="Python-logo" /></a></p>
<p><strong><span style="text-decoration: underline;">The Python way first</span></strong></p>
<p>In theory, this is what you would need to do for pure C++/Python and not using Maya API:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;Python.h&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">#pragma</span> <span style="color: blue;">comment</span> (<span style="color: blue;">lib</span>, <span style="color: #a31515;">"python26.lib"</span>)</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> PyObject *say_hello (PyObject *self, PyObject *args) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">char</span> *name =NULL ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( !PyArg_ParseTuple (args, <span style="color: #a31515;">"s"</span>, &amp;name) )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> (NULL) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; printf (<span style="color: #a31515;">"Hello %s! (from Python)\n"</span>, name) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; Py_INCREF (Py_None) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; Py_RETURN_NONE ;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> PyMethodDef HelloMethods[] = {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {<span style="color: #a31515;">"say_hello"</span>, say_hello, METH_VARARGS, <span style="color: #a31515;">"say_hello(self, str)"</span>},</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {NULL, NULL, 0, NULL}</p>
<p style="margin: 0px;">} ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">PyMODINIT_FUNC initPyCyrille(<span style="color: blue;">void</span>) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; (<span style="color: blue;">void</span>)Py_InitModule (<span style="color: #a31515;">"PyCyrille"</span>, HelloMethods) ;</p>
<p style="margin: 0px;">}</p>
</div>
<p><span style="color: #888888;">[Output the module as PyCyrille.pyd on Windows / PyCyrille.so on Linux]</span></p>
<p>But exposing directly from a Maya plug-in, you need a different approach&nbsp;<span style="color: #8b8b8b;">(assuming Maya 2012 or Maya 2013, otherwise use the correct Python version number)</span>.</p>
<p>Note: replace&nbsp;<span style="color: #800000;">$MAYA_LOCATION</span> by your install path</p>
<ul>
<li>Create a new Maya C++ plug-in project</li>
<li>In the “C++” -&gt; “General” -&gt; “AdditionalIncludeDirectories” include <span style="color: #800000;">$MAYA_LOCATION</span>\include</li>
<li>In the “Linker” -&gt; “Input” -&gt; “AdditionalDependencies” add <span style="color: #800000;">$MAYA_LOCATION</span>\lib\python26.lib</li>
<li>In your code add&nbsp; &nbsp;
<div style="font-family: Courier New; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">#include&nbsp;</span><span style="color: #a31515;">&lt;python2.6/Python.h&gt;</span></p>
</div>
</li>
<li>in the plug-in&nbsp;<span style="font-family: 'Courier New'; font-size: 10pt; color: #a31515;">initializePlugin</span><span style="font-family: 'Courier New'; font-size: 10pt;">()</span>method, initialize the Python kernel and register your Python functions - see code below.</li>
</ul>
<p>But what is important to know at this point is that since Maya main thread is not a Python thread you need to register the GIL (thread state structure) towards that thread so the Python interpreter can see it and execute your code without crashing Maya. You do this using the pair&nbsp;<span style="background-color: white; font-family: 'Courier New'; font-size: 10pt;">PyGILState_Ensure()</span>and&nbsp;<span style="background-color: white; font-family: 'Courier New'; font-size: 10pt;">PyGILState_Release()</span>.</p>
<p>Last, do not call the&nbsp;PyFinalize() method from the uninitializePlugin() method since Maya will do that only when it will be necessary.</p>
<p>The complete sample code</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;python2.6/Python.h&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">#pragma</span> <span style="color: blue;">comment</span> (<span style="color: blue;">lib</span>, <span style="color: #a31515;">"python26.lib"</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;maya/MGlobal.h&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;maya/MSelectionList.h&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;maya/MFnPlugin.h&gt;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> PyObject *say_hello (PyObject *self, PyObject *args) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">char</span> *name =NULL ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( !PyArg_ParseTuple (args, <span style="color: #a31515;">"s"</span>, &amp;name) )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> (NULL) ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; PyGILState_STATE state =PyGILState_Ensure () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; MSelectionList list ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; MGlobal::getActiveSelectionList (list) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">unsigned</span> <span style="color: blue;">int</span> nb =list.length () ;</p>
<p style="margin: 0px;">&nbsp; &nbsp; PyGILState_Release (state)&nbsp;;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; printf (<span style="color: #a31515;">"Hello %s, %d object(s) selected! (from Maya)\n"</span>,name,nb);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; Py_INCREF (Py_None) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; Py_RETURN_NONE ;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> PyMethodDef HelloMethods [] ={</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {&nbsp;<span style="color: #a31515;">"say_hello"</span>, say_hello, METH_VARARGS, <span style="color: #a31515;">"say_hello(self, str)"&nbsp;</span>},</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; { NULL, NULL, 0, NULL }</p>
<p style="margin: 0px;">} ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">PLUGIN_EXPORT MStatus initializePlugin (MObject obj)&nbsp;<span style="background-color: white; font-size: 10pt;">{</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; MGlobal::displayInfo (<span style="color: #a31515;">"Initializing PyCyrille"</span>) ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; MStatus stat ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; MFnPlugin plugin (obj, <span style="color: #a31515;">"vendor"</span>, <span style="color: #a31515;">"version"</span>, <span style="color: #a31515;">"any"</span>, &amp;stat) ;</p>
<p style="margin: 0px;"><span style="background-color: white; font-size: 10pt;">&nbsp; &nbsp;&nbsp;</span><span style="color: blue;">if</span><span style="background-color: white; font-size: 10pt;"> ( !stat )</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span>&nbsp;(MS::kFailure) ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( !Py_IsInitialized () )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Py_Initialize () ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( Py_IsInitialized () ) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; PyGILState_STATE state =PyGILState_Ensure () ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; PyObject *module =Py_InitModule (<span style="color: #a31515;">"PyCyrille"</span>, HelloMethods) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Py_INCREF (module) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; PyGILState_Release (state) ;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span>&nbsp;(MS::kSuccess) ;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">PLUGIN_EXPORT MStatus uninitializePlugin(MObject obj)&nbsp;<span style="background-color: white; font-size: 10pt;">{</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; MFnPlugin plugin (obj) ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Do not call Py_Finalize()</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">//Py_Finalize();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span>&nbsp;(MS::kSuccess) ;</p>
<p style="margin: 0px;">}</p>
</div>
<p><span style="color: #888888;">[Output the module as a standard Maya plug-in]</span></p>
<p><span style="color: #888888;">Note on MacOS, I'll recommend to use distutils to compile your plug-in, instead of Xcode. Specifically, the problem is that you probably link to a different instance than the one you tihnk. distutils solves this problem for you because it knows which Python to link to and which flags to use by way of the Python interpreter you invoke with setup.py</span></p>
<p>OK, that's it for the hardcore technical details on exposing C++ to Python. In the next post we'll see how to do the same thing using Boost&nbsp;(and you can trust me when I say that's going to be a whole lot simpler than this post, honestly! :-)</p>
