---
layout: "post"
title: "A better way to add custom toolbar for Autodesk viewer"
date: "2014-08-13 21:21:11"
author: "Daniel Du"
categories:
  - "Client"
  - "Daniel Du"
  - "HTML5"
  - "Javascript"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2014/08/a-better-way-to-add-custom-toolbar-for-autodesk-viewer.html "
typepad_basename: "a-better-way-to-add-custom-toolbar-for-autodesk-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>In <a href="http://adndevblog.typepad.com/cloud_and_mobile/2014/08/adding-custom-toolbar-for-autodesk-viewer.html">last post</a>, I introduced how to add custom toolbar with the new Autodesk Viewer UI API in the simplest way, I believe you have already understand the usage of these APIs. In this post, I will introduce a better way to do that, make it more organized, and easy to maintain. In the code snippet of last post, you notice that the information of toolbar or button is scattered here and there in code, to add/change/remove a toolbar, you will have to scan the source code, it is very easy to make mistake. Is there a better way to do that? Here is what I did. </p>  <p>Firstly, to centralize the definition of toolbar and buttons, such as Id, tooltip, button text, icon url, etc. I create a toolbarConfig object as below, if I want to add another toolbar or button, I just edit this part: </p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: green">/////////////////////////////////////////////////////////////////////</span><br /><span style="color: green">// custom toobar config </span><br /><span style="color: blue">var</span> toolbarConfig = {<br />&#160;&#160;&#160; <span style="color: #a31515">'id'</span>: <span style="color: #a31515">'toolbar_id_1'</span>,<br />&#160;&#160;&#160; <span style="color: #a31515">'containerId'</span>: <span style="color: #a31515">'toolbarContainer'</span>,<br />&#160;&#160;&#160; <span style="color: #a31515">'subToolbars'</span>: [<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'id'</span>: <span style="color: #a31515">'subToolbar_id_non_radio_1'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'isRadio'</span>: <span style="color: blue">false</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'visible'</span>: <span style="color: blue">true</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'buttons'</span>: [<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'id'</span>: <span style="color: #a31515">'buttonRotation'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'buttonText'</span> : <span style="color: #a31515">'Rotation'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'tooltip'</span>: <span style="color: #a31515">'Ratate the model at X direction'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'cssClassName'</span>: <span style="color: #a31515">'glyphicon glyphicon glyphicon-play-circle'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'iconUrl'</span> :<span style="color: #a31515">'Images/3d_rotation.png'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'onclick'</span>: buttonRotationClick<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; },<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'id'</span>: <span style="color: #a31515">'buttonExplode'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'buttonText'</span>: <span style="color: #a31515">'Explode'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'tooltip'</span>: <span style="color: #a31515">'Explode the model'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'cssClassName'</span>: <span style="color: #a31515">''</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'iconUrl'</span>: <span style="color: #a31515">'Images/explode_icon.jpg'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'onclick'</span>: buttonExplodeClick<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }<br /> <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ]<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; },<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'id'</span>: <span style="color: #a31515">'subToolbar_id_radio_1'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'isRadio'</span>: <span style="color: blue">true</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'visible'</span>: <span style="color: blue">true</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'buttons'</span>: [<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'id'</span>: <span style="color: #a31515">'radio_button1'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'buttonText'</span>: <span style="color: #a31515">'radio_button1'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'tooltip'</span>: <span style="color: #a31515">'this is tooltip for radio button1'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'cssClassName'</span>: <span style="color: #a31515">''</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'iconUrl'</span>: <span style="color: #a31515">''</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'onclick'</span>: radioButton1ClickCallback<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; },<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; {<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'id'</span>: <span style="color: #a31515">'radio_button2'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'buttonText'</span>: <span style="color: #a31515">'radio_button2'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'tooltip'</span>: <span style="color: #a31515">'this is tooltip for radio button2'</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'cssClassName'</span>: <span style="color: #a31515">''</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'iconUrl'</span>: <span style="color: #a31515">''</span>,<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: #a31515">'onclick'</span>: radioButton2ClickCallback<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }<br /> <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ]<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; }<br />&#160;&#160;&#160; ]<br /> <br />};</pre>

