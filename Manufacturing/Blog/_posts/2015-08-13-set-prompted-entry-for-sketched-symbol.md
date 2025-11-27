---
layout: "post"
title: "Set Prompted Entry for Sketched Symbol"
date: "2015-08-13 02:59:05"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/08/set-prompted-entry-for-sketched-symbol.html "
typepad_basename: "set-prompted-entry-for-sketched-symbol"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can do it in a similar way to <strong>Borders</strong>:&#0160;<br /><a href="http://adndevblog.typepad.com/manufacturing/2012/07/getset-the-prompted-text-in-the-border-of-a-sheet-in-inventor-api.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2012/07/getset-the-prompted-text-in-the-border-of-a-sheet-in-inventor-api.html</a></p>
<p>We have the following drawing with a single <strong>SketchedSymbolDefinition</strong> and instance of it:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14853a0970c-pi" style="display: inline;"><img alt="SketchedSymbol1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d14853a0970c image-full img-responsive" src="/assets/image_f99261.jpg" title="SketchedSymbol1" /></a></p>
<p>If we select it in the <strong>UI</strong> then the following <strong>VBA</strong> code will change its <strong>PrompterEntry</strong> value to &quot;<strong>NewValue</strong>&quot;:</p>
<pre>Sub MofidyPromptedEntry()
  Dim oSS As SketchedSymbol
  Set oSS = ThisApplication.ActiveDocument.SelectSet(1)
  
  Dim oTB As TextBox
  For Each oTB In oSS.Definition.Sketch.TextBoxes
    If oTB.Text = &quot;&lt;MyPrompt&gt;&quot; Then
      Call oSS.SetPromptResultText(oTB, &quot;NewValue&quot;)
    End If
  Next
End Sub</pre>
<p>The result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14853ac970c-pi" style="display: inline;"><img alt="SketchedSymbol2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d14853ac970c image-full img-responsive" src="/assets/image_864d36.jpg" title="SketchedSymbol2" /></a></p>
<p>You could also use the same <strong>API</strong> from an&#0160;<strong>iLogic</strong>&#0160;rule:</p>
<pre>&#39; Set the value for the first sketched symbol
&#39; on the current sheet
Dim oSS As SketchedSymbol
oSS = ActiveSheet.Sheet.SketchedSymbols(1)

Dim oTB As TextBox
For Each oTB In oSS.Definition.Sketch.TextBoxes
  If oTB.Text = &quot;&lt;MyPrompt&gt;&quot; Then
    Call oSS.SetPromptResultText(oTB, &quot;NewValue&quot;)
  End If
Next</pre>
