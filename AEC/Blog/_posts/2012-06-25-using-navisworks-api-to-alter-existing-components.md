---
layout: "post"
title: "Using Navisworks API to alter existing components"
date: "2012-06-25 08:00:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "COM"
  - "Navisworks"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/06/using-navisworks-api-to-alter-existing-components.html "
typepad_basename: "using-navisworks-api-to-alter-existing-components"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>
<p>Can we use the Navisworks API to alter existing components like splitting up cable trays into smaller lengths, etc.</p>
<p>The simple answer is No. The actual&#0160; Navisworks products do not support functionality like that of altering geometry. Once the geometry is in NavisWorks, it can’t be modified. And so the API cannot alter geometry either. The COM API contains ObjectFactory but that does not support creating or editing new nodes.&#0160; We can only use it to override color/transformation/transparency.</p>
