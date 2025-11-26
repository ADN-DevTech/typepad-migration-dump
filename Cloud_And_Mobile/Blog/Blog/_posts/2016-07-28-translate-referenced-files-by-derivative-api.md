---
layout: "post"
title: "Translate Referenced Files by Derivative API"
date: "2016-07-28 01:28:05"
author: "Xiaodong Liang"
categories:
  - "Viewer"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/translate-referenced-files-by-derivative-api.html "
typepad_basename: "translate-referenced-files-by-derivative-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>A 3D model file can be a single file, or a composite file that links with other dependent files. The typical is Inventor assembly (with subassemblies, parts etc), AutoCAD DWG (with Xref), Revit (with linked files). Derivative API supports to translate single file or composite file.&#0160;</p>
<p>In the past, the way to configure is using the endpoint of Set Reference. <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/09/how-to-set-references-with-inventor-files-for-view-and-data-api.html">The other blog</a> tells more. It requires the developer to configure the relationship of each child one by one. It was tedious.</p>
<p>With new version <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/overview/">Derivative API</a> endpoint, it does not need to set relationship manually anymore. The workflow is now:</p>
<ol>
<li>Package all files in one zip.</li>
<li>Upload the zip file to bucket</li>
<li>Start translation by the endpoint of Derivative</li>
</ol>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; POST <a href="https://developer.api.autodesk.com/modelderivative/v2/designdata/job">https://developer.api.autodesk.com/modelderivative/v2/designdata/job</a></p>
<p>In the parameters, set input.compressedUrn = true and input.rootFilename = the root file name.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88093b5970b-pi"><img alt="image" border="0" height="192" src="/assets/image_df5d49.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="454" /></a></p>
<p>&#0160;</p>
<p>The Derivative service will detect the relationship and build the hierarchy automatically.</p>
<p>This is a test harness. <br /><a href="https://github.com/xiaodongliang/Forge-Test-Harness-Node.js" title="https://github.com/xiaodongliang/Forge-Test-Harness-Node.js">https://github.com/xiaodongliang/Forge-Test-Harness-Node.js</a></p>
<p>And the demo Inventor, AutoCAD, Revit files are available at <br /><a href="https://github.com/xiaodongliang/Forge-Test-Harness-Node.js/tree/master/demo_files" title="https://github.com/xiaodongliang/Forge-Test-Harness-Node.js/tree/master/demo_files">https://github.com/xiaodongliang/Forge-Test-Harness-Node.js/tree/master/demo_files</a></p>
<p>The relevant codes are:</p>
<pre><code>
Lmv.prototype.translate =function (urn,iszip,rootfile) {
    var self =this ;

    //default is to export a single file to svf
    var desc ={
        &quot;input&quot;: {
            &quot;urn&quot;: new Buffer (urn).toString (&#39;base64&#39;)// urn of zip file
        },
        &quot;output&quot;: {
            &quot;formats&quot;: [
                {
                    &quot;type&quot;: &quot;svf&quot;,
                    &quot;views&quot;: [
                        &quot;3d&quot;
                    ]
                }
            ]
        }
    };

    if(iszip)
    {
 <span style="background-color: #ffff00;">       //if it is a composite file
        desc.input[&#39;compressedUrn&#39;] = true;
        //which one is the root file
        desc.input[&#39;rootFilename&#39;] = rootfile;</span>
    }

    console.log(desc);

   //https://developer.api.autodesk.com/modelderivative/v2/designdata/job 
    unirest.post (config.endPoints.translate)
        .headers ({
            &#39;Accept&#39;: &#39;application/json&#39;,
            &#39;Content-Type&#39;: &#39;application/json&#39;,
            &#39;Authorization&#39;: (&#39;Bearer &#39; + Lmv.getToken ()),
            &#39;x-ads-force&#39;: true
        })
        .send (desc)
        .end (function (response) {
            try {
                if ( response.statusCode != 200 &amp;&amp; response.statusCode != 201 )
                    throw response ;
                try {
                    self.emit (&#39;success&#39;, { &#39;urn&#39;: desc.input.urn, &#39;response&#39;: response.body }) ;
                } catch ( err ) {
                }
            } catch ( err ) {
                self.emit (&#39;fail&#39;, err) ;
            }
        })
    ;
    return (this) ;
} ;

</code></pre>
