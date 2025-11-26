---
layout: "post"
title: "Setting Load Case parameter value"
date: "2012-06-18 18:31:54"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/06/setting-load-case-parameter-value.html "
typepad_basename: "setting-load-case-parameter-value"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p> <p>Setting Load case parameter value</p>  <p>If you see ‘Load Case’ parameter in Property window in UI, it is listed in dropdown list. However, this parameter is defined in the element Id and not in Integer index. You can find Load Case elements using the element filer. You can filter with LoadCase class and then Name property to find the one you need. You can then set the element&#160; Id as following where pointLoad is Point Load element and idLoadCase is the element Id of Load Case element.</p>  <p>pointLoad.Parameter(   <br />Parameters.BuiltInParameter.LOAD_CASE_ID).    <br />Set(IdLoadCase). </p>  <p>Please note the built-in parameter has been changed from LOAD_CASE_NUMBER to LOAD_CASE_ID for a more meaningful name.</p>  <p>You can easily can find these using RevitLookUp tool.</p>
