---
layout: "post"
title: ".NET API namespace simplification in AutoCAD Civil 3D 2013"
date: "2012-04-04 04:03:04"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/net-api-namespace-simplification-in-autocad-civil-3d-2013.html "
typepad_basename: "net-api-namespace-simplification-in-autocad-civil-3d-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>AutoCAD Civil 3D 2013 release introduces a namespace restructuring that simplifies referencing objects. We have fewer namespaces in AutoCAD Civil 3D .NET API assembly (AeccDbMgd.dll) compared to a long list of the namespaces in 2012 or earlier release. Here is the first look of the simplified namespaces in Civil 3D 2013 .NET API -</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016764a0861d970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016764a0861d970b" title="C3D2013_API_Namespace" src="/assets/image_db4333.jpg" border="0" alt="C3D2013_API_Namespace" /></a><br /><br /><br />And here is the long list of the namespaces in Civil 3D 2012 .NET API -</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168e9a1905d970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0168e9a1905d970c" title="C3D2012_API_Namespace" src="/assets/image_8d82d9.jpg" border="0" alt="C3D2012_API_Namespace" /></a></p>
<p><br />You can see clearly that in AutoCAD Civil 3D 2013 release, all the feature name/domain levels (e.g. Land, PipeNetwork, Roadway...) are removed from the namespaces. This change will necessitate your existing projects to be rebuilt by updating the new namespaces. This restructuring of the namespaces will improve the usability by grouping related classes into the same namespace. Currently, some classes need to access other namespaces to work with certain types. With the new structure, all related classes will be grouped in the same namespace, which will simplify the usage.</p>
<p><br /><br /></p>
