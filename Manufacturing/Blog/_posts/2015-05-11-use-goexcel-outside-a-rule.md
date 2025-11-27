---
layout: "post"
title: "Use GoExcel outside a rule "
date: "2015-05-11 06:54:07"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/use-goexcel-outside-a-rule.html "
typepad_basename: "use-goexcel-outside-a-rule"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>It’s possible to call <strong>GoExcel</strong> directly, but it’s not documented and we don’t support it. For <strong>Excel</strong> functionality you can use the <strong>Excel COM API</strong>&#0160;directly instead of going through&#0160;<strong>iLogic</strong>. That&#39;s what <strong>iLogic</strong> is using too and that should provide access to all <strong>Excel</strong> functionality, not only those exposed in&#0160;<strong>iLogic</strong>.&#0160;</p>
<p>If you want to use <strong>GoExcel</strong> from anywhere (<strong>.NET AddIn</strong> or even external application) then you could wrap the functionality you need inside e.g. an external rule with arguments. The arguments are in the form of an Inventor <strong>NameValueMap</strong>. You can use this “rule with arguments” method to indirectly call any of the <strong>iLogic</strong> functions from an exe or addin.</p>
<p>Here’s a sample rule with some <strong>GoExcel</strong> code:</p>
<pre>&#39; In this case the code is saved 
&#39; in &quot;C:\Test3\iLogicExcelTest.iLogicVb&quot;
Dim filePath As String = RuleArguments(&quot;FilePath&quot;)
Dim sheetName As String = RuleArguments(&quot;Sheet&quot;)
Dim dia As Double = RuleArguments(&quot;Dia&quot;)
Dim length As Double = RuleArguments(&quot;Length&quot;)
 
Dim rowNum As Integer = GoExcel.FindRow( _
  filePath, sheetName, &quot;Dia&quot;, &quot;=&quot;, dia, &quot;Length&quot;, &quot;=&quot;, length)
 
If (rowNum &gt; 0) Then
  Dim outputs As NameValueMap = _
    ThisServer.TransientObjects.CreateNameValueMap()
  outputs.Add(&quot;PartNumber&quot;, GoExcel.CurrentRowValue(&quot;PartNumber&quot;))
 
  RuleArguments.Arguments.Add(&quot;Outputs&quot;, outputs)
End If</pre>
<p>And here’s sample code to run that rule from e.g. a <strong>VB exe</strong> or <strong>addin</strong>:</p>
<pre>Public Sub Test()
 
  &#39; m_app is a member variable of type Inventor.Application
  Dim arguments As NameValueMap = _
    m_app.TransientObjects.CreateNameValueMap()
 
  arguments.Add(&quot;FilePath&quot;, &quot;C:\Test3\Data.xlsx&quot;)
  arguments.Add(&quot;Sheet&quot;, &quot;Sheet1&quot;)
  arguments.Add(&quot;Dia&quot;, 0.2)
  arguments.Add(&quot;Length&quot;, 4.5)
 
  Dim document As Document = m_app.ActiveDocument
  Dim externalRulePath = &quot;C:\Test3\iLogicExcelTest.iLogicVb&quot;

  &#39; m_iLogicAuto is a late-bound variable of type Object. 
  &#39; It implements the IiLogicAutomation interface 
  &#39; (defined in Autodesk.iLogic.Interfaces.dll). 
  &#39; See below GetiLogicAutomation() function to get 
  &#39; it from the application
  m_iLogicAuto.RunExternalRuleWithArguments( _
    document, externalRulePath, arguments)
 
  Dim outputs As NameValueMap = Nothing
  Try
    outputs = arguments.Value(&quot;Outputs&quot;)
    If (outputs IsNot Nothing) Then
      MessageBox.Show( _
        String.Format(&quot;PartNumber = {0}&quot;, _
        outputs.Value(&quot;PartNumber&quot;)), &quot;ExcelTest&quot;)
    End If
  Catch ex As Exception
    MessageBox.Show(String.Format(&quot;Exception in Test: {0}&quot;, ex), _
      &quot;ExcelTest&quot;)
  End Try
End Sub</pre>
<pre>Function GetiLogicAutomation(ByVal app As Inventor.Application) _
As Object
 
  Dim addIn As Inventor.ApplicationAddIn = Nothing
  Try
    addIn = app.ApplicationAddIns.ItemById( _
      &quot;{3bdd8d79-2179-4b11-8a5a-257b1c0263ac}&quot;)
  Catch ex As Exception
    Return Nothing
  End Try
 
  Return addIn.Automation
End Function</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb082c6877970d-pi" style="display: inline;"><img alt="GoExcel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb082c6877970d image-full img-responsive" src="/assets/image_12e4c6.jpg" title="GoExcel" /></a></p>
<p>&#0160;</p>
