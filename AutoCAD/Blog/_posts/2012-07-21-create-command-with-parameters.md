---
layout: "post"
title: "Create command with parameters"
date: "2012-07-21 13:43:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/create-command-with-parameters.html "
typepad_basename: "create-command-with-parameters"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;ve created some commands in .NET but I cannot see how to set them up so that they will accept parameters.</p>
<p><strong>Solution</strong></p>
<p>The function that implements the command cannot accept parameters, but inside the command implementation you can accept parameters using command line input functions like GetString(), GetInteger(), etc. that can be found under the Editor class.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;CommandWithParameters&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CommandWithParameters()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptResult</span><span style="line-height: 140%;"> pr = ed.GetString(</span><span style="color: #a31515; line-height: 140%;">&quot;\nProvide a string: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;No string was provided\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptIntegerResult</span><span style="line-height: 140%;"> pir = ed.GetInteger(</span><span style="color: #a31515; line-height: 140%;">&quot;\nProvide an integer: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pir.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;No integer was provided\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;\nCommand got following parameters: {0} and {1}\n&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; pr.StringResult, pir.Value.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>In case of the above command I can call the command and pass in the parameters one by one, or pass them at the same time e.g. using the <strong>(command)</strong> LISP function. Here is what it would look like in the Command Line:</p>
<p style="line-height: 120%;"><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;">Command: COMMANDWITHPARAMETERS </span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Provide string: mystring</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Provide an integer: 12</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Command got following parameters: mystring and 12</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Command: (command &quot;COMMANDWITHPARAMETERS&quot; &quot;mystring&quot; 12)</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> COMMANDWITHPARAMETERS</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Provide string: mystring</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Provide an integer: 12</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Command got following parameters: mystring and 12</span><br /><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;"> Command: nil</span></p>
<p>Depending on your requirements you could also create a LISP function in .NET instead of a command that could accept parameters. In that case you&#39;d need to use the <strong>LispFunction</strong> attribute instead of <strong>CommandMethod</strong>.</p>
