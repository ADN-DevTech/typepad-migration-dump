---
layout: "post"
title: "Zero Touch Node Wrapper and Load from Stream"
date: "2019-08-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Dynamo"
  - "Philosophy"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/08/zero-touch-node-element-wrapper-and-load-from-stream.html "
typepad_basename: "zero-touch-node-element-wrapper-and-load-from-stream"
typepad_status: "Publish"
---

<p>Let's start this week with these topics that came up in the last one:</p>

<ul>
<li><a href="#2">Dynamo Zero Touch Node Revit element wrapper</a></li>
<li><a href="#3">Loading a .NET assembly from a memory stream</a></li>
<li><a href="#4">How to become a successful freelancer</a></li>
</ul>

<p>Talking about memory streams, I hiked up Rio Chillar in Nerja, Andalusia:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4a15fe2200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4a15fe2200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Natural stream &ndash; Rio Chillar" title="Natural stream &ndash; Rio Chillar" src="/assets/image_4025c8.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a> Dynamo Zero Touch Node Revit Element Wrapper</h4>

<p>Yet another solution suggested by
Frank <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518">@Fair59</a> Aarssen
helped Alexandra Nelson share the solution to implement a wrapper for passing back a Revit element to Dynamo from a C# Zero-Touch node, in the thread 
on <a href="https://forums.autodesk.com/t5/revit-api-forum/trying-to-retrieve-the-dimensiontype-of-a-dimension/m-p/8968599">retrieving the <code>DimensionType</code> of a <code>Dimension</code></a>:</p>

<p><strong>Question:</strong> I'm trying to build a ZeroTouchNode with C# in Visual Studio whose input is a <code>Dimension</code> element and outputs its <code>DimensionType</code>. It seemed simple, but I'm having trouble returning the <code>DimensionType</code> element. Instead, the node is returning "Autodesk.Revit.DB.DimensionType". How do I retrieve the actual element? </p>

<p>When I try to return the element id, the id of the dimension type appears, but not the dimension type element.</p>

<p>When I tried to return the Revit element itself using the <code>GetElement</code> method and changed the return type to <code>Element</code> rather than <code>ElementType</code>, it still gives me the same output.</p>

<p><strong>Answer:</strong> You cannot directly return a Revit class. You need to wrap it in a Dynamo wrapper class.</p>

<p>Please see how to <a href="https://forum.dynamobim.com/t/become-a-dynamo-zero-touch-c-node-developer-in-75-minutes/28007">become a Dynamo Zero Touch C# Node developer in 75 minutes</a>
and download the handout.</p>

<p><strong>Answer:</strong> Thank you for your help and for sharing the link with me. I worked through the concepts in that handout and got it working.</p>

<p>Here's the working script:</p>

<pre class="code">
  using Autodesk.Revit.DB;
  using RevitServices.Persistence;
  using Revit.Elements;

  namespace theWorks
  {
    public class Dimensions
    {
      private Dimensions() { }

      public static Revit.Elements.Element GetDimType(
        Revit.Elements.Element dimension )
      {
        Document doc = DocumentManager.Instance
          .CurrentDBDocument;

        Autodesk.Revit.DB.Element UnwrappedElement
          = dimension.InternalElement;

        ElementId id = UnwrappedElement.GetTypeId();

        ElementType dimType = doc.GetElement(id)
          as ElementType;

        return dimType.ToDSType(false);
      }
    }
  }  
</pre>

<p>Many thanks for Alexandra and Frank for clarifying this!</p>

<h4><a name="3"></a> Loading a .NET Assembly from a Memory Stream</h4>

<p>A little note on how the add-in manager avoids locking the DLLs it loads:</p>

<p><strong>Question:</strong> I am interested in how the Revit add-in manager manages (:wink:) to reload addins on the fly &ndash; in Dynamo, we have the notion of packages, which are similar to add-ins (a set of DLLs or Dynamo code we might need to load)  &ndash; reloading or unloading can be done for Dynamo code, but for .NET DLLs &ndash; my understanding is that in the NET framework (prior to the release of .NET core 3) you cannot unload an assembly once loaded &ndash; how does the addin manager do this?</p>

<p><strong>Answer:</strong> You can load .NET code either from a DLL file on disk or from a stream in memory.</p>

<p>The <a href="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8">Assembly.Load method</a> Loads an assembly.</p>

<p>It also provides
an <a href="https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly.load?view=netframework-4.8#System_Reflection_Assembly_Load_System_Byte___">overload taking a <code>Byte</code> array argument</a>. That loads the assembly from a memory stream instead.
Cf. also <a href="https://stackoverflow.com/questions/40384619/how-to-load-assembly-from-stream-in-net-core">how to load assembly from stream</a>.</p>

<p>The add-in manager reads the DLL file from disk, converts it to a memory stream, and uses that to load the .NET code into the .NET environment. Therefore, .NET never gets to see or touch (or lock) the DLL file.</p>

<p>This approach has also been used to implement frameworks
enabling <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.49">debug and continue functionality for Revit add-ins</a>.</p>

<h4><a name="4"></a> How to Become a Successful Freelancer</h4>

<p>Let\s close with this 90-minute <a href="https://www.freecodecamp.org">freeCodeCamp</a> interview
on <a href="https://www.freecodecamp.org/news/how-to-become-a-successful-freelancer-podcast">how to become a successful freelancer</a>:</p>

<blockquote>
  <p>Kyle dropped out of school and worked as a jewellery salesman before teaching himself to code.</p>
  
  <p>His freelance business grew, and he now runs a profitable software development consultancy...</p>
</blockquote>
