---
layout: "post"
title: "SubOccurrences may return ComponentOccurrenceProxy objects"
date: "2012-07-16 23:09:19"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/suboccurrences-may-return-componentoccurrenceproxy-objects.html "
typepad_basename: "suboccurrences-may-return-componentoccurrenceproxy-objects"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html" target="_self">Vladimir Ananyev</a></p>
<p><strong>Q</strong>: Why do SubOccurrences return ComponentOccurrenceProxy objects when according to the documentation it should return ComponentOccurrence objects?</p>
<p><strong>A</strong>: ComponentOccurrenceProxy is derived from ComponentOccurrence, as PartDocument is derived from Document. Therefore when SubOccurrences is said to return ComponentOccurrence, it means that it can also return object types derived from that - in this case ComponentOccurrenceProxy. It works the same way as e.g. Application.ActiveDocument, which is said to return Document, therefore it can return many other derived types like PartDocument, AssemblyDocument, etc. </p>
