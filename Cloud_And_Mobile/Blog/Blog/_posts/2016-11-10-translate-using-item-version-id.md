---
layout: "post"
title: "Translate using Item Version id"
date: "2016-11-10 05:01:54"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Model Derivative"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/11/translate-using-item-version-id.html "
typepad_basename: "translate-using-item-version-id"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a>&#0160;(<a href="https://twitter.com/adamthenagy" target="_self">@AdamTheNagy</a>)</p>
<p>In order to get a file on <strong>A360</strong> translated and show it in the viewer, so far you needed to drill down to the file on <strong>OSS</strong> (Object Storage System) and use its id/urn to get it translated using the <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/overview/">Model Derivative API</a>. Just like it&#39;s shown in <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/07/show-an-a360-file-in-viewer.html">this</a> blog post.</p>
<p>Now you can simply use the <strong>id</strong> of the <strong>Item Version</strong>. The main benefit of this is that this way you have access to the translation already requested by&#0160;the <strong>A360 UI</strong>.&#0160;When you upload a file to <strong>A360</strong>&#0160;through its <strong>webpage</strong> then it will automatically kick off the <strong>SVF</strong> translation so that it will be viewable on the web. Now you have access to this translation.</p>
<p>For comparison, let&#39;s use the same response body that was used in the previous blog post. Now instead of using the id highlighted in <span style="color: #ff0000;">red</span>, you can use the one highlighted in <span style="color: #00aa00;">green</span>.</p>
<pre>{
      &quot;type&quot;: &quot;<span style="color: #00aa00;"><strong>versions</strong></span>&quot;,
      &quot;id&quot;: &quot;<span style="color: #00aa00;"><strong>urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q?version=1</strong></span>&quot;,
      &quot;attributes&quot;: {
        &quot;name&quot;: &quot;House Design.rvt&quot;,
        &quot;displayName&quot;: &quot;House Design.rvt&quot;,
        &quot;createTime&quot;: &quot;2016-05-24T19:25:23+00:00&quot;,
        &quot;createUserId&quot;: &quot;38SCJGX4R4PV&quot;,
        &quot;lastModifiedTime&quot;: &quot;2016-05-24T19:25:23+00:00&quot;,
        &quot;lastModifiedUserId&quot;: &quot;38SCJGX4R4PV&quot;,
        &quot;versionNumber&quot;: 1,
        &quot;mimeType&quot;: &quot;application/vnd.autodesk.revit&quot;,
        &quot;fileType&quot;: &quot;rvt&quot;,
        &quot;storageSize&quot;: 12550144,
        &quot;extension&quot;: {
          &quot;type&quot;: &quot;versions:autodesk.core:File&quot;,
          &quot;version&quot;: &quot;1.0&quot;,
          &quot;schema&quot;: {
            &quot;href&quot;: &quot;<a class="vglnk" href="https://developer.api.autodesk.com/schema/v1/versions/versions%3Aautodesk.core%3AFile-1.0" rel="nofollow">https://developer.api.autodesk.com/schema/v1/versions/versions%3Aautodesk.core%3AFile-1.0</a>&quot;
          },
          &quot;data&quot;: {}
        }
      },
      &quot;links&quot;: {
        &quot;self&quot;: {
          &quot;href&quot;: &quot;<a class="vglnk" href="https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1" rel="nofollow">https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1</a>&quot;
        }
      },
      &quot;<span style="color: #ff0000;"><strong>relationships</strong></span>&quot;: {
        &quot;item&quot;: {
          &quot;data&quot;: {
            &quot;type&quot;: &quot;items&quot;,
            &quot;id&quot;: &quot;urn:adsk.wipprod:dm.lineage:6bVr4EVDSaOpykczeQYR2Q&quot;
          },
          &quot;links&quot;: {
            &quot;related&quot;: {
              &quot;href&quot;: &quot;<a class="vglnk" href="https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/item" rel="nofollow">https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/item</a>&quot;
            }
          }
        },
        &quot;refs&quot;: {
          &quot;links&quot;: {
            &quot;self&quot;: {
              &quot;href&quot;: &quot;<a class="vglnk" href="https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/relationships/refs" rel="nofollow">https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/relationships/refs</a>&quot;
            },
            &quot;related&quot;: {
              &quot;href&quot;: &quot;<a class="vglnk" href="https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/refs" rel="nofollow">https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/refs</a>&quot;
            }
          }
        },
        ... 
        &quot;<span style="color: #ff0000;"><strong>storage</strong></span>&quot;: {
          &quot;data&quot;: {
            &quot;type&quot;: &quot;objects&quot;,
            &quot;id&quot;: &quot;<span style="color: #ff0000;"><strong>urn:adsk.objects:os.object:wip.dm.prod/977d69b1-43e7-40fa-8ece-6ec4602892f3.rvt</strong></span>&quot;
          },</pre>
<p>Here is some graphics to help explain the change:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb095135c2970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ModelDerivativeJob2" class="asset  asset-image at-xid-6a0167607c2431970b01bb095135c2970d img-responsive" src="/assets/image_aff7d3.jpg" title="ModelDerivativeJob2" /></a></p>
<p>Once you have the id (in case of our example&#0160;<span style="color: #00aa00;"><strong>urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q?version=1</strong></span>) you just have to <strong>base64</strong> encode it&#0160;and use that with the <a href="https://developer.autodesk.com/en/docs/model-derivative/v2">Model Derivative API</a> to post an <strong>svf&#0160;</strong>translation <strong>job</strong> for that file:<br /><a href="https://developer.autodesk.com/en/docs/model-derivative/v2/tutorials/prepare-file-for-viewer/">https://developer.autodesk.com/en/docs/model-derivative/v2/tutorials/prepare-file-for-viewer/</a></p>
<p>That&#39;s it.</p>
<p><strong>Note</strong>:&#0160;you need to use <strong>URL safe base64 encoding</strong> (RFC 6920) which has &#39;<strong>_</strong>&#39; instead of &#39;<strong>/</strong>&#39;, &#39;<strong>-</strong>&#39; instead of &#39;<strong>+</strong>&#39;, and no &#39;<strong>=</strong>&#39; padding at the end. This is also described on <a href="https://en.wikipedia.org/wiki/Base64#Variants_summary_table">this</a> wiki page in the &quot;<strong>Unpadded &#39;base64url&#39; for &quot;named information&quot; URI&#39;s (<a class="new" href="https://en.wikipedia.org/w/index.php?title=RFC_6920&amp;action=edit&amp;redlink=1" title="RFC 6920 (page does not exist)">RFC 6920</a>)</strong>&quot; row of the comparison table</p>
