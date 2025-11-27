---
layout: "post"
title: "Get COM object type name programmatically"
date: "2015-06-11 09:58:49"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/get-com-object-type-name-programmatically.html "
typepad_basename: "get-com-object-type-name-programmatically"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In different programming languages you can get the name of the object&#39;s type in different ways.</p>
<p><strong>VBA, VB, VB.NET</strong></p>
<p>Here you can use the <a href="https://msdn.microsoft.com/en-us/library/5422sfdf(v=vs.90).aspx" target="_self"><strong>TypeName</strong></a> built-in function and just pass in the object whose type name you want to know</p>
<p><strong>C#</strong></p>
<p>The <strong>object.GetType().Name</strong>&#0160;property only return&#0160;<strong>__ComObject </strong>for&#0160;<strong>COM</strong> objects, and not the underlying type, so it is not that useful.<br />In <strong>C#</strong> you can either declare the <strong>ITypeInfo</strong> object and use that like in <a href="http://adndevblog.typepad.com/manufacturing/2013/03/replace-dimension-style.html" target="_self">this</a> article, or just take advantage of the <strong>VB.NET</strong> functionality by adding a reference to <strong>Microsoft.VisualBasic</strong>&#0160;assembly and use&#0160;<strong>Microsoft.VisualBasic.Information.TypeName()</strong> function.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0840fd7a970d-pi" style="display: inline;"><img alt="TypeName" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0840fd7a970d image-full img-responsive" src="/assets/image_8c4e6a.jpg" title="TypeName" /></a></p>
<p><strong>C++</strong></p>
<p>Here you do need to use <strong>ITypeInfo</strong> interface as shown in <a href="http://adndevblog.typepad.com/manufacturing/2013/04/check-geometry-type-in-c.html" target="_self">this</a> article.</p>
<p><strong>Note:</strong> most <strong>Inventor</strong> object types also have a <strong>Type</strong> property that returns an <strong>ObjectTypeEnum</strong> which can also be useful when identifying object type.</p>
