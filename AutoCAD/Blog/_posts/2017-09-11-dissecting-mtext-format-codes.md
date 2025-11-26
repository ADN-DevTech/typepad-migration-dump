---
layout: "post"
title: "Dissecting MTEXT Format Codes"
date: "2017-09-11 22:23:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/09/dissecting-mtext-format-codes.html "
typepad_basename: "dissecting-mtext-format-codes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>I have received a query how can I identify a special symbol from MTEXT content format string.To understand this better, we will look at a simple example, below is a screen shot of Cylinder whose dimensions are expressed in </p><p>∅68<span class="supsub" style="position: absolute;">
 
<sup style="left: 2px; top: -5px; display: block; position: relative;">+0.8</sup>

 <sub style="left: 2px; top: 15px; display: block; position: relative;">+0.1</sub>

</span></p><p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a9a90d970c-pi"><img width="554" height="297" title="CADDiameter" style="display: inline; background-image: none;" alt="CADDiameter" src="/assets/image_711406.jpg" border="0"></a></p><p>The format codes would be </p>
<pre>\A1;\fAIGDT|b0|i0;\H2.5000;\ln\fArial|b0|i0;\H2.5000;68{\H1.3;\S+0,8^+0,1;}</pre>
<p>
<h4>Understanding each format code:</h4><ul><li>\f = Font file name, in this example it is AIGDT</li><ul><li>AIGDT stands for Autodesk Inventor Geomertic Dimension and Tolerance font file.</li></ul><li>codes starting with pipe are generally displays the traits of font.</li><ul><li>b tells ‘bold’ where 0 is off,and 1 is on.</li><li>i tells ‘italic’ where 0 is off and 1 is on.</li><li>c tells ‘code page’ followed by code page number for example |c238</li><li>p&nbsp; tells ‘pitch; followed by number for example |p10</li></ul><li>\L&nbsp;&nbsp;&nbsp; Start underline</li><li>\l&nbsp;&nbsp;&nbsp; Stop underline</li><li>\O&nbsp;&nbsp;&nbsp; Start overstrike</li><li>\o&nbsp;&nbsp;&nbsp; Stop overstrike</li><li>\K&nbsp;&nbsp;&nbsp; Start strike-through</li><li>\P&nbsp;&nbsp;&nbsp; New paragraph (new line)</li><li>\pxi&nbsp;&nbsp;&nbsp; Control codes for bullets, numbered paragraphs and columns</li><li>\X&nbsp;&nbsp;&nbsp; Paragraph wrap on the dimension line (only in dimensions)</li><li>\Q&nbsp;&nbsp;&nbsp; Slanting (obliquing) text by angle - e.g. \Q30;</li><li>\H&nbsp;&nbsp;&nbsp; Text height - e.g. \H3x or \H2.500</li><li>\W&nbsp;&nbsp;&nbsp; Text width - e.g. \W0.8x</li><li>\S&nbsp;&nbsp;&nbsp;&nbsp; Stacking Fractions</li><ul><li>e.g. <tt>\SA^B</tt>:
<p>A<p>B</p><li>e.g. <tt>\SX/Y</tt>:
<p><u>X</u><p>Y</p><li>e.g. <tt>\S1#4</tt>:<br>¼</li></ul><li>\A&nbsp;&nbsp; Alignment</li><ul><ul><!--StartFragment-->
</ul><li><tt>\A0;</tt> = bottom <li><tt>\A1;</tt> = center <li><tt>\A2;</tt> = top</li></ul><li>\C&nbsp; Color change</li><ul><ul><!--StartFragment-->
</ul><li><tt>\C1;</tt> = red <li><tt>\C2;</tt> = yellow <li><tt>\C3;</tt> = green <li><tt>\C4;</tt> = cyan <li><tt>\C5;</tt> = blue <li><tt>\C6;</tt> = magenta <li><tt>\C7;</tt> = white</li><ul><!--EndFragment--></ul><ul><!--EndFragment--></ul></ul></ul><p><br></p><ul><li>\T&nbsp; Tracking, char.spacing - e.g. <tt>\T2;</tt><li><tt></tt>\~&nbsp; Non-wrapping space, hard space</li><p>{}<br>Braces - define the text area influenced by the code</p><li>\&nbsp; Escape character - e.g. <tt>\\</tt> = "\", <tt>\{</tt> = "{"<br><br><br></li></ul>
<pre>\fAIGDT|b0|i0;\H2.5000;\ln</pre>
<p> '\l' is format code for lower underline, and from Font family AIGDT 'n' is special symbol for ∅.</p><p>Below is table of character mapping between control code and special symbol.</p><p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09c28566970d-pi"><img width="409" height="911" title="AIGDT" style="display: inline; background-image: none;" alt="AIGDT" src="/assets/image_986283.jpg" border="0"></a></p>
<p>For example following format code <pre>\A1;{\fAIGDT|b0|i0;m}\H2.5000;80</pre><p> would render this</p><p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09c2856e970d-pi"><img width="244" height="128" title="M" style="display: inline; background-image: none;" alt="M" src="/assets/image_440393.jpg" border="0"></a></p><p><br></p><p>For character map utility, this document contains how to retrieve tool for various versions of windows.</p><p><a title="http://sites.psu.edu/symbolcodes/windows/charmap/" href="http://sites.psu.edu/symbolcodes/windows/charmap/">http://sites.psu.edu/symbolcodes/windows/charmap/</a></p><p>MTEXT also supports straight away Unicode tool, and not all font files will support all symbols, every font files has its reason for existence but there will unique Unicode which prevents from clashing between symbols.</p><p>Using different font file like ISOCPEUR, I can render diameter symbol or ‘latin captial letter with diagonal stoke’, or the symbol which I have begin this post ∅, unicode is ‘\U+00D8’</p><pre>{\fISOCPEUR|b0|i0|c0|p34;\H0.95833x;\C256;\U+00D8\A1;\H1.04348x;\C7;80}</pre>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c91f54e9970b-pi"><img width="244" height="137" title="U 00D8" style="margin: 0px; display: inline; background-image: none;" alt="U 00D8" src="/assets/image_201563.jpg" border="0"></a><p>Hope this helps, if you find any interesting format coding for font file, do let me know, I will keep updating this.
