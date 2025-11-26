---
layout: "post"
title: "Creating a Wall with a Sloped Profile"
date: "2009-02-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Geometry"
  - "Getting Started"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/02/creating-a-wall-with-a-sloped-profile.html "
typepad_basename: "creating-a-wall-with-a-sloped-profile"
typepad_status: "Publish"
---

<p>Here is a 

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-elevation-profile.html#comments">
question from Winnie</a>

on the creation of walls with a sloped profile:</p>

<p><strong>Question:</strong>
I'm having problems trying to create walls that have constant sloped top edge.
I tried using the NewWall method and passed in a CurveArray that contains the edges of the wall that would create a sloped top but the result was still a rectangular wall.
Also, how would you alter an existing wall to have a top sloped edge?</p>

<p><strong>Answer:</strong>
Using NewWall and supplying a profile to define the slope is exactly the right approach.
I implemented a minimal new command named CmdSlopedWall to do this.
Here is the code for the Execute method to create a wall with a sloped upper edge:</p>

<pre class="code">
<span class="teal">Application</span> app = commandData.Application;
&nbsp;
Autodesk.Revit.Creation.<span class="teal">Application</span> ac
&nbsp; = app.Create;
&nbsp;
<span class="teal">CurveArray</span> profile = ac.NewCurveArray();
&nbsp;
<span class="blue">double</span> length = 10;
<span class="blue">double</span> heightStart = 5;
<span class="blue">double</span> heightEnd = 8;
&nbsp;
<span class="teal">XYZ</span> p = ac.NewXYZ( 0.0, 0.0, 0.0 );
<span class="teal">XYZ</span> q = ac.NewXYZ( length, 0.0, 0.0 );
&nbsp;
profile.Append( ac.NewLineBound( p, q ) );
&nbsp;
p.X = q.X;
q.Z = heightEnd;
&nbsp;
profile.Append( ac.NewLineBound( p, q ) );
&nbsp;
p.Z = q.Z;
q.X = 0.0;
q.Z = heightStart;
&nbsp;
profile.Append( ac.NewLineBound( p, q ) );
&nbsp;
p.X = q.X;
p.Z = q.Z;
q.Z = 0.0;
&nbsp;
profile.Append( ac.NewLineBound( p, q ) );
&nbsp;
<span class="teal">Document</span> doc = app.ActiveDocument;
&nbsp;
<span class="teal">Wall</span> wall = doc.Create.NewWall( profile,
&nbsp; <span class="blue">false</span> );
&nbsp;
<span class="blue">return</span> <span class="teal">CmdResult</span>.Succeeded;
</pre>

<p>This is what the resulting wall looks like:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330105370f0185970b-pi"><img class="at-xid-6a00e553e1689788330105370f0185970b" alt="Wall with a sloped profile" title="Wall with a sloped profile" src="/assets/image_c1557f.jpg" border="0"  /></a>

<p>Regarding your second query on the modification of an existing wall:
applying a profile to an existing wall which has none to start with is currently not supported by the API.
Such a method would be similar to the Truss SetProfile method, but walls do not currently support this.</p>

<p>Here is 

<a  href="http://thebuildingcoder.typepad.com/files/bc10022.zip"><span class="at-xid-6a00e553e1689788330111684947a1970c">version 1.0.0.22</span></a>

of the complete Visual Studio solution with this new command implementation.</p>
