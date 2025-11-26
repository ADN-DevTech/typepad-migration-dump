---
layout: "post"
title: "Deleting a group entry from the project browser"
date: "2012-05-30 01:22:18"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/05/deleting-a-group-entry-from-the-project-browser.html "
typepad_basename: "deleting-a-group-entry-from-the-project-browser"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>
<p>It is not clear and we tend to miss this when we are deleting groups. So I thought it is worth mentioning it here.</p>
<p>You need to delete the group definition t remove the entry from the project browser. The following code is deleting “Group 1” group definintion.</p>
<p>FilteredElementCollector collector = <span style="color: #0000bf;">new </span>FilteredElementCollector(doc);<br />collector.WherePasses(<span style="color: #0000bf;">new </span>ElementClassFilter(<span style="color: #0000bf;">typeof</span>(GroupType)));<br />var groupTypes = <span style="color: #0000bf;">from </span>element <span style="color: #0000bf;">in </span>collector <span style="color: #0000bf;">where </span>element.Name == <span style="color: #c00000;">&quot;Group 1&quot;</span> <span style="color: #0000bf;">select </span>element;<br />Element groupType = groupTypes.First();<br />doc.Delete(groupType);</p>
<p>BTW, when elements in a group can be deleted with its element all together if you use Delete method. You do not need to use Ungroup method and delete elements one by one.</p>
<p>Group group = elem <span style="color: #0000bf;">as </span>Group;<br />if (<span style="color: #0000bf;">null </span>!= group)<br />{<br />&#0160; doc.Delete(group);<br />}</p>
<p>&#0160;</p>
<p>&#0160;</p>
