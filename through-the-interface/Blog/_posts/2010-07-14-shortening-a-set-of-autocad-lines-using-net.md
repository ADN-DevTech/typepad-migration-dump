---
layout: "post"
title: "Shortening a set of AutoCAD lines using .NET"
date: "2010-07-14 14:25:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
  - "Geometry"
original_url: "https://www.keanw.com/2010/07/shortening-a-set-of-autocad-lines-using-net.html "
typepad_basename: "shortening-a-set-of-autocad-lines-using-net"
typepad_status: "Publish"
---

<p>Brent Burgess <a href="http://through-the-interface.typepad.com/through_the_interface/2010/02/extending-a-set-of-autocad-lines-using-net.html#comment-6a00d83452464869e20133f2393489970b" target="_blank">commented</a> on <a href="http://through-the-interface.typepad.com/through_the_interface/2010/02/extending-a-set-of-autocad-lines-using-net.html" target="_blank">this previous post</a> showing how to extend lines in AutoCAD:</p>
<blockquote>
<p><em>Is there a different process for shortening lines, or is it similar syntax? </em></p></blockquote>
<p>A very valid question… unfortunately the Curve.Extend() method only works with points or parameters that are outside the current definition of the curve, so we need to find another way (if you try, you’ll get an eInvalidInput, at least that’s what I found :-).</p>
<p>I chose the approach of using Curve.GetSplitCurves() on each line, passing in an array of parameters at which to split the curve. I calculate the start parameter as 25% along the length, and the end parameter as being at the same point from the end. Which means each line will end up being half its original size. Lines are generally parameterised between 0 and 1, so we’ll be passing an array containing 0.25 and 0.75 to the GetSplitCurves() method, which should consequently return an array of exactly three objects: lines from 0 to 0.25, 0.25 to 0.75 and from 0.75 to 1. We will therefore discard the first and last lines and use the middle one.</p>
<p>So what shall we do with this line? We could add it to the modelspace, but we really want it to replace the old one. We can use DbObject.HandOverTo() to replace the original line with this new one, retaining the original line’s XData and Extension Dictionary. To make use of this function, we need to open the original line using an open/close transaction (as the “normal” type of transaction will cause the function to return eInvalidContext), and then call Dispose() on the original object, once done.</p>
<p>Here’s the C# code that now contains commands to extend and shorten lines, with a shared function to handle the object selection:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> GeometryExtension</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;EXL&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ExtendLines()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Call a function to return a selection set of lines</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SelectionSet</span><span style="LINE-HEIGHT: 140%"> ss =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; SelectLines(ed, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nSelect lines to extend: &quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (ss != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start our transaction</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Edit each of the selected lines</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SelectedObject</span><span style="LINE-HEIGHT: 140%"> so </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> ss)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We&#39;re assuming only lines are in the selection-set</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Could also use a more defensive approach and</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// use a dynamic cast (Line ln = xxx as Line;)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Line</span><span style="LINE-HEIGHT: 140%"> ln =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Line</span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; so.ObjectId,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We&#39;ll extend our line by a quarter of the</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// existing length in each direction</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Vector3d</span><span style="LINE-HEIGHT: 140%"> ext = ln.Delta / 4;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// First the start-point</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ln.Extend(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">, ln.StartPoint - ext);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And then the end-point</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ln.Extend(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span><span style="LINE-HEIGHT: 140%">, ln.EndPoint + ext);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Mustn&#39;t forget to commit</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;SHL&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ShortenLines()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Call a function to return a selection set of lines</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SelectionSet</span><span style="LINE-HEIGHT: 140%"> ss =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; SelectLines(ed, </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nSelect lines to shorten: &quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (ss != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Start our transaction</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (Needs to be Open/Close to avoid an eInvalidContext</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">//&#0160; from HandOverTo())</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; db.TransactionManager.StartOpenCloseTransaction();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Edit each of the selected lines</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SelectedObject</span><span style="LINE-HEIGHT: 140%"> so </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> ss)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We&#39;re assuming only curves are in the selection-set</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Could also use a more defensive approach and</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// use a dynamic cast (Curve cur = xxx as Curve;)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Curve</span><span style="LINE-HEIGHT: 140%"> cur =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Curve</span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; so.ObjectId,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">double</span><span style="LINE-HEIGHT: 140%"> sp = cur.StartParam,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ep = cur.EndParam,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; delta = (ep-sp) * 0.25;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DoubleCollection</span><span style="LINE-HEIGHT: 140%"> dc = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DoubleCollection</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; dc.Add(sp + delta);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; dc.Add(ep - delta);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DBObjectCollection</span><span style="LINE-HEIGHT: 140%"> objs = cur.GetSplitCurves(dc);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (objs.Count == 3)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Entity</span><span style="LINE-HEIGHT: 140%"> ent = (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Entity</span><span style="LINE-HEIGHT: 140%">)objs[1];</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cur.HandOverTo(ent, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tr.AddNewlyCreatedDBObject(ent, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cur.Dispose();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; objs[0].Dispose();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; objs[2].Dispose();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Mustn&#39;t forget to commit</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Helper function to select a set of lines</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SelectionSet</span><span style="LINE-HEIGHT: 140%"> SelectLines(</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> msg)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We only want to select lines...</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Use an options object to specify how the</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// selection occurs (in terms of prompts)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptSelectionOptions</span><span style="LINE-HEIGHT: 140%"> pso =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptSelectionOptions</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; pso.MessageForAdding =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">.IsNullOrEmpty(msg) ? </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nSelect lines: &quot;</span><span style="LINE-HEIGHT: 140%"> : msg);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Use a filter to specify the objects that</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// get included in the selection set</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TypedValue</span><span style="LINE-HEIGHT: 140%">[] tvs =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TypedValue</span><span style="LINE-HEIGHT: 140%">[1] {</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">TypedValue</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%">)</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DxfCode</span><span style="LINE-HEIGHT: 140%">.Start,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LINE&quot;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; )</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; };</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SelectionFilter</span><span style="LINE-HEIGHT: 140%"> sf = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">SelectionFilter</span><span style="LINE-HEIGHT: 140%">(tvs);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Perform our restricted selection</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptSelectionResult</span><span style="LINE-HEIGHT: 140%"> psr = ed.GetSelection(pso, sf);</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (psr.Status != </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">PromptStatus</span><span style="LINE-HEIGHT: 140%">.OK || psr.Value.Count &lt;= 0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%"> psr.Value;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>Let’s see our re-factored EXL command working on a set of lines.</p>
<p>Before…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348569f017970c-pi"><img alt="Lines" border="0" height="368" src="/assets/image_847796.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Lines" width="460" /></a> </p>
<p>After…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348569f03a970c-pi"><img alt="Extended lines" border="0" height="368" src="/assets/image_659375.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Extended lines" width="459" /></a> </p>
<p>Now when we shorten these same lines using the SHL command, we see them shrink back, but not quite to the same place (as 25% is relatively to the current length of the line, of course):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348569f055970c-pi"><img alt="Reshortened lines" border="0" height="368" src="/assets/image_857768.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Reshortened lines" width="461" /></a> </p>
<p>We could clearly extend this code to work with other curve types (I did test the shorten command to work with Arcs, for instance), but that’s left as an exercise for the reader. If anyone has an approach they prefer to use for addressing this requirement, be sure to post a comment.</p>
