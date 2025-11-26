---
layout: "post"
title: "Model-View-Controller Design Pattern"
date: "2012-06-04 13:07:46"
author: "Gopinath Taget"
categories:
  - "Cloud"
  - "Gopinath Taget"
  - "Mobile"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/model-view-controller-design-pattern.html "
typepad_basename: "model-view-controller-design-pattern"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html" target="_blank">Gopinath Taget</a></p>
<p>The <a href="http://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller" target="_blank">Model-view-controller</a> design pattern is a pretty important pattern to learn and understand if you are interested in Web/cloud and mobile programming. This design pattern allows you to architect your applications by dividing them into three main components:</p>
<p>1) The Model – Which represents the data</p>
<p>2) The View – Which provides a visual, graphic or any other kind of “view” of the data</p>
<p>3) The Controller – Which controls the view</p>
<p>The Model and View components do not directly talk to each other. They always communicate via the controller. This separation between the data and the view makes it very easy to manage the application and add/modify functionality. For instance if your data is represented as a bar chart, then the data would be the model and the bar chart would be the view. Also, the Bar Chart would be rendered and managed by a controller object or component. Now if you need to represent your data as a Pie chart instead, all you have to do is create a new view (for the pie chart) and/or a controller to manage the pie chart. The model component does not have to change.</p>
<p>This powerful design pattern is used very extensively in web programming technologies including <a href="http://www.asp.net/" target="_blank">ASP .NET</a>. It is also a&#0160; very central architecture used in <a href="http://en.wikipedia.org/wiki/IOS" target="_blank">iOS</a> programming.</p>
