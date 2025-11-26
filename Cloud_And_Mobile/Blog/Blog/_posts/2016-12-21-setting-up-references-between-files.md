---
layout: "post"
title: "Setting up external references between files"
date: "2016-12-21 11:38:21"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Data Management"
  - "Model Derivative"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/setting-up-references-between-files.html "
typepad_basename: "setting-up-references-between-files"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a>&#0160;(<a href="https://twitter.com/adamthenagy" target="_self">@AdamTheNagy</a>)</p>
<p>When the <strong>Data Management</strong> and <strong>Model Derivative API</strong>&#39;s got introduced, replacing the previous ones that were bundled with the <strong>Viewer</strong>, then you had to upload <strong>complex models</strong> in a <strong>zip&#0160;</strong>file. By complex model I mean those that consist of multiple files referencing each other. In case of <strong>AutoCAD DWG</strong> files these references would be called &quot;<strong>XREF&#39;s</strong>&quot; in case of others it&#39;s just &quot;<strong>File References</strong>&quot; or &quot;<strong>Referenced Documents</strong>&quot;</p>
<p>The <strong>good</strong> thing about using the <strong>zip</strong> approach is that it contains all the files needed in a single place and so the <strong>Model Derivative</strong> service can resolve all the references <strong>automatically</strong>. The <strong>bad</strong> thing about it is that you might have to upload the same file again and again in different zip files in order to create viewables that can be shown in the <strong>Viewer</strong>.</p>
<p>The <strong>Data Management API</strong> is now capable of setting up <strong>XREF</strong> (it&#39;s going with the <strong>AutoCAD</strong>&#0160;naming convention) relationships between files which will be honoured by the <strong>Model Derivative API</strong> when it&#39;s doing a translation.</p>
<p><strong>Note</strong>: relationships can only be set up between files that reside in the same <strong>A360</strong> (or&#0160;<strong>Fusion Team</strong>, <strong>BIM 360 Team</strong>) <strong>Project</strong>.</p>
<p>To show how it works I uploaded an <strong>assembly</strong> and a referenced <strong>part</strong> document to <a href="https://a360.autodesk.com/">A360</a>. As you&#39;d expect, the translation of the <strong>part</strong> file <strong>succeeded</strong>, but the <strong>assembly</strong>&#39;s <strong>failed</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09626a0e970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PartVsAssembly" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09626a0e970d image-full img-responsive" src="/assets/image_c2f88b.jpg" title="PartVsAssembly" /></a></p>
<p>If you check with a sample app like <a href="http://derivatives.autodesk.io/">http://derivatives.autodesk.io/</a>&#0160;then you&#39;ll run into the same issue, that the translation fails for the assembly:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096262a8970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="TranslationFailed" class="asset  asset-image at-xid-6a0167607c2431970b01bb096262a8970d img-responsive" src="/assets/image_c892cc.jpg" title="TranslationFailed" /></a></p>
<p>With that sample you can also check the manifest of the file in the <strong>Console</strong> window of the browser and you&#39;ll find the reason why the translation failed. Which is, as expected, that the part file could not be found:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096269cc970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="TranslationFailedMessage" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb096269cc970d image-full img-responsive" src="/assets/image_2aa121.jpg" title="TranslationFailedMessage" /></a></p>
<p>Using the <a href="https://developer.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-versions-version_id-relationships-refs-POST/">POST /relationships/refs</a> endpoint of the given file version on <strong>A360</strong> you can add&#0160;<br /><strong>xrefs:autodesk.core:Xref</strong>&#0160;relationship from the assembly to the part, which will be honoured by the <strong>Model Derivative</strong>&#0160;<strong>API&#0160;</strong>when doing the translation and looking for the part.&#0160;</p>
<p>The version id of the files I&#39;m using:<br /><strong>AssemblyTest.iam (v1)</strong>: urn:adsk.wipprod:fs.file:vf.cwgYCF5LQOShkzKA414Zbw?version=1&#0160;<br /><strong>AssemblyTest.ipt (v1)</strong>:&#0160;urn:adsk.wipprod:fs.file:vf.E0ObBbVgQP-Nko5AfMNHDw?version=1</p>
<p>Using those we can send this <strong>POST</strong> request:<br />https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6dWUyOWM4YjZiIzIwMTQxMTI4NjQ1MzQwNg/versions/urn%3Aadsk.wipprod%3Afs.file%3Avf.cwgYCF5LQOShkzKA414Zbw%3Fversion%3D1/<strong>relationships/refs</strong></p>
<p>Body:</p>
<pre>{
  &quot;jsonapi&quot;: {
    &quot;version&quot;: &quot;1.0&quot;
  },
  &quot;data&quot;: {
    &quot;type&quot;: &quot;versions&quot;,
    &quot;id&quot;: &quot;urn:adsk.wipprod:fs.file:vf.E0ObBbVgQP-Nko5AfMNHDw?version=1&quot;,
    &quot;meta&quot;: {
      &quot;extension&quot;: {
        &quot;type&quot;: &quot;xrefs:autodesk.core:Xref&quot;,
        &quot;version&quot;: &quot;1.0&quot;,
        &quot;data&quot;: {}
      }
    }
  }
}</pre>
<p>Now we need to ask for the <strong>SVF</strong> translation of the assembly again. In order to do that you can either use the <strong>x-ads-force: true</strong> header value when <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/">POST-ing the job</a>, &#0160;or <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-DELETE/">delete the manifest</a> of the assembly before asking for the translation (that would delete the result of other jobs as well though).</p>
<p>After that&#0160;the translation succeeded:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2493cec970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AssemblyWorking1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2493cec970c img-responsive" src="/assets/image_957e4c.jpg" title="AssemblyWorking1" /></a></p>
<p>And now the relationship shows up in <strong>A360</strong> as well:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8bf7a42970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AssemblyWorking2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8bf7a42970b img-responsive" src="/assets/image_2a00b1.jpg" title="AssemblyWorking2" /></a></p>
<p>&#0160;</p>
