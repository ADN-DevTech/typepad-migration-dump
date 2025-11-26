---
layout: "post"
title: "How to Detect CenterLine and CenterMark Entities"
date: "2018-09-10 17:02:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2018/09/how-to-detect-centerline-and-centermark-entities.html "
typepad_basename: "how-to-detect-centerline-and-centermark-entities"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">
Madhukar Moogala</a></p>
<p><br></p><p>CenterLine and CenterMark are two new entities based on AcDbBlockReferenes introduced in AutoCAD 2017, they are basically unnamed blockreference catering different purpose.</p><p>For more information on these two entities – <a href="https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2017/ENU/AutoCAD-Core/files/GUID-C078E9E4-FF38-4BA7-B72B-F2DAB92AFC99-htm.html">CenterLine &amp; CenterMark</a></p><p>In this post we will look at how to identifying if the underlying entities is a CenterLine or CenterMark objects, unfortunately there isn’t a public API to get to know what is the type of the Entity.</p><p>However through using Non-Com property system we can figure out if the entity is whether a CenterLine or CenterMark.</p><p><br></p>
<pre class="prettyprint">void testIt() {
       
       ads_name ename;
       ads_point pt;
       AcDbObjectId objId = AcDbObjectId::kNull;
       AcDbEntity* pEnt = nullptr;
       if (RTNORM != acedEntSel(_T(""), ename, pt)) return; 
       if (!eOkVerify(acdbGetObjectId(objId, ename))) return;
       if (!eOkVerify(acdbOpenAcDbEntity(pEnt, objId, AcDb::kForRead))) return;
       isCenterLineOrNot(pEnt);
       
}
</pre>
<p> <strong>Utils </strong><p><strong>
</strong><pre class="prettyprint"><strong>void getAttInfo(const AcRxAttribute * att,
const AcRxObject * member, 
AcString &amp; attInfo)
{
       if (att-&gt;isA() == AcRxCOMAttribute::desc())
       {
             AcRxCOMAttribute * a = AcRxCOMAttribute::cast(att);
             attInfo.format(_T("\n%s - %s"), att-&gt;isA()-&gt;name(), a-&gt;name());
       }
       else if (att-&gt;isA() == AcRxUiPlacementAttribute::desc())
       {
             AcRxUiPlacementAttribute* a = AcRxUiPlacementAttribute::cast(att);
             attInfo.format(
                    _T("\n%s - %s - %f"),
                    att-&gt;isA()-&gt;name(),
                    a-&gt;getCategory(member),
                    a-&gt;getWeight(member));
       }
       else
       {
             if (att-&gt;isA() != nullptr)
             {
                    attInfo.format(_T("\n%s"),
		    att-&gt;isA()-&gt;name());
             }
       }
}
 
void printValues(AcRxObject * entity, 
const AcRxMember * member, 
Adesk::Boolean&amp; isCenterLine)
{
       Acad::ErrorStatus err = Acad::eOk;
 
       AcString strValue;
       AcRxProperty * prop = AcRxProperty::cast(member);
       if (prop != NULL)
       {
             AcRxValue value;
 
             if ((err = prop-&gt;getValue(entity, value)) == Acad::eOk)
             {
                    ACHAR * szValue = NULL;
 
                    int buffSize = value.toString(NULL, 0);
                    if (buffSize &gt; 0)
                    {
                           buffSize++;
                           szValue = new ACHAR[buffSize];
                           value.toString(szValue, buffSize);
                    }
 
                    
                    strValue.format(_T("%s = %s"),
		    value.type().name(),
		    (szValue == NULL) ? _T("none") : szValue);
 
                    if (szValue)
                           delete szValue;
             }
             else
             {
                    strValue.format(_T("Error Code = %d"), err);
             }
       }
 
       AcString str;
       str.format(_T("\n%s - %s [%s]"), member-&gt;isA()-&gt;name(),
		  member-&gt;name(), strValue.kACharPtr());
       AcString memberName;
       memberName.format(_T("%s"), member-&gt;name());
       if (0 == memberName.collateNoCase(_T("IsCenterLine")))
       {
             AcString isTrue(strValue.kACharPtr());
             if (isTrue.find(_T("1")) &gt; 0) {
                    isCenterLine = Adesk::kTrue;
             }
 
       }
       acutPrintf(str);
 
       const AcRxAttributeCollection &amp; atts = member-&gt;attributes();
 
       for (int i = 0; i &lt; atts.count(); i++)
       {
             const AcRxAttribute * att = atts.getAt(i);
             AcString attInfo;
             getAttInfo(att, member, attInfo);
             acutPrintf(attInfo);
       }
       
       if (member-&gt;children() != NULL)
       {
             for (int i = 0; i &lt; member-&gt;children()-&gt;length(); i++)
             {
                    const AcRxMember * subMember = 
                    member-&gt;children()-&gt;at(i);
                    printValues(entity, subMember, isCenterLine);
             }
       }
}
Adesk::Boolean isCenterLineOrNot(AcDbEntity* pEnt)
{
       AcRxMemberIterator * iter = 
       AcRxMemberQueryEngine::theEngine()-&gt;newMemberIterator(pEnt);
       Adesk::Boolean isCenterLine = Adesk::kFalse;
       for (; !iter-&gt;done(); iter-&gt;next())
       {
             printValues(pEnt, iter-&gt;current(), isCenterLine);
       }
       return isCenterLine;
}
</strong></pre>

