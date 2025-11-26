---
layout: "post"
title: "Check if Subscription Release is installed"
date: "2015-08-28 10:25:37"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/08/check-if-subscription-release-is-installed.html "
typepad_basename: "check-if-subscription-release-is-installed"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/augusto-goncalves.html">Augusto Goncalves</a> (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>  <p>At the <a href="http://www.autodesk.com/developrevit">Developer Center</a> you’ll find the <a href="http://images.autodesk.com/adsk/files/REVIT2015SDK_SubscriptionRelease.msi">Revit 2015 SDK for Subscription Release</a>. Like in previous years, this update include some new features, and Jeremy Tammik discussed at his <a href="http://thebuildingcoder.typepad.com/blog/2015/02/revit-2015-r2-and-the-read-write-workset-api.html">blog post</a>. </p>  <p>Ok, but can use on my plugin? You may have a user that don’t have this installed (due many reasons). You may check if is available via version number, but can get complicated if the user has a newer Update Release without this Subscription Release. </p>  <p>Here is a quick &amp; easy approach: check if the new method is indeed available on the API reference loaded on runtime via Reflection. Here is how:</p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: blue">public</span>&#160;<span style="color: blue">static</span>&#160;<span style="color: blue">class</span>&#160;<span style="color: #2b91af">VersionHelper</span><br />{<br />&#160; <span style="color: blue">public</span>&#160;<span style="color: blue">static</span>&#160;<span style="color: blue">bool</span> Has2015SubscriptionRelease<br />&#160; {<br />&#160;&#160;&#160; <span style="color: blue">get</span><br />&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160; <span style="color: blue">return</span>&#160;<span style="color: blue">typeof</span>(Autodesk.Revit.DB.<span style="color: #2b91af">Workset</span>).GetMethod(<span style="color: #a31515">&quot;Create&quot;</span>) != <span style="color: blue">null</span>;<br />&#160;&#160;&#160; }<br />&#160; }<br />}</pre>

<p>The Workset.Create method is only available with this Subscription Release. </p>

<p>Now you may 2 DLLs, one compiled for the RTM and another for the Subscription Release. For a Ribbon, try something like:</p>

<pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: blue">public</span>&#160;<span style="color: blue">class</span>&#160;<span style="color: #2b91af">MyApp</span> : <span style="color: #2b91af">IExternalApplication</span><br />{<br />&#160; <span style="color: blue">public</span>&#160;<span style="color: #2b91af">Result</span> OnStartup(<span style="color: #2b91af">UIControlledApplication</span> a)<br />&#160; {<br /> <br />&#160;&#160;&#160; <span style="color: #2b91af">PushButtonData</span> pbd = <span style="color: blue">new</span>&#160;<span style="color: #2b91af">PushButtonData</span>(<span style="color: #a31515">&quot;MY_CMD&quot;</span>, <span style="color: #a31515">&quot;My command&quot;</span>,<br />&#160;&#160;&#160;&#160;&#160; <span style="color: #2b91af">VersionHelper</span>.Has2015SubscriptionRelease ? <span style="color: #a31515">&quot;2015_SubsRelease.dll&quot;</span> : <span style="color: #a31515">&quot;2015_RTM.dll&quot;</span>,<br />&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">&quot;Namespace.ClassName&quot;</span>);<br /> <br />&#160;&#160;&#160; <span style="color: #2b91af">RibbonPanel</span> pnl = a.CreateRibbonPanel(<span style="color: #a31515">&quot;Version test&quot;</span>);<br />&#160;&#160;&#160; pnl.AddItem(pbd); <span style="color: green">// this button will point to the correct DLL</span><br /> <br />&#160;&#160;&#160; <span style="color: blue">return</span>&#160;<span style="color: #2b91af">Result</span>.Succeeded;<br />&#160; }<br />}</pre>
