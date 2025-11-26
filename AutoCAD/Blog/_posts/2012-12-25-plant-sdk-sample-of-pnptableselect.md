---
layout: "post"
title: "Plant SDK: Sample of PnPTable.Select()"
date: "2012-12-25 07:05:32"
author: "Marat Mirgaleev"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "Marat Mirgaleev"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/plant-sdk-sample-of-pnptableselect.html "
typepad_basename: "plant-sdk-sample-of-pnptableselect"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><b>Issue</b></p>  <p>What if I want to apply a selection filter to PnPTable.Select()? </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>One of the PnPTable.Select() overloads accepts a filter string as a parameter in a form of the SQLite's SELECT-WHERE clause. Please, note the syntax of the expression: if the field name contains spaces, it must be enclosed in quotes. The value is enclosed in apostrophes (you can find the escape sequences description in the SQLite documentation, if you search it online).</p>  <p>In this sample we are trying to use the PnPDrawings table to filter on the &quot;Dwg Name&quot; field:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <p style="margin: 0px"><span style="line-height: 140%">&#160; PnPTable tbl = db.Tables[</span><span style="line-height: 140%; color: #a31515">&quot;PnPDrawings&quot;</span><span style="line-height: 140%">];</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: blue">const</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">string</span><span style="line-height: 140%"> where = </span><span style="line-height: 140%; color: #a31515">&quot;\&quot;Dwg Name\&quot;='PIP-01-102.dwg'&quot;</span><span style="line-height: 140%">;</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160; PnPRow[] rows = tbl.Select(where);</span></p>    <p style="margin: 0px"></p> </div>
