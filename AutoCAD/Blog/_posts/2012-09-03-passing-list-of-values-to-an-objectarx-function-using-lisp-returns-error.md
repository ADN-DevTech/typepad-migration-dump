---
layout: "post"
title: "Passing list of values to an ObjectARX function using Lisp returns error"
date: "2012-09-03 20:29:00"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/passing-list-of-values-to-an-objectarx-function-using-lisp-returns-error.html "
typepad_basename: "passing-list-of-values-to-an-objectarx-function-using-lisp-returns-error"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>Passing list of values to an ObjectARX function using Lisp throws error when certain numbers (For ex : 5011, 25111) are part of the list. Why and how to overcome this ?</p>
<div><strong>Solution</strong></div>
<p style="background: white;">Certain numbers are interpreted as DXF codes which causes AutoCAD to throw an exception.</p>
<p style="background: white;">The simple workaround for this is to send the values as real numbers as shown here :</p>
<p style="background: white;">For ex :&nbsp; (myLispCallableTestFunc '(5011.0 2 3 4)) </p>
<p style="background: white;">The other workaround is to send the values as independent lists. These list may have to be joined together inside the ObjectARX function that is called from LISP.</p>
<p style="background: white;">For ex : (myLispCallableTestFunc '((5011) (2) (3) (4))) </p>
