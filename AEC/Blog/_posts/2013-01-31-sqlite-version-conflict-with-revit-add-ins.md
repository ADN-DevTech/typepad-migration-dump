---
layout: "post"
title: "SQLite Version Conflict with Revit Add-ins"
date: "2013-01-31 15:57:49"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/01/sqlite-version-conflict-with-revit-add-ins.html "
typepad_basename: "sqlite-version-conflict-with-revit-add-ins"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Let us assume that a Revit add-in references a SQLite Database (version 1.0.80.0) and the SQLite library files are deployed within the Revit add-in application. Since Revit uses SQLite (version 1.0.60.0) itself and invokes the add-in, it will generate a version conflict during runtime. </p>  <p><em><font size="1">InvalidCastException: System.Data.SQLite.SQLiteConnection cannot be cast to [B]System.Data.SQLite.SQLiteConnection. Type A originates from 'System.Data.SQLite, Version=1.0.60.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' in the context 'Default' at location 'C:\Windows\assembly\GAC_64\System.Data.SQLite\1.0.60.0__db937bc2d44ff139\System.Data.SQLite.dll'. Type B originates from 'System.Data.SQLite, Version=1.0.80.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' in the context 'Default' at location 'C:\Windows\Microsoft.Net\assembly\GAC_64\System.Data.SQLite\v4.0_1.0.80.0__db937bc2d44ff139\System.Data.SQLite.dll'. </font></em></p>  <p>How can we resolve this exception?</p>  <p>Here are the two approaches that came up during the internal discussions:&#160; </p>  <ul>   <li>Change the add-in to use 1.0.60 for Revit 2013</li>    <li>Or use a different <a href="http://msdn.microsoft.com/en-us/library/system.appdomain.createdomain%28v=vs.71%29.aspx" target="_blank">AppDomain</a> for the code that accesses the SQLite database. Since the Revit API communication must be in the standard domain, there will need to be a way to get the needed data across the boundary.</li> </ul>
