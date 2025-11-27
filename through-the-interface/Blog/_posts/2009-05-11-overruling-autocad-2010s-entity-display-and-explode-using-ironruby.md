---
layout: "post"
title: "Overruling AutoCAD 2010&rsquo;s entity display and explode using IronRuby"
date: "2009-05-11 06:54:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Custom objects"
  - "Graphics system"
  - "IronRuby"
  - "Overrules"
  - "Ruby"
  - "Solid modeling"
original_url: "https://www.keanw.com/2009/05/overruling-autocad-2010s-entity-display-and-explode-using-ironruby.html "
typepad_basename: "overruling-autocad-2010s-entity-display-and-explode-using-ironruby"
typepad_status: "Publish"
---

<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/04/overruling-autocad-2010s-entity-display-and-explode-using-ironpython.html">this previous post</a>, where I gave the same treatment to IronPython, I’ve been trying to get display and explode overrules defined in IronRuby working properly in AutoCAD. IronRuby is still at version 0.3, so this effort has been hindered by a number of CLR interop bugs (it turns out).</p>
<p>I finally managed to work around these issues thanks to Ivan Porto Carrero, who is just finishing up his book, <a href="http://www.manning.com/carrero/">Iron Ruby in Action</a>, and has been working with IronRuby since pre-Alpha 1 (brave fellow). Ivan’s help was invaluable: he ended up downloading and installing AutoCAD 2010 to work through the issues on my behalf, uncover the various problems and submitting bugs against IronRuby, where appropriate. There was a small measure of self-interest involved, as I’ve been working on some content Ivan will be including in his book – I just hope the material proves usable for him. Oh, and hopefully I’ll be getting a few copies of Ivan’s book to give away at my proposed AU 2009 class on “Developing for AutoCAD with IronPython and IronRuby”.</p>
<p>Incidentally, in spite of the workarounds implemented with Ivan’s help, I wasn’t able to get <a href="http://through-the-interface.typepad.com/through_the_interface/2009/04/jigging-an-autocad-solid-using-ironruby-and-net-well-almost.html">the previous IronRuby sample to jig a solid</a> working. Hopefully the underlying IronRuby bug that’s stopping it from working will be addressed in an upcoming release.</p>
<p>The C# code defining the RBLOAD command has been update slightly:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> IronRuby.Hosting;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> IronRuby;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> Microsoft.Scripting.Hosting;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> System.Reflection;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="COLOR: blue">namespace</span> RubyLoader</p>
<p style="MARGIN: 0px">{</p>
<p style="MARGIN: 0px">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px">&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; [<span style="COLOR: #2b91af">CommandMethod</span>(<span style="COLOR: #a31515">&quot;-RBLOAD&quot;</span>)]</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> RubyLoadCmdLine()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; RubyLoad(<span style="COLOR: blue">true</span>);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; [<span style="COLOR: #2b91af">CommandMethod</span>(<span style="COLOR: #a31515">&quot;RBLOAD&quot;</span>)]</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> RubyLoadUI()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; RubyLoad(<span style="COLOR: blue">false</span>);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">void</span> RubyLoad(<span style="COLOR: blue">bool</span> useCmdLine)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Document</span> doc =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Editor</span> ed = doc.Editor;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">short</span> fd =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="COLOR: blue">short</span>)<span style="COLOR: #2b91af">Application</span>.GetSystemVariable(<span style="COLOR: #a31515">&quot;FILEDIA&quot;</span>);</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// As the user to select a .rb file</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">PromptOpenFileOptions</span> pfo =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">PromptOpenFileOptions</span>(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #a31515">&quot;Select Ruby script to load&quot;</span></p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; pfo.Filter = <span style="COLOR: #a31515">&quot;Ruby script (*.rb)|*.rb&quot;</span>;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; pfo.PreferCommandLine =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (useCmdLine || fd == 0);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">PromptFileNameResult</span> pr =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.GetFileNameForOpen(pfo);</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// And then try to load and execute it</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: #2b91af">PromptStatus</span>.OK)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExecuteRubyScript(pr.StringResult);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; [<span style="COLOR: #2b91af">LispFunction</span>(<span style="COLOR: #a31515">&quot;RBLOAD&quot;</span>)]</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: #2b91af">ResultBuffer</span> RubyLoadLISP(<span style="COLOR: #2b91af">ResultBuffer</span> rb)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">const</span> <span style="COLOR: blue">int</span> RTSTR = 5005;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Document</span> doc =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Editor</span> ed = doc.Editor;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">if</span> (rb == <span style="COLOR: blue">null</span>)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="COLOR: #a31515">&quot;\nError: too few arguments\n&quot;</span>);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// We&#39;re only really interested in the first argument</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Array</span> args = rb.AsArray();</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">TypedValue</span> tv = (<span style="COLOR: #2b91af">TypedValue</span>)args.GetValue(0);</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// Which should be the filename of our script</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">if</span> (tv != <span style="COLOR: blue">null</span> &amp;&amp; tv.TypeCode == RTSTR)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// If we manage to execute it, let&#39;s return the</span></p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// filename as the result of the function</span></p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// (just as (arxload) does)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">bool</span> success =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExecuteRubyScript(<span style="COLOR: #2b91af">Convert</span>.ToString(tv.Value));</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">return</span></p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (success ?</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">ResultBuffer</span>(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">TypedValue</span>(RTSTR, tv.Value)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; : <span style="COLOR: blue">null</span>);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">return</span> <span style="COLOR: blue">null</span>;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; <span style="COLOR: blue">private</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">bool</span> ExecuteRubyScript(<span style="COLOR: blue">string</span> file)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: green">// If the file exists, let&#39;s load and execute it</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">bool</span> ret = System.IO.<span style="COLOR: #2b91af">File</span>.Exists(file);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">if</span> (ret)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">try</span></p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">LanguageSetup</span> ls = <span style="COLOR: #2b91af">Ruby</span>.CreateRubySetup();</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">ScriptRuntimeSetup</span> rs =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">ScriptRuntimeSetup</span>();</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rs.LanguageSetups.Add(ls);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rs.DebugMode = <span style="COLOR: blue">true</span>;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">ScriptRuntime</span> runtime =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Ruby</span>.CreateRuntime(rs);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; runtime.LoadAssembly(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Assembly</span>.GetAssembly(<span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Commands</span>))</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">ScriptEngine</span> engine = <span style="COLOR: #2b91af">Ruby</span>.GetEngine(runtime);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; engine.ExecuteFile(file);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">catch</span> (System.<span style="COLOR: #2b91af">Exception</span> ex)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Document</span> doc =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #2b91af">Editor</span> ed = doc.Editor;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: #a31515">&quot;\nProblem executing script: {0}&quot;</span>, ex</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">return</span> ret;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160; }</p>
<p style="MARGIN: 0px">}</p></div>
<p>Inside the ExecuteRubyScript() function we’re now creating a runtime environment within which we load our C# assembly, as it defines some classes to workaround some derivation problems. Here is the additional C# source that needed to be added to our project:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="COLOR: blue">namespace</span> ConcreteClasses</p>
<p style="MARGIN: 0px">{</p>
<p style="MARGIN: 0px">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: #2b91af">DrawableOverrule</span> :</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; Autodesk.AutoCAD.GraphicsInterface.<span style="COLOR: #2b91af">DrawableOverrule</span></p>
<p style="MARGIN: 0px">&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">int</span> ParentSetAttributes(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.GraphicsInterface.<span style="COLOR: #2b91af">Drawable</span> drawable,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.GraphicsInterface.<span style="COLOR: #2b91af">DrawableTraits</span> traits</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; )</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">return</span> <span style="COLOR: blue">base</span>.SetAttributes(drawable, traits);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">bool</span> ParentWorldDraw(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.GraphicsInterface.<span style="COLOR: #2b91af">Drawable</span> drawable,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.GraphicsInterface.<span style="COLOR: #2b91af">WorldDraw</span> wd</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; )</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; {</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="COLOR: blue">return</span> <span style="COLOR: blue">base</span>.WorldDraw(drawable, wd);</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; }</p>
<p style="MARGIN: 0px">&#0160; }</p>
<p style="MARGIN: 0px">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: #2b91af">TransformOverrule</span> :</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; Autodesk.AutoCAD.DatabaseServices.<span style="COLOR: #2b91af">TransformOverrule</span></p>
<p style="MARGIN: 0px">&#0160; {}</p>
<p style="MARGIN: 0px">}</p></div>
<p>This code works around two issues:</p>
<ol>
<li>IronRuby 0.3 has issues implementing abstract classes and our Overrule classes are abstract. So we derive “concrete” classes from these abstract classes – empty implementations would be enough, as neither DrawableOverrule no TransformOverrule include abstract function definitions that require overriding – and in our Ruby script we derive from these concrete classes. </li>
<li>Our concrete DrawableOverrule is not empty as we need to work around another issue: super-messaging to our parent classes proved problematic, so we now expose methods (ParentSetAttributes() and ParentWorldDraw()) that do so explicitly. </li>
</ol>
<p>I also hit a number of subtle issues related to Ruby’s naming conventions… Here’s what <a href="http://ironruby.net/Documentation/CLR_Interop/Names">the IronRuby documentation</a> says about naming conventions when using CLR types (which our types are, as they’re imported using .NET):</p>
<blockquote>
<p><em>In an effort to make consuming .NET APIs in IronRuby more Rubyesque, IronRuby allows calling .NET code with Ruby idioms:</em></p>
<ol>
<li><em>CLR namespaces and interfaces must be capitalized as they are mapped onto Ruby modules</em> </li>
<li><em>CLR classes must be capitalized as they are mapped onto Ruby classes</em> </li>
<li><em>CLR methods that you call may either retain their original spelling (ie &quot;WriteLine&quot;) or they may be used in a more Rubyesque form which is obtained by translating CamelCase to lowercase_and_delimited (ie &quot;write_line&quot;).</em> </li>
<li><em>CLR virtual methods which you override from IronRuby must be in their lowercase_and_delimited form.</em> </li>
</ol>
</blockquote>
<p>I was fine with item 1 (I’d hit this in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/04/using-ironruby-inside-autocad.html">my first attempt to write an IronRuby application</a>, and found that I had to capitalicise even the variables I’d created as shortcuts to namespaces) and with item 2.</p>
<p>It was item 4 that caught me unawares: my WorldDraw() and SetAttributes() overides were simply not getting called: they had to be renamed to world_draw() and set_attributes(). That took some time to work out (and is no doubt one of the issues with the solid jigging code).</p>
<p>Item 3 proved to be quite fun: I ended up going back through the entire code sample, changing the CamelCase method calls to lowercase_and_delimited, even if this sometimes ended up looking a little strange (my personal favourite being Transaction.add_newly_created_d_b_object() :-).</p>
<p>Here’s the code from our .rb file:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px">require &#39;acmgd.dll&#39;</p>
<p style="MARGIN: 0px">require &#39;acdbmgd.dll&#39;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">Ai =&#0160; Autodesk::AutoCAD::Internal</p>
<p style="MARGIN: 0px">Aiu = Autodesk::AutoCAD::Internal::Utils</p>
<p style="MARGIN: 0px">Aas = Autodesk::AutoCAD::ApplicationServices</p>
<p style="MARGIN: 0px">Ads = Autodesk::AutoCAD::DatabaseServices</p>
<p style="MARGIN: 0px">Aei = Autodesk::AutoCAD::EditorInput</p>
<p style="MARGIN: 0px">Agi = Autodesk::AutoCAD::GraphicsInterface</p>
<p style="MARGIN: 0px">Ag =&#0160; Autodesk::AutoCAD::Geometry</p>
<p style="MARGIN: 0px">Ac =&#0160; Autodesk::AutoCAD::Colors</p>
<p style="MARGIN: 0px">Ar =&#0160; Autodesk::AutoCAD::Runtime</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def print_message(msg)</p>
<p style="MARGIN: 0px">&#0160; app = Aas::Application</p>
<p style="MARGIN: 0px">&#0160; doc = app.document_manager.mdi_active_document</p>
<p style="MARGIN: 0px">&#0160; ed = doc.editor</p>
<p style="MARGIN: 0px">&#0160; ed.write_message(msg)</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"># Function to register AutoCAD commands</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def autocad_command(cmd)&#0160; </p>
<p style="MARGIN: 0px">&#0160; cc = Ai::CommandCallback.new method(cmd)</p>
<p style="MARGIN: 0px">&#0160; Aiu.add_command(&#39;rbcmds&#39;, cmd, cmd, Ar::CommandFlags.Modal, cc)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # Let&#39;s now write a message to the command-line</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; print_message(&quot;\nRegistered Ruby command: &quot; + cmd)</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def add_commands(names)</p>
<p style="MARGIN: 0px">&#0160; names.each { |n| autocad_command n }</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">APP_NAME = &quot;TTIF_PIPE&quot;</p>
<p style="MARGIN: 0px">APP_CODE = 1001</p>
<p style="MARGIN: 0px">RAD_CODE = 1040</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def pipe_radius_for_object(obj)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # Get the XData for a particular object</p>
<p style="MARGIN: 0px">&#0160; # and return the &quot;pipe radius&quot; if it exists</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; res = 0.0</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; begin</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; rb = obj.XData</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if rb.nil?</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; return res</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; foundStart = false</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; for tv in rb do</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; if (tv.type_code == APP_CODE and tv.value == APP_NAME)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; foundStart = true</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; else</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if foundStart</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if (tv.type_code == RAD_CODE)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res = tv.value</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; break</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end&#0160;&#0160;&#0160;&#0160;&#0160; </p>
<p style="MARGIN: 0px">&#0160; rescue</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; return 0.0</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160; return res</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def set_pipe_radius_for_object(tr, obj, radius)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # Set the pipe radius as XData on a particular object</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; db = obj.Database</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # Make sure the application is registered</p>
<p style="MARGIN: 0px">&#0160; # (we could separate this out to be called</p>
<p style="MARGIN: 0px">&#0160; # only once for a set of operations)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; rat =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; tr.get_object(db.reg_app_table_id, Ads::OpenMode.for_read)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; if (not rat.Has(APP_NAME))</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; rat.UpgradeOpen()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; ratr = Ads::RegAppTableRecord.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; ratr.Name = APP_NAME</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; rat.Add(ratr)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; tr.add_newly_created_d_b_object(ratr, true)</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # Create the XData and set it on the object</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; rb = Ads::ResultBuffer.new(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::TypedValue.new(APP_CODE, APP_NAME),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::TypedValue.new(RAD_CODE, radius))</p>
<p style="MARGIN: 0px">&#0160; obj.XData = rb</p>
<p style="MARGIN: 0px">&#0160; rb.Dispose()</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">class PipeDrawOverrule &lt; ConcreteClasses::DrawableOverrule</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # The base class for our draw overrules specifying the</p>
<p style="MARGIN: 0px">&#0160; # registered application name for the XData upon which</p>
<p style="MARGIN: 0px">&#0160; # to filter</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def initialize</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Tell AutoCAD to filter on our application name</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # (this means our overrule will only be called</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # on objects possessing XData with this name)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; set_x_data_filter(APP_NAME)</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">class LinePipeDrawOverrule &lt; PipeDrawOverrule</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # An overrule to make a pipe out of a line</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def initialize</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; @sweep_opts = Ads::SweepOptions.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; super</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def world_draw(d, wd)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; radius = pipe_radius_for_object(d)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if radius &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # Draw the line as is, with overruled attributes</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # Should just be able to call super</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; parent_world_draw(d, wd)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; if not d.id.is_null and d.length &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; # Draw a pipe around the line</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; c = wd.sub_entity_traits.true_color</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.sub_entity_traits.true_color =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ac::EntityColor.new 0x00AFAFFF</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.sub_entity_traits.line_weight =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ads::LineWeight.line_weight_000</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; start = d.start_point</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt = d.end_point</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; norm = Ag::Vector3d.new(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt.X - start.X,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt.Y - start.Y,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt.Z - start.Z)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr = Ads::Circle.new start, norm, radius</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe = Ads::ExtrudedSurface.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; begin</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.create_extruded_surface(clr, norm, @sweep_opts)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rescue</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print_message &quot;\nFailed with CreateExtrudedSurface.&quot;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr.dispose()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.world_draw(wd)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.dispose()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.sub_entity_traits.true_color = c</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; return true</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; return super</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def set_attributes(d, t)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Should just be able to call super</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; i = parent_set_attributes(d, t)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; radius = pipe_radius_for_object(d)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if radius &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # Set color to magenta</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; t.color = 6</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # and lineweight to .40 mm</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; t.line_weight = Ads::LineWeight.line_weight_040</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; return i</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">class CirclePipeDrawOverrule &lt; PipeDrawOverrule</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # An overrule to make a pipe out of a circle</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def initialize</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; @sweep_opts = Ads::SweepOptions.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; super</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def world_draw(d, wd)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; radius = pipe_radius_for_object(d)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if radius &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # Draw the circle as is, with overruled attributes</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; parent_world_draw(d, wd)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # Needed to avoid ill-formed swept surface</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; if d.radius &gt; radius</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; # Draw a pipe around the circle</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; c = wd.sub_entity_traits.true_color</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.sub_entity_traits.true_color =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ac::EntityColor.new 0x3FFFE0E0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.sub_entity_traits.line_weight =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ads::LineWeight.LineWeight000</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; start = d.StartPoint</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen = d.Center</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; norm = Ag::Vector3d.new(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen.X - start.X,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen.Y - start.Y,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen.Z - start.Z)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ads::Circle.new start, norm.cross_product(d.normal), radius</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe = Ads::SweptSurface.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.create_swept_surface(clr, d, @sweep_opts)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr.dispose()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.world_draw(wd)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.dispose()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; wd.sub_entity_traits.true_color = c</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; return true</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; return parent_world_draw(d, wd)</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def set_attributes(d, t)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Should just be able to call super</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; i = parent_set_attributes(d, t)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; radius = pipe_radius_for_object(d)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if radius &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # Set color to yellow</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; t.color = 2</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # and lineweight to .60 mm</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; t.line_weight = Ads::LineWeight.line_weight_060</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; return i</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">class LinePipeTransformOverrule &lt; ConcreteClasses::TransformOverrule</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # An overrule to explode a linear pipe into Solid3d objects</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def initialize</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; @sweep_opts = Ads::SweepOptions.new</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def explode(e, objs)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; radius = pipe_radius_for_object(e)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if radius &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; if not e.Id.IsNull and e.Length &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; # Draw a pipe around the line</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; start = e.start_point</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt = e.end_point</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; norm = Ag::Vector3d.new(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt.X - start.X,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt.Y - start.Y,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endpt.Z - start.Z)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr = Ads::Circle.new start, norm, radius</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe = Ads::ExtrudedSurface.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; begin</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.create_extruded_surface clr, norm, @sweep_opts</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rescue</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print_message &quot;\nFailed with CreateExtrudedSurface.&quot;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr.dispose()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objs.add(pipe)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; return</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; super</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">class CirclePipeTransformOverrule &lt; ConcreteClasses::TransformOverrule</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # An overrule to explode a circular pipe into Solid3d objects</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def initialize</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; @sweep_opts = Ads::SweepOptions.new</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; def explode(e, objs)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; radius = pipe_radius_for_object(e)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if radius &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; if e.radius &gt; radius</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; start = e.start_point</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen = e.center</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; norm = Ag::Vector3d.new(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen.X - start.X,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen.Y - start.Y,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cen.Z - start.Z)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ads::Circle.new start, norm.cross_product(e.normal), radius</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe = Ads::SweptSurface.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pipe.create_swept_surface(clr, e, @sweep_opts)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; clr.dispose()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objs.add(pipe)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; return</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; super</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def overrule(enable)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; # Regen to see the effect</p>
<p style="MARGIN: 0px">&#0160; # (turn on/off Overruling and LWDISPLAY)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; Ar::Overrule.Overruling = enable</p>
<p style="MARGIN: 0px">&#0160; if enable</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; Aas::Application.set_system_variable(&quot;LWDISPLAY&quot;, 1)</p>
<p style="MARGIN: 0px">&#0160; else</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; Aas::Application.set_system_variable(&quot;LWDISPLAY&quot;, 0)</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; doc = Aas::Application.document_manager.mdi_active_document</p>
<p style="MARGIN: 0px">&#0160; doc.send_string_to_execute(&quot;REGEN3\n&quot;, true, false, false)</p>
<p style="MARGIN: 0px">&#0160; doc.editor.regen()</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">$overruling = false</p>
<p style="MARGIN: 0px">$radius = 0.0</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def overrule1</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; begin</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if !$overruling</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; $lpdo = LinePipeDrawOverrule.new&#0160;&#0160;&#0160;&#0160;&#0160; </p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; $cpdo = CirclePipeDrawOverrule.new&#0160;&#0160;&#0160;&#0160;&#0160; </p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; $lpto = LinePipeTransformOverrule.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; $cpto = CirclePipeTransformOverrule.new</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.add_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Line.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $lpdo,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; true)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.add_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Line.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $lpto,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; true)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.add_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Circle.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $cpdo,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; true)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.add_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Circle.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $cpto,</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; true)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; $overruling = true</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; overrule(true)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160; rescue</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; print_message(&quot;\nProblem found: &quot; + $! + &quot;\n&quot;)</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def overrule0</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; begin</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if $overruling</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.remove_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Line.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $lpdo)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.remove_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Line.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $lpto)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.remove_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Circle.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $cpdo)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; Ads::ObjectOverrule.remove_overrule(</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Ar::RXClass::get_class(Ads::Circle.to_clr_type),</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $cpto)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; $overruling = false</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; overrule(false)</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160; rescue</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; print_message(&quot;\nProblem found: &quot; + $! + &quot;\n&quot;)</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">def makePipe()</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160; begin</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; doc = Aas::Application.document_manager.mdi_active_document</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; db = doc.Database</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; ed = doc.Editor</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Ask the user to select the entities to make into pipes</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; pso = Aei::PromptSelectionOptions.new</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; pso.allow_duplicates = false</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; pso.message_for_adding =</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; &quot;\nSelect objects to turn into pipes: &quot;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; sel_res = ed.GetSelection(pso)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # If the user didn&#39;t make valid selection, we return</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if sel_res.Status != Aei::PromptStatus.OK</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; return</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; ss = sel_res.Value</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Ask the user for the pipe radius to set</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; pdo = Aei::PromptDoubleOptions.new &quot;\nSpecify pipe radius:&quot;</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Use the previous value, if if already called</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if $radius &gt; 0.0</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; pdo.default_value = $radius</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; pdo.use_default_value = true</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; pdo.allow_negative = false</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; pdo.allow_zero = false</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; pdr = ed.get_double(pdo)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Return if something went wrong</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; if pdr.Status != Aei::PromptStatus.OK</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; return</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Set the &quot;last radius&quot; value for when</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # the command is called next</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; $radius = pdr.value</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Use a transaction to edit our various objects</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; tr = db.transaction_manager.start_transaction()</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; # Loop through the selected objects</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; for o in ss do</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # We could choose only to add XData to the objects</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; # we know will use it (Lines and Circles, for now)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; obj = tr.get_object(o.object_id, Ads::OpenMode.for_write)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160;&#0160;&#0160; set_pipe_radius_for_object(tr, obj, $radius)</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; tr.Commit()</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; tr.Dispose()</p>
<p style="MARGIN: 0px">&#0160; rescue</p>
<p style="MARGIN: 0px">&#0160;&#0160;&#0160; print_message(&quot;\nProblem found: &quot; + $! + &quot;\n&quot;)</p>
<p style="MARGIN: 0px">&#0160; end</p>
<p style="MARGIN: 0px">end</p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px">add_commands [&quot;overrule1&quot;, &quot;overrule0&quot;, &quot;makePipe&quot;]</p></div>
<p>When we build and load our C# module via NETLOAD, we can then use RBLOAD to load our .rb file:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156f81c767970c-pi"><img alt="Select a Ruby script to load and execute" border="0" height="290" src="/assets/image_692466.jpg" style="BORDER-BOTTOM: 0px; BORDER-LEFT: 0px; DISPLAY: inline; BORDER-TOP: 0px; BORDER-RIGHT: 0px" title="Select a Ruby script to load and execute" width="400" /></a> </p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px">Registered Ruby command: overrule1</p>
<p style="MARGIN: 0px">Registered Ruby command: overrule0</p>
<p style="MARGIN: 0px">Registered Ruby command: makePipe</p></div>
<p>As in the previous examples, we create some geometry to which we attach data using the MAKEPIPE command:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2011570779134970b-pi"><img alt="Some basic linear geometry" border="0" height="335" src="/assets/image_42875.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Some basic linear geometry" width="475" /></a></p>
<p>Which we then use as a pipe radius for our geometry by turning on our overrules using the OVERRULE1 command:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2011570779141970b-pi"><img alt="Our overruled lines and circles with varying profile radii" border="0" height="334" src="/assets/image_245496.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Our overruled lines and circles with varying profile radii" width="474" /></a> </p>
<p>Here is the same geometry in a conceptual 3D view:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201157077915d970b-pi"><img alt="3D conceptual display of our overruled geometry" border="0" height="296" src="/assets/image_321497.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="3D conceptual display of our overruled geometry" width="471" /></a> </p>
<p>And finally we call EXPLODE to try out our TransformOverrule and see the resultant Solid3d objects:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2011570779178970b-pi"><img alt="3D conceptual display of our exploded, overruled geometry" border="0" height="295" src="/assets/image_865156.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="3D conceptual display of our exploded, overruled geometry" width="469" /></a></p>
