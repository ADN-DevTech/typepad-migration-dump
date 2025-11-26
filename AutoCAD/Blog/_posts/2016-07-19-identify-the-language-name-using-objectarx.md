---
layout: "post"
title: "Identify the language name using ObjectARX"
date: "2016-07-19 20:30:00"
author: "Virupaksha Aithal"
categories:
  - "2017"
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2016/07/identify-the-language-name-using-objectarx.html "
typepad_basename: "identify-the-language-name-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>ObjectARX 2017 SDK exposes class “AcLocale” using which language information of the AutoCAD can be retrieved. This “AcLocale” is new class in 2017.</p>
<pre>#include &quot;rxregsvc.h&quot;
void getLocal()
{
	AcLocale locale = acrxProductLocale();
	acutPrintf(locale.iso2LangName());
	acutPrintf(locale.iso2CountryName());
}
</pre>
