---
layout: "post"
title: "Maximum size of string can be stored in parameter with ParameterType.Text type"
date: "2012-06-10 14:09:00"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/maximum-size-of-string-can-be-stored-in-parameter-with-parametertypetext-type.html "
typepad_basename: "maximum-size-of-string-can-be-stored-in-parameter-with-parametertypetext-type"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>Got a question about the parameter value size. I would like to store some data into a parameter with type of ParameterType.Text. What's the maximum size that it can contain?</p>  <p>The maximum length of string that can be store in ParameterType.Text parameter is the maximum value of type System.int. In practice, the length is still limited by specific computer's available memory.</p>
