---
layout: "post"
title: "Sneak Peek - New Autodesk 360 Viewer"
date: "2014-05-30 01:18:44"
author: "Wayne Brill"
categories:
  - "Announcements"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2014/05/sneak-peek-new-autodesk-360-viewer.html "
typepad_basename: "sneak-peek-new-autodesk-360-viewer"
typepad_status: "Publish"
---

<p>Kean Walmsley recently presented the first sneak peek at the <a href="http://click.typepad.com/api/click?format=go&amp;jsonp=vglnk_jsonp_14014047399988&amp;key=7626aa89b5cdca8c8b5ad715622cd669&amp;libId=f3649e15-6a55-4183-8d5e-96642f6d0e5a&amp;loc=http%3A%2F%2Fthebuildingcoder.typepad.com%2Fblog%2F2014%2F05%2Fupdated-sdk-devtv-bim-360-news-and-viewer.html%236&amp;v=1&amp;out=http%3A%2F%2Fthrough-the-interface.typepad.com%2Fthrough_the_interface%2F2014%2F05%2Fa-sneak-peek-at-the-new-autodesk-360-viewer.html&amp;title=The%20Building%20Coder%3A%20Updated%20SDK%2C%20DevTV%2C%20BIM%20360%20News%20and%20Viewer&amp;txt=%0Asneak%20peek%20at%20the%20new%20Autodesk%20360%20viewer" target="_blank">new Autodesk 360 viewer</a>.</p>
<p>The new 3D viewer is really cool. To try it out you use your Autodesk 360 account. You can upload your files (by folder if you want) and then view them using the new viewing web service. (it does a lot more than just viewing) The viewer is in Tech Preview but it is working very well. Also you will need a WebGL-enabled browser. You can use this <a href="http://doesmybrowsersupportwebgl.com/" target="_blank">site</a> to test. </p>
<p>Here is a model of a front loader using the viewer. (Model courtesy of Engineering Center) This model is being served up by a demo service that takes care authentication. For more information, please refer to <a href="http://through-the-interface.typepad.com/through_the_interface/2014/05/a-sneak-peek-at-the-new-autodesk-360-viewer.html">Kean&#39;s original post</a>&#0160;</p>
<p><iframe frameborder="0" height="264" id="MFG-viewer" name="mfg_viewer" src="https://s3.amazonaws.com/FastViewer/index.html?file=frontloader/0.svf" title="Front Loader" width="470"></iframe></p>
<p>&#0160;</p>
<p>Aside from the standard zoom, pan and orbit, in this model, you can press the <strong>structure</strong> button <img alt="Structure button" border="0" height="27" src="/assets/image_667788.jpg" title="Structure button" width="27" /> to browse down through the model&#39;s assembly structure or component hierarchy. You can use this to isolate specific components in your model, hiding everything else.</p>
<p>Double click an individual building component to highlight it, list its identity and other properties.</p>
<p>After hitting reset <img alt="Reset button" border="0" height="26" src="/assets/image_537891.jpg" title="Reset button" width="26" />, now try the <strong>explode</strong> button <img alt="Explode button" border="0" height="27" src="/assets/image_702103.jpg" title="Explode button" width="28" /> and then manipulate the slider that appears at the top of the window to move the various model components outwards from the center to form an exploded view.</p>
<p>Please note that these super-simple single-line embedded viewers do not immediately support all the possible functionality. It is – or will be soon, we hope – really easy to implement that in a slightly more full-fledged context, though.</p>
<p>Aside from the need to support a huge array of formats, the viewer is really good at streaming large models – displaying them at appropriate levels of detail – and allowing you to get in and work with the structure of these models.</p>
<p>-Wayne</p>
