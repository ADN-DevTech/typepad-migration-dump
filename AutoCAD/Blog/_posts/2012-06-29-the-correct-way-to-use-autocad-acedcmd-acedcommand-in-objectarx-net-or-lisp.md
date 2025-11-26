---
layout: "post"
title: "The &ldquo;correct&rdquo; way to use AutoCAD acedCmd() acedCommand() in ObjectARX, .NET, or LISP"
date: "2012-06-29 09:58:53"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/the-correct-way-to-use-autocad-acedcmd-acedcommand-in-objectarx-net-or-lisp.html "
typepad_basename: "the-correct-way-to-use-autocad-acedcmd-acedcommand-in-objectarx-net-or-lisp"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>By now, if you have been following my posts, will have probably realized that I really do like acedCmd and acedCommand (command in LISP).</p>
<p>I wanted to write an tell you how you should format the command strings and keywords that you pass to these functions,</p>
<p>Firstly, let’s talk about the Command strings themselves. AutoCAD comes in many languages, and the Command strings are actually translated for each language. This means that we can never assume the English LINE command will be called the same in the Russian version, for instance. As a programmer, one that uses acedCmd or acedCommand, we don’t really want to worry ourselves about localized Command strings. Thankfully, we can always access the English (non-localized version of any Command string by using the underscore symbol… e.g.</p>
<p><strong>_LINE</strong></p>
<p>Another issue that programmers like us don’t want to worry ourselves with when using acedCmd or acedCommand is whether or not the user has <a href="http://exchange.autodesk.com/autocad/enu/online-help/search#WS73099cc142f4875516d84be10ebc87a53f-7c41.htm">REDEFINE</a>’d one of the Commands that we want to use… If they have, they could cause our lovely program to fail because they have changed the way the command we are using works – costing us support calls!! Thankfully, we can always access the raw Command using …. e.g.</p>
<p><strong>.LINE</strong></p>
<p>You can combine both of these tricks together… e.g.</p>
<p><strong>_.LINE</strong></p>
<p><strong>in roundup, the “correct” way to specify Command Strings using acedCmd and acedCommand is: prefix all Commands with _.</strong></p>
<p>Finally, let’s talk about the keyword strings that you pass to acedCmd or acedCommand. Again, these are localized, again we don’t want to worry about running into keywords that are different on different localized versions of AutoCAD, so we can use the underscore technique... (note you cannot REDEFINE keywords, so there is no dot required)… e.g.</p>
<p><strong>_Startpoint</strong></p>
<p>You’ll notice that I have specified the whole keyword string, not just _S. Why I recommend that you do this is is purely from experience…</p>
<p>1) Commands can change in the future, if you specify _S then later on, we introduce a new keyword called _Separate, then your code is broken. Always use the full keyword, it will save you time and effort in the future.</p>
<p>2) It’s easier to read in your code… You’ll be glad of this years down the line when you are trying to remember what the command is doing.</p>
