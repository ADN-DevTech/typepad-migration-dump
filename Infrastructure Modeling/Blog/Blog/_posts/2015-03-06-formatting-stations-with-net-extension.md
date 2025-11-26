---
layout: "post"
title: "Formatting Stations with .NET Extension"
date: "2015-03-06 06:35:38"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2015"
original_url: "https://adndevblog.typepad.com/infrastructure/2015/03/formatting-stations-with-net-extension.html "
typepad_basename: "formatting-stations-with-net-extension"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>Format stations like Civil 3D using, for instance, 3+1.00 for a number like 61.00, can be easy with .NET, and even more easier with .NET Extensions. Here it is: first define a module class that contains your utility functions, something like</p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: blue">Public</span>&#160;<span style="color: blue">Module</span>&#160;<span style="color: #2b91af">Utils</span>&#160;&#160; &lt;System.Runtime.CompilerServices.<span style="color: #2b91af">Extension</span>()&gt;&#160;&#160; <span style="color: blue">Public</span>&#160;<span style="color: blue">Function</span> ToStationString(station <span style="color: blue">As</span>&#160;<span style="color: blue">Double</span>) <span style="color: blue">As</span>&#160;<span style="color: blue">String</span>&#160;&#160;&#160;&#160; <span style="color: blue">Dim</span> fractionPart <span style="color: blue">As</span>&#160;<span style="color: blue">Double</span> = station <span style="color: blue">Mod</span> 20<br />&#160;&#160;&#160; <span style="color: blue">Dim</span> integerPart <span style="color: blue">As</span>&#160;<span style="color: blue">Double</span> = (station - fractionPart) / 20<br />&#160;&#160;&#160; <span style="color: blue">Return</span>&#160;<span style="color: blue">String</span>.Format(<span style="color: #a31515">&quot;{0:0}+{1:0.00}&quot;</span>, integerPart, fractionPart)<br />&#160; <span style="color: blue">End</span>&#160;<span style="color: blue">Function</span><br /><span style="color: blue">End</span>&#160;<span style="color: blue">Module</span></pre>

<p>The above code assumes the distance between stations as 20 and, for simplicity, this number was hard coded. Note the Extension attribute that does the trick to add it for all doubles. Now we can use like:</p>

<pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: blue">Dim</span> stationDouble <span style="color: blue">As</span>&#160;<span style="color: blue">Double</span> = <font color="#008000">' some number here</font>
<span style="color: blue">Dim</span> stationString <span style="color: blue">As</span>&#160;<span style="color: blue">String</span> = stationDouble.ToStationString()<br /></pre>

<p>Isn’t that nice? </p>

<p>Hope you enjoyed.</p>

<p>Important: this simple code is not taking Civil 3D styles into consideration, but can be a nice addition to it.</p>
