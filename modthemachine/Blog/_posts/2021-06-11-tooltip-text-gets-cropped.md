---
layout: "post"
title: "Tooltip text gets cropped"
date: "2021-06-11 10:40:48"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2021/06/tooltip-text-gets-cropped.html "
typepad_basename: "tooltip-text-gets-cropped"
typepad_status: "Publish"
---

<p>If you are using very long strings without <strong>tabs</strong> or <strong>spaces</strong> in them (e.g. a long file path), those will get cropped.<br />E.g. the file path &quot;C:\\top_folder\\sub_folder\\sub_sub_folder\\sub_sub_sub_folder\\sub_sub_sub_sub_folder\\somefile.txt&quot; will show up like this:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340278802fabfb200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Tooltip_issue" class="asset  asset-image at-xid-6a00e553fcbfc688340278802fabfb200d img-responsive" src="/assets/image_47437.jpg" title="Tooltip_issue" /></a><br />Currently, that will also add an empty area on the right side of the tooltip, but that will be fixed.</p>
<p>There are two things you can do:<br />- add <strong>zero-width space</strong> characters (\u200b) wherever you are ok with the string being broken into a new line (e.g. after each \ character in the file path)<br />- add an <strong>ellipsis</strong> character (\u2026) at the point where the rest of the information is not important<br />(the tooltip width is 300 pixels which would allow about 40-50 characters in a line)&#0160;</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340278802faca5200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Tooltip_fixed" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340278802faca5200d image-full img-responsive" src="/assets/image_872434.jpg" title="Tooltip_fixed" /></a></p>
<p>-Adam</p>
