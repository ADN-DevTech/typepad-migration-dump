---
layout: "post"
title: "Compile Errors in C++ with VS2012"
date: "2014-04-15 19:26:34"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/04/compile-errors-in-c-with-vs2012.html "
typepad_basename: "compile-errors-in-c-with-vs2012"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>In our development, we found a compiling error in VS2012 with RxInventor.tlb. The compile error looks like:</p>  <p><em><font size="1">&quot;error C2556: 'miComponentColumnTypeEnum iPartTableColumn::GetReferencedDataType(void)' : overloaded function differs only by return type from 'iComponentColumnTypeEnum iPartTableColumn::GetReferencedDataType(void)”.</font></em></p>  <p>This issue is caused by VS. Microsoft has provided a hotfix which is available at the <a href="http://hotfixv4.microsoft.com/Visual%20Studio%202012/latest/DevDiv1031342/61109.400/free/474368_intl_i386_zip.exe">link</a>. </p>
