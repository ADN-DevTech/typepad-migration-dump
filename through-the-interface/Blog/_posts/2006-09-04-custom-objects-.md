---
layout: "post"
title: "Custom objects in .NET"
date: "2006-09-04 22:03:24"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Custom objects"
original_url: "https://www.keanw.com/2006/09/custom_objects_.html "
typepad_basename: "custom_objects_"
typepad_status: "Publish"
---

<p>One of the services ADN provides its members is representation of their wishes with Autodesk’s Engineering teams, helping influence product direction. For instance, each year we run an API wishlist survey for a number of our products (this year it was AutoCAD, ADT, Revit, Inventor, and Map 3D). An online survey for each product gets filled out by ADN members, allowing them to provide targeted feedback that goes straight to the appropriate Product Management contacts.</p>

<p>I wanted to talk about an item that has featured highly in recent AutoCAD API surveys – the exposure of custom objects through .NET. The reason I wanted to broach this subject in particular is that it has been high on the wishlist for a few releases, and I dislike the fact we haven’t yet been able to address it. So part of this entry explains the background to where we are today (I really don’t want to call it an apology :-) and the rest asks you to let us know – whether by email or by comments to this blog – what it is you want us to do to address the situation. Unfortunately I can’t commit to how and when it will be addressed, but I can guarantee that the feedback you provide will be compiled and brought to AutoCAD Engineering, to help influence the direction that’s taken.</p>

<p>OK, so let’s start with some background...</p>

<p>Custom objects were introduced back in Release 13. The mechanism was a core part of ARX, the AutoCAD Runtime eXtension (which later became ObjectARX), and allowed both Autodesk and external developers to implement new functionality inside AutoCAD without modifying the core product. It was pretty revolutionary at the time – it allowed developers deep access into AutoCAD, resulting in very tight integrations with the platform. Autodesk’s AutoCAD-based vertical applications (such as ADT, AutoCAD Mechanical, Map 3D, Civil 3D etc.) and many other complex 3rd party products would not have been possible without custom objects.</p>

<p>However powerful this mechanism has proven to be, there are downsides:</p>

<ul><li>The effort required to implement a fully-fledged custom object is high. Many different virtual functions need implementing and the entities need testing in many different scenarios – open, save, wblock, undo, explode, move, copy, scale, object snaps, grip stretching, trim, extend, refedit, audit, etc. etc.</li>

<li>There needs to be an Object Enabler or .DBX module for the object to be functional on someone else’s system<ul><li>This can be a problem for some customers – they would rather not depend on multiple providers to safeguard their data</li></ul></li>

<li>Custom objects are a mix of custom data (stored in the DWG) and custom behaviour (defined by the .DBX module) – there’s currently no way to have one without the other in ObjectARX<ul><li>Many developers implementing custom objects only really need a subset of the functionality they get</li>

<li>For example, many only really need custom graphics and/or behaviour – they don’t need to store large chunks of custom binary data</li></ul></li></ul>

<p>It would be technically relatively straightforward to expose ObjectARX’s current custom object mechanism to a managed environment. Ultimately, though, this would not actually improve on the existing mechanism very much at all: the benefits from a developer’s perspective would be around the use of VB.NET or C# to implement the various callbacks needed to define the data to be stored in the DWF &amp; DXF formats and the behaviour of the objects when used inside AutoCAD. While it would allow developers to consolidate more of their development under a single .NET language, it would not reduce the number of callbacks that would need to be implemented, nor would it make the objects inherently easier to develop.</p>

<p>C++ is considered one of the more complicated programming languages to learn: requiring it for exposure of custom objects (which is the situation right now, mainly from a historical perspective), considerably reduces the audience for this technology. Now that may or may not be a bad thing, depending on your perspective: the complexity of the architecture makes it hard to do <strong>*anyway*</strong>, so having the additional hurdle of C++ getting in the way does naturally restrict it to people that are strongly motivated (enough to learn C++ or hire C++ programmers to get there).</p>

<p>So what’s the right thing to do? Clearly we could just go ahead and expose the mechanism as it is today in ObjectARX. And yet here we are with a technology we know to be highly complex and difficult to implement, and an ideal opportunity to redesign it – enabling more people to harness it effectively at lower effort. The more favoured approach (at least from our perspective) would be to investigate further how better to meet developers’ needs for enabling custom graphics/behaviour (a.k.a. stylization) in AutoCAD – in a way that could be supported technically for many releases to come.</p>

<p>Here are some of the questions that we have been asking ourselves, for instance...</p>

<ul><li>What could be done to enable stylization without custom data being stored? Should this be implemented per class or per object instance?</li>

<li>Can we reduce the requirement to store custom binary data, perhaps by enabling data storage in XML fragments?</li>

<li>What (if anything) needs to be done to the existing Dynamic Blocks mechanism to meet developers’ needs?</li></ul>

<p>We did ask our ADN members a follow-up question during a previous API wishlist survey. Here are the results (respondents could select multiple options, hence the [response rate] numbers adding up to &gt;100%):</p>

<ul><li>Support for custom drawing of object instances. (i.e. stylization) [83%]</li>

<li>Support for custom transform on object instances. (ability to override how stretch, move etc. work on an instance by instance basis) [60%]</li>

<li>Support for custom grips on object instances. (ability to add or override grips on a specific line, for instance) [58%]</li>

<li>Support for custom copy on object instances. (ability to override how objects are copied on an instance by instance basis) [42%]</li></ul>

<p>So... what do you think? If you care strongly about what Autodesk should do about this topic, <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/custom_objects_.html#comments">post a comment</a> to this blog or <a href="mailto:kean.walmsley@autodesk.com">send me an email</a>. As I expect this to generate quite a lot of responses (given the clear popularity of this item in our wishlist surveys), I apologise in advance if I don’t manage to respond to each one.</p>
