---
layout: "post"
title: "Managing SDK Samples"
date: "2008-08-30 14:00:00"
author: "Jeremy Tammik"
categories:
  - "Getting Started"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2008/08/managing-sdk-sa.html "
typepad_basename: "managing-sdk-sa"
typepad_status: "Publish"
---

<p>As mentioned in the <a href="http://thebuildingcoder.typepad.com/blog/2008/08/the-revit-sdk-c.html">previous post</a>, the Revit SDK samples provide the biggest knowledge base on how to solve any specific programming task in Revit. The API documentation RevitAPI.chm lists the available classes and their properties and methods, and the API diagram in 'Revit API Diagram.dwf' shows their class hierarchy, but there is no information there about how they work together to provide specific functionality. The getting started document 'Getting Started Revit API 2009.doc' does provide some background information about the Revit API. A Revit API user manual explaining in more depth how to solve specific tasks is available from ADN. Since this document has not yet been completed and added to the standard SDK documentation, I am appending a copy of it <a href="http://thebuildingcoder.typepad.com/blog/files/revit_2009_api_dev_guide.doc">here</a> as a Word doc file for 2009 and <a href="http://thebuildingcoder.typepad.com/blog/files/Revit_2008_API_User_Manual.pdf">here</a> in PDF format for 2008.</p>

<p>And anyway, as every programmer knows, the only completely trustworthy source of information on any API is source code. So let's return to the samples, which provide exactly that.</p>

<p>To examine any sample in depth, we need to <strong>compile</strong> and <strong>load</strong> it. Compiling it involves loading the source code into the development environment and creating the executable binary code, the .NET class assembly. Loading it incurs making certain changes to the Revit.ini file, which is located in the same directory as Revit.exe. Both of these steps are well documented in 'Getting Started Revit API 2009.doc', and also explained and demonstrated in the Revit API <a href="http://www.autodesk.com/developrevit">DevTV presentation</a>.</p>

<p>However, with over one hundred samples to explore, it would be a rather onerous task to perform these steps manually and individually for each sample, one at a time. Actually, it is even impossible to add all the samples to the Revit Tools &gt; External Tools menu, because it cannot host more than about fifty entries at once.</p>

<p>Luckily, additional tools are included in the Revit SDK which help us address each of these two steps easily for all samples in one fell swoop: a Visual Studio solution file <strong>SDKSamples2009.sln</strong> is provided which allows us to compile all samples in one go. In addition, it provides a good base for debugging and searching the entire source code. Secondly, one of the samples provided, <strong>RvtSamples</strong>, is an external application which creates a hierarchical menu substructure within the Revit menu enabling us to load any one of the other samples. As an external application, it also needs to be added to the Revit.ini file to inform Revit to load it, but requires only a couple of lines instead of hundreds.</p>

<p>Finally, one useful resource providing an overview over all Revit SDK samples is the read-me file SamplesReadMe.htm, which was added in the Revit 2009 release. Whereas each individual sample has its own read-me file with detailed information and instructions on the sample, the global read-me file provides an overview and classification in several different ways: by skill level, release version, and category, such as database access, new object creation, geometry. This can be used to decide where to start looking for the material of most interest to you.</p>
