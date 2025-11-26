---
layout: "post"
title: "What are AcDbCurve/Curve Parameters? ObjectARX/.NET"
date: "2012-05-17 14:09:21"
author: "Philippe Leefsma"
categories:
  - ".NET"
  - "AutoCAD"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/what-are-acdbcurvecurve-parameters-objectarxnet.html "
typepad_basename: "what-are-acdbcurvecurve-parameters-objectarxnet"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>Please can you explain what Parameters are?</p>  <p>In simple terms, the parameter of a curve defines a 0-n floating point value indicating point positions on a curve. </p>  <p>Lets use a polyline as an example of what to expect from a parameter value. </p>  <p>If we have a polyline which has 5 points, and each of the 4 lines are different in length. If we call AcDbCurve::getStartParam() we will be returned a value of 0, if we call AcDbCurve::getEndParam() we will get a value of 4, if we extract the 2nd polyline point and call AcDbCurve::getParamAt() we will be returned a parameter value of 1. So you can see that in the case of a polyline, the parameter values directly represent each of the start and end points of the Polyline. </p>  <p>Parameters are very powerful, for instance, I can extract the halfway point between p1 and p2 by calling AcDbCurve::getPointAtParam(0.5) or the third of a distance between p3 and p4 by calling AcDbCurve::getPointAtParam(2.33333).</p>  <p>Another point to mention is that parameter values, although define a 0-n floating point value indicating point positions on a curve, are designed to best fit the needs of the curve being implemented. For example, the polyline example above uses parameter values as start and end point positions, this is a very efficient way to use parameters for a polyline and indeed makes logical sense, but for a circle the parameter values correspond to a radian increment around the circle. So, for a polyline the parameter value is non-uniform whereas the circle uses a uniform parameter value.</p>
