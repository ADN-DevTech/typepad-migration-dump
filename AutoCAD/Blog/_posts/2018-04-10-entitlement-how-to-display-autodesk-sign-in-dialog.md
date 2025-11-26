---
layout: "post"
title: "Entitlement: How To Display Autodesk Sign In Dialog"
date: "2018-04-10 21:28:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2018/04/entitlement-how-to-display-autodesk-sign-in-dialog.html "
typepad_basename: "entitlement-how-to-display-autodesk-sign-in-dialog"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script> <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar  Moogala</a></p> <p>This is a small post which will be useful by App publishers, in case if user is not logged, app developers can display Autodesk Sign In Dialog.</p> <pre class="prettyprint">bool bIsUserLoggedIn = true;
#define CONNECTTOWEB _T("CONNECTWEBSERVICES")
void onlineUI()
{
	//if user isn't logged, onlineuserid will be empty
	struct resbuf rb;
	acedGetVar(_T("ONLINEUSERID"), &amp;rb);
	if (rb.resval.rstring != NULL) {

		CString userId(rb.resval.rstring);
		if (userId.IsEmpty()) bIsUserLoggedIn = false;
		// Release memory acquired for string:
		free(rb.resval.rstring);
	}
	// pop once only
	if (!bIsUserLoggedIn) {
		// prompt user to login
		// ensure unload-able from ui
		acrxLoadModule(_T("AcConnectWebServices.arx"), false, true);
		acrxLoadModule(_T("AcConnectWebServices.arx"), false, false);

		ASSERT(acrxServiceIsRegistered(CONNECTTOWEB));
		if (acrxServiceIsRegistered(CONNECTTOWEB))
		{
			typedef void(*ADSKLOGIN) ();
			ADSKLOGIN pAdskLogin = (ADSKLOGIN)
				acrxDynamicLinker-&gt;getSymbolAddress(CONNECTTOWEB,
					_T("AcConnectWebServicesLogin"));
			ASSERT(pAdskLogin);
			if (pAdskLogin != NULL)
				pAdskLogin();
		}
		acrxUnloadModule(_T("AcConnectWebServices.arx"), false);
	}

}
</pre>

<pre class="prettyprint">namespace MgdOnlineUI
{
    public class WebUtils
    {
        [DllImport("AcConnectWebServices.arx", EntryPoint = "AcConnectWebServicesLogin")]
            public static extern bool AcConnectWebServicesLogin();
    }
    public class Class1
    {
        [CommandMethod("ONLINEUI")]
        public void Onlineui()
        {
            //If ONLINEUSERID is empty or null, user is not logged in to system.
            if(String.IsNullOrEmpty((string)Application.GetSystemVariable("ONLINEUSERID")))
            {
                WebUtils.AcConnectWebServicesLogin();
            }
        }
    }
}
</pre>
<div style="width:100%;height:0;padding-bottom:67%;position:relative;"><iframe src="https://giphy.com/embed/2uy0r42TWcLbkLZnBB" width="100%" height="100%" style="position:absolute" frameBorder="0" class="giphy-embed" allowFullScreen></iframe></div><p><a href="https://giphy.com/gifs/autodesk-sign-in-2uy0r42TWcLbkLZnBB">via GIPHY</a></p>
