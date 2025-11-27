---
layout: "post"
title: "Issues with migrating to Python 3.7.3"
date: "2019-10-02 19:19:13"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
  - "Python"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/10/issues-with-migrating-to-python-373.html "
typepad_basename: "issues-with-migrating-to-python-373"
typepad_status: "Publish"
---

<p>As I mentioned it in <a href="https://modthemachine.typepad.com/my_weblog/2019/09/debug-fusion-360-add-ins.html">this blog post</a>, the new version of <strong>Python</strong> (3.7.3) that <strong>Fusion 360</strong> migrated to seems more strict, and is causing some <strong>add-ins</strong> to fail.</p>
<p>Here are the main issues I&#39;ve seen pop up:<br /><br />1) Having an empty code block: if/else, try/except, etc<br /><strong>Code:</strong></p>
<pre>try:
except:</pre>
<p><strong>Error:</strong></p>
<pre>Sorry: IndentationError: expected an indented block (PythonTest.py, line 25)</pre>
<p><strong>Solution:</strong> remove the whole block or just put a &quot;<strong>pass</strong>&quot; inside it:</p>
<pre>try:
  pass
except:
  pass</pre>
<p>2) Referencing a global variable multiple times in the same function using the &quot;<strong>global</strong>&quot; keyword - even if from different execution paths<br /><strong>Code:</strong></p>
<pre>my_global_variable = &quot;&quot;

def run(context):
  if ui:
    global my_global_variable
    my_global_variable = &quot;True&quot;
  else: 
    global my_global_variable
    my_global_variable = &quot;False&quot;</pre>
<p><strong>Error:</strong></p>
<pre>SyntaxError: name &#39;my_global_variable&#39; is assigned to before global declaration</pre>
<p><strong>Solution:</strong> use the &quot;<strong>global</strong>&quot; references at the beginning of the function&#0160;</p>
<pre>my_global_variable = &quot;&quot;

def run(context):
  global my_global_variable
  if ui:
    my_global_variable = &quot;True&quot;
  else: 
    my_global_variable = &quot;False&quot;</pre>
<p>3) One other issue, which is not necessarily a result of the compiler&#39;s strictness, is that you may be relying on&#0160;<strong>packages</strong> that are based on an earlier version of <strong>Python</strong>. If loading those modules is causing errors then you might have to upgrade those packages. It might be done in different ways depending on where you got those packages from.&#0160;</p>
