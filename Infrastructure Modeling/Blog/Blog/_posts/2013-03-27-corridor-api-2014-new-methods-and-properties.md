---
layout: "post"
title: "Corridor API 2014: new methods and properties"
date: "2013-03-27 13:44:06"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2014"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/corridor-api-2014-new-methods-and-properties.html "
typepad_basename: "corridor-api-2014-new-methods-and-properties"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>As mentioned on our previous post on <a href="http://adndevblog.typepad.com/infrastructure/2013/03/whats-new-in-autocad-civil-3d-2014-api.html">What’s New</a>, now we can create and modify Corridor and its components, like Baselines, BaselineRegions, Assemblies and Subassemblies.</p>  <p>As several developers create reports based on the API, this post show some of the new methods and properties available:</p>  <ul>   <li>Corridor</li>    <ul>     <li>GetLinkPoints, GetPointCodes, GetShapeCode, RegionLockMode, SlopePatterns</li>   </ul>    <li>Baselines</li>    <ul>     <li>SetAlignmentAndProfile</li>   </ul>    <li>BaselineRegion</li>    <ul>     <li>AppliedAssemblySetting, GetOverriddenStations, Match, Merge, RemoveOverriddenStation, Split</li>   </ul>    <li>Assembly</li>    <ul>     <li>CodeSetStyleId, CodeSetStyleName, Groups, Location, OffsetAssemblies, Type (AssemblyType), AddSubassembly, CopySubassembly, Insert/Mirror/Replace Subassembly</li>   </ul>    <li>Subassembly</li>    <ul>     <li>PointIndexHookTo, OffsetAssemblyName</li>   </ul>    <li>Collection with ability to iterate, add/import and remove:</li>    <ul>     <li>BaselineCollection</li>      <li>BaselineRegionCollection</li>      <li>SubassemblyCollection</li>      <li>AssemblyCollection</li>   </ul> </ul>  <p>Some methods are now deprecate, when you build with the new references, a warning message should appear at the compiler output.</p>  <p>Hope this help you get a feel of the big improvements we did on this release. Next we’ll show some code samples. Stay tuned.</p>
