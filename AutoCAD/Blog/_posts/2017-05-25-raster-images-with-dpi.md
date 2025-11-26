---
layout: "post"
title: "Raster Images With DPI"
date: "2017-05-25 23:41:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/05/raster-images-with-dpi.html "
typepad_basename: "raster-images-with-dpi"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>When Images with encoded DPI information under <a href="https://en.wikipedia.org/wiki/Exif">EXIF</a> structure is attached in AutoCAD, application is not able to correctly detect the resolution information.</p> <p>To understand this better, I have attached one such image with resolution in AutoCAD.</p> <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb099f4aad970d-pi"><img title="RectifiedImageWithWrongDPI" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="RectifiedImageWithWrongDPI" src="/assets/image_349798.jpg" width="349" height="325"></a></p> <p>From above screenshot, the horizontal and vertical resolution are incorrect.</p> <p>However if we use any EXIF library the DPI resolution in the image can be viewed.</p> <p>For example, I have used </p> <p><a href="https://github.com/drewnoakes/metadata-extractor-dotnet">https://github.com/drewnoakes/metadata-extractor-dotnet</a> Module to extract information from image. (Thanks a ton)</p><pre style="background: #ffffff; color: #000000"><span style="font-weight: bold; color: #800000">string</span> imagePath <span style="color: #808030">=</span> <span style="color: #800000">"</span><span style="color: #0000e6">D:\\Temp\\DPI_Resolution\\Rectified Scaled image.jpg</span><span style="color: #800000">"</span><span style="color: #800080">;</span>
IEnumerable<span style="color: #808030">&lt;</span>Directory<span style="color: #808030">&gt;</span> directories <span style="color: #808030">=</span> ImageMetadataReader<span style="color: #808030">.</span>ReadMetadata<span style="color: #808030">(</span>imagePath<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">foreach</span> <span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">var</span> directory <span style="font-weight: bold; color: #800000">in</span> directories<span style="color: #808030">)</span>
<span style="font-weight: bold; color: #800000">foreach</span> <span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">var</span> tag <span style="font-weight: bold; color: #800000">in</span> directory<span style="color: #808030">.</span>Tags<span style="color: #808030">)</span>
Console<span style="color: #808030">.</span>WriteLine<span style="color: #808030">(</span>$<span style="color: #800000">"</span><span style="color: #0000e6">{directory.Name} - {tag.Name} = {tag.Description}</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
</pre><pre style="background: #000000; color: #d1d1d1">JPEG - Compression Type = Baseline
JPEG - Data Precision = 8 bits
JPEG - Image Height = 2212 pixels
JPEG - Image Width = 2949 pixels
JPEG - Number of Components = 3
JPEG - Component 1 = Y component: Quantization table 0, Sampling factors 2 horiz/2 vert
JPEG - Component 2 = Cb component: Quantization table 1, Sampling factors 1 horiz/1 vert
JPEG - Component 3 = Cr component: Quantization table 1, Sampling factors 1 horiz/1 vert
Exif IFD0 - Orientation = Top, left side (Horizontal / normal)
Exif IFD0 - X Resolution = 2 dots per inch
Exif IFD0 - Y Resolution = 2 dots per inch
Exif IFD0 - Resolution Unit = Inch
Exif IFD0 - Orientation = Top, left side (Horizontal / normal)
Exif SubIFD - Color Space = sRGB
Exif SubIFD - Exif Image Width = 2968 pixels
Exif SubIFD - Exif Image Height = 2226 pixels
Photoshop - Caption Digest = 212 29 140 217 143 0 178 4 233 128 9 152 236 248 66 126
File - File Name = Rectified Scaled image.jpg
File - File Size = 609338 bytes
File - File Modified Date = Thu May 18 16:25:33 +05:30 2017
</pre>
<p>&nbsp;</p>
<p>From the console output, under Exif IFDO (Image File Director Zero) header. Resolution for X and Y are coded to 2 dots per inch.</p>
<p>But unfortunately AutoCAD doesn’t recognizes EXIF structure, but AutoCAD is designed to read resolution and scale the image accordingly, excerpt from the <a href="https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/ENU/AutoCAD-Core/files/GUID-075AED05-46E8-4ABF-812D-75D0E450FDF1-htm.html">knowledge article</a> </p>
<blockquote>
<p>If an image has resolution information, the program combines this information with the scale factor and the unit of measurement of the drawing to scale the image in your drawing. For example, if your raster image is a scanned blueprint on which the scale is 1 inch equals 50 feet, or 1:600, and your drawing is set up so that 1 unit represents 1 inch, then in the Image dialog box under Scale, select Specify On-Screen. To scale the image, you clear Specify On-Screen, and then enter <strong>600</strong> in Scale. The image is then attached at a scale that brings the geometry in the image into alignment with the geometry in the drawing.</p></blockquote>
<p><font style="background-color: #ffff00">Image resolution can be encoded as under <a href="https://en.wikipedia.org/wiki/JPEG_File_Interchange_Format">JFIF</a> structure or EXIF structure, currently AutoCAD can recognizes if the image resolution is encoded under JFIF structure.</font></p>
<p><font style="background-color: #ffffff">As per Wikipedia, </font></p>
<blockquote>
<p>JFIF provides resolution or aspect ratio information using an application segment extension to JPEG. It uses Application Segment #0, with a segment header consisting of the <a href="https://en.wikipedia.org/wiki/Null-terminated_string">null-terminated string</a> spelling "JFIF" in <a href="https://en.wikipedia.org/wiki/ASCII">ASCII</a> followed by a byte equal to 0, and specifies that this must be the first segment in the file, hence making it simple to recognize a JFIF file. <a href="https://en.wikipedia.org/wiki/Exif">Exif</a> images recorded by digital cameras generally do not include this segment, but typically comply in all other respects with the JFIF standard.</p></blockquote>
<p>So we need to encode resolution information under JFIF structure.</p>
<p><strong>Encoding resolution under JFIF ?</strong></p>
<p>Fortunately, Bitmap .NET API sets resolution in JFIF data structure, so we need not rely on any other library nor we need to do byte coding.</p><pre style="background: #ffffff; color: #000000"><span style="font-weight: bold; color: #800000">string</span> imagePath <span style="color: #808030">=</span> <span style="color: #800000">"</span><span style="color: #0000e6">D:\\Temp\\DPI_Resolution\\Rectified Scaled image.jpg</span><span style="color: #800000">"</span><span style="color: #800080">;</span>
Bitmap bitmap <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> Bitmap<span style="color: #808030">(</span>imagePath<span style="color: #808030">)</span><span style="color: #800080">;</span>
bitmap<span style="color: #808030">.</span>SetResolution<span style="color: #808030">(</span>2F<span style="color: #808030">,</span> 2F<span style="color: #808030">)</span><span style="color: #800080">;</span><span style="color: #696969">//2F resolution we would like to set in DPI, 2 Dots per Inch</span>
bitmap<span style="color: #808030">.</span>Save<span style="color: #808030">(</span><span style="color: #800000">"</span><span style="color: #0000e6">D:\\Temp\\DPI_Resolution\\Rectified.jpg</span><span style="color: #800000">"</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
</pre>
<p>&nbsp;</p>
<p>After setting image resolution JFIF, and attaching image in AutoCAD,will display correct resolution. </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8fc3012970b-pi"><img title="ImageWithCorrectJfif" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="ImageWithCorrectJfif" src="/assets/image_918708.jpg" width="343" height="318"></a></p>
<p>&nbsp;</p>
<p>The images in the screenshot are taken from a customer, this is captured using <a href="http://ikegps.com/spike/">Spike</a> device.</p>
<p>Inserting image in AutoCAD in real world size is essential for Designer and can leverage AutoCAD tools efficiently if working on existing structure that has been scanned using any device.</p>
