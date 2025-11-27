---
layout: "post"
title: "Cancelling an active command in AutoCAD"
date: "2006-08-14 17:26:14"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Commands"
  - "ObjectARX"
original_url: "https://www.keanw.com/2006/08/cancelling_an_a.html "
typepad_basename: "cancelling_an_a"
typepad_status: "Publish"
---

<p><em>Thanks to Alexander Rivilis for this topic (he submitted a comment to my <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/techniques_for_.html">previous post</a> that got me thinking about this addendum).</em></p>

<p>When you're asking AutoCAD to execute commands by submitting them to its command-line, it helps to make sure no command is currently active. The accepted approach is to send two escape characters to the command-line... this is adopted by our menu &amp; toolbar macros that consistently start with ^C^C in MNU and now CUI files.</p>

<p>Why two? The first is to cancel the &quot;most active&quot; command, and the second is to drop out of either the dimension mode or the outermost command, if the innermost was actually being executed transparently. Strictly speaking there are probably a few cases where you might actually need three escape characters. Type &quot;DIM VERT 'ZOOM&quot; into the command-line, and you'll need three escapes to get back to the &quot;Command:&quot; prompt, but that's fairly obscure - two is generally considered enough for most realistic situations.</p>

<p>So... what constitutes an <a href="http://en.wikipedia.org/wiki/Escape_character">escape character</a>? Well, luckily we no longer need to thumb back to the <a href="http://en.wikipedia.org/wiki/ASCII">ASCII</a> table in the back of our AutoCAD R12 Programming manuals to find this out. It's ASCII code 27, which is represented as 1B in hexadecimal notation.</p>

<p>A common way to send the character from C++ is to send the string &quot;\x1B&quot; for each escape character to AutoCAD. From VB you would tend to use Chr$(27) for the same purpose.</p>

<p>In terms of your choice for sending the character(s) across to AutoCAD... you can generally use one of the functions suggested previously, sendStringToExecute(), SendMessage() or SendCommand(). It won't work from ads_queueexpr(), but then given its typical usage context you shouldn't need to use it for this. Also, as Alexander very rightly pointed out in his comment, you might use PostMessage() or acedPostCommand() (more on this second one later).</p>

<p>SendMessage() and PostMessage() are very similar - you can check this article for <a href="http://msdn.microsoft.com/msdnmag/issues/1200/c/default.aspx">the differences</a>. I tend to prefer SendMessage() for synchronous use, as it waits to have an effect before returning, but PostMessage() is still good to have around in the toolbox (it's good for firing off keystrokes into a window's message loop).</p>

<p>acedPostCommand() is another undocumented function, which needs declaring in your code before use:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">extern</span> Adesk::Boolean acedPostCommand(<span style="COLOR: blue">const</span> ACHAR* );</p></div>

<p>What's particularly interesting about this function is its use of the special keyword, which should get you back to the &quot;Command:&quot; prompt irrespective of command nesting:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">acedPostCommand(_T(<span style="COLOR: maroon">&quot;CANCELCMD&quot;</span>));</p></div>

<p>Then, of course, you can continue to use it as shown in my previous post:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">acedPostCommand(_T(<span style="COLOR: maroon">&quot;_POINT 6,6,0 &quot;</span>));</p></div>

<p>One quick note about cancelling commands from acedCommand(). If you pass RTNONE as the only argument to acedCommand(), this will be interpreted as an escape by AutoCAD.</p>
