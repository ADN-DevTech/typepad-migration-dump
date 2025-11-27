---
layout: "post"
title: "Transaction.Commit throws exception"
date: "2016-03-28 15:29:16"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/transactioncommit-throws-exception.html "
typepad_basename: "transactioncommit-throws-exception"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Unlike in <strong>vanilla AutoCAD</strong>, in <strong>AutoCAD Mechanical</strong> each command is automatically wrapped in a <strong>Transaction</strong>. You can verify this by checking the value of <strong>Database.TransactionManager.TopTransaction</strong>&#0160;inside your command - in case of <strong>AutoCAD Mechanical</strong> it will be an object with type <strong>AppTransaction</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1b563b6970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AppTransaction" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1b563b6970c img-responsive" src="/assets/image_bb7acf.jpg" title="AppTransaction" /></a></p>
<p>When using the <strong>PlotEngine</strong>&#0160;in your code to publish a drawing and creating your own <strong>Transaction</strong> to open the necessary objects, it seems to get entangled with <strong>AppTransaction</strong>. As a result you get an exception &quot;<strong>Operation is not valid due to the current state of the object.</strong>&quot;&#0160;when calling <strong>Transaction.Commit</strong>() at the end of your command:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1b567dd970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="TrCommit" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1b567dd970c img-responsive" src="/assets/image_ad8a51.jpg" title="TrCommit" /></a></p>
<p>There seems to be two workarounds: either just use the already available&#0160;<strong>AppTransaction</strong> inside <strong>AutoCAD Mechanical</strong>&#0160;to open the objects you need (not sure if that could have some side effects) or use an <strong>OpenCloseTransaction&#0160;</strong>instead of a normal <strong>Transaction</strong>, by calling&#0160;<strong>StartOpenCloseTransaction</strong>():</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1b56850970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="StartOpenCloseTransaction" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1b56850970c img-responsive" src="/assets/image_9329d6.jpg" title="StartOpenCloseTransaction" /></a></p>
<p>&#0160;</p>
