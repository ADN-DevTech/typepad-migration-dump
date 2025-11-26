---
layout: "post"
title: "Creating stacked and multiple Profile Views using ProfileView.Create()"
date: "2012-07-09 02:08:31"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/creating-stacked-and-multiple-profile-views-using-profileviewcreate.html "
typepad_basename: "creating-stacked-and-multiple-profile-views-using-profileviewcreate"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Stacked profile views are a collection of related profiles that are drawn in separate, vertically arranged profile views.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616446ed4970c-pi" style="display: inline;"><img alt="Stacked_PV" class="asset  asset-image at-xid-6a0167607c2431970b017616446ed4970c" src="/assets/image_4df5ae.jpg" title="Stacked_PV" /></a><br /><br /></p>
<p>In Civil 3D, <strong>ProfileView</strong> manages the graphic display of profile data objects within a drawing. A ProfileView is used to display one or more profiles for a horizontal alignment. You can configure data bands and profile annotations in a ProfileView to make it clearer or more informative for the user.</p>
<p>Civil 3D 2013 .NET API allows us to create stacked and multiple profile views using <strong>ProfileView.Create().&#0160;</strong>Here is a C# code snippet which demonstrates creation of stacked, multiple Profile views :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// select an alignment </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> prOpt = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect an Alignment :&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; prOpt.SetRejectMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nObject must be an Alignment!\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; prOpt.AddAllowedClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">), </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> alignID = ed.GetEntity(prOpt).ObjectId;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Select the location to insert the ProfileView in the DWG file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;"> ppo = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect the location to Insert the Profile View :&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr = ed.GetPoint(ppo);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> insertPosition = ppr.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;"> profileViewName = </span><span style="color: #a31515; line-height: 140%;">&quot;ADN_DEMO_Stacked_ProfileView&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get profile view band set style ID:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> profileViewBandSetId = civilDoc.Styles.ProfileViewBandSetStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Standard&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// If this doesn&#39;t exist, get the first style in the collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (profileViewBandSetId == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">) profileViewBandSetId = civilDoc.Styles.ProfileViewBandSetStyles[0];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// StackedProfileViewsCreationOptions stackedOptions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> topViewStyleId = civilDoc.Styles.ProfileViewStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Stacked - Top&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> middleViewStyleId = civilDoc.Styles.ProfileViewStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Stacked - Middle&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> bottomViewStyleId = civilDoc.Styles.ProfileViewStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Stacked - Bottom&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">StackedProfileViewsCreationOptions</span><span style="line-height: 140%;"> stackPVOptions = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">StackedProfileViewsCreationOptions</span><span style="line-height: 140%;">(topViewStyleId, middleViewStyleId, bottomViewStyleId);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Set number of Views</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; stackPVOptions.NumberOfViews = 3;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// ProfileView.Create(ObjectId alignmentId, Point3d insertPosition, string profileViewName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// ObjectId profileViewBandSetId,&#0160; StackedProfileViewsCreationOptions stackedOptions )</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ProfileView</span><span style="line-height: 140%;">.Create(alignID, insertPosition, profileViewName, profileViewBandSetId, stackPVOptions);</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
