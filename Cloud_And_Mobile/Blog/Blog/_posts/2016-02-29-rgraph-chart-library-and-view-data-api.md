---
layout: "post"
title: "RGraph chart library and View & Data API"
date: "2016-02-29 04:45:04"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Javascript"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/rgraph-chart-library-and-view-data-api.html "
typepad_basename: "rgraph-chart-library-and-view-data-api"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>It's quite straightforward to write an <a href="http://adndevblog.typepad.com/cloud_and_mobile/2014/10/how-to-write-custom-extensions-for-the-large-model-viewer.html">extension to View &amp; Data</a>, just write a custom code for&nbsp;<em>load&nbsp;</em>and for&nbsp;<em>unload.&nbsp;</em>You'll find several extensions on this blog, including this: <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/07/integrating-a-charting-library-with-view-data-api.html">Integrating a chart library</a>, by Philippe Leefsma.</p>
<p>This post uses Philippe's sample and replaces the&nbsp;<strong>loadChartFromProperty</strong> to create charts using <a href="http://www.rgraph.net/">RGraph library</a>. The code assumes the model is already loaded on the viewer and search for all properties on the geometries, so make sure you call&nbsp;<em>loadExtension&nbsp;</em><strong>after</strong> calling&nbsp;<em>load.&nbsp;</em>For instance:</p>
<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #408080; font-style: italic;">// Load the document (urn) on the view object</span>
<span style="color: #008000; font-weight: bold;">function</span> loadDocument(documentId) {
    <span style="color: #408080; font-style: italic;">// Find the first 3d geometry and load that.</span>
    Autodesk.Viewing.Document.load(
        documentId,
        <span style="color: #008000; font-weight: bold;">function</span> (doc) { <span style="color: #408080; font-style: italic;">// onLoadCallback</span>
            <span style="color: #008000; font-weight: bold;">var</span> geometryItems <span style="color: #666666;">=</span> [];
            geometryItems <span style="color: #666666;">=</span> Autodesk.Viewing.Document.getSubItemsWithProperties(doc.getRootItem(), {
                <span style="color: #ba2121;">'type'</span><span style="color: #666666;">:</span> <span style="color: #ba2121;">'geometry'</span>,
                <span style="color: #ba2121;">'role'</span><span style="color: #666666;">:</span> <span style="color: #ba2121;">'3d'</span>
            }, <span style="color: #008000; font-weight: bold;">true</span>);
            <span style="color: #008000; font-weight: bold;">if</span> (geometryItems.length <span style="color: #666666;">&gt;</span> <span style="color: #666666;">0</span>) {
                _viewer.load(doc.getViewablePath(geometryItems[<span style="color: #666666;">0</span>]), <span style="color: #008000; font-weight: bold;">null</span>, <span style="color: #008000; font-weight: bold;">function</span> () {
                    <span style="background-color: #ffff00;">_viewer.loadExtension(<span style="color: #ba2121;">'Autodesk.ADN.Viewing.Extension.Chart.RGraph'</span>, <span style="color: #ba2121;">''</span>);</span>
                });
            }
        },
        <span style="color: #008000; font-weight: bold;">function</span> (errorMsg) { <span style="color: #408080; font-style: italic;">// onErrorCallback</span>
            alert(<span style="color: #ba2121;">"Load Error: "</span> <span style="color: #666666;">+</span> errorMsg);
        }
    );
}
</pre>
</div>
<p>Here is what the result should look like. See code below (<a href="https://github.com/augustogoncalves/Sample-Extensions-for-Autodesk-View-and-Data/blob/master/Autodesk.ADN.Viewing.Extension.Chart.RGraph.js">hosted on Github</a>)</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81bbd21970b-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c81bbd21970b img-responsive" style="margin: 0px 5px 5px 0px;" title="Rgraph_view_and_data" src="/assets/image_3ccdc4.jpg" alt="Rgraph_view_and_data" width="393" height="292" /></a></p>
<p>See code below (<a href="https://github.com/augustogoncalves/Sample-Extensions-for-Autodesk-View-and-Data/blob/master/Autodesk.ADN.Viewing.Extension.Chart.RGraph.js">hosted on Github</a>)</p>
<script src="https://gist.github.com/augustogoncalves/74bcfeaa7d4841166150.js"></script>
