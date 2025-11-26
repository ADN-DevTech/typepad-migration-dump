---
layout: "post"
title: "Parallel Aggregation using AccoreConsole"
date: "2015-02-23 02:17:47"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/02/parallel-aggregation-using-accoreconsole.html "
typepad_basename: "parallel-aggregation-using-accoreconsole"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In a comment to <a href="http://adndevblog.typepad.com/autocad/2012/04/getting-started-with-accoreconsole.html">this</a> blog post, a developer had enquired about the possibility of using AccoreConsole to gather information from multiple drawings without having to launch AccoreConsole for each drawing.&nbsp;AccoreConsole can only work on the drawing passed to it using the "/i" startup switch. This prevents it from working on other drawings. But to considerably speed up the processing, parallel aggregation can be used to launch multiple instances of AccoreConsole. This leverages the multi-core capabilities of the system to perform and easily aggregate the results.</p>
<p>Here are two versions of a code that gathers the entity type of all the entities from drawings. In my Oct-core system, the serial version completed its processing for 5 drawings in about 8 seconds, while the parallel version completed it in 1.9 seconds. The results may vary at your end, but this should provide an idea of the performance enhancement that can be expected.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Serial version</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> Dictionary&lt;String, String&gt; entityBreakUp </pre>
<pre style="margin:0em;">             = <span style="color:#0000ff">new</span><span style="color:#000000">  Dictionary&lt;String, String&gt;();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> DirectoryInfo di = </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">new</span><span style="color:#000000">  DirectoryInfo(<span style="color:#a31515">@&quot;D:\Temp\TestDrawings&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> FileInfo[] fiCollection = di.GetFiles(<span style="color:#a31515">&quot;*.dwg&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> DateTime startTime = DateTime.Now;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">foreach</span><span style="color:#000000">  (FileInfo fi <span style="color:#0000ff">in</span><span style="color:#000000">  fiCollection)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     String consoleOutput = String.Empty;</pre>
<pre style="margin:0em;">     String entityNames = String.Empty;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Process coreprocess = <span style="color:#0000ff">new</span><span style="color:#000000">  Process())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         coreprocess.StartInfo.UseShellExecute </pre>
<pre style="margin:0em;">                                     = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         coreprocess.StartInfo.CreateNoWindow </pre>
<pre style="margin:0em;">                                     = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         coreprocess.StartInfo.RedirectStandardOutput </pre>
<pre style="margin:0em;">                                     = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         coreprocess.StartInfo.FileName =</pre>
<pre style="margin:0em;">         <span style="color:#a31515">@&quot;C:\Program Files\Autodesk\</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#a31515">        AutoCAD 2015\accoreconsole.exe&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         coreprocess.StartInfo.Arguments =</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">string</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;/i \&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span>\&quot; /s \&quot;<span style="color:#000000">{</span>1<span style="color:#000000">}</span>\&quot; /l en-US&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;">             fi.FullName,</pre>
<pre style="margin:0em;">             <span style="color:#a31515">@&quot;C:\Temp\RunCustomNETCmd.scr&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         coreprocess.Start();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// Max wait for 5 seconds</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         coreprocess.WaitForExit(5000); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         StreamReader outputStream </pre>
<pre style="margin:0em;">                 = coreprocess.StandardOutput;</pre>
<pre style="margin:0em;">         consoleOutput = outputStream.ReadToEnd();</pre>
<pre style="margin:0em;">         String cleaned = </pre>
<pre style="margin:0em;">             consoleOutput.Replace(<span style="color:#a31515">&quot;\0&quot;</span><span style="color:#000000"> , <span style="color:#0000ff">string</span><span style="color:#000000"> .Empty);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">int</span><span style="color:#000000">  first = cleaned.IndexOf(<span style="color:#a31515">&quot;BreakupBegin&quot;</span><span style="color:#000000"> ) </pre>
<pre style="margin:0em;">                             + <span style="color:#a31515">&quot;BreakupBegin&quot;</span><span style="color:#000000"> .Length;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">int</span><span style="color:#000000">  last = cleaned.IndexOf(<span style="color:#a31515">&quot;BreakupEnd&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (first != -1 &amp;&amp; last != -1)</pre>
<pre style="margin:0em;">             entityNames = </pre>
<pre style="margin:0em;">                 cleaned.Substring(first, last - first);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         outputStream.Close();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     entityBreakUp.Add(fi.FullName, entityNames);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Console.WriteLine(<span style="color:#0000ff">string</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;*** Serial processing : <span style="color:#000000">{</span>0:0.0<span style="color:#000000">}</span> seconds ***&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         DateTime.Now.Subtract(startTime).TotalSeconds));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">foreach</span><span style="color:#000000">  (KeyValuePair&lt;String, String&gt; kvp <span style="color:#0000ff">in</span><span style="color:#000000">  entityBreakUp)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Console.WriteLine(String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> - <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 kvp.Key.ToString(), kvp.Value.ToString()));</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Parallel version</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> Dictionary&lt;String, String&gt; entityBreakUp </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  Dictionary&lt;String, String&gt;();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> DirectoryInfo di = </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">new</span><span style="color:#000000">  DirectoryInfo(<span style="color:#a31515">@&quot;D:\Temp\TestDrawings&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> FileInfo[] fiCollection = di.GetFiles(<span style="color:#a31515">&quot;*.dwg&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> entityBreakUp.Clear();</pre>
<pre style="margin:0em;"> startTime = DateTime.Now;</pre>
<pre style="margin:0em;"> entityBreakUp = GetEntityBreakUp(fiCollection);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Console.WriteLine(<span style="color:#0000ff">string</span><span style="color:#000000"> .Format(</pre>
<pre style="margin:0em;"> <span style="color:#a31515">&quot;*** Parallel processing : <span style="color:#000000">{</span>0:0.0<span style="color:#000000">}</span> seconds ***&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> DateTime.Now.Subtract(startTime).TotalSeconds));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">foreach</span><span style="color:#000000">  (KeyValuePair&lt;String, String&gt; kvp </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">in</span><span style="color:#000000">  entityBreakUp)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Console.WriteLine(String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span> - <span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">         kvp.Key.ToString(), kvp.Value.ToString()));</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Launches multiple instance of AccoreConsole</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  Dictionary&lt;String, String&gt; </pre>
<pre style="margin:0em;">             GetEntityBreakUp(FileInfo[] fiCollection)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">object</span><span style="color:#000000">  lockObject = <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#0000ff">object</span><span style="color:#000000"> ();</pre>
<pre style="margin:0em;">     Dictionary&lt;String, String&gt; entBreakup </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  Dictionary&lt;String, String&gt;();</pre>
<pre style="margin:0em;">   </pre>
<pre style="margin:0em;">     Parallel.ForEach(</pre>
<pre style="margin:0em;">         <span style="color:#008000">// The values to be aggregated </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         fiCollection,</pre>
<pre style="margin:0em;">     </pre>
<pre style="margin:0em;">         <span style="color:#008000">// The local initial partial result</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         () =&gt; <span style="color:#0000ff">new</span><span style="color:#000000">  Dictionary&lt;String, String&gt;(),</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// The loop body</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         (x, loopState, partialResult) =&gt;</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#008000">// Lauch AccoreConsole and find the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#008000">// entity breakup </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             FileInfo fi = x <span style="color:#0000ff">as</span><span style="color:#000000">  FileInfo;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             String consoleOutput = String.Empty;</pre>
<pre style="margin:0em;">             String entityBreakup = String.Empty;</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">using</span><span style="color:#000000"> (Process coreprocess = <span style="color:#0000ff">new</span><span style="color:#000000">  Process())</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 coreprocess.StartInfo.UseShellExecute </pre>
<pre style="margin:0em;">                                             = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                 coreprocess.StartInfo.CreateNoWindow </pre>
<pre style="margin:0em;">                                             = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                 coreprocess.StartInfo.RedirectStandardOutput </pre>
<pre style="margin:0em;">                                             = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">                 coreprocess.StartInfo.FileName = </pre>
<pre style="margin:0em;">                 <span style="color:#a31515">@&quot;C:\Program Files\</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#a31515">                Autodesk\AutoCAD 2015\accoreconsole.exe&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 coreprocess.StartInfo.Arguments = </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">string</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;/i \&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span>\&quot; /s \&quot;<span style="color:#000000">{</span>1<span style="color:#000000">}</span>\&quot; /l en-US&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 fi.FullName, </pre>
<pre style="margin:0em;">                 <span style="color:#a31515">@&quot;C:\Temp\RunCustomNETCmd.scr&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 coreprocess.Start();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#008000">// Max wait for 5 seconds</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 coreprocess.WaitForExit(5000); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 StreamReader outputStream </pre>
<pre style="margin:0em;">                         = coreprocess.StandardOutput;</pre>
<pre style="margin:0em;">                 consoleOutput = outputStream.ReadToEnd();</pre>
<pre style="margin:0em;">                 String cleaned = </pre>
<pre style="margin:0em;">                 consoleOutput.Replace(<span style="color:#a31515">&quot;\0&quot;</span><span style="color:#000000"> , <span style="color:#0000ff">string</span><span style="color:#000000"> .Empty);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000">  first = cleaned.IndexOf(<span style="color:#a31515">&quot;BreakupBegin&quot;</span><span style="color:#000000"> ) </pre>
<pre style="margin:0em;">                                     + <span style="color:#a31515">&quot;BreakupBegin&quot;</span><span style="color:#000000"> .Length;</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">int</span><span style="color:#000000">  last = cleaned.IndexOf(<span style="color:#a31515">&quot;BreakupEnd&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000"> (first != -1 &amp;&amp; last != -1)</pre>
<pre style="margin:0em;">                     entityBreakup = </pre>
<pre style="margin:0em;">                        cleaned.Substring(first, last - first);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 outputStream.Close();</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             Dictionary&lt;String, String&gt; partialDict </pre>
<pre style="margin:0em;">                 = partialResult <span style="color:#0000ff">as</span><span style="color:#000000">  Dictionary&lt;String, String&gt;;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             partialDict.Add(x.FullName, entityBreakup);</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000">  partialDict;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span>,</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// The final step of each local context            </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         (partialEntBreakup) =&gt;</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#008000">// Enforce serial access to single, shared result</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">lock</span><span style="color:#000000">  (lockObject)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 Dictionary&lt;String, String&gt; partialDict </pre>
<pre style="margin:0em;">                 = partialEntBreakup <span style="color:#0000ff">as</span><span style="color:#000000">  Dictionary&lt;String, String&gt;;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">foreach</span><span style="color:#000000">  (KeyValuePair&lt;String, String&gt; kvp </pre>
<pre style="margin:0em;">                                             <span style="color:#0000ff">in</span><span style="color:#000000">  partialDict)</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     entBreakup.Add(kvp.Key, kvp.Value);</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span>);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  entBreakup;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is the code from the custom .Net plugin for "EntBreakup" command which lists the entities in a drawing.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;MyCommands&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 <span style="color:#a31515">&quot;EntBreakup&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                 CommandFlags.Modal)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  EntBreakupMethod()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     DocumentCollection docs = Autodesk.AutoCAD</pre>
<pre style="margin:0em;">         .ApplicationServices.Core.Application.DocumentManager;</pre>
<pre style="margin:0em;">     Document activeDoc = docs.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Editor ed = activeDoc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Obtain the selection set of all the entities </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// in the drawing.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     PromptSelectionResult psr1 = ed.SelectAll();</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (psr1.Status == PromptStatus.OK)</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         PrintSelectionSet(<span style="color:#a31515">&quot;SelectAll&quot;</span><span style="color:#000000"> , psr1.Value);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  PrintSelectionSet(<span style="color:#0000ff">string</span><span style="color:#000000">  Title, SelectionSet ss)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     DocumentCollection docs = Autodesk.AutoCAD</pre>
<pre style="margin:0em;">         .ApplicationServices.Core.Application.DocumentManager;</pre>
<pre style="margin:0em;">     Document activeDoc = docs.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Editor ed = activeDoc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ed.WriteMessage(<span style="color:#0000ff">string</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span>BreakupBegin&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                                 Environment.NewLine));</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">foreach</span><span style="color:#000000">  (ObjectId oid <span style="color:#0000ff">in</span><span style="color:#000000">  ss.GetObjectIds())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ed.WriteMessage(<span style="color:#0000ff">string</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span><span style="color:#000000">{</span>1<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">             Environment.NewLine, oid.ObjectClass.Name));</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     ed.WriteMessage(<span style="color:#0000ff">string</span><span style="color:#000000"> .Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span>BreakupEnd&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                                     Environment.NewLine));</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>The sample project can be donwnloaded here :</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7a11d76970b img-responsive"><a href="http://adndevblog.typepad.com/files/sampleproject-2.zip">Download SampleProject</a></span>
