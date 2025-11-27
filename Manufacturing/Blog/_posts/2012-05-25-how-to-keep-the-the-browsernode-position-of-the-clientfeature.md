---
layout: "post"
title: "How to keep the the BrowserNode position of the ClientFeature"
date: "2012-05-25 16:27:54"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/how-to-keep-the-the-browsernode-position-of-the-clientfeature.html "
typepad_basename: "how-to-keep-the-the-browsernode-position-of-the-clientfeature"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p><strong>Issue</strong></p>
<p>I create a ClientFeature which contains some elements. I then remove the elements in the ClientFeature and add new elements.&#0160; But the BrowserNode position of the ClientFeature changes to the node position of the first element. Is there a way to keep the the BrowserNode of the ClientFeature at its original position?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>As of the 2009 release the only way to achieve this is to put the BrowserNode of&#0160; the first element right after that of the ClientFeature first and then add the element .</p>
<p>The VB.NET code below shows how this is done. Use the attached part file (link below) with this example. There is a ClientFeature which does not have any elements.&#0160; The code will add ExtrudeFeatures(4), ExtrudeFeatures(5) and all ResolveFeatures. First the elements are added to an object array, then moved their node right after the node of ClientFeature one by one, and the elements are added one by one.</p>
<div style="font-family: consolas; background: white; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Sub</span> CreateClientFeature()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_inventorApp <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">Application</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_quitInventor <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span> = <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Try to get an active instance of Inventor</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Runtime.InteropServices _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .<span style="color: #2b91af;">Marshal</span>.GetActiveObject _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">Exception</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; If not active, create a new Inventor session</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> m_inventorApp <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> inventorAppType <span style="color: blue;">As</span> <span style="color: #2b91af;">Type</span> = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">Type</span>.GetTypeFromProgID _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.<span style="color: #2b91af;">Activator</span>. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateInstance(inventorAppType)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_quitInventor = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = m_inventorApp.ActiveDocument</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDef <span style="color: blue;">As</span> <span style="color: #2b91af;">PartComponentDefinition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDef = oDoc.ComponentDefinition</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oElements() <span style="color: blue;">As</span> <span style="color: blue;">Object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Check if ClientFeature allready exist&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> oDef.Features.ClientFeatures.Count &gt; 0 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;ReDim oElements(1 To 1)&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ReDim</span> oElements(0 <span style="color: blue;">To</span> 1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElements(0) = oDef.Features. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExtrudeFeatures.Item(4)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ReDim</span> <span style="color: blue;">Preserve</span> oElements _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (0 <span style="color: blue;">To</span> UBound(oElements) + 1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElements(1) = oDef.Features _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .ExtrudeFeatures.Item(5)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElements(2) = oDef.Features. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ExtrudeFeatures.Item(6)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oFeature <span style="color: blue;">As</span> <span style="color: #2b91af;">PartFeature</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> oFeature <span style="color: blue;">In</span> oDef.Features</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">TypeOf</span> oFeature <span style="color: blue;">Is</span> <span style="color: #2b91af;">RevolveFeature</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ReDim</span> <span style="color: blue;">Preserve</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElements(0 <span style="color: blue;">To</span> UBound _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oElements) + 1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElements(UBound(oElements)) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = oFeature</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;get the current BrowserPane, such as </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; in this case, the pane is &quot;PmDefault&quot;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPane <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserPane</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPane = oDoc.BrowserPanes.Item(<span style="color: #a31515;">&quot;PmDefault&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Use existing one&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oClFeat <span style="color: blue;">As</span> <span style="color: #2b91af;">ClientFeature</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oClFeat = oDoc.ComponentDefinition _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .Features.ClientFeatures.Item(1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; get the BrowserNodeDefinition </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; of ClientFeature&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oClientBrowserNodeDef <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">BrowserNodeDefinition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oClientBrowserNodeDef = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oClFeat.BrowserNode _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .BrowserNodeDefinition</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; ClientFeature provides its BrowserNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; directly, however when I used it, </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; there is some issue. So I have to </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; use its BrowserNodeDefinition to get&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; BrowserNode.&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oClientBrowserNode <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oClientBrowserNode = oPane.TopNode. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AllReferencedNodes _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oClientBrowserNodeDef).Item(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; move position of element node one by one </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; and Add them one by one&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oFIndex <span style="color: blue;">As</span> <span style="color: blue;">Integer</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;For oFIndex = 1 To UBound(oElements, 1)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">For</span> oFIndex = 0 <span style="color: blue;">To</span> UBound(oElements, 1)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oEveryObj <span style="color: blue;">As</span> <span style="color: blue;">Object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEveryObj = oElements(oFIndex)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; get the BrowserNodeDefinition </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; of the element, because for most </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; object, there is no direct way</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; to get its BrowserNode, we have </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; to get its BrowserNodeDefinition </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; first by </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; GetNativeBrowserNodeDefinition.&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oElementBrowserNodeDef <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">BrowserNodeDefinition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElementBrowserNodeDef = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.BrowserPanes _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .GetNativeBrowserNodeDefinition _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oEveryObj)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oElementBrowserNode <span style="color: blue;">As</span> <span style="color: #2b91af;">BrowserNode</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElementBrowserNode = oPane.TopNode _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .AllReferencedNodes _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oElementBrowserNodeDef).Item(1)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; put the element node near after the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; node of ClientFeature&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oPane.Reorder _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oClientBrowserNode, <span style="color: blue;">False</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oElementBrowserNode)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;add the element&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Call</span> oClFeat.Definition. _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ClientFeatureElements.Add _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oEveryObj, <span style="color: blue;">False</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create a new one&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> begin <span style="color: blue;">As</span> <span style="color: blue;">Object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; begin = oDef.Features.ExtrudeFeatures.Item(2)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oCDF <span style="color: blue;">As</span> <span style="color: #2b91af;">ClientFeatureDefinition</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oClientFeature <span style="color: blue;">As</span> <span style="color: #2b91af;">ClientFeature</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oCDF = oDef.Features.ClientFeatures _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .CreateDefinition(<span style="color: #a31515;">&quot;TestFeature&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; begin, begin, <span style="color: blue;">Nothing</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oClientFeature = oDef.Features _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .ClientFeatures.Add _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oCDF, <span style="color: #a31515;">&quot;TestClientFeatureID&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;</span></p>
<p style="margin: 0px;"><span style="color: blue;"><span class="asset  asset-generic at-xid-6a0167607c2431970b016766cb0e48970b"><a href="http://adndevblog.typepad.com/files/clientfeature_browsernode-2.ipt">Download ClientFeature_BrowserNode</a></span></span></p>
</div>
