---
layout: "post"
title: "Fusion API: Attribute returns none for the Face that has been attached attribute"
date: "2016-12-02 06:16:12"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/12/fusion-api-attribute-returns-none-for-the-face-that-has-been-attached-attribute.html "
typepad_basename: "fusion-api-attribute-returns-none-for-the-face-that-has-been-attached-attribute"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>:     <br />I attached the attributes to some selected faces of an assembly. When I iterate the faces of each component and try to find out those faces, they returned none attributes. My code is as below. It assumes a face has been selected in the assembly.<code>     <br />def main():      <br />&#160;&#160;&#160; ui = None      <br />&#160;&#160;&#160; try:      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; app = adsk.core.Application.get()      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; ui = app.userInterface      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; face = adsk.fusion.BRepFace.cast(ui.activeSelections.item(0).entity)      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; face.attributes.add('MyGroup', 'MyName', 'MyValue')       <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; design = adsk.fusion.Design.cast(app.activeProduct)      <br />&#160;&#160;&#160;&#160;&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; for occ in design.rootComponent.occurrences:      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; comp = occ.component      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; body = comp.bRepBodies.item(0)      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; for face in body.faces:      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; attr = face.attributes.itemByName('MyGroup', 'MyName')      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; #always none      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; if attr != None:       <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ui.messageBox('Attribute is {}'.format(attr))      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; else:      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; print('Attribute is None')      <br />      <br />&#160;&#160;&#160; except:      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; if ui:      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))      <br /></code></p>  <p><strong>Solution</strong>:     <br />The geometries we see in the assembly are actually the references from the native components. In Fusion 360, they are called Proxy objects. So, when you select a face in the context of an assembly, the face is actually a proxy face. That means, the attributes was added to the proxy face. While when you iterate the faces of each component, that will be the native face which does not have such attributed added for proxy face. So if you want to find back those information, you need to get the proxy face from the native one. e.g.</p>  <pre><code>
def main():
    ui = None
    try:
        app = adsk.core.Application.get()
        ui = app.userInterface
        face = adsk.fusion.BRepFace.cast(ui.activeSelections.item(0).entity)
        face.attributes.add('MyGroup', 'MyName', 'MyValue') 
        design = adsk.fusion.Design.cast(app.activeProduct)
       
        for occ in design.rootComponent.occurrences:
            comp = occ.component
            body = comp.bRepBodies.item(0)
            for face in body.faces:
                #get proxy face in the assembly context, corresponding to the
                # native face in this occurence
                faceproxy= face.createForAssemblyContext(occ)
                
                #get attribute from the proxy face
                attr = faceproxy.attributes.itemByName('MyGroup', 'MyName')
                #attr = face.attributes.itemByName('MyGroup', 'MyName')
                
                if attr != None: 
                    ui.messageBox('Attribute is {}'.format(attr))
                else:
                    print('Attribute is None')

    except:
        if ui:
            ui.messageBox('Failed:\n{}'.format(traceback.format_exc()))
</code></pre>
