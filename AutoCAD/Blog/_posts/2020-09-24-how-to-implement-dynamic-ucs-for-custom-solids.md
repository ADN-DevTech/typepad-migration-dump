---
layout: "post"
title: "How to Implement Dynamic UCS for Custom Solids"
date: "2020-09-24 03:15:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2020/09/how-to-implement-dynamic-ucs-for-custom-solids.html "
typepad_basename: "how-to-implement-dynamic-ucs-for-custom-solids"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>You need to derive&nbsp; from this class(AcDbDynamicUCSPE) in order to enable Dynamic UCS switching for your custom objects or for other core objects. </p>
<p>
      By default, the core DUCS code will call this protocol extension on
      objects under the cursor during the initial point acquisition in
      DUCS-enabled command.
    </p>
    <p>
      Derived classes must override and implement
      <code>getCandidatePlanes()</code>.
    </p>
    <p>
      And, also if you don’t implement AutoCAD Solid’s and it derived entities
      can participate in dynamic UCS using the default
      <code>AcDb3dSolidDynamicUCSPE</code>, this&nbsp; protocol extension is
      always registered by AcDb during startup.&nbsp; So, unless some other app
      has explicitly removed or replaced it, it will always be in place for
      AcDb3dSolids and classes derived from that.
    </p>
    <p>
      There's also an <code>AcDbEntityDynamicUCSPE</code> protocol extension
      that's added at the AcDbEntity level to provide a default implementation
      for all classes.
    </p>
    <p><strong>Note:</strong></p>
    <p>
      <code>getCandidatePlanes()</code> is only called for entities that have
      subentities.&nbsp; For such entities, it is called once for each subentity
      with the subentId argument being the id of the subent for which it is
      being called.
    </p>
    <p>
      For entities that don't have subentities, the
      <code>AcDbNonSubEntDynamicUCSPE</code> protocol extension base class
      should be used, and its <code>getCandidatePlane()</code>
      method will be called.
    </p>
    <p><br></p>


<pre class="prettyprint">Acad::ErrorStatus AsdkUCSPE::getCandidatePlanes(
	AcArray<acgeplane>&amp; results,
	double&amp; distToEdge,
	double&amp; objWidth,
	double&amp; objHeight,
	AcDbEntity* pEnt,
	const AcDbSubentId&amp; subentId,
	const AcGePlane&amp; viewplane,
	AcDbDynamicUCSPE::Flags flags) const
{
	Acad::ErrorStatus es = Acad::eInvalidInput;
	if (!pEnt)
		return es;

	// Check if User selected custom Solid plane, we are not doing anything here, just to make sure 
	   //Debugger hits us, ideally we are expected to populate the result array 
	   // with one or more AcGePlane objects and return Acad::eOk if successful.

	CMyBox* pSolid = CMyBox::cast(pEnt);
	if (!VERIFY(pSolid != NULL))
		return Acad::eInvalidInput;

	AcDb::SubentType seType = subentId.type();
	if (seType != AcDb::kFaceSubentType)
		return Acad::eWrongSubentityType;

	AcDbFullSubentPath facePath(pEnt-&gt;objectId(), subentId);
	std::auto_ptr<acdbentity> pFaceSubEnt;
	pFaceSubEnt.reset(pEnt-&gt;subentPtr(facePath));
	
	if (pFaceSubEnt.get() &amp;&amp; 
	pFaceSubEnt-&gt;isKindOf(AcDbRegion::desc()))
	{
		AcDbRegion* reg = AcDbRegion::cast(pFaceSubEnt.get());
		assert(reg != NULL);
		if (!reg)
			return (Acad::eWrongObjectType);
		AcGePlane entPlane;
		AcDb::Planarity kPanarity;
		es = reg-&gt;getPlane(entPlane, kPanarity);

		//- Test for intersection 
		// between the circle's plane and Zaxis of viewplane
		AcGeLine3d line;
		line.set(viewplane.pointOnPlane(),
		viewplane.normal());

		AcGePoint3d intersectPnt;
		if (entPlane.intersectWith(line, intersectPnt) == Adesk::kTrue)
		{
			AcDbExtents extents;
			if (reg-&gt;getGeomExtents(extents) != Acad::eOk)
				return (Acad::eInvalidInput);

			const double diagonal = extents.minPoint().
						distanceTo(extents.maxPoint());
			objWidth = diagonal;
			objHeight = diagonal;

			//- now calculate closest edge to the intersection point
			//- and set the origin and X axis accordingly

			AcGePoint3d origin;
			AcGeVector3d xAxis, yAxis;
			entPlane.getCoordSystem(origin, xAxis, yAxis);
			distToEdge = origin.distanceTo(intersectPnt);

			AcGePlane newPlane(entPlane);
			results.append(newPlane);
			return Acad::eOk;

		}
		return Acad::eNotApplicable;
	}
	return Acad::eNotApplicable;
}
</acdbentity></acgeplane></pre>
<p><a href="https://github.com/MadhukarMoogala/DynamicUCS">Demo and source code</a></p>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde959acd200c-pi"><img width="673" height="492" title="DynamicUCS" style="display: inline;" alt="DynamicUCS" src="/assets/image_355144.jpg"></a>