<p><strong>Update:</strong></p>
<p>Thanks to Engineering Colleague Huyau Liu for this tip </p>
<pre class="prettyprint">enum CenterType
{
	CenterMark = 0,
	CenterLine
};
AcString SmartCenters[] = { L"CenterMark", L"CenterLine" };

Acad::ErrorStatus getTypeOfSmartCenterObject(const AcDbObjectId&amp; blockRefObjId , CenterType&amp; type)
{
	AcDbSmartObjectPointer<acdbblockreference> pCenter(blockRefObjId, AcDb::kForRead, true);
	if (pCenter.openStatus() != Acad::eOk)
		return Acad::eNullEntityPointer;

	AcDbObjectIdArray actionIds;
	Acad::ErrorStatus err = AcDbAssocAction::getActionsDependentOnObject(pCenter, false, true, actionIds);
	if (!eOkVerify(err))
		return err;

	for (int i = 0; i &lt; actionIds.length(); i++)
	{
		AcDbObjectId actionBody = AcDbAssocAction::actionBody(actionIds[i]);
		auto objClass = actionBody.objectClass();
		AcString objClassName = objClass-&gt;name();

		if (objClassName == L"AcDbCenterMarkActionBody") {
			type = CenterType::CenterMark; 
			return Acad::eOk;
		}
		else if (objClassName == L"AcDbCenterLineActionBody") {
			type = CenterType::CenterLine;
			return Acad::eOk;
		}
		else;
	}

	return Acad::eNotApplicable;

}
</acdbblockreference></pre>
<pre class="prettyprint">  CenterType type;
  if (eOkVerify(getTypeOfSmartCenterObject(objId, type)))
  {
   acutPrintf(_T("CenterType : %s"), SmartCenters[type].kACharPtr());
  }		
</pre>
<p><strong>.NET Port </strong> courtesy Alexander Rivilis, Thank you :)
 </p>
<pre class="prettyprint">public void TestCenter()
{
Document doc = Application.DocumentManager.MdiActiveDocument;
if (doc == null) return;
Editor ed = doc.Editor;
Database db = doc.Database;
PromptEntityOptions prOpt = new PromptEntityOptions("Select block: ");
prOpt.SetRejectMessage("Not a block!");
prOpt.AddAllowedClass(typeof(BlockReference), true);
PromptEntityResult prRes = ed.GetEntity(prOpt);
if (prRes.Status != PromptStatus.OK) return;
using (Transaction tr = doc.TransactionManager.StartTransaction())
{
    var br = 
    tr.GetObject(prRes.ObjectId, OpenMode.ForRead) as BlockReference;
    try
    {
        var idCols = AssocAction.GetActionsDependentOnObject(br, false, true);
        if (idCols.Count &gt; 0)
        {
            foreach (ObjectId id in idCols)
            {
                ObjectId idAct = AssocAction.GetActionBody(id);
                if (idAct.ObjectClass.Name == "AcDbCenterMarkActionBody")
                {
                    ed.WriteMessage("\nIt is CenterMark");
                    break;
                }
                if (idAct.ObjectClass.Name == "AcDbCenterLineActionBody")
                { 
                    ed.WriteMessage("\nIt is CenterLine");
                    break;
                }
            }
        }
    }
    catch { };
    tr.Commit();
}

}
</pre>
