---
layout: "post"
title: "What&rsquo;s the GenericObject interface?"
date: "2012-06-18 12:17:24"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/whats-the-genericobject-interface.html "
typepad_basename: "whats-the-genericobject-interface"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I found a reference on a GenericObject interface in the Inventor documentation. What is it? and how to use it?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>GenericObject is an interface put in place to wrap an object that hasn't been designed or implemented yet, or that we haven't connected yet. All objects in the system will not support the GenericObject interface, and it's use will decrease over time, as the API coverage becomes more complete.</p>  <p>If you want a generic utility to query the object type, we added a utility (Rx::GetObjectType) for C++ programmers to do just this in DispatchUtils.h.</p>
