---
layout: "post"
title: "Fail to open DXF/DWG in .NET control application"
date: "2012-09-17 23:42:12"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/09/fail-to-open-dxfdwg-in-net-control-application.html "
typepad_basename: "fail-to-open-dxfdwg-in-net-control-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue</strong>    <br />I have an application of .NET control which tries to open DXF/DWG, but it failed. Is there any known issue?</p>  <p><strong>Solution     <br /></strong>ApplicationControl has a ApplicationType property that allows developers to specify how many DocumentControls will be used in a given application. If this property is set to ApplicationType.SingleDocument, non-native file loaders and the Presenter materials will also be available. However using&#160; MultipleDocument, the relevant loaders are not available. So you will receive the error ‘No plugin exists that will open test.dwg’. This is the current design.</p>
