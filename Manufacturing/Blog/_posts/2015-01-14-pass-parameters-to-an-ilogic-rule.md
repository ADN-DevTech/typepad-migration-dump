---
layout: "post"
title: "Pass parameters to an iLogic Rule"
date: "2015-01-14 03:55:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/pass-parameters-to-an-ilogic-rule.html "
typepad_basename: "pass-parameters-to-an-ilogic-rule"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>In <a href="http://adndevblog.typepad.com/manufacturing/2013/09/run-ilogic-rule-from-external-application.html" target="_self">this</a> article we are accessing <strong>iLogic</strong> <strong>Rules</strong> from outside and run them using <strong>RunRule(&quot;componentName&quot;, &quot;ruleName&quot;)</strong>, which does not provide arguments to pass extra information to the <strong>Rule</strong>. There are other functions though that can be used. They are listed on <a href="http://help.autodesk.com/view/INVNTOR/2014/ENU/?guid=GUID-8DF6F761-1634-4D26-B13A-58AF275FD6F8" target="_self">this</a> help page. In case it moves somewhere else I copy here the info:</p>
<p><strong>Rule arguments </strong></p>
<p><strong>iLogic</strong> provides advanced functionality that allows you to pass context information as rule arguments into the rules you run. This information can be used to modify the behavior of a rule without having to create a duplicate rule.</p>
<p>You pass rule arguments using functions available in the <strong>IiLogicAutomation</strong> interface (for external clients), and in <strong>iLogicVB</strong> (for other rule code and internal clients). These arguments are made available within a rule via the <strong>RuleArguments</strong> property.</p>
<p>For <strong>IiLogicAutomation</strong>, the functions available include:</p>
<pre>Function <strong>RunRuleWithArguments</strong>(
  ByVal doc As Inventor.Document, 
  ByVal ruleName As String, 
  ByVal ruleArguments As Inventor.NameValueMap) As Integer 
Function <strong>RunExternalRuleWithArguments</strong>(
  ByVal doc As Inventor.Document, ByVal ruleName As String, 
  ByVal ruleArguments As Inventor.NameValueMap) As Integer 
Function <strong>RunRuleDirectWithArguments</strong>(
  ByVal rule As iLogicRule, 
  ByVal ruleArguments As Inventor.NameValueMap) As Integer</pre>
<p>For <strong>iLogicVB</strong>, the functions available include:</p>
<pre>Function <strong>RunRule</strong>(
  ByVal ruleName As String, 
  ByVal ruleArguments As Inventor.NameValueMap) As Integer 
Function <strong>RunRule</strong>(
  ByVal compoOrDocName As Object, ByVal ruleName As String, 
  ByVal ruleArguments As Inventor.NameValueMap) As Integer 
Function <strong>RunExternalRule</strong>(
  ByVal ruleName As String, 
  ByVal ruleArguments As Inventor.NameValueMap) As Integer</pre>
<p><strong>Create rule arguments </strong></p>
<p>To create rule arguments, use the Inventor API to create a new <strong>NameValueMap</strong> object. It is then passed to one of the functions when running the rule.</p>
<p><strong>Access an argument passed to the rule </strong></p>
<pre>x = RuleArguments(&quot;myArg&quot;)</pre>
<p><strong>Determine if an argument has been passed to the rule </strong></p>
<pre>If RuleArguments.Exists(&quot;myArg&quot;) Then...</pre>
<p><strong>Pass the set of arguments to another rule using RunRule </strong></p>
<pre>iLogicVB.RunRule(&quot;someOtherRule&quot;, RuleArguments.Arguments)</pre>
