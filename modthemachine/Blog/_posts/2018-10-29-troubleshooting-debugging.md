---
layout: "post"
title: "Troubleshooting, debugging"
date: "2018-10-29 13:38:19"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2018/10/troubleshooting-debugging.html "
typepad_basename: "troubleshooting-debugging"
typepad_status: "Publish"
---

<p>Sometimes we get code samples with hundreds of lines of code in <strong>iLogic</strong> where the last part fails. Even if it&#39;s not <strong>iLogic</strong>, it&#39;s still better to narrow down things to the specific function that is not working and create a test that focuses on it.</p>
<p>This will make it much easier for others to help you (e.g. on the forum) and might enable you to spot the issue with your code (if it&#39;s not a problem with the <strong>API</strong> itself)</p>
<p>For example, if your code is creating all the geometry that you want to add a feature to and that last bit fails for some reason, then <br />- remove the feature creation you have problems with<br />- run the rest of the code to create the geometry that the feature needs<br />- save that part/assembly and create a function that focuses on the feature that you&#39;re having problems with creating<br /><br />The best is to do that in a programming environment that allows you to step through code and investigate the variables you are using. The simplest way for everyone to run your code in is&#0160;<strong>VBA</strong> - if you create a <strong>.NET</strong> project (whether <strong>add-in</strong> or <strong>stand-alone app</strong>) not everyone has <strong>Visual Studio</strong> installed to run it, and if it&#39;s <strong>iLogic</strong> then you have limited debugging options.</p>
<p>Let&#39;s say we run into issues with <strong>KnitFeatures.Add()</strong> when running this sample code:&#0160;<a href="https://help.autodesk.com/view/INVNTOR/2018/ENU/?guid=GUID-0A0F1E4A-FC5E-4241-8E1C-C0A617C47554">Adding a new stitch (knit) feature API Sample</a>&#0160;</p>
<p>I can just remove the last part about creating the <strong>KnitFeature</strong> object and run it:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc68834022ad37581d6200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="KnitFeature" class="asset  asset-image at-xid-6a00e553fcbfc68834022ad37581d6200c img-responsive" src="/assets/image_652910.jpg" title="KnitFeature" /></a><a class="asset-img-link" href="https://a4.typepad.com/6a0112791b8fe628a4022ad3bb3a44200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a></p>
<p>Now I have the geometry that the creation of the last feature needs, and can move the last part of the code into a separate <strong>VBA</strong> method that would only run the code that creates the <strong>KnitFeature</strong>:</p>
<pre>Sub StitchFeatureCreate_Test()
    Dim oPartDoc As PartDocument
    Set oPartDoc = ThisApplication.ActiveDocument

    Dim oCompDef As PartComponentDefinition
    Set oCompDef = oPartDoc.ComponentDefinition
    
    Dim oSurfaces As ObjectCollection
    Set oSurfaces = ThisApplication.TransientObjects.CreateObjectCollection
    
    oSurfaces.Add oCompDef.WorkSurfaces.Item(1)
    oSurfaces.Add oCompDef.WorkSurfaces.Item(2)
    
    Dim oKnitFeature As KnitFeature
    Set oKnitFeature = oCompDef.Features.KnitFeatures.Add(oSurfaces)
End Sub</pre>
<p>As discussed in <a href="https://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html">this blog post</a>, this way you can also examine the various objects you are using - you might e.g. realize that you are trying to pass in the wrong objects to the function, or some parameters are not correct.</p>
<p>If you happened to find an issue with the <strong>API</strong> then it will make it much easier for us to verify the problem on our side :)&#0160;</p>
<p>When it comes to debugging <strong>iLogic</strong> code, this might come handy too:&#0160;<br /><a href="https://adndevblog.typepad.com/manufacturing/2014/08/debug-ilogic.html">https://adndevblog.typepad.com/manufacturing/2014/08/debug-ilogic.html</a></p>
<p>-Adam</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
