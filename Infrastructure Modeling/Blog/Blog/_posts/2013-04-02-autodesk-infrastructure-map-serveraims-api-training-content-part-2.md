---
layout: "post"
title: "Autodesk Infrastructure Map Server(AIMS) API Training content : Part-2"
date: "2013-04-02 00:38:10"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/autodesk-infrastructure-map-serveraims-api-training-content-part-2.html "
typepad_basename: "autodesk-infrastructure-map-serveraims-api-training-content-part-2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>This is <a href="http://adndevblog.typepad.com/en_aims_api_chapter_2.pdf">second part</a> of AIMS API training materials. This part will cover an overview of AIMS/MapGuide API including Server API and Viewer API. </p>
<p>In AIMS/MapGuide Server API, it contains 8 services, they are:</p>
<table border="0" cellpadding="2" cellspacing="0" width="400">
<tbody>
<tr>
<td valign="top" width="200">
<p>Site Service            <br />Resource Service             <br />Mapping Service             <br />Rendering Service             <br />Tile Service             <br />Drawing Service             <br />Feature Service<br />Profiling Service&#0160;</p>
</td>
<td valign="top" width="200"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9eae8fe970d-pi"><img alt="image" border="0" height="244" src="/assets/image_3885a3.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="186" /></a></td>
</tr>
</tbody>
</table>
<blockquote>
<p>&#0160;</p>
</blockquote>
<p>The most commonly used services are Resource Service and Feature Service. Besides the services, there are some additional objects including Geometry, Coordinate related objects and other common objects.</p>
<p>A service performs a particular set of related functions. For example, a resource service manages data in the repository, a feature service provides access to feature data sources, and a mapping service provides visualization and plotting functions. Before a page can use a service, it must open a site connection and create an instance of the necessary service type. All other 6 services are created by site service from a site connection. The following php code creates a resource service and a feature service, please find the .net <a href="http://adndevblog.typepad.com/infrastructure/2012/05/devtv-and-code-sample-create-temporary-layer-feature-editing-in-aims2012.html">code samples here</a>:</p>
<pre>$userInfo = new MgUserInformation($mgSessionId);
$siteConnection = new MgSiteConnection();
$siteConnection-&gt;Open($userInfo);
 
$resourceService = $siteConnection-&gt;
   CreateService(MgServiceType::ResourceService);
$featureService = $siteConnection-&gt;
   CreateService(MgServiceType::FeatureService);</pre>
<p>&#0160;</p>
<p>At client side, you can use Ajax viewer(Basic web layout) or Fusion viewer(Flexible web layout)&#0160; on browser, or Mobile viewer on a mobile device. You are recommended to use fusion viewer if you are familiar with web programing as it give your more flexibility to extend/customize it to meet your need. The document can be found <a href="http://wikihelp.autodesk.com/Infr._Map_Server/enu/2013/Help/0005-Develope0/0094-Flexible94">here</a> and this <a href="http://download.autodesk.com/media/adn/FusionIntroductionDevTV.zip">DevTV</a> may also be helpful for you. Furthermore, all source code of fusion viewer is released with the product, you can dig into the source code if some APIs are not documented. Now please check out the <a href="http://adndevblog.typepad.com/en_aims_api_chapter_2.pdf">chapter 2</a> of training materials. The code sample of solution2 can be downloaded <a href="http://download.autodesk.com/media/adn/AIMS_Sample_DevTV_Solution_5_6/MapGuideSamples2012.zip">here</a>.</p>
