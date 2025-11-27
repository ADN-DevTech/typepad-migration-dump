---
layout: "post"
title: "Cleaning up after yourself: how and when to dispose of AutoCAD objects in .NET"
date: "2008-06-16 14:51:11"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Concurrent programming"
  - "F#"
  - "ObjectARX"
  - "RealDWG"
  - "Runtime"
original_url: "https://www.keanw.com/2008/06/cleaning-up-aft.html "
typepad_basename: "cleaning-up-aft"
typepad_status: "Publish"
---

<p>A question came up recently in an internal discussion and I thought I'd share it as it proved so illuminating.</p><blockquote><p><em>If I have an object of a type which implements IDisposable, is it good practice to explicitly dispose it (whether via the <a href="http://msdn.microsoft.com/en-us/library/yh598w02.aspx">using</a> statement or calling <a href="http://msdn.microsoft.com/en-us/library/system.idisposable.dispose.aspx">Dispose()</a> explicitly)?</em></p></blockquote><p>The quick(ish) answer is:</p><blockquote><p><em>Yes it is, but sometimes you might choose not to as the increase in code simplicity outweighs the benefits derived from manually disposing of the objects.</em></p></blockquote><p>So, naturally, the devil is in the detail. Let's take a look at the three scenarios where you're likely to be working with IDisposable objects inside AutoCAD:</p>

<ol><li>Temporary objects - such as those provided by Autodesk.AutoCAD.Geometry - which are never Database-resident</li>

<li>Temporary objects with the potential to be database-resident but which never actually get added to a Database</li>

<li>Database-resident objects added/accessed via a Transaction</li></ol>

<p>Below follows the details on each of these categories.</p>

<h4>Temporary objects of types not derived from DBObject</h4>

<p>The first category of temporary objects, such as Geometry.Line, are safe to be disposed of either &quot;manually&quot; (by your own code) or &quot;automatically&quot; (by the .NET garbage collector).</p>

<h4>Temporary objects of DBObject-derived types</h4>

<p>The second category of temporary objects, which are of a type derived from DBObject, <strong>must be disposed of manually</strong>. It is <strong>absolutely unsafe</strong> not to dispose of objects of DBObject-derived classes from the main execution thread in AutoCAD.</p>

<p>Why is this the case? Firstly, the majority of the classes available through AutoCAD's .NET API are currently unmanaged, with a relatively thin wrapper exposing them to the .NET world. Inside AutoCAD, all Database-resident objects are managed by a single-threaded runtime component, AcDb (which, along with some other components, is productized as Autodesk RealDWG). A side note: if you're using ObjectARX or RealDWG from C++, don't be confused by the fact your project's C-runtime memory management is likely to be &quot;Multi-threaded DLL&quot;, RealDWG is not thread-aware and so must be treated as single-threaded, for all intents and purposes.</p>

<p>And - secondly - on to the reason that automatic garbage collection is not to be trusted on DBObject-derived types: the .NET garbage collector runs on a separate, typically low-priority - unless memory is running short - thread. So if a DBObject-derived type is garbage-collected inside AutoCAD, a separate thread will essentially call into a non thread-safe component (RealDWG), which is very, very likely to cause AutoCAD to crash.</p>

<p>So you must always call Dispose() on temporary objects of DBObject-derived classes in your .NET code - you cannot rely on the CLR to manage your objects'&nbsp; lifetimes for you.</p>

<p>Interestingly, this is also the reason why the F# code I posted in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/parallelizing-r.html">this prior post</a> will not effectively leverage multiple cores (something I've just tested since getting a multi-core machine). We are using a Ray (which is a DBObject-derived class) to get intersection points with our path, and then disposing of this temporary object from an arbitrary thread (farmed off via F# Asynchronous Workflows). So this is unsafe, and won't run for long before crashing. At least now I understand why.</p>

<h4>Database-resident, Transaction-managed objects</h4>

<p>The third category of objects are those we use a Transaction either to add to the Database or to open for access. The good news is that - as the Transaction is aware of the objects it manages (via calls to AddNewlyCreatedDBObject() or to GetObject()), it is able to dispose of them automatically when it, itself, is disposed. So the key here is to make sure you wrap your use of Transactions in using blocks or call Dispose() on them when you're done. There is no need to explicitly dispose of the objects managed by a Transaction (unless there is a failure between the time the object is created and when it is added to the transaction via AddNewlyCreatedDBObject(), of course).</p>
