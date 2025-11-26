---
layout: "post"
title: "Change a view&rsquo;s &ldquo;Annotation Crop&rdquo; parameter value"
date: "2013-11-17 23:53:08"
author: "Joe Ye"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2013/11/how-to-change-a-views-annotation-crop-parameter-value.html "
typepad_basename: "how-to-change-a-views-annotation-crop-parameter-value"
typepad_status: "Publish"
---

<p><strong></strong></p>  <p><a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p><strong>Question</strong>:Is a view's 'annotation crop&quot; exposed via the API? I see the properties cropBox, cropBoxActive, and cropBoxVisible -- but not similar properties for the annotation crop.</p>  <p><strong>Solution</strong></p>  <p>For easily access the view’s parameter value, Revit API exposes some properties for View class to get and set the parameter value. So developers can read and write parameters’ value in one line. However, not all the view’s parameters have a corresponding property under View class. In this situation, we can access directly access the target parameter, and change its value in the way of changing parameter value.</p>  <p>Here is the code fragment to change the Annotation Crop parameter value. it changes the current view’s Annotation Crop value.</p>  <p>&#160;&#160;&#160; Parameter param = doc.ActiveView.get_Parameter(BuiltInParameter.VIEWER_ANNOTATION_CROP_ACTIVE);    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Transaction trans = new Transaction(doc);     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; trans.Start(&quot;ChangeAnnotationCrop&quot;);     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; param.Set(1);     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; trans.Commit();</p>  <p>&#160;</p>  <p>For other kind of element, developer can use the same way to change a parameter value if the representing class doesn’t provide the corresponding property.</p>
