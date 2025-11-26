---
layout: "post"
title: "Snoop Civil 3D 2014 Database"
date: "2013-04-05 13:58:15"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2014"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/snoop-civil-3d-2014-database.html "
typepad_basename: "snoop-civil-3d-2014-database"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>
<p>After a <a href="http://civilizeddevelopment.typepad.com/civilized-development/2011/10/introducing-snoop-civil-3d-2012-database.html">first launch</a>, now it is time for a new version!</p>
<p>The good news is that his tool uses Reflection to inspect the objects, so all new properties will appear on the list without extra work, like the <a href="http://adndevblog.typepad.com/infrastructure/2013/03/corridor-api-2014-new-methods-and-properties.html">new properties</a> available on the Corridor 2014 related objects are already there. Additionally, the main list now includes <strong>Point Groups</strong>.</p>
<p>The main new feature is the ability to go from one entity to another: from one object, e.g. <strong>Baseline</strong>, if we click on the <strong>AlignmentId</strong> property, the tool will automatically open the Alignment and show its properties! That enable us to go from one Corridor Baseline to the respective Alignment very easily! Also work for all other ObjectId properties.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c38601dd8970b-pi"><img alt="objectid_property" border="0" height="407" src="/assets/image_e26b1a.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="objectid_property" width="492" /></a></p>
<p>Download the <span class="asset  asset-generic at-xid-6a0167607c2431970b017d42aa4e0d970c"><a href="http://adndevblog.typepad.com/files/snoopcivil3d_sourcecode-1.zip">source code</a></span> and&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b017d42aa4e7b970c"><a href="http://adndevblog.typepad.com/files/snoopcivil3d_executable-1.zip">executable</a></span>, NETLOAD the <em>SnoopCivil3DObjects.dll</em> and run <strong>SNOOPCIVIL3DDB</strong> command. Enjoy!</p>
<p>Feel free to leave feedback :-)</p>
<p><strong>Update</strong>: after some feedback, I made some minor changes on the tools. This version is updated with the changes.</p>
