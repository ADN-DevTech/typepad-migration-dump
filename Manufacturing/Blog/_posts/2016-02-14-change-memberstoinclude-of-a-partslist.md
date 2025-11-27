---
layout: "post"
title: "Change MembersToInclude of a PartsList"
date: "2016-02-14 07:14:03"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/change-memberstoinclude-of-a-partslist.html "
typepad_basename: "change-memberstoinclude-of-a-partslist"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>In the <strong>UI</strong> you can change which member of an <strong>iPart</strong> or <strong>iAssembly</strong> is included by double clicking the parts list and then clicking the &quot;<strong>Member Selection</strong>&quot; button:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19f8c59970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MembersToInclude2" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19f8c59970c img-responsive" src="/assets/image_72f590.jpg" title="MembersToInclude2" /></a></p>
<p>If you try to change the included member like this then you&#39;ll get an &quot;<strong>Invalid procedure call or argument</strong>&quot; error:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08ba3409970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MembersToInclude" class="asset  asset-image at-xid-6a0167607c2431970b01bb08ba3409970d img-responsive" src="/assets/image_7912e2.jpg" title="MembersToInclude" /></a></p>
<p>If something does not work then it&#39;s always good to try to do it through the <strong>UI</strong> and then check the results in the <strong>VBA</strong> <strong>Watches</strong> window:<br /><a href="http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html">http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html</a></p>
<p>If you did that then you&#39;d find (as shown in the above image) that&#0160;<strong>MembersToInclude</strong> stores an <strong>array of strings</strong> - so that&#39;s what you&#39;d need to pass to it:</p>
<pre>Sub ChangeMembersToInclude()
  &#39; The PartsList needs to be selected in the UI
  Dim oPL As PartsList
  Set oPL = ThisApplication.ActiveDocument.SelectSet(1)
  
  &#39; This works
  Dim members(0) As String
  members(0) = &quot;iPart-02&quot;
  oPL.MembersToInclude = members
End Sub</pre>
