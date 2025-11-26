---
layout: "post"
title: "How to determine if two line segments are overlapping in AutoCAD"
date: "2023-11-21 08:44:00"
author: "Sreeparna Mandal"
categories:
  - "Sreeparna Mandal"
original_url: "https://adndevblog.typepad.com/autocad/2023/11/how-to-determine-if-two-line-segments-are-overlapping-in-autocad.html "
typepad_basename: "how-to-determine-if-two-line-segments-are-overlapping-in-autocad"
typepad_status: "Publish"
---

<p> <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script> </p> <p>By <a href="https://adndevblog.typepad.com/autocad/sreeparna-mandal.html" target="_self">Sreeparna Mandal</a></p>  <p> To check if two line segments are overlapping, we shall use the AcGeLinearEnt3d::overlap() method.  It returns the line that coincides with their region of overlap. The declaration of the method goes as mentioned:  <pre class="prettyprint">

<em>GE_DLLEXPIMPORT Adesk::Boolean overlap(
    const AcGeLinearEnt3d& line, 
    AcGeLinearEnt3d*& overlap, 
    const AcGeTol& tol = AcGeContext::gTol
) const;</em>
</pre>

<p>
Here, the overlap parameter is null if this function returns a value of Adesk::kFalse, else the overlap line may be an object of any derived class of AcGeLinearEnt3d, depending on the types of the two lines. The overlap() method can also be used for an input line which is 2D linear entity.
<p>
Below is the code snippet for its usage:


<pre class="prettyprint">
static void ADSKMyGroupCheckOverlap()
{

	AcGeLineSeg3d line1(AcGePoint3d(0, 0, 0), AcGePoint3d(5, 0, 0));
	AcGeLineSeg3d line2(AcGePoint3d(2, 2, 0), AcGePoint3d(2, -5, 0)); //intersecting with line1
	AcGeLineSeg3d line3(AcGePoint3d(0, 0, 0), AcGePoint3d(10, 0, 0)); // overlapping with line1

	bool isOverlapped = Adesk::kFalse;

	AcGeLinearEnt3d* overlappingEnt;

	isOverlapped = line1.overlap(line3, overlappingEnt);

	if (isOverlapped)
	{
		AcGePoint3d startPt, endPoint;
		if (!overlappingEnt->hasEndPoint(endPoint))
			return;
		if (!overlappingEnt->hasStartPoint(startPt))
			return;
		acutPrintf(_T("\nBoth lines are overlapping. Start and end points of the overlapping segment are:\n"));
		acutPrintf(_T("\nStart point: %f, %f, %f"), startPt.x, startPt.y, startPt.z);
		acutPrintf(_T("\nEnd point: %f, %f, %f"), endPoint.x, endPoint.y, endPoint.z);
	}
	else
	{
		acutPrintf(_T("\nLines are not overlapping."));
	}
}
</pre>
