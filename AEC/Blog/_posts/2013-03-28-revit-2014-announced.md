---
layout: "post"
title: "Revit 2014 Announced"
date: "2013-03-28 10:20:32"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2013/03/revit-2014-announced.html "
typepad_basename: "revit-2014-announced"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>Revit 2014 was announced yesterday. The main product features can be access from <a href="http://www.autodesk.com/products/autodesk-revit-family/features">this page</a>&#160; </p>  <p>   <br />Here I share part of the&#160; new API features that was highly expected. </p>  <p>1. When the current view is the project browser, external commands can be started by users. Previously external command Ribbon buttons are gray out in this situation. And more than that, your external command can also detect which item is selected in the project browser.And your external command do specific task depending on which item is selected.</p>  <p>2. Developers can fully control the elements display in a specific view just like the users can control those elements display through the UI commands in Visibility/Graphics Overrides dialog. </p>  <p>3. Dockable dialog API comes. This API wish was highly expected by Revit developers. The docked pane can be hidden, show, and removed via API. For details, see DockablePane class in the RevitAPI.chm file.</p>  <p>4. Post Revit shipped command in your plug-ins is available now. Many developer wish to have this long time ago, now the dream comes true. This can enhance the plugins’s functionalities by calling existing powerful commands.</p>  <p>5. The API to copy elements between documents can be used now. It supports copy and paste of arbitrary element. That means you can store some standard views or details in a template model view, and copy them to new model documents via API. System types can also been copied to new model files. You can think more scenarios to use this API. For detail see ElementTransformUtils.CopyElements() method</p>  <p>6. Revit now supports APIs for joining, unjoining, querying join state, and changing join order of elements in a model through the JoinGeometryUtils class.</p>  <p>7. Another cool API feature is that developer can create Macros, and launch Macros commands, and manager Macros via Revit API.&#160; For example you can create a external command that can start a macro. and your users can click a button in Ribbon to start a macro.</p>  <p>Hope you are happy to see these APIs.Here just a small part of the new API features. Please explore all new features when the SDK is posted to <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=2484975">Autodesk Developer Center.</a></p>
