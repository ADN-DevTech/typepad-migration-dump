---
layout: "post"
title: "Accessing and Modifying Settings in the Ini File"
date: "2019-02-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "External"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/02/accessing-useful-settings-in-the-ini-file.html "
typepad_basename: "accessing-useful-settings-in-the-ini-file"
typepad_status: "Publish"
---

<p>Some interesting settings are stored in and can be modified by editing the Revit ini file <code>Revit.ini</code>.</p>

<p>Peter <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/859112">@pgerz</a> pointed
out yet another possibility in his answer to 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/adding-project-template-to-new-project-via-api/m-p/8585348">adding a project template to 'New Project' via API</a>:</p>

<p><strong>Question:</strong> I noticed that the UI method for adding a project template to the 'New Project' dialog on the Start Window is by going to Options &gt; File Locations and clicking the little plus symbol.</p>

<p>Is there any way to achieve this same effect using the API?</p>

<p>I would like to add templates to the dropdown.</p>

<p><strong>Answer:</strong> You can do it by editing the ini file with standard .NET functions; it is located at:</p>

<ul>
<li>C:\Users\%username%\AppData\Roaming\Autodesk\Revit\Autodesk Revit 2019\Revit.ini</li>
</ul>

<p>In the section <code>[DirectoriesENU]</code>, modify the setting <code>DefaultTemplate</code>.</p>

<p>Example:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;oriFile&nbsp;=&nbsp;<span style="color:maroon;">@&quot;&quot;</span>
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:#2b91af;">Environment</span>.GetEnvironmentVariable(&nbsp;<span style="color:#a31515;">&quot;appdata&quot;</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:maroon;">@&quot;\Autodesk\Revit\Autodesk&nbsp;Revit&nbsp;2019\Revit.ini&quot;</span>;

&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;tmpFile&nbsp;=&nbsp;<span style="color:maroon;">@&quot;c:\temp\11.ini&quot;</span>;

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;System.IO.<span style="color:#2b91af;">File</span>.Exists(&nbsp;oriFile&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">StreamReader</span>&nbsp;sr&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">StreamReader</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;oriFile,&nbsp;<span style="color:#2b91af;">Encoding</span>.Unicode&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">StreamWriter</span>&nbsp;sw&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">StreamWriter</span>(&nbsp;tmpFile,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">false</span>,&nbsp;<span style="color:#2b91af;">Encoding</span>.Unicode&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;inputLine&nbsp;=&nbsp;<span style="color:#a31515;">&quot;&quot;</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">while</span>(&nbsp;(&nbsp;inputLine&nbsp;=&nbsp;sr.ReadLine()&nbsp;)&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;inputLine.StartsWith(&nbsp;<span style="color:#a31515;">&quot;DefaultTemplate=&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;inputLine.Contains(&nbsp;<span style="color:#a31515;">&quot;Example_SCHEMA.rte&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;do&nbsp;nothing</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">else</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;inputLine&nbsp;=&nbsp;inputLine
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;<span style="color:maroon;">@&quot;,&nbsp;Example_SCHEMA=C:\temp\Example_SCHEMA.rte&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sw.WriteLine(&nbsp;inputLine&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sw.Close();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;System.IO.<span style="color:#2b91af;">File</span>.Replace(&nbsp;tmpFile,&nbsp;oriFile,&nbsp;<span style="color:blue;">null</span>&nbsp;);
&nbsp;&nbsp;}
</pre>

<p>Many thanks to Peter for this solution!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3c2811e200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3c2811e200d img-responsive" style="width: 320px; display: block; margin-left: auto; margin-right: auto;" alt="Stencil" title="Stencil" src="/assets/image_24d32b.jpg" /></a><br /></p>

<p></center></p>
