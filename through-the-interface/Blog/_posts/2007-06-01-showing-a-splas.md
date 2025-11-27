---
layout: "post"
title: "Showing a splash-screen from your AutoCAD .NET application"
date: "2007-06-01 22:18:11"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2007/06/showing_a_splas.html "
typepad_basename: "showing_a_splas"
typepad_status: "Publish"
---

<p><em>Thanks once again to Viru Aithal for the inspiration behind this post, although I did write most of the code, this time. :-)</em></p>
<p>Adding a splash screen can give a touch of class to your application, assuming it&#39;s done non-intrusively. This post focuses on how best to do so within AutoCAD, and use the time it&#39;s displayed to perform initialization for your application.</p>
<p>The first thing you need to do is add a Windows Form to your project:</p>
<p><a href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/06/01/splash_screen_1.png" onclick="window.open(this.href, &#39;_blank&#39;, &#39;width=388,height=473,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false"><img alt="Splash_screen_1" border="0" height="365" src="/assets/splash_screen_1.png" title="Splash_screen_1" width="300" /></a> </p>
<p>You should select the standard &quot;Windows Form&quot; type, giving an appropriate name (in this case I&#39;ve used &quot;SplashScreen&quot;, imaginatively enough).</p>
<p><a href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/06/01/splash_screen_2.png" onclick="window.open(this.href, &#39;_blank&#39;, &#39;width=681,height=419,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false"><img alt="Splash_screen_2" border="0" height="184" src="/assets/splash_screen_2.png" title="Splash_screen_2" width="300" /></a> </p>
<p>Once this is done, you should set the background for the form to be your preferred bitmap image, by browsing to it from the form&#39;s BackgroundImage property:</p>
<p><a href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/06/01/splash_screen_3.png" onclick="window.open(this.href, &#39;_blank&#39;, &#39;width=800,height=618,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false"><img alt="Splash_screen_3" border="0" height="231" src="/assets/splash_screen_3.png" title="Splash_screen_3" width="300" /></a> </p>
<p>Now we&#39;re ready to add some code. Here&#39;s some C# code that shows how to show the splash-screen from the <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/initialization_.html">Initialize()</a> method:</p><br />
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Prompts; <span style="COLOR: green">// This is the name of the module</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">namespace</span> SplashScreenTest</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Startup</span> : <span style="COLOR: teal">IExtensionApplication</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">SplashScreen</span> ss = <span style="COLOR: blue">new</span> <span style="COLOR: teal">SplashScreen</span>();</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Rather than trusting these properties to be set</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// at design-time, let&#39;s set them here</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.StartPosition =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; System.Windows.Forms.<span style="COLOR: teal">FormStartPosition</span>.CenterScreen;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.FormBorderStyle =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; System.Windows.Forms.<span style="COLOR: teal">FormBorderStyle</span>.None;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.Opacity = 0.8;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.TopMost = <span style="COLOR: blue">true</span>;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.ShowInTaskbar = <span style="COLOR: blue">false</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Now let&#39;s disply the splash-screen</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: teal">Application</span>.ShowModelessDialog(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: teal">Application</span>.MainWindow,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ss,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">false</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.Update();</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// This is where your application should initialise,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// but in our case let&#39;s take a 3-second nap</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;System.Threading.<span style="COLOR: teal">Thread</span>.Sleep(3000);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ss.Close();</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">}</p></div>
<p>Some notes on the code:</p>
<ul>
<li>I used a sample application called &quot;Prompts&quot; - you should change the using directive to refer to your own module name.</li>
<li>We&#39;re setting a number of properties dynamically (at runtime), rather than stepping through how to set them at design-time.</li>
<li>We&#39;ve set the splash screen to be 80% opaque (or 20% transparent). This is easy to adjust.</li>
<li>Some of the additional properties may be redundant, but they seemed sensible to set (at least to me).</li>
</ul>
<p>Here&#39;s the result... I&#39;ve set up my application to <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/automatic_loadi.html">demand-load</a> when I invoke a command, which allowed me to load a DWG first to show off the transparency of the splash-screen (even though the above code doesn&#39;t actually define a command - so do expect an &quot;Unknown command&quot; message, if you do exactly the same thing as I have). You may prefer to set the module to load on AutoCAD startup, otherwise.</p>
<p><a href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/06/01/splash_screen_4.png" onclick="window.open(this.href, &#39;_blank&#39;, &#39;width=800,height=638,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false"><img alt="Splash_screen_4" border="0" height="239" src="/assets/splash_screen_4.png" title="Splash_screen_4" width="300" /></a>&#0160;</p>
<br />
<p><strong><em>Update:</em></strong></p>
<p>Roland Feletic brought it to my attention that this post needed updating <span id="fck_dom_range_temp_1237843456261_740"></span>for AutoCAD 2010. Thanks, Roland!</p>
<p>I looked into the code, and found that the call to ShowModelessDialog needed changing to this:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.ShowModelessDialog(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Application</span><span style="LINE-HEIGHT: 140%">.MainWindow.Handle,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; ss,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; );</span></p></div>
<p>I also found I had to add an additional assembly reference to PresentationCore (a .NET Framework 3.0 assembly).</p>
