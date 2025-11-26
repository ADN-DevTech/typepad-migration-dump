---
layout: "post"
title: "Autodesk Infrastructure Map Server(AIMS) API Training content : Part-3"
date: "2013-04-03 00:19:56"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/autodesk-infrastructure-map-serveraims-api-training-content-part-3.html "
typepad_basename: "autodesk-infrastructure-map-serveraims-api-training-content-part-3"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>In this post, we discuss site service and resource service. As we talked in <a href="http://adndevblog.typepad.com/infrastructure/2013/04/autodesk-infrastructure-map-serveraims-api-training-content-part-2.html">part 2</a>, there are 7 services in MapGuide Web Extension API. Before a page can use a service, it must open a site connection and create an instance of the necessary service type. All other 6 services are created by site service from a site connection. You have to initialize web tier in every page with following code snippet: </p>
<p>MapGuideApi.MgInitializeWebTier(“webconfig.ini”)</p>
<p>webconfig.ini file is text file used to perform basic initialization when a session starts, it is read by MgInitializeWebTier(). It contains parameters required to connect to the site server: IP address, port numbers, map agent requests customization, e.g. pause time between successive request, passwords for OGC (WMS &amp; WFS) requests and etc. The values set in this file affect the performance of map server. In most cases, no need to change. If you have to, please check out <a href="http://trac.osgeo.org/mapguide/wiki/ServerconfigWebConfigIni">this document</a>.&#0160; </p>
<p>An Infrastructure Map Server <em>repository</em> is a database that stores and manages the data for the site. The repository stores all data except data that is stored in external databases. Data stored in a repository is a <em>resource</em>.&#0160; A resource can be persistent resource or temporary resource. Persistent resource that is available to all users is stored in the Library repository.In addition, each session has its own repository, which stores the run-time map state. It can also be used to store other data, like temporary layers that apply only to an individual session. For example, a temporary layer might be used to overlay map symbols indicating places of interest.</p>
<p>Resource service is used to manipulate these resources with API. To use a resource service, web page must open a site connection and create an instance of a resource service:</p>
<pre style="line-height: normal; background: white;"><span style="font-family: 新宋体;"><span style="color: #000000;"><span style="font-size: 7.8pt;">MgUserInformation userInfo = </span></span><span style="font-size: 7.8pt;"><span><span style="color: #0000ff;">new</span></span></span></span><span style="font-family: 新宋体;"><span style="font-size: 7.8pt;"><span style="color: #000000;"> MgUserInformation(sessionID); <br />siteConnection = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> MgSiteConnection(); siteConnection.Open(userInfo); <br />MgResourceService resourceService = (MgResourceService)siteConnection<br />&#0160;&#0160;&#0160; .CreateService(MgServiceType.ResourceService); </span></span></span></pre>
<p>Here are some commonly used methods of resource service, please find the complete method list from the <a href="http://images.autodesk.com/adsk/files/autodesk_infrastructure_map_server_2013_api_references.zip">AIMS API Reference</a>.</p>
<p>MgResourceService::EnumerateResources
  <br />MgResourceService::GetResourceContent
  <br />MgResourceService::SetResource</p>
<p>With these simple APIs, you can create very useful/powerful applications as long as you have deep understanding of the resource content, in most case they are xml. If you are not familiar with resource and resource service, please view the <a href="http://adndevblog.typepad.com/files/en_aims_api_chapter_3.pdf" target="_self">chapter 3</a> of AIMS API training materials first. The exercise code sample solution 3 can be downloaded <a href="http://adndevblog.typepad.com/infrastructure/2012/05/devtv-and-code-sample-create-temporary-layer-feature-editing-in-aims2012.html">here</a>. After that, you may want to check out this <a href="http://adndevblog.typepad.com/infrastructure/2012/09/intelligent-landing-page-for-aimsmapguide-ajax-viewer-1.html">intelligence landing page</a>, it is a good example of usage of resource service. Please look at the source code, I believe you will find it very interesting.&#0160;
</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b017ee9eb4181970d"><a href="http://adndevblog.typepad.com/files/en_aims_api_chapter_3.pdf">Download EN_AIMS_API_Chapter_3</a></span> </p>
