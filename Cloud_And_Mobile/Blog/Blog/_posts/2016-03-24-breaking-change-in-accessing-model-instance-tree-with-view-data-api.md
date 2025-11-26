---
layout: "post"
title: "Breaking change in accessing model instance tree with View & Data API"
date: "2016-03-24 09:02:15"
author: "Philippe Leefsma"
categories:
  - "Philippe Leefsma"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/03/breaking-change-in-accessing-model-instance-tree-with-view-data-api.html "
typepad_basename: "breaking-change-in-accessing-model-instance-tree-with-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek">(@F3lipek)</a></p>
<p>The latest release of the viewer available at&nbsp;<a href="https://autodeskviewer.com/viewers/2.5/viewer3D.min.js">https://autodeskviewer.com/viewers/2.5/viewer3D.min.js</a>&nbsp;is unfortunately introducing a breaking change in the way you have to access the model structure. The migration is rather straightforward but any code you have in your application relying on <strong><em>viewer.getObjectTree</em></strong> is likely to be broken.</p>

Here is a simple snippet that illustrates how to iterate the direct children of the root node:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:12pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> instanceTree = viewer.model.getData().instanceTree;
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 
 3 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> rootId = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.rootId = instanceTree.getRootId();
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> rootName = instanceTree.getNodeName(rootId);
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> childCount = </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">instanceTree.enumNodeChildren(rootId, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(childId) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> childName = instanceTree.getNodeName(childId);
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">  console.log(childName);
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">  childCount++;
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">});</span></pre>

For a more elaborated sample, take a look at the implementation of my ModelStructure Extension. It implements two methods: <em>"buildModelTree"</em> that returns the model tree as object using a recursive function and <em>"executeTaskOnModelTree"</em> that returns an array of <a href="https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Promise">Promises</a> which run a task on each node component.

Using that function, you could for example get all components which contain a property named 'Material', it will then return the list of all matches in an array when all promises have resolved:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:12pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// A demo task
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> hasPropertyTask(model, dbId, propName, matches) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Promise(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(resolve, reject){
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">    model.getProperties(dbId, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(result) {
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(result.properties) {
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;"> (</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> i = </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">; i &lt; result.properties.length; ++i) {
</span><span style="color:#800000;background-color:#f0f0f0;">14 
15 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> prop = result.properties[i];
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">          </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//check if we have a match
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (prop.displayName == propName) {
</span><span style="color:#800000;background-color:#f0f0f0;">19 
20 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> match = {
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">              dbId: dbId
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">23 
24 </span><span style="background-color:#ffffff;">            match[propName] = prop.displayValue;
</span><span style="color:#800000;background-color:#f0f0f0;">25 
26 </span><span style="background-color:#ffffff;">            matches.push(match);
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">          }
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">29 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">30 
31 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> resolve();
</span><span style="color:#800000;background-color:#f0f0f0;">32 
33 </span><span style="background-color:#ffffff;">    }, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">34 
35 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> reject();
</span><span style="color:#800000;background-color:#f0f0f0;">36 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">37 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">38 </span><span style="background-color:#ffffff;">}</span></pre>

Use it has follow:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:12pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> matches = [];
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 
 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates a thunk for our task
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// We look for all components which have a
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// property named 'Material' and returns a list
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// of matches containing dbId and the prop value
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> taskThunk = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(model, dbId) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> hasPropertyTask(
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">    model, dbId, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Material'</span><span style="background-color:#ffffff;">, matches);
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">}
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> taskResults = executeTaskOnModelTree(
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">  viewer.model, taskThunk);
</span><span style="color:#800000;background-color:#f0f0f0;">15 
16 </span><span style="background-color:#ffffff;">Promise.all(taskResults).then(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(){
</span><span style="color:#800000;background-color:#f0f0f0;">17 
18 </span><span style="background-color:#ffffff;">  console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Found '</span><span style="background-color:#ffffff;"> + matches.length + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">' matches'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">  console.log(matches);
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">});</span></pre>

Here is the full implementation of the extension:

<br>
<br>

<script src="https://gist.github.com/leefsmp/9810c0d524a5aa7b6c26.js"></script>
