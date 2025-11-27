---
layout: "post"
title: "Suppress constraints with similar name"
date: "2015-01-29 04:25:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/suppress-constraints-with-similar-name.html "
typepad_basename: "suppress-constraints-with-similar-name"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If components (e.g. constraints) with names that are the same until the last number part are logically connected, and so you want to find them, then the easiest might be to use <a href="http://en.wikipedia.org/wiki/Regular_expression" target="_self">Regular Expressions</a>.</p>
<p>In this <strong>VBA</strong> sample we check the pattern or main part of the constraint&#39;s name so that we can match it against other constraints. If we find such constraints we suppress those too:</p>
<pre>Sub SuppressConstraintsWithNamePattern()
  Dim doc As AssemblyDocument
  Set doc = ThisApplication.ActiveDocument
  
  &#39; Get the currently selected constraint
  Dim c As AssemblyConstraint
  Set c = doc.SelectSet(1)
  
  &#39; Easiest is to use Regular Expressions
  &#39; to get the ending number.
  &#39; E.g. if we have &quot;Con1_23_12&quot; it
  &#39; will return &quot;Con1_23_&quot; and &quot;12&quot;,
  &#39; for &quot;Con:12:32&quot; it returns
  &#39; &quot;Con:12:&quot; and &quot;32&quot;
  &#39; Useful web pages:
  &#39; https://www.udemy.com/blog/vba-regex/
  &#39; http://www.rubular.com/r/qMjioXRiOG
  Set re = CreateObject(&quot;vbscript.regexp&quot;)
  re.pattern = &quot;^(.*?)([0-9]*)$&quot;
  
  &#39; If we run into an error we just continue
  On Error Resume Next
  
  Dim matches As Object
  Set matches = re.Execute(c.Name)
  
  Dim pattern As String
  pattern = matches(0).SubMatches(0)
  
  &#39; Nothing to do if no pattern found
  &#39; in name of constraint
  If pattern = &quot;&quot; Then Exit Sub
  
  &#39; Now iterate through the other
  &#39; constraints to see if any exists
  &#39; with a similar name
  For Each c In doc.ComponentDefinition.Constraints
    Set matches = re.Execute(c.Name)
    
    Dim s As String
    s = matches(0).SubMatches(0)
    
    &#39; If its name has the same pattern then
    &#39; suppress it
    If s = pattern Then c.Suppressed = True
  Next
End Sub</pre>
<p>When running the code we get this:</p>
<p><a class="asset-img-link" href="http://a2.typepad.com/6a0112791b8fe628a401b7c7414082970b-pi" style="display: inline;"><img alt="Regex" class="asset  asset-image at-xid-6a0112791b8fe628a401b7c7414082970b img-responsive" src="/assets/image_acbb6e.jpg" style="width: 420px;" title="Regex" /></a></p>
<p>&#0160;</p>