<pre style="font-size: 13px; font-family: consolas; background: white; color: black"><br /><span style="color: blue">function</span> buttonRotationClick(e) {<br /> <br />&#160;&#160;&#160; <br />}<br />&#160;<br /> <br /><span style="color: blue">function</span> buttonExplodeClick() {<br />&#160;&#160;&#160;&#160; <br />}
<br /> <br /><span style="color: blue">function</span> button2ClickCallback(e) {<br />&#160;&#160;&#160; alert(<span style="color: #a31515">'Button2 is clicked'</span>);<br />}<br /><span style="color: blue">function</span> radioButton1ClickCallback(e) {<br />&#160;&#160;&#160; alert(<span style="color: #a31515">'radio Button1 is clicked'</span>);<br />}<br /><span style="color: blue">function</span> radioButton2ClickCallback(e) {<br />&#160;&#160;&#160; alert(<span style="color: #a31515">'radio Button2 is clicked'</span>);<br />}</pre>

<p>&#160;</p>

<p>Next step, I create an utility function, reading the configuration of toolbar and create them with Viewer UI API: 
  <br />

  <br /><span style="color: green">////add custom toolbar , usage example:</span>

  <br /><span style="color: green">//addToolbar(toolbarConfig);</span>

  <br /> 

  <br /><span style="color: green">////////////////////////////////////////////////////////////////////////////</span>

  <br /><span style="color: blue">function</span> addToolbar(toolbarConfig, viewer) {

  <br /> 

  <br />&#160;&#160;&#160; <span style="color: green">//find the container element in client webpage first</span>

  <br />&#160;&#160;&#160; <span style="color: blue">var</span> containter = document.getElementById(toolbarConfig.containerId);

  <br /> 

  <br />&#160;&#160;&#160; <span style="color: green">// if no toolbar container on client's webpage, create one and append it to viewer</span>

  <br />&#160;&#160;&#160; <span style="color: blue">if</span> (!containter) {

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; containter = document.createElement(<span style="color: #a31515">'div'</span>);

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; containter.id = <span style="color: #a31515">'custom_toolbar'</span>;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: green">//'position: relative;top: 75px;left: 0px;z-index: 200;';</span>

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; containter.style.position = <span style="color: #a31515">'relative'</span>;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; containter.style.top = <span style="color: #a31515">'75px'</span>;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; containter.style.left = <span style="color: #a31515">'0px'</span>;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; containter.style.zIndex= <span style="color: #a31515">'200'</span>;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; viewer.clientContainer.appendChild(containter);

  <br />&#160;&#160;&#160; }

  <br /> 

  <br />&#160;&#160;&#160; <span style="color: green">//create a toolbar</span>

  <br />&#160;&#160;&#160; <span style="color: blue">var</span> toolbar = <span style="color: blue">new</span> Autodesk.Viewing.UI.ToolBar(containter);

  <br /> 

  <br />&#160;&#160;&#160; <span style="color: blue">for</span> (<span style="color: blue">var</span> i = 0, len = toolbarConfig.subToolbars.length; i &lt; len; i++) {

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">var</span> stb = toolbarConfig.subToolbars[i];

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: green">//create a subToolbar</span>

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">var</span> subToolbar = toolbar.addSubToolbar(stb.id, stb.isRadio);

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; subToolbar.setToolVisibility(stb.visible);

  <br /> 

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: green">//create buttons</span>

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">for</span> (<span style="color: blue">var</span> j = 0, len2 = stb.buttons.length; j &lt; len2; j++) {

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">var</span> btn = stb.buttons[j];

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">var</span> button = Autodesk.Viewing.UI.ToolBar.createMenuButton(btn.id, btn.tooltip, btn.onclick);

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: green">//set css calss if availible </span>

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">if</span> (btn.cssClassName) {

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; button.className = btn.cssClassName;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: green">//set button text if availible</span>

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">if</span> (btn.buttonText) {

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">var</span> btnText = document.createElement(<span style="color: #a31515">'span'</span>);

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; btnText.innerText = btn.buttonText;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; button.appendChild(btnText);

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: green">//set icon image if availible</span>

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">if</span> (btn.iconUrl) {

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: blue">var</span> ico = document.createElement(<span style="color: #a31515">'img'</span>);

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ico.src = btn.iconUrl;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ico.className = <span style="color: #a31515">'toolbar-button'</span>;

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; button.appendChild(ico);

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; }

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; <span style="color: green">//add button to sub toolbar</span>

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; toolbar.addToSubToolbar(stb.id, button);

  <br /> 

  <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; }

  <br /> 

  <br /> 

  <br /> 

  <br />&#160;&#160;&#160; }</p>

<p>And here is what I get on Viewer: </p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73e018cfc970d-pi"><img title="image" style="border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px; display: inline" border="0" alt="image" src="/assets/image_23ed07.jpg" width="446" height="124" /></a> </p>

<p><a href="http://checkoutmymodel.azurewebsites.net/"><img title="image" border="0" alt="image" src="/assets/image_e4363e.jpg" width="446" height="421" /></a></p>
