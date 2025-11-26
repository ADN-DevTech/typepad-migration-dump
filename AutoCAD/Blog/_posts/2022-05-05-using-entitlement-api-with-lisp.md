---
layout: "post"
title: "Using Entitlement API with Lisp"
date: "2022-05-05 22:45:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2022/05/using-entitlement-api-with-lisp.html "
typepad_basename: "using-entitlement-api-with-lisp"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p><font size="2">By </font><a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self"><font size="2">Madhukar Moogala</font></a></p><p><font size="2">There is an </font><a href="https://adndevblog.typepad.com/cloud_and_mobile/2014/03/how-to-protect-my-intellectual-property-of-my-app-on-autodesk-exchange-part-1.html"><font size="2">introductory briefing</font></a><font size="2"> on Entitlement API by Daniel.</font><p><font size="2">For reading brevity, I will give simple statement what it brings to the table.</font><p><font size="2">Entitlement API is a rest enabled API, allows you to check if the user is entitled to access your app i.e., user has bought the app from </font><a href="https://apps.autodesk.com/en"><font size="2">Autodesk App Store</font></a><p><font size="2">We have examples on .NET, C++ and now on Lisp</font><p><font size="2"><strong>.NET</strong></font><p><font size="2">Please check - </font><a title="https://github.com/MadhukarMoogala/Entitlement/blob/9ee757fefa9be4369a0503396b8dc1b774056182/ACAD_EntitilementApi/Plugin.cs#L36" href="https://github.com/MadhukarMoogala/Entitlement/blob/9ee757fefa9be4369a0503396b8dc1b774056182/ACAD_EntitilementApi/Plugin.cs#L36"><font size="2">https://github.com/MadhukarMoogala/Entitlement/blob/9ee757fefa9be4369a0503396b8dc1b774056182/ACAD_EntitilementApi/Plugin.cs#L36</font></a><p><font size="2"><strong>C++</strong></font><p><font size="2">Please check</font><p><a title="https://adndevblog.typepad.com/autocad/2020/07/using-entitlement-api-within-objectarx-c.html" href="https://adndevblog.typepad.com/autocad/2020/07/using-entitlement-api-within-objectarx-c.html"><font size="2">https://adndevblog.typepad.com/autocad/2020/07/using-entitlement-api-within-objectarx-c.html</font></a><p><font size="2"><strong>Lisp</strong></font><p><font size="2">For lisp we will be using <a href="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ms759148(v=vs.85)">IXMLHTTPRequest</a> which provides client-side protocol support for communication with HTTP servers.</font><p><font size="2">The members of the API can be found here </font><p><font size="2"><a title="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ms760305(v=vs.85)" href="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ms760305(v=vs.85)">https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ms760305(v=vs.85)</a></font><p><font size="2"><br></font><pre class="prettyprint lang-lisp"><font size="2">(defun c:IsEntitled (/ http response) 
  (setq appId (getstring "Enter AppId "))
  (setq userId (getvar "ONLINEUSERID"))
  (setq url (strcat "https://apps.autodesk.com/webservices/checkentitlement?userid=" 
                    userId
                    "&amp;appid="
                    appId
            )
  )
  ;https://docs.microsoft.com/en-us/previous-versions/windows/desktop/ms759148(v=vs.85)
  (if (setq http (vlax-create-object "MSXML2.XMLHTTP")) 
    (progn 
      (vlax-invoke-method http 'open "get" url :vlax-false)
      (if 
        (not 
          (vl-catch-all-error-p (vl-catch-all-apply 'vlax-invoke (list http 'send)))
        )
        (setq response (vlax-get http 'responseText))
      )
      (vlax-release-object http)
    )
  )
  (cond 
    ((&gt; (vl-string-search "true" response) 0) (princ "\nTrue"))
    (t (princ "\nFalse"))
  )
  (princ)
);defun<br></font></pre>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fa95290200c-pi"><img width="579" height="210" title="testLisp" style="display: inline; background-image: none;" alt="testLisp" src="/assets/image_740386.jpg" border="0"></a>
