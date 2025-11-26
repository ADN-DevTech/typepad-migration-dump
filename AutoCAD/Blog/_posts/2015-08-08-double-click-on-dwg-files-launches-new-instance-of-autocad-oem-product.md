---
layout: "post"
title: "Double click on .dwg files launches new instance of AutoCAD OEM product"
date: "2015-08-08 03:14:32"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/08/double-click-on-dwg-files-launches-new-instance-of-autocad-oem-product.html "
typepad_basename: "double-click-on-dwg-files-launches-new-instance-of-autocad-oem-product"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When a drawing file is opened via the explorer by double clicking on it, the application associated with that file type is launched automatically. If you notice that separate instances of the AutoCAD or AutoCAD OEM product gets launched on double clicking two separate drawings, here are two likely reasons and ways to set it right.</p>
<p>1. In the explorer, right click on a .dwg file and choose "Properties". In the "General" tab, the application that the .dwg file will open is displayed as shown in the below screenshot. If an application such as your AutoCAD OEM product or a specific version of AutoCAD is chosen, then a new instance will be launched on double click. Instead, try choosing the "AutoCAD DWG Launcher" as the application by clicking on the "Change" button.</p>
<a class="asset-img-link"   href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1457f8d970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1457f8d970c img-responsive" alt="1" title="1" src="/assets/image_175376.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p>2. In some systems, it is also possible that despite choosing the "AutoCAD DWG Launcher", separate instances get launched on double clicking two different drawings. This is likely because of the improper association of the .dwg file type and the application. To set it right, you may need to tweak the registry values but please ensure that you have a backup of the registry keys that you modify in this process.</p>
<p>- Delete the "HKEY_CLASSES_ROOT\.dwg". This will ensure that there are no applications associated with the .dwg file type.</p>
<p>- Under "HKEY_CURRENT_USER\Software\Autodesk\DwgCommon\shellex\apps", delete the AutoCAD OEM product entry in case it is already listed. The entries under apps are the ones that launcher application will use to open a .dwg file. The default application that the launcher will use is listed under HKEY_CURRENT_USER\Software\Autodesk\DwgCommon\shellex\apps -&gt; Default key.</p>
<p>For example, here is the value that was removed for an OEM product</p>
<a class="asset-img-link"   href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7bbcc4c970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7bbcc4c970b img-responsive" alt="2" title="2" src="/assets/image_907988.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p>- Launch the AutoCAD / AutoCAD OEM product and at startup, the DWG association dialog should get displayed. Choose the recommended option which is "Always reassociate DWG files with &lt;Product name&gt;".</p>
<p>- Close AutoCAD / AutoCAD OEM product. The double clicking on two separate .dwg files should not get opened in the same instance of the application. You can also verify that DWG file association is right under&nbsp;HKEY_CURRENT_USER\Software\Autodesk\DwgCommon\shellex\apps -&gt; Default key.</p>
