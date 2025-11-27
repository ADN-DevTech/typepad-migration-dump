---
layout: "post"
title: "Searching the web from inside AutoCAD using the Bing API and .NET"
date: "2009-09-18 16:23:39"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "JSON"
  - "LINQ"
  - "REST"
original_url: "https://www.keanw.com/2009/09/searching-the-web-from-inside-autocad-using-the-bing-api-and-net.html "
typepad_basename: "searching-the-web-from-inside-autocad-using-the-bing-api-and-net"
typepad_status: "Publish"
---

<p>I know, I know... <a href="http://through-the-interface.typepad.com/through_the_interface/2009/09/jigging-an-autocad-solid-using-ironruby-and-net-yes-finally.html">I said I’d be posting on IronRuby</a>, but yet again I got distracted &lt;sigh&gt;. Back to that next week, I promise.</p>
<p>The good news is that, once again, I managed to get distracted by something pretty cool. :-)</p>
<p>Earlier in the week I’d stumbled across <a href="http://blogs.techrepublic.com.com/programming-and-development/?p=1713&amp;tag=nl.e101">an article</a> mentioning the <a href="http://www.bing.com/developer">Bing API</a> (currently in version 2.0), which allows you to perform programmatic web searches using various web-service related technologies, such as <a href="http://en.wikipedia.org/wiki/Representational_State_Transfer">REST</a>, <a href="http://en.wikipedia.org/wiki/JSON">JSON</a> &amp; <a href="http://en.wikipedia.org/wiki/SOAP">SOAP</a>. I played around with that, for a while, importing the Bing web service into different versions of Visual Studio (both of which gave me the same run-time error, once I actually tried to execute them), and then decided to look for an alternative approach.</p>
<p>That was when I came across <a href="http://www.nikhilk.net/BLinq-LINQ-over-Bing.aspx">this post</a>, introducing an implementation of a <a href="http://en.wikipedia.org/wiki/Language_Integrated_Query">LINQ</a> provider for Bing called BLinq (love that name! :-). The <a href="http://www.nikhilk.net/Content/Posts/BLinq/BLinq.zip">BLinq implementation</a> posted there takes away the heavy lifting of working with Bing – I believe it uses REST under-the-hood, but to be honest I don’t even need to know that - and allows you to take advantage of the elegant syntax of LINQ.</p>
<p>I’ve threatened to talk about LINQ before, but this is the first time I’ve really used it. Anyone familiar with <a href="http://en.wikipedia.org/wiki/SQL">SQL</a> should find LINQ straightforward: it basically integrates the ability to query various data-sources into your favourite .NET language (hence Language INtegrated Query). I haven’t looked into the BLinq implementation very deeply: I simply rebuilt the BLinq module, referenced it from an AutoCAD .NET project created using <a href="http://through-the-interface.typepad.com/through_the_interface/2009/09/updated-versions-of-the-autocad-net-wizard-labs.html">the new Wizard</a> and integrated the code from the test project, in Program.cs.</p>
<p>A few comments about the various steps needed to get this working:</p>
<ul>
<li>Getting an Bing AppId  
<ul>
<li>You will need to submit <a href="http://www.bing.com/developers/createapp.aspx">an online request</a> for your own Bing AppId </li>
<li>You can replace the “TODO: INSERT YOUR APPID HERE” string below with this ID </li>
<li>Mine was a 40-character hex string, presumably yours will be, too :-) </li>
</ul>
</li>
<li>Rebuilding BLinq  
<ul>
<li>You will need to use Visual Studio 2008 or higher (I’m using VS 2008 SP1) using at least the .NET Framework 3.5 </li>
<li>I ended up downloading and installing the <a href="http://www.microsoft.com/downloads/details.aspx?familyid=1EA49236-0DE7-41B1-81C8-A126FF39975B&amp;displaylang=en">Silverlight 3.0 SDK</a> to get the right version of the System.ComponentModel.DataAnnotations assembly  
<ul>
<li>If you find certain errors such as DisplayAttribute, EditableAttribute and KeyAttribute undefined, you will probably also need to do this </li>
<li>I added the assembly by browsing to (on my system) <em>C:\Program Files\Microsoft SDKs\Silverlight\v3.0\Libraries\Client</em> </li>
</ul>
</li>
</ul>
</li>
<li>Building your client application  
<ul>
<li>As mentioned earlier, I did this using <a href="http://images.autodesk.com/adsk/files/autocad_2010_dotnet_wizards.zip">the AutoCAD 2010 .NET Wizard</a> </li>
<li>I added an assembly reference to the BLinq DLL, but that was the only one needed&#0160;  
<ul>
<li>The default project includes references to a number of (probably) significant assemblies, such as System.Xml.Linq </li>
</ul>
</li>
</ul>
</li>
</ul>
<p>We’re going to use BLinq to implement a command named BING which asks the user for a query string and then uses it to perform three searches using Bing: a standard web search, a local search in the area of San Rafael, California, and an image search. The results then get displayed at the AutoCAD command-line.</p>
<p>Here’s the C# code I used to implement our BING command:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Linq;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> BLinq;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> BLinqInAutoCAD</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> _ed = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> WriteImages(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> query,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #2b91af;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">ImageSearchResult</span><span style="line-height: 140%;">&gt; images</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nImage Query: &quot;</span><span style="line-height: 140%;"> + query);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;"> + </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&#39;=&#39;</span><span style="line-height: 140%;">, 80));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">ImageSearchResult</span><span style="line-height: 140%;"> image </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> images)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nTitle:&#0160;&#0160;&#0160; &quot;</span><span style="line-height: 140%;"> + image.Title</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nUri:	&#0160; &quot;</span><span style="line-height: 140%;"> + image.Uri.AbsoluteUri</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nSize:	&#0160; &quot;</span><span style="line-height: 140%;"> + image.Width + </span><span style="line-height: 140%; color: #a31515;">&quot; x &quot;</span><span style="line-height: 140%;"> + image.Height</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nThumbnail: &quot;</span><span style="line-height: 140%;"> + image.ThumbnailUri.AbsoluteUri</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> WritePages(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> query,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #2b91af;">IEnumerable</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">PageSearchResult</span><span style="line-height: 140%;">&gt; pages</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nPage Query: &quot;</span><span style="line-height: 140%;"> + query);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;"> + </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&#39;=&#39;</span><span style="line-height: 140%;">, 80));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">PageSearchResult</span><span style="line-height: 140%;"> page </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> pages)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nTitle:&#0160; &quot;</span><span style="line-height: 140%;"> + page.Title);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nUri:&#0160;&#0160;&#0160; &quot;</span><span style="line-height: 140%;"> + page.Uri.AbsoluteUri);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nDisplay: &quot;</span><span style="line-height: 140%;"> + page.DisplayUrl);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nDescription&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;"> + page.Description);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; _ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;BING&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> BingSearch()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; _ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptStringOptions</span><span style="line-height: 140%;"> pso =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptStringOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nEnter search string: &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; pso.AllowSpaces = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptResult</span><span style="line-height: 140%;"> pr = _ed.GetString(pso);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pr.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK &amp;&amp; pr.StringResult != </span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> searchString = pr.StringResult;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BingContext</span><span style="line-height: 140%;"> bing =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">BingContext</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;TODO: INSERT YOUR APPID HERE&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">IQueryable</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">PageSearchResult</span><span style="line-height: 140%;">&gt; pages1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> p </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> bing.Pages</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">where</span><span style="line-height: 140%;"> p.Query == searchString</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">select</span><span style="line-height: 140%;"> p;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; pages1 = pages1.Take(2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; WritePages(searchString, pages1);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> q2 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> p</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> bing.Pages.SafeResults().LocalResults(</span><span style="line-height: 140%; color: #a31515;">&quot;San Rafael&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">where</span><span style="line-height: 140%;"> p.Query == searchString</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">select</span><span style="line-height: 140%;"> p;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; WritePages(</span><span style="line-height: 140%; color: #a31515;">&quot;Local search&quot;</span><span style="line-height: 140%;">, q2);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> q3 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">from</span><span style="line-height: 140%;"> i </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> bing.Images</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">where</span><span style="line-height: 140%;"> i.Query == searchString</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; </span><span style="line-height: 140%; color: blue;">select</span><span style="line-height: 140%;"> i;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160;&#0160;&#0160; WriteImages(</span><span style="line-height: 140%; color: #a31515;">&quot;Image search&quot;</span><span style="line-height: 140%;">, q3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Here’s what happens when we run our BING command, entering the name of this blog as a search term:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 7.5pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">BING</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter search string: <span style="color: #ff0000;">Through the Interface</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Page Query: Through the Interface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">================================================================================</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; Through the Interface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; http://through-the-interface.typepad.com/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: through-the-interface.typepad.com</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">A blog for developers working with AutoCAD and other Autodesk technologies</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; Shearwater Flows Through the Interface - Spinner</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://www.spinner.com/2008/08/29/shearwater-flows-through-the-interface/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: www.spinner.com/2008/08/29/shearwater-flows-through-the-interface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Free Music Downloads, MP3 Blog, Indie Rock, Music Videos, Interviews, Songs and </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Live Performances</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Page Query: Local search</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">================================================================================</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; IxDA Discussion: Job 001A, Interaction Designer, San Rafael, CA ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; http://www.interactiondesigners.com/discuss.php?post=27874</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: www.interactiondesigners.com/discuss.php?post=27874</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">... skilled at filtering brand and business objectives through ... project team </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">and will be expected to present and interface ... Job 001A, Interaction </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Designer, San Rafael, CA, Groove11 ...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; Browse Reviews Near San Leandro | Yelp</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://www.yelp.com/browse/reviews/recent?loc=San+Leandro%2C+CA&amp;category=plumbin</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">g</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">www.yelp.com/browse/reviews/recent?loc=San+Leandro%2C+CA&amp;category=plumbing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">I found out about this company through a friend at work ... T T. San Rafael, CA </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">... with had their work ethic and excellent customer interface.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; social events found near San Francisco, California, United States ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://upcoming.yahoo.com/search/?category_id=4&amp;loc=San+Francisco%2CCA&amp;rt=1&amp;page</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">=4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">upcoming.yahoo.com/search/?category_id=4&amp;loc=San+Francisco%2CCA&amp;rt=1&amp;page=4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Dare to tread the San Andreas Fault where the earth&#39;s ... Four Points by </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sheraton S..., San Rafael, ca ... your movies, TV shows, music, and photos into </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">one interface..</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; IxDA Discussion: JOB-Senior Interaction Designer-San Francisco ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; http://www.ixda.org/discuss.php?post=27990&amp;search=designer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: www.ixda.org/discuss.php?post=27990&amp;search=designer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Develop the user interface for new features, including ... Job 001A, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Interaction Designer, San Rafael, CA, Groove11 ... Loc: NYC. CO: ArielPartners: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Recruiter/ Full-time ...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; Through the Interface: November 2006</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://through-the-interface.typepad.com/through_the_interface/2006/11/index.htm</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">l</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">through-the-interface.typepad.com/through_the_interface/2006/11/index.html</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">A blog for developers working with AutoCAD and other Autodesk technologies</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; IxDA Discussion: JOB # Interaction Designer # Seattle, WA # Big Fish </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; http://www.interactiondesigners.com/discuss.php?post=27877</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: www.interactiondesigners.com/discuss.php?post=27877</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Solid understanding of the fundamentals of user interface design; should have </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">experience working through the ... Job 001A, Interaction Designer, San Rafael, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CA, Groove11-Revamped ...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; SAN DIEGO HOTELS - CHEAP RATES GUARANTEED - SAN DIEGO, CA HOTELS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; http://travel.modernhumorist.com/city.cfm/_San-Diego_CA</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: travel.modernhumorist.com/city.cfm/_San-Diego_CA</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Cheap Hotels in San Diego, CA - California Choose from 188 hotels in San Diego, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CA - San ... travelers due to our easy accessibility to the freeway, the close </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">proximity to the loc ...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; Subject: Library of California Board actions February 14-16, 2001</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; http://www.library.ca.gov/loc/board/KeyActions/actions_Feb01.pdf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: www.library.ca.gov/loc/board/KeyActions/actions_Feb01.pdf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">... that the Library of California Board adopts the LoC ... County Library San </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Mateo Union High School District San Rafael ... Ridge School Pleasant Valley </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">State Prison Gold Coast Interface ...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; localtweeps :: San Mateo, California</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; http://www.localtweeps.com/state/CA/San_Mateo/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: www.localtweeps.com/state/CA/San_Mateo</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">SustainableSMC - San Mateo County. San Mateo, CA - http://www ... Senior User </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Interface Designer at Gaia Interactive (gaiaonline ... San Rafael: 34: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Temecula: 33: Palo Alto: 33: Venice: 32: Chico: 32</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160; arch 1, 2008</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://www.marin.ca.gov/depts/bs/main/sups/sdistr1/docs/March08Newsletter.pdf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Display: www.marin.ca.gov/depts/bs/main/sups/sdistr1/docs/March08Newsletter.pdf</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Description</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">MMWD Tank Loc ation to be disc ussed with com- ... 00 a.m.-4:00 p.m. Mary </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Silveira Elementary School, San Rafael ... disease, it is very common in the </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">urban-wildland interface ...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Image Query: Image search</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">================================================================================</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:	&#0160; ... through the bbc motion</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://www.tomcampbell.com/pages/hdreel.html</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 211 x 284</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts1.images.live.com/images/thumbnail.aspx?q=975287422560&amp;id=49d24255edf84</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">6166e9d7b69cc1f0366</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160;&#0160;&#0160; Behold, through the truth of ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://www.orthodoxdelmarva.org/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 169 x 169</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts3.images.live.com/images/thumbnail.aspx?q=965845388446&amp;id=09998a4971c97</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">12fcfb1bd0b7c48e6dc</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:	&#0160; ... through caring, support and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://csrbc.org/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 288 x 173</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts4.images.live.com/images/thumbnail.aspx?q=958547176951&amp;id=13b4b1d35283e</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">19c3d53387a3a2a6dd7</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160;&#0160;&#0160; Through the Interface ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://through-the-interface.typepad.com/through_the_interface/personal/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 455 x 480</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts4.images.live.com/images/thumbnail.aspx?q=1151060279387&amp;id=aca0138b8489</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">8d0699bd738a77bbddf0</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:	&#0160; ... read through the terms</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://www.wallstreetorganization.com/html/menuofservices.html</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 323 x 108</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts4.images.live.com/images/thumbnail.aspx?q=1100438768723&amp;id=a1eb24b8240a</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">92b8a47c59da0a97ecaa</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:	&#0160; ... through caring, support and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://www.csrbc.org/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 273 x 173</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts2.images.live.com/images/thumbnail.aspx?q=1170535684605&amp;id=b7de190ffd3e</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">c8b24d5fbdf6d21b5e7a</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:	&#0160; ... through the course</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://www.skinastc.com/update_dryland-06.html</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 254 x 192</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts2.images.live.com/images/thumbnail.aspx?q=1255470604085&amp;id=5a686c11d820</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">4ce708dfbeb7340f9168</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:	&#0160; ... through the slalom course</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://www.skinastc.com/update_dryland-06.html</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 254 x 192</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts1.images.live.com/images/thumbnail.aspx?q=1152025828944&amp;id=866272997b3b</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">18a11f2c38ba06573d78</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:	&#0160; ... through the collection of</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://www.typewritermuseum.org/collection/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 209 x 139</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts2.images.live.com/images/thumbnail.aspx?q=1042642900713&amp;id=927657174551</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">f10d8409b77865c3484a</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Title:&#0160;&#0160;&#0160; remotely controlled through ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Uri:	&#0160; http://solar.psu.edu/2007/systems_lighting_controls.aspx?lang=en</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size:	&#0160; 255 x 179</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Thumbnail: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">http://ts2.images.live.com/images/thumbnail.aspx?q=1060407483821&amp;id=32f483f0b57b</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">eba9967f3ae2f85bc898</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>As you can see, we get good and bad results back, much like any search run with a sufficiently vague query string. :-)</p>
<p>A command-line interface is clearly not the best way to display search our results – displaying them via WPF inside an AutoCAD palette would be much better – but this post is primarily focused on performing a search rather than worrying too much about the presentation side (we’ll see where it goes from here :-).</p>
<p>One last comment, in case people are wondering: I didn’t choose Bing over Google because I especially prefer it (I use both on a daily basis, depending on the results I get), but simply because I came across the code showing how to use it. It may very well be easy to integrate similar capabilities into your application to search Google programmatically – I’m not an expert in this area, by any means – but I will say that I was reasonably impressed with the capabilities provided by the Bing API and the BLinq framework, the little I’ve actually used them.</p>
