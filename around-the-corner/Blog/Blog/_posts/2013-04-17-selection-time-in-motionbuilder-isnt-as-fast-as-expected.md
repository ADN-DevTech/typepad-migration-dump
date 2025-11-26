---
layout: "post"
title: "Selection time in MotionBuilder isn't as fast as expected"
date: "2013-04-17 05:34:10"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "MotionBuilder"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/04/selection-time-in-motionbuilder-isnt-as-fast-as-expected.html "
typepad_basename: "selection-time-in-motionbuilder-isnt-as-fast-as-expected"
typepad_status: "Publish"
---

<p>If you are using MotionBuilder 2012 and above, you should use the functions FBBeginChangeAllModels() and FBEndChangeAllModels() to accelerate the selection time otherwise the selection might not be smooth like in the UI.<br /><br />Here is an example:</p>
<pre class="brush: python; toolbar: false;">import time
from pyfbsdk import *

FBBeginChangeAllModels()
s_Time = time.clock()
all_Grp = FBSystem().Scene.Groups
for grp in all_Grp :
    for item in grp.Items :
        item.Selected = True
e_Time = time.clock()
FBEndChangeAllModels()
print &quot;Select&quot;,&quot;ProccesTime = %s&quot;%(e_Time - s_Time),&quot;OK&quot;
</pre>
<p>If now you try the same code without these 2 function calls, you see the difference immediately.</p>
