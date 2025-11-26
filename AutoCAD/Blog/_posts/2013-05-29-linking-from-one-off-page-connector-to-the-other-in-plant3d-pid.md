---
layout: "post"
title: "Linking from one off-page connector to the other in Plant3d P&amp;ID"
date: "2013-05-29 08:34:27"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "Fenton Webb"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2013/05/linking-from-one-off-page-connector-to-the-other-in-plant3d-pid.html "
typepad_basename: "linking-from-one-off-page-connector-to-the-other-in-plant3d-pid"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you want to obtain an id or reference to an off-page connector that is linked to a given off-page connector using the API then…</p>  <p><strong>For the 2013 Plant3d SDK</strong></p>  <p>Use the DataLinksManager.GetRelatedAcPpObjectIds(), GetRelatedRowIds() using “ConnectorsRelationship” as the relationship name and “Connector1”, “Connector2” as the role names. Relationships of this nature have to be checked using both roles.</p>  <p><strong>For the 2014 Plant3d SDK</strong></p>  <p>Use the OffpageConnectionManager. If has methods such as: IsConnected(), GetConnectedPpId(), GetConnectedRowId(). </p>
