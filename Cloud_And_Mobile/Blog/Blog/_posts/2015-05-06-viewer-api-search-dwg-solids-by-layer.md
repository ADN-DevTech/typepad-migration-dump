---
layout: "post"
title: "Viewer API: search DWG Solids by Layer"
date: "2015-05-06 11:28:37"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Javascript"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/viewer-api-search-dwg-solids-by-layer.html "
typepad_basename: "viewer-api-search-dwg-solids-by-layer"
typepad_status: "Publish"
---

<p>By Augusto Goncalves</p>
<p>Yesterday I went for a cool <a href="http://www.meetup.com/GDG-SP/events/222055615/" target="_self">meetup on Android</a>, was interesting to get a fresh air on languages: <a href="http://kotlinlang.org/" target="_self">Kotlin</a>. The basic idea is to have a safer language where we (developers) have less worries with programming problems, such as null pointers. It's not released yet, but may worth looking at.</p>
<p>Also, I spent my lottery luck wining a nice book on <a href="https://angularjs.org/" target="_self">Angular JS</a>, now I'll have a good motivation to learn it. Expect some posts soon...</p>
<p><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0829f80a970d-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0829f80a970d img-responsive" style="width: 150px;" title="Angular_js_book" src="/assets/image_94f710.jpg" alt="Angular_js_book" /></a></p>
<p>Back to this post topic, the idea is from a drawing (.dwg) uploaded to Autodesk View and Data API, search for entities of a specific type, then search again by a specific property. In this sample, the idea is search for <strong>Solids</strong> and the by <strong>Layer</strong> name.</p>
<p>To do this, we need to open all components on the viewer, starting from the root, the iterate through each children. Below is the code sample.&nbsp;</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffffpadding:2px;font-size:10pt;"><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> checkTree() {
  viewer.getObjectTree(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (rootComponent) {
    checkChildren(rootComponent.children);
  });
}

</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> checkChildren(children) {
  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (children == undefined) </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;">;
  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (!Array.isArray(children)) </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;">;
  children.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (component) {
    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (Array.isArray(component.children))
      checkChildren(component.children);
    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (!component.hasOwnProperty(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'fragIds'</span><span style="background-color:#ffffff;">)) </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;">;

    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (component.name.indexOf(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Solid'</span><span style="background-color:#ffffff;">) &gt;= </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">) {
      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragIdsArray = (Array.isArray(component.fragIds)
        ? component.fragIds : [component.fragIds]);
      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mesh = viewer.impl.getRenderProxy(
        viewer, fragIdsArray[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">]);
      viewer.getProperties(mesh.dbId, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (props) {
        props.properties.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (prop) {
          </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (prop.displayName.indexOf(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Layer'</span><span style="background-color:#ffffff;">) &gt;= </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">) {


            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Do something here
</span><span style="background-color:#ffffff;">          }
        });
      });
    }
  });
}</span></pre>
