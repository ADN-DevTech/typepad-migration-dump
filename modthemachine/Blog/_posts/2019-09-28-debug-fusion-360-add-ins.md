---
layout: "post"
title: "Debug Fusion 360 add-ins with VS Code"
date: "2019-09-28 10:55:01"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/09/debug-fusion-360-add-ins.html "
typepad_basename: "debug-fusion-360-add-ins"
typepad_status: "Publish"
---

<p>As I wrote about it in <a href="https://modthemachine.typepad.com/my_weblog/2019/08/new-python-version-and-visual-studio-code.html">this blog post</a>, we were switching to <a href="https://code.visualstudio.com/">Visual Studio Code</a> as the debugging tool for <strong>Fusion 360</strong> - I think that&#39;s great 😀</p>
<p>At the same time, we also upgraded the <a href="https://www.python.org/">Python</a> version used in <strong>Fusion 360</strong> to <a href="https://www.python.org/downloads/release/python-373/">3.7.3</a>&#0160;<br />Unfortunately, some <strong>add-ins</strong> got caught up in that. It looks like the new <strong>Python</strong> version is more strict and so you might run into compiler issues.</p>
<p><span style="color: #ff0000;"><strong>Please do check your add-ins to make sure they are still working ok. Thank you!</strong></span></p>
<p><strong><span style="text-decoration: underline;">Debugging to the rescue!</span></strong></p>
<p><span style="color: #111111;">First of all, you need to install <a href="https://code.visualstudio.com/">Visual Studio Code</a>. If you follow the below steps and you do not have it installed yet, then you will get a notification dialog asking you to do this along with the <strong>URL</strong> to the download site - but of course, you can easily find it on the net, and I&#39;m providing the link as well in the paragraph 🙂&#0160;</span></p>
<p><span style="color: #111111;">1) Select your <strong>add-in</strong> in the &quot;<strong>Scripts and Add-Ins</strong>&quot; dialog and select &quot;<strong>Debug</strong>&quot; under the &quot;<strong>Run</strong>&quot; drop-down menu&#0160;</span></p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4b30e45200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ScriptsAndAddins" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4b30e45200d img-responsive" src="/assets/image_191334.jpg" title="ScriptsAndAddins" /></a></p>
<p>2) As soon as <strong>Visual Studio Code</strong> starts up, switch over to it (in case it does not get focus automatically), select the &quot;<strong>Debug</strong>&quot; environment and click the &quot;<strong>Start Debugging</strong>&quot; button (looks like a Play button)</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4b3113c200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Debugging" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4b3113c200d image-full img-responsive" src="/assets/image_196693.jpg" title="Debugging" /></a></p>
<p>3) As shown in the above picture, it&#39;s a good idea to select the &quot;<strong>Raised Exceptions</strong>&quot; option in the &quot;<strong>Breakpoints</strong>&quot; section. This way you&#39;ll get a meaningful error even if the compiler runs into problems with a module you&#39;re trying to load. This won&#39;t help though if the problem is in our main module - <a href="https://modthemachine.typepad.com/my_weblog/2019/10/cannot-debug-my-python-add-in.html">this blog post</a> has a workaround for that.</p>
<p>4) Now you have the usual options for stepping through the code, use breakpoints, etc</p>
<p>5) If you want to start the process again (maybe your code failed, etc) and the debugging toolbar in <strong>Visual Studio Code</strong> is still visible, then click the &quot;<strong>Disconnect</strong>&quot; button on it, and follow the above steps again</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4d79ea9200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Disconnect" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4d79ea9200b img-responsive" src="/assets/image_299972.jpg" title="Disconnect" /></a></p>
<p>6) Using <strong>print()</strong> you can write information to the &quot;<strong>Debug Console</strong>&quot;:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4d7a82b200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DebugConsole" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4d7a82b200b img-responsive" src="/assets/image_660882.jpg" title="DebugConsole" /></a></p>
<p>More information on debugging in <strong>VS Code</strong> on its website: <a href="https://code.visualstudio.com/docs/editor/debugging">https://code.visualstudio.com/docs/editor/debugging</a>&#0160;</p>
<p>-Adam&#0160;</p>
