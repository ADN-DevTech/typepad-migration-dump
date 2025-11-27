---
layout: "post"
title: "Read Parameter values using Apprentice"
date: "2016-02-15 05:23:13"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/02/read-parameter-values-using-apprentice.html "
typepad_basename: "read-parameter-values-using-apprentice"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Unfortunately, <a href="http://modthemachine.typepad.com/my_weblog/2010/03/iproperties-without-inventor-apprentice.html">Apprentice</a>&#0160;cannot access the <strong>Parameters</strong> of an <strong>Inventor</strong> document. You would need to use <strong>Inventor</strong> for that.<br />If firing up an <strong>Inventor</strong> instance would be too slow or not feasible for e.g. licensing reasons, a workaround could be setting those <strong>Parameters</strong> to export to <strong>iProperties</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19fe105970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Params2" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19fe105970c img-responsive" src="/assets/image_5aef7e.jpg" title="Params2" /></a></p>
<p>In that&#0160;case a corresponding <strong>iProperty</strong> will be automatically created and kept up-to-date in the &quot;<strong>Inventor User Defined Properties</strong>&quot; (Custom) section:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08ba8d7d970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Params3" class="asset  asset-image at-xid-6a0167607c2431970b01bb08ba8d7d970d img-responsive" src="/assets/image_395c7e.jpg" title="Params3" /></a></p>
<p>The good news is that <strong>iProperties</strong> can be read not only through <strong>Apprentice</strong>, but also using <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa380361(v=vs.85).aspx">Structured Storage API</a>, which is completely independent of <strong>Inventor</strong>. Here is a view of our <strong>Inventor</strong>&#0160;part file in a program called <strong>Structured Storage Viewer</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19fe221970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Params1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19fe221970c img-responsive" src="/assets/image_1837c0.jpg" title="Params1" /></a></p>
<p>Here is a sample using <strong>Structured Storage API</strong> to get information from an <strong>Inventor</strong> file:&#0160;<br /><a href="http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-3.html">http://adndevblog.typepad.com/manufacturing/2013/03/save-extra-data-in-inventor-file-3.html</a></p>
<p>If setting the &quot;<strong>Export Parameter</strong>&quot; option for the <strong>Parameters</strong>&#0160;you are interested in is not an option, then maybe you could run a batch process that stores those parameters somewhere else (e.g. in a <strong>CSV</strong> file named the same but with a &quot;.csv&quot; extension) next to the <strong>Inventor</strong> file, or create an <strong>Add-In</strong>&#0160;which&#0160;does the same but as part of the <strong>OnSaveDocument</strong>&#0160;event of the <strong>ApplicationEvents</strong> object.</p>
