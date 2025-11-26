---
layout: "post"
title: "DevCon, API Docs and Fabrication Part Service"
date: "2025-03-14 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "DevCon"
  - "Docs"
  - "Python"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2025/03/devcon-api-docs-and-fabrication-part-service.html "
typepad_basename: "devcon-api-docs-and-fabrication-part-service"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>My last Autodesk conference nears, new and updated API documentation online, and a tutorial-style exploration of implementing fabrication part &ndash; show service:</p>

<ul>
<li><a href="#2">DevCon in Amsterdam</a></li>
<li><a href="#3">CivApiDocs Civil3D API documentation</a></li>
<li><a href="#4">ApiDocs Revit 2025 API docs</a></li>
<li><a href="#5">Fabrication part &ndash; show service</a></li>
</ul>

<h4><a name="2"></a> DevCon in Amsterdam</h4>

<p>I will be attending <a href="https://aps.autodesk.com/blog/register-today-autodesk-devcon-returns-2025-amsterdam">DevCon in Amsterdam</a>!
It is taking place on May 20-21 2025 in
the famous old <a href="https://en.wikipedia.org/wiki/Beurs_van_Berlage">Beurs van Berlage</a> stock market
constructed 1896-1903.
I mentioned the <a href="https://thebuildingcoder.typepad.com/blog/2024/11/devcon-ai-for-revit-api-modeless-add-ins-leave.html#2">call for papers</a> in November.</p>

<p>May will also be the last month of my life as an Autodesk employee.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3ce67d3200c-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3ce67d3200c img-responsive" alt="DevCon in Beurs van Berlage" title="DevCon in Beurs van Berlage"  src="/assets/image_4fb2d3.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> CivApiDocs Civil3D API Documentation</h4>

<p>Jaime Alonso Candau of the Revit API consulting company <a href="https://nonica.io/">Nonica.io</a> published</p>

<p><center>
  <a href="https://civapidocs.com/">Civ API Docs</a>
</center></p>

<p>The new website provides online Civil3D API documentation for the releases 2022, 2023, 2024 and 2025, in the same style as
the <a href="https://revapidocs.com/"><code>RevApiDocs</code> Revit API documentation</a> supporting
Revit 2025 as well as preceding versions that
I <a href="https://thebuildingcoder.typepad.com/blog/2025/02/unit-testing-and-more-serious-matters.html#4">mentioned in February</a>.</p>

<p>Many thanks to Jaime for creating and sharing these resource.</p>

<h4><a name="4"></a> ApiDocs Revit 2025 API Docs</h4>

<p>Following up on that,
<a href="https://twitter.com/gtalarico">Gui Talarico</a> now also
updated the <a href="https://apidocs.co">apidocs</a> web site for both Revit 2025 and Revit 2025.3 API:</p>

<ul>
<li><a href="https://apidocs.co/apps/revit/2025.3/1593f994-fb7b-4b7d-ae1d-1c0ba3337577.htm">Revit 2025.3 API</a></li>
<li><a href="https://apidocs.co/apps/revit/2025/1593f994-fb7b-4b7d-ae1d-1c0ba3337577.htm#">Revit 2025 API</a></li>
</ul>

<p><a href="https://apidocs.co">Apidocs</a> also includes API documentation for Grasshopper, Navisworks, Rhino and previous Revit releases all the way back to Revit 2015.</p>

<p>Thank you, Gui, for maintaining this invaluable resource!</p>

<h4><a name="5"></a> Fabrication Part &ndash; Show Service</h4>

<p><a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11800801">Steven Williams</a> shared
a very nice tutorial-style explanation of how to replicate
<a href="https://forums.autodesk.com/t5/revit-api-forum/fabrication-part-show-service/td-p/13354945">Fabrication Part &ndash; Show Service</a>:</p>

<p>This post started as a question, but I figured it out and thought someone might be interested. I couldn't find any examples of this in the API samples or through web searches, and ChatGPT failed miserably.</p>

<p>My goal is to replicate the Show Service button that appears in the Modify tab when you select a Fabrication Part.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3ce67b0200c-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3ce67b0200c img-responsive" style="width: 49px; display: block; margin-left: auto; margin-right: auto;" alt="Fabrication Part - Show Service" title="Fabrication Part - Show Service"  src="/assets/image_e498d9.jpg" /></a><br /></p>

<p></center></p>

<p>Clicking this button opens the MEP Fabrication Parts panel to the matching service and palette that generated that part, and highlights the button that was used.</p>

<p>I snooped through the attributes and parameters of several fabrication parts but could only find ServiceId indicating what button was used to draw the part. The fabrication button name does not necessarily correspond to the name of the part drawn in Revit.</p>

<p>Example: I have a button named "Pipe":</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860e513c6200b-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860e513c6200b img-responsive" style="width: 93px; display: block; margin-left: auto; margin-right: auto;" alt="Pipe" title="Pipe"  src="/assets/image_a3bb94.jpg" /></a><br />
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860e513b7200b-pi"></a><br /></p>

<p></center></p>

