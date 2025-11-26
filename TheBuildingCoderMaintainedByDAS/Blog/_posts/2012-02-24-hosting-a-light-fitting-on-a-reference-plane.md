---
layout: "post"
title: "Hosting a Light Fitting on a Reference Plane"
date: "2012-02-24 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Geometry"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/02/hosting-a-light-fitting-on-a-reference-plane.html "
typepad_basename: "hosting-a-light-fitting-on-a-reference-plane"
typepad_status: "Publish"
---

<p>It is sometimes a challenge to determine which of the numerous 

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/newfamilyinstance-overloads.html">
overloads of the NewFamilyInstance method</a> to 

use to achieve a desired effect.
Here is a question on using it to host a light fitting by Simon Jones, with an initial answer by Saikat Bhattacharya, followed by lots more research by Simon to nail down the right solution that really works.

<p><strong>Question:</strong> How can I host a light fitting onto a reference plane?

<p>I tried several different ways without any luck so far.

<p>When I use the following, the light is hosted by the reference plane but doesn't move with it:

<pre class="code">
  mRevitDoc.Document.Create.NewFamilyInstance(
    pt, mFamilySymbol, refDir, mReferencePlane,
    StructuralType.NonStructural);
</pre>

<p>What should I use instead to make the fitting remain attached to the plane at all times?

<p><strong>Answer:</strong> The following modification to your code addresses your requirement.

<p>It uses the overload of NewFamilyInstance taking a reference, point, vector and symbol to insert a new instance of a family onto a face referenced by the input Reference instance, using a location and reference direction.

<p>This creates a family instance which moves when the ReferencePlane is moved.

<p>For simplicity, I have hard coded the element id of the ReferencePlane of my test project.

<p>Before running this code, please select the lighting instance on the base work plane.
This code then creates a lighting instance on the reference plane (along with the existing lighting fixture on the reference).

<p>Now you can move the reference plane to the other side of the wall and confirm that the new lighting fixture instance moves along with it too.

<pre class="code">
&nbsp; <span class="teal">Selection</span> sel = uidoc.Selection;
&nbsp;
&nbsp; <span class="teal">ElementSet</span> elemSet = sel.Elements;
&nbsp;
&nbsp; <span class="teal">Transaction</span> trans = <span class="blue">new</span> <span class="teal">Transaction</span>( doc );
&nbsp; trans.Start( <span class="maroon">&quot;Lights Camera Action!&quot;</span> );
&nbsp;
&nbsp; <span class="teal">IEnumerator</span> iter = elemSet.ForwardIterator();
&nbsp;
&nbsp; <span class="teal">Element</span> element;
&nbsp;
&nbsp; <span class="teal">ReferencePlane</span> mReferencePlane = <span class="blue">null</span>;
&nbsp; <span class="teal">FamilyInstance</span> mFamilyInstance = <span class="blue">null</span>;
&nbsp; <span class="teal">FamilySymbol</span> mFamilySymbol = <span class="blue">null</span>;
&nbsp;
&nbsp; <span class="blue">while</span>( iter.MoveNext() )
&nbsp; {
&nbsp; &nbsp; element = iter.Current <span class="blue">as</span> <span class="teal">Element</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">FamilyInstance</span> fi = element <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp;
&nbsp; &nbsp; mFamilyInstance = fi;
&nbsp; &nbsp; mFamilySymbol = fi.Symbol;
&nbsp; }
&nbsp;
&nbsp; mReferencePlane = doc.get_Element(
&nbsp; &nbsp; <span class="blue">new</span> <span class="teal">ElementId</span>( 615738 ) ) <span class="blue">as</span> <span class="teal">ReferencePlane</span>;
&nbsp;
&nbsp; <span class="teal">XYZ</span> pt = <span class="teal">XYZ</span>.Zero;
&nbsp; <span class="teal">XYZ</span> refDir = <span class="teal">XYZ</span>.BasisZ;
&nbsp;
&nbsp; uidoc.Document.Create.NewFamilyInstance(
&nbsp; &nbsp; mReferencePlane.Reference, pt, refDir,
&nbsp; &nbsp; mFamilySymbol );
&nbsp;
&nbsp; trans.Commit();
</pre>

<p><strong>Response:</strong> Perfect, got it working now.

<p>The issue was that I was trying to place the light on a horizontal reference plane and it seems that the <refDir> argument was set incorrectly.
To begin with, strangely enough, it seemed that I needed to use a null vector for the reference direction for a horizontal reference plane:

<pre class="code">
  XYZ refDir = new XYZ(0,0,0);
</pre>

<p>Before that, I was using:

<pre class="code">
  XYZ refDir = new XYZ(0,0,1);
</pre>

<p>That caused my error.

<p>Here is a current snapshot of my

