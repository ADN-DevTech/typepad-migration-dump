---
layout: "post"
title: "Update RevisionTable content"
date: "2014-08-05 17:43:02"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/08/update-revisiontable-content.html "
typepad_basename: "update-revisiontable-content"
typepad_status: "Draft"
---

<p>The DESCRIPTION column&#39;s data is in sync with iProperties &gt;&gt; Summary &gt;&gt; Comments. Not sure if the ZONE is kept in sync with something or not, but you can definitely modify it like this:</p>
<p>&#0160;</p>
<p><strong>Dim</strong>&#0160;rt&#0160;<strong>As</strong>&#0160;RevisionTable</p>
<p>rt&#0160;<strong>=</strong>&#0160;<strong>ActiveSheet</strong>.<strong>Sheet</strong>.RevisionTables<strong>(1)</strong></p>
<p>&#0160;</p>
<p><strong>Dim</strong>&#0160;rows&#0160;</p>
<p>rows&#0160;<strong>=</strong>&#0160;rt.RevisionTableRows</p>
<p>&#0160;</p>
<p>rows<strong>(</strong>rows.count<strong>)</strong>.item<strong>(</strong>&quot;Zone&quot;<strong>)</strong>.Text&#0160;<strong>=</strong>&#0160;<strong>Parameter</strong><strong>(</strong>&quot;revzone&quot;<strong>)</strong></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>The above updated the ZONE cell in the last row of the Revision Table.</p>
<p>If you have more sheets or more revision tables then the code would need to be modified accordingly.</p>
