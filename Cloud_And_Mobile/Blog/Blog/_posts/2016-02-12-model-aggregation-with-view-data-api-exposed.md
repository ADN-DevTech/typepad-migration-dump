---
layout: "post"
title: "Model Aggregation with View & Data API exposed"
date: "2016-02-12 09:32:16"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/model-aggregation-with-view-data-api-exposed.html "
typepad_basename: "model-aggregation-with-view-data-api-exposed"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek">@F3lipek</a></p>
<p>Model "aggregation" is the term we use to define multiple models being loaded into the same View &amp; Data scene. The viewer API was designed initially with the ability to load a single model per scene, so some of this material is work in progress, keep on reading ...</p>
<p>Being able to load&nbsp;multiple models into a scene seems to be quite popular among the developers using View &amp; Data API for commercial applications, and I believe for a reason: dynamically adding and removing already translated models in a scene can bring some very powerful features to your app, thinking about configurations, animations or more complex workflows for example. All my code samples below are&nbsp;relying on <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/11/getting-rid-of-javascript-callbacks-using-asyncawait.html" target="_blank">ES6 and async syntax</a>, to leverage that it's also <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise" target="_blank">promisified</a>. Let's get started.</p>


<p><strong>Loading an extra model to your scene</strong></p>

<p>I assume you already&nbsp;know how to load the first model into your scene. In order to add a second model to it, all you need is to know is it's viewable path and pass it to the <em>viewer.loadModel</em> method.&nbsp;Grabbing the viewable path from the URN and loading the model could look like that:</p>




