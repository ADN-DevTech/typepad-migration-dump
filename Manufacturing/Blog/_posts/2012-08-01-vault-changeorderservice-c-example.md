---
layout: "post"
title: "Vault - ChangeOrderService C# Example"
date: "2012-08-01 17:39:49"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/vault-changeorderservice-c-example.html "
typepad_basename: "vault-changeorderservice-c-example"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>This zip contains a VS 2010 C# project. (Tested with Vault Pro 2013)</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b016768fd1cde970b"><a href="http://adndevblog.typepad.com/files/changeordersample2013.zip">Download Changeordersample2013</a></span></p>
<p>When you run the program a form is displayed:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016768fd194d970b-pi"><img alt="image" border="0" height="263" src="/assets/image_e37003.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="472" /></a></p>
<p>What this sample demonstrates is a way to do the following:</p>
<ul>
<li>Create a new Change Order </li>
<li>Submit an activity for a Change Order </li>
<li>Update a Change Order </li>
</ul>
<p>When the form is started it instantiates a class named PSHelper. In the constructor for this class global variables for ItemService, ChangeOrderService and WebServiceManager are created. These objects are used in the functions that are called when you click one of the three buttons.</p>
<p>Use the top text box to input a value of a property of one of the Items in the Vault that you are connecting to. The second textbox is where you input the number of the Change Order. Keep in mind that once a change order is created you will need to change the number. Also once an Item is referenced to a Change Order using the item again will cause an error. (1612 “The item is being managed by another change order”). You may need to update Vault Explorer to see the effect.&#0160; (I just hit F5)</p>
<p>Note: This example uses localhost as the server address and you may need to change it for your environment. You can see this in the PSHelper constructor.</p>
