---
layout: "post"
title: "iLogic variable not up-to-date"
date: "2020-04-06 12:02:31"
author: "Adam Nagy"
categories:
  - "Adam"
  - "iLogic"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/04/ilogic-variable-not-up-to-date.html "
typepad_basename: "ilogic-variable-not-up-to-date"
typepad_status: "Publish"
---

<p>When an <strong>iLogic rule</strong> runs and it has a <strong>variable</strong> with the name of a given <strong>Inventor parameter,</strong> then it will take the current value of that <strong>parameter</strong> and keep using it - its value will <strong>not get updated</strong> if the value of the <strong>parameter</strong> is changed in the meantime outside the <strong>rule</strong>.<br />The following article alludes to it, though does not say it explicitly: <a href="https://help.autodesk.com/view/INVNTOR/2020/ENU/?guid=GUID-EF53484C-D750-41F8-9AB1-032B73BB071F">About Rules and Forms in iLogic</a></p>
<p>Let&#39;s say we have a <strong>parameter</strong> named &quot;<strong>MyParam</strong>&quot; and two <strong>rules</strong>: &quot;<strong>MyRule</strong>&quot; and &quot;<strong>ChangeMyParam</strong>&quot;. Neither&#0160;<strong>rule </strong>is run automatically, i.e. they have the &quot;<strong>Don&#39;t run automatically</strong>&quot; option checked:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a51dba70200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DontRunAutomatically" class="asset  asset-image at-xid-6a00e553fcbfc688340240a51dba70200b img-responsive" src="/assets/image_450979.jpg" title="DontRunAutomatically" /></a></p>
<p>Inside &quot;<strong>MyRule</strong>&quot; we use &quot;<strong>MyParam</strong>&quot; variable and show its value <strong>before</strong> and <strong>after</strong> calling &quot;<strong>ChangeMyParam</strong>&quot; rule.</p>
<p><span style="text-decoration: underline;"><strong>MyRule</strong>:</span></p>
<pre>MessageBox.Show(MyParam, &quot;MyRule before ChangeMyParam&quot;)

iLogicVb.RunRule(&quot;ChangeMyParam&quot;) 

MessageBox.Show(MyParam, &quot;MyRule after ChangeMyParam&quot;)

MessageBox.Show(Parameter(&quot;MyParam&quot;), &quot;MyRule after ChangeMyParam&quot;)</pre>
<p><span style="text-decoration: underline;"><strong>ChangeMyParam</strong>:</span></p>
<pre>MyParam = MyParam + 10</pre>
<p>This is what we get when we run &quot;<strong>MyRule</strong>&quot;:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a51dbb0c200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MyRule" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a51dbb0c200b image-full img-responsive" src="/assets/image_112472.jpg" title="MyRule" /></a></p>
<p>As you can see, if you want to get the <strong>up-to-date value</strong> of an <strong>Inventor parameter</strong>, then you can use the<strong> Parameter() </strong>function.</p>
<p>-Adam</p>
