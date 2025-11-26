---
layout: "post"
title: "Integration with a Database or ERP System"
date: "2009-07-27 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "External"
  - "SDK Samples"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/07/integration-with-a-database-or-erp-system.html "
typepad_basename: "integration-with-a-database-or-erp-system"
typepad_status: "Publish"
---

<p>A while ago, I published Miroslav Schonauer's popular high-level 

<a href="http://thebuildingcoder.typepad.com/blog/2009/01/database-integration.html">
overview of Revit and database integration</a>. 

Here is an additional useful note from Anthony Hauck on this topic:</p>

<p><strong>Question:</strong>
How can I integrate Revit with an ERP solution or an SQL database?</p>

<p><strong>Answer:</strong>
Revit information can be exported to an ODBC-compatible database. 
ODBC export is a native feature of Revit. 
The Labs posting 

<a href="http://labs.autodesk.com/utilities/revit_rdb">
RDBLink</a>

demonstrates a similar capability using the API, with the additional feature of being able to change some parameters within a Revit model by editing the resulting database and re-importing the changes.
Some of these parameter changes can affect Revit model geometry.</p>

<p>The 

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/rfa-version-grey-commands-family-context-and-rdb-link.html#4">
availability of RDBLink</a>

has already been mentioned briefly previously.</p>
