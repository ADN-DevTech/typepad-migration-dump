---
layout: "post"
title: "Using Entitlement API within ObjectARX C++"
date: "2020-07-10 03:01:42"
author: "Madhukar Moogala"
categories:
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2020/07/using-entitlement-api-within-objectarx-c.html "
typepad_basename: "using-entitlement-api-within-objectarx-c"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>We have .NET (C#) example on how to secure and protect your app on AutoCAD and other Autodesk products, for AutoCAD, <a href="https://www.keanw.com/2016/02/securing-your-autocad-app-using-net.html">Kean has written long back</a></p><p><a href="https://adndevblog.typepad.com/aec/2015/04/entitlement-api-for-revit-exchange-store-apps.html">For Revit</a> – A blog from Mikako</p><p><a href="https://adndevblog.typepad.com/manufacturing/2020/07/using-entitlement-api-from-fusion-360.html">For Fusion 360</a> – A C++ code, which I used most part of it and made few changes to best fit with AutoCAD</p><p>There is an <a href="https://adndevblog.typepad.com/cloud_and_mobile/2014/03/how-to-protect-my-intellectual-property-of-my-app-on-autodesk-exchange-part-1.html">introductory briefing</a> on Entitlement API by Daniel.</p><p>For reading brevity, I will give simple statement what it brings to the table.</p><p>Entitlement API is a rest enabled API, allows you to check if the user is entitled to access your app i.e., user has bought the app from <a href="https://apps.autodesk.com/en">Autodesk App Store</a></p><p>To call Rest API with in ObjectARX, we need to use a Rest Client, for this I’m using <a href="https://github.com/mrtazz/restclient-cpp">restclient-cpp</a> from mrtazz, a <a href="https://github.com/open-source-parsers/jsoncpp">Json</a> library to parse the response and make code more readable (this an optional not a must to run the sample)</p><p>Full instructions to build the sample is provided in <a href="https://github.com/MadhukarMoogala/EntitlementAPIForACAD">Github</a></p><p><br></p>
<pre class="prettyprint">		if (getUserId()[0] == _T('\0')) {
			//User is not logged in, we will prompt to login!
			onlineUI();
		}
		//plumbing wide
		TCHAR buffer[512];
		acedGetString(0,_T("Enter AppId To Test CheckEntitlement:\t"), buffer);
		string userId = toNarrow(getUserId().kwszPtr());	 
		string appId = toNarrow(buffer);
		string url ="https://apps.exchange.autodesk.com/webservices/checkentitlement"+
					string("?userid=") + userId + string("&amp;appid=") + appId;
		RestClient::Response response = RestClient::get(url);

		Json::Reader reader;
		Json::Value root;

		bool isparseSuccessful = reader.parse(response.body, root);
		if (!isparseSuccessful) {
			acutPrintf(_T("Error parsing URL"));
		}
		if ((root["IsValid"].asString()) == "true")
		{
			acutPrintf(_T("IsValid is True"));
		}
		else
		{
			acutPrintf(_T("IsValid is False"));
		}
</pre>
<p>
Working Gif
 - <a href="https://pasteboard.co/JgZaD71.gif">https://pasteboard.co/JgZaD71.gif</a><p>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e7c344200d-pi"><img width="745" height="371" title="CheckEntitlement" style="display: inline;" alt="CheckEntitlement" src="/assets/image_43002.jpg"></a>
