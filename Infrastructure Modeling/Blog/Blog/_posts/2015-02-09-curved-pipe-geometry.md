---
layout: "post"
title: "Curved Pipe Geometry"
date: "2015-02-09 03:25:02"
author: "Augusto Goncalves"
categories: []
original_url: "https://adndevblog.typepad.com/infrastructure/2015/02/curved-pipe-geometry.html "
typepad_basename: "curved-pipe-geometry"
typepad_status: "Publish"
---

<p>Just a quick note that catch my attention, in fact a recurrent question. If the Pipe is of type Curved, how can we access/read the geometry information?</p>
<p>In fact, there is a Pipe.Curve2d property will return a CircularArc2d, which has a Radius property, and some others, like Center. If you need a point as a specific position, try EvaluatePoint at a specific parameter of the curve, which is a BRep API from AutoCAD.</p>
<p>Pipe myPipe = // read the pipe<br />myPipe.Curve2d.EvaluatePoint(0.5); // middle of the arc, for instance</p>
