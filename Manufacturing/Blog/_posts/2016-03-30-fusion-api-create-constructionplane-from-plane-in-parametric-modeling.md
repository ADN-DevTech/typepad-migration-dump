---
layout: "post"
title: "Fusion API: Create ConstructionPlane from Plane in Parametric Modeling"
date: "2016-03-30 04:47:33"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/fusion-api-create-constructionplane-from-plane-in-parametric-modeling.html "
typepad_basename: "fusion-api-create-constructionplane-from-plane-in-parametric-modeling"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The following code is a snippet of my add-in, Basically, it will create a ConstructionPlane based on a Plane. However, it failed at sketch = sketches.add(cons_plane).After debugging, I found constructionPlanes.add returns nothing, that is why sketches.add failed.</p>
<pre>import adsk.core, adsk.fusion, traceback
def run(context):
ui = None
try:
app = adsk.core.Application.get()
ui = app.userInterface
product = app.activeProduct
rootComp = product.rootComponent
pt_3d_1 = adsk.core.Point3D.create(0,0,0)
normal = adsk.core.Vector3D.create(0,0,1)
plane = adsk.core.Plane.create(pt_3d_1,normal)
print(plane.normal.x,plane.normal.y,plane.normal.z)
cons_planes = rootComp.constructionPlanes
print(cons_planes.count)
cons_planeInput = cons_planes.createInput()
cons_planeInput.setByPlane(plane)
cons_plane = cons_planes.add(cons_planeInput)
#watch cons_plane. it is none.
sketches = rootComp.sketches
#throw exception because cons_plane is none
sketch = sketches.add(cons_plane)
#can work if using default plane
#xyPlane = rootComp.xYConstructionPlane
#sketch = sketches.add(xyPlane)
except:
if ui:
ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))
</pre>
<p>&#0160;</p>
<p>When we create a construction plane in [non-Parametric Modeling], it has no relationship to anything else and is positioned in space.&#0160; We can use the Move command to reposition it anywhere in the model.&#0160; When working in [Parametric Modeling], the construction plane remembers the input geometry and is tied to it.&#0160; If that geometry changes, the construction plane will be recomputed.&#0160; It’s not possible to create a construction plane that has not relationship to anything.&#0160; The exception to this is because I create a construction plane and then delete whatever it’s dependent on.&#0160; But then it just becomes sick and the only option is to redefine it which means I need to re-associate it to some other geometry.&#0160;Finally, I got know the root reason. It is an as design behavior in [Parametric Modeling]. Fusion 360 provides two types of modeling: [Parametric Modeling] and [non-Parametric Modeling]. The former is also called modeling with history, while the latter is called direct modeling.</p>
<p>Construction planes are real entities, while Plane object is transient, which just provides the mathematical definition of a plane.</p>
<p>So in [non-Parametric Modeling], the code will work well.</p>
<p>In default, the modeling mode follows the setting in Preference</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08d06eb3970d-pi"><img alt="image" border="0" height="74" src="/assets/image_a03299.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="244" /></a></p>
<p>If you want to switch the modeling mode in the middle way, you can right click the root node and click the last menu item.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08d06ebc970d-pi"><img alt="image" border="0" height="227" src="/assets/image_f79b75.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="244" /></a></p>
