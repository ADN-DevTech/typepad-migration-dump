---
layout: "post"
title: "Navisworks API: Resolving Exceptions in LiNwcApiTerminate() When Using NwCreate API"
date: "2025-03-06 02:09:06"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2025/03/navisworks-api-resolving-exceptions-in-linwcapiterminate-when-using-nwcreate-api-1.html "
typepad_basename: "navisworks-api-resolving-exceptions-in-linwcapiterminate-when-using-nwcreate-api-1"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p>When working with the Navisworks NwCreate API, you may encounter an exception at LiNwcApiTerminate(). This issue occurs in multiple versions, including NwCreate 2022, 2023, and 2024. This post explains why this happens and how to fix it.</p>
<p><strong>Issue Overview: </strong>Following the &quot;<a href="https://adndevblog.typepad.com/aec/2013/07/get-started-with-nwcreate-part-1.html">Get Started with NwCreate - Part 1</a>&quot; article from the Autodesk Developer Network (ADN) blog, I created a sample project that successfully generates a .nwc file. However, an exception occurs when calling LiNwcApiTerminate(), causing the program to crash.<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e45acb200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="NW_Error" class="asset  asset-image at-xid-6a0167607c2431970b02e860e45acb200b img-responsive" src="/assets/image_25020.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="NW_Error" /></a><br /><br /></p>
<p><strong>Cause of the Issue:</strong> The main reason for this exception is not properly releasing all handles before calling LiNwcApiTerminate(). If any handles remain open, the API does not shut down cleanly, leading to an error.</p>
<p><strong>Solution:</strong> Avoid this issue by ensuring all handles are properly cleaned up. Call <code data-end="440" data-start="413">LiNwcSceneDestroy(scene);</code> for cleanup<code data-end="110" data-start="83"></code></p>
<pre class="prettyprint">#include &lt;iostream&gt;
#include &lt;tchar.h&gt;
#include &quot;nwcreate/LiNwcAll.h&quot;

// Forward declaration for doExport function
void doExport();

int main()
{
    // Initialise low-level API first.
    LiNwcApiErrorInitialise();
    // Then initialise the rest of the API.
    switch (LiNwcApiInitialise())
    {
    case LI_NWC_API_OK:
        doExport();
        break;
    case LI_NWC_API_NOT_LICENSED:
        printf(&quot;Not Licensed\n&quot;);
        return 1;
    case LI_NWC_API_INTERNAL_ERROR:
    default:
        printf(&quot;Internal Error\n&quot;);
        return 1;
    }

    // Terminate API after use
    LiNwcApiTerminate();
    return 0;
}

void doExport()
{
    LtWideString wfilename = L&quot;C:\\test.nwc&quot;;
    

    // Create scene and geometry
    LtNwcScene scene = LiNwcSceneCreate();
    LtNwcGeometry geom = LiNwcGeometryCreate();

    if (!scene || !geom)
    {
        printf(&quot;Failed to create scene or geometry\n&quot;);
        return;
    }

    // Open geometry stream
    LtNwcGeometryStream stream = LiNwcGeometryOpenStream(geom);
    if (!stream)
    {
        printf(&quot;Failed to open geometry stream\n&quot;);
        LiNwcGeometryDestroy(geom);
        LiNwcSceneDestroy(scene);
        return;
    }

    LiNwcGeometryStreamBegin(stream, 0);
    LiNwcGeometryStreamTriangleVertex(stream, 1, 0, 0);
    LiNwcGeometryStreamTriangleVertex(stream, 2, 0, 10);
    LiNwcGeometryStreamTriangleVertex(stream, 3, 10, 10);
    LiNwcGeometryStreamEnd(stream);
    LiNwcGeometryStreamColor(stream, 0, 1, 0, 1);

    // Close geometry stream
    LiNwcGeometryCloseStream(geom, stream);

    // Add geometry to scene
    LiNwcSceneAddNode(scene, geom);

    // Cleanup geometry
    LiNwcGeometryDestroy(geom);

    // Write NWC file
    if (LiNwcSceneWriteCacheEx(scene, wfilename, wfilename, 0, 0) != LI_NWC_API_OK)
    {
        printf(&quot;Failed to write NWC file\n&quot;);
    }

    // Destroy scene to free memory
    LiNwcSceneDestroy(scene);
}

</pre>
