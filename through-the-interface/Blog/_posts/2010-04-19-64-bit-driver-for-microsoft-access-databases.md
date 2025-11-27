---
layout: "post"
title: "64-bit driver for Microsoft Access databases"
date: "2010-04-19 12:13:00"
author: "Kean Walmsley"
categories:
  - "Database"
original_url: "https://www.keanw.com/2010/04/64-bit-driver-for-microsoft-access-databases.html "
typepad_basename: "64-bit-driver-for-microsoft-access-databases"
typepad_status: "Publish"
---

<p><em>A big thanks to Stephen Preston for passing on this very interesting information. Stephen is one of the “volcanically challenged” members of my team, so fingers crossed he’ll be able to get a flight across to the UK in the coming days. Aside from Stephen’s delayed trip back to the motherland, the recent travel disruptions have also impacted an Inventor API training class being held in Moscow (which will go ahead with a back-up trainer) and extended another team member’s vacation in the Philippines.</em></p>  <p>The lack of a native 64-bit driver for <a href="http://en.wikipedia.org/wiki/Open_Database_Connectivity" target="_blank">ODBC</a>, <a href="http://en.wikipedia.org/wiki/ActiveX_Data_Objects" target="_blank">ADO</a>, <a href="http://en.wikipedia.org/wiki/OLE_DB" target="_blank">OLE DB</a>, etc. access to Microsoft Access databases has been a big problem for developers porting their code to support 64-bit, especially as when working within an AutoCAD plugin you’re not able to <a href="http://en.wikipedia.org/wiki/Thunk#Thunk_as_compatibility_mapping" target="_blank">thunk</a> down to 32-bit using <a href="http://en.wikipedia.org/wiki/WoW64" target="_blank">WoW64</a>.</p>  <p>Well, there is now a ray of hope breaking the horizon: Microsoft has released <a href="http://www.microsoft.com/downloads/details.aspx?familyid=C06B8369-60DD-4B64-A44B-84B371EDE16D&amp;displaylang=en" target="_blank">a beta version of their Access Database Engine component</a> (abbreviated to ACE, for Access Control Entry) which appears to be an x64-capable replacement for their 32-bit <a href="http://en.wikipedia.org/wiki/Microsoft_Jet" target="_blank">Jet</a> technology.</p>  <p>For more information, please check out <a href="http://blogs.msdn.com/psssql/archive/2010/01/21/how-to-get-a-x64-version-of-jet.aspx" target="_blank">this blog post</a>.</p>  <p>One word of warning… I stumbled across a mention of this component <a href="http://www.altova.com/Access-Database-OLEDB-32bit-64bit.html" target="_blank">not being able to co-exist with 32-bit versions of Office</a>. I haven’t tried this myself, but I thought I’d at least pass on that tidbit even if uncorroborated.</p>
