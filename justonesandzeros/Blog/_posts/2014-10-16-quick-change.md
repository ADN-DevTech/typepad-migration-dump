---
layout: "post"
title: "Quick Change"
date: "2014-10-16 08:36:36"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/10/quick-change.html "
typepad_basename: "quick-change"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>I may be exaggerating slightly when I say that <strong>the Quick Change state on Items is the greatest invention mankind has ever or will ever achieve</strong>. The bad news is that it’s only available for Vault 2015 R2.&#0160; But that’s also a good thing, because once you start using it, there is no going back.&#0160; You are changed forever.</p>
<p><a href="http://justonesandzeros.typepad.com/images/2014/QuickChange/quickChange.png" target="_blank"><img alt="" src="/assets/quickChange_scaled.png" /></a><br /><span style="color: #888888;">(click for full size image)</span></p>
<p>If you are wondering why I am so excited about the Quick Change state, then you probably have never tried programming with Items in the Vault.&#0160; In Vault 2015 and earlier, the lifecycle states are rigidly defined, which made it really difficult to perform operations on a Released item.&#0160; And 99% of the time, Released Items are the only type of Items your app cares about.</p>
<p>Take this scenario.&#0160; You are writing an app that pushes Item data to an ERP system automatically when an Item gets released.&#0160; You when the new entry gets created in the ERP system, you would like to save that ERP object ID on the Vault Item.&#0160; But Vault won&#39;t let you, it’s released, which means it’s read only.&#0160; Your app needs to find a way to move the Item to WIP without bumping the revision.&#0160; Then, it needs a way to move back to Released without triggering the update again.&#0160; It’s possible, but it’s a pain.</p>
<p>Quick change fixes all of that.&#0160; Your app can safely move in and out of the Released state without worrying about a revision bump.&#0160; The permissions are configurable, so you can guarantee that nobody will mess with the Item while your app is editing it.</p>
<p>Even outside the context of custom programming, Quick Change is useful.&#0160; If you are a Vault admin and you notice a small detail that needs fixing, you can, um, quickly change it.&#0160; Files have had the Quick Change state for years now.&#0160; It’s still highly useful for Files, but I think it will be even more useful for the Item world.&#0160;</p>
<p>When you upgrade to Vault 2015 R2, Quick Change is automatically added.&#0160; If, for some strange reason, you don’t want it, you can always just delete it from the Vault settings dialog.&#0160; If you hear loud sobbing while you delete it, that’s just me.&#0160; I hate to see a good lifecycle state die.</p>
<p>I’ll leave you with an clip from the movie Quick Change.&#0160; That&#39;s right, the Quick Change state is so awesome that Hollywood made a movie about it 24 years before it was invented.&#0160; If you are a Bill Murray fan and have not seen the move, then you are not actually a Bill Murray fan.</p>
<p><iframe allowfullscreen="allowfullscreen" frameborder="0" height="315" src="//www.youtube.com/embed/LkipjCxzXUU" width="420"></iframe></p>
<hr noshade="noshade" style="color: #d09219;" />
