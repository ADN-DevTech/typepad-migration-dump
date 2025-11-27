---
layout: "post"
title: "Showing AutoCAD's hatch dialog from a .NET application"
date: "2007-03-16 11:45:47"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Hatches"
  - "User interface"
original_url: "https://www.keanw.com/2007/03/showing_autocad.html "
typepad_basename: "showing_autocad"
typepad_status: "Publish"
---

<p>This question was posted by csharpbird:</p>
<blockquote dir="ltr">
<p><em>How to get the Hatch dialog using .NET? It seems that there is no such class in the .NET API?</em></p></blockquote>
<p>It&#39;s true there is no public class - or even a published function - to show the hatch dialog inside AutoCAD. It is, however, possible to P/Invoke an unpublished (and therefore unsupported and liable to change without warning) function exported from acad.exe.</p>
<p>Here&#39;s some C# code that shows how. The code works for AutoCAD 2007, but will be different for AutoCAD 2006: the function takes and outputs strings, so the change to Unicode in 2007 will have modified the function signature.</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> System.Reflection;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> System.Runtime.InteropServices;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">namespace</span> HatchDialogTest</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; [<span style="COLOR: teal">DllImport</span>(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: maroon">&quot;acad.exe&quot;</span>,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; EntryPoint =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: maroon">&quot;?acedHatchPalletteDialog@@YA_NPB_W_NAAPA_W@Z&quot;</span>,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; CharSet = <span style="COLOR: teal">CharSet</span>.Auto</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; ]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">static</span> <span style="COLOR: blue">extern</span> <span style="COLOR: blue">bool</span> acedHatchPalletteDialog(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">string</span> currentPattern,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">bool</span> showcustom,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">out</span> <span style="COLOR: blue">string</span> newpattern</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;SHD&quot;</span>)]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ShowHatchDialog()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">string</span> sHatchType;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">string</span> sNewHatchType;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">bool</span> bRet;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;sHatchType = <span style="COLOR: maroon">&quot;ANGLE&quot;</span>;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;bRet =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; acedHatchPalletteDialog(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sHatchType,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">true</span>,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">out</span> sNewHatchType</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (bRet)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">Editor</span> ed =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: maroon">&quot;\nHatch type selected: &quot;</span> + sNewHatchType</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">}</p></div>
<p>Here&#39;s what happens when you run the code:</p>
<p style="MARGIN: 0px; FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">Command: <span style="COLOR: red">SHD</span></p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt"></div><br />
<p style="MARGIN: 0px; FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt"><a href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/03/16/hatch_dialog.png" onclick="window.open(this.href, &#39;_blank&#39;, &#39;width=380,height=444,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false"><img alt="Hatch_dialog" border="0" height="350" src="/assets/hatch_dialog.png" title="Hatch_dialog" width="300" /></a> </p><br />
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt"></div>
<p style="MARGIN: 0px; FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">Hatch type selected: SWAMP</p>
<p><strong>Update</strong></p>
<p>Someone asked me for the VB.NET code for this one (it was quite tricky to marshall the new string being returned). As I&#39;d put it together, I thought I&#39;d post it here:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt"><span style="COLOR: blue">
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.EditorInput</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">Imports</span> System.Text</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">Namespace</span> HatchDialogTest</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> Commands</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">Private</span> <span style="COLOR: blue">Declare</span> <span style="COLOR: blue">Auto</span> <span style="COLOR: blue">Function</span> acedHatchPalletteDialog _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">Lib</span> <span style="COLOR: maroon">&quot;acad.exe&quot;</span> _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">Alias</span> <span style="COLOR: maroon">&quot;?acedHatchPalletteDialog@@YA_NPB_W_NAAPA_W@Z&quot;</span> _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; (<span style="COLOR: blue">ByVal</span> currentPattern <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span>, _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">ByVal</span> showcustom <span style="COLOR: blue">As</span> <span style="COLOR: blue">Boolean</span>, _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">ByRef</span> newpattern <span style="COLOR: blue">As</span> StringBuilder) <span style="COLOR: blue">As</span> <span style="COLOR: blue">Boolean</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; &lt;CommandMethod(<span style="COLOR: maroon">&quot;SHD&quot;</span>)&gt; _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> ShowHatchDialog()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">Dim</span> sHatchType <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span> = <span style="COLOR: maroon">&quot;ANGLE&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">Dim</span> sNewHatchType <span style="COLOR: blue">As</span> <span style="COLOR: blue">New</span> StringBuilder</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">Dim</span> bRet <span style="COLOR: blue">As</span> <span style="COLOR: blue">Boolean</span> = _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; acedHatchPalletteDialog(sHatchType, _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">True</span>, sNewHatchType)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">If</span> bRet <span style="COLOR: blue">And</span> sNewHatchType.ToString.Length &gt; 0 <span style="COLOR: blue">Then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed = _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; Application.DocumentManager.MdiActiveDocument.Editor</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed.WriteMessage( _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; vbLf + <span style="COLOR: maroon">&quot;Hatch type selected: &quot;</span> + _</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sNewHatchType.ToString)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">End</span> <span style="COLOR: blue">If</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Namespace</span></p></div></span></div><br />
<p><strong>Update 2</strong></p>
<p>I&#39;ve recently come back to this post at the prompting of a colleague who was struggling to get it working with AutoCAD 2010. Sure enough, the dialog would appear but the marshalling back of the return string to AutoCAD is now causing a problem, for some unknown reason (at least it&#39;s unknown to me :-).</p>
<p>Anyway - to address the issue I&#39;ve updated the code to perform the string marshalling a little more explicitly, and it now works. This may also address the issue one of the people commenting on this post experienced trying to get the code to work with AutoCAD 2009 (although I do remember testing it there and having no issues, which has me scratching my head somewhat).</p>
<p>Here&#39;s the updated C# code:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Runtime.InteropServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> HatchDialogTest</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">DllImport</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;acad.exe&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; EntryPoint =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;?acedHatchPalletteDialog@@YA_NPB_W_NAAPA_W@Z&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; CharSet = </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CharSet</span><span style="LINE-HEIGHT: 140%">.Auto</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; )</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; ]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">extern</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> acedHatchPalletteDialog(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> currentPattern,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> showcustom,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IntPtr</span><span style="LINE-HEIGHT: 140%"> newpattern</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;SHD&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> ShowHatchDialog()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> sHatchType = </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;ANGLE&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">IntPtr</span><span style="LINE-HEIGHT: 140%"> ptr;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> bRet =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; acedHatchPalletteDialog(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; sHatchType,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">out</span><span style="LINE-HEIGHT: 140%"> ptr</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (bRet)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> sNewHatchType = </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Marshal</span><span style="LINE-HEIGHT: 140%">.PtrToStringAuto(ptr);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (sNewHatchType.Length &gt; 0)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Editor</span><span style="LINE-HEIGHT: 140%"> ed =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;\nHatch type selected: &quot;</span><span style="LINE-HEIGHT: 140%"> + sNewHatchType</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>And here&#39;s the updated VB code:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">Imports</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">Imports</span><span style="LINE-HEIGHT: 140%"> System.Runtime.InteropServices</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">Imports</span><span style="LINE-HEIGHT: 140%"> System</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">Namespace</span><span style="LINE-HEIGHT: 140%"> HatchDialogTest</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Class</span><span style="LINE-HEIGHT: 140%"> Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Declare</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Auto</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Function</span><span style="LINE-HEIGHT: 140%"> acedHatchPalletteDialog _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Lib</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;acad.exe&quot;</span><span style="LINE-HEIGHT: 140%"> _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Alias</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;?acedHatchPalletteDialog@@YA_NPB_W_NAAPA_W@Z&quot;</span><span style="LINE-HEIGHT: 140%"> _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">ByVal</span><span style="LINE-HEIGHT: 140%"> currentPattern </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">String</span><span style="LINE-HEIGHT: 140%">, _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">ByVal</span><span style="LINE-HEIGHT: 140%"> showcustom </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Boolean</span><span style="LINE-HEIGHT: 140%">, _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">ByRef</span><span style="LINE-HEIGHT: 140%"> newpattern </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> IntPtr) </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Boolean</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &lt;CommandMethod(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;SHD&quot;</span><span style="LINE-HEIGHT: 140%">)&gt; _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Sub</span><span style="LINE-HEIGHT: 140%"> ShowHatchDialog()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Dim</span><span style="LINE-HEIGHT: 140%"> sHatchType </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">String</span><span style="LINE-HEIGHT: 140%"> = </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;ANGLE&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Dim</span><span style="LINE-HEIGHT: 140%"> ptr </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> IntPtr</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Dim</span><span style="LINE-HEIGHT: 140%"> bRet </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Boolean</span><span style="LINE-HEIGHT: 140%"> = _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; acedHatchPalletteDialog(sHatchType, _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">True</span><span style="LINE-HEIGHT: 140%">, ptr)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">If</span><span style="LINE-HEIGHT: 140%"> bRet </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Dim</span><span style="LINE-HEIGHT: 140%"> sNewHatchType </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">String</span><span style="LINE-HEIGHT: 140%"> = _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; Marshal.PtrToStringAuto(ptr)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">If</span><span style="LINE-HEIGHT: 140%"> sNewHatchType.ToString.Length &gt; 0 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Dim</span><span style="LINE-HEIGHT: 140%"> ed </span><span style="LINE-HEIGHT: 140%; COLOR: blue">As</span><span style="LINE-HEIGHT: 140%"> Editor</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ed = _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Application.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage( _</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; vbLf + </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Hatch type selected: &quot;</span><span style="LINE-HEIGHT: 140%"> + sNewHatchType)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">If</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">If</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Sub</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Class</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">End</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">Namespace</span></p></div>
