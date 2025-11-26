---
layout: "post"
title: "Avoid DG evaluation caused by maya.cmds when there are python custom nodes in the scene in parallel mode"
date: "2016-07-27 19:37:20"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Concurrent programming"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2016/07/avoid-dg-evaluation-caused-by-mayacmds-when-there-are-python-custom-nodes-in-the-scene-in-parallel-mode.html "
typepad_basename: "avoid-dg-evaluation-caused-by-mayacmds-when-there-are-python-custom-nodes-in-the-scene-in-parallel-mode"
typepad_status: "Publish"
---

<p>I ran into an issue about Maya 2016 extension 2 hanging earlier this week. The customer has a custom locator node in their scene and Maya hangs when running code like below:</p>  <pre><code>import maya.cmds as cmds

cmds.evaluationManager(mode=&quot;parallel&quot;)

for i in range(10):
    cmds.currentTime(i)
</code></pre>

<p>After spending some time for debugging, the issue was found. It turns out that when Maya is executing Python, it will acquire a Python lock for the interpreter. During the parallel evaluation of your scene caused by cmds.currentTime, another working thread is created and tries to acquire the same lock, resulting in a deadlock scenerio. In Maya 2017, it only happens when MAYA_RETAIN_PYTHON_GIL <em>is set in your system environment. </em>MAYA_RETAIN_PYTHON_GIL is for the users who are experiencing performance issues when executing high volumes of python commands. Maya will be more prone to deadlocks if custom Python nodes are evaluating in other threads.</p>

<p>To avoid the deadlock, please use Mel command only if you are going to evaluate the scene. You could also try to rewrite your nodes in C++ to avoid sharing a lock with the Python interpreter.</p>
