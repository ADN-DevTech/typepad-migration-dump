---
layout: "post"
title: "A simple overrule to change the way AutoCAD lines are displayed using .NET"
date: "2009-08-17 14:57:13"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Overrules"
original_url: "https://www.keanw.com/2009/08/a-simple-overrule-to-change-the-way-autocad-lines-are-displayed-using-net.html "
typepad_basename: "a-simple-overrule-to-change-the-way-autocad-lines-are-displayed-using-net"
typepad_status: "Publish"
---

<p><em>Thanks to Stephen Preston, who manages our DevTech Americas team, for donating the samples from his upcoming AU class for posting on this blog.</em></p>
<p>Let’s start the week with a nice simple sample: the first from Stephen’s AU class. Looking back even to <a href="http://through-the-interface.typepad.com/through_the_interface/2009/04/customizing-the-display-of-standard-autocad-objects-using-net.html">the first C# overrule sample I posted here</a>, I can see that most have been quite complex, mainly because they’ve performed complicated things. Today’s code implements a very simple DrawableOverrule which changes the way lines are displayed in AutoCAD:</p>
<p>Here’s Stephen’s C# code, reformatted to fit the blog:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.GraphicsInterface;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> MyFirstOverrule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// This is our custom DrawableOverrule class. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">// In this case we're just overruling WorldDraw</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyDrawOverrule</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #2b91af;">DrawableOverrule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> WorldDraw(</span><span style="line-height: 140%; color: #2b91af;">Drawable</span><span style="line-height: 140%;"> drawable, </span><span style="line-height: 140%; color: #2b91af;">WorldDraw</span><span style="line-height: 140%;"> wd)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Cast Drawable to Line so we can access its methods and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// properties</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Line</span><span style="line-height: 140%;"> ln = (</span><span style="line-height: 140%; color: #2b91af;">Line</span><span style="line-height: 140%;">)drawable;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Draw some graphics primitives</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; wd.Geometry.Circle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ln.StartPoint + 0.5 * ln.Delta,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ln.Length / 5,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ln.Normal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// In this case we don't want the line to draw itself, nor do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// we want ViewportDraw called </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Shared member variable to store our Overrule instance </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyDrawOverrule</span><span style="line-height: 140%;"> _drawOverrule;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">"TOG"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ToggleOverrule()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Initialize Overrule if first time run </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (_drawOverrule == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _drawOverrule = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyDrawOverrule</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Overrule</span><span style="line-height: 140%;">.AddOverrule(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">RXObject</span><span style="line-height: 140%;">.GetClass(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">typeof</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #2b91af;">Line</span><span style="line-height: 140%;">)),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _drawOverrule,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Overrule</span><span style="line-height: 140%;">.Overruling = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Toggle Overruling on/off</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Overrule</span><span style="line-height: 140%;">.Overruling = !</span><span style="line-height: 140%; color: #2b91af;">Overrule</span><span style="line-height: 140%;">.Overruling;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">// Regen is required to update changes on screen</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor.Regen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Some points to note:</p>
<ul>
<li>This code chooses to replace the way lines are currently drawn 
<ul>
<li>Rather than drawing a line, we draw a circles at the line’s mid-point with a radius relative to the line’s length </li>
</ul>
</li>
<li>We’ve deliberately kept the code very simple: a single command (TOG) is used to toggle the use of the overrule </li>
<li>Rather than always relying on toggling the overall Overruling state, we force it to true when we run for the first time 
<ul>
<li>I have another application loaded that implements overrules: the first time the TOG command was run previously, overruling was actually turned off if we don’t do it this way </li>
</ul>
</li>
</ul>
<p>To try the code, build the application against AutoCAD 2010 (or even higher, if you’re visiting us from the future :-) and draw some lines:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a4fda2a5970b-pi"><img style="display: inline; border-width: 0px;" title="Some simple lines" src="/assets/image_585381.jpg" border="0" alt="Some simple lines" width="484" height="393" /></a></p>
<p>Now let’s NETLOAD our application and run the TOG command:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a4fda2b6970b-pi"><img style="display: inline; border-width: 0px;" title="Replaced with circles" src="/assets/image_757495.jpg" border="0" alt="Replaced with circles" width="486" height="395" /></a></p>
<p>Each line has been “replaced” by a circle. But when we select one of the circles, we see it’s really just a line, and shows the grips a line would:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a4fda2c3970b-pi"><img style="display: inline; border-width: 0px;" title="Now with one of our ~lines~ selected" src="/assets/image_309923.jpg" border="0" alt="Now with one of our ~lines~ selected" width="484" height="393" /></a></p>
<p>When we grip-edit an end-point of one of our lines, we can see it changing the line, but the graphics displayed continue to be circular:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a554c31b970c-pi"><img style="display: inline; border-width: 0px;" title="Grip-editing one of our circles, I mean lines" src="/assets/image_81554.jpg" border="0" alt="Grip-editing one of our circles, I mean lines" width="485" height="394" /></a></p>
<p>And finally, if we finish the editing operation and run the TOG command again we see the lines has been modified:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20120a554c32f970c-pi"><img style="display: inline; border-width: 0px;" title="Seeing our lines as lines again" src="/assets/image_802854.jpg" border="0" alt="Seeing our lines as lines again" width="483" height="393" /></a></p>
<p>That’s it for today’s post. I’ll probably be delving further into Stephen’s material for posts later in the week.</p>
<p><strong>Update</strong></p>
<p>It seems there has been some kind of change in AutoCAD since this post was made: it's no longer (at least in AutoCAD 2012) possible to disable application-wide overruling by setting Overrule.Overruling to false. This actually makes some sense, in my opinion: it would be logical for overrules to be selective removed, rather than allowing applications to trample on each other.</p>
<p>Here's the updated C# code that shows how to effectively toggle an overrule on and off without relying on this flag:</p>
<p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.GraphicsInterface;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> MyFirstOverrule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// This is our custom DrawableOverrule class.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// In this case we're just overruling WorldDraw</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyDrawOverrule</span><span style="line-height: 140%;"> : </span><span style="color: #2b91af; line-height: 140%;">DrawableOverrule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">override</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> WorldDraw(</span><span style="color: #2b91af; line-height: 140%;">Drawable</span><span style="line-height: 140%;"> drawable, </span><span style="color: #2b91af; line-height: 140%;">WorldDraw</span><span style="line-height: 140%;"> wd)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Cast Drawable to Line so we can access its methods and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// properties</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Line</span><span style="line-height: 140%;"> ln = (</span><span style="color: #2b91af; line-height: 140%;">Line</span><span style="line-height: 140%;">)drawable;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Draw some graphics primitives</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; wd.Geometry.Circle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; ln.StartPoint + 0.5 * ln.Delta,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; ln.Length / 5,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; ln.Normal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// In this case we don't want the line to draw itself, nor do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// we want ViewportDraw called</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Shared member variable to store our Overrule instance</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyDrawOverrule</span><span style="line-height: 140%;"> _drawOverrule;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; [</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">"TOG"</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> ToggleOverrule()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Initialize Overrule if first time run</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (_drawOverrule == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Turn Overruling on</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _drawOverrule = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MyDrawOverrule</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Overrule</span><span style="line-height: 140%;">.AddOverrule(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">RXObject</span><span style="line-height: 140%;">.GetClass(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Line</span><span style="line-height: 140%;">)),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _drawOverrule,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Overrule</span><span style="line-height: 140%;">.Overruling = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Turn Overruling off</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Overrule</span><span style="line-height: 140%;">.RemoveOverrule(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">RXObject</span><span style="line-height: 140%;">.GetClass(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Line</span><span style="line-height: 140%;">)), _drawOverrule</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _drawOverrule.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _drawOverrule = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Regen is required to update changes on screen</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor.Regen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
</p>
