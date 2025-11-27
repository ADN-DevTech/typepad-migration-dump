---
layout: "post"
title: "A handy .NET class to help manage long operations in AutoCAD"
date: "2007-08-06 16:31:43"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Fractals"
  - "User interface"
original_url: "https://www.keanw.com/2007/08/a-handy-net-cla.html "
typepad_basename: "a-handy-net-cla"
typepad_status: "Publish"
---

<p>This post was almost called &quot;<em>Generating Koch fractals in AutoCAD using .NET - Part 3</em>&quot;, following on from Parts <a href="http://through-the-interface.typepad.com/through_the_interface/2007/07/generating-koch.html">1</a> &amp; <a href="http://through-the-interface.typepad.com/through_the_interface/2007/08/generating-koch.html">2</a> of the series. But by the time I'd completed the code, I realised it to be of more general appeal and decided to provide it with a more representative title.</p>

<p>I started off by adding <a href="http://through-the-interface.typepad.com/through_the_interface/2007/05/displaying_a_pr.html">a progress meter</a> and <a href="http://through-the-interface.typepad.com/through_the_interface/2007/02/allowing_users_.html">an escape key handler</a> to the code in the last post. Then, while refactoring the code, I decided to encapsulate the functionality in a standalone class that could be dropped into pretty much any AutoCAD .NET project (although I've implemented it in C#, as usual).</p>

<p>So what we have is a new class called LongOperationManager, which does the following:</p>

<ul><li>Displays and updates a progress meter (at the bottom left of AutoCAD's window)<ul><li>Allowing you to set an arbitrary message and total number of operations</li></ul></li>

<li>Listens for &quot;escape&quot; in case the user wants to interrupt the current operation</li></ul>

<p>Here's the class implementation:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">LongOperationManager</span> :</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">IDisposable</span>, System.Windows.Forms.<span style="COLOR: teal">IMessageFilter</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// The message code corresponding to a keypress</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">int</span> WM_KEYDOWN = 0x0100;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// The number of times to update the progress meter</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// (for some reason you need 600 to tick through</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">//&nbsp; for each percent)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">int</span> progressMeterIncrements = 600;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Internal members for metering progress</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: teal">ProgressMeter</span> pm;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">long</span> updateIncrement;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">long</span> currentInc;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// External flag for checking cancelled status</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> cancelled = <span style="COLOR: blue">false</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Constructor</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> LongOperationManager(<span style="COLOR: blue">string</span> message)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; System.Windows.Forms.<span style="COLOR: teal">Application</span>.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;AddMessageFilter(<span style="COLOR: blue">this</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; pm = <span style="COLOR: blue">new</span> <span style="COLOR: teal">ProgressMeter</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; pm.Start(message);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; pm.SetLimit(progressMeterIncrements);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; currentInc = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// System.IDisposable.Dispose</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; pm.Stop();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; pm.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; System.Windows.Forms.<span style="COLOR: teal">Application</span>.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;RemoveMessageFilter(<span style="COLOR: blue">this</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Set the total number of operations</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SetTotalOperations(<span style="COLOR: blue">long</span> totalOps)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// We really just care about when we need</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// to update the timer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; updateIncrement =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(totalOps &gt; progressMeterIncrements ?</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; totalOps / progressMeterIncrements :</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; totalOps</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// This function is called whenever an operation</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// is performed</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> Tick()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> (++currentInc == updateIncrement)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pm.MeterProgress();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;currentInc = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.Windows.Forms.<span style="COLOR: teal">Application</span>.DoEvents();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Check whether the filter has set the flag</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> (cancelled)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pm.Stop();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> !cancelled;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// The message filter callback</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> PreFilterMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">ref</span> System.Windows.Forms.<span style="COLOR: teal">Message</span> m</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> (m.Msg == WM_KEYDOWN)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Check for the Escape keypress</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.Windows.Forms.<span style="COLOR: teal">Keys</span> kc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (System.Windows.Forms.<span style="COLOR: teal">Keys</span>)(<span style="COLOR: blue">int</span>)m.WParam &amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; System.Windows.Forms.<span style="COLOR: teal">Keys</span>.KeyCode;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m.Msg == WM_KEYDOWN &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; kc == System.Windows.Forms.<span style="COLOR: teal">Keys</span>.Escape)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cancelled = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Return true to filter all keypresses</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Return false to let other messages through</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>In terms of how to use the class... first of all you create an instance of it, setting the string to be shown on the progress meter (just like AutoCAD's ProgressMeter class). As the LongOperationManager implements IDisposable, then at the end you should either call Dispose or manage it's scope with the using() statement.</p>

<p>I chose to separate the setting of the total number of operations to be completed from the object's construction, as in our example we need to an initial pass before we know how many objects we're working with (and we want to at least put the label on the progress meter while we perform that initial pass).</p>

<p>Then we just call the Tick() method whenever we perform an operation - this updates the progress meter and checks for use of the escape key. The idea is that you set the total number of operations and then call Tick() for each one of those individual operations - the class takes care of how often it needs to update the progress meter. If it finds escape has been used, the Tick() method will return false.</p>

<p>That's about it, aside from the fact you can also query the &quot;cancelled&quot; property to see whether escape has been used.</p>

<p>Here's the basic approach:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">LongOperationManager lom =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> LongOperationManager(<span style="COLOR: maroon">&quot;Fractalizing entities&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> (lom)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ...</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; lom.SetTotalOperations(totalOps);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ...</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">while</span> (<span style="color: #0000ff;">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ...</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> (!lom.Tick())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nFractalization cancelled.\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Here's the code integrated into the previous example, with the significant lines in <span style="COLOR: red">red</span>:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 1</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 2</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 3</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 4</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 5</span> <span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 6</span> <span style="COLOR: blue">using</span> System.Collections.Generic;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 7</span> <span style="COLOR: blue">using</span> System;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 8</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 9</span> <span style="COLOR: blue">namespace</span> Kochizer</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;10</span> {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;11</span>&nbsp; &nbsp;<span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;12</span>&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;13</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// We generate 4 new entities for every old entity</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;14</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (unless a complex entity such as a polyline)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;15</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;16</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">int</span> newEntsPerOldEnt = 4;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;17</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;18</span>&nbsp; &nbsp;&nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;KA&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;19</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> KochizeAll()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;20</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;21</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;22</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;23</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;24</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;25</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;26</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Acquire user input - whether to create the</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;27</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// new geometry to the left or the right...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;28</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;29</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">PromptKeywordOptions</span> pko =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;30</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptKeywordOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;31</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;\nCreate fractal to side (Left/&lt;Right&gt;): &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;32</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;33</span>&nbsp; &nbsp;&nbsp; &nbsp; pko.Keywords.Add(<span style="COLOR: maroon">&quot;Left&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;34</span>&nbsp; &nbsp;&nbsp; &nbsp; pko.Keywords.Add(<span style="COLOR: maroon">&quot;Right&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;35</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;36</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">PromptResult</span> pr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;37</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.GetKeywords(pko);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;38</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">bool</span> bLeft = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;39</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;40</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pr.Status != <span style="COLOR: teal">PromptStatus</span>.None &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;41</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pr.Status != <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;42</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;43</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;44</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> ((<span style="COLOR: blue">string</span>)pr.StringResult == <span style="COLOR: maroon">&quot;Left&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;45</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bLeft = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;46</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;47</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// ... and the recursion depth for the command.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;48</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;49</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">PromptIntegerOptions</span> pio =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;50</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptIntegerOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;51</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;\nEnter recursion level &lt;1&gt;: &quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;52</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;53</span>&nbsp; &nbsp;&nbsp; &nbsp; pio.AllowZero = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;54</span>&nbsp; &nbsp;&nbsp; &nbsp; pio.AllowNegative = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;55</span>&nbsp; &nbsp;&nbsp; &nbsp; pio.AllowNone = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;56</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;57</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">PromptIntegerResult</span> pir =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;58</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.GetInteger(pio);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;59</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">int</span> recursionLevel = 1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;60</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;61</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pir.Status != <span style="COLOR: teal">PromptStatus</span>.None &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;62</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pir.Status != <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;63</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;64</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;65</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pir.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;66</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;recursionLevel = pir.Value;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;67</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;68</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create and add our long operation handler</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;69</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">LongOperationManager</span> lom =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;70</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="COLOR: teal">LongOperationManager</span>(<span style="COLOR: maroon">&quot;Fractalizing entities&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;71</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;72</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (lom)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; &nbsp;73</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;74</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Note: strictly speaking we're not recursing,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;75</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// we're iterating, but the effect to the user</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;76</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// is the same.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;77</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;78</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;79</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; doc.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;80</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;81</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;82</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;83</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="COLOR: teal">BlockTable</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;84</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;85</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;86</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;87</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">using</span> (bt)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;88</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;89</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// No need to open the block table record</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;90</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// for write, as we're just reading data</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;91</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// for now</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;92</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;93</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;94</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">BlockTableRecord</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;95</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bt[<span style="COLOR: teal">BlockTableRecord</span>.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;96</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;97</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;98</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">using</span> (btr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;99</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 100</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// List of changed entities</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 101</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (will contain complex entities, such as</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 102</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// polylines&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 103</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 104</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">ObjectIdCollection</span> modified =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 105</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">ObjectIdCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 106</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 107</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// List of entities to erase</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 108</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (will contain replaced entities)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 109</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 110</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">ObjectIdCollection</span> toErase =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 111</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">ObjectIdCollection</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 112</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 113</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// List of new entitites to add</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 114</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// (will be processed recursively or</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 115</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// assed to the open block table record)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 116</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 117</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt; newEntities =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 118</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt;(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 119</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db.ApproxNumObjects * newEntsPerOldEnt</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 120</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 121</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 122</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Kochize each entity in the open block</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 123</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// table record</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 124</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 125</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> objId <span style="COLOR: blue">in</span> btr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 126</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 127</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Entity</span> ent =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 128</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="COLOR: teal">Entity</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 129</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;objId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 130</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 131</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 132</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Kochize(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 133</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 134</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; modified,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 135</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; toErase,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 136</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; newEntities,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 137</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; bLeft</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 138</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 139</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 140</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 141</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// The number of operations is...</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 142</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; The number of complex entities multiplied</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 143</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; by the recursion level (they each get</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 144</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; &quot;kochized&quot; once per level,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 145</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; even if that's a long operation)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 146</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// plus</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 147</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; (4^0 + 4^1 + 4^2 + 4^3... + 4^n) multiplied</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 148</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">//&nbsp; by the number of db-resident ents</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 149</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// where n is the recursion level. Phew!</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 150</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 151</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">long</span> totalOps =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 152</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;modified.Count * recursionLevel +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 153</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;operationCount(recursionLevel) *</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 154</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;toErase.Count;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 155</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;lom.SetTotalOperations(totalOps);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 156</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 157</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// If we need to loop,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 158</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// work on the returned entities</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 159</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 160</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">while</span> (--recursionLevel &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 161</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 162</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create an output array</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 163</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 164</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt; newerEntities =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 165</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt;(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 166</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;newEntities.Count * newEntsPerOldEnt</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 167</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 168</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 169</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Kochize all the modified (complex) ents</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 170</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 171</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> objId <span style="COLOR: blue">in</span> modified)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 172</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 173</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (!lom.Tick())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 174</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 175</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 176</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;\nFractalization cancelled.\n&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 177</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 178</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 179</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 180</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 181</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Entity</span> ent =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 182</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">Entity</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 183</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; objId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 184</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 185</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 186</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Kochize(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 187</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ent,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 188</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;modified,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 189</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;toErase,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 190</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;newerEntities,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 191</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bLeft</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 192</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 193</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 194</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 195</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Kochize all the non-db resident entities</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 196</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (!lom.cancelled)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 197</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 198</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">Entity</span> ent <span style="COLOR: blue">in</span> newEntities)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 199</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 200</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (!lom.Tick())</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 201</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 202</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 203</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nFractalization cancelled.\n&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 204</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 205</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">break</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 206</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 207</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;Kochize(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 208</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ent,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 209</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; modified,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 210</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; toErase,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 211</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; newerEntities,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 212</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; bLeft</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 213</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 214</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 215</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 216</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 217</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// We now longer need the intermediate ents</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 218</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// previously output for the level above,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 219</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// we replace them with the latest output</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 220</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 221</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; newEntities.Clear();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 222</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; newEntities = newerEntities;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 223</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 224</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 225</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;lom.Tick();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 226</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 227</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (!lom.cancelled)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 228</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 229</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Erase each replaced db-resident ent</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 230</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 231</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> objId <span style="COLOR: blue">in</span> toErase)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 232</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 233</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Entity</span> ent =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 234</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">Entity</span>)tr.GetObject(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 235</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; objId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 236</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForWrite</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 237</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 238</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Erase();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 239</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 240</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 241</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Add the new entities</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 242</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 243</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; btr.UpgradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 244</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">Entity</span> ent <span style="COLOR: blue">in</span> newEntities)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 245</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 246</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; btr.AppendEntity(ent);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 247</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(ent, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 248</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 249</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 250</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 251</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 252</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 253</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 254</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 255</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 256</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 257</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">long</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 258</span>&nbsp; &nbsp;&nbsp; operationCount(<span style="COLOR: blue">int</span> nRecurse)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 259</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 260</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (1 &gt;= nRecurse)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 261</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> 1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 262</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 263</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">long</span>)<span style="COLOR: teal">Math</span>.Pow(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 264</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; newEntsPerOldEnt,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 265</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; nRecurse - 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 266</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 267</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;+ operationCount(nRecurse - 1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 268</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 269</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 270</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Dispatch function to call through to various per-type</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 271</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// functions</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 272</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 273</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> Kochize(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 274</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Entity</span> ent,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 275</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> modified,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 276</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> toErase,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 277</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt; toAdd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 278</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">bool</span> bLeft</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 279</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 280</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 281</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Line</span> ln = ent <span style="COLOR: blue">as</span> <span style="COLOR: teal">Line</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 282</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (ln != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 283</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 284</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;Kochize(ln, modified, toErase, toAdd, bLeft);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 285</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 286</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 287</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Arc</span> arc = ent <span style="COLOR: blue">as</span> <span style="COLOR: teal">Arc</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 288</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (arc != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 289</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 290</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;Kochize(arc, modified, toErase, toAdd, bLeft);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 291</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 292</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 293</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Polyline</span> pl = ent <span style="COLOR: blue">as</span> <span style="COLOR: teal">Polyline</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 294</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pl != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 295</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 296</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;Kochize(pl, modified, toErase, toAdd, bLeft);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 297</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 298</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 299</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 300</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 301</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create 4 new lines from a line passed in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 302</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 303</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> Kochize(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 304</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Line</span> ln,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 305</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> modified,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 306</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> toErase,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 307</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt; toAdd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 308</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">bool</span> bLeft</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 309</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 310</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 311</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Get general info about the line</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 312</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// and calculate the main 5 points</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 313</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 314</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> pt1 = ln.StartPoint,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 315</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pt5 = ln.EndPoint;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 316</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Vector3d</span> vec1 = pt5 - pt1,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 317</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;norm1 = vec1.GetNormal();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 318</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">double</span> d_3 = vec1.Length / 3;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 319</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> pt2 = pt1 + (norm1 * d_3),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 320</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pt4 = pt1 + (2 * norm1 * d_3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 321</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Vector3d</span> vec2 = pt4 - pt2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 322</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 323</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (bLeft)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 324</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;vec2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 325</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vec2.RotateBy(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 326</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Math</span>.PI / 3, <span style="COLOR: blue">new</span> <span style="COLOR: teal">Vector3d</span>(0, 0, 1)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 327</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 328</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 329</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;vec2 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 330</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vec2.RotateBy(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 331</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; 5 * <span style="COLOR: teal">Math</span>.PI / 3, <span style="COLOR: blue">new</span> <span style="COLOR: teal">Vector3d</span>(0, 0, 1)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 332</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 333</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> pt3 = pt2 + vec2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 334</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 335</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Mark the original to be erased</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 336</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 337</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (ln.ObjectId != <span style="COLOR: teal">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 338</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;toErase.Add(ln.ObjectId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 339</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 340</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the first line</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 341</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 342</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Line</span> ln1 = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Line</span>(pt1, pt2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 343</span>&nbsp; &nbsp;&nbsp; &nbsp; ln1.SetPropertiesFrom(ln);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 344</span>&nbsp; &nbsp;&nbsp; &nbsp; ln1.Thickness = ln.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 345</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(ln1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 346</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 347</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the second line</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 348</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 349</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Line</span> ln2 = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Line</span>(pt2, pt3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 350</span>&nbsp; &nbsp;&nbsp; &nbsp; ln2.SetPropertiesFrom(ln);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 351</span>&nbsp; &nbsp;&nbsp; &nbsp; ln2.Thickness = ln.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 352</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(ln2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 353</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 354</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the third line</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 355</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 356</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Line</span> ln3 = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Line</span>(pt3, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 357</span>&nbsp; &nbsp;&nbsp; &nbsp; ln3.SetPropertiesFrom(ln);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 358</span>&nbsp; &nbsp;&nbsp; &nbsp; ln3.Thickness = ln.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 359</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(ln3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 360</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 361</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the fourth line</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 362</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 363</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Line</span> ln4 = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Line</span>(pt4, pt5);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 364</span>&nbsp; &nbsp;&nbsp; &nbsp; ln4.SetPropertiesFrom(ln);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 365</span>&nbsp; &nbsp;&nbsp; &nbsp; ln4.Thickness = ln.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 366</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(ln4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 367</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 368</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 369</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create 4 new arcs from an arc passed in</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 370</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 371</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> Kochize(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 372</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Arc</span> arc,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 373</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> modified,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 374</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> toErase,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 375</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt; toAdd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 376</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">bool</span> bLeft</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 377</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 378</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 379</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Get general info about the arc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 380</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// and calculate the main 5 points</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 381</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 382</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> pt1 = arc.StartPoint,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 383</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pt5 = arc.EndPoint;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 384</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">double</span> length = arc.GetDistAtPoint(pt5),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 385</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; angle = arc.StartAngle;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 386</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Vector3d</span> full = pt5 - pt1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 387</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 388</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> pt2 = arc.GetPointAtDist(length / 3),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 389</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pt4 = arc.GetPointAtDist(2 * length / 3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 390</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 391</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Mark the original to be erased</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 392</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 393</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (arc.ObjectId != <span style="COLOR: teal">ObjectId</span>.Null)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 394</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;toErase.Add(arc.ObjectId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 395</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 396</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the first arc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 397</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 398</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> mid = arc.GetPointAtDist(length / 6);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 399</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">CircularArc3d</span> tmpArc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 400</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> <span style="COLOR: teal">CircularArc3d</span>(pt1, mid, pt2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 401</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Arc</span> arc1 = circArc2Arc(tmpArc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 402</span>&nbsp; &nbsp;&nbsp; &nbsp; arc1.SetPropertiesFrom(arc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 403</span>&nbsp; &nbsp;&nbsp; &nbsp; arc1.Thickness = arc.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 404</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(arc1);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 405</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 406</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the second arc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 407</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 408</span>&nbsp; &nbsp;&nbsp; &nbsp; mid = arc.GetPointAtDist(length / 2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 409</span>&nbsp; &nbsp;&nbsp; &nbsp; tmpArc.Set(pt2, mid, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 410</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (bLeft)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 411</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tmpArc.RotateBy(<span style="COLOR: teal">Math</span>.PI / 3, tmpArc.Normal, pt2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 412</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 413</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tmpArc.RotateBy(5 * <span style="COLOR: teal">Math</span>.PI / 3, tmpArc.Normal, pt2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 414</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Arc</span> arc2 = circArc2Arc(tmpArc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 415</span>&nbsp; &nbsp;&nbsp; &nbsp; arc2.SetPropertiesFrom(arc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 416</span>&nbsp; &nbsp;&nbsp; &nbsp; arc2.Thickness = arc.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 417</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(arc2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 418</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 419</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the third arc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 420</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 421</span>&nbsp; &nbsp;&nbsp; &nbsp; tmpArc.Set(pt2, mid, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 422</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (bLeft)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 423</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tmpArc.RotateBy(5 * <span style="COLOR: teal">Math</span>.PI / 3, tmpArc.Normal, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 424</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 425</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tmpArc.RotateBy(<span style="COLOR: teal">Math</span>.PI / 3, tmpArc.Normal, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 426</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Arc</span> arc3 = circArc2Arc(tmpArc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 427</span>&nbsp; &nbsp;&nbsp; &nbsp; arc3.SetPropertiesFrom(arc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 428</span>&nbsp; &nbsp;&nbsp; &nbsp; arc3.Thickness = arc.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 429</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(arc3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 430</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 431</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create the fourth arc</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 432</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 433</span>&nbsp; &nbsp;&nbsp; &nbsp; mid = arc.GetPointAtDist(5 * length / 6);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 434</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Arc</span> arc4 =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 435</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circArc2Arc(<span style="COLOR: blue">new</span> <span style="COLOR: teal">CircularArc3d</span>(pt4, mid, pt5));</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 436</span>&nbsp; &nbsp;&nbsp; &nbsp; arc4.SetPropertiesFrom(arc);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 437</span>&nbsp; &nbsp;&nbsp; &nbsp; arc4.Thickness = arc.Thickness;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 438</span>&nbsp; &nbsp;&nbsp; &nbsp; toAdd.Add(arc4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 439</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 440</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 441</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Arc</span> circArc2Arc(<span style="COLOR: teal">CircularArc3d</span> circArc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 442</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 443</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Point3d</span> center = circArc.Center;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 444</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Vector3d</span> normal = circArc.Normal;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 445</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Vector3d</span> refVec = circArc.ReferenceVector;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 446</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Plane</span> plane = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Plane</span>(center, normal);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 447</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">double</span> ang = refVec.AngleOnPlane(plane);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 448</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">new</span> <span style="COLOR: teal">Arc</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 449</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;center,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 450</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;normal,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 451</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circArc.Radius,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 452</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circArc.StartAngle + ang,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 453</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;circArc.EndAngle + ang</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 454</span>&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 455</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 456</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 457</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">void</span> Kochize(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 458</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Polyline</span> pl,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 459</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> modified,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 460</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">ObjectIdCollection</span> toErase,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 461</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">List</span>&lt;<span style="COLOR: teal">Entity</span>&gt; toAdd,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 462</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">bool</span> bLeft</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 463</span>&nbsp; &nbsp;&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 464</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 465</span>&nbsp; &nbsp;&nbsp; &nbsp; pl.UpgradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 466</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 467</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (pl.ObjectId != <span style="COLOR: teal">ObjectId</span>.Null &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 468</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; !modified.Contains(pl.ObjectId))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 469</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 470</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;modified.Add(pl.ObjectId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 471</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 472</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 473</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">for</span>(<span style="COLOR: blue">int</span> vn = 0; vn &lt; pl.NumberOfVertices; vn++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 474</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 475</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">SegmentType</span> st = pl.GetSegmentType(vn);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 476</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (st != <span style="COLOR: teal">SegmentType</span>.Line &amp;&amp; st != <span style="COLOR: teal">SegmentType</span>.Arc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 477</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">continue</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 478</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 479</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">double</span> sw = pl.GetStartWidthAt(vn),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 480</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ew = pl.GetEndWidthAt(vn);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 481</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 482</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (st == <span style="COLOR: teal">SegmentType</span>.Line)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 483</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 484</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (vn + 1 == pl.NumberOfVertices)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 485</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">continue</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 486</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 487</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">LineSegment2d</span> ls = pl.GetLineSegment2dAt(vn);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 488</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point2d</span> pt1 = ls.StartPoint,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 489</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pt5 = ls.EndPoint;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 490</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Vector2d</span> vec = pt5 - pt1;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 491</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> d_3 = vec.Length / 3;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 492</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point2d</span> pt2 = pt1 + (vec.GetNormal() * d_3),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 493</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pt4 = pt1 + (vec.GetNormal() * 2 * d_3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 494</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Vector2d</span> vec2 = pt4 - pt2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 495</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 496</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (bLeft)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 497</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vec2 = vec2.RotateBy(<span style="COLOR: teal">Math</span>.PI / 3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 498</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 499</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vec2 = vec2.RotateBy(5 * <span style="COLOR: teal">Math</span>.PI / 3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 500</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 501</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point2d</span> pt3 = pt2 + vec2;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 502</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 503</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.AddVertexAt(++vn, pt2, 0, sw, ew);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 504</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.AddVertexAt(++vn, pt3, 0, sw, ew);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 505</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.AddVertexAt(++vn, pt4, 0, sw, ew);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 506</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 507</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span> <span style="COLOR: blue">if</span> (st == <span style="COLOR: teal">SegmentType</span>.Arc)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 508</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 509</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">CircularArc3d</span> ca = pl.GetArcSegmentAt(vn);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 510</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> oldBulge = pl.GetBulgeAt(vn);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 511</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 512</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Build a standard arc and use that for the calcs</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 513</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 514</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Arc</span> arc = circArc2Arc(ca);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 515</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 516</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Get the main 5 points</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 517</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 518</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point3d</span> pt1 = arc.StartPoint,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 519</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pt5 = arc.EndPoint;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 520</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 521</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> ln = arc.GetDistAtPoint(pt5);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 522</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point3d</span> pt2 = arc.GetPointAtDist(ln / 3),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 523</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; pt4 = arc.GetPointAtDist(2 * ln / 3);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 524</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 525</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point3d</span> mid = arc.GetPointAtDist(ln / 2);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 526</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 527</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">CircularArc3d</span> tmpArc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 528</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">CircularArc3d</span>(pt2, mid, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 529</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (bLeft)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 530</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tmpArc.RotateBy(5*<span style="COLOR: teal">Math</span>.PI/3, tmpArc.Normal, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 531</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 532</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tmpArc.RotateBy(<span style="COLOR: teal">Math</span>.PI / 3, tmpArc.Normal, pt4);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 533</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 534</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Point3d</span> pt3 = tmpArc.StartPoint;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 535</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 536</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Now add the new segments, setting the bulge</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 537</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// for the existing one and the new ones to a third</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 538</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// (as the segs are a third as big as the old one)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 539</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 540</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">CoordinateSystem3d</span> ecs = pl.Ecs.CoordinateSystem3d;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 541</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Plane</span> pn = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Plane</span>(ecs.Origin, pl.Normal);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 542</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">double</span> bu = oldBulge / 3;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 543</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 544</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.SetBulgeAt(vn, bu);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 545</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.AddVertexAt(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 546</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ++vn, pt2.Convert2d(pn), bu, sw, ew);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 547</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.AddVertexAt(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 548</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ++vn, pt3.Convert2d(pn), bu, sw, ew);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 549</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pl.AddVertexAt(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 550</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ++vn, pt4.Convert2d(pn), bu, sw, ew);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 551</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 552</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 553</span>&nbsp; &nbsp;&nbsp; &nbsp; pl.DowngradeOpen();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 554</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 555</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 556</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">LongOperationManager</span> :</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 557</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">IDisposable</span>, System.Windows.Forms.<span style="COLOR: teal">IMessageFilter</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 558</span>&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 559</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// The message code corresponding to a keypress</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 560</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">int</span> WM_KEYDOWN = 0x0100;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 561</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 562</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// The number of times to update the progress meter</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 563</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// (for some reason you need 600 to tick through</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 564</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; for each percent)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 565</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">int</span> progressMeterIncrements = 600;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 566</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 567</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Internal members for metering progress</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 568</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: teal">ProgressMeter</span> pm;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 569</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">long</span> updateIncrement;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 570</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">long</span> currentInc;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 571</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 572</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// External flag for checking cancelled status</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 573</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> cancelled = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 574</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 575</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Constructor</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 576</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 577</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> LongOperationManager(<span style="COLOR: blue">string</span> message)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 578</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 579</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;System.Windows.Forms.<span style="COLOR: teal">Application</span>.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 580</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; AddMessageFilter(<span style="COLOR: blue">this</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 581</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pm = <span style="COLOR: blue">new</span> <span style="COLOR: teal">ProgressMeter</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 582</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pm.Start(message);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 583</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pm.SetLimit(progressMeterIncrements);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 584</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;currentInc = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 585</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 586</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 587</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// System.IDisposable.Dispose</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 588</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 589</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 590</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 591</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pm.Stop();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 592</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;pm.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 593</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;System.Windows.Forms.<span style="COLOR: teal">Application</span>.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 594</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; RemoveMessageFilter(<span style="COLOR: blue">this</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 595</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 596</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 597</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Set the total number of operations</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 598</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 599</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SetTotalOperations(<span style="COLOR: blue">long</span> totalOps)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 600</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 601</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// We really just care about when we need</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 602</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// to update the timer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 603</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;updateIncrement =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 604</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (totalOps &gt; progressMeterIncrements ?</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 605</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; totalOps / progressMeterIncrements :</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 606</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; totalOps</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 607</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 608</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 609</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 610</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// This function is called whenever an operation</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 611</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// is performed</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 612</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 613</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> Tick()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 614</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 615</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (++currentInc == updateIncrement)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 616</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 617</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pm.MeterProgress();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 618</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; currentInc = 0;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 619</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; System.Windows.Forms.<span style="COLOR: teal">Application</span>.DoEvents();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 620</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 621</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Check whether the filter has set the flag</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 622</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (cancelled)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 623</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pm.Stop();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 624</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 625</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> !cancelled;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 626</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 627</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 628</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// The message filter callback</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 629</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 630</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> PreFilterMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 631</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">ref</span> System.Windows.Forms.<span style="COLOR: teal">Message</span> m</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 632</span>&nbsp; &nbsp;&nbsp; &nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 633</span>&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 634</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (m.Msg == WM_KEYDOWN)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 635</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 636</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Check for the Escape keypress</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 637</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; System.Windows.Forms.<span style="COLOR: teal">Keys</span> kc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 638</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (System.Windows.Forms.<span style="COLOR: teal">Keys</span>)(<span style="COLOR: blue">int</span>)m.WParam &amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 639</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; System.Windows.Forms.<span style="COLOR: teal">Keys</span>.KeyCode;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 640</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 641</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (m.Msg == WM_KEYDOWN &amp;&amp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 642</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;kc == System.Windows.Forms.<span style="COLOR: teal">Keys</span>.Escape)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 643</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 644</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cancelled = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 645</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 646</span> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 647</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Return true to filter all keypresses</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 648</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 649</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 650</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Return false to let other messages through</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 651</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 652</span>&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: red">&nbsp; 653</span>&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 654</span>&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 655</span> }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 656</span> </p></div>

<p>Here's the source <a href="http://through-the-interface.typepad.com/through_the_interface/files/kochize-with-long-operation-manager.cs">the source file</a> for download.</p>