<p>These are the relevant properties of the button object, which I obtained like this:</p>

<pre><code class="language-cs"># doc is the Autodesk.Revit.DB.Document in question
# service_id, palette_index, and button_index are given integers
fabrication_configuration = FabricationConfiguration.GetFabricationConfiguration(doc)
service = fabrication_configuration.GetService(service_id)
button = service.GetButton(palette_index, button_index)
print(button.ButtonIndex) # == 0
print(button.Code) # == P01
print(button.ConditionCount) # == 1
print(button.IsAHanger) # == False
print(button.IsStraight) # == True
print(button.Name) # == Pipe
print(button.PaletteIndex) # == 0
print(button.ServiceId) # == 3381</code></pre>

<p>Not much information about what will be drawn in Revit.
There is a method FabricationServiceButton.ContainsFabricationPartType(partType) that I finally found to be the key.</p>

<p>The part drawn in Revit looks like this ("Cerro Type L Hard Copper Pipe"):</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860e513a6200b-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860e513a6200b img-responsive" style="width: 371px; display: block; margin-left: auto; margin-right: auto;" alt="Copper pipe" title="Copper pipe"  src="/assets/image_93d2cb.jpg" /></a><br /></p>

<p></center></p>

<p>It has attributes and properties like this:</p>

<pre><code class="language-cs"># select your part
ref = uidoc.Selection.PickObject(ObjectType.Element)
element = doc.GetElement(ref)
# properties
print(element.ProductName) # == Pipe
print(element.ProductLongDescription) # == Type L Hard Copper Pipe
print(element.ProductOriginalEquipmentManufacture) # == Cerro
print(element.ProductSpecificationDescription) # == Type L
print(element.Name) # == Default
# parameters
print(element.LookupParameter('Product Name').AsValueString()) # == Pipe, also BuiltInParameter.FABRICATION_PRODUCT_DATA_PRODUCT
print(element.LookupParameter('Product Long Description').AsValueString()) # == Type L Hard Copper Pipe, also BuiltInParameter.FABRICATION_PRODUCT_DATA_LONG_DESCRIPTION
print(element.LookupParameter('OEM').AsValueString()) # == Cerro, also BuiltInParameter.FABRICATION_PRODUCT_DATA_OEM
print(element.LookupParameter('Product Specification Description').AsValueString()) # == Type L, also BuiltInParameter.FABRICATION_PRODUCT_DATA_SPECIFICATION</code></pre>

<p>Then I discovered the <code>FabricationPartType</code> class that can be retrieved using the fabrication part <code>GetTypeId</code> method:</p>

<pre><code class="language-cs">doc.GetElement(element.GetTypeId())</code></pre>

<p>The API documentation says "For the product-based MAP parts, every size is a new part type in Revit.
For others, one part type can have many sizes."</p>

<p>Now I can put FabricationServiceButton.ContainsFabricationPartType(partType) together with element.GetTypeId().
There is one last step, which took me a little longer.
Sometimes we have several ITM files in a single button:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3ce67f1200c-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3ce67f1200c img-responsive" style="width: 325px; display: block; margin-left: auto; margin-right: auto;" alt="Adapters" title="Adapters"  src="/assets/image_74b0ba.jpg" /></a><br /></p>

<p></center></p>

<p>Revit classifies each of these as a Condition. ContainsFabricationPartType() only tells you that the button contains the given partType; now you have to get the condition that matches the partType. For that you have to go back to the FabricationPartType class and use the Lookup(document, button, condition) method with each valid condition the button contains.</p>

<p>This is the summary.</p>

<pre><code class="language-cs"># you can run this in RevitPythonShell
fabrication_configuration = FabricationConfiguration.GetFabricationConfiguration(doc)

# select a FabricationPart
# -- consider creating an ISelectionFilter that only passes elements of type FabricationPart
ref = uidoc.Selection.PickObject(ObjectType.Element)
element = doc.GetElement(ref)
part_type = doc.GetElement(element.GetTypeId())

service_id = element.ServiceId
service = fabrication_configuration.GetService(service_id)

matching_button = None
matching_condition = None
for palette_index in range(service.PaletteCount):
    for button_index in range(service.GetButtonCount(palette_index)):
        button = service.GetButton(palette_index, button_index)
        for condition in range(button.ConditionCount):
            part_type_element = FabricationPartType.Lookup(doc, button, condition)
            # the GUI always picks the first button and condition that matches
            # -- the efficiency of this would be improved by making this into
            # a a function that yields the first match
            if part_type_element != ElementId.InvalidElementId and matching_button is None:
                matching_button = button
                matching_condition = condition</code></pre>

<p>I think you can now use this matching button, for example, to replicate the 'Create Similar' command for fabrication parts, using the second overload with matching_button and matching_condition from above.</p>

<pre><code class="language-cs">public static FabricationPart Create(
  Document document,
  FabricationServiceButton button,
  int condition,
  ElementId levelId
)</code></pre>

<p>Many thanks to Steven for his nice clear explanation and overview.</p>
