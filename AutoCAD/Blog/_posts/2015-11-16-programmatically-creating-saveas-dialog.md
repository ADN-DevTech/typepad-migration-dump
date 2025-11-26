---
layout: "post"
title: "Programmatically Creating SaveAs Dialog"
date: "2015-11-16 16:08:00"
author: "Madhukar Moogala"
categories:
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/11/programmatically-creating-saveas-dialog.html "
typepad_basename: "programmatically-creating-saveas-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>  <p>This blog work is result of a query resulted from customer, in this blog we ‘ll see a simple implementation of save drawing as dialog.</p>  <p>Though we have acedGetNavDialog, we don’t have much control over combo list specifying all drawing formats.</p>  <pre>void SaveDialog()
{
     acutPrintf(L&quot;TEST\n&quot;);

     AcApDocument *pDoc = acDocManager-&gt;mdiActiveDocument();

     acDocManager-&gt;lockDocument(pDoc);
    
     AcDbDatabase *pDB = pDoc-&gt;database();
     AcDbBlockTable *pBT = NULL;
     ErrorStatus es = pDB-&gt;getBlockTable(pBT,AcDb::kForRead);
     AcDbBlockTableRecord *pBTR =NULL;

     pBT-&gt;getAt(ACDB_MODEL_SPACE,pBTR,AcDb::kForWrite);
    

     ::AcDbLine *pLine = new AcDbLine(AcGePoint3d(0,0,0), AcGePoint3d(10,10,0));
     pBTR-&gt;appendAcDbEntity(pLine);
     pLine-&gt;close();

     pBT-&gt;close();
     pBTR-&gt;close();

     acDocManager-&gt;unlockDocument(pDoc);
	 // Create an Save Dialog
	 CFileDialog fileDlg(FALSE, NULL,NULL,
     OFN_FILEMUSTEXIST | OFN_HIDEREADONLY |OFN_EXTENSIONDIFFERENT, NULL);
	 DWORD dwSelItem;
	 /*Setting Dialog Comboxitems to Index, it will help to get aware of user choice*/
	 fileDlg.m_pOFN-&gt;nFilterIndex = 1;
	 /*A buffer containing pairs of null-terminated filter strings. 
	 The last string in the buffer must be terminated by two NULL characters. */
	 fileDlg.m_pOFN-&gt;lpstrFilter = L&quot;AutoCAD 2013 Drawing(*.dwg)\0*.DWG\0
					AutoCAD 2012 Drawing(*.dwg)\0*.DWG\0
					AutoCAD 2010 Drawing(*.dwg)\0*.DWG\0
					AutoCAD 2008 Drawing(*.dwg)\0*.DWG\0
					AutoCAD 2007 Drawing(*.dwg)\0*.DWG\0
					AutoCAD 2007 DXF(*.dxf)\0*.DXF\0\0&quot;;


   // Display the file dialog. When user clicks OK, fileDlg.DoModal() 
   // returns IDOK.
   if(fileDlg.DoModal() == IDOK)
   {
     
	   CString pathName = fileDlg.GetPathName();
	  /*User selection choice*/
	dwSelItem = fileDlg.m_pOFN-&gt;nFilterIndex;

	switch(dwSelItem)
	{
	case 1:
		acutPrintf(L&quot;AutoCAD 2013 Drawing(*.dwg) is selected&quot;);
	
		/*Write custom save fromat logic*/
		break;
	case 2:
		acutPrintf(L&quot;AutoCAD 2012 Drawing(*.dwg) is selected&quot;);
		break;
	case 3:
		acutPrintf(L&quot;AutoCAD 2010 Drawing(*.dwg) is selected&quot;);
		break;
	case 4:
		acutPrintf(L&quot;AutoCAD 2008 Drawing(*.dwg) is selected&quot;);
		break;
	case 5:
		acutPrintf(L&quot;AutoCAD 2007 Drawing(*.dwg) is selected&quot;);
		break;
	case 6:
		acutPrintf(L&quot;AutoCAD 2007 DXF(*.dxf) is selected&quot;);
		break;
	default:
		acutPrintf(L&quot;Nothin&quot;);
		break;
	}

	 acutPrintf(L&quot;\nFile name returned: %s\n&quot;, pathName);
     pDoc-&gt;database()-&gt;saveAs(pathName, true);
     
   }
     
   acedPostCommandPrompt();


}</pre>
