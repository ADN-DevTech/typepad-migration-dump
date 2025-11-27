---
layout: "post"
title: "Import Data Exchange model "
date: "2022-04-12 21:49:43"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2022/04/import-data-exchange-model.html "
typepad_basename: "import-data-exchange-model"
typepad_status: "Publish"
---

<p>There is a new feature called <strong>Data Exchange</strong> that also has an <strong>API</strong> now available in <strong>public</strong> <strong>beta</strong> - see <a href="https://forge.autodesk.com/blog/data-exchange-released-forge-data-exchange-apis-now-available-public-beta">Data Exchange Released! Forge Data Exchange APIs now available in Public Beta</a></p>
<p>Through the user interface you can import such models in <strong>Inventor</strong> and you can also automate that process using the <strong>Inventor API.</strong></p>
<p>For testing purposes, the easiest thing is to find a previously created <strong>Data Exchange</strong> model in your <strong>Autodesk Construction Cloud</strong> account, check its id available under <strong>entityId</strong> in the <strong>URL</strong>, and assign that in the below code to the <strong>sUrn </strong>variable.</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834027880769f86200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Image (14)" border="0" class="asset  asset-image at-xid-6a00e553fcbfc68834027880769f86200d image-full img-responsive" src="/assets/image_483411.jpg" title="Image (14)" /></a></p>
<pre>Sub ImportDataExchangeComponentSample()
    Dim oDoc As AssemblyDocument
    Set oDoc = ThisApplication.ActiveDocument
    
    Dim sFdx As String
    sFdx = &quot;https://developer.api.autodesk.com/exchange/v1/exchanges&quot;
    
    Dim sUrn As String
    sUrn = &quot;urn:adsk.wipprod:dm.lineage:sTpS6wv0ROS_QQglZOXReQ&quot;
    
    Dim sUrl As String
    sUrl = sFdx + &quot;?filters=attribute.exchangeFileUrn==&quot; + sUrn
    
    Dim oDEDef As ImportedDataExchangeComponentDefinition
    Set oDEDef = oDoc.ComponentDefinition.ImportedComponents.CreateDataExchangeDefinition(sUrl)
    
    Dim oDEComp As ImportedDataExchangeComponent
    Set oDEComp = oDoc.ComponentDefinition.ImportedComponents.Add(oDEDef)
End Sub
</pre>
<p>&#0160;</p>
<p>-Adam</p>
