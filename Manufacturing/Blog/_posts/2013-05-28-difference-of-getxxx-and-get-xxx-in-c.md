---
layout: "post"
title: "Difference of GetXXX and get_XXX in C++"
date: "2013-05-28 02:18:12"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/difference-of-getxxx-and-get_xxx-in-c.html "
typepad_basename: "difference-of-getxxx-and-get_xxx-in-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>:    <br />In C++ method, some methods are declared in the format of GetXXX, some are get_XXX. But it looks they are for the same requirement. So what is their difference? </p>  <p>   <br /><strong>Solution</strong>:    <br />GetProperty and MethodMethName(): is called “high” method syntax&#160; <br />get_property and MethodName(): is the “raw” syntax.&#160; <br />The first one returns a value and can throw exceptions. The second one returns an error code and take the return value as in/out param. This is usually the suggested way to use the API, reason is that your program is less likely to crash in case you are not handling exceptions. </p>  <p>The returned property value itself is of course identical using one or the other syntax.</p>
