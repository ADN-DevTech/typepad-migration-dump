---
layout: "post"
title: "No iProperty Change event fired for iPart row change"
date: "2015-12-14 03:48:17"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/12/no-iproperty-change-event-fired-for-ipart-row-change.html "
typepad_basename: "no-iproperty-change-event-fired-for-ipart-row-change"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>If you have an <strong>iLogic</strong> rule then you can hook it up to various events - one of those is <strong>iProperty&#0160;Change</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1839d5f970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IPropertyChange" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1839d5f970c img-responsive" src="/assets/image_377f66.jpg" title="IPropertyChange" /></a></p>
<p>Though ideally this event should fire even when the <strong>iProperty</strong> changes as a result of an <strong>iPart</strong> factory row change, it does not. It&#39;s a limitation in the <strong>Inventor API</strong>.</p>
<p>However, the following&#0160;workaround is available.</p>
<p>First of all, you might have to add a new user parameter to the part just for this purpose:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f9eccc970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="IPropertyChange2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f9eccc970b img-responsive" src="/assets/image_185f52.jpg" title="IPropertyChange2" /></a></p>
<p>Add this parameter to the <strong>iPart</strong> table and make sure its value changes whenever the <strong>iProperty</strong> (in this case <strong>Title</strong>) changes - i.e. it needs to have different values in each row.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb089e586f970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IPropertyChange3" class="asset  asset-image at-xid-6a0167607c2431970b01bb089e586f970d img-responsive" src="/assets/image_c24c0f.jpg" title="IPropertyChange3" /></a></p>
<p>&#0160;</p>
<p>Then add a line like this to the rule:</p>
<pre>trigger = UserParameter</pre>
<p>Even though the purpose of the rule is to work with the <strong>iProperty</strong>, you can use the parameter change to trigger it.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1839dbc970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IPropertyChange4" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1839dbc970c img-responsive" src="/assets/image_6efd2e.jpg" title="IPropertyChange4" /></a></p>
<p>Attached is the sample part file:&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01bb089e58a1970d img-responsive"><a href="http://adndevblog.typepad.com/files/iparttest.ipt">Download IPartTest</a></span><br /><br /></p>