<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">  1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Returns viewable path from URN (needs matching token)
</span><span style="color:#800000;background-color:#f0f0f0;">  3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">  4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  5 </span><span style="background-color:#ffffff;">getViewablePath: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(token, urn) {
</span><span style="color:#800000;background-color:#f0f0f0;">  6 
  7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Promise((resolve, reject)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">  8 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">  9 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">try</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;"> 10 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 11 </span><span style="background-color:#ffffff;">      Autodesk.Viewing.Initializer({
</span><span style="color:#800000;background-color:#f0f0f0;"> 12 </span><span style="background-color:#ffffff;">        accessToken: token
</span><span style="color:#800000;background-color:#f0f0f0;"> 13 </span><span style="background-color:#ffffff;">        }, ()=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;"> 14 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 15 </span><span style="background-color:#ffffff;">      Autodesk.Viewing.Document.load(
</span><span style="color:#800000;background-color:#f0f0f0;"> 16 </span><span style="background-color:#ffffff;">        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'urn:'</span><span style="background-color:#ffffff;"> + urn,
</span><span style="color:#800000;background-color:#f0f0f0;"> 17 </span><span style="background-color:#ffffff;">        (document)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;"> 18 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 19 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> rootItem = document.getRootItem();
</span><span style="color:#800000;background-color:#f0f0f0;"> 20 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 21 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> geometryItems3d = Autodesk.Viewing.Document.
</span><span style="color:#800000;background-color:#f0f0f0;"> 22 </span><span style="background-color:#ffffff;">            getSubItemsWithProperties(
</span><span style="color:#800000;background-color:#f0f0f0;"> 23 </span><span style="background-color:#ffffff;">            rootItem, {
</span><span style="color:#800000;background-color:#f0f0f0;"> 24 </span><span style="background-color:#ffffff;">              </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'type'</span><span style="background-color:#ffffff;">: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'geometry'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 25 </span><span style="background-color:#ffffff;">              </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'role'</span><span style="background-color:#ffffff;">: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'3d'</span><span style="background-color:#ffffff;"> },
</span><span style="color:#800000;background-color:#f0f0f0;"> 26 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 27 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 28 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> geometryItems2d = Autodesk.Viewing.Document.
</span><span style="color:#800000;background-color:#f0f0f0;"> 29 </span><span style="background-color:#ffffff;">            getSubItemsWithProperties(
</span><span style="color:#800000;background-color:#f0f0f0;"> 30 </span><span style="background-color:#ffffff;">            rootItem, {
</span><span style="color:#800000;background-color:#f0f0f0;"> 31 </span><span style="background-color:#ffffff;">              </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'type'</span><span style="background-color:#ffffff;">: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'geometry'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 32 </span><span style="background-color:#ffffff;">              </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'role'</span><span style="background-color:#ffffff;">: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'2d'</span><span style="background-color:#ffffff;"> },
</span><span style="color:#800000;background-color:#f0f0f0;"> 33 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 34 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 35 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> got2d = (geometryItems2d && geometryItems2d.length &gt; </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 36 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> got3d = (geometryItems3d && geometryItems3d.length &gt; </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 37 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 38 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(got2d || got3d) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 39 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 40 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> pathCollection = [];
</span><span style="color:#800000;background-color:#f0f0f0;"> 41 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 42 </span><span style="background-color:#ffffff;">            geometryItems2d.forEach((item)=&gt;{
</span><span style="color:#800000;background-color:#f0f0f0;"> 43 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 44 </span><span style="background-color:#ffffff;">              pathCollection.push(document.getViewablePath(item));
</span><span style="color:#800000;background-color:#f0f0f0;"> 45 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;"> 46 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 47 </span><span style="background-color:#ffffff;">            geometryItems3d.forEach((item)=&gt;{
</span><span style="color:#800000;background-color:#f0f0f0;"> 48 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 49 </span><span style="background-color:#ffffff;">              pathCollection.push(document.getViewablePath(item));
</span><span style="color:#800000;background-color:#f0f0f0;"> 50 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;"> 51 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 52 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> resolve(pathCollection);
</span><span style="color:#800000;background-color:#f0f0f0;"> 53 </span><span style="background-color:#ffffff;">          }
</span><span style="color:#800000;background-color:#f0f0f0;"> 54 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;"> 55 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 56 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> reject(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'no viewable content'</span><span style="background-color:#ffffff;">)
</span><span style="color:#800000;background-color:#f0f0f0;"> 57 </span><span style="background-color:#ffffff;">          }
</span><span style="color:#800000;background-color:#f0f0f0;"> 58 </span><span style="background-color:#ffffff;">        },
</span><span style="color:#800000;background-color:#f0f0f0;"> 59 </span><span style="background-color:#ffffff;">        (err)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;"> 60 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 61 </span><span style="background-color:#ffffff;">          console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Error loading document... '</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 62 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 63 </span><span style="background-color:#ffffff;">          </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//Autodesk.Viewing.ErrorCode
</span><span style="color:#800000;background-color:#f0f0f0;"> 64 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 65 </span><span style="background-color:#ffffff;">          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">switch</span><span style="background-color:#ffffff;">(err){
</span><span style="color:#800000;background-color:#f0f0f0;"> 66 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 67 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// removed for clarity, see full sample
</span><span style="color:#800000;background-color:#f0f0f0;"> 68 </span><span style="background-color:#ffffff;">          }
</span><span style="color:#800000;background-color:#f0f0f0;"> 69 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;"> 70 </span><span style="background-color:#ffffff;">      });
</span><span style="color:#800000;background-color:#f0f0f0;"> 71 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;"> 72 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">catch</span><span style="background-color:#ffffff;">(ex){
</span><span style="color:#800000;background-color:#f0f0f0;"> 73 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 74 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> reject(ex);
</span><span style="color:#800000;background-color:#f0f0f0;"> 75 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;"> 76 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;"> 77 </span><span style="background-color:#ffffff;">},
</span><span style="color:#800000;background-color:#f0f0f0;"> 78 
 79 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 80 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Loads model into current scene
</span><span style="color:#800000;background-color:#f0f0f0;"> 81 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 82 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 83 </span><span style="background-color:#ffffff;">loadModel: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(path, opts) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 84 
 85 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Promise(async(resolve, reject)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;"> 86 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 87 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _onGeometryLoaded(event) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 88 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 89 </span><span style="background-color:#ffffff;">      viewer.removeEventListener(
</span><span style="color:#800000;background-color:#f0f0f0;"> 90 </span><span style="background-color:#ffffff;">        Autodesk.Viewing.GEOMETRY_LOADED_EVENT,
</span><span style="color:#800000;background-color:#f0f0f0;"> 91 </span><span style="background-color:#ffffff;">        _onGeometryLoaded);
</span><span style="color:#800000;background-color:#f0f0f0;"> 92 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 93 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> resolve(event.model);
</span><span style="color:#800000;background-color:#f0f0f0;"> 94 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;"> 95 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 96 </span><span style="background-color:#ffffff;">    viewer.addEventListener(
</span><span style="color:#800000;background-color:#f0f0f0;"> 97 </span><span style="background-color:#ffffff;">      Autodesk.Viewing.GEOMETRY_LOADED_EVENT,
</span><span style="color:#800000;background-color:#f0f0f0;"> 98 </span><span style="background-color:#ffffff;">      _onGeometryLoaded);
</span><span style="color:#800000;background-color:#f0f0f0;"> 99 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">100 </span><span style="background-color:#ffffff;">    viewer.loadModel(path, opts, ()=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">101 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">102 </span><span style="background-color:#ffffff;">      },
</span><span style="color:#800000;background-color:#f0f0f0;">103 </span><span style="background-color:#ffffff;">      (errorCode, errorMessage, statusCode, statusText)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">104 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">105 </span><span style="background-color:#ffffff;">        viewer.removeEventListener(
</span><span style="color:#800000;background-color:#f0f0f0;">106 </span><span style="background-color:#ffffff;">          Autodesk.Viewing.GEOMETRY_LOADED_EVENT,
</span><span style="color:#800000;background-color:#f0f0f0;">107 </span><span style="background-color:#ffffff;">          _onGeometryLoaded);
</span><span style="color:#800000;background-color:#f0f0f0;">108 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">109 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> reject({
</span><span style="color:#800000;background-color:#f0f0f0;">110 </span><span style="background-color:#ffffff;">          errorCode: errorCode,
</span><span style="color:#800000;background-color:#f0f0f0;">111 </span><span style="background-color:#ffffff;">          errorMessage: errorMessage,
</span><span style="color:#800000;background-color:#f0f0f0;">112 </span><span style="background-color:#ffffff;">          statusCode: statusCode,
</span><span style="color:#800000;background-color:#f0f0f0;">113 </span><span style="background-color:#ffffff;">          statusText: statusText
</span><span style="color:#800000;background-color:#f0f0f0;">114 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">115 </span><span style="background-color:#ffffff;">      });
</span><span style="color:#800000;background-color:#f0f0f0;">116 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">117 </span><span style="background-color:#ffffff;">},</span></pre>

<br>

<p><strong>Transforming models</strong></p>

Most likely you will need to either translate, rotate and/or scale the other models you bring to the scene so they fit nicely together. The <em>viewer.loadModel</em> method take a <em>placementTransform</em> optional argument which is a THREE.Matrix4 transformation matrix:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> loadOptions = {
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#ffffff;">  placementTransform: </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//your THREE.Matrix4 goes here ...
</span><span style="color:#800000;background-color:#f0f0f0;">3 </span><span style="background-color:#ffffff;">}
</span><span style="color:#800000;background-color:#f0f0f0;">4 
5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> model = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">await</span><span style="background-color:#ffffff;"> API.loadModel(
</span><span style="color:#800000;background-color:#f0f0f0;">6 </span><span style="background-color:#ffffff;">  path,
</span><span style="color:#800000;background-color:#f0f0f0;">7 </span><span style="background-color:#ffffff;">  loadOptions);</span></pre>

You can compose in the same matrix translation, rotation and scale. See <a href="http://threejs.org/docs/#Reference/Math/Matrix4">THREE.Matrix4 compose method</a>.

<br>
<br>

What if you need to transform a model after it's been added to the scene? In that case you need to apply the same transform to each of the model fragments, it's a bit more tricky:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Applies transform to specific model
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">transformModel: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(model, transform) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _transformFragProxy(fragId){
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragProxy = viewer.impl.getFragmentProxy(
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">      model,
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">      fragId);
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="background-color:#ffffff;">    fragProxy.getAnimTransform();
</span><span style="color:#800000;background-color:#f0f0f0;">14 
15 </span><span style="background-color:#ffffff;">    fragProxy.position = transform.translation;
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">    fragProxy.scale = transform.scale;
</span><span style="color:#800000;background-color:#f0f0f0;">18 
19 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//Not a standard three.js quaternion
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">    fragProxy.quaternion._x = transform.rotation.x;
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">    fragProxy.quaternion._y = transform.rotation.y;
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">    fragProxy.quaternion._z = transform.rotation.z;
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">    fragProxy.quaternion._w = transform.rotation.w;
</span><span style="color:#800000;background-color:#f0f0f0;">24 
25 </span><span style="background-color:#ffffff;">    fragProxy.updateAnimTransform();
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">27 
28 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Promise(async(resolve, reject)=&gt;{
</span><span style="color:#800000;background-color:#f0f0f0;">29 
30 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragCount = model.getFragmentList().
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">      fragments.fragId2dbId.length;
</span><span style="color:#800000;background-color:#f0f0f0;">32 
33 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//fragIds range from 0 to fragCount-1
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragId=</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">; fragId&lt;fragCount; ++fragId){
</span><span style="color:#800000;background-color:#f0f0f0;">35 
36 </span><span style="background-color:#ffffff;">      _transformFragProxy(fragId);
</span><span style="color:#800000;background-color:#f0f0f0;">37 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">38 
39 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> resolve();
</span><span style="color:#800000;background-color:#f0f0f0;">40 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">41 </span><span style="background-color:#ffffff;">},</span></pre>

<br>
<strong>Fixing the Model Structure Panel behaviour</strong>
<br>
<br>

We now have multiple models into our scene, positioned and scale as we want, however some components of the viewer UI will start to behave impolitely. That's the case of the Model Structure Panel which will display only the hierarchy of the last model loaded. In order to fix that, you can switch programmatically the structure being used by the panel:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:12pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="background-color:#ffffff;">model.getObjectTree((instanceTree) =&gt;{
</span><span style="color:#800000;background-color:#f0f0f0;">2 
3 </span><span style="background-color:#ffffff;">  viewer.modelstructure.setModel(instanceTree);
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#ffffff;">});</span></pre>

This is enough to get the correct structure tree, however selecting components from the tree will not work correctly as it does with a single model. In order to fix that, it would be required to completely redefine the behaviour of that component by using a custom structure panel which I haven't implemented so far. A simple example of a custom structure panel can be found here: <a href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/Autodesk.ADN.Viewing.Extension.ModelStructurePanel">Autodesk.ADN.Viewing.Extension.ModelStructurePanel</a>

<br>
<br>

<strong>Fixing the Property Panel behaviour</strong>
<br>
<br>

The property panel will also stop to display properties of selected component, although at the time of this writing, this has been fixed into the dev version of the API. For the time being you can use that workaround. One reason is that the <strong><em>Autodesk.Viewing.SELECTION_CHANGED_EVENT</em></strong> is no longer fired but another event is used as replacement: <strong><em>Autodesk.Viewing.AGGREGATE_SELECTION_CHANGED_EVENT</em></strong>

<br>
<br>
This is how the event argument is looking:
<br>
<br>



<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c814badf970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c814badf970b image-full img-responsive" alt="Screen Shot 2016-02-12 at 17.16.21" title="Screen Shot 2016-02-12 at 17.16.21" src="/assets/image_fe2b2b.jpg" border="0" /></a><br />




We can use that event to fix the correct properties being passed to the property panel. Here is the code I came up with:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Aggregate SelectionChanged handler
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">async </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> onAggregateSelectionChanged(event) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(event.selections && event.selections.length){
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> selection = event.selections[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> model = selection.model;
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">await</span><span style="background-color:#ffffff;"> API.setCurrentModel(model);
</span><span style="color:#800000;background-color:#f0f0f0;">14 
15 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> nodeId = selection.dbIdArray[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">    setPropertyPanelNode(nodeId);
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">  </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//no components selected -&gt; display properties of root
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">21 
22 </span><span style="background-color:#ffffff;">    viewer.model.getObjectTree((instanceTree) =&gt;{
</span><span style="color:#800000;background-color:#f0f0f0;">23 
24 </span><span style="background-color:#ffffff;">      setPropertyPanelNode(instanceTree.rootId);
</span><span style="color:#800000;background-color:#f0f0f0;">25 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">}
</span><span style="color:#800000;background-color:#f0f0f0;">28 
29 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> setPropertyPanelNode(nodeId) {
</span><span style="color:#800000;background-color:#f0f0f0;">30 
31 </span><span style="background-color:#ffffff;">  viewer.getProperties(nodeId, (result)=&gt;{
</span><span style="color:#800000;background-color:#f0f0f0;">32 
33 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(result.properties) {
</span><span style="color:#800000;background-color:#f0f0f0;">34 
35 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> propertyPanel = viewer.getPropertyPanel(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">36 
37 </span><span style="background-color:#ffffff;">      propertyPanel.setNodeProperties(nodeId);
</span><span style="color:#800000;background-color:#f0f0f0;">38 
39 </span><span style="background-color:#ffffff;">      propertyPanel.setProperties(result.properties);
</span><span style="color:#800000;background-color:#f0f0f0;">40 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">41 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">42 </span><span style="background-color:#ffffff;">}</span></pre>

<br>
<strong>Fixing the context menu options</strong>
<br>
<br>
The visibility options in the context menu will only work for the initial model, I guess because of the different selection behaviour. I believe using a custom context menu will be more or less straightforward to implement and then using custom methods to turn on or off the visibility of the selected nodes, based on which model they belong. I quickly tested the following functions and they work, however some extra logic is required to be able for example to select nodes from different models and switch their visibility through context menu options.

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Hides node (if nodeOff = true completely hides the node)
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">hideNode: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(model, dbIds, nodeOff=</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Promise((resolve, reject)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">    dbIds = Array.isArray(dbIds) ? dbIds : [dbIds];
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">    model.getObjectTree((instanceTree)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> vm = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.Viewing.Private.VisibilityManager(
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">        viewer.impl,
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">        viewer.model);
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">      dbIds.forEach((dbId)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">18 
19 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> node = instanceTree.dbIdToNode[dbId];
</span><span style="color:#800000;background-color:#f0f0f0;">20 
21 </span><span style="background-color:#ffffff;">        vm.hide(node);
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">        vm.setNodeOff(node, nodeOff);
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">      });
</span><span style="color:#800000;background-color:#f0f0f0;">24 
25 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> resolve();
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">},
</span><span style="color:#800000;background-color:#f0f0f0;">29 
30 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Shows node
</span><span style="color:#800000;background-color:#f0f0f0;">32 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">33 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">showNode: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(model, dbIds) {
</span><span style="color:#800000;background-color:#f0f0f0;">35 
36 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Promise((resolve, reject)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">37 
38 </span><span style="background-color:#ffffff;">    dbIds = Array.isArray(dbIds) ? dbIds : [dbIds];
</span><span style="color:#800000;background-color:#f0f0f0;">39 
40 </span><span style="background-color:#ffffff;">    model.getObjectTree((instanceTree)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">41 
42 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> vm = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.Viewing.Private.VisibilityManager(
</span><span style="color:#800000;background-color:#f0f0f0;">43 </span><span style="background-color:#ffffff;">        viewer.impl,
</span><span style="color:#800000;background-color:#f0f0f0;">44 </span><span style="background-color:#ffffff;">        viewer.model);
</span><span style="color:#800000;background-color:#f0f0f0;">45 
46 </span><span style="background-color:#ffffff;">      dbIds.forEach((dbId)=&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">47 
48 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> node = instanceTree.dbIdToNode[dbId];
</span><span style="color:#800000;background-color:#f0f0f0;">49 
50 </span><span style="background-color:#ffffff;">        vm.setNodeOff(node, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">51 </span><span style="background-color:#ffffff;">        vm.show(node);
</span><span style="color:#800000;background-color:#f0f0f0;">52 </span><span style="background-color:#ffffff;">      });
</span><span style="color:#800000;background-color:#f0f0f0;">53 
54 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> resolve();
</span><span style="color:#800000;background-color:#f0f0f0;">55 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">56 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">57 </span><span style="background-color:#ffffff;">}</span></pre>

<br>

That's all for now, if you have issues, comments or suggestions about that aggregation feature, we are happy to hear it, you can use this forum thread: <a target="_blank" href="http://forums.autodesk.com/t5/view-and-data-api/multiple-urn-s-in-one-lmv-instance/td-p/6014473">Multiple URN's in one LMV instance</a>

<br>
<br>

The complete code for that sample is available here: <a target="_blank" href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/Autodesk.ADN.Viewing.Extension.ModelLoader">Autodesk.ADN.Viewing.Extension.ModelLoader</a>

<br>
<br>

And this is how the the extension panel looks like and a link from where you can test it <a target="_blank" href="http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&extIds=Autodesk.ADN.Viewing.Extension.ModelLoader">live</a>.

<br>
<br>



<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b98a20970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08b98a20970d img-responsive" alt="Screen Shot 2016-02-11 at 18.42.00" title="Screen Shot 2016-02-11 at 18.42.00" src="/assets/image_cf7b38.jpg" /></a><br />

<script src="https://gist.github.com/leefsmp/36b7b737cda36ab8542a.js"></script>
