---
layout: "post"
title: "Snoop Civil 3D 2015 Database (.bundle format)"
date: "2014-04-15 11:20:48"
author: "Augusto Goncalves"
categories: []
original_url: "https://adndevblog.typepad.com/infrastructure/2014/04/snoop-civil-3d-2015-database-bundle-format.html "
typepad_basename: "snoop-civil-3d-2015-database-bundle-format"
typepad_status: "Draft"
---

<p>The Civil 3D 2015 release is available, you can already download from our <a href="http://www.autodesk.com/products/autodesk-autocad-civil-3d/free-trial">website</a> and check the new APIs <a href="http://adndevblog.typepad.com/infrastructure/2014/03/autocad-civil-3d-2015-new-features-in-net-api-and-com-api-changes.html">here</a>. Now it’s time for our snoop database tool to be ready, actually I’m glad that the code base is the same, mainly because it’s based on reflection and automatically ‘understand’ changes on methods and properties. </p>  <p>Starting from the code posted <a href="http://adndevblog.typepad.com/infrastructure/2013/05/snoop-civil-3d-2014-database-bundle-format.html">here</a>, the new version is ready to download at the link below. Note it still using .NET 4.0 so it can remain compatible with previous release.</p>  <p>Download</p>  <p>Like previous release, just extract the .zip under <em><u>C:\Users\&lt;your login&gt;\AppData\Roaming\Autodesk\ApplicationPlugins</u></em> folder and it will autoload on Civil 3D 2012, 2013, 2014 and 2015. Please note that if you extract under <u>C:\ProgramData\Autodesk\ApplicationPlugins</u> it will not load on 2012 version.</p>
