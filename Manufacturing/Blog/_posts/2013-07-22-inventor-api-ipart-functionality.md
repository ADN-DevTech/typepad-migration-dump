---
layout: "post"
title: "Inventor API : iPart functionality"
date: "2013-07-22 00:29:23"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/inventor-api-ipart-functionality.html "
typepad_basename: "inventor-api-ipart-functionality"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>
<p>The Inventor API supports the ability to create an iPartFactory from a standard part (starting from Inventor 2008), access the iPart table, publish iPart members, insert iPart member in assemblies, create new rows in the iPart factory table, etc.</p>
<p>Once the iPart factory has been defined, the iPart members can be explicitly published or published when inserted into an assembly. To replace an iPart instance in an assembly, you can use ComponentOccurrence.ChangeRowOfiPartMember method directly.</p>
<p>The attached VBA sample demonstrates some of the iParts functionality. To run the sample, please&#0160; copy attached folfer&#0160; to the &quot;C:\temp&quot; folder or copy it to any location and change the hard-coded paths in the VBA project (the strFilePath string variable defines this path). The attachment also contains a standard factory “iPart_Standard.ipt” and a custom iPart factory &quot;iPart_Custom.ipt&quot; which are used in the sample. The sample shows how to publish iPart members, insert iPart members into an assembly, and replace one member with another item from the table by “ChangeRowOfiPartMember”.</p>
<p>All the sample files are created by R2014.</p>
<p>&#0160;<a href="http://adndevblog.typepad.com/files/ipart_demo.zip">Download IPart_Demo</a></p>
<p>Detailed review of how iPart API can be used to create and manipulate iPart table content is posted here:</p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2013/02/manipulate-rows-and-columns-of-ipart-1.html">http://adndevblog.typepad.com/manufacturing/2013/02/manipulate-rows-and-columns-of-ipart-1.html</a></p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2013/03/manipulate-rows-and-columns-of-ipart-2.html">http://adndevblog.typepad.com/manufacturing/2013/03/manipulate-rows-and-columns-of-ipart-2.html</a></p>
