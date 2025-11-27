---
layout: "post"
title: "What&rsquo;s new in the PLM 360 API - 9.3 release"
date: "2014-02-19 10:39:38"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2014/02/whats-new-in-the-plm-360-api-93-release.html "
typepad_basename: "whats-new-in-the-plm-360-api-93-release"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/Announcements4.png" /></p>
<p>PLM 360 was updated last weekend.&#0160; The <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-7EF1798F-A80C-47D2-A237-E19501EB9F41" target="_blank">Read Me First page</a> has a listing of new REST API pages, but I’d like to also point some things out.</p>
<ul>
<li><strong>New Feature - Better error information when adding or updating an item</strong>.&#0160; Now if the operation fails because of bad field data, you will get back information on which field(s) caused the failure.&#0160; It’s a big time saver when, for example, you are updating an item with 100 fields on it.&#0160; See <a href="http://help.autodesk.com/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-431E3F3D-1C87-4FA6-AA2A-BB744A86E599.htm" target="_blank">FieldValidationError</a> for more information.</li>
<li><strong>New Feature - Item and System change logs.</strong>&#0160; You can now read the change logs through the API.&#0160; The <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-896FA1C1-B5BD-4EC1-BD36-C78BD8E14AF8" target="_blank">Item change log</a> shows you all recent item changes.&#0160; The <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-114F9C9B-0761-408D-BADB-009D2D92153B" target="_blank">System Configuration change log</a> shows changes to workspaces and other system level objects.&#0160; Both APIs require admin-level access.</li>
<li><strong>Enhancement - Max page size increased to 5000 when getting items</strong>.&#0160; Only applies to the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-14018953-E39F-4CA5-B069-974618231608" target="_blank">get Items</a> endpoint.&#0160; 100 is still the max page size for other endpoints.</li>
<li><strong>Bugfix - filtered picklist values</strong> - When getting or setting filtered picklist values, the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-0C8722DF-C209-4875-BA9C-A991E2985E19" target="_blank">PicklistValue</a> object should have a displayName field but not an id or url field.&#0160; This is because a filtered picklist is just a filter value, not a pointer to a specific item.&#0160; However, Item Detail is an exception.&#0160; In those cases, PicklistValue should have all 3 values set.</li>
<li><strong>API Change - Integer to Long</strong> - Most integer values have been changed to long.&#0160; For example, Item.Id.&#0160; If your app is currently using integers, you should be OK as long as you don’t have over 2 billion objects in your tenant.</li>
</ul>
<hr noshade="noshade" style="color: #ff0000;" />
