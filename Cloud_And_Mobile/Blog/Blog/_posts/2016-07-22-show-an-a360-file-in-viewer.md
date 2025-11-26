---
layout: "post"
title: "Show an A360 file in the Viewer"
date: "2016-07-22 16:00:28"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Data Management"
  - "Model Derivative"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/show-an-a360-file-in-viewer.html "
typepad_basename: "show-an-a360-file-in-viewer"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This will likely be improved, but currently in order to show an <strong>A360</strong> item in the <a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/">Viewer</a> you&#39;ll also have to use the <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/overview/">Model Derivative API</a> on the item - as mentioned by <strong>Philippe</strong> too here:&#0160;<br /><a href="http://stackoverflow.com/questions/37835178/creating-a-viewer-application-with-an-urn-from-autodesk-a360">http://stackoverflow.com/questions/37835178/creating-a-viewer-application-with-an-urn-from-autodesk-a360</a></p>
<p>So first you need to get to the version of the item you want to show in the viewer. You can just follow the steps here to get to the <strong>item version</strong> you want:&#0160;<br /><a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/download-file/">https://developer.autodesk.com/en/docs/data/v2/tutorials/download-file/</a></p>
<p>Following the sample&#39;s values that you got back in <strong>Step 3</strong>, you&#39;ll see that the returned value contains the <strong>items</strong> and their <strong>versions</strong> as well. And the version also contains a <strong>storage</strong> section under <strong>relationships</strong> with the <strong>id&#0160;</strong>we need:</p>
<pre>{
      &quot;type&quot;: &quot;<span style="color: #ff0000;"><strong>versions</strong></span>&quot;,
      &quot;id&quot;: &quot;urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q?version=1&quot;,
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
            &quot;href&quot;: &quot;https://developer.api.autodesk.com/schema/v1/versions/versions%3Aautodesk.core%3AFile-1.0&quot;
          },
          &quot;data&quot;: {}
        }
      },
      &quot;links&quot;: {
        &quot;self&quot;: {
          &quot;href&quot;: &quot;https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1&quot;
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
              &quot;href&quot;: &quot;https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/item&quot;
            }
          }
        },
        &quot;refs&quot;: {
          &quot;links&quot;: {
            &quot;self&quot;: {
              &quot;href&quot;: &quot;https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/relationships/refs&quot;
            },
            &quot;related&quot;: {
              &quot;href&quot;: &quot;https://developer.api.autodesk.com/data/v1/projects/a.cGVyc29uYWw6cGUyOWNjZjMyI0QyMDE2MDUyNDEyOTI5NzY/versions/urn:adsk.wipprod:fs.file:vf.6bVr4EVDSaOpykczeQYR2Q%3Fversion%3D1/refs&quot;
            }
          }
        },
        ... 
        &quot;<span style="color: #ff0000;"><strong>storage</strong></span>&quot;: {
          &quot;data&quot;: {
            &quot;type&quot;: &quot;objects&quot;,
            &quot;id&quot;: &quot;<span style="color: #ff0000;"><strong>urn:adsk.objects:os.object:wip.dm.prod/977d69b1-43e7-40fa-8ece-6ec4602892f3.rvt</strong></span>&quot;
          },</pre>
<p>Now you just have to <strong>base64</strong> encode that <strong>urn</strong> and use it with the <a href="https://developer.autodesk.com/en/docs/model-derivative/v2">Model Derivative API</a> to post an <strong>svf</strong> translation <strong>job</strong> for that file:<br /><a href="https://developer.autodesk.com/en/docs/model-derivative/v2/tutorials/prepare-file-for-viewer/">https://developer.autodesk.com/en/docs/model-derivative/v2/tutorials/prepare-file-for-viewer/</a></p>
<p>That&#39;s it.</p>
