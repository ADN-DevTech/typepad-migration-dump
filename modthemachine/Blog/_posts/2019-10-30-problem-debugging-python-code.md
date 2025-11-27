---
layout: "post"
title: "Problem debugging Python code"
date: "2019-10-30 21:48:04"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/10/problem-debugging-python-code.html "
typepad_basename: "problem-debugging-python-code"
typepad_status: "Publish"
---

<p>When using the latest two versions of the <strong>Python</strong> extension inside <strong>Visual Studio Code</strong>, then instead of <strong>VS Code</strong> showing up when you click &quot;<strong>Edit</strong>&quot; or &quot;<strong>Debug</strong>&quot; inside the&#0160; &quot;<strong>Script&#0160;and&#0160;Add-Ins</strong>&quot; dialog, another instance of <strong>Fusion 360</strong> will come up 😬</p>
<p>Fortunately, you can simply downgrade back to version <strong>2019.9.34911</strong> of the <strong>Python</strong> extension to solve the problem:<br />- go to the <strong>Extensions</strong> tab in <strong>VS Code</strong>&#0160;<br />- click on the <strong>Settings</strong> button ⚙ of the <strong>Python</strong> extension<br />- select &quot;<strong>Install Another Version...</strong>&quot;<br />- select <strong>2019.9.34911</strong></p>
<p><strong> <a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4e5a105200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PythonExtension" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4e5a105200b img-responsive" src="/assets/image_308234.jpg" title="PythonExtension" /></a><br /></strong></p>
