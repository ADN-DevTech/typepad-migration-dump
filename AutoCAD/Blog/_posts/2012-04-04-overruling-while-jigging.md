---
layout: "post"
title: "Overruling while jigging"
date: "2012-04-04 13:47:47"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/overruling-while-jigging.html "
typepad_basename: "overruling-while-jigging"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html" target="_self">Stephen Preston</a></p>
<p>If you filter your overrule based on entries in an entity’s extension dictionary (SetExtensionDictionaryEntryFilter method), then your overrules won’t be applied the entity is being jigged (e.g. in a copy, rotate or move command). In general, AutoCAD copies an entity (creating a non-database-resident copy) while jigging it in a command, and the extension dictionary is not copied. This means the overrule isn't applied.</p>
<p>If you instead filter on an Xdata registered app id (SetXdataFilter method), then the xdata should be copied with the entity, and your overrule will be applied while jigging.</p>
