---
layout: "post"
title: "MotionBuilder Custom Property .Selected not exposed to Python"
date: "2013-06-18 22:47:00"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "MotionBuilder"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/06/motionbuilder-custom-property-selected-not-exposed-to-python.html "
typepad_basename: "motionbuilder-custom-property-selected-not-exposed-to-python"
typepad_status: "Publish"
---

<p>When using Python in MotionBuilder, you can select object in the scene using the Python .Selected property. When setting that property to True, you would put the object into the selection list. However, Custom Properties do not have their .Selected Python property exposed which makes in theory impossible to select them programmatically.</p>
<p>Hopefully there is a workaround :)</p>
<p>The workaround is to use the .SetFocus() method. When using that method with the parameter set to True, you will put the property object onto the selection.</p>
