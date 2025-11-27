---
layout: "post"
title: "Metaprogramming with AutoCAD - Part 2"
date: "2007-11-22 09:07:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Visual Studio"
original_url: "https://www.keanw.com/2007/11/metaprogrammi-1.html "
typepad_basename: "metaprogrammi-1"
typepad_status: "Publish"
---

<p>In this post we're going to continue the topic started in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/metaprogramming.html">Part 1</a> of this series, which looked briefly at metaprogramming with AutoCAD using AutoLISP and VB(A). Now we're going to look at .NET, focusing initially on C# and VB.NET.</p>

<p>[I found the inspiration for the code in this post from <a href="http://www.codeproject.com/csharp/evalcscode.asp">The Code Project</a>, although I had to update the code to use non-deprecated CLR methods as well as making it work for AutoCAD, of course.]</p>

<p>While .NET doesn't provide something as simple as an Eval() function, it actually provides something much more interesting. The CLR exposes the ability to compile and execute source code in .NET languages for which implementations of the CodeDomProvider protocol have been provided.</p>

<p>The Microsoft.CSharp namespace, for instance, contains the CSharpCodeProvider class, which allows you to specify and compile C# code from any .NET language. Microsoft.VisualBasic contains VBCodeProvider, which does the same for VB.NET.</p>

<p>Which means that it's actually very easy to implement dynamic metaprogramming in a homogeneous or heterogeneous fashion from .NET. Yay! :-)</p>

<p>As I had some time, and decided that implementing this for both C# and VB.NET would tell the story nicely, I've provided code below for both environments.</p>

