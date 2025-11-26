---
layout: "post"
title: "Monitoring object during UNDO/REDO with .NET API"
date: "2013-01-09 10:36:52"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/monitoring-object-during-undoredo-with-net-api.html "
typepad_basename: "monitoring-object-during-undoredo-with-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>When an object is added, modified or deleted from the drawing, a common scenario consist in write that information to an XML file by using the <strong>ObjectAdded</strong> and <strong>ObjectErased</strong> events of the database reactor. But when UNDO and REDO commands are used in between, these events did not work. Is there any way to handle this situation with reactors?</p>  <p>In this situation, two reactors need to be used, one is document reactor and the other is database reactor. For example, monitor two events of the document, <strong>CommandWillStart</strong> and <strong>CommandEnded</strong> to know when the UNDO/REDO command is going to start and when it ends. Then in the events of the current database, <strong>ObjectReappended</strong> and <strong>ObjectUnappended</strong>, make records what objects have been unappended or reappended.</p>  <p>Then in the <strong>CommandEnded</strong> event of the current document iterate through those recorded objects, check if some particular information you want and do any other operations if necessary such as updating your XML file. </p>  <p>Generally speaking, avoid manipulating objects and database in any events other than <strong>CommandEnded</strong> since the current database is most of times in a flux state and related objects are already in read/write mode.</p>
