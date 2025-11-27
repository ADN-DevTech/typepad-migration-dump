---
layout: "post"
title: "Adding Dropdown/popup buton in inventor ribbon panel"
date: "2012-06-27 21:54:22"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/adding-dropdownpopup-buton-in-inventor-ribbon-panel.html "
typepad_basename: "adding-dropdownpopup-buton-in-inventor-ribbon-panel"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I want to create dropdown and popup controls in Inventor ribbon interface, like for example the &quot;Text&quot; command button in the Sketch -&gt; Draw panel, and the &quot;UserInterface&quot; command button in the View tab in the Part.</p>  <p><b>A:</b></p>  <p>You should use CommandControls.AddButtonPopup method, and CommandControls.AddTogglePopup method respectively in order to add those two styles of controls in the ribbon panel. The attached .Net add-in contains an example for both.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:330a3ca7-1f48-4d73-a0a5-64c3976bad59" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/menudemo-1.zip" target="_blank">Ribbon Sample</a></p></div>
