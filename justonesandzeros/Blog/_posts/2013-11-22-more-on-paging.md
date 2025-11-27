---
layout: "post"
title: "More on Paging"
date: "2013-11-22 17:23:39"
author: "Doug Redmond"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2013/11/more-on-paging.html "
typepad_basename: "more-on-paging"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>The <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-CD70000F-9F4C-47D3-87FA-CDF30BAA03D9.htm" target="_blank">basics of paging</a> are already described up on the PLM 360 online help.&#0160; But for today’s article, I’d like to go a bit deeper.&#0160; I’ll also be answering some “why did they do it that way” style questions.</p>
<p>&#0160;</p>
<p><strong>Which endpoints support paging?</strong></p>
<p>If an endpoint supports paging, it will say so in the documentation.&#0160; In most cases, it’s a <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-14018953-E39F-4CA5-B069-974618231608.htm" target="_blank">GET call</a>, like when getting the items in a workspace.&#0160; Page parameters are passed in as query parameters, which are part of the endpoint API documentation.&#0160; If the documentation doesn’t list any query parameters, then paging is not supported.&#0160; If you try to use paging query parameters with an endpoint that does not support paging, you will get a GEN_PAGING_NOT_SUPPORTED error.</p>
<p>Sometimes a <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-BB31FFBD-3904-44A1-80C4-DDB451035617.htm" target="_blank">POST call</a> will support paging.&#0160; In these cases, the <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-CAD2268B-B4B6-4100-B617-1B51CA22F3C2.htm" target="_blank">input payload</a> has the paging parameters, not the query string.&#0160; Another thing different about POST calls is that the nextUrl and prevUrl values are null in the returned PagedCollection.&#0160; Since POST calls require data in the body of the HTTP request, the URL doesn’t contain enough data to run the operation.&#0160; </p>
<p>&#0160;</p>
<p><strong>Going off the edge of the world</strong></p>
<p>There is no upper bound to the “page” parameter.&#0160; You can grab page 100000 if you want.&#0160; The call will succeed even if the object list finished at page 3.&#0160; Go ahead, try it.&#0160; Try the URL <strong>api/v2/workspaces/1/items?page=99999999</strong> in your tenant.</p>
<p>You get back an empty set.&#0160; You don’t get a nextUrl for obvious reasons, but it does provide a prevUrl.&#0160; This is because REST paging doesn’t yet have a totalCount feature (sorry).&#0160; So REST doesn’t know if the previous page contains items or not.</p>
<p>&#0160;</p>
<p><strong>PagedCollection for endpoints that don’t support paging</strong></p>
<p>All collection endpoints return a <a href="http://help.autodesk.com/view/PLM/ENU/?url=/cloudhelp/ENU/PLM-360-REST-APIv2/files/GUID-BB013573-8C54-454E-94BC-C9AA0E5A872B.htm" target="_blank">PagedCollection</a> even if they don’t support paging input parameters.&#0160; This is a bit counter-intuitive, but there are a couple of good reasons why we went with this approach:</p>
<ol>
<li>Consistency across the API.</li>
<li>Allows paging to be added in the future without changing the return schema, which would be a breaking change.</li>
</ol><hr noshade="noshade" style="color: #d09219;" />
