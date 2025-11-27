---
layout: "post"
title: "Automate Add-In's like BIM Exchange"
date: "2015-11-10 02:56:43"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/11/automate-add-ins-like-bim-exchange.html "
typepad_basename: "automate-add-ins-like-bim-exchange"
typepad_status: "Draft"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Unfortunately, just like most of the 3rd party add-in&#39;s, the ones provided by <strong>Autodesk</strong> do not usually provide an <strong>Automation</strong> interface either. That means that only the <a href="http://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html" target="_self">add-in commands can be executed at will</a>, but if they gather information from the user through a dialog then you have no direct access to those options.</p>
<p>One exception is the <strong>iLogic</strong>&#0160;add-in: <a href="http://adndevblog.typepad.com/manufacturing/2013/04/call-ilogic-from-net.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/04/call-ilogic-from-net.html</a>&#0160;and the <strong>BIM Exchange</strong> add-in.</p>
<p>To find out if a given functionality that you are trying to automate is provided by an add-in or not, you could simply unload all the add-in&#39;s in the <strong>Add-In Manager</strong> dialog:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088dc3a0970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Add-In Manager" class="asset  asset-image at-xid-6a0167607c2431970b01bb088dc3a0970d img-responsive" src="/assets/image_126f17.jpg" title="Add-In Manager" /></a></p>
<p>If the functionality stays available after that then it&#39;s an internal <strong>Inventor</strong> functionality and there is a good chance it can be automated.</p>
<p>If the functionality is provided by an add-in then you could check if the add-in provides an <strong>Automation</strong> interface through its <strong>Automation</strong> property: if it&#39;s something other than&#0160;<strong>Nothing</strong> then the functionality might be exposed through that interface. You could load the add-in&#39;s through the <strong>UI</strong> and then use this <strong>VBA</strong> code to do the check:</p>
<pre>Sub AddInAtomation()
  &#39; Before running this code load all the Add-In&#39;s that
  &#39; you are interested in through the &#39;Add-In Manager&#39; dialog
  &#39; Reason: some Add-In&#39;s seem to cause issues when loaded
  &#39; programmatically one after the other, so it&#39;s better
  &#39; to do it through the UI

  Dim addin As ApplicationAddIn
  For Each addin In ThisApplication.ApplicationAddIns
    If addin.activated Then
      Dim addinAutomation As Object
      Set addinAutomation = addin.Automation

      Debug.Print _
        &quot;Add-In &quot;&quot;&quot; + addin.DisplayName + _
        &quot;&quot;&quot; has Automation =&gt; &quot; + _
        TypeName(addinAutomation)
    Else
      Debug.Print _
        &quot;Add-In &quot;&quot;&quot; + addin.DisplayName + _
        &quot;&quot;&quot; not activated&quot;
    End If
  Next
End Sub</pre>
<p>You will get something like this when running the code - highlighted the add-in&#39;s with an interface:</p>
<pre>Add-In &quot;Translator: SAT&quot; not activated
Add-In &quot;Routed Systems: Tube &amp; Pipe&quot; not activated
Add-In &quot;Translator: STEP&quot; not activated
Add-In &quot;Translator: IGES&quot; not activated
Add-In &quot;Translator: CATIA V5 Product Export&quot; not activated
Add-In &quot;Translator: DWG&quot; has Automation =&gt; Nothing
Add-In &quot;Translator: DXF&quot; not activated
Add-In &quot;Assembly Bonus Tools&quot; not activated
<strong>Add-In &quot;iLogic&quot; has Automation =&gt; iLogicAutomation</strong>
Add-In &quot;Translator: STL Import&quot; not activated
Add-In &quot;Simulation: Dynamic Simulation&quot; not activated
Add-In &quot;Translator: DWF&quot; not activated
Add-In &quot;Translator: PDF&quot; not activated
Add-In &quot;Translator: DWFx&quot; not activated
Add-In &quot;Translator: CATIA V5 Part Export&quot; not activated
Add-In &quot;Mold Design&quot; not activated
Add-In &quot;Translator: RVT&quot; not activated
Add-In &quot;Translator: Parasolid Text&quot; not activated
Add-In &quot;EDM Addin&quot; has Automation =&gt; Nothing
Add-In &quot;BIM Simplify&quot; has Automation =&gt; Nothing
Add-In &quot;Suite Workflows: to 3ds Max&quot; has Automation =&gt; Nothing
Add-In &quot;Autodesk IDF Translator&quot; not activated
Add-In &quot;Simulation: Frame Analysis&quot; not activated
<strong>Add-In &quot;Content Center&quot; has Automation =&gt; ICCV2AddInServer</strong>
Add-In &quot;Translator: SolidWorks&quot; not activated
Add-In &quot;Translator: Pro/ENGINEER Granite&quot; not activated
Add-In &quot;Inventor Studio&quot; not activated
Add-In &quot;Additive Manufacturing&quot; not activated
Add-In &quot;Translator: NX&quot; not activated
Add-In &quot;Design Accelerator&quot; not activated
Add-In &quot;ExtraWindow&quot; not activated
Add-In &quot;ESKD Support&quot; not activated
Add-In &quot;Auto Limits&quot; not activated
Add-In &quot;Eco Materials Adviser&quot; has Automation =&gt; Nothing
Add-In &quot;Configurator 360&quot; not activated
Add-In &quot;Translator: SMT&quot; not activated
Add-In &quot;Design Checker&quot; not activated
Add-In &quot;Frame Generator&quot; not activated
Add-In &quot;Suite Workflows: to Showcase&quot; has Automation =&gt; Nothing
Add-In &quot;Translator: Parasolid Binary&quot; not activated
Add-In &quot;Autodesk i-drop Translator&quot; not activated
<strong>Add-In &quot;BIM Exchange&quot; has Automation =&gt; BIMExchangeServer</strong>
Add-In &quot;Routed Systems: Cable &amp; Harness&quot; not activated
Add-In &quot;iCopy&quot; not activated
Add-In &quot;Translator: Pro/ENGINEER and Creo Parametric&quot; not activated
Add-In &quot;Content Center Item Translator&quot; not activated
Add-In &quot;Translator: Pro/ENGINEER Neutral&quot; not activated
Add-In &quot;EnvSampleAddin&quot; has Automation =&gt; Nothing
Add-In &quot;Translator: CATIA V4 Import&quot; not activated
Add-In &quot;Translator: Rhino&quot; not activated
Add-In &quot;Electrical Catalog Browser&quot; not activated
Add-In &quot;Drag &amp; Drop Interoperability&quot; not activated
Add-In &quot;Translator: CATIA V5 Import&quot; not activated
Add-In &quot;Simulation: Stress Analysis&quot; not activated
Add-In &quot;Autodesk DWF Markup Manager&quot; not activated
Add-In &quot;Translator: JT&quot; not activated
Add-In &quot;Translator: Alias&quot; not activated
Add-In &quot;Translator: STL Export&quot; not activated</pre>
<p>&#0160;</p>
