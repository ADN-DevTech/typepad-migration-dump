---
layout: "post"
title: "Export to 3D PDF C++ Example"
date: "2017-02-13 03:20:42"
author: "Sajith Subramanian"
categories:
  - "Inventor"
  - "Sajith Subramanian"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/02/export-to-3d-pdf-c-example.html "
typepad_basename: "export-to-3d-pdf-c-example"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p>
<p>There is a VBA example available in the Inventor API help file. Here is an example in C++.</p>
<p>One of the easier ways to test the below code, would be to replace the code in the SimpleExe SDK example.</p>
<p>The SimpleExe example is located on my system at: “C:\Users\Public\Documents\Autodesk\Inventor 2017\SDK\DeveloperTools\Samples\VC++\Standalone Applications\Inventor”.</p>
<pre>// SimpleExe.cpp : Defines the entry point for the console application.
//
#include &quot;stdafx.h&quot;

// Forward declarations
static HRESULT PublishPDF();

// Main. Note that all COM related activity (including the automatic &#39;release&#39; within smart
// pointers) MUST take place BEFORE CoUnitialize(). Hence the function &#39;block&#39; within which
// the smart-pointers construct and destruct (and AddRef and Release) keeping the CoUnitialize
// safely out of the way.

int _tmain(int argc, _TCHAR* argv[])
{
	HRESULT Result = NOERROR;

	Result = CoInitialize(NULL);

	if (SUCCEEDED(Result))
		Result = PublishPDF();

	CoUninitialize();

	return 0;
}

static HRESULT PublishPDF()
{
	HRESULT Result = NOERROR;

	CLSID InvAppClsid;
	Result = CLSIDFromProgID(L&quot;Inventor.Application&quot;, &amp;InvAppClsid);
	if (FAILED(Result)) return Result;

	/*Try and get hold of the running inventor app*/
	CComPtr pInvAppUnk;
	Result = ::GetActiveObject(InvAppClsid, NULL, &amp;pInvAppUnk);
	if (FAILED(Result))
	{
		_tprintf_s(_T(&quot;*** Could not get hold of an active Inventor application ***\n&quot;));
		return Result;
	}
	
	CComPtr pInvApp;
	Result = pInvAppUnk-&gt;QueryInterface(__uuidof(Application), (void **)&amp;pInvApp);
	if (FAILED(Result)) return Result;

	/*Get the active document*/
	CComPtr pDoc;
	Result = pInvApp-&gt;get_ActiveDocument(&amp;pDoc);
	if (FAILED(Result)) return Result;
	
	/*Output path of the pdf*/
	char filename[50];
	sprintf_s(filename, &quot;c:\\temp\\test.pdf&quot;);
	
	/*Populate required options*/
	CComPtr oOptions = NULL;
	pInvApp-&gt;TransientObjects-&gt;CreateNameValueMap(&amp;oOptions);
	oOptions-&gt;put_Value(CComBSTR(&quot;FileOutputLocation&quot;), CComVariant(filename));
	oOptions-&gt;put_Value(CComBSTR(&quot;VisualizationQuality&quot;), CComVariant((long)kHigh));

	CComSafeArray sDesignViews;
	sDesignViews.Add(CComBSTR(&quot;Master&quot;));
	oOptions-&gt;put_Value(CComBSTR(&quot;ExportDesignViewRepresentations&quot;), CComVariant(sDesignViews));


	/* Get the Add-Ins*/
	CComPtr pAddIns;
	Result = pInvApp-&gt;get_ApplicationAddIns(&amp;pAddIns);

	
	/*Try and get hold of the 3D PDF addin*/
	CComBSTR clsidAnarkAddin = CComBSTR(_T(&quot;{3EE52B28-D6E0-4EA4-8AA6-C2A266DEBB88}&quot;));
	CComPtr pAnarkAddin = nullptr;
	CComVariant vRes;
	Result = pAddIns-&gt;get_ItemById(clsidAnarkAddin, &amp;pAnarkAddin);
	if (pAnarkAddin) {
		CComPtr pAnarkAPI;
		pAnarkAddin-&gt;get_Automation(&amp;pAnarkAPI);
		if (pAnarkAPI) {
			DISPID dispid;
			OLECHAR* name(OLESTR(&quot;Publish&quot;));
			Result = pAnarkAPI-&gt;GetIDsOfNames(IID_NULL, &amp;name, 1,<br />                                                          LOCALE_SYSTEM_DEFAULT, &amp;dispid);
			if (SUCCEEDED(Result)) {
				CComVariant vars[2];
				vars[1] = pDoc;
				vars[0] = oOptions;
				DISPPARAMS params = { &amp;vars[0], NULL, 2, 0 };
				Result = pAnarkAPI-&gt;Invoke(dispid, IID_NULL, LOCALE_SYSTEM_DEFAULT, <br />                                                           DISPATCH_METHOD, &amp;params, &amp;vRes, <br />                                                           NULL, NULL);
			}
		}
	}
	return Result;
}
</pre>
