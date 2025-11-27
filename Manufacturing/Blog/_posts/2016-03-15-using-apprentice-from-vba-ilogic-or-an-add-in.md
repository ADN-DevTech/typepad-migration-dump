---
layout: "post"
title: "Using Apprentice from VBA, iLogic or an Add-In "
date: "2016-03-15 03:01:05"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/using-apprentice-from-vba-ilogic-or-an-add-in.html "
typepad_basename: "using-apprentice-from-vba-ilogic-or-an-add-in"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Using <strong>Apprentice</strong> from inside the <strong>Inventor process</strong> is not supported. So using it from anything that is running <strong>in-process</strong> with <strong>Inventor</strong> like <strong>VBA</strong>, an <strong>Add-In dll</strong> or an <strong>iLogic</strong> code, is also not supported. &#0160;</p>
<p>Before <strong>64 bit</strong> <strong>VBA</strong> became available, the <strong>64 bit&#0160;</strong>version of&#0160;<strong>Inventor</strong> was using the&#0160;<strong>32 bit VBA</strong> environment which had to run in a <strong>separate process</strong>. In that case you could get away with using <strong>Apprentice</strong> from <strong>VBA</strong>. Now (I think since <strong>Inventor 2014</strong>) <strong>VBA</strong> is running <strong>in-process</strong>&#0160;with <strong>64 bit</strong> <strong>Inventor</strong>, so now you cannot use <strong>Apprentice</strong> from <strong>VBA</strong>. &#0160;</p>
<p>Easiest solution&#0160;is to wrap all the <strong>Apprentice</strong> related functionality into your own <strong>exe</strong> and call that from inside <strong>Inventor</strong>, or just rely on the <strong>Inventor</strong> <strong>API</strong> to achieve what you need. E.g. for quite a few releases now&#0160;<strong>ReplaceReference</strong> has been available in <strong>Inventor</strong> <strong>API</strong> too. &#0160;</p>
