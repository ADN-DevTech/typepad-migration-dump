---
layout: "post"
title: "No Inheritance and No Strong Naming"
date: "2014-01-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "ARX"
  - "BIM"
  - "Getting Started"
  - "Training"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/01/no-inheritance-and-no-strong-naming.html "
typepad_basename: "no-inheritance-and-no-strong-naming"
typepad_status: "Publish"
---

<p>Here are two little items of general long-standing interest that just came up in a Revit API discussion forum:</p>

<ul>
<li><a href="#2">Why you cannot derive a new class from FamilyInstance</a>.</li>
<li><a href="#3">Why the Revit API assemblies are not strongly named</a>.</li>
</ul>

<p>These two issues sometimes come as a surprise to developers experienced in

<a href="http://en.wikipedia.org/wiki/Object-oriented_programming">
OOP</a> and other CAD system programming.</p>

<p>However, there are good reasons for both.</p>

<p>Here are the detailed explanations:</p>



<a name="2"></a>

<h4>Why You Cannot Derive a New Class From FamilyInstance</h4>

<p><strong>Question:</strong> I would like to extend some classes provided by the Revit API, e.g. by inheriting from them and implementing my own derived classes.
I think it would be very useful to specialise some classes, like FamilyInstance.</p>

<p><strong>Answer:</strong> Do you have any particular goals behind inheriting from FamilyInstance?
What would you like to be able to accomplish that inheritance or extensibility would allow?</p>

<p><strong>Question:</strong> I created an add-in that uses a Revit family to delimitate an area.
I call it a 'marker' and can identify it by its symbol.
It helps identify where and how other element need to be placed.</p>

<p>Having a public constructor will allow us to inherit our marker class from FamilyInstance.
In fact, a marker IS a family instance, with just some other properties.
Now we have to copy some data from the FamilyInstance to the marker it belongs to, like its unique id, location, rotation and so on.</p>

<p>It would be easier for us to serialize and deserialize the class being sure correct data is copied to the database.</p>

<p>Also the code understandability would be improved, since calling <code>new FamilyInstance(x,y,z, rotation, symbol)</code> is much more obvious than calling a static constructor.</p>

<p>Any programmer would expect that calling a constructor such as this would drop a new instance into the project.</p>


<p><strong>Answer:</strong> Thank you for the clarification.</p>

<p>The structure of Revit and it’s API is a little bit different than I think you expect, so this is not going to work as well as you hope.</p>

<p>Revit API objects are managed wrappers around native C++ objects used by Revit’s UI and database.
Therefore, a FamilyInstance in the API does not actually contain the properties you see &ndash; the API code accesses them through the matching native object’s members.    So extending our managed classes would not actually allow for new data to end up in the Revit file during serialization &ndash; we don’t know how to get the data from your inherited class.</p>

<p>We don’t supply public constructors for many of these objects for precisely this reason &ndash; much more needs to happen than just building the object and populating the x, y, z, etc. &ndash; the document tables need updating, dependent elements must be added, relationships established etc.  So this is what we have methods like NewFamilyInstance and, for our newer element creation calls, static creation methods taking the document and necessary parameters.</p>

<p>If you want to get additional serialized data onto a Revit element, you should look at

<a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23">
Extensible Storage</a> in the Revit API.</p>

<p>If you want to make code easier to write and want to 'add' additional members to our classes (so you could have a call to a FamilyInstance.GetMarker method or something like that), you can look at

<a href="http://thebuildingcoder.typepad.com/blog/2010/02/getpolygon-extension-methods.html">
C# extension methods</a> &ndash;

however, adding such methods will not allow additional data to be stored in those classes unless you implement these members using Extensible Storage.</p>




<a name="3"></a>

<h4>Why the Revit API Assemblies are not Strongly Named</h4>

<p><strong>Question:</strong> Looking at the Revit API documentation, I do not see any mention of

<a href="http://en.wikipedia.org/wiki/Strong_key">
strong naming</a>.</p>

<p>Is there a particular reason for that?</p>

<p>This can cause several security issues, and some customers may require us to sign our Revit add-in assemblies, which we currently cannot do.</p>

<p><strong>Answer:</strong> Yes, this is true.
There are good reasons for this.</p>

<p>Here is an explanation why

<a href="http://adndevblog.typepad.com/autocad/2012/07/can-i-sign-my-autocad-net-plug-in-with-a-strong-name.html">
you cannot sign your AutoCAD .NET plug-in with a strong name</a>.</p>

<p>It covers all the technical details and includes some workarounds, which all apply to Revit add-ins as well.</p>

<p>Note that Microsoft modified the strong name verification system so that

<a href="http://blogs.msdn.com/b/shawnfa/archive/2008/05/14/strong-name-bypass.aspx">
some security measures related to signatures are now bypassed</a> by

default.</p>


<a name="4"></a>

<h4>Wise Words</h4>

<p>I hope these explanations make sense.</p>

<p>There are quite a few surprises lurking in the Revit API and the entire Revit product paradigm for unwary developers experienced in other less parametric and BIM oriented CAD systems, as we have already repeatedly seen in the past:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/02/bim-versus-free-geometry-and-product-training.html">BIM versus free geometry and product training</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2012/10/porting-an-autocad-application.html">Porting an AutoCAD application</a></li>
</ul>

<p>In fact, as I often point out, one of the hardest tasks for a budding Revit add-in developer experienced in ObjectARX or other CAD system programming is to forget her accumulated previous experience and open her mind for something completely new and different.</p>

<p><a href="http://www.wikihow.com/Become-a-Lifelong-Learner">Learning something new</a> and

<a href="http://www.marcandangel.com/2013/09/02/5-things-you-should-know-about-letting-go">
letting go of something old</a>

can prove very challenging indeed.</p>

<p>And very rewarding.</p>

<p>Good luck!</p>
