---
layout: "post"
title: "Appearance properties"
date: "2016-05-24 10:41:51"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2016/05/appearance-properties.html "
typepad_basename: "appearance-properties"
typepad_status: "Publish"
---

<p>There is <a href="http://adndevblog.typepad.com/manufacturing/2016/04/fusion-api-change-appearance-of-body.html">an article</a> related to <strong>Appearance</strong>&#39;s and a sample script installed with <strong>Fusion 360</strong> that shows how to access <strong>Appearances</strong> from the libraries and assign them to objects&#0160;in the model: &quot;<strong>ApplyAppearanceToSelection.py</strong>&quot;</p>
<p>In some cases you may want to drill down into the properties of the&#0160;<strong>Appearance</strong>&#39;s which are used in the model, to find e.g. the texture that is being used by a given <strong>Appearance</strong>.<br />The following <strong>Python</strong> sample only shows how to access the <strong>name</strong> and <strong>value&#0160;</strong>of the properties, but there are more than that available. If you look at the <strong>Property</strong> object in the online help file, then in the &quot;<strong>Derived Classes</strong>&quot; section you&#39;ll see that quite a few other objects are derived from that. If you wanted to access all the properties then you&#39;d need to handle all of them and check all their properties:&#0160;<a href="http://help.autodesk.com/view/NINVFUS/ENU/?guid=GUID-db167e70-665f-4101-ba3c-3bcc88000fc7">http://help.autodesk.com/view/NINVFUS/ENU/?guid=GUID-db167e70-665f-4101-ba3c-3bcc88000fc7<br /></a></p>
<pre>import adsk.core, adsk.fusion, adsk.cam, traceback

def exportProperties(properties, indent, outputFile):
    for prop in properties:
        if type(prop) is adsk.core.AppearanceTextureProperty: 
            outputFile.writelines(indent + prop.name + &quot;\n&quot;)
            try:
                exportProperties(prop.value.properties, indent + &quot;  &quot;, outputFile)
            except:
                outputFile.writelines(indent + &quot;  Couldn&#39;t get sub properties\n&quot;)  
        elif type(prop) is adsk.core.ColorProperty:
            if prop.value:
                color = prop.value 
                outputFile.writelines(indent + 
                    &quot;red = &quot; + str(color.red) + 
                    &quot;; green = &quot; + str(color.green) + 
                    &quot;; blue = &quot; + str(color.blue) +&quot;\n&quot;)
        else:
            outputFile.writelines(indent + prop.name + &quot; = &quot; + str(prop.value) + &quot;\n&quot;)    

def run(context):
    ui = None
    try:
        app = adsk.core.Application.get()
        ui  = app.userInterface

        fileDialog = ui.createFileDialog()
        fileDialog.isMultiSelectEnabled = False
        fileDialog.title = &quot;Get the file to save to&quot;
        fileDialog.filter = &#39;Text files (*.txt)&#39;
        fileDialog.filterIndex = 0
        dialogResult = fileDialog.showSave()
             
        if dialogResult == adsk.core.DialogResults.DialogOK:
             fileName = fileDialog.filename
        else:
             return      
        
        design = app.activeProduct
        
        with open(fileName, &#39;w&#39;) as outputFile:
            for appearance in design.appearances:
                outputFile.writelines(&quot;&gt;&gt;&gt;&gt;&gt; &quot; + appearance.name + &quot; &lt;&lt;&lt;&lt;&lt;\n&quot;)
                exportProperties(appearance.appearanceProperties, &quot;  &quot;, outputFile)
            
    except:
        if ui:
            ui.messageBox(&#39;Failed:\n{}&#39;.format(traceback.format_exc()))
</pre>
<p>You will get something like this:</p>
<pre>&gt;&gt;&gt;&gt;&gt; Oak - Semigloss &lt;&lt;&lt;&lt;&lt;
  Material Type = 0
  URN = 
  Emission = False
  Reflectance = 0.06027025
  URN = 
  Emissive Luminance = 0.0
  red = 255; green = 255; blue = 255
  URN = 
  Depth = 0.5
  red = 255; green = 255; blue = 255
  URN = 
  Translucency = False
  red = 255; green = 255; blue = 255
  URN = 
  Anisotropy = 0.0
  URN = 
  Image
    Couldn&#39;t get sub properties
  URN = 
  NDF = surface_ndf_ggx
  Image
    Source = /Users/adamnagy/Library/Containers/com.autodesk.mas.fusion360/
Data/Library/Application Support/Autodesk/Common/Material Library/16021701/
slib/resource/1/Mats/wood_oak_bump.jpg
    URN = 
    Amount = 0.003
    Amount = 1.0
    Bump Type = bumpmap_height_map
    Sharing = common_shared
    red = 80; green = 80; blue = 80
    Tint = False
    Link texture transforms = False
    Map Channel = 1
    Map Channel = 1
    UVW Source = 0
    Offset Lock = False
    Offset = 0.0
    Offset Y = 0.0
    Sample Size = 18.0
    Size Y = 36.0
    Scale Lock = True
    U Offset = 0.0
    Horizontal = True
    U Scale = 1.0
    UV Scale = 1.0
    V Offset = 0.0
    Vertical = True
    V Scale = 1.0
    Rotation = 0.0
  URN = 
  Rotation = 0.0
  URN = 
  Roughness = 0.2
  URN = 
</pre>
<p>-Adam</p>
