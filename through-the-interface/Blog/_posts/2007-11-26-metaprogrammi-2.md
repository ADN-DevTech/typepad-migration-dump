---
layout: "post"
title: "Metaprogramming with AutoCAD - Part 3"
date: "2007-11-26 21:28:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/11/metaprogrammi-2.html "
typepad_basename: "metaprogrammi-2"
typepad_status: "Publish"
---

<p>In parts <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/metaprogramming.html">1</a> &amp; <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/metaprogrammi-1.html">2</a> of this series we looked at metaprogramming with AutoCAD using AutoLISP and VB(A), and then using VB.NET and C#.</p>

<p>In this post we're going to look at what's possible from F#, through the lens of my relative inexperience with the language, of course.</p>

<p>The <a href="http://cs.hubfs.net/blogs/tomasp/archive/2006/07/07/413.aspx">quotations</a> mechanism in F# appears to be the way to represent, analyse and execute program structure. <a href="http://research.microsoft.com/~dsyme/papers/ml03-syme.pdf">This article</a> describes the concepts, although it's quite deep and doesn't address the case that's most immediately interesting to AutoCAD develeopers: the ability to evaluate and execute code represented as a string. In fact, this doesn't yet appear to be part of the F# language, according to <a href="http://cs.hubfs.net/forums/thread/3047.aspx">this thread</a>, which helped me formulate the below F# version of the code I showed in my last post.</p>

<p>I would like to see a Microsoft.FSharp.FSharpCodeProvider class allowing execution of F# code provided as a string. I don't know whether the plan is to provide this - or something like it - but I can certainly see it being of use.</p>

<p>What we can do today is execute C# and VB.NET code, just as we did in the previous post, but this time the generation language is F#. The C# and VB.NET code we create is heterogeneous from the perspective of F#, of course.</p>

<p>Here's the F# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> MyNamespace.MyApplication</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Import managed assemblies</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="COLOR: maroon">@&quot;C:\Program Files\Autodesk\AutoCAD 2008&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.CodeDom.Compiler</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.Reflection;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Microsoft.VisualBasic;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Microsoft.CSharp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> acadFolder =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: maroon">&quot;c:\\Program Files\\Autodesk\\AutoCAD 2008\\&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> CompileVBExpression (expression:string) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> source = </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports System\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.Runtime\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.ApplicationServices\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.DatabaseServices\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.EditorInput\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.Geometry\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Namespace VBCodeEval\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Public Class VBCodeEval\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Public Function EvalCode() As Object\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Return &quot;</span> ^ expression ^ <span style="COLOR: maroon">&quot;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;End Function\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;End Class\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;End Namespace&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> provider = <span style="COLOR: blue">new</span> VBCodeProvider()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> parameters = <span style="COLOR: blue">new</span> CompilerParameters()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(parameters.ReferencedAssemblies.Add</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;system.dll&quot;</span>))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(parameters.ReferencedAssemblies.Add</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (acadFolder ^ <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span>))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(parameters.ReferencedAssemblies.Add</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (acadFolder ^ <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span>))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; parameters.CompilerOptions &lt;- <span style="COLOR: maroon">&quot;/t:library&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; parameters.GenerateInMemory &lt;- <span style="COLOR: blue">true</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; provider.CompileAssemblyFromSource(parameters, [|source|])</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> RunVBCode (expression:string) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> results = CompileVBExpression expression</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> not results.Errors.HasErrors <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> a = results.CompiledAssembly</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> o = a.CreateInstance(<span style="COLOR: maroon">&quot;VBCodeEval.VBCodeEval&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> t = o.GetType()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> mi = t.GetMethod(<span style="COLOR: maroon">&quot;EvalCode&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; mi.Invoke(o, <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">null</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> CompileCSExpression (expression:string) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> source = </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using System;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.Runtime;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.ApplicationServices;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.DatabaseServices;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.EditorInput;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.Geometry;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;namespace CSCodeEval{\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;public class CSCodeEval{\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;public object EvalCode(){\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;return &quot;</span> ^ expression ^ <span style="COLOR: maroon">&quot;;\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;}\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;}\n&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;}&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> provider = <span style="COLOR: blue">new</span> CSharpCodeProvider();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> parameters = <span style="COLOR: blue">new</span> CompilerParameters();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(parameters.ReferencedAssemblies.Add</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;system.dll&quot;</span>))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(parameters.ReferencedAssemblies.Add</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (acadFolder ^ <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span>))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(parameters.ReferencedAssemblies.Add</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (acadFolder ^ <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span>))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; parameters.CompilerOptions &lt;- <span style="COLOR: maroon">&quot;/t:library&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; parameters.GenerateInMemory &lt;- <span style="COLOR: blue">true</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; provider.CompileAssemblyFromSource(parameters, [|source|])</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> RunCSCode (expression:string) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> results = CompileCSExpression expression</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> not results.Errors.HasErrors <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> a = results.CompiledAssembly</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> o = a.CreateInstance(<span style="COLOR: maroon">&quot;CSCodeEval.CSCodeEval&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> t = o.GetType()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> mi = t.GetMethod(<span style="COLOR: maroon">&quot;EvalCode&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; mi.Invoke(o, <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">null</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;ev&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> evaluate () =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> csCode =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: maroon">&quot;typeof(Autodesk.AutoCAD.&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: maroon">&quot;ApplicationServices.Application)&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> vbCode =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: maroon">&quot;GetType(Autodesk.AutoCAD.&quot;</span> ^</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: maroon">&quot;ApplicationServices.Application)&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Autodesk.AutoCAD.ApplicationServices.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.DocumentManager.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; MdiActiveDocument.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nEvaluating C# code:\n&quot;</span> ^ csCode);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> csRes = RunCSCode csCode</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> csRes != <span style="COLOR: blue">null</span> <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nC# code returned: &quot;</span> ^ csRes.ToString());</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nEvaluating VB code:\n&quot;</span> ^ vbCode);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> vbRes = RunVBCode vbCode</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> vbRes != <span style="COLOR: blue">null</span> <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nVB code returned: &quot;</span> ^ vbRes.ToString());&nbsp; </p></div>

<p>And when we run the &quot;ev&quot; command, we see the same results as with the VB.NET and C# implementation in the last post:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">ev</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Evaluating C# code:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">typeof(Autodesk.AutoCAD.ApplicationServices.Application)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">C# code returned: Autodesk.AutoCAD.ApplicationServices.Application</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Evaluating VB code:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">GetType(Autodesk.AutoCAD.ApplicationServices.Application)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">VB code returned: Autodesk.AutoCAD.ApplicationServices.Application</p></div>
