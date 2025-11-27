---
layout: "post"
title: "List Referencing Performance problem"
date: "2012-08-17 17:51:27"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/list-referencing-performance-problem.html "
typepad_basename: "list-referencing-performance-problem"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>Applies to:    <br />Autodesk Intent Professional 2011     <br />Autodesk® Inventor® Engineer-to-Order Series 2012</p>  <p><b>Issue</b></p>  <p>A potentially major performance problem was discovered in Inventor ETO Components 2012 and Intent Professional 2011 Update 3. A serious memory-crash problem involving the List data type discovered late in the release cycle of both products was remedied by making a copy of the list when it is referenced.&#160; This was known to be a potential performance problem, especially with large lists, but it resolved the crash issue. Since release, it has become evident that certain coding styles exacerbate the performance problem.&#160; For example, consider the following:</p>  <p style="line-height: 140%; font-family: arial; background: #eeeeee; color: black; font-size: 9pt">Rule myList As List = {1, 2, 3, 4, 5, 6}    <br />    <br />Rule slowLoop As List     <br />&#160;&#160;&#160;&#160; For index = 1 To Length(myList)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; slowLoop = slowLoop + { nth(index, myList) }     <br />&#160;&#160;&#160;&#160; Next index     <br />End Rule</p>  <p>In this example, myList is referenced 7 times.&#160; This is not an issue if myList is small.&#160; But with 1000 elements, it is copied 1001 times, creating more than a million copies of the elements; this <i>is</i> a problem.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>To work around this issue make a <i>single</i> reference to myList in the rule in a local variable, and then use the local variable.&#160; This is much faster, and will always be faster, since a local variable reference is faster than a rule reference.&#160; So this workaround need not be removed when the original problem is fixed.</p>  <p style="line-height: 140%; font-family: arial; background: #eeeeee; color: black; font-size: 9pt">Rule fastLoop As List    <br />&#160;&#160;&#160;&#160; Dim theList As List = myList     <br />&#160;&#160;&#160;&#160; For index = 1 To Length(theList)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; fastLoop = fastLoop + { nth(index, theList) }     <br />&#160;&#160;&#160;&#160; Next index     <br />End Rule</p>
