---
layout: "post"
title: "Compatibility of AutoCAD applications between releases"
date: "2006-06-26 18:08:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
original_url: "https://www.keanw.com/2006/06/compatibility_o.html "
typepad_basename: "compatibility_o"
typepad_status: "Publish"
---

<p>This is a huge topic, so I'm not going to be able to do it justice in this one post.</p>

<p>Applications developed using AutoCAD's APIs need to be tested (and sometimes ported) to make sure they work with a new release of the AutoCAD platform. For several generations of AutoCAD we have consciously chosen to break binary application compatibility once every 3 releases (historically - but in recent memory - this happened for AutoCAD 2000, 2004 and now 2007). To make applications work on interim releases they might need minor porting work to migrate their use of Registry settings to be under the right location (for example), but we do test to minimise the pain experienced by our developers.</p>

<p>Also, applications implemented using different APIs typically require different efforts to port from one release to the next. Generally LISP applications are fairly portable between releases (although the names and locations of support files may change from time to time), as are COM clients (such as VBA and external VB). Applications using the managed API (C# or VB.NET clients) are a little more portable than ObjectARX (C++) applications, but as the managed API has been evolving quickly we have so far not always enforced compatibility (the exception being between AutoCAD 2005 and 2006, which was otherwise a binary application compatible release), Moving forwards I would expect to see more stability and release-to-release compatibility in our managed API to AutoCAD.</p>

<p>Architecturally ObjectARX applications are closest to AutoCAD's core (in terms of their implementation) so it is really for these modules that developers - whether external or internal to Autodesk - need to spend most development effort when application compatibility is broken.</p>

<p>So why do we break compatibility at all? Well there are a few reasons...</p>

<ol><li>Sometimes we simply want to update API classes, to add new methods etc. While we can add non-virtual methods during an API-compatible release, adding virtual methods changes a module's v-table and breaks compatibility.</li>

<li>We also need to update our internal architecture or use of technology. An example of this is our extensive use of <a href="http://www.unicode.org/standard/WhatIsUnicode.html">Unicode</a> in AutoCAD 2007. This is a deep - and wide-reaching - change to the way AutoCAD handles strings internally, and has therefore impacted many ObjectARX function signatures.</li>

<li>Finally we also want to take advantage of the latest compiler technology to build AutoCAD. AutoCAD 2000-2002 were built with Visual Studio 6, AutoCAD 2004-2006 were built with Visual Studio .NET 2002 and AutoCAD 2007 was built with Visual Studio 2005. It's important for us to remain on a supported compiler version, in order to be able to get critical issues addressed by Microsoft, but beyond that it also provides us with new development capabilities, such as the possibility to make use of WinForms in AutoCAD and expose a managed API.</li></ol>

<p>The third point has some subtleties: a pure C++ API could (in theory) be version independent, but whenever C-runtime or MFC classes are used in function signatures (or class derivations), then you do become more closely linked to the specific version of the compiler used to build the API provider (i.e. AutoCAD). So while clients of C++ APIs don't automatically need to use the same compiler version as the API provider, applications using ObjectARX do have this requirement.</p>

<p>So why don't we break compatibility every release? Because of the pain felt by our customers, and by developers. Non-availability of applications for a particular release has the potential to impact adoption of that release, as many customers are dependent on an independent application to perform their work. Maintaining a 3-release (which right now means a 3-year) window gives developers more time to focus on driving customer value by implementing serious enhancements to their applications when they are not focusing development effort on migration to support the basic requirements of the new platform.</p>

<p>It's DevTech's job to help minimise our developers' pain on both these fronts, whether when dealing with migration issues or helping explain the additional features and APIs available in a new release of one of our products.</p>
