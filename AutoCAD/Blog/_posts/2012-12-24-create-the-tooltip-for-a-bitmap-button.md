---
layout: "post"
title: "create the tooltip for a bitmap button"
date: "2012-12-24 01:58:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/create-the-tooltip-for-a-bitmap-button.html "
typepad_basename: "create-the-tooltip-for-a-bitmap-button"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue</strong></p>
<p>I have created a CAcUiBitmapButton. How do I create the tooltip text that   <br />displays when the cursor is placed over the button?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>CAcUiBitmapButton has implemented everything necessary for doing this; you only need to provide the tooltip string within the caption, such as <em><strong>SAMP|Sample Button1.</strong></em></p>
<p>Notice the SAMP before the vertical bar is the bitmap&#39;s resource ID name. This is when you design the dialog button and set the properties. Also, make sure you choose owner draw style for the button. </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c34c4f74d970b-pi"><img alt="image" border="0" height="335" src="/assets/image_494212.jpg" style="display: inline; border-width: 0px;" title="image" width="485" /></a></p>
<p>In addition, you can create multiple lines of tooltip by overriding OnGetTipText() and modifying the reference parameter to the text string you want.<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6686c5a970d-pi"><img alt="image" border="0" height="332" src="/assets/image_635771.jpg" style="display: inline; border: 0px;" title="image" width="475" /></a></p>
<p>To test these approaches, the attached sample application has two separate buttons; one uses CAcUiBitmapButton and the other is derived from CAdUiOwnerDrawButton. In addition, it shows how to draw a text button that changes it appearance when it is clicked. Note that this approach also applies to all CAxUiWhatever control classes, such as CAcUiColorComboBox, CAcUiAngleComboBox, CAcUiAngleEdit, and so on.</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3ef3dcad970c"><a href="http://adndevblog.typepad.com/files/create_tooltip_bitmap_button_vs2008.zip">Download Create_tooltip_bitmap_button_VS2008</a></span></p>
<p>&#0160;</p>
<p>&#0160;<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c34c4f74d970b-pi"></a></p>
