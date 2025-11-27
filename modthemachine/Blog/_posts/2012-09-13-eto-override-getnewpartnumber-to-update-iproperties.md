---
layout: "post"
title: "ETO - Override GetNewPartNumber() to update iProperties"
date: "2012-09-13 22:39:59"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/09/eto-override-getnewpartnumber-to-update-iproperties.html "
typepad_basename: "eto-override-getnewpartnumber-to-update-iproperties"
typepad_status: "Publish"
---

<p>One of the products I am supporting currently is “Engineer-To-Order” (ETO). When I see a good tip or solution that applies to ETO for Inventor I am going to post it here. Here is the first post for this product and it shows a way to create a custom iProperty when the member file is saved.</p>
<p>When a member document is about to be saved the GetNewPartNumber() method is called. You can override this method and get and set data. (This is exactly the right time to do this). Geometry-wise the document is in a good shape, because it contains the required values of Inventor Parameters, sheet metal defaults, etc.&#0160;&#0160;&#0160; <br />&#0160; <br />The example below was used for a sheet metal part. The highlighted lines do the following:</p>
<p><br />1.&#0160;&#0160;&#0160; Extract the “lengths” of the Flat Pattern as an Intent List <br />2.&#0160;&#0160;&#0160; Formats a string from the List <br />3.&#0160;&#0160;&#0160; Creates the custom iProperty FlatPatternSize in the member IPT document</p>
<p>&#0160;</p>
<p>&lt; %%category(&quot;Inventor&quot;) &gt; _ <br />Method GetNewPartNumber( docHandle As String) As String <br />&#0160; <br /><span style="background-color: #cccccc;">&#0160;&#0160;&#0160;&#0160; Dim fpLengths As List =&#0160; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160; IvExtGetFlatPatternLenghts _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ( Parent,False,&quot;in&quot;, docHandle:=docHandle)&#0160; <br />&#0160;&#0160;&#0160;&#0160; Dim strList As String = &quot;FP Lengths: &quot; _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; + StringValueEx( fpLengths, :AsFormula)&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160; iv_documentPropertyPut _ <br />&#0160;&#0160;&#0160;&#0160; ( docHandle, &quot;FlatPatternSize&quot;, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strList, createNew?:=True)&#0160; <br /></span>&#0160; <br /><span style="color: #0000ff;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#39; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#39; the rest is original implementation from <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#39; %%IvAdoptedPart Design <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#39; <br /></span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Dim iPropertiesPN As String = _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; iv_ExtractPartNumber( iProperties) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; If( length(iPropertiesPN) &gt; 0) Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Return iPropertiesPN <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; End If <br />&#0160;&#0160;&#0160; If UseFactoryPartNumber? Then <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Return &quot;&quot; <br />&#0160;&#0160;&#0160; End If <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Return iv_pathFileNameOnly( %%cacheFilePath) <br />End Method</p>
<p>- Wayne&#0160;&#0160;</p>
