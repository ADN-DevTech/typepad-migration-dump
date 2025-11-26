---
layout: "post"
title: "RevitAPI: Create FamilyInstance on a wall face"
date: "2015-06-02 04:25:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/06/revitapi-create-familyinstance-on-a-wall-face.html "
typepad_basename: "revitapi-create-familyinstance-on-a-wall-face"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/45825713">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>A customer has a headache to create family instance on wall, he tried several combinations of Document.Create.NewFamilyInstance but got nothing.</p>
<p>Here is what he wants, place a light fixture on wall:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb083023e4970d-pi" style="display: inline;"><img alt="NewFamilyInstanceOnFace_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb083023e4970d img-responsive" src="/assets/image_439662.jpg" title="NewFamilyInstanceOnFace_1" /></a></p>
<p>I found when we place them by manual, it is always on a face of the wall, so we can get rid of other overloads, but focus only on the following:</p>
<pre class="csharp prettyprint">NewFamilyInstance(Reference, Line position, FamilySymbol)
NewFamilyInstance(Reference, XYZ location, XYZ referenceDirection, FamilySymbol)
NewFamilyInstance(Face, Line position, FamilySymbol)
NewFamilyInstance(Face, XYZ location, XYZ referenceDirection, FamilySymbol)</pre>
<p>And since it is placed by point, so I decided to use this method:</p>
<pre class="csharp prettyprint">NewFamilyInstance(Reference, XYZ location, XYZ referenceDirection, FamilySymbol)</pre>
<p><br /> As you can see, the first argument Reference should be the reference of a face on the wall, the second is the location, the third referenceDirection is the rotation of the instance, and last one FamilySymbol is the symbol.</p>
<p>But how to get face reference?</p>
<p>Fortunately, there is a class named HostObjectUtils, which has several convinient methods to get faces of host objects, for example, GetSideFaces is used to get side faces of Wall:</p>
<pre class="csharp prettyprint">var reference = HostObjectUtils.GetSideFaces(wall, 
    ShellLayerType.Interior).First();</pre>
<p>ShellLayerType is to designate whethere it is exterior face or interior face. Call .First() is to get the first face, I believe there is only one face in one side :).</p>
<p>Now it is simple, whole code looks like this:</p>
<pre class="csharp prettyprint">FamilySymbol familySymbol = GetFamilySymbol(&quot;Switch board&quot;);
ElementId elementId = new ElementId(308696);
Wall wall = RevitDoc.GetElement(elementId) as Wall;
var reference = HostObjectUtils.GetSideFaces(wall, 
    ShellLayerType.Interior).First();
if (wall != null)
{
    var instance = RevitDoc.Create.NewFamilyInstance(reference, 
        new XYZ(13.3997604933275,-1.35161192601243,9.0571329707487),
        new XYZ(0,0,0),
        familySymbol);
    if (instance == null)
        TaskDialog.Show(&quot;ERROR&quot;, &quot;Instance is null!&quot;);
}</pre>
<p>We don&#39;t want to rotate the instance, so referenceDirection is set to (0,0,0).</p>
<p>But new problem comes out, we found difference between the one created by API and the one by manual</p>
<p>Here is the one by manual:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78c288f970b-pi" style="display: inline;"><img alt="NewFamilyInstanceOnFace_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c78c288f970b image-full img-responsive" src="/assets/image_906972.jpg" title="NewFamilyInstanceOnFace_3" /></a></p>
<p>And here is the one by API:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb083023f2970d-pi" style="display: inline;"><img alt="NewFamilyInstanceOnFace_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb083023f2970d image-full img-responsive" src="/assets/image_830353.jpg" title="NewFamilyInstanceOnFace_2" /></a><br /><br /> <br /> You should find that the elevation parameter is disabled in the second image but not in the first one.</p>
<p>So how to solve it?</p>
<p>Inspecting again, I found there is another difference, the &quot;Schedule Level&quot;, the one created manually has a &quot;Schedule Level&quot; by default, but API created one has nothing set.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d115bd41970c-pi" style="display: inline;"><img alt="NewFamilyInstanceOnFace_5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d115bd41970c img-responsive" src="/assets/image_120097.jpg" title="NewFamilyInstanceOnFace_5" /></a></p>
<p>So now, we&#39;ve got a solution: change the parameter &quot;Schedule Level&quot; to a real level after the instance is created. Cheers!</p>
<pre class="csharp prettyprint">Parameter parameter = instance.get_Parameter(
    BuiltInParameter.INSTANCE_SCHEDULE_ONLY_LEVEL_PARAM);
if (parameter != null)
{
    parameter.Set(new ElementId(311));
}</pre>
<p>&#0160;</p>
