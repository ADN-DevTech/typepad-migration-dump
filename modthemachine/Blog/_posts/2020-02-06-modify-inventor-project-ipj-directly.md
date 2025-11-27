---
layout: "post"
title: "Modify Inventor Project (*.ipj file) directly"
date: "2020-02-06 16:38:45"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/02/modify-inventor-project-ipj-directly.html "
typepad_basename: "modify-inventor-project-ipj-directly"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4e49c5f200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ProjectManager" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4e49c5f200d img-responsive" src="/assets/image_96966.jpg" title="ProjectManager" /></a></p>
<p>The <strong>Inventor API</strong> exposes pretty much all the <strong>Project</strong> related functionality through the <strong>DesignProjectManager</strong> and <strong>DesignProject</strong> objects:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4bb6fc4200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DesignProjectAPI" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4bb6fc4200c img-responsive" src="/assets/image_288730.jpg" title="DesignProjectAPI" /></a></p>
<p>However, if a certain functionality is not exposed or is not working correctly, you may modify the project file (<strong>*.ipj</strong>) directly as well.</p>
<p>1) Make sure that a copy of the project file is saved somewhere so that you won&#39;t overwrite it by accident<br />2) Then modify the project through the <strong>UI</strong> - see above picture<br />3) Save the project file<br />4) Compare the content of the original project file and the new one to see what exactly got added or changed<br />5) Now you know how to modify the project programmatically so that it will have the necessary content<br /><br /><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4e49ead200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ProjectFile" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4e49ead200d image-full img-responsive" src="/assets/image_446337.jpg" title="ProjectFile" /></a><br /><br />This is the method that was followed in this example as well: <a href="https://adndevblog.typepad.com/manufacturing/2016/11/add-to-frequently-used-subfolders-collection.html">https://adndevblog.typepad.com/manufacturing/2016/11/add-to-frequently-used-subfolders-collection.html</a></p>
<p>-Adam</p>
