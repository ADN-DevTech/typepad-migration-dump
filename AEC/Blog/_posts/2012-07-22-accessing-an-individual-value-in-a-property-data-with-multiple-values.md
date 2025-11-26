---
layout: "post"
title: "Accessing an individual value in a property data with multiple values"
date: "2012-07-22 16:23:54"
author: "Mikako Harada"
categories:
  - "AutoCAD Architecture"
  - "AutoCAD MEP"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/07/accessing-an-individual-value-in-a-property-data-with-multiple-values.html "
typepad_basename: "accessing-an-individual-value-in-a-property-data-with-multiple-values"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I have a duct fitting, where I have attached many property sets.&#0160; I noticed that some of automatic properties, such as RectangularConnectorWidth and RectangularConnectorHeight, show the information as a form of a concatenated string with semicolon (;) as a delimiter. This is because the duct fitting has two connectors. For example, for an elbow with width x height of 36x24 to 30x24, it shows as 36.0;30.0 for width and 20.0;24.0 for height.&#0160;</p>
<p>I want to access to individual values in those string. How can we do that?&#0160;</p>
<p><strong>Solution</strong></p>
<p>You can use Split function in a formula property to separate a string with “;” as a delimiter.&#0160; The following shows an example:</p>
<p>MyString = &quot;[RectangularConnectionWidth]&quot;<br />MyArray = Split( MyString, &quot;;&quot;, -1, 1 )<br />RESULT = MyArray(0)</p>
