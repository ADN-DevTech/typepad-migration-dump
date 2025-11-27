---
layout: "post"
title: "Understanding the properties of textual linetype segments in AutoCAD"
date: "2008-01-11 17:10:53"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
  - "Object properties"
original_url: "https://www.keanw.com/2008/01/understanding-t.html "
typepad_basename: "understanding-t"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/creating-a-comp.html">the last post</a> we looked at using .NET to define complex linetypes containing text segments. In the post I admitted to not knowing specifics about the properties used to create the text segment in the linetype, and, in the meantime, an old friend took pity on me and came to the rescue. :-)</p>

<p>Mike Kehoe, who I've known for many years since we worked together in the Guildford office of Autodesk UK, sent me some information that I've reproduced below. Mike now works for Micro Concepts Ltd., an Autodesk reseller, developer and training centre. He originally wrote the below description in the R12/12 timeframe, but apparently most of it remains valid; and while it refers to the text string used to define a linetype in a .lin file, these are also mostly properties that are exposed via the .NET interface.</p><blockquote dir="ltr"><p>Example: Using Text within a Linetype.<br />A,.5,-.2,[&quot;MK&quot;,STANDARD,S=.2,R=0.0,X=-0.1,Y=-.1],-.2</p>

<p>The key elements for defining the TEXT are as follows:</p>

<p><strong>&quot;MK&quot;</strong> - These are the letters that will be printed along the line.</p>

<p><strong>STANDARD -</strong>This tells AutoCAD what text style to apply to the text.&nbsp; NB: This is optional. When no style is defined AutoCAD will use the current text style – <strong>TextStyle</strong> holds the setting for the current text style.</p><blockquote dir="ltr"><p>[<em><strong>Note from Kean:</strong> I found the text style to be mandatory when using the .NET interface.</em>]</p></blockquote><p><strong>S=.2</strong> - This is the text scaling factor. However, there are 2 options: (1) when the text style's height is 0, then <strong>S</strong> defines the height; in this case, 0.2 units; or (2) when the text style's height parameter is non-zero, the height is found by multiplying the text style's height by this number; in this case, the linetype would place the text at 20% of the height defined in the text style.</p>

<p><strong>R=0.0</strong> - This rotates the text relative to the direction of the line; e.g.: 0.0 means there is no rotation. NB: This is optional. When no rotation is defined AutoCAD will assume zero degrees. The default measurement is degrees; NB: you can use r to specify radians, g for grads, or d for degrees, such as <strong>R</strong>=150g.</p><blockquote dir="ltr"><p>[<em><strong>Note from Kean:</strong> just like ObjectARX, the .NET interface accepts radians for this value, in SetShapeRotationAt(). A quick reminder: 360 degrees = 2 x PI radians. So you can pass 90 degrees using &quot;System.Math.PI / 2&quot;.</em>]</p></blockquote><p><strong>A=0.0</strong>&nbsp; - This rotates the text relative to the x-axis (&quot;A&quot; is short for Absolute); this ensures the text is always oriented in the same direction, no matter the direction of the line. The rotation is always performed within the text baseline and capital height. That's so that you don't get text rotated way off near the orbit of Pluto.</p><blockquote dir="ltr"><p>[<em><strong>Note from Kean</strong>: to use this style of rotation using .NET, you need to use SetShapeIsUcsOrientedAt() to make sure the rotation is calculated relative to the current UCS rather than the direction of the line.</em>]</p></blockquote><p><strong>X=-0.1</strong> - This setting moves the text just in the x-direction from the linetype definition vertex.</p>

<p><strong>Y=-0.1</strong> – This setting moves the text in the y-direction from the linetype definition vertex.<br />These 2 settings can be used to center the text in the line. The units are defined from the linetype scale factor, which is stored in system variable <strong>LtScale</strong>.</p></blockquote><p>Thanks for the information, Mike!</p>
