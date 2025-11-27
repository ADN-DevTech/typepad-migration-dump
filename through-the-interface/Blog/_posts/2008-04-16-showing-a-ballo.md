---
layout: "post"
title: "Showing a balloon notification using the InfoCenter API in AutoCAD 2009"
date: "2008-04-16 07:54:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2008/04/showing-a-ballo.html "
typepad_basename: "showing-a-ballo"
typepad_status: "Publish"
---

<p>This post is the latest in the series of closer looks at the <a href="http://through-the-interface.typepad.com/through_the_interface/2008/03/new-apis-in-aut.html" target="_blank">new APIs in AutoCAD 2009</a>. It dips into the InfoCenter API, a .NET API allowing you to customize and drive the InfoCenter feature inside AutoCAD.</p>
<p>To make use of this API you need to add Project References to two managed assemblies from the AutoCAD 2009 root folder: <strong>AcInfoCenterConn.dll</strong> and <strong>AdInfoCenter.dll</strong>.</p>
<p>Here&#39;s some C# code that will display a balloon notification to your users:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.AcInfoCenterConn;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">namespace</span> InfoCenterApp</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;icb&quot;</span>)]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> infoCenterBalloon()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">InfoCenterManager</span> icm =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">AcInfoCenterConn</span>.InfoCenterManager;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;Autodesk.InfoCenter.<span style="COLOR: teal">PaletteMgr</span> pm =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; icm.PaletteManager;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pm.ShowBalloon(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: maroon">&quot;Custom Application Notification&quot;</span>,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: maroon">&quot;Kean has some information for you...&quot;</span>,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">null</span>, <span style="COLOR: green">// Don&#39;t provide an icon</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> System.<span style="COLOR: teal">Uri</span>(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ),</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; 5,&#0160; &#0160;<span style="COLOR: green">// Show the balloon for 5 seconds</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; 1&#0160; &#0160; <span style="COLOR: green">// Make it relatively slow to fade in</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">}</p></div></div></div>
<p>Here&#39;s what you see when you run the ICB command:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Custom%20balloon%20notification.png"><img alt="Custom balloon notification" border="0" height="102" src="/assets/Custom%20balloon%20notification_thumb.png" style="BORDER-BOTTOM: 0px; BORDER-LEFT: 0px; BORDER-TOP: 0px; BORDER-RIGHT: 0px" width="291" /></a></p>
<p><strong><em>Update:</em></strong></p>
<p>For AutoCAD 2010 things has changed a little. It seems you now need to include project references to <strong>AcWindows.dll</strong> and <strong>AdWindows.dll</strong>, as well as making some changes to your code. Some of the required types are now in the Autodesk.Internal namespace, which leads me to believe this may be subject to further change.</p>
<p>Here is some code that worked well for me:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.AcInfoCenterConn;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.Internal.InfoCenter;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> InfoCenterApp</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; [</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;icb&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> infoCenterBalloon()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">InfoCenterManager</span><span style="LINE-HEIGHT: 140%"> icm = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">InfoCenterManager</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ResultItem</span><span style="LINE-HEIGHT: 140%"> ri = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ResultItem</span><span style="LINE-HEIGHT: 140%">();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ri.Category = </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Custom Application Notification&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ri.Title = </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Kean has some information for you...&quot;</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ri.Uri =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> System.</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Uri</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ri.IsFavorite = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; ri.IsNew = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; icm.PaletteManager.ShowBalloon(ri);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>There&#39;s a version of the ShowBalloon() function which takes a XAML fragment as a parameter, for those of you&#0160;into WPF.</p>
<p>Here&#39;s the notification it creates in AutoCAD 2010:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a91f35e3970b-pi" style="DISPLAY: inline"><img alt="InfoCenter bubble in AutoCAD 2010" class="asset asset-image at-xid-6a00d83452464869e20120a91f35e3970b " src="/assets/image_279698.jpg" style="WIDTH: 300px" /></a> <br /> <a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201310f85c173970c-pi" style="DISPLAY: inline"></a></p>
