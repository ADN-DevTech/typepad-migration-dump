---
layout: "post"
title: "Analytical Model IsEnabled Method and Parameter"
date: "2011-09-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2012"
  - "Data Access"
  - "Element Relationships"
  - "Parameters"
  - "RST"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/09/analytical-model-isenabled-method-and-parameter.html "
typepad_basename: "analytical-model-isenabled-method-and-parameter"
typepad_status: "Publish"
---

<p>I recently mentioned the changes in 

<a href="http://thebuildingcoder.typepad.com/blog/2011/08/reference-to-analytical-curve.html">
accessing the analytical model</a> in 

the Revit Structure 2012 API.

<p>We just discovered a little issue and an effective workaround related to the AnalyticalModel IsEnabled method, which reports whether the analytical model is currently enabled or disabled.
Right now, it does not do that, but throws an exception for all members except walls.
The Enable and CanDisable methods have similar problems.
This will soon be resolved.
Meanwhile, here is a simple workaround to avoid the issue by accessing the underlying data directly:

<p>The correct "enable" parameter is on the physical element, not its analytical model, and accessible through the built-in parameter STRUCTURAL_ANALYTICAL_MODEL. 
To enable the analytical model, e.g. for a wall, simply set STRUCTURAL_ANALYTICAL_MODEL to true on the wall element, not on the analytical wall element.
