---
layout: "post"
title: "How to Check if BEDIT&ndash;Is Saved or Discarded"
date: "2023-08-07 18:59:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2023/08/how-to-check-if-beditis-saved-or-discarded.html "
typepad_basename: "how-to-check-if-beditis-saved-or-discarded"
typepad_status: "Publish"
---

<p>
  <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst">
  </script>
</p>
<p><font size="2">By </font><a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self"><font size="2">Madhukar Moogala</font></a></p>
<p>
  <font size="2">This is in continuation to </font><a href="https://adndevblog.typepad.com/autocad/2019/02/how-to-check-if-xref-edit-in-placeis-saved-or-discarded.html">
    <font size="2">How to Check if XREF Edit In Place Is Saved or Discarded -
      AutoCAD DevBlog (typepad.com)</font>
  </a>
</p>
<p>
  <font size="2">I have received query how similar thing can be achieved for
    Block Edit in place.</font>
</p>
<p>
  <font size="2">We can retrieve this information from </font><a href="https://help.autodesk.com/view/OARX/2023/ENU/?guid=OARX-RefGuide-acedGetBlockEditMode" target="_blank">
    <font size="2">acedGetBlockEditMode</font>
  </a>
</p>
<p><br></p>
<p><br></p>
<pre class="prettyprint"><font size="1">
<font size="2">class XrefCheckEditor : public AcEditorReactor {

virtual void commandEnded(const TCHAR* cmdStr) {
if (wcscmp(cmdStr, L"REFCLOSE") == 0)
{
			
	switch (EditInPlaceXref::XrefState)
	{
	case EditInPlaceXref::Saved:
		acutPrintf(L"\n Modifications To In External Reference Are Saved");
		EditInPlaceXref::Reset();
		break;
	case EditInPlaceXref::Discarded:
		acutPrintf(L"\n Modifications To In External Reference Are Discarded");
		break;
	default:
		break;
	}

}
if (wcscmp(cmdStr, L"BCLOSE") == 0) {
	const bool bSaveHappened = (::acedGetBlockEditMode() &amp; kBlkEditModeBSaved) != 0;
	if (bSaveHappened)
	{
		acutPrintf(L"\n Block Reference is Saved");
	}
	else
	{
		acutPrintf(L"\n Block Reference is Not Saved");
	}
}
}
};</font>
</font>
</pre>
