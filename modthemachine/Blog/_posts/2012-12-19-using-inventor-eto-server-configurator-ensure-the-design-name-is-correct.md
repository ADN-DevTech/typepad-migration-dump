---
layout: "post"
title: "Using Inventor ETO Server Configurator &ndash;ensure the Design name is correct"
date: "2012-12-19 21:45:17"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/12/using-inventor-eto-server-configurator-ensure-the-design-name-is-correct.html "
typepad_basename: "using-inventor-eto-server-configurator-ensure-the-design-name-is-correct"
typepad_status: "Publish"
---

<p>In this <a href="http://modthemachine.typepad.com/my_weblog/2012/10/using-inventor-eto-server-configurator.html" target="_blank">post</a> you can find a discussion on using the ETO server configurator. Recently I had a case where ETO server was being used and an error “No Such Design” was occurring when&#0160; AddDynamicChildRule() was called. (One of the parameters is the name of a design). The design was available in the application and the code worked on my system. After looking at the behavior on the developer’s system we found that the design set in the ETO server configurator was not a name of an available design. In this screenshot the Design is set to FloorSystem and the problem occurred because the actual name of the design was FloorSystems.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee66d5413970d-pi"><img alt="image" border="0" height="337" src="/assets/image_458802.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="482" /></a></p>
<p>I have logged a wish list with engineering to have the configurator pop up a warning if the design set for the application is not available.</p>
<p>-Wayne&#0160;</p>
