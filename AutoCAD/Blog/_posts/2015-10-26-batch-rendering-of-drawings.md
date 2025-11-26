---
layout: "post"
title: "Batch rendering of drawings"
date: "2015-10-26 01:53:21"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/10/batch-rendering-of-drawings.html "
typepad_basename: "batch-rendering-of-drawings"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In a recent query, a developer wanted to batch render drawings in AutoCAD. The API does support rendering to a file as explained in <a href="http://adndevblog.typepad.com/autocad/2015/09/rendering-using-rendertoimage-api-.html">this</a> blog post. But, if you do not want to code all that and would prefer running AutoCAD's Render command on a set of files, here is a code snippet to do that. This uses the AutoCAD's COM API to open the drawing, run the Render command and close it after its done. As the Render command in AutoCAD brings up the Render window, the following code snippet closes that window using Win32 API.</p>
<p></p>
<p>Here is a recording to show the Batch rendering of drawings in action :</p>
<iframe src="https://screencast.autodesk.com/Embed/Timeline/dc61b4fd-1002-4863-8391-e1c563d094e5" frameborder="0" webkitallowfullscreen="webkitallowfullscreen" allowfullscreen="allowfullscreen"></iframe>
<p></p>
<p>Here is the code snippet :</p>
<p></p>
<pre>
#include <acadi.h>

#pragma warning( disable : 4278 )
// Change it if you use a different AutoCAD version
#import "acax19ENU.tlb" no_implementation raw_interfaces_only named_guids
using namespace AutoCAD; 
#pragma warning( default : 4278 )

#pragma pack (pop)


Acad::ErrorStatus es;
AcDbBlockTable *pBlockTable = NULL;

AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
		
HRESULT hr;

CStringArray m_arDrawingsToOpen ;
m_arDrawingsToOpen.SetSize(3);
m_arDrawingsToOpen[0] = "D:\\Temp\\RT1.dwg";
m_arDrawingsToOpen[1] = "D:\\Temp\\RT2.dwg";
m_arDrawingsToOpen[2] = "D:\\Temp\\RT3.dwg";

CWinApp* pWinApp = acedGetAcadWinApp(); 
if(! pWinApp) 
	return;
CComPtr<IDispatch> pDisp = pWinApp->GetIDispatch(TRUE); 
if(! pDisp) 
	return;
			
CComPtr<AutoCAD::IAcadApplication> pComApp; 
hr = pDisp->QueryInterface(
    AutoCAD::IID_IAcadApplication,(void**)&pComApp); 
if(FAILED(hr)) 
	return;

CComPtr<AutoCAD::IAcadDocuments> pDocs;
hr = pComApp->get_Documents(&pDocs);
if(FAILED(hr)) 
	return;
						
for (int index = 0; 
    index < m_arDrawingsToOpen.GetSize(); index++)
{
	CString strDrawingFile = m_arDrawingsToOpen[index];

	CComPtr<AutoCAD::IAcadDocument> pDispDoc;
	hr = pDocs->Open(CComBSTR(strDrawingFile), 
    CComVariant(true), CComVariant(NULL), &pDispDoc);

	if(SUCCEEDED(hr))
	{
		WaitUntilReady(pComApp);

		//;;Command
		//-RENDER
		//;;Specify render preset [Draft/Low/Medium/High/Presentation/Other] <Medium>: 
		//Medium
		//;;Specify render destination [Render window/Viewport] <Render window>: 
		//R
		//;;Enter output width <640>:
		//640
		//;;Enter output height <480>:
		//480
		//;;Save rendering to a file? [Yes/No] <No>: 
		//Y
		//;;Specify output file name and path: 
		//D:\Temp\Test.png
		CString outFilePath;
		outFilePath.Format(ACRX_T("D:\\Temp\\Test%d.jpg"), index+1);

		CString cmd;
		cmd.Format(ACRX_T("-RENDER Medium R 640 480 Y %s\n"), outFilePath); 
		BSTR bstrCmd = cmd.AllocSysString();

		CFile file;
		CFileStatus status;
		if(file.GetStatus(outFilePath, status) == TRUE)
		{// Erase existing file, before proceeding to render
			CFile::Remove(outFilePath);
		}

		hr = pDispDoc->SendCommand(bstrCmd);

		::SysFreeString(bstrCmd);

		TCHAR windowText[MAX_PATH];
		GetWindowTextW(GetActiveWindow(), windowText, MAX_PATH);

		CString wndCaption;
		wndCaption.Format(ACRX_T("Test%d - Render"), index+1);

		if (! lstrcmpi(windowText, wndCaption))
		{
			SendMessage(GetActiveWindow(), WM_CLOSE, 0,0);
			acutPrintf(ACRX_T("\n%s window closed."), windowText);
		}

		_variant_t b(VARIANT_FALSE);
		hr = pDispDoc->Close(b);
	}
}


static Adesk::Boolean WaitUntilReady(CComPtr<AutoCAD::IAcadApplication> pComApp)
{
	HRESULT hr;
	Adesk::Boolean renderDone = Adesk::kFalse;
	int maxTries = 10;
	do
	{
		CComPtr<AutoCAD::IAcadState> pAcadState;
		hr = pComApp->GetAcadState(&pAcadState);
		if(SUCCEEDED(hr))
		{
			VARIANT_BOOL bReady = VARIANT_FALSE; 
			pAcadState->get_IsQuiescent(&bReady);
			if(bReady == VARIANT_TRUE)
			{
				return Adesk::kTrue;
			}
			else
				::Sleep(1000);
		}
		maxTries--;
	}while( (maxTries > 0) && ! renderDone);
	return Adesk::kFalse;
}
</pre>
