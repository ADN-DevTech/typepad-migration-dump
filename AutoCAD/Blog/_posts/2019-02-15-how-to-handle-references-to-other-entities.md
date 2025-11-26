---
layout: "post"
title: "How to handle references to other entities"
date: "2019-02-15 00:38:00"
author: "Madhukar Moogala"
categories:
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2019/02/how-to-handle-references-to-other-entities.html "
typepad_basename: "how-to-handle-references-to-other-entities"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p>
<p>An example of handling some common operations for certain types of entities. This does not attempt to be a full-featured entity in the sense that it fills out every single function that it is supposed to. Its main purpose is to illustrate how to deal with pointing to a dictionary-resident"Style" object This sample is stripped out from <a href="https://www.linkedin.com/in/jim-awe-4630a94/">Jim Awe's</a> classic ArxDBG, this mainly focus on establishing hard linkage to other entities, dealing copy with in and across database.</p>
<p>I received a query from ADN partner who is facing WBlock cloning issues i.e., when copying an entity with hard refererence to another entity</p>
<p>You need to be very cautious while copy and pasting entities, in below I have shown steps need to follow in both wblockclone and deepclone <p>
<pre class="prettyprint">Acad::ErrorStatus
ArxDbgDbEntity::subWblockClone(AcRxObject* pOwner, AcDbObject*&amp; pClone,
                    AcDbIdMapping&amp; idMap, Adesk::Boolean isPrimary) const
{
       if (ArxDbgOptions::m_instance.m_showWblockCloneDetails) {
             CString titleStr, tmpStr;
             titleStr.Format(_T("Beginning -- wblockClone: %s"),
                           ArxDbgUtils::objToClassAndHandleStr(const_cast<arxdbgdbentity   *="">(this), tmpStr));
             ArxDbgUiTdmIdMap dbox(&amp;idMap, acedGetAcadDwgView(), titleStr);
             dbox.DoModal();
       }
 
    AcDb::DeepCloneType type = idMap.deepCloneContext();
 
        // if xrefInsert or xrefBind, we know everything will
        // be cloned, so just let normal routine handle this
    if ((type == AcDb::kDcXrefBind) || (type == AcDb::kDcXrefInsert)) {
        return AcDbEntity::subWblockClone(pOwner, pClone, idMap, isPrimary);
    }
             // if we've already been cloned, just return
       AcDbIdPair idPair(objectId(), AcDbObjectId::kNull, true);
    if (idMap.compute(idPair) &amp;&amp; (idPair.value() != AcDbObjectId::kNull)) {
        return Acad::eOk;
    }
 
        // If isPrimary is kTrue, then override the default cloning
        // within our own cloning, which would set it to kFalse,
        // by cloning our referenced entity first.
    if (isPrimary) {
                    // now ask derived classes what references they want cloned for them
             AcDbObjectIdArray refEntIds;
             AcDbIntArray refTypes;
             getCloneReferences(type, refEntIds, refTypes);
 
             ASSERT(refEntIds.length() == refTypes.length());
 
            // clone each entity we reference first and change the value
            // of isPrimary to fake it out.  Since we clone these first,
            // when the normal wblockClone is called, it will see that
            // they are already in the set of cloned objects and will not
            // try to clone it again.
        AcDbEntity* ent;
        Acad::ErrorStatus es;
             int len = refEntIds.length();
             for (int i=0; i<len ent-="" (blockid()="=" if="" case!)="" the="" be="" always="" should="" (which="" block="" same="" from="" come="" they="" works="" only="" method="" this="" {="" acad::eok)="" (es="=" acdb::kforread);="" refentids[i],="" es="acdbOpenAcDbEntity(ent," kclone)="" (reftypes[i]="=" i++)="" ;="">blockId()) {
                                               // Use the same owner, and pass in the same isPrimary value
                                        AcDbObject* pSubClone = NULL;
                                        es = ent-&gt;wblockClone(pOwner, pSubClone, idMap, Adesk::kTrue);
                                        if (pSubClone != NULL)
                                               pSubClone-&gt;close();
 
                                        if (es != Acad::eOk) {
                                               ASSERT(0);
                                        }
                                 }
                                 else {
                                        ASSERT(0);
                                 }
 
                                 ent-&gt;close();
                           }
                    }
             }
    }
        // Now we can clone ourselves via calling our parent's method.
       Acad::ErrorStatus es =  AcDbEntity::subWblockClone(pOwner, pClone, idMap, isPrimary);
 
       if (ArxDbgOptions::m_instance.m_showWblockCloneDetails) {
             CString titleStr, tmpStr;
             titleStr.Format(_T("End -- wblockClone: %s"),
                           ArxDbgUtils::objToClassAndHandleStr(const_cast<arxdbgdbentity   *="">(this), tmpStr));
             ArxDbgUiTdmIdMap dbox(&amp;idMap, acedGetAcadDwgView(), titleStr);
             dbox.DoModal();
       }
 
       return es;
}
 
 
 
