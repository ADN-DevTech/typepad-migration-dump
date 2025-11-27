---
layout: "post"
title: "Using Visual Studio to Debug iLogic Rules"
date: "2019-12-06 21:51:22"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/12/using-visual-studio-to-debug-ilogic-rules.html "
typepad_basename: "using-visual-studio-to-debug-ilogic-rules"
typepad_status: "Publish"
---

<p>In <strong>Inventor 2019</strong>, we have incorporated an experimental mechanism that allows you to use <strong>Visual Studio</strong> to debug your <strong>iLogic</strong> rules.&#0160; It is not a seamless experience, but nevertheless is an effective technique.&#0160; We are looking for feedback as to whether the quirks in the experience are tolerable, versus the benefits of being able to use an industrial-strength debugger.</p>
<p>To debug your <strong>iLogic</strong> rules:</p>
<ul>
<li>Prerequisite 1:&#0160; Install <strong>Inventor 2019</strong> or a later version (does not work in <strong>2018</strong> or earlier releases)</li>
<li>Prerequisite 2:&#0160; Install <strong>Microsoft Visual Studio 2017</strong> or <strong>2019</strong> (<strong>Community</strong>, <strong>Professional</strong> or <strong>Enterprise edition</strong>.&#0160; Not &quot;<strong>Code</strong>&quot;).&#0160; The &quot;<strong>Community</strong>&quot; edition is a free download if you meet certain qualifications, available here:&#0160;<a class="external-link" href="https://www.visualstudio.com/downloads/" rel="nofollow noopener" target="_blank">https://www.visualstudio.com/downloads/</a>&#0160; Earlier versions of <strong>VS</strong> will probably work, too.&#0160; We have not tested every version of every edition, but we expect that any recent version will be OK.&#0160; When I tested the <strong>Community</strong> edition, I only installed the &quot;<strong>.NET Desktop</strong>&quot; subset.&#0160; For more information about <strong>Visual Studio</strong>, try these links:<br /><a class="external-link" href="https://www.visualstudio.com/vs/community/" rel="nofollow noopener" target="_blank">https://www.visualstudio.com/vs/community/</a> <a class="external-link" href="https://visualstudio.microsoft.com/license-terms/mlt031819/" rel="nofollow noopener" target="_blank">https://visualstudio.microsoft.com/license-terms/mlt031819/</a> <a class="external-link" href="https://www.visualstudio.com/vs/pricing/#tab-b8953f16f0b68f60f18" rel="nofollow noopener" target="_blank">https://www.visualstudio.com/vs/pricing/#tab-b8953f16f0b68f60f18</a></li>
<li>Setup:&#0160;&#0160;
<ul>
<li>Start <strong>Visual Studio</strong> (&quot;VS&quot;).&#0160;&#0160;</li>
<li>Create a new <strong>VS</strong> project using &quot;<strong>Visual Basic Class Library (standard .NET Framework)</strong>&quot;.&#0160; You don&#39;t have to do anything with this project, you just have to have one, so it&#39;s <strong>OK</strong> to accept all the defaults.</li>
<li>Turn on all <strong>CLR</strong> Exceptions, as shown here:<br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4a6078d200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Image2018-1-2_15-57-21" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4a6078d200c img-responsive" src="/assets/image_831310.jpg" title="Image2018-1-2_15-57-21" /></a><br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4f3cd66200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Image2018-1-2_15-58-34" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4f3cd66200b img-responsive" src="/assets/image_228474.jpg" title="Image2018-1-2_15-58-34" /></a></li>
<li>Disable the option shown below:<br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4f3cd73200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Vs-option" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4f3cd73200b img-responsive" src="/assets/image_613118.jpg" title="Vs-option" /></a></li>
<li>Enable the option shown below:<br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4cf2dee200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DebuggingLegacyEvaluators" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4cf2dee200d img-responsive" src="/assets/image_439902.jpg" title="DebuggingLegacyEvaluators" /></a></li>
</ul>
</li>
</ul>
<p>To debug your rules:</p>
<ul>
<li>Open an Inventor file with <strong>iLogic</strong> rules.</li>
<li>Start <strong>Visual Studio</strong> (&quot;VS&quot;)</li>
<li><s>Open the VS project you created (or create a new one)</s>&#0160; (not necessary)</li>
<li>Attach <strong>VS</strong> to the <strong>Inventor</strong> process.&#0160; This is easy in <strong>VS 2017</strong> or <strong>2019</strong>, a little trickier in earlier <strong>VS</strong> versions.&#0160; Put &quot;<strong>Inventor</strong>&quot; in the search box to find the process.&#0160; Use&#0160;the &quot;<strong>Attach to</strong>&quot; option set to&#0160;&quot;<strong>Managed (v4.6, v4.5, v4.0) code</strong>&quot; (automatic)<br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4a607b9200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Image2018-1-2_16-3-12" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4a607b9200c img-responsive" src="/assets/image_855138.jpg" title="Image2018-1-2_16-3-12" /></a><br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4a607c0200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Image2018-1-2_16-4-31" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4a607c0200c img-responsive" src="/assets/image_468780.jpg" title="Image2018-1-2_16-4-31" /></a></li>
<li>Set breakpoints by one or more of these methods:
<ul>
<li>Use this function in your rule:&#0160;<strong>Break&#0160;</strong>&#0160;(as a single line of code in your rule).&#0160; This is persistent (across sessions, like any code in your rule)</li>
<li>Get an exception (error) in your rule-code.&#0160; <strong>VS</strong> will automatically stop and give you the chance to inspect the state.</li>
<li>Later, you can use additional <strong>VS</strong> breakpoints, by simply clicking on the line where you want to break, in the <strong>VB</strong> files in <strong>VS</strong>, once the files are loaded. These breakpoints are saved with the <strong>VS</strong> project, and can be used again in future sessions.&#0160;&#0160;</li>
</ul>
</li>
</ul>
<ul>
<li>Now go back to <strong>Inventor</strong> and run the rule.&#0160; If the rule execution hits a breakpoint or gets any exception, then the code will stop in <strong>VS</strong>, and you can use all the <strong>VS</strong> tools to examine the current state.&#0160;Note that Inventor itself is &quot;hung&quot; at this point; you cannot do anything else inside Inventor while you are at the breakpoint.&#0160; When you&#39;re ready to go back to Inventor, click &quot;<strong>Continue</strong>&quot; (or press F5), and use Inventor normally after that.<br /><br /></li>
<li>Tools you can use:
<ul>
<li>Examine values of local variables</li>
<li>If a variable is declared &quot;<strong>as Object</strong>&quot; (perhaps implicitly), use the &quot;<strong>Dynamic view</strong>&quot; to its properties&#0160;</li>
<li>Use &quot;<strong>Watch</strong>&quot; window to enter expressions, especially to see items of arrays/collections, e.g., myArray(6)</li>
<li>Call stack</li>
<li>Use &quot;<strong>debug.print</strong>&quot; in your rule to send text to the &quot;output&quot; window</li>
<li>In addition, in <strong>2019.1</strong>, you can also use the new <strong>iLogic</strong> &quot;<strong>Logger</strong>&quot; functions (does not require nor use VS).</li>
</ul>
</li>
</ul>
<ul>
<li>However, note that &quot;the code&quot; that you see in <strong>VS</strong> is not exactly your original code from your rule.&#0160; It has been &quot;processed&quot; to support some special <strong>iLogic</strong> features.&#0160; <strong>VS</strong> is actually using a temporary file that is &quot;pure&quot; <strong>Visual Basic</strong>. Fortunately, the differences are minor,&#0160;&#0160;including: &quot;wrapper&quot; code, conversion of unit-strings to expressions, extra &quot;m_parameter&quot; on parameter-names, etc.&#0160;&#0160;</li>
<li>When you have found the problem, you have to use the <strong>Rule Editor</strong> to change the code of the rule.&#0160; You cannot edit the temporary <strong>VB</strong> file directly in <strong>VS</strong> (think of it as read-only).</li>
<li>By default, all rule code temporary files are stored in the folder &quot;%Temp%\iLogic Rules&quot;</li>
</ul>
<p>Re-stating some of the known limitations:</p>
<ul>
<li>You have to install and run <strong>Visual Studio</strong> (VS).</li>
<li>You have to start <strong>VS</strong> and &quot;attach&quot; it to the running Inventor process.</li>
<li>Inventor is &quot;hung&quot; while in the breakpoint.</li>
<li>The &quot;code&quot; that is displayed has some minor deviations from the rule as-written.</li>
<li>You cannot edit your code in <strong>VS</strong>, you must go back to the <strong>Rule Editor</strong>.</li>
</ul>
<p>We welcome your feedback on this experimental feature.&#0160;&#0160;</p>
<p>Differences between the code in the rule and the code shown in <strong>VS</strong>:</p>
<ul>
<li>&quot;System code&quot;&#0160;– extra bits and pieces that <strong>iLogic</strong> provides for you.&#0160; These lines are flagged with a comment, &quot;<strong>**iLogic system**</strong>&quot;, and you can ignore them.</li>
<li>&quot;m_parameter_&quot;&#0160;&#0160;– references to parameters will have this prefix</li>
<li>Units ...</li>
<li>Highlighting</li>
</ul>
<p>Things to try to get used to it:</p>
<ul>
<li>Start with a simple rule that has both local variables (&quot;dim xxx =thisDoc.path&quot;) and references to parameters in the document.&#0160;&#0160;
<ul>
<li>Insert a &quot;break&quot; line in the rule.&#0160;&#0160;</li>
<li>Run the rule; it should automatically stop in the debugger (<strong>VS</strong>)</li>
<li>Use &quot;F10&quot; (Or click on &quot;step over&quot; icon) to step through.&#0160; You may have to use it once just to get it &quot;in&quot;.</li>
<li>Look at the values of the variables in the &quot;Locals&quot; or &quot;Auto&quot; pane.&#0160;&#0160;</li>
<li>Try to use Inventor while still in the debugger – you can&#39;t.&#0160; Inventor is &quot;hung&quot; while it is stopped in the debugger.&#0160;</li>
<li>Note that the code you&#39;re seeing is not (really) editable.&#0160; You will have to use the <strong>Rule Editor</strong> to make changes.</li>
<li>Click &quot;<strong>continue</strong>&quot; (or press F5) to go back to Inventor.</li>
</ul>
</li>
<li>Change your rule:&#0160; Remove the &#39;break&#39; line, and add a deliberate error, e.g., refer to the -1&#39;th element of an array, etc.
<ul>
<li>Run the rule.&#0160; It should automatically stop in <strong>VS</strong>.</li>
<li>Look at the values</li>
<li>Enter some tests in a <strong>Watch window</strong>.</li>
<li>Click &quot;<strong>continue</strong>&quot; or &quot;F5&quot;</li>
</ul>
</li>
</ul>
<p>-Mike Deck</p>
