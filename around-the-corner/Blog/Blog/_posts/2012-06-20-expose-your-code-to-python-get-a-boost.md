---
layout: "post"
title: "Expose your code to Python - get Boost 'ed!"
date: "2012-06-20 02:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "FBX"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "MotionBuilder"
  - "Python"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/expose-your-code-to-python-get-a-boost.html "
typepad_basename: "expose-your-code-to-python-get-a-boost"
typepad_status: "Publish"
---

<p>Some house cleaning first...</p>
<p><a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0176158be5f0970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d0176158be5f0970c" style="display: block; margin-left: auto; margin-right: auto;" title="Boost" src="/assets/image_14dc09.jpg" border="0" alt="Boost" /></a></p>
<p>"Boost provides free peer-reviewed portable C++ source libraries". <a href="http://www.boost.org/">http://www.boost.org/</a></p>
<p>The Boost C++/Python binding library is just one member of the boost C++ library collection at <a href="http://www.boost.org/">http://www.boost.org</a></p>
<p>It is quick and easy to export C++ to Python using Boost. Boost is also designed to be minimally intrusive on your C++ design. In most cases, you should not have to alter your C++ classes in any way in order to use them with Boost Python. The system should simply reflect your C++ classes and functions into Python. Boost Python bindings are written in pure C++, using no tools other than your editor and your C++ compiler.</p>
<p><a href="http://www.boost.org/doc/libs/1_49_0/libs/python/doc/index.html">http://www.boost.org/doc/libs/1_49_0/libs/python/doc/index.html</a></p>
<p>Documentation at: <a href="http://www.boost.org/libs/python/">http://www.boost.org/libs/python/</a><br />Find binaries at: <a href="http://boost.teeks99.com/">http://boost.teeks99.com</a><br />Tutorial:&nbsp;<a href="http://www.boost.org/doc/libs/1_49_0/libs/python/doc/tutorial/doc/html/python/exposing.html">http://www.boost.org/doc/libs/1_49_0/libs/python/doc/tutorial/doc/html/python/exposing.html</a></p>
<p><a class="asset-img-link" style="display: inline;" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0176158be6d1970c-pi"><img class="asset  asset-image at-xid-6a0163057a21c8970d0176158be6d1970c" style="display: block; margin-left: auto; margin-right: auto;" title="GetBoost" src="/assets/image_3aac59.jpg" border="0" alt="GetBoost" /></a></p>
<p>OK, so now let's see how it looks like: Like in the previous <a href="http://around-the-corner.typepad.com/adn/2012/06/expose-your-code-to-python-the-python-way.html" target="_blank">post</a> something simple.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;boost/python.hpp&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> python =boost::python ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">void</span> say_hello (<span style="color: blue;">char</span> *name) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; printf (<span style="color: #a31515;">"Hello %s!\n"</span>, name) ;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">BOOST_PYTHON_MODULE (PyCyrille) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; python::def (<span style="color: #a31515;">"say_hello"</span>, say_hello) ;</p>
<p style="margin: 0px;">}</p>
</div>
<p>Yes this is it :) nice isn't it?</p>
<p>And now here is a very simple class exposed.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">#include</span> <span style="color: #a31515;">&lt;boost/python.hpp&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> python =boost::python ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">class</span> CyrilleClass {</p>
<p style="margin: 0px;"><span style="color: blue;">private</span>:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">int</span> mInt ;</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">float</span> mFloat ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; CyrilleClass () : mInt(-1), mFloat(-1) {}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">int</span> inc () { <span style="color: blue;">return</span> (++mInt) ; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">int</span> dec () { <span style="color: blue;">return</span> (--mInt) ; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">int</span> _get () { <span style="color: blue;">return</span> (mInt) ; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">void</span> _set (<span style="color: blue;">int</span> i) { mInt =i ; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">} ;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">BOOST_PYTHON_MODULE (PyCyrille) {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; python::class_&lt;CyrilleClass&gt;(<span style="color: #a31515;">"CyrilleClass"</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; .def(<span style="color: #a31515;">"inc"</span>, &amp;CyrilleClass::inc)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; .def(<span style="color: #a31515;">"dec"</span>, &amp;CyrilleClass::dec)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; .def(<span style="color: #a31515;">"_get"</span>, &amp;CyrilleClass::_get)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; .def(<span style="color: #a31515;">"_set"</span>, &amp;CyrilleClass::_set)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; .def_readwrite(<span style="color: #a31515;">"mFloat"</span>, &amp;CyrilleClass::mFloat)</p>
<p style="margin: 0px;"><span style="background-color: white; font-size: 10pt;">&nbsp; &nbsp; &nbsp; &nbsp; ;</span></p>
<p style="margin: 0px;">}</p>
</div>
<p>You see excepted the BOOST_PYTHON_MODULE&nbsp;block, which remains fairly easy to understand, there is nowhere complicated thing like PYINC(), PyInitialize(), etc... and I do not have to care about the GIL state. Wow, I like that !</p>
<p>But there is a price for this - Boost adds an additional memory footprint to Maya, and you better choose (or recompile) Boost using the same compiler Maya used (I.e. Visual Studio 2010 SP1 for the Windows platform). For MotionBuiler, that is probably the way to go as the MotionBuilder Python API was created using Boost - just make sure to use the same Boost libraries.</p>
<p>Next time, we will see how to deploy C++ (or Boost) Python extension into applications.</p>
