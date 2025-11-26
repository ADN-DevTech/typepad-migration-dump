---
layout: "post"
title: "Timestamp information on Revit objects via the API"
date: "2013-01-07 14:52:42"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "Revit"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/01/timestamp-information-on-revit-objects-via-the-api.html "
typepad_basename: "timestamp-information-on-revit-objects-via-the-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>Here is quick short question that has come up couple of times from the API users in the Revit API community:</p>  <p>Is there timestamp information available for Revit elements, accessible using the API – which tells us when elements have been </p>  <p>Unfortunately, Revit itself does not provide the ability to track the timestamp on elements (to track when they were last edited, etc) and so the API does not have any access to such data natively. The workaround that could be suggested for this workflow would be to use Dynamic Update to update parameters on creation and modification of elements. Data could stored as shared parameters or as extensible storage. Though this would not help with files coming from any other firm/organizations, but it would help a single firm wanting to track their own elements.</p>
