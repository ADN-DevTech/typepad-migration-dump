---
layout: "post"
title: "Finding all elements in a view"
date: "2012-06-13 01:57:47"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/06/finding-elements-in-a-view.html "
typepad_basename: "finding-elements-in-a-view"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p> You can find all elements in a view using FilteredElementCollector filer class. The first parameter is the document and second parameter is the element Id of the view.   <p>Here is a sample code.</p>  <p><font face="Calibri"><span><font color="#2b91af">FilteredElementCollector</font></span><font color="#000000"> coll = </font><span><font color="#0000ff">new</font></span><font color="#000000">&#160;</font><span><font color="#2b91af">FilteredElementCollector</font></span></font><font face="Calibri"><font color="#000000">(view.Document, view.Id);        <br />coll = coll.WhereElementIsNotElementType();         <br /></font><span><font color="#2b91af">IList</font></span><font color="#000000">&lt;</font><span><font color="#2b91af">Element</font></span></font><font face="Calibri"><font color="#000000">&gt; elementList = coll.ToElements();        <br /></font><span><font color="#0000ff">foreach</font></span><font color="#000000"> (</font><span><font color="#2b91af">Element</font></span><font color="#000000"> el </font><span><font color="#0000ff">in</font></span></font><font face="Calibri"><font color="#000000"> elementList)        <br />{         <br />&#160; </font><span><font color="#008000">// Do something for each element</font></span>       <br /></font><font color="#000000">     <br /><font face="Calibri">{</font></font></p>
