---
layout: "post"
title: "Viewing 50+ design software formats from a web-page"
date: "2015-05-22 08:10:42"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "PaaS"
  - "SaaS"
original_url: "https://www.keanw.com/2015/05/viewing-50-design-software-formats-from-a-web-page.html "
typepad_basename: "viewing-50-design-software-formats-from-a-web-page"
typepad_status: "Publish"
---

<p>This is very cool. As <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/05/a360-widget.html" target="_blank">Stephen Preston has reported</a>, over on the Cloud &amp; Mobile DevBlog, the A360 team has delivered a widget that can be embedded in web-pages and views design files – including DWG files saved from AutoCAD, of course – that are dragged &amp; dropped onto it. Basically allowing you to view them as you would in A360, but inside <em>any</em> web-page.</p>  <p>Instructions are available at <a title="http://360.autodesk.com/viewer/widget" href="http://360.autodesk.com/viewer/widget">360.autodesk.com/viewer/widget</a>, although – as Stephen notes – be sure to call adskViewerWidget.Init() with a capital “I”.</p>  <p>There are two ways to render the widget. You can either render just the drop area…</p>  <br />  <div id="dropAreaContainer" style="width: 100%"></div>  <br />  <p>… or the full widget:</p>  <br />  <div id="widgetContainer" style="width: 230px; margin-left: auto; margin-right: auto"></div>  <br />  <p>I'm sure you'll agree this is very handy. Give it a try yourself!</p> <script type="text/javascript">
var adskViewerWidget = adskViewerWidget();
adskViewerWidget.Init('#dropAreaContainer', true);
adskViewerWidget.Init('#widgetContainer');
</script>
