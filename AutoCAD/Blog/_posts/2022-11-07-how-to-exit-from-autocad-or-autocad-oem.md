---
layout: "post"
title: "How to Exit from AutoCAD or AutoCAD OEM"
date: "2022-11-07 17:13:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "AutoCAD OEM"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2022/11/how-to-exit-from-autocad-or-autocad-oem.html "
typepad_basename: "how-to-exit-from-autocad-or-autocad-oem"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p><font size="2">By </font><a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self"><font size="2">Madhukar Moogala</font></a></p>
<p><font face="Arial" size="2">There may be a situation where your Arx application needs to exit AutoCAD if the license check or some other business logic fails</font></p><font face="Arial" size="2">
</font><p><font face="Arial" size="2">When aborting, use AcDbHostApplicationServices::fatalError() 
</font><p><font face="Arial" size="2">Or, acrx_abort() instead of a direct or indirect call to exit().This will allow AutoCAD and other ObjectARX applications to recover as much work as possible. Always use API calls rather than system calls.</font></p><font face="Arial" size="2">
</font><p><font face="Arial" size="2">It is highly desirable for the RealDWG host application to override this method, and do whatever needs to be done for a clean and graceful shutdown. 
For example, allowing the user to save some portion of the work in progress, cleaning up memory allocations, and so on are all things that should be done upon a fatal error.
</font></p><font face="Arial" size="2">


</font><pre class="prettyprint"><font face="Arial" size="2">extern "C" AcRx::AppRetCode
acrxEntryPoint(AcRx::AppMsgCode msg, void* pkt)
{
	AcRx::AppRetCode retVal = AcRx::kRetOK;
	switch (msg)
	{
	case AcRx::kInitAppMsg:
		//acrxDynamicLinker-&gt;unlockApplication(pkt);
		//acrxRegisterAppMDIAware(pkt);
		//initApp();

		// perform license check
		retVal = checkLicense();
		if (retVal == AcRx::kRetOK)
		{
			acrxDynamicLinker-&gt;unlockApplication(pkt);
			acrxRegisterAppMDIAware(pkt);
			initApp();
		}
		else
		{
			MessageBox(adsw_acadMainWnd(),
			L"Application should not be allowed to load.", L"Start-up Error", MB_OK + MB_ICONWARNING);
			acrx_abort(L"Invalid License: Application Quits Now");
			//or
			//acdbHostApplicationServices()-&gt;fatalError(_T("Application is Quitting Now"));					
			return AcRx::kRetError;
		}
		break;
	case AcRx::kUnloadAppMsg:
		unloadApp();
		break;
	default:
		break;

	}

	//return AcRx::kRetOK;
	return retVal;

}</font>
</pre>