Acad::ErrorStatus
ArxDbgDbEntity::subDeepClone(AcDbObject* pOwner,
              AcDbObject*&amp; pClonedObject,
              AcDbIdMapping&amp; idMap,
              Adesk::Boolean isPrimary) const
{
        // You should always pass back pClonedObject == NULL
        // if, for any reason, you do not actually clone it
        // during this call.  The caller should pass it in
        // as NULL, but to be safe, we set it here as well.
    pClonedObject = NULL;
 
       if (ArxDbgOptions::m_instance.m_showDeepCloneDetails) {
             CString titleStr, tmpStr;
             titleStr.Format(_T("Beginning -- deepClone: %s"),
                           ArxDbgUtils::objToClassAndHandleStr(const_cast<arxdbgdbentity   *="">(this), tmpStr));
             ArxDbgUiTdmIdMap dbox(&amp;idMap, acedGetAcadDwgView(), titleStr);
             dbox.DoModal();
       }
 
    AcDb::DeepCloneType type = idMap.deepCloneContext();
 
        // if we know everything will be cloned for us, just let
             // the base class do everything for us.
    if ((type == AcDb::kDcInsert) ||
             (type == AcDb::kDcInsertCopy) ||
             (type == AcDb::kDcExplode))
        return AcDbEntity::subDeepClone(pOwner, pClonedObject, idMap, isPrimary);
 
        // following case happens when doing a AcDbDatabase::deepCloneObjects()
        // and the owner happens to be the same... then its really like a
        // kDcCopy, otherwise deepCloneObjects() is like a kDcBlock
    if (type == AcDb::kDcObjects) {
             if (ownerId() == pOwner-&gt;objectId())
                    type = AcDb::kDcCopy;
             else
                    type = AcDb::kDcBlock;
       }
 
             // now ask derived classes what references they want cloned for them
       AcDbObjectIdArray refEntIds;
       AcDbIntArray refTypes;
       getCloneReferences(type, refEntIds, refTypes);
       ASSERT(refEntIds.length() == refTypes.length());
 
             // if derived class doesn't have any references to take care of, then
             // we will just let the AcDbEntity::deepClone() take care of things.
       if (refEntIds.isEmpty())
             return AcDbEntity::subDeepClone(pOwner, pClonedObject, idMap, isPrimary);
 
        // If this object is in the idMap and is already
        // cloned, then return.
       bool tmpIsPrimary = isPrimary ? true : false;   // get around compiler performance warning
    AcDbIdPair idPair(objectId(), AcDbObjectId::kNull, false, tmpIsPrimary);
    if (idMap.compute(idPair) &amp;&amp; (idPair.value() != NULL))
        return Acad::eOk;
 
    // STEP 1:
    // Create the clone
    //
    AcDbObject *pClone = (AcDbObject*)isA()-&gt;create();
    if (pClone != NULL)
        pClonedObject = pClone;    // set the return value
    else
        return Acad::eOutOfMemory;
 
    // STEP 2:
    // Append the clone to its new owner.  In this example,
    // we know that we are derived from AcDbEntity, so we
    // can expect our owner to be an AcDbBlockTableRecord,
    // unless we have set up an ownership relationship with
    // another of our objects.  In that case, we need to
    // establish how we connect to that owner in our own
    // way.  This sample shows a generic method using
    // setOwnerId().
    //
    AcDbBlockTableRecord *pBTR = AcDbBlockTableRecord::cast(pOwner);
    if (pBTR != NULL) {
        AcDbEntity* ent = AcDbEntity::cast(pClone);
        pBTR-&gt;appendAcDbEntity(ent);
    }
    else {
        if (isPrimary)
            return Acad::eInvalidOwnerObject;
 
        // Some form of this code is only necessary if
        // anyone has set up an ownership for our object
        // other than with an AcDbBlockTableRecord.
        //
        pOwner-&gt;database()-&gt;addAcDbObject(pClone);
        pClone-&gt;setOwnerId(pOwner-&gt;objectId());
    }
 
    // STEP 3:
    // Now we copy our contents to the clone.  This is done
    // using an AcDbDeepCloneFiler.  This filer keeps a
    // list of all AcDbHardOwnershipIds and
    // AcDbSoftOwnershipIds we, and any classes we derive
    // from,  have.  This list is then used to know what
    // additional, "owned" objects need to be cloned below.
    //
    AcDbDeepCloneFiler filer;
    dwgOut(&amp;filer);
 
    // STEP 4:
    // Rewind the filer and read the data into the clone.
 
    //
    filer.seek(0L, AcDb::kSeekFromStart);
    pClone-&gt;dwgIn(&amp;filer);
 
    // STEP 5:
    // This must be called for all newly created objects
    // in deepClone.  It is turned off by endDeepClone()
    // after it has translated the references to their
    // new values.
    //
    pClone-&gt;setAcDbObjectIdsInFlux();
 
    // STEP 6:
    // Add the new information to the idMap.  We can use
    // the idPair started above.
    //
    idPair.setValue(pClonedObject-&gt;objectId());
    idPair.setIsCloned(Adesk::kTrue);
    idMap.assign(idPair);
 
    // STEP 7:
    // Using the filer list created above, find and clone
    // any owned objects.
    //
    AcDbObject *pSubObject;
    AcDbObject *pClonedSubObject;
    AcDbObjectId id;
    Acad::ErrorStatus es;
    while (filer.getNextOwnedObject(id)) {
            // Open the object and clone it.  Note that we now
            // set "isPrimary" to kFalse here because the object
            // is being cloned, not as part of the primary set,
            // but because it is owned by something in the
            // primary set.
        es = acdbOpenAcDbObject(pSubObject, id, AcDb::kForRead);
        if (es != Acad::eOk)
            continue;   // could have been NULL or erased
 
        pClonedSubObject = NULL;
        pSubObject-&gt;deepClone(pClonedObject, pClonedSubObject, idMap, Adesk::kFalse);
 
            // If this is a kDcInsert context, the objects
            // may be "cheapCloned".  In this case, they are
            // "moved" instead of cloned.  The result is that
            // pSubObject and pClonedSubObject will point to
            // the same object.  So, we only want to close
            // pSubObject if it really is a different object
            // than its clone.
        if (pSubObject != pClonedSubObject)
            pSubObject-&gt;close();
 
            // The pSubObject may either already have been
            // cloned, or for some reason has chosen not to be
            // cloned.  In that case, the returned pointer will
            // be NULL.  Otherwise, since we have no immediate
            // use for it now, we can close the clone.
        if (pClonedSubObject != NULL)
            pClonedSubObject-&gt;close();
    }
 
        // clone the referenced entities
    AcDbObject* ent;
       int len = refEntIds.length();
       for (int i=0; i<len if="" {="" acad::eok)="" (es="=" acdb::kforread);="" refentids[i],="" es="acdbOpenAcDbObject(ent," kclone)="" (reftypes[i]="=" i++)="" pclonedsubobject="NULL;" ;="">deepClone(pOwner, pClonedSubObject, idMap, Adesk::kTrue);
                           if (es == Acad::eOk) {
                                        // see comment above about cheap clone
                                 if (ent != pClonedSubObject)
                                        ent-&gt;close();
 
                                 if (pClonedSubObject != NULL)
                                        pClonedSubObject-&gt;close();
                           }
                    }
             }
                    // this case is needed for RefEdit so we can pass its validation
                    // test when editing a blockReference.  We don't actually clone it
                    // but we add it to the map so it thinks it got cloned and is therefore
                    // a valid "Closed Set" of objects.
             else if (refTypes[i] == kFakeClone) {
            AcDbIdPair idPair(refEntIds[i], refEntIds[i], false, false, true);
            idMap.assign(idPair);
             }
       }
 
       if (ArxDbgOptions::m_instance.m_showDeepCloneDetails) {
             CString titleStr, tmpStr;
             titleStr.Format(_T("End -- deepClone: %s"),
                           ArxDbgUtils::objToClassAndHandleStr(const_cast<arxdbgdbentity   *="">(this), tmpStr));
             ArxDbgUiTdmIdMap dbox(&amp;idMap, acedGetAcadDwgView(), titleStr);
             dbox.DoModal();
       }
 
        // Leave pClonedObject open for the caller
    return Acad::eOk;
}
</arxdbgdbentity></len></arxdbgdbentity></arxdbgdbentity></len></arxdbgdbentity></pre>

<p> Complete Source <p>
<a href="https://github.com/MadhukarMoogala/adn_adskLogo">AdskLogo-Project</a>
<iframe width="400" height="440" src="https://screencast.autodesk.com/Embed/Timeline/ec82d88e-8cbd-42f1-8a40-319722435349" frameborder="0" allowfullscreen="" webkitallowfullscreen=""></iframe>
