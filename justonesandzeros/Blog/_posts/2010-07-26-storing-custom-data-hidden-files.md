---
layout: "post"
title: "Storing Custom Data: Hidden Files"
date: "2010-07-26 08:14:10"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/07/storing-custom-data-hidden-files.html "
typepad_basename: "storing-custom-data-hidden-files"
typepad_status: "Publish"
---

<p><img src="/assets/TipsAndTricks2.png" /> </p> <p>Writing <a href="http://justonesandzeros.typepad.com/blog/2010/06/the-recycle-bin-20.html" target="_blank">Recycle Bin 2.0</a> allowed be to get up close and personal with the various issues that come with storing your data as a Vault file.&#0160; Like with my other articles in the <a href="http://justonesandzeros.typepad.com/sitemap.html#StoringCustomData" target="_blank">Storing Custom Data series</a>, there are advantages and disadvantages to this technique.&#0160; It&#39;s up to you to decide what is best for your needs.</p> <p><strong>Hidden Files</strong>   <br />The idea here is that you take your data, serialize it to a file, and check it into Vault.&#0160; The file should be hidden so that it doesn&#39;t confuse end users.&#0160; This technique isn&#39;t fool-proof however, users can adjust their settings so that they can see hidden files.&#0160; So you might also want to set special security on the file so that it can only be edited by your program.</p> <p><strong>Example</strong>   <br />For Recycle Bin 2.0, I needed a way to remember where a file came from.&#0160; That way it can easily move the file back to it&#39;s original location.&#0160; I decided to create a file called RecycleBinManifest.xml to store this information (and other stuff).</p> <p>Just about every Recycle Bin command involves checking this file out, editing it and checking it back in.</p> <p><strong>Analysis</strong>   <br />This technique is probably not a good fit for something like Recycle Bin.&#0160; Hidden files work much better if the data isn&#39;t likely to change.&#0160; But if you need to constantly change the data, you run into problems:</p> <ul>
  <li>Multi-user issues.&#0160; Only 1 person can run a command at a time, since only 1 person can check out the file at a time.&#0160; If you want to guarantee only 1 user at a time, this is a good thing.&#0160; But for most commands, you want to allow multi-user workflows. </li>
  <li>Error handling.&#0160; If something goes wrong, the file might get stuck in a checked out state.&#0160; This will effectively prevent anyone else from running the command until the the checkout is undone.&#0160; In Recycle Bin&#39;s case, the undo checkout has to be done manually by an administrator.</li>
  <li>Replication.&#0160; If you are running in a connected workgroup environment, your site needs ownership of the file in order to check it out.&#0160; Recycle Bin gets around this by logging in to the workgroup that owns the file.&#0160; The downside is that operations are slower on other sites.</li>
  <li>Historical versions.&#0160; Every edit results in a new version of the file.&#0160; This means that you constantly need to purge old versions to avoid needless clutter.&#0160; On the plus side, there may be cases where you want to go back a version or two. </li>
 </ul>
