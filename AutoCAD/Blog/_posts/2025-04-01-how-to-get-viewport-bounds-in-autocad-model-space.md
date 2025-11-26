---
layout: "post"
title: "How to Get Viewport Bounds in AutoCAD Model Space"
date: "2025-04-01 21:57:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2025/04/how-to-get-viewport-bounds-in-autocad-model-space.html "
typepad_basename: "how-to-get-viewport-bounds-in-autocad-model-space"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>
  <p>When working with viewports in AutoCAD, particularly when translating between paper space and model space, it is
    often necessary to determine the viewport boundaries within the model space. This blog post will guide you through
    obtaining viewport extents and transforming them appropriately.</p>
  <h2>Understanding the Viewport Transformation</h2>
  <p>AutoCAD viewports in paper space define a window into model space. To retrieve the viewport bounds in model space,
    we need to apply a transformation matrix that accounts for scaling, rotation, and translation.</p>
  <h3>Key Steps:</h3>
  <ol>
    <li>
      <p><strong>Retrieve viewport properties</strong> – Extract viewport size, center, and transformation
        parameters.</p>
    </li>
    <li>
      <p><strong>Compute transformation matrix</strong> – Convert from Paper Space Display Coordinate System
        (PSDCS) to the World Coordinate System (WCS).</p>
    </li>
    <li>
      <p><strong>Transform viewport extents</strong> – Apply the matrix to get accurate model space boundaries.
      </p>
    </li>
    <li>
      <p><strong>Create a bounding polyline</strong> – Draw a rectangle representing the viewport bounds.</p>
    </li>
  </ol>
  <h2>Implementation</h2>
  <p>Below is the C++ implementation using ObjectARX APIs to extract the viewport bounds in model space.</p>
  <h3>Setting Viewport Properties</h3>
  <pre class="prettyprint lang-cpp">    <code> 
      static void SetViewportProperties(AcDbViewport* pVp,
        AcDbViewTableRecord&amp; vwRec)
      {

        vwRec.setHeight(pVp-&gt;height());
        vwRec.setWidth(pVp-&gt;width());
        vwRec.setCenterPoint(AcGePoint2d(pVp-&gt;centerPoint().x,
         pVp-&gt;centerPoint().y));
        vwRec.setViewDirection(pVp-&gt;viewDirection());
        vwRec.setTarget(pVp-&gt;viewTarget());
        vwRec.setLensLength(pVp-&gt;lensLength());
        vwRec.setViewTwist(pVp-&gt;twistAngle());
        vwRec.setFrontClipDistance(pVp-&gt;frontClipDistance());
        vwRec.setBackClipDistance(pVp-&gt;backClipDistance());
        vwRec.setPerspectiveEnabled(pVp-&gt;isPerspectiveOn());
        vwRec.setFrontClipEnabled(pVp-&gt;isFrontClipOn());
        vwRec.setBackClipEnabled(pVp-&gt;isBackClipOn());
        vwRec.setFrontClipAtEye(pVp-&gt;isFrontClipAtEyeOn());
        vwRec.setViewAssociatedToViewport(pVp-&gt;isUcsSavedWithViewport());
        vwRec.setVisualStyle(pVp-&gt;visualStyleId());
        vwRec.setIsPaperspaceView(true); 
    }     
    </code>     
  </pre>

  <h3>Computing the Transformation Matrix</h3>
  <pre class="prettyprint lang-cpp">    <code> 
    static AcGeMatrix3d GetTransformationMatrix(AcDbViewport* pVpPtr) {
      if (!pVpPtr) return AcGeMatrix3d::kIdentity;
  
      // Extract viewport properties
      AcGePoint3d viewCenterPSDCS(pVpPtr-&gt;viewCenter().x, pVpPtr-&gt;viewCenter().y, 0.0);
      AcGePoint3d viewCenterDCS = pVpPtr-&gt;centerPoint();
      double scale = 1.0 / pVpPtr-&gt;customScale();
  
      // Transform from PSDCS to DCS
      AcGeMatrix3d transformPSDCSToDCS = AcGeMatrix3d::scaling(scale, viewCenterDCS) *
          AcGeMatrix3d::translation(viewCenterDCS - viewCenterPSDCS);
  
      // Retrieve view properties
      AcDbViewTableRecord vwRec;
      SetViewportProperties(pVpPtr, vwRec);
      AcGeVector3d targetDirection = vwRec.target() - AcGePoint3d::kOrigin;
  
      AcGeMatrix3d translation = AcGeMatrix3d::translation(targetDirection);
      double twistAngle = vwRec.viewTwist();
      AcGeVector3d viewDirection = vwRec.viewDirection();
      AcGePoint3d pointOfRotation = vwRec.target();
  
      AcGeMatrix3d rotation = AcGeMatrix3d::rotation(-twistAngle, 
                                                  viewDirection,
                                                  pointOfRotation);
  
      // Transform from DCS to WCS
      AcGeMatrix3d transformDCSToWCS = AcGeMatrix3d::planeToWorld(viewDirection)
                                       * translation
                                       * rotation;
  
    return transformDCSToWCS * transformPSDCSToDCS;
    }
    </code>
  </pre>

  <h3>Retrieving Viewport Bounds in Model Space</h3>
  <p>Once we have the transformation matrix, we can apply it to the viewport extents to get the bounds in model space.
  </p>
  <pre class="prettyprint lang-cpp">    <code> 
      static AcDbExtents GetViewportBoundsInMS(AcDbViewport* pVp) {
        AcGeMatrix3d xform = GetTransformationMatrix(pVp);    
        AcDbExtents vpExtents;
        pVp-&gt;getGeomExtents(vpExtents);
        AcGePoint3d minPt = vpExtents.minPoint();
        AcGePoint3d maxPt = vpExtents.maxPoint();  
        vpExtents.transformBy(xform);
        return vpExtents;       
    }
    </code>
  </pre>

  <h3>Test Method</h3>
  <p>Finally, we can create a test method or run through a Command to execute the above functions and visualize the
    viewport bounds.</p>
  <pre class="prettyprint lang-cpp">    <code> 
      void runGetVpBoundsInMS() {
        auto workingDb = acdbHostApplicationServices()-&gt;workingDatabase();
        if (workingDb-&gt;tilemode() == Adesk::kTrue) {
            acutPrintf(_T("\nTilemode is on! Execute in PAPERSPACE"));
            return;
        }
    
        AcDbObjectPointer &lt;AcDbViewport&gt; pViewportPtr(workingDb-&gt;paperSpaceVportId(),
           AcDb::kForRead);
        if (!eOkVerify(pViewportPtr.openStatus())) return;
    
        AcDbObjectPointer &lt;AcDbLayout&gt; pLayoutPtr(workingDb-&gt;currentLayoutId(), AcDb::kForRead);
        if (!eOkVerify(pLayoutPtr.openStatus())) return;
    
        AcDbObjectIdArray vpIds = pLayoutPtr-&gt;getViewportArray();
        if (vpIds.length() &lt; 2) {
            acutPrintf(_T("\nNo viewports found in paperspace"));
            return;
        }
    
        AcDbObjectPointer &lt;AcDbViewport&gt; pVpPtr(vpIds[1], AcDb::kForRead);
        if (!eOkVerify(pVpPtr.openStatus())) return;
    
        AcDbExtents vpExtents = GetViewportBoundsInMS(pVpPtr.object());
        AcGePoint3d minPt = vpExtents.minPoint();
        AcGePoint3d maxPt = vpExtents.maxPoint();
    
        AcDbPolyline* pPoly = new AcDbPolyline(4);
        pPoly-&gt;addVertexAt(0, AcGePoint2d(minPt.x, minPt.y));  
        pPoly-&gt;addVertexAt(1, AcGePoint2d(maxPt.x, minPt.y));
        pPoly-&gt;addVertexAt(2, AcGePoint2d(maxPt.x, maxPt.y));
        pPoly-&gt;addVertexAt(3, AcGePoint2d(minPt.x, maxPt.y));
        pPoly-&gt;setClosed(true);
        pPoly-&gt;setColorIndex(3);
        addToDb(pPoly, workingDb);
    }
    </code>
  </pre>
  <p>Here is video</p>
  <iframe width="560" height="315" title="Get Viewport Bounds in Model Space" src="https://www.youtube.com/embed/96iWh6mYCuI" frameborder="0" allowfullscreen="">
  </iframe>
