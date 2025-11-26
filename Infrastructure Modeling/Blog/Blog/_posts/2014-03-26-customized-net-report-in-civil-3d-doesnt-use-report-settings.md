---
layout: "post"
title: "Customized .NET Report in Civil 3D doesn't use 'Report Settings'"
date: "2014-03-26 02:13:04"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/03/customized-net-report-in-civil-3d-doesnt-use-report-settings.html "
typepad_basename: "customized-net-report-in-civil-3d-doesnt-use-report-settings"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you have created a custom Civil 3D .NET report and added that in Toolbox and then set the units, precision value as shown in the picture below, but to your surprise when you generate the report you find the unit or precision value is not set as per the setting you opted in the &#39;Report Settings&#39; dialogbox.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdf1a80970b-pi" style="display: inline;"><img alt="Report_Settings_Civil3D" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcdf1a80970b img-responsive" src="/assets/image_e8d79d.jpg" title="Report_Settings_Civil3D" /></a><br />&#0160;</p>
<p>&#0160;&#0160;</p>
<p>Reason for this is :&#0160;</p>
<p>.NET reports use a combination of the Report Settings (for Client and Owner settings) and <strong>they use the drawing Ambient Settings</strong> for units, precision, and so on.&#0160;</p>
<p>LandXML reports use the Report Settings for all settings, such as report header information, units, precision, and so on.&#0160;</p>
<p>[Note -&#0160; You can tell if a report is a LandXML-based report if the Export To XML Report dialog box is displayed when you run the report.]</p>
<p>&#0160;</p>
<p>To set the Precision, units for your custom .NET report, use the Drawing Ambient Settings as shown in the picture below -</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdf1a85970b-pi" style="display: inline;"><img alt="Drawing_Ambient_Settings" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcdf1a85970b img-responsive" src="/assets/image_68e12a.jpg" title="Drawing_Ambient_Settings" /></a><br />&#0160;</p>
<p>Hope this is useful to you !</p>
