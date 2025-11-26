---
layout: "post"
title: "Compatibility of Maya plug-ins between releases"
date: "2012-06-05 14:31:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "MEL"
  - "Python"
  - "Visual Studio"
  - "Windows"
original_url: "https://around-the-corner.typepad.com/adn/2012/06/compatibility-of-maya-plug-ins-between-releases.html "
typepad_basename: "compatibility-of-maya-plug-ins-between-releases"
typepad_status: "Publish"
---

<div>
<p>This is a huge topic, so I&#39;m not going to be able to do it justice in this one post.</p>
<p>Plug-ins developed using Maya&#39;s APIs need to be ported and&#0160;tested&#0160;on each major releases&#0160;to make sure they work with a new release of the&#0160;Maya platform. For several generations of&#0160;Maya we have consistently broken binary&#0160;plug-in compatibility every major releases. To make plug-ins work on interim hotfix, service pack, they might need minor porting work to migrate their use of the API and/or use new API, but we do test to minimize the pain experienced by our developers.</p>
<p>Also, applications implemented using different APIs typically require different efforts to port from one release to the next. Generally MEL, Python&#0160;codes are fairly portable between releases. Python plug-ins are a little more portable than C++ plug-ins, but as the&#0160;Python API has been evolving quickly we have so far not always enforced compatibility, Moving forwards I would expect to see more stability and release-to-release compatibility in our&#0160;Python API to Maya when the Python 2.0 implementation will be fully completed.</p>
<p>Architecturally Maya C++ plug-ins are closest to Maya&#39;s core (in terms of their implementation) so it is really for these modules that developers - whether external or internal to Autodesk - need to spend most development effort when application compatibility is broken.</p>
<p>So why do we break compatibility at all? Well there are a few reasons...</p>
<ol>
<li>Sometimes we simply want to update API classes, to add new methods etc... While we can add non-virtual methods during an API-compatible release, adding virtual methods might change a module&#39;s v-table and breaks compatibility.</li>
<li>We also need to update our internal architecture or use of technology. An example of this is our extensive use of Viewport 2.0 in Maya 2012. This is a deep - and wide-reaching - change to the way&#0160;Maya handles&#0160;viewport display&#0160;internally, and has therefore impacted many&#0160;Maya&#0160;plug-ins using&#0160;Viewport API.</li>
<li>Finally we also want to take advantage of the latest compiler technology to build Maya.&#0160;Maya 8.5-2012 were built with Visual Studio 2008 Service Pack 1, Maya 2013 was built with Visual Studio 2010 Service Pack1 (on Windows, for other platform, I am going to post a more complete map). It&#39;s important for us to remain on a supported compiler version, in order to be able to get critical issues addressed by Microsoft (or others), but beyond that it also provides us with new development capabilities, and stay synch with what most developers want to use&#0160;outside.</li>
</ol>
<p>The third point has some subtleties: a pure C++ API could (in theory) be version independent, but whenever C-runtime or&#0160;library classes are used in function signatures (or class derivations), then you do become more closely linked to the specific version of the compiler used to build the API provider (i.e. Maya). So while clients of C++ APIs don&#39;t automatically need to use the same compiler version as the API provider, plug-ins using Maya C++ API do have this requirement. Also strictly speaking you could use some other compiler with Maya, but it is not guaranteed it will always work nicely, and will increase the memory footprint of the running Maya instance.</p>
<p>It&#39;s ADN&#39;s job to help minimize our developers&#39; pain on both these fronts, whether when dealing with migration issues or helping explain the additional features and APIs available in a new release of one of our products.</p>
</div>
