---
layout: "post"
title: "CultureInfoChanger and IronPython3"
date: "2023-01-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Batch"
  - "Family"
  - "Journal"
  - "Python"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/01/cultureinfochanger-and-ironpython3.html "
typepad_basename: "cultureinfochanger-and-ironpython3"
typepad_status: "Publish"
---

<p>Happy New Year 2023!</p>

<p>Topics to begin the new year:</p>

<ul>
<li><a href="#2">Copy as HTML 2022</a></li>
<li><a href="#3">Size MEP connector with <code>CultureInfoChanger</code></a></li>
<li><a href="#4">Journal files AU class</a></li>
<li><a href="#5">Bulk reload families with <code>IFamilyLoadOptions</code></a></li>
<li><a href="#6">IronPython3, APS and RPS</a></li>
<li><a href="#6.1">IronPython3 in pyRevit</a></li>
</ul>

<h4><a name="2"></a> Copy as HTML 2022</h4>

<p>After <a href="https://thebuildingcoder.typepad.com/blog/2022/12/exploring-arm-chatgpt-nairobi-and-the-tsp.html#11">moving to the new Mac and switching to Visual Studio 2022</a>,
I also needed to update my C# source code colouriser.</p>

<p>The last time was the
<a href="https://thebuildingcoder.typepad.com/blog/2021/11/revit-20221-sdk-revitlookup-build-and-install.html#7">Copy as HTML update</a> in November 2021 using the Productivity Power Tools 2017/2019.</p>

<p>This time around, I switched to a new extension, Copy as HTML 2022 by Tim Mathias:</p>

<blockquote>
  <p>Copy selected code in HTML format while preserving syntax highlighting, indentation, background colour and font.
  Options: Max Height, Title Block, Alternating Lines, Line Numbers, Wrap Lines, Un-indent, Background Colour, Class Names.
  Converts RTF, outputted by VS, into HTML.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302af148b88dc200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302af148b88dc200c img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Copy As HTML 2022" title="Copy As HTML 2022" src="/assets/image_22baa4.jpg" /></a><br /></p>

<p></center></p>

<p>You can see the results of using the new code colouriser immediately below to format the <code>CultureInfoChanger</code> sample.</p>

<p>I also updated <a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples</a> for VS 2022, in
<a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2023.1.153.5">release 2023.1.153.5</a>.</p>

<h4><a name="3"></a> Size MEP Connector with CultureInfoChanger</h4>

<p>Luiz Henrique <a href="https://github.com/ricaun">@ricaun</a> Cassettari implemented a nice workaround to solve the problem raised in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
being <a href="https://forums.autodesk.com/t5/revit-api-forum/unable-to-size-mep-connectors-with-revit-2021/m-p/9609260">unable to size MEP connectors with Revit 2021</a>.</p>

<p><i>Ekaterina.kudelina.beam</i> noticed that switching the <code>CurrentCulture</code> from <code>de-DE</code> to <code>en-EN</code> makes it possible to change connector size in a project. <i>Ricaun</i> made use of this observation to implement a <code>CultureInfoChanger</code> derived from <code>IDisposable</code> that can be used to wrap the setting, temporarily changing <code>CultureInfo</code> to English and resetting it back to the original when disposed:</p>

<!--

/// <summary>
/// CultureInfoChanger
/// </summary>
public class CultureInfoChanger : IDisposable
{
    private readonly CultureInfo CultureInfo;

    /// <summary>
    /// CultureInfoChanger
    /// </summary>
    /// <param name="name"></param>
    public CultureInfoChanger(string name = "en")
    {
        CultureInfo = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo(name);
    }
    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        CultureInfo.CurrentCulture = CultureInfo;
    }
}

-->

