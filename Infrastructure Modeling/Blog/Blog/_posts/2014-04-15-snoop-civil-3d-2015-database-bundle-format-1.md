---
layout: "post"
title: "Snoop Civil 3D 2015 Database (.bundle format)"
date: "2014-04-15 11:21:04"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2015"
  - "Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/04/snoop-civil-3d-2015-database-bundle-format-1.html "
typepad_basename: "snoop-civil-3d-2015-database-bundle-format-1"
typepad_status: "Publish"
---

<p>The Civil 3D 2015 release is available, you can already download from our <a href="http://www.autodesk.com/products/autodesk-autocad-civil-3d/free-trial">website</a> and check the new APIs <a href="http://adndevblog.typepad.com/infrastructure/2014/03/autocad-civil-3d-2015-new-features-in-net-api-and-com-api-changes.html">here</a>. Now it’s time for our snoop database tool to be ready, actually I’m glad that the code base is the same, mainly because it’s based on reflection and automatically ‘understand’ changes on methods and properties.</p>
<p>Starting from the code posted <a href="http://adndevblog.typepad.com/infrastructure/2013/05/snoop-civil-3d-2014-database-bundle-format.html">here</a>, the new version is ready to download at the link below. Note it still using .NET 4.0 so it can remain compatible with previous release.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01a73dab3527970d img-responsive"><a href="http://adndevblog.typepad.com/files/civil3dsnoopdb.bundle-1.zip">Download</a></span></p>
<p>Like previous release, just extract the .zip under <em><span style="text-decoration: underline;">C:\Users\&lt;your login&gt;\AppData\Roaming\Autodesk\ApplicationPlugins</span></em> folder and it will autoload on Civil 3D 2012, 2013, 2014 and 2015. Please note that if you extract under <span style="text-decoration: underline;">C:\ProgramData\Autodesk\ApplicationPlugins</span> it will not load on 2012 version.</p>
