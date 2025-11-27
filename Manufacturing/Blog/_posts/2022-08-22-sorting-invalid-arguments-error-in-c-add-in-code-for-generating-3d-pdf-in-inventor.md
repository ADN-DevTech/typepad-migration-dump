---
layout: "post"
title: "Sorting “invalid arguments” error in C# add in code for generating 3D PDF in Inventor"
date: "2022-08-22 07:31:01"
author: "Fidel Makatia"
categories:
  - "Fidel Makatia"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2022/08/sorting-invalid-arguments-error-in-c-add-in-code-for-generating-3d-pdf-in-inventor.html "
typepad_basename: "sorting-invalid-arguments-error-in-c-add-in-code-for-generating-3d-pdf-in-inventor"
typepad_status: "Publish"
---

<p>The VB.NET add-in for 3D PDF generation works well and directly without issues. However, the C# API code throws an invalid arguments error at the <code>Publish()</code> method. This is because the code uses late binding.&#0160; This blog contains steps on how to resolve this error</p>
<p><br />Step 1:<br />As mentioned in this blog (<a href="https://modthemachine.typepad.com/my_weblog/2021/02/member-not-found-error-in-c-add-in.html">here</a>) set the Embed Interop Type to False under the Inventor Interop Assembly reference</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed21a64200d-pi" style="display: inline;"><img alt="Embed" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed21a64200d img-responsive" src="/assets/image_a00a34.jpg" title="Embed" /></a></p>
<p>Step 2:<br />Add the <em>Microsoft.CSharp</em> reference to your project then import the namespace in the code</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d497c8a200b-pi" style="display: inline;"><img alt="Reference" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d497c8a200b img-responsive" src="/assets/image_7f3225.jpg" title="Reference" /></a></p>
<p>&#0160;</p>
<p>Step 3:<br />Declare the PDF converter 3D ApplicationAddin Variable as dynamic as seen below<br /><code>dynamic oPDFConvertor3D ;</code><br /><code>oPDFConvertor3D = oPDFAddIn.Automation;</code></p>
<p>Step 4:<br />The <code>Publish()</code> method takes two arguments, the first is of type Inventor document and the second is of Inventor NameValueMap type. Declare the variable of the type document as <em>dynamic</em> before parsing it to the method.</p>
<p>See the sample code below</p>
<p><br /><code>dynamic oDocument;</code><br /><code>oDocument = m_inventorApplication.ActiveDocument;</code></p>
<p>These steps should be able to resolve the error.</p>
<p>- Fidel</p>