<div style="border: #000080 1px solid; color: #000; font-family: 'Cascadia Mono', Consolas, 'Courier New', Courier, Monospace; font-size: 10pt">
<div style="background-color: #ffffff; color: #000000; max-height: 300px; overflow: auto; padding: 2px 5px;"><span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;summary&gt;</span><br>
<span style="color:#808080">///</span><span style="color:#008000"> CultureInfoChanger</span><br>
<span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;/summary&gt;</span><br>
<span style="color:#0000ff">public</span> <span style="color:#0000ff">class</span> <span style="color:#2b91af">CultureInfoChanger</span> : IDisposable<br>
{<br>
&#160; <span style="color:#0000ff">private</span> <span style="color:#0000ff">readonly</span> CultureInfo CultureInfo;<br>
<br>
&#160; <span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;summary&gt;</span><br>
&#160; <span style="color:#808080">///</span><span style="color:#008000"> CultureInfoChanger</span><br>
&#160; <span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;/summary&gt;</span><br>
&#160; <span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;param</span> <span style="color:#808080">name=&quot;</span>name<span style="color:#808080">&quot;&gt;&lt;/param&gt;</span><br>
&#160; <span style="color:#0000ff">public</span> <span style="color:#2b91af">CultureInfoChanger</span>(<span style="color:#0000ff">string</span> name = <span style="color:#a31515">&quot;en&quot;</span>)<br>
&#160; {<br>
&#160;&#160;&#160; CultureInfo = CultureInfo.CurrentCulture;<br>
&#160;&#160;&#160; CultureInfo.CurrentCulture = <span style="color:#0000ff">new</span> CultureInfo(name);<br>
&#160; }<br>
&#160; <span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;summary&gt;</span><br>
&#160; <span style="color:#808080">///</span><span style="color:#008000"> Dispose</span><br>
&#160; <span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;/summary&gt;</span><br>
&#160; <span style="color:#0000ff">public</span> <span style="color:#0000ff">void</span> Dispose()<br>
&#160; {<br>
&#160;&#160;&#160; CultureInfo.CurrentCulture = CultureInfo;<br>
&#160; }<br>
}</div>
</div>

<p>The code to use it could be something like this.</p>

<!--



0adce0a6babb823a4587f5ab59d64cd9



-->

<div style="border: #000080 1px solid; color: #000; font-family: 'Cascadia Mono', Consolas, 'Courier New', Courier, Monospace; font-size: 10pt">
<div style="background-color: #ffffff; color: #000000; max-height: 300px; overflow: auto; padding: 2px 5px;"><span style="color:#0000ff">using</span> (<span style="color:#0000ff">new</span> CultureInfoChanger())<br>
{<br>
&#160; connector.Radius = 0.5;<br>
}</div>
</div>

<p>An extension should be perfect in this case, some <code>SetRadius</code>, <code>SetWidth</code>, and <code>SetHeight</code>.</p>

<!--



654e4163bb40b15b464dbbee42652a15



-->

<div style="border: #000080 1px solid; color: #000; font-family: 'Cascadia Mono', Consolas, 'Courier New', Courier, Monospace; font-size: 10pt">
<div style="background-color: #ffffff; color: #000000; max-height: 300px; overflow: auto; padding: 2px 5px;">
&#160; connector.SetRadius(0.5);<br>
&#160; connector.SetHeight(0.5);<br>
&#160; connector.SetWidth(0.5);<br>
<br>
<span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;summary&gt;</span><br>
<span style="color:#808080">///</span><span style="color:#008000"> Set the radius of the connector. </span><br>
<span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;/summary&gt;</span><br>
<span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;param</span> <span style="color:#808080">name=&quot;</span>connector<span style="color:#808080">&quot;&gt;&lt;/param&gt;</span><br>
<span style="color:#808080">///</span><span style="color:#008000"> </span><span style="color:#808080">&lt;param</span> <span style="color:#808080">name=&quot;</span>radius<span style="color:#808080">&quot;&gt;&lt;/param&gt;</span><br>
<span style="color:#0000ff">public</span> <span style="color:#0000ff">static</span> <span style="color:#0000ff">void</span> SetRadius(<span style="color:#0000ff">this</span> Connector connector, <span style="color:#0000ff">double</span> radius)<br>
{<br>
&#160; <span style="color:#0000ff">using</span> (<span style="color:#0000ff">new</span> CultureInfoChanger())<br>
&#160; {<br>
&#160;&#160;&#160; connector.Radius = radius;<br>
&#160; }<br>
}</div>
</div>

