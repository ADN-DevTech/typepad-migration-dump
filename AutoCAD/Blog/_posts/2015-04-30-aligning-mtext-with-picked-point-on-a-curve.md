---
layout: "post"
title: "Aligning MText with picked point on a curve"
date: "2015-04-30 00:22:52"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "2016"
  - "ActiveX"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/aligning-mtext-with-picked-point-on-a-curve.html "
typepad_basename: "aligning-mtext-with-picked-point-on-a-curve"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In <a href="http://forums.autodesk.com/t5/autocad-2013-2014-2015-2016/help-modifying-this-lisp/m-p/5610380#M79621">this</a> discussion forum post, the developer wanted an MText to align automatically with the curve without having to provide additional inputs for specifying the rotation. <a href="http://forums.autodesk.com/t5/user/viewprofilepage/user-id/69526">Kent Cooper</a>'s nice reply in that forum post provides all that is necessary to implement that. Since this requirement of aligning an MText along a curve is quite essential in Civil / Survey applications, I am posting a bare-bone implementation of it that you can customize. The key to finding the rotation is to determine the first derivative (slope) of the curve at the point the entity was selected.&nbsp;</p>
<p>Here is the code snippet :</p>
<p></p>
<PRE>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vl-load-com</FONT></B><FONT COLOR="#800000">)</FONT>

<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">setq</FONT></B> <B><FONT COLOR="#0000FF">es</FONT></B>    <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">entsel</FONT></B><FONT COLOR="#800000">)</FONT>
      <B><FONT COLOR="#0000FF">entpt</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">osnap</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">cadr</FONT></B> <B><FONT COLOR="#0000FF">es</FONT></B><FONT COLOR="#800000">)</FONT> <FONT COLOR="#FF0000">&quot;_nea&quot;</FONT><FONT COLOR="#800000">)</FONT>
      <B><FONT COLOR="#0000FF">ang</FONT></B>   <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">angle</FONT></B> '<FONT COLOR="#800000">(</FONT><FONT COLOR="#800080">0</FONT> <FONT COLOR="#800080">0</FONT> <FONT COLOR="#800080">0</FONT><FONT COLOR="#800000">)</FONT>
		   <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vlax-curve-getFirstDeriv</FONT></B>
		     <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vlax-ename-</FONT></B><FONT COLOR="#800000">&gt;</FONT><B><FONT COLOR="#0000FF">vla-object</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">car</FONT></B> <B><FONT COLOR="#0000FF">es</FONT></B><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT>
		     <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vlax-curve-getParamAtPoint</FONT></B>
		       <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vlax-ename-</FONT></B><FONT COLOR="#800000">&gt;</FONT><B><FONT COLOR="#0000FF">vla-object</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">car</FONT></B> <B><FONT COLOR="#0000FF">es</FONT></B><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT>
		       <B><FONT COLOR="#0000FF">entpt</FONT></B>
		     <FONT COLOR="#800000">)</FONT>
		   <FONT COLOR="#800000">)</FONT>
	    <FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">if</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">and</FONT></B> <FONT COLOR="#800000">(</FONT><FONT COLOR="#800000">&gt;</FONT> <B><FONT COLOR="#0000FF">ang</FONT></B> <FONT COLOR="#800000">(</FONT>/ <B><FONT COLOR="#0000FF">pi</FONT></B> <FONT COLOR="#800080">2</FONT><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT> <FONT COLOR="#800000">(</FONT><FONT COLOR="#800000">&lt;</FONT><FONT COLOR="#800000">=</FONT> <B><FONT COLOR="#0000FF">ang</FONT></B> <FONT COLOR="#800000">(</FONT><FONT COLOR="#800000">*</FONT> <B><FONT COLOR="#0000FF">pi</FONT></B> <FONT COLOR="#800080">1</FONT>.<FONT COLOR="#800080">5</FONT><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT>
  <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">setq</FONT></B> <B><FONT COLOR="#0000FF">ang</FONT></B> <FONT COLOR="#800000">(</FONT><FONT COLOR="#800000">+</FONT> <B><FONT COLOR="#0000FF">ang</FONT></B> <B><FONT COLOR="#0000FF">pi</FONT></B><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">setq</FONT></B> <B><FONT COLOR="#0000FF">hght</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">getreal</FONT></B> <FONT COLOR="#FF0000">&quot;\nText Height : &quot;</FONT><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">setq</FONT></B>
  <B><FONT COLOR="#0000FF">mspace</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-get-modelspace</FONT></B>
	   <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-get-activedocument</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vlax-get-acad-object</FONT></B><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT>
	 <FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">)</FONT>

<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">setq</FONT></B> <B><FONT COLOR="#0000FF">mtextobj</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-addMText</FONT></B>
		 <B><FONT COLOR="#0000FF">mspace</FONT></B>
		 <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vlax-3d-point</FONT></B> <B><FONT COLOR="#0000FF">entpt</FONT></B><FONT COLOR="#800000">)</FONT>
		 <FONT COLOR="#800080">0</FONT>.<FONT COLOR="#800080">0</FONT>
		 <FONT COLOR="#FF0000">&quot;AUTOCAD&quot;</FONT>
	       <FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-put-attachmentPoint</FONT></B>
  <B><FONT COLOR="#0000FF">mtextobj</FONT></B>
  <B><FONT COLOR="#0000FF">acAttachmentPointMiddleCenter</FONT></B>
<FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-put-insertionPoint</FONT></B> <B><FONT COLOR="#0000FF">mtextobj</FONT></B> <FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vlax-3d-point</FONT></B> <B><FONT COLOR="#0000FF">entpt</FONT></B><FONT COLOR="#800000">)</FONT><FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-put-Rotation</FONT></B> <B><FONT COLOR="#0000FF">mtextobj</FONT></B> <B><FONT COLOR="#0000FF">ang</FONT></B><FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-put-Height</FONT></B> <B><FONT COLOR="#0000FF">mtextobj</FONT></B> <B><FONT COLOR="#0000FF">hght</FONT></B><FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-put-Color</FONT></B> <B><FONT COLOR="#0000FF">mtextobj</FONT></B> <FONT COLOR="#800080">7</FONT><FONT COLOR="#800000">)</FONT>
<FONT COLOR="#800000">(</FONT><B><FONT COLOR="#0000FF">vla-put-backgroundfill</FONT></B> <B><FONT COLOR="#0000FF">mtextobj</FONT></B> <B><FONT COLOR="#0000FF">:vlax-true</FONT></B><FONT COLOR="#800000">)</FONT>
</PRE>
<p>Here is a sample output :</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c782410a970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c782410a970b img-responsive" alt="1" title="1" src="/assets/image_5171.jpg" style="margin: 0px 5px 5px 0px;" /></a>
