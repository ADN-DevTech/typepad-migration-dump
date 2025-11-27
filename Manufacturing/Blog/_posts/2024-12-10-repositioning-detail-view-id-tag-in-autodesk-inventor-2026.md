---
layout: "post"
title: "Repositioning Detail View ID Tag in Autodesk Inventor 2026 API"
date: "2024-12-10 10:49:38"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2024/12/repositioning-detail-view-id-tag-in-autodesk-inventor-2026.html "
typepad_basename: "repositioning-detail-view-id-tag-in-autodesk-inventor-2026"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>When working in Autodesk Inventor, keeping your drawings clean and readable is essential. A common challenge faced by users is repositioning the <strong>detail view ID tag</strong>—the identifier that marks a detail or section view. While moving a view label is straightforward, adjusting the position of the ID tag using the Inventor API has traditionally been far more complex. This blog explores the current limitations and a practical VBA workaround, and highlights a major improvement coming in <strong>Inventor 2026</strong>.</p>
<h3>Understanding the Challenge</h3>
<p>The Inventor API currently allows you to manipulate the <strong>fence</strong> (the circular or rectangular boundary around a detail view), including its shape and size. However, repositioning the <strong>ID tag</strong> that labels the view is not directly supported.</p>
<p>Some users attempt to work around this by deleting and recreating the detail view, which causes the reference to reappear next to the fence—but notably, <em>without a leader</em>. Unfortunately, the API doesn’t yet support adding a leader to restore that visual connection.</p>
<h3>What’s Coming in Inventor 2026</h3>
<p>There’s good news on the horizon. The <strong>Inventor 2026</strong> release introduces a new API object: <code>DrawingViewAnnotation</code>. This enhancement includes the <code>TextPosition</code> property, which allows developers to <strong>programmatically move the ID tag</strong> of a drawing view—something that was previously unsupported.</p>
<p>The beta version of Inventor 2026 is already available, and we encourage developers and power users to begin testing these new capabilities.</p>
<h3>Sample VBA Code to Reposition the ID Tag</h3>
<p>To help you get started, here&#39;s a simple <strong>VBA script</strong> that demonstrates how to move the annotation text of a detail or section view using the new functionality:</p>
<pre><code>
Sub MoveViewAnnotationTextPositionSample()

    Dim oDoc As DrawingDocument
    Set oDoc = ThisApplication.ActiveDocument

    Dim oView As DrawingView
    Dim oTemp As DrawingView

    For Each oTemp In oDoc.ActiveSheet.DrawingViews
        If oTemp.ViewType = kDetailDrawingViewType Or oTemp.ViewType = kSectionDrawingViewType Then

            Set oView = oTemp

            Dim oViewAnnotation As DrawingViewAnnotation
            Set oViewAnnotation = oView.ViewAnnotation

            &#39; Move the annotation text
            Dim oPt As Point2d
            Set oPt = oViewAnnotation.TextPosition

            oPt.X = oPt.X + 2
            oPt.Y = oPt.Y + 3

            oViewAnnotation.TextPosition = oPt
            oDoc.Update

        End If
    Next

End Sub
</code></pre>
<h3 class="" data-end="2724" data-start="2706">Visual Example</h3>
<p class="" data-end="2801" data-start="2726"><strong data-end="2736" data-start="2726">Before</strong><br data-end="2739" data-start="2736" /><em data-end="2801" data-start="2739">The view ID tag is misaligned or overlapping other elements.</em></p>
<p><br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c60393200c-pi" style="display: inline;"><img alt="Before" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c60393200c image-full img-responsive" src="/assets/image_662f79.jpg" title="Before" /></a><br /><br /></p>
<p><strong>After<br data-end="2815" data-start="2812" /></strong><em data-end="2875" data-start="2815">The tag is repositioned cleanly within the drawing border.</em></p>
<p><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c60395200c-pi" style="display: inline;"><img alt="After" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c60395200c image-full img-responsive" src="/assets/image_4d87c0.jpg" title="After" /></a><br /></strong><strong><br /><br /></strong></p>
<p><strong>Conclusion</strong></p>
<p class="" data-end="3258" data-start="2897">While current versions of Autodesk Inventor have limitations in handling detail view ID tags via the API, the <strong data-end="3024" data-start="3007">Inventor 2026</strong> update is a game changer. With the new <code data-end="3087" data-start="3064">DrawingViewAnnotation</code> object, developers will gain the long-awaited ability to programmatically reposition ID tags—improving drawing clarity and maintaining compliance with drafting standards.</p>
<p class="" data-end="3405" data-start="3260">Until then, the provided VBA workaround can help you manage your annotations more effectively and keep your documentation professional and clean.</p>
