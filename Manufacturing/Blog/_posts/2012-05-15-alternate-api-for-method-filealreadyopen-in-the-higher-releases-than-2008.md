---
layout: "post"
title: "Alternate API for Method_FileAlreadyOpen in the Higher Releases than 2008"
date: "2012-05-15 21:58:16"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/alternate-api-for-method_filealreadyopen-in-the-higher-releases-than-2008.html "
typepad_basename: "alternate-api-for-method_filealreadyopen-in-the-higher-releases-than-2008"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>Some developers were using the API FileAlreadyOpen that originally (in the old releases) would tell you if any Inventor session had a document open.&#160; But long ago, we removed the file system locking mechanism used by Inventor and moved to the ‘old versions’ scheme.&#160; Ever since then, this API never really served any purpose.&#160; It could only tell you if a document was open in this process (not any process), and that functionality is already available with the Documents collection.&#160; So, if you want a replacement for the functionality provided by FileAlreadyOpen (as it behaved in 2008), use <strong>Documents.Item</strong>.</p>
