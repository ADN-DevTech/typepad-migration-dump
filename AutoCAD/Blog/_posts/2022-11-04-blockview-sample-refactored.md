---
layout: "post"
title: "BlockView Sample Refactored"
date: "2022-11-04 01:38:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2022/11/blockview-sample-refactored.html "
typepad_basename: "blockview-sample-refactored"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p><font size="2">By </font><a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self"><font size="2">Madhukar Moogala</font></a></p>

<p><font face="Arial" size="2">BlockView is an ObjectARX graphics sample which use to be shipped with ObjectARX in previous year</font></p><font face="Arial" size="2">
</font><p><font face="Arial" size="2">I've recently recieved a request from an ADN partner</font></p><font face="Arial" size="2">
</font><p><strong><font face="Arial" size="2">The problem</font></strong></p><font face="Arial" size="2">

</font><blockquote><font face="Arial" size="2">
If we are running in 2022 or 2023 and we have Hardware Acceleration turned ON. We get some graphics not showing up in our CGsPreviewCtrl.
Specifically the void CGsPreviewCtrl::RubberRectangle(CPoint startPt, CPoint endPt) will not display on the view.
Again, this only occurs in AutoCAD 2022 &amp; 20223, on Windows 11 with Hardware Acceleration turned ON. If we turn OFF hardware acceleration it works, or if we run on Windows 10 or if on AutoCAD 2021 and before it appears to work.
</font></blockquote><font face="Arial" size="2">

</font><p><strong><font face="Arial" size="2">Reason</font></strong></p><font face="Arial" size="2">

</font><p><font face="Arial" size="2">We start to support DX12 from  AutoCAD 2022. DX12 uses flip mode but GDI uses blit mode. The issue happens because the below code attempt to use GDI to draw over a Window used by DX12.</font></p><font face="Arial" size="2">

</font><pre class="prettyprint lang-cpp"><font face="Arial" size="2">void CGsPreviewCtrl::RubberRectangle(CPoint startPt, CPoint endPt) 
{
  // get the device context for the client area
  CDC *cdc = this-&gt;GetDC();
  // if ok
  if (cdc != NULL)
  {
    HDC hdc = cdc-&gt;GetSafeHdc();
    // Create a black pen with a dotted style to draw the border of the rectangle.
    HPEN gdiPen = CreatePen(PS_DOT, 1, RGB(0,0,0));
    // Set the ROP cdrawint mode to XOR.
    SetROP2(hdc, R2_XORPEN);

    // Select the pen into the device context.
    HGDIOBJ oldPen = SelectObject(hdc, gdiPen);

    // Create a stock NULL_BRUSH brush and select it into the device
    // context so that the rectangle isn't filled.
    HGDIOBJ oldBrush = SelectObject(hdc, GetStockObject(NULL_BRUSH) );

    // Now XOR the hollow rectangle on the Graphics object with
    // a dotted outline.
    Rectangle(hdc, startPt.x, startPt.y, endPt.x, endPt.y);

    // Put the old stuff back where it was.
    SelectObject(hdc, oldBrush); // no need to delete a stock object
    SelectObject(hdc, oldPen);
    DeleteObject(gdiPen);		// but we do need to delete the pen

    // Return the device context to Windows.
    this-&gt;ReleaseDC(cdc);
  }
}

</font></pre><font face="Arial" size="2">
</font><p><font face="Arial" size="2">It is highly recommended to move away from GDI API </font></p><font face="Arial" size="2">
</font><p><font face="Arial" size="2">In case you can't invest in upgrading the code, you can use following workaround to set GFXDX12=0 and close AutoCAD, it will switch to DX11</font></p><font face="Arial" size="2">

</font><p><font face="Arial" size="2">We updated the old Arx sample to work effectively without switching to DX11 in AutoCAD 2022 and later version.</font></p><p><font face="Arial" size="2">Github - <a href="https://github.com/MadhukarMoogala/BlockView" target="_blank"><font face="Arial" size="2">BlockView</font></a></font></p>