<span class="asset  asset-generic at-xid-6a00e553e168978833016762d5b066970b"><a href="http://thebuildingcoder.typepad.com/files/lightingfixtureinsert.cs">C# code</a></span> and 

some

<span class="asset  asset-generic at-xid-6a00e553e168978833016301e0d062970d"><a href="http://thebuildingcoder.typepad.com/files/lightingfixturepositions.txt">sample input data</a></span>.

The code will read a TXT file and place lights on a named reference plane.

<p>I explored the effect of the reference direction used in more detail.
Here are my findings:

<p>The reference direction required obviously depends on the orientation of the reference plane.
So I guess the sensible thing to do is to use a direction relative to the plane's normal.

<p>However, my solution requires taking ceiling layouts from AutoCAD and reading them into Revit via a txt file exported from AutoCAD.
Within Revit we use horizontal reference planes drawn from right to left (as even with manual insertion that effects the orientation of the light); therefore, this is the solution I require and am happy that I have.

<p>From what I can work out, the reference direction needs to point in the direction the reference plane is drawn; therefore 0,0,1 works for vertical planes, and 1,0,0 for horizontal ones.
For a quick hack, the following works for both vertical and horizontal ref planes:

<pre class="code">
  XYZ refDir = new XYZ(
    rp.Normal.Z, rp.Normal.X, rp.Normal.Y );
</pre>

<p>Here are the results of my tests prior to adding the above hack:

<h4>Vertical Reference Plane along the End Wall</h4>

<p><strong>1.</strong> Vertical RP +ve Y (drawn bottom to top), RefDir 0,0,1:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016762d5b389970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016762d5b389970b" alt="Lighting fixture on reference plane test 1" title="Lighting fixture on reference plane test 1" src="/assets/image_80e712.jpg" border="0" /></a><br />

</center>

<p><strong>2.</strong> Vertical RP -ve Y (drawn top to bottom), RefDir 0,0,1:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330168e7d7a3e3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330168e7d7a3e3970c" alt="Lighting fixture on reference plane test 2" title="Lighting fixture on reference plane test 2" src="/assets/image_68f859.jpg" border="0" /></a><br />

</center>

<p><strong>Result:</strong> The lights have been placed on the other side of the reference plane.

<p><strong>3.</strong> Vertical RP -ve Y (drawn top to bottom), RefDir 0,0,0:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016762d5b744970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016762d5b744970b image-full" alt="Lighting fixture on reference plane test 3" title="Lighting fixture on reference plane test 3" src="/assets/image_2d51db.jpg" border="0" /></a><br />

</center>

<p><strong>Result:</strong> Same side of ref plane, but now they're rotated around 90 degrees.

<h4>Horizontal Reference Plane along the Underside of the Ceiling</h4>

<p><strong>4.</strong> Horizontal RP -ve X (drawn right to left), RefDir 0,0,0:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330168e7d7af21970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330168e7d7af21970c image-full" alt="Lighting fixture on reference plane test 4" title="Lighting fixture on reference plane test 4" src="/assets/image_f24560.jpg" border="0" /></a><br />

</center>

<p><strong>Result:</strong> Attached to the underside of the reference plane (as required).

<p><strong>5.</strong> Horizontal RP +ve X (drawn left to right), RefDir 0,0,0:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016301e0e4f2970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016301e0e4f2970d image-full" alt="Lighting fixture on reference plane test 5" title="Lighting fixture on reference plane test 5" src="/assets/image_02cd87.jpg" border="0" /></a><br />

</center>

<p><strong>Result:</strong> Attached to the topside of the reference plane (not as required).

<p><strong>6.</strong> Horizontal RP +ve X (drawn left to right), RefDir 0,0,1 & Horizontal RP -ve X (drawn right to left), RefDir 0,0,1:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016762d5c7ce970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016762d5c7ce970b" alt="Lighting fixture on reference plane test 6" title="Lighting fixture on reference plane test 6" src="/assets/image_4257b8.jpg" border="0" /></a><br />

</center>

<p><strong>Result:</strong> 'Error code: 5'.

<p><strong>7.</strong> Horizontal RP -ve X (drawn right to left), RefDir 1,0,0:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016301e0eca5970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016301e0eca5970d image-full" alt="Lighting fixture on reference plane test 7" title="Lighting fixture on reference plane test 7" src="/assets/image_81133d.jpg" border="0" /></a><br />

</center>

<p><strong>Result:</strong> Look OK.

<p><strong>8.</strong> Horizontal RP -ve X (drawn right to left), RefDir 0,1,0:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016762d5ca98970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016762d5ca98970b image-full" alt="Lighting fixture on reference plane test 8" title="Lighting fixture on reference plane test 8" src="/assets/image_aff347.jpg" border="0" /></a><br />

</center>

<p><strong>Result:</strong> Lights rotated 90 degrees.

<p>Many thanks to Simon and Saikat for this example and all the research!
