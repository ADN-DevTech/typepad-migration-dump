---
layout: "post"
title: "Check if external iLogic rules are used"
date: "2023-12-13 12:43:47"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2023/12/check-if-external-ilogic-rules-are-used.html "
typepad_basename: "check-if-external-ilogic-rules-are-used"
typepad_status: "Publish"
---

<p>It might not be easy to check manually if a document and its referenced documents are relying on external iLogic rules or not.</p>
<p>They might be used from &quot;Event Triggers&quot; or called from other rules using the &quot;RunExternalRule&quot; function.</p>
<p>Here is a utility rule that can be used to help with this. The results will be printed in the &quot;iLogic Log&quot; panel.</p>
<pre>Imports System.Text.RegularExpressions
Sub Main()
  Dim doc As Inventor.Document
  doc = ThisDoc.Document

  CheckDocument(doc)
  For Each doc In doc.AllReferencedDocuments
    CheckDocument(doc)
  Next
End Sub

Sub CheckDocument(doc As Document)
  Logger.Debug(&quot;Checking document &quot; + doc.FullDocumentName)
  CheckEventTriggers(doc)
  CheckRules(doc)
End Sub

&#39; Check if any Event Trigger use an external rule
Sub CheckEventTriggers(doc As Document)
  Dim propSets As PropertySets = doc.PropertySets
  Dim iLogicPropSet As PropertySet

  Try
    iLogicPropSet = propSets.Item(&quot;{2C540830-0723-455E-A8E2-891722EB4C3E}&quot;)
  Catch
    Exit Sub
  End Try

  For Each prop As Inventor.Property In iLogicPropSet
    If Not prop.Value.StartsWith(&quot;file://&quot;) Then Continue For

    Logger.Debug(prop.Name + &quot; uses &quot; + prop.Value)
  Next
End Sub

&#39; Check if any rules try to execute an external rule
Sub CheckRules(doc As Document)
  Dim iLogicAuto = iLogicVb.Automation

  Dim rules = iLogicAuto.Rules(doc)
  If (rules Is Nothing) Then Exit Sub

  For Each rule In rules
    CheckRuleText(rule.Name, rule.Text)
  Next
End Sub

Sub CheckRuleText(ruleName As String, ruleText As String)
  &#39; Used https://regexr.com/ to figure out the Regex string
  Dim res = Regex.Matches(ruleText, &quot;(RunExternalRule\s*\(\s*&quot;&quot;)(.+)(\&quot;&quot;)&quot;)

  Dim externalRules = New List(Of String)
  For Each match As Match In res
    Try
      Dim name = match.Groups(2).Value
      If Not externalRules.Contains(name) Then
        externalRules.Add(name)
      End If
    Catch

    End Try
  Next

  If externalRules.Count &gt; 0 Then
    Dim str = String.Join(&quot;, &quot;, externalRules)
    Logger.Debug(ruleName + &quot; uses these external rules: &quot; + str)
  End If
End Sub</pre>
<p>And the result when running it on my assembly:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc6883402c8d3a15139200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CheckForExternalRuleReferences" border="0" class="asset  asset-image at-xid-6a00e553fcbfc6883402c8d3a15139200c image-full img-responsive" src="/assets/image_162320.jpg" title="CheckForExternalRuleReferences" /></a></p>
