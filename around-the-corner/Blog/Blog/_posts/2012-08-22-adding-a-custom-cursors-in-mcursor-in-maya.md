---
layout: "post"
title: "Adding a Custom Cursor in MCursor in Maya"
date: "2012-08-22 02:15:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "Kristine Middlemiss"
  - "Maya"
  - "Qt"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/adding-a-custom-cursors-in-mcursor-in-maya.html "
typepad_basename: "adding-a-custom-cursors-in-mcursor-in-maya"
typepad_status: "Publish"
---

<p>To create a custom cursor when using MPxContext::SetCursor, a cursor requires two XBM images: one for the mask, which defines which bits are transparent and which opaque, and one for the color, which defines which bits are white and which are black.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017617364f1a970c-pi" style="display: inline;"><img alt="Cursor" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017617364f1a970c image-full" src="/assets/image_317ef9.jpg" title="Cursor" /></a></p>
<p>If you look at an XBM image file you&#39;ll see that it&#39;s just a snippet of C code which defines macros giving the width and height of the image, and an array of unsigned char. E.g:</p>
<p>#define HtumbleCursorMask_width 16<br />#define HtumbleCursorMask_height 16<br />static unsigned char HtumbleCursorMask_bits[] = {<br />&#0160; 0xc0, 0x07, 0xe0, 0x0f, 0xf0, 0xdf, 0xf8, 0xff, 0x3c, 0xf8, 0x1e, 0xf8,<br />&#0160; 0x1e, 0xfc, 0x00, 0xfc, 0x7e, 0x00, 0x7e, 0xf0, 0x3e, 0xf0, 0x3e, 0x78,<br />&#0160; 0xfe, 0x3f, 0xf6, 0x1f, 0xe0, 0x0f, 0xc0, 0x07, };</p>
<p>So you can include the two XBM image files directly into the C++ source for your plug-in, and then pass the sizes and arrays to the MCursor constructor.</p>
<p>This will only work if the XBM image is known at compile time and doesn&#39;t change. If you want to dynamically create a cursor at runtime from an arbitrary pair of XBM files then you would have to either write some code to parse the information from the files or find a library routine to do it for you. It&#39;s probably possible to do it with the Qt&#39;s image classes (QImage, QBitmap, QPixmap) but I haven&#39;t tried it myself.</p>
<p>One suggestion I have heard is overriding an existing cursor by replacing the contents of the header files,&#0160;however this&#0160;will not work for these reasons:</p>
<p>1) The header files were only read when Maya was built, not at runTime. They&#39;re not even shipped to you.</p>
<p>2) Since the switch to Qt we no longer use those XBM files for cursors. Instead we use PNG files. There should be a constructor for MCursor which takes a PNG file, but we don&#39;t yet have that.</p>
<p>3) Even the PNG files which Maya now uses are only used when Maya is built and do not get shipped to you. Their contents are embedded into a Qt resource file. So overriding the PNGs won&#39;t work, either.</p>
<p>An example of a custom cursor is in the Devkit sample lassoTool.</p>
<p>Here is the required XBM information:</p>
<p>Files used for:<br />Cursors</p>
<p>File Format:<br />xbm</p>
<p>Requirements:<br />16x16<br />transparent background</p>
<p>Created with this application:<br />xpaint</p>
<p>Naming conventions:<br />H&lt;name&gt;Cursor.h<br />H&lt;name&gt;CursorMask.h</p>
<p>Where used:<br />Mouse pointer</p>
<p>Enjoy,</p>
<p>Kristine</p>
