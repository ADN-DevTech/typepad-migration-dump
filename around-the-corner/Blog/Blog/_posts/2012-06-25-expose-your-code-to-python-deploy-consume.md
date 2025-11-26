---
layout: "post"
title: "Expose your code to Python - Deploy & Consume"
date: "2012-06-25 07:05:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "FBX"
  - "Maya"
  - "MotionBuilder"
  - "Mudbox"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/expose-your-code-to-python-deploy-consume.html "
typepad_basename: "expose-your-code-to-python-deploy-consume"
typepad_status: "Publish"
---

<p>If you want to know in details follow that <a href="http://packages.python.org/distribute" target="_blank">link</a>&#0160;- or use this <a href="http://packages.python.org/distribute/setuptools.html" target="_blank">tools</a></p>
<p>But since Python is already installed on your Maya (or MotionBuilder) distribution, it is fairly easy to distribute your Python &#39;module&#39; onto other machines for these products. However depending on what you did while compiling your module, you may run into couple of issues. It is never simple, isn&#39;t it? let&#39;s take the simple approach and we see the downfalls later.</p>
<p>In case you did use the correct version of the Python headers/libraries (or Boost on MotionBuilder - using the same Boost version as MotionBuilder), that will help a lot and make it very simple. If it is a pure Python module (not plug-in), just drop your module into the &#39;site-packages&#39; folder. If it is a C++ plug-in, exposing a Python API, it does not matter - put it anywhere you want but you still need to load it as a plug-in into Maya (or MotionBuilder).</p>
<p>If you&#39;re using Boost, you need to make sure Boost libraries are in the OS search path for libraries. Otherwise, your module will silently fail to load in the Python interpreter. This is true only for products not having Boost installed already, so you can skip that task for MotionBuilder.</p>
<p>Where it becomes complicated is when you either don&#39;t match the same Python/Boost versions and/or have dependencies on other modules. And here, I am afraid there isn&#39;t any single easy answer, but Kristine and I will be happy to help to identify the problem. Today, I do experiment some Python 3.0 bridge on Maya and may blog about this later in the year. This post ends the serie of exposing your C++ code to Python...</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0177429c551d970d-pi" style="display: inline;"><img alt="The_end" class="asset  asset-image at-xid-6a0163057a21c8970d0177429c551d970d" src="/assets/image_f4018a.jpg" style="width: 200px; display: block; margin-left: auto; margin-right: auto;" title="The_end" /></a></p>
