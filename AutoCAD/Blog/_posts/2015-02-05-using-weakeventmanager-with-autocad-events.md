---
layout: "post"
title: "Using WeakEventManager with AutoCAD events"
date: "2015-02-05 23:43:24"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/02/using-weakeventmanager-with-autocad-events.html "
typepad_basename: "using-weakeventmanager-with-autocad-events"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Recently we had a query from a developer on using WeakEventManager when subscribing to AutoCAD events. In this blog post we will look at three different versions all of which subscribe to the CommandEnded event of the Document class. The benefit of using the WeakEventManager is to prevent memory leaks when the events are left subscribed but no longer needed. The best practice is to unsubscribe the events when its no longer needed. Also, the use of WeakEventManager comes at a cost because the events are now delivered through the event manager. The additional cost of using the Weak event pattern is nicely explained in <a href="http://reedcopsey.com/2009/08/06/preventing-event-based-memory-leaks-weakeventmanager/">this</a> blog post by Reed Copsey.</p>
<p>It is recommended to unsubscribe the AutoCAD events manually, but the following code snippets should demonstrate the use of WeakEventManager in case you need it.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  <span style="color:#2b91af">Commands</span><span style="color:#000000">  :</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">IExtensionApplication</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             System.Windows.<span style="color:#2b91af">IWeakEventListener</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">void</span><span style="color:#000000">  <span style="color:#2b91af">IExtensionApplication</span><span style="color:#000000"> .Initialize() <span style="color:#000000">{</span> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">void</span><span style="color:#000000">  <span style="color:#2b91af">IExtensionApplication</span><span style="color:#000000"> .Terminate() <span style="color:#000000">{</span> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">    #region</span><span style="color:#000000">  Version 1 (Without the use of WeakEventManager)</pre>
<pre style="margin:0em;">     <span style="color:#008000">// Version : 1</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">bool</span><span style="color:#000000">  bAdded1 = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">CommandMethod</span><span style="color:#000000"> (<span style="color:#a31515">&quot;StartMonitor1&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StartMonitorMethod()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  doc</pre>
<pre style="margin:0em;">         = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bAdded1 == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             doc.CommandEnded</pre>
<pre style="margin:0em;">             += <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">CommandEventHandler</span><span style="color:#000000"> (doc_CommandEnded);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             bAdded1 = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;">             <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Subscribed to CommandEnded event&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">CommandMethod</span><span style="color:#000000"> (<span style="color:#a31515">&quot;EndMonitor1&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  EndMonitorMethod()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  doc</pre>
<pre style="margin:0em;">         = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bAdded1 == <span style="color:#0000ff">true</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             doc.CommandEnded</pre>
<pre style="margin:0em;">             -= <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">CommandEventHandler</span><span style="color:#000000"> (doc_CommandEnded);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             bAdded1 = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;">             <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Unsubscribed CommandEnded event&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">void</span><span style="color:#000000">  doc_CommandEnded(<span style="color:#0000ff">object</span><span style="color:#000000">  sender, <span style="color:#2b91af">CommandEventArgs</span><span style="color:#000000">  e)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  doc</pre>
<pre style="margin:0em;">         = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Editor</span><span style="color:#000000">  ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bAdded3)</pre>
<pre style="margin:0em;">             ed.WriteMessage(</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> (Weak event) Command Ended : <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine, e.GlobalCommandName));</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             ed.WriteMessage(</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Command Ended : <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine, e.GlobalCommandName));</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">    #endregion</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//(Using WeakEventManager (non-generic version) </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">    #region</span><span style="color:#000000">  Version 2</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">bool</span><span style="color:#000000">  bAdded2 = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">CommandMethod</span><span style="color:#000000"> (<span style="color:#a31515">&quot;StartMonitor2&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StartMonitor2Method()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  doc</pre>
<pre style="margin:0em;">         = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bAdded2 == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> .AddListener(doc, <span style="color:#0000ff">this</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">             bAdded2 = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;">             <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Subscribed to CommandEnded weak event&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">CommandMethod</span><span style="color:#000000"> (<span style="color:#a31515">&quot;EndMonitor2&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  EndMonitor2Method()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  doc</pre>
<pre style="margin:0em;">         = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bAdded2 == <span style="color:#0000ff">true</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> .RemoveListener</pre>
<pre style="margin:0em;">                                         (doc, <span style="color:#0000ff">this</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">             bAdded2 = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;">             <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Unsubscribed CommandEnded weak event&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">bool</span><span style="color:#000000">  System.Windows.<span style="color:#2b91af">IWeakEventListener</span><span style="color:#000000"> .ReceiveWeakEvent</pre>
<pre style="margin:0em;">             (<span style="color:#2b91af">Type</span><span style="color:#000000">  managerType, <span style="color:#0000ff">object</span><span style="color:#000000">  sender, <span style="color:#2b91af">EventArgs</span><span style="color:#000000">  e)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (managerType.Equals(</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">typeof</span><span style="color:#000000"> (<span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> )))</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Document</span><span style="color:#000000">  doc = sender <span style="color:#0000ff">as</span><span style="color:#000000">  <span style="color:#2b91af">Document</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">CommandEventArgs</span><span style="color:#000000">  ce = e <span style="color:#0000ff">as</span><span style="color:#000000">  <span style="color:#2b91af">CommandEventArgs</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Editor</span><span style="color:#000000">  ed = doc.Editor;</pre>
<pre style="margin:0em;">             ed.WriteMessage(</pre>
<pre style="margin:0em;">                 <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> (Weak event) Command Ended : <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">                 <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine, ce.GlobalCommandName));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">    #endregion</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//(Using generic WeakEventManager from .Net framework 4.5)</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">    #region</span><span style="color:#000000">  Version 3 </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">bool</span><span style="color:#000000">  bAdded3 = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">CommandMethod</span><span style="color:#000000"> (<span style="color:#a31515">&quot;StartMonitor3&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StartMonitor3Method()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  doc </pre>
<pre style="margin:0em;">             = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bAdded3 == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             System.Windows.WeakEventManager</pre>
<pre style="margin:0em;">                 &lt;<span style="color:#2b91af">Document</span><span style="color:#000000"> , <span style="color:#2b91af">CommandEventArgs</span><span style="color:#000000"> &gt;</pre>
<pre style="margin:0em;">                 .AddHandler(</pre>
<pre style="margin:0em;">                 doc, </pre>
<pre style="margin:0em;">                 <span style="color:#a31515">&quot;CommandEnded&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 doc_CommandEnded);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             bAdded3 = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">                 <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;">                 <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Subscribed to CommandEnded weak event&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     [<span style="color:#2b91af">CommandMethod</span><span style="color:#000000"> (<span style="color:#a31515">&quot;EndMonitor3&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  EndMonitor3Method()</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  doc </pre>
<pre style="margin:0em;">             = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (bAdded3 == <span style="color:#0000ff">true</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             System.Windows.WeakEventManager</pre>
<pre style="margin:0em;">                 &lt;<span style="color:#2b91af">Document</span><span style="color:#000000"> , <span style="color:#2b91af">CommandEventArgs</span><span style="color:#000000"> &gt;</pre>
<pre style="margin:0em;">                 .RemoveHandler(</pre>
<pre style="margin:0em;">                 doc, </pre>
<pre style="margin:0em;">                 <span style="color:#a31515">&quot;CommandEnded&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 doc_CommandEnded);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             bAdded3 = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             doc.Editor.WriteMessage(</pre>
<pre style="margin:0em;">                 <span style="color:#2b91af">String</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;">                 <span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> Unsubscribed CommandEnded weak event&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 <span style="color:#2b91af">Environment</span><span style="color:#000000"> .NewLine));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">    #endregion</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// For Version 2 </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Using Non-generic version of WeakEventManager</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">class</span><span style="color:#000000">  <span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             : System.Windows.<span style="color:#2b91af">WeakEventManager</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000">  CurrentManager</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">get</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000">  manager</pre>
<pre style="margin:0em;">             = System.Windows.<span style="color:#2b91af">WeakEventManager</span><span style="color:#000000"> .GetCurrentManager(</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">typeof</span><span style="color:#000000"> (<span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> ))</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">as</span><span style="color:#000000">  <span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (manager == <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 manager = <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> ();</pre>
<pre style="margin:0em;">                 System.Windows.<span style="color:#2b91af">WeakEventManager</span><span style="color:#000000"> .SetCurrentManager</pre>
<pre style="margin:0em;">                     (<span style="color:#0000ff">typeof</span><span style="color:#000000"> (<span style="color:#2b91af">CommandEndedEventManager</span><span style="color:#000000"> ), manager);</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000">  manager;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  AddListener(</pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  source,</pre>
<pre style="margin:0em;">         System.Windows.<span style="color:#2b91af">IWeakEventListener</span><span style="color:#000000">  listener)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         CurrentManager.ProtectedAddListener</pre>
<pre style="margin:0em;">                             (source, listener);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  RemoveListener(</pre>
<pre style="margin:0em;">         <span style="color:#2b91af">Document</span><span style="color:#000000">  source,</pre>
<pre style="margin:0em;">         System.Windows.<span style="color:#2b91af">IWeakEventListener</span><span style="color:#000000">  listener)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         CurrentManager.ProtectedRemoveListener</pre>
<pre style="margin:0em;">                             (source, listener);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">protected</span><span style="color:#000000">  <span style="color:#0000ff">override</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StartListening(<span style="color:#0000ff">object</span><span style="color:#000000">  source)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">try</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             ((<span style="color:#2b91af">Document</span><span style="color:#000000"> )source).CommandEnded</pre>
<pre style="margin:0em;">                         += deliver_CommandEnded;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">catch</span><span style="color:#000000">  (System.<span style="color:#2b91af">Exception</span><span style="color:#000000">  ex)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Document</span><span style="color:#000000">  doc</pre>
<pre style="margin:0em;">               = <span style="color:#2b91af">Application</span><span style="color:#000000"> .DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">             <span style="color:#2b91af">Editor</span><span style="color:#000000">  ed = doc.Editor;</pre>
<pre style="margin:0em;">             ed.WriteMessage(ex.Message);</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">void</span><span style="color:#000000">  deliver_CommandEnded(</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">object</span><span style="color:#000000">  sender, <span style="color:#2b91af">CommandEventArgs</span><span style="color:#000000">  e)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">this</span><span style="color:#000000"> .DeliverEvent(sender, e);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">protected</span><span style="color:#000000">  <span style="color:#0000ff">override</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  StopListening(<span style="color:#0000ff">object</span><span style="color:#000000">  source)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">try</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             ((<span style="color:#2b91af">Document</span><span style="color:#000000"> )source).CommandEnded</pre>
<pre style="margin:0em;">                 -= deliver_CommandEnded;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">catch</span><span style="color:#000000">  (System.<span style="color:#2b91af">Exception</span><span style="color:#000000">  ex)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             System.Windows.<span style="color:#2b91af">MessageBox</span><span style="color:#000000"> .Show(ex.Message);</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
