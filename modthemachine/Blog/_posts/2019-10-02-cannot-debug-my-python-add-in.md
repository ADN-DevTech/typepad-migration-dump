---
layout: "post"
title: "Cannot debug my Python add-in"
date: "2019-10-02 18:33:35"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/10/cannot-debug-my-python-add-in.html "
typepad_basename: "cannot-debug-my-python-add-in"
typepad_status: "Publish"
---

<p>Just wrote <a href="https://modthemachine.typepad.com/my_weblog/2019/09/debug-fusion-360-add-ins.html">an article</a> on how to debug your <strong>Python</strong> script/add-in</p>
<p>One thing missing from it is about what to do if the compiler runs into problems with the main module (vs an additional module your code is loading). <br />In that case, checking &quot;<strong>Raised Exceptions</strong>&quot; in the &quot;<strong>Breakpoints</strong>&quot; section is not enough 🙁<br />Your code will fail silently and the debug process won&#39;t start</p>
<p>You can work around that by running <strong>Python</strong> compile on your main module/main <strong>Python</strong> file to find out what the problem is with it.</p>
<p>Once you started debugging your code from <strong>Fusion 360</strong> and ended up in <strong>VS Code</strong>, you can follow these steps:</p>
<p>1) Open up the <strong>Terminal</strong> in <strong>VS Code</strong> (View &gt;&gt; Terminal)</p>
<p>2) Open the <strong>.vscode\settings.json</strong> file of your add-in - this will have the path to the <strong>Python</strong> version that <strong>Fusion 360</strong> is using</p>
<p>3) Copy the full path set for &quot;<strong>python.pythonPath</strong>&quot; into the terminal</p>
<p>4) Add &quot;<strong>-m py_compile</strong>&quot; to the text in the <strong>Terminal</strong> window</p>
<p>5) <strong>Drag&amp;drop</strong> your <strong>Python</strong> file&#39;s <strong>tab</strong> into the <strong>Terminal</strong> - that will copy/paste the full path of that file in the <strong>Terminal</strong></p>
<p>6) Now just hit <strong>enter</strong> in the <strong>Terminal</strong> and that should run the <strong>Python</strong> compiler, which should list the first error the <strong>compiler</strong> runs into&#0160;</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4b4da6c200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DebugPython1" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4b4da6c200d image-full img-responsive" src="/assets/image_820713.jpg" title="DebugPython1" /></a></p>
<p>Let&#39;s compare the error and our <strong>Python</strong> file:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a48b8f12200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DebugPython2" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a48b8f12200c image-full img-responsive" src="/assets/image_470964.jpg" title="DebugPython2" /></a></p>
<p>In this case, the error message is not the clearest, but at least we have something to go on 🙂<br />Solution to this specific error is here: <a href="https://modthemachine.typepad.com/my_weblog/2019/10/issues-with-migrating-to-python-373.html">Issues with migrating to Python 3.7.3</a></p>
