---
layout: "post"
title: "How to find Circle from a given centre point"
date: "2022-05-30 15:44:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2022/05/how-to-find-circle-from-a-given-centre-point.html "
typepad_basename: "how-to-find-circle-from-a-given-centre-point"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>

<p>Selection filters are very powerful device yet most of the times it is overlooked, using selection filters you can achieve many regular tasks of conditional selections.</p>
<p>In this snippet we will see, how you can leverage the selection filters to get a circle(s) from a given a centre point! </p>
<p>Do let me know if you have used conditional filters in any interesting scenario. </p>

<p>How do I prepare a conditional filter sequence ?</p>
<p>For example, we have a task to find all the <code>MTEXT</code> entities with particular background mask color fill </p>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fad650e200c-pi"><img width="244" height="182" title="BGMASK" style="margin: 0px; display: inline; background-image: none;" alt="BGMASK" src="/assets/image_812829.jpg" border="0"></a>
<p>From the above picture, we have few MText entities with different colors, suppose if I want to get a MText with green background color fill ?</p>
<p>First, I would check the entity information, I can use this entity information in building a selection filter, <code> (entget (car (entsel))) </code> </p>
<p> Applying this code and pick MTEXT entity with green background mask would give me <p>
<pre>((-1 . <entity   138ea2e9c90="">) (0 . "MTEXT") (330 . <entity   138ea2e39f0="">) (5 . "299") (100 . "AcDbEntity") (67 . 0) (410 . "Model") (8 . "0") (100 . "AcDbMText") (10 1495.9 1933.67 0.0) (40 . 2.5) (41 . 17.7906) (46 . 0.0) (71 . 1) (72 . 5) (1 . "AUTOCAD") (7 . "Standard") (210 0.0 0.0 1.0) (11 1.0 0.0 0.0) (42 . 16.8895) (43 . 2.58697) (50 . 0.0) (73 . 1) (44 . 1.0) (90 . 1) (63 . 3) (45 . 1.0) (441 . 0))
</entity></entity></pre>
<p> Now referring to MTEXT DXF reference, it is clear that DXF Code 90 is background mask on and 63 is the color when Background mask is enabled <p>
<a href="https://help.autodesk.com/view/OARX/2022/ENU/?guid=GUID-5E5DB93B-F8D3-4433-ADF7-E92E250D2BAB#:~:text=0.25%20to%204.00-,90,-Background%20fill%20setting">DXF Reference</a>

<p>Now, to pick all MTEXT entities with background color Green </p>
<p>The conditional filter would be <code> (90 . 1) (63 . 3) </code> </p>
<p>This give length of entities respecting our filter
<pre>(sslength  (ssget "X"  (list  (cons 0 "MTEXT")(cons 90 1)(cons 63 3))))</pre>


<p>Back to our topic, applying similar logic we can construct a code to filter circle based on given center point </p>
<pre class="prettyprint">
AcDbObjectIdArray getCirclesFromAGivenPoint(AcGePoint3d centerPoint) {

    AcDbObjectIdArray idArray;
    ads_name ss, entName;
    /*Construct a filter list*/
    resbuf* filterRb = acutBuildList(RTDXF0, L"CIRCLE",
        10, asDblArray(centerPoint),
        RTNONE);    bool hasSS = false;

    /*Make selection based on filtered list*/
    if (acedSSGet(_T("_X"), NULL, NULL, filterRb, ss) == RTNORM) {
        Adesk::Int32 len;
        acedSSLength(ss, &amp;len);
        if (len &gt; 0) {
            hasSS = true;
            /*Collect and append the objectId to an array*/
                    for (int nEnt = 0; nEnt &lt; len; nEnt++)
                    {
                           if (acedSSName(ss, nEnt,entName) == RTNORM)
                           {
                                 AcDbObjectId selId;
                                 if (acdbGetObjectId(selId, entName) == Acad::eOk)
                                        idArray.append(selId);
                           }
                    }
        }
        freeResbuf(&amp;filterRb);

        if (hasSS)
            acedSSFree(ss);
    }
    return idArray;
}

</pre>
