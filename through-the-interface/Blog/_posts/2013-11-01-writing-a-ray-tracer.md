---
layout: "post"
title: "Writing a ray-tracer"
date: "2013-11-01 10:48:11"
author: "Kean Walmsley"
categories:
  - "Graphics system"
  - "Personal"
original_url: "https://www.keanw.com/2013/11/writing-a-ray-tracer.html "
typepad_basename: "writing-a-ray-tracer"
typepad_status: "Publish"
---

<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2013/10/sharpening-the-saw.html" target="_blank">this previous post</a> – in-between bouts of preparation for AU2013 and my other work responsibilities – I’ve been spending time following <a href="https://courses.edx.org/courses/BerkeleyX/CS-184.1x/2013_October/info" target="_blank">an edX class on computer graphics</a>. It’s been really beneficial: I’ve shored up some of the basics I’d missed out on studying formally at university and it was a great continuation of many of the concepts gleaned from <a href="http://through-the-interface.typepad.com/through_the_interface/2013/08/applications-for-linear-algebra-straightening-out-perspective.html" target="_blank">the linear algebra class on Coursera</a>.</p>
<p>The final assignment in the class has been a challenge: I’ve had to write a ray-racer from the ground up. It’s been really interesting, though: there are various steps to go through, such as loading a scene from a (text) file, implementing a camera model, the ability to test for intersections between rays and the elements of your scene and then lighting and shadow calculations. I knew that ray-tracers cast per-pixel rays to determine colouring – and followed reflections in the model to determine the effect of light bouncing off other objects in the scene – but I didn’t realise they also cast rays towards light sources to determine whether the pixel was in shade (with respect to that particular light). In many ways obvious, but going through the whole process has been very instructive.</p>
<p>And I&#39;m really happy I had this opportunity to dust off my C++ skills: I’d actually forgotten how much I enjoy the language, despite hassles such as class declarations and multiple inclusion of header files.</p>
<p>For fun I decided to save out the results I received from my evolving application and create an animated GIF showing some of the progress. Lots of the early issues – such as the black and white images – ended up relating to the image output code, but I eventually progressed to being able to deal with geometry, transformations and – towards the end – shading and lighting effects. I’m still not quite done but I won’t post any further images – at least not of the scenes provided in the course – as these images are actually used to submit the homework and the deadline isn’t for another couple of weeks.</p>
<p>&#0160;</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019b008888d7970d-pi"><img alt="Raytracer bloopers" height="300" src="/assets/image_887477.jpg" style="display: block; float: none; margin-left: auto; margin-right: auto;" title="Raytracer bloopers" width="400" /></a></p>
