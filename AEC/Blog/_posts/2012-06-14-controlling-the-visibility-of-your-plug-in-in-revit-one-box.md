---
layout: "post"
title: "Controlling the visibility of your plug-in in Revit One Box."
date: "2012-06-14 21:46:05"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/06/controlling-the-visibility-of-your-plug-in-in-revit-one-box.html "
typepad_basename: "controlling-the-visibility-of-your-plug-in-in-revit-one-box"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p> <p>Revit One Box 2013 is composed of Revit Architecture, Structure, and MEP as single product. It is included in Building Design Suites Ultimate and Premium. Users can configure the user interface for these domains during the installation or from Options dialog box. </p>  <p>You might need to control the visibility of your external command and/or application based on this user interface setting. For example, you would want to make your plug-in for Revit Architecture visible if 'Architecture' user interface is selected. </p>  <p>You can enable/disable your external command by the interface setting using VisibilityMode attributes in Add-in manifest file. Please see how in Jeremy’s API blog <a href="http://thebuildingcoder.typepad.com/blog/2010/05/addin-visibility-mode.html">http://thebuildingcoder.typepad.com/blog/2010/05/addin-visibility-mode.html</a>. However, VisibilityMode attributes are currently not&#160; available for external applications.</p>  <p>Using Revit API, you can query current user interface settings in runtime. UIAPI sample in Revit SDK adds own tab to Options dialog box and shows the settings.&#160;&#160; </p>  <p>Your external application might be able to display/hide your ribbon tab by setting ribbon’s Visible property by monitoring user interface settings in an event handler for instance for a view event such as UIControlledApplication.ViewActiving、ViewActived、DocumentCreating、and DocumentOpening.</p>