<p>Here's the C# code, which shows how to compile and execute C# (homogeneous) and VB.NET (heterogeneous) code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Microsoft.CSharp;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Microsoft.VisualBasic;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.CodeDom.Compiler;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Reflection;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Text;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> Metaprogramming</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> acadFolder =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\Program Files\\Autodesk\\AutoCAD 2008\\&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// EvalCS: Evaluates C# source</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">object</span> EvalCS(<span style="COLOR: blue">string</span> csCode)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DocumentCollection</span> dm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = dm.MdiActiveDocument.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">CSharpCodeProvider</span> cs = <span style="COLOR: blue">new</span> <span style="COLOR: teal">CSharpCodeProvider</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">CompilerParameters</span> cp = <span style="COLOR: blue">new</span> <span style="COLOR: teal">CompilerParameters</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(<span style="COLOR: maroon">&quot;system.dll&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.CompilerOptions = <span style="COLOR: maroon">&quot;/t:library&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.GenerateInMemory = <span style="COLOR: blue">true</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">StringBuilder</span> sb = <span style="COLOR: blue">new</span> <span style="COLOR: teal">StringBuilder</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using System;\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.Runtime;\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.ApplicationServices;\n&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.DatabaseServices;\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.EditorInput;\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.Geometry;\n&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;namespace CSCodeEval{\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;public class CSCodeEval{\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;public object EvalCode(){\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;return &quot;</span> + csCode + <span style="COLOR: maroon">&quot;;\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;}\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;}\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;}\n&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">CompilerResults</span> cr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cs.CompileAssemblyFromSource(cp, sb.ToString());</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (cr.Errors.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nErrors evaluating C# code (&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cr.Errors.Count +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;):&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; cr.Errors.Count; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;\nLine number &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors[i].Line + <span style="COLOR: maroon">&quot;: &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors[i].ErrorText</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.Reflection.<span style="COLOR: teal">Assembly</span> a =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cr.CompiledAssembly;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> o =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; a.CreateInstance(<span style="COLOR: maroon">&quot;CSCodeEval.CSCodeEval&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Type</span> t = o.GetType();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">MethodInfo</span> mi = t.GetMethod(<span style="COLOR: maroon">&quot;EvalCode&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> s = mi.Invoke(o, <span style="COLOR: blue">null</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> s;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// EvalVB: Evaluates VB source</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">object</span> EvalVB(<span style="COLOR: blue">string</span> vbCode)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DocumentCollection</span> dm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = dm.MdiActiveDocument.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">VBCodeProvider</span> vb = <span style="COLOR: blue">new</span> <span style="COLOR: teal">VBCodeProvider</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">CompilerParameters</span> cp = <span style="COLOR: blue">new</span> <span style="COLOR: teal">CompilerParameters</span>();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(<span style="COLOR: maroon">&quot;system.dll&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.CompilerOptions = <span style="COLOR: maroon">&quot;/t:library&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.GenerateInMemory = <span style="COLOR: blue">true</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">StringBuilder</span> sb = <span style="COLOR: blue">new</span> <span style="COLOR: teal">StringBuilder</span>();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports System\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.Runtime\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.ApplicationServices\n&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.DatabaseServices\n&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.EditorInput\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.Geometry\n&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Namespace VBCodeEval\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Public Class VBCodeEval\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Public Function EvalCode() As Object\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Return &quot;</span> + vbCode + <span style="COLOR: maroon">&quot; \n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;End Function\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;End Class\n&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;End Namespace\n&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">CompilerResults</span> cr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vb.CompileAssemblyFromSource(cp, sb.ToString());</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (cr.Errors.Count &gt; 0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nErrors evaluating VB code (&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cr.Errors.Count +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;):&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> (<span style="COLOR: blue">int</span> i = 0; i &lt; cr.Errors.Count; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;\nLine number &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors[i].Line + <span style="COLOR: maroon">&quot;: &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors[i].ErrorText</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;System.Reflection.<span style="COLOR: teal">Assembly</span> a =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cr.CompiledAssembly;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> o =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; a.CreateInstance(<span style="COLOR: maroon">&quot;VBCodeEval.VBCodeEval&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Type</span> t = o.GetType();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">MethodInfo</span> mi = t.GetMethod(<span style="COLOR: maroon">&quot;EvalCode&quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> s = mi.Invoke(o, <span style="COLOR: blue">null</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> s;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;EV&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Eval()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DocumentCollection</span> dm =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = dm.MdiActiveDocument.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> csCode =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;typeof(Autodesk.AutoCAD.&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;ApplicationServices.Application)&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> vbCode =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;GetType(Autodesk.AutoCAD.&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;ApplicationServices.Application)&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nEvaluating C# code:\n&quot;</span> + csCode);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">object</span> result = EvalCS(csCode);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (result != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nC# code returned: &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; result.ToString()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nEvaluating VB code:\n&quot;</span> + vbCode);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;result = EvalVB(vbCode);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (result != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;\nVB code returned: &quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; result.ToString()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Here's the VB.NET code, which shows how to compile and execute C# (heterogeneous) and VB.NET (homogeneous) code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Microsoft.CSharp</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Microsoft.VisualBasic</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System.CodeDom.Compiler</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System.Reflection</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System.Text</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Namespace</span> Metaprogramming</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> Commands</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> <span style="COLOR: blue">Const</span> acadFolder <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;c:\\Program Files\\Autodesk\\AutoCAD 2008\\&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">'EvalCS: Evaluates C# source</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Shared</span> <span style="COLOR: blue">Function</span> EvalCS(<span style="COLOR: blue">ByVal</span> csCode <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span>) _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> dm <span style="COLOR: blue">As</span> DocumentCollection = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Application.DocumentManager</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = dm.MdiActiveDocument.Editor</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> cs <span style="COLOR: blue">As</span> CSharpCodeProvider = <span style="COLOR: blue">New</span> CSharpCodeProvider</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> cp <span style="COLOR: blue">As</span> CompilerParameters = <span style="COLOR: blue">New</span> CompilerParameters</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(<span style="COLOR: maroon">&quot;system.dll&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.CompilerOptions = <span style="COLOR: maroon">&quot;/t:library&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.GenerateInMemory = <span style="COLOR: blue">True</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> sb <span style="COLOR: blue">As</span> StringBuilder = <span style="COLOR: blue">New</span> StringBuilder</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using System;&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.Runtime;&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.ApplicationServices;&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.DatabaseServices;&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.EditorInput;&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;using Autodesk.AutoCAD.Geometry;&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;namespace CSCodeEval{&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;public class CSCodeEval{&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;public object EvalCode(){&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;return &quot;</span> + csCode + <span style="COLOR: maroon">&quot;;&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;}&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;}&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;}&quot;</span> &amp; vbLf)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> cr <span style="COLOR: blue">As</span> CompilerResults = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cs.CompileAssemblyFromSource(cp, sb.ToString)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> (cr.Errors.Count &gt; 0) <span style="COLOR: blue">Then</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vbLf &amp; <span style="COLOR: maroon">&quot;Errors evaluating C# code (&quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cr.Errors.Count.ToString + <span style="COLOR: maroon">&quot;):&quot;</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Dim</span> i <span style="COLOR: blue">As</span> <span style="COLOR: blue">Integer</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">For</span> i = 0 <span style="COLOR: blue">To</span> cr.Errors.Count - 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;vbLf &amp; <span style="COLOR: maroon">&quot;Line number &quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors(i).Line.ToString + <span style="COLOR: maroon">&quot;: &quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors(i).ErrorText)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Next</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Return</span> <span style="COLOR: blue">Nothing</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> a <span style="COLOR: blue">As</span> System.Reflection.Assembly = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cr.CompiledAssembly</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> o <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span> = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; a.CreateInstance(<span style="COLOR: maroon">&quot;CSCodeEval.CSCodeEval&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> t <span style="COLOR: blue">As</span> Type = o.GetType</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> mi <span style="COLOR: blue">As</span> MethodInfo = t.GetMethod(<span style="COLOR: maroon">&quot;EvalCode&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> s <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span> = mi.Invoke(o, <span style="COLOR: blue">Nothing</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Return</span> s</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Function</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">'EvalVB: Evaluates VB source</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Shared</span> <span style="COLOR: blue">Function</span> EvalVB(<span style="COLOR: blue">ByVal</span> vbCode <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span>) _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> dm <span style="COLOR: blue">As</span> DocumentCollection = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Application.DocumentManager</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = dm.MdiActiveDocument.Editor</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> vb <span style="COLOR: blue">As</span> VBCodeProvider = <span style="COLOR: blue">New</span> VBCodeProvider</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> cp <span style="COLOR: blue">As</span> CompilerParameters = <span style="COLOR: blue">New</span> CompilerParameters</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(<span style="COLOR: maroon">&quot;system.dll&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.ReferencedAssemblies.Add(acadFolder + <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.CompilerOptions = <span style="COLOR: maroon">&quot;/t:library&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;cp.GenerateInMemory = <span style="COLOR: blue">True</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> sb <span style="COLOR: blue">As</span> StringBuilder = <span style="COLOR: blue">New</span> StringBuilder</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports System&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.Runtime&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.ApplicationServices&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.DatabaseServices&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.EditorInput&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Imports Autodesk.AutoCAD.Geometry&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Namespace VBCodeEval&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Public Class VBCodeEval&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Public Function EvalCode() As Object&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;Return &quot;</span> + vbCode + <span style="COLOR: maroon">&quot; &quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;End Function&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;End Class&quot;</span> &amp; vbLf)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;sb.Append(<span style="COLOR: maroon">&quot;End Namespace&quot;</span> &amp; vbLf)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> cr <span style="COLOR: blue">As</span> CompilerResults = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vb.CompileAssemblyFromSource(cp, sb.ToString)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> (cr.Errors.Count &gt; 0) <span style="COLOR: blue">Then</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vbLf &amp; <span style="COLOR: maroon">&quot;Errors evaluating VB code (&quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; cr.Errors.Count.ToString + <span style="COLOR: maroon">&quot;):&quot;</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Dim</span> i <span style="COLOR: blue">As</span> <span style="COLOR: blue">Integer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">For</span> i = 0 <span style="COLOR: blue">To</span> cr.Errors.Count - 1</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;vbLf &amp; <span style="COLOR: maroon">&quot;Line number &quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors(i).Line.ToString + <span style="COLOR: maroon">&quot;: &quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;cr.Errors(i).ErrorText)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Next</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Return</span> <span style="COLOR: blue">Nothing</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> a <span style="COLOR: blue">As</span> System.Reflection.Assembly = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; cr.CompiledAssembly</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> o <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span> = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; a.CreateInstance(<span style="COLOR: maroon">&quot;VBCodeEval.VBCodeEval&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> t <span style="COLOR: blue">As</span> Type = o.GetType</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> mi <span style="COLOR: blue">As</span> MethodInfo = t.GetMethod(<span style="COLOR: maroon">&quot;EvalCode&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> s <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span> = mi.Invoke(o, <span style="COLOR: blue">Nothing</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Return</span> s</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Function</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;EV&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Eval()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> dm <span style="COLOR: blue">As</span> DocumentCollection = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Application.DocumentManager</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = dm.MdiActiveDocument.Editor</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Const</span> csCode <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;typeof(Autodesk.AutoCAD.&quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;ApplicationServices.Application)&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Const</span> vbCode <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;GetType(Autodesk.AutoCAD.&quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;ApplicationServices.Application)&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vbLf + <span style="COLOR: maroon">&quot;Evaluating C# code:&quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vbLf + csCode)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> result <span style="COLOR: blue">As</span> <span style="COLOR: blue">Object</span> = EvalCS(csCode)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> (<span style="COLOR: blue">Not</span> result <span style="COLOR: blue">Is</span> <span style="COLOR: blue">Nothing</span>) <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vbLf + <span style="COLOR: maroon">&quot;C# code returned: &quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; result.ToString)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vbLf + <span style="COLOR: maroon">&quot;Evaluating VB code:&quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; vbLf + vbCode)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;result = EvalVB(vbCode)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">If</span> (<span style="COLOR: blue">Not</span> result <span style="COLOR: blue">Is</span> <span style="COLOR: blue">Nothing</span>) <span style="COLOR: blue">Then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; vbLf + <span style="COLOR: maroon">&quot;VB code returned: &quot;</span> + _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; result.ToString)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Namespace</span></p></div>

<p>When we run the &quot;ev&quot; command, implemented by either of the above code fragments, we see these results:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">ev</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Evaluating C# code:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">typeof(Autodesk.AutoCAD.ApplicationServices.Application)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">C# code returned: Autodesk.AutoCAD.ApplicationServices.Application</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Evaluating VB code:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">GetType(Autodesk.AutoCAD.ApplicationServices.Application)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">VB code returned: Autodesk.AutoCAD.ApplicationServices.Application</p></div>
