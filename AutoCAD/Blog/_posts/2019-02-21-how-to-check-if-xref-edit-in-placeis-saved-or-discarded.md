---
layout: "post"
title: "How to Check if XREF Edit In Place&ndash;Is Saved or Discarded"
date: "2019-02-21 13:24:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2019/02/how-to-check-if-xref-edit-in-placeis-saved-or-discarded.html "
typepad_basename: "how-to-check-if-xref-edit-in-placeis-saved-or-discarded"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>When user edit external reference in place, there is no way to get to know if user had saved the changes or discarded.</p><p>If your application would like to capture the user intent, for example.</p>
<pre class="prettyprint">if (e.GlobalCommandName == /*MSG0*/"REFCLOSE")
{

if (save){
//logic 1
}
if(discard){
//logic 2
}


}
</pre>
<p>This post will show one way to find out, if Xref is saved or discarded </p>
<pre class="prettyprint">class XrefCheckEditor;
XrefCheckEditor* xrefEd = nullptr;
class XrefLongTranReactor;
XrefLongTranReactor* xrefTransRctr = nullptr;

Acad::ErrorStatus getSysVar(const TCHAR* varName, TCHAR* val)
{
	resbuf rb;
	if (acedGetVar(varName, &amp;rb) == RTNORM) {
		assert(rb.restype == RTSTR);
		val = rb.resval.rstring;
		free(rb.resval.rstring);
		return(Acad::eOk);
	}
	else
		return(Acad::eInvalidInput);
}
namespace EditInPlaceXref {
	enum EditInPlaceXrefState
	{
		Discarded,
		Saved
		
	};
	EditInPlaceXrefState XrefState;
	EditInPlaceXrefState Reset() {
		XrefState = Discarded;
		return XrefState;
	}
	
}

class XrefLongTranReactor : public AcApLongTransactionReactor {

	virtual void endCheckIn(AcDbLongTransaction&amp; lt) {
		if (lt.type() == AcDbLongTransaction::kXrefDb)
		{
			//We are good, changes have been Saved.
			EditInPlaceXref::XrefState = EditInPlaceXref::Saved;
		}
	}

};
class XrefCheckEditor : public AcEditorReactor {

	virtual void commandEnded(const TCHAR* cmdStr) {
		if (wcscmp(cmdStr, L"REFCLOSE") == 0)
		{
			
			switch (EditInPlaceXref::XrefState)
			{
			case EditInPlaceXref::Saved:
				acutPrintf(L"\n Modifications 
					   To In External Reference Are Saved");
				EditInPlaceXref::Reset();
				break;
			case EditInPlaceXref::Discarded:
				acutPrintf(L"\n Modifications
					   To In External Reference Are Discarded");
				break;
			default:
				break;
			}

		}
	}
};

void watch() {

	if (xrefEd == nullptr &amp;&amp; xrefTransRctr == nullptr) {
		xrefEd = new XrefCheckEditor();
		xrefTransRctr = new XrefLongTranReactor();
	}

	acedEditor-&gt;addReactor(xrefEd);
	acapLongTransactionManager-&gt;addReactor(xrefTransRctr);
	
}
void unWatch() {
	acedEditor-&gt;removeReactor(xrefEd);
	acapLongTransactionManager-&gt;removeReactor(xrefTransRctr);
	if (xrefEd != nullptr) {
		delete xrefEd;
		xrefEd = nullptr;
	}
	if (xrefTransRctr != nullptr) {
		delete xrefTransRctr;
		xrefTransRctr = nullptr;
	}
	acutPrintf(_T("Watching Editor Reactor\n"));
}

void checkIfXrefSavedOrDiscarded()
{
	acutPrintf(_T("\nThis function checks
		     if changes made to edit-in place xref are saved or discarded"));
	acutPrintf(_T("\nModifications %s"), 
			EditInPlaceXref::XrefState ? _T("Saved") : _T("Discarded"));
}
</pre>
