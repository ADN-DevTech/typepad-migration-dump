---
layout: "post"
title: ".NET and AutoCAD"
date: "2006-06-28 13:45:28"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2006/06/net_and_autocad.html "
typepad_basename: "net_and_autocad"
typepad_status: "Publish"
---

<p>The AutoCAD Engineering first prototyped a &quot;managed&quot; (for &quot;managed&quot; read &quot;.NET&quot;) API for AutoCAD 2004. It was pretty revolutionary stuff at the time - a mixed-mode DLL was created to expose the managed interface and marshall these calls through to &quot;unmanaged&quot; ObjectARX calls. There were a number of reasons .NET was - and remains - very interesting for developers...</p>

<p>.NET provides the ease of development previously available only (or at least primarily) to Visual Basic clients through COM. You can make use of COM or .NET components in your project, but generate simple client code using more evolved programming languages (even VB.NET is really a much more evolved language than VB6, in terms of its intrinsic capabilities).</p>

<p>A key benefit for Autodesk was the ability to map more complex datatypes (such as those defined in ObjectARX) to a managed API. When designing a COM API you are more limited - you can use certain basic types or beyond that define more complex interfaces based on IDispatch, but these come with a substantial design and development overhead: it is laborious to expose COM Automation interfaces for complex C++ classes. With .NET things were different... the consistency of the design of ObjectARX provided us with the capability to automate this to a great extent, generating code semi-automatically from our internal API definition database (which is the same one used to generate our API reference material for both ObjectARX and the managed API for AutoCAD).</p>

<p>For some initial pointers around using the .NET API to AutoCAD, check out the <a href="http://www.autodesk.com/developautocad">AutoCAD Developer Center</a> - as mentioned previously the <a href="http://images.autodesk.com/adsk/files/AutoCAD_NET_labs.zip">Labs</a> are a great place to start. In my next few posts I'll talk about the basic structure of a simple .NET application for AutoCAD, and eventually how to harness the existing COM and unmanaged ObjectARX APIs through COM Interop and P/Invoke respectively.</p>
