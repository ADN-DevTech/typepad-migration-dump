---
layout: "post"
title: "Autodesk PLM 360 Sync"
date: "2012-05-08 21:36:22"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/05/autodesk-plm-360-sync.html "
typepad_basename: "autodesk-plm-360-sync"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p><strong>Update:</strong>&#0160; This utility is no longer available.</p>
<p>The first release of the Vault/PLM 360 integration has been released.&#0160; Just like with the <a href="http://justonesandzeros.typepad.com/blog/2011/07/autodesk-vault-for-sharepoint-2010.html">SharePoint 2010 integration</a>, I was involved in the design and implementation of this tool.&#0160; So I thought I would share my thoughts on the project and go over the technical details.</p>
<p>You can get PLM 360 Sync from the <a href="http://subscription.autodesk.com/">subscription center</a>.&#0160; PLM 360 Sync only works with Vault Professional 2013 since items are the only things synchronized in R1.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong><span style="font-size: medium;">Integration is in the eye of the beholder</span></strong></p>
<p>Everyone reading this post probably has a different idea on what a Vault/PLM 360 integration means.&#0160; For this reason, it’s hard to put out a single utility that is supposed to serve everyone’s needs.&#0160; We solved this problem by taking an incremental approach.&#0160; This first release is not meant to be everything to everyone.&#0160; It has a limited feature set which we will be building on in future releases.&#0160;</p>
<p>This incremental approach allows us to gather feedback.&#0160; Based on that feedback we will add new features, expand on what is working well and fix things that are not working well.</p>
<p>Currently, the <a href="http://forums.autodesk.com/t5/Autodesk-PLM-360/bd-p/705">PLM 360 discussion group</a> is the best place to give feedback.&#0160; Your suggestions will definitely get more eyeballs than comments on blog.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong><span style="font-size: medium;">The R1 Feature Set</span></strong></p>
<p><strong>Item Synchronization (one way)</strong> -&#0160; Released and obsolete items in Vault get replicated to PLM.&#0160;</p>
<p><strong>Seamless Integration</strong> - The idea is that the average Vault user doesn’t have to do anything special.&#0160; They just use Vault as normal, and all the integration stuff happens in the background.&#0160;</p>
<p><strong>Configurability</strong> - The Vault administrator has a lot of options regarding what and how data gets synchronized.</p>
<p><strong>Property Mapping</strong> - Any Vault item property (except thumbnails) can be mapped to a PLM field.&#0160;</p>
<p><strong>Revision History</strong> - When an item is synchronized, all revisions get synchronized.&#0160; So you can view the item history in PLM 360 just like you can in Vault.</p>
<p><strong>BOM</strong> - The entire BOM is synchronized, not just a single item.&#0160; Again, the result is an identical data in Vault and PLM 360.</p>
<p><strong>Item Selection</strong> - By default, all released and obsolete items get synchronized.&#0160; However, you can use the search mechanism to specify exactly what Vault items should be copied.</p>
<p><strong>Multiple Tenants\Workspaces</strong> - Your items don’t have to go to one and only one spot in PLM 360.&#0160; You can sync your items to multiple locations in PLM 360.&#0160; Each location can have its own criteria.&#0160; For example, all Vault items from project X can go to one spot in PLM 360 while items from project Y go to a different location.</p>
<p><strong>Bulk Load</strong> - If you are starting with a lot of Vault items, you can queue them up for synchronization with a click of the Rebuild History button.&#0160; This is also a good tool to have if something goes wrong and you are unsure of which items are replicated and which are not.</p>
<p><strong>Error Handling</strong> - If something goes wrong with a synchronization, for example a mapped property no longer exists in PLM 360, the result is an error job in the Vault Job queue.&#0160; Once the problem is fixed, the Vault admin can re-submit the job.</p>
<p><strong>Does Not Consume a Vault License</strong> - During regular operations, the sync tool does not use up a Vault license.&#0160; This feature is possible because the tool only reads from Vault.&#0160; If/when we add a 2-way sync feature, we may have to require a license at that point.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong><span style="font-size: medium;">Technical Details</span></strong></p>
<p>There are 2 main parts to PLM 360 Sync.&#0160; There is a Sync Service and there is a configuration command.&#0160; The configuration command is in Vault Explorer, and it’s what the administrator uses to determine which Vault items go to which workspaces in PLM 360.</p>
<p>The Sync Service is a Windows Service that does the actual work.&#0160; It reads data from Vault, connects to PLM 360, determines what needs to be updated and makes the changes to PLM 360.</p>
<p>Here is the basic order of operations:</p>
<ol>
<li>User releases an item in Vault. </li>
<li>Vault server adds a job to the job queue indicating that an item has changed.&#0160; This step does not happen immediately. </li>
<li>Sync Service reads the job off the job queue and performs the necessary updates in PLM 360.&#0160; This step also does not happen immediately. </li>
</ol>
<p>The Job Queue is pretty central to how PLM 360 Sync works.&#0160; The queue tells the Sync Service which items may need to be synchronized.&#0160; It also is a repository for errors that may happen.&#0160; It’s very important that there be a way to re-do a sync in the case of a failure.</p>
<p>Even though the job queue is used, Job Processor has nothing to do with PLM 360 Sync.&#0160; The Sync Service talks directly to the job queue, so Job Processor is not required.&#0160; <strong>Sync Service and Job Processor are completely orthogonal to each other.</strong>&#0160; The actions of one do not affect the actions of the other.&#0160; For example, if Job Processor is busy with 1000 DWF create jobs, the Sync Service runs as normal.&#0160; It does <strong><span style="text-decoration: underline;">NOT</span></strong> have to wait for the 1000 DWF jobs to complete.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong><span style="font-size: medium;">Additional Resources</span></strong></p>
<p><a href="http://underthehood-autodesk.typepad.com/blog/2012/04/autodesk-vault-and-plm-360-integration.html">Under the Hood</a> - Brian Schanen goes through the configuration steps.     <br /><a href="http://designandmotion.net/autodesk/autodesk-plm-360/plm-360-vault-professional-2013-plm-360-sync-tool-released/">Design and Motion</a> - Scott Moyse provides his insights.&#0160; <br /><a href="http://forums.autodesk.com/t5/Autodesk-PLM-360/bd-p/705">PLM 360 Discussion Group</a> - Give us your feedback.     <br /><a href="http://crackingthevault.typepad.com/crackingthevault/2012/05/vault-professional-2013-plm-360-extension-has-been-released.html">Cracking the Vault</a> - Where to get the tool.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
