---
layout: "post"
title: "Set &ldquo;Embed Interop Types&rdquo; to False to avoid problems with events"
date: "2012-07-17 20:41:12"
author: "Wayne Brill"
categories:
  - "C#"
  - "Inventor"
  - "VB.NET"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/07/set-embed-interop-types-to-false-to-avoid-problems-with-events.html "
typepad_basename: "set-embed-interop-types-to-false-to-avoid-problems-with-events"
typepad_status: "Publish"
---

<p>We have had several cases recently where events such as OnFileSaveAsDialog were not working properly. Problems with events can occur if the property “Embeding Interop Types” of the referenced Inventor Interop is set to True. This setting is new in Visual Studio 2010 with .NET 4 and it defaults to True. If you change the setting to False and rebuild the events work normally.</p>
<p>Here is a screenshot of the properties window in VB.NET.&#0160;</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340177436dfabf970d-pi"><img alt="image" border="0" height="270" src="/assets/image_45866.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="503" /></a></p>
<p>-Wayne</p>