<p>The source code for the full extension with a command sample is provided in
<a href="https://gist.github.com/ricaun/693470e914295786fa62a2be6c67e662">Ricaun's ConnectorSetValueExtension.cs gist</a>:</p>

<blockquote>
  <p>Connector Set Value Extension for Revit to 'fix' the ArgumentOutOfRangeException when setting Radius, Width, and Height.</p>
</blockquote>

<p>Many thanks to Luiz Henrique for this nice solution!</p>

<h4><a name="4"></a> Journal Files AU Class</h4>

<p>Luiz Henrique also pointed out an interesting AU class on journal files in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/journal-step-by-step/m-p/11625744">journal step by step</a>:</p>

<p><strong>Question:</strong> Any idea how to run a journal file step by step to debug a problem running it?</p>

<p><strong>Answer:</strong> Nope. It is all or nothing.
For more info, please check The Building Coder articles in the <a href="https://thebuildingcoder.typepad.com/blog/journal">Journal category</a>.</p>

<p>You could look at the Autodesk University class 
on <a href="https://www.autodesk.com/autodesk-university/class/Revit-Journal-Files-They-Arent-Just-Autodesk-Support-2018#video">Revit Journal files: they aren’t just for Autodesk support</a>.</p>

<h4><a name="5"></a> Bulk Reload Families with IFamilyLoadOptions</h4>

<p>Before Christmas, I had a nice and fruitful conversation
with <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/10364294">@Eatrevitpoopcad</a>
on <a href="https://forums.autodesk.com/t5/revit-api-forum/bulk-reloading-families-in-a-template-from-a-slightly-different/td-p/11623721">bulk reloading families in a template from a slightly different location</a>,
helping to get started working on a macro to automate the execution of <code>LoadFamily</code> with an <code>IFamilyLoadOptions</code> handler and saving a large amount of manual labour.</p>

<h4><a name="6"></a> IronPython3, APS and RPS</h4>

<p>Chuong Ho shared some exciting news on <a href="https://ironpython.net">IronPython3</a>, APS and RPS.</p>

<p>RPS is the beloved <a href="ttps://github.com/architecture-building-systems/revitpythonshell">RevitPythonShell</a> that
adds an IronPython interpreter to Revit and lets you to write plugins for Revit in Python.
Even better, it provides you with an interactive shell that lets you see the results of your code as you type it.
This is great for exploring the Revit API while writing your Revit Addins, especially in combination with
the <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup database exploration tool</a>.</p>

<p>APS is the <a href="https://github.com/chuongmep/CadPythonShell">CADPythonShell</a>, a fork of RevitPythonShell, bringing an IronPython interpreter to AutoCAD as well.</p>

<p>Chuong Ho <a href="https://www.linkedin.com/posts/chuongmep_bim-python-ironpython3-activity-7009453448463077377-cQtY?utm_source=share&amp;utm_medium=member_desktop">announced</a> the
advent of IronPython 3.4:</p>

<blockquote>
  <p>I'm very excited because last week the IronPython3 team released 3.4.0.
  Today, I also quickly brought them to CadPythonShell and RevitPythonShell, which is a great expectation of Python-friendly engineers. Python 3.4 is a big upgrade for engineers to get the most out of the new features that Python brings.</p>
</blockquote>

<p>The RevitPythonShell enhancement was submitted, discussed and merged in
the <a href="https://github.com/architecture-building-systems/revitpythonshell/pull/136">PR 136 &ndash; support IronPython3.4</a>.</p>

<p>Many thanks to Chuong Ho for implementing this!</p>

<h4><a name="6.1"></a> IronPython3 in pyRevit</h4>

<p>Ehsan Iran Nejad added in his <a href="https://thebuildingcoder.typepad.com/blog/2023/01/cultureinfochanger-and-ironpython3.html#comment-6080745754">comment below</a>:</p>

<blockquote>
  <p>We added <a href="https://discourse.pyrevitlabs.io/t/ironpython-3-4-0/1310">IronPython3 to pyRevit work-in-progress</a> as well...</p>
</blockquote>
