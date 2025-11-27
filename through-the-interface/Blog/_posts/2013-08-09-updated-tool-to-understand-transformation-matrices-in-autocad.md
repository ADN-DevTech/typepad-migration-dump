---
layout: "post"
title: "Updated tool to understand transformation matrices in AutoCAD"
date: "2013-08-09 06:48:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
  - "Selection"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2013/08/updated-tool-to-understand-transformation-matrices-in-autocad.html "
typepad_basename: "updated-tool-to-understand-transformation-matrices-in-autocad"
typepad_status: "Publish"
---

<p>As I started on <a href="http://through-the-interface.typepad.com/through_the_interface/2013/08/coursera-the-future-of-education.html" target="_blank">my linear algebra class</a>, some weeks ago, I decided to dust off <a href="http://through-the-interface.typepad.com/through_the_interface/2010/12/a-tool-to-help-understand-transformation-matrices-in-autocad.html" target="_blank">the Transformer app I’d written a few years ago</a> and make sure it works in AutoCAD 2014. It actually really helped me in creating appropriate transformation matrices for certain parts of the course.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201910485776c970c-pi" target="_blank"><img alt="Transformer app in AutoCAD 2014" border="0" height="287" src="/assets/image_55087.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="Transformer app in AutoCAD 2014" width="469" /></a></p>
<p>Coincidentally, a few days ago, I received an email from a colleague – who isn’t a programmer but seems to be working on a very interesting side project – who was interested in taking matrix input from an external system and using that to transform AutoCAD geometry. This colleague wanted the transformation to be applied to multiple entities, so in the process I extended the TRANS command to work with a selection set rather than a single entity.</p>
<p>Here’s <a href="http://through-the-interface.typepad.com/files/Transformer2.zip" target="_blank">the updated application</a>, which now makes use of a newer version of <a href="http://wpftoolkit.codeplex.com" target="_blank">the WPF Toolkit from Xceed</a>.</p>
