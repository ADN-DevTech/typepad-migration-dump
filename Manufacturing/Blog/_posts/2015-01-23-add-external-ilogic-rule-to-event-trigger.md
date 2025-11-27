---
layout: "post"
title: "Add external iLogic Rule to Event Trigger"
date: "2015-01-23 09:41:56"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/01/add-external-ilogic-rule-to-event-trigger.html "
typepad_basename: "add-external-ilogic-rule-to-event-trigger"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>Let&#39;s say we have a drawing (e.g. <strong>MyDrawing.idw</strong>) that has the following <strong>iLogic</strong> <strong>Rules Event Triggers</strong> setup:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c6e55e970c-pi" style="display: inline;"><img alt="Rules1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c6e55e970c img-responsive" src="/assets/image_088422.jpg" title="Rules1" /></a></p>
<p>We would like to remove the <strong>MyDocumentRule</strong> document/internal Rule from the <strong>Before Save Document</strong> event and add <strong>MyExternalRule</strong> external Rule instead. For testing purposes the external rule ony does this:</p>
<pre>MsgBox(&quot;MyExternalRule got executed!&quot;)</pre>
<p>... and <strong>MyDocumentRule</strong> does this:</p>
<pre>MsgBox(&quot;MyDocumentRule got executed!&quot;)</pre>
<p>There is no <strong>API</strong>&#0160;to change the <strong>Event Triggers</strong> settings, however you can do it by modifying property sets used by <strong>iLogic</strong>&#0160;for storing the relevant information. My colleague, <strong>Mike Deck</strong>, looked into this and came up with the following solution.</p>
<p>You can create a template/seed document by hand that has the <strong>Event Triggers</strong> setup you need and then copy those settings into other documents programmatically. The attached&#0160;<strong>EventDrivenRulesCopy.iLogicVb</strong>&#0160;external Rule can do the copying for you -&#0160;<span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0c6f90e970c img-responsive"><a href="http://adndevblog.typepad.com/files/eventdrivenrulescopy.ilogicvb">Download EventDrivenRulesCopy</a></span></p>
<p>It only works on external rules. In fact, if the document has event-driven internal rules, this program will delete the <strong>Event Triggers</strong> for them. It does this for the sake of simplicity: it is copying the exact set of triggers from the source to the destination, without trying to merge them.</p>
<p><strong>Please make a backup of your destination documents before running this, or run it in a temporary project.</strong></p>
<p>This rule will copy the settings for event driven rules from a seed document to all the documents you select. The seed document must have been saved previously. The rule will copy only from <strong>part</strong> to <strong>part</strong>, from <strong>assembly</strong> to <strong>assembly</strong>, or from <strong>drawing</strong> to <strong>drawing</strong>.</p>
<p>To run it: <br />- Open the seed document. Ensure that this document has all the event-driven external rule links that you want. <br />- Run the rule. It brings up a file selection dialog. <br />- Multi-select the destination documents. Make sure you don’t select the seed document in this group.</p>
<p>Note that if the seed document has any event-driven rules that run internal rules, the rule will show an error message and not continue. This is because it is not set up to copy internal rules. It just copies the links to external rules.</p>
<p>Also note that if the destination documents already have some event-driven rules settings (with or without internal rules), this rule will overwrite them. It will replace them with the rule settings from the seed part. It is not smart enough to merge the two sets of rules. (It doesn’t delete the rules themselves: it only deletes the event trigger information.)</p>
<p>The rule can also be used to delete the event-driven rules in the destination documents. It copies the settings from the seed document. So if the seed document has no event-driven rules, it will make sure that the destination documents also have none.</p>
<p>Based on the above info I created a new drawing document called <strong>EventSettingsTemplate.idw</strong>, added <strong>EventDrivenRulesCopy.iLogicVb</strong>,<strong>&#0160;</strong>and set up the <strong>Event Triggers</strong>&#0160;like so:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c6ead6970c-pi" style="display: inline;"><img alt="Rules2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c6ead6970c img-responsive" src="/assets/image_59b2b4.jpg" title="Rules2" /></a></p>
<p>Once this document was saved and still active, I ran <strong>EventDrivenRulesCopy</strong>&#0160;external Rule and selected the drawing file I wanted to modify in the first place, <strong>MyDrawing.idw</strong>. After that I checked if the settings were copied OK:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c73d596e970b-pi" style="display: inline;"><img alt="Rules3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c73d596e970b img-responsive" src="/assets/image_c5c15b.jpg" title="Rules3" /></a></p>
<p>I also modied the document and saved it to see if all is working fine and I got the expected message box:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c73d5b66970b-pi" style="display: inline;"><img alt="Rules4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c73d5b66970b img-responsive" src="/assets/image_1e6bdb.jpg" title="Rules4" /></a></p>
<p>&#0160;</p>
