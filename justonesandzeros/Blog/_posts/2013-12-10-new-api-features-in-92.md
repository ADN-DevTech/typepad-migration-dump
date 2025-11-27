---
layout: "post"
title: "New API features in 9.2"
date: "2013-12-10 12:41:07"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2013/12/new-api-features-in-92.html "
typepad_basename: "new-api-features-in-92"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/Announcements4.png" /></p>
<p>Here are the new REST API features in the 9.2 release of PLM 360.</p>
<ul>
<li>New endpoint:&#0160; Get the list of users ( <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-53DB554C-2E40-436B-94A7-2BEEBD672433.htm" target="_blank">/api/v2/users</a> ).&#0160; The user information is very basic, and any logged-in user can call this API.</li>
<li>New endpoint:&#0160; Get your user profile ( <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-CE99F272-0F78-415F-A325-252D566FF049.htm" target="_blank">/api/v2/users/profile</a> ).&#0160; This returns detailed information about the logged-in user only.</li>
<li>New endpoint:&#0160; Get the list of groups ( <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-1E55BE34-3C8C-4985-81CD-F1FE2F737160.htm" target="_blank">/api/v2/groups</a> ).&#0160; The group information is very basic, and any logged-in user can call this API.</li>
<li>New endpoint:&#0160; Get the main menu ( <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-C9956333-B8E7-4045-A582-151032D314F0.htm" target="_blank">/api/v2/menu</a> ).&#0160; Shows the configurable parts of the menu, which is just the grouped workspace list.</li>
<li>Updated payload:&#0160; Ownership and Audit added to <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-B3CE8894-4DEE-4C89-A051-4D15921DA250.htm" target="_blank">ItemDetail</a>.&#0160; Ownership shows the owner and additional owners on the item.&#0160; Audit shows information about item creation and when it was last updated.&#0160; This data will be null if the user does not have the “View Owner and Change Summary Section” permission.</li>
<li>Bug fix: Picklist values can be updated by passing in a valid picklist value.</li>
<li>Known issue:&#0160; Error is returned if you pass in a null for a picklist value.&#0160; In other words, there is no way to clear a picklist value through the API.</li>
<li>Known issue:&#0160; Error is returned if you try to set a filtered picklist value for a picklist not using the Item Descriptor.</li>
</ul>
<hr noshade="noshade" style="color: #ff0000;" />
