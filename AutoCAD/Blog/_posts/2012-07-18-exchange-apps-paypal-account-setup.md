---
layout: "post"
title: "Exchange Apps &ndash; PayPal account setup"
date: "2012-07-18 10:58:31"
author: "Madhukar Moogala"
categories:
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/exchange-apps-paypal-account-setup.html "
typepad_basename: "exchange-apps-paypal-account-setup"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>This information applies only to Apps sold for a fee on Exchange Apps. It is important to check your settings are as described in Required PayPal Settings for Autodesk Exchange Apps below.</p>
<p><strong><span style="font-size: medium;">Recommended PayPal account types</span></strong></p>
<p>Publishers with fee-based Apps should have either a “PayPal Business Account” or a “Premier Account”. PayPal Premier accounts allow publishers to use their individual name. PayPal Business Accounts allow publishers to conduct business under a company or group name. (<a href="https://www.paypal.com/cgi-bin/webscr?cmd=xpt/Marketing/general/PayPalAccountTypes-outside">See PayPal account type comparison</a>). For Autodesk Exchange Apps PayPal accounts “PayPal Website Payment Standard” is sufficient ¾ this has no setup fees and can accept not only buyers with a PayPal account, but also anyone with a credit card even without a PayPal account.</p>
<p><strong>&#0160;</strong></p>
<h4><span style="font-size: medium;"><span style="font-weight: bold;">Required PayPal settings for Exchange apps</span></span></h4>
<p><strong>&#0160;</strong></p>
<p>Autodesk Exchange Apps uses PayPal as its payment vendor. In order for customers to be able to successfully purchase your app, you must change some settings in your PayPal account.</p>
<h4><span style="font-weight: bold;">PayPal Settings</span></h4>
<p>In your PayPal account, under My Account, click on the Profile link:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167688d784a970b-pi"><img alt="image003" border="0" height="30" src="/assets/image_194385.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image003" width="244" /></a></p>
<p><em><strong>Enable Auto Return for website payments. </strong></em></p>
<p>Click on <strong>Website Payment Preferences</strong> under <strong>Selling Preferences</strong>:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616825cf0970c-pi"><img alt="image004" border="0" height="237" src="/assets/image_955902.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image004" width="196" /></a></p>
<p><strong>Turn On</strong> Auto return and type <a href="http://apps.exchange.autodesk.com/Payment/Success">http://apps.exchange.autodesk.com/Payment/Success</a> as <strong>Return URL,</strong> like this:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616825cfb970c-pi"><img alt="image005" border="0" height="162" src="/assets/image_421514.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image005" width="244" /></a></p>
<p><strong>Save the changes</strong> clicking on the <strong>Save button</strong> at the bottom of the page.&#0160;</p>
<p><strong><em>Enable Instant Payment Notification (IPN)</em></strong></p>
<p>Click on <strong>Instant Payment Notification Preferences</strong> under <strong>Selling Preferences:</strong></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177436872cb970d-pi"><img alt="image006" border="0" height="230" src="/assets/image_26466.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image006" width="190" /></a></p>
<p><strong>&#0160;</strong></p>
<p>You will see the IPN preferences page and click on <strong>Choose IPN Settings</strong> <strong>button</strong>:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616825d0b970c-pi"><img alt="image007" border="0" height="150" src="/assets/image_967295.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image007" width="244" /></a></p>
<p><strong>Enable</strong> IPN messages selecting the <strong>Receive IPN messages radio button</strong> and type <a href="http://apps.exchange.autodesk.com/Payment/IPNHandler">http://apps.exchange.autodesk.com/Payment/IPNHandler</a> as the <strong>Notification URL</strong>, like this:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616825d10970c-pi"><img alt="image008" border="0" height="115" src="/assets/image_553945.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image008" width="244" /></a></p>
<p><strong>Save</strong> the changes by clicking on the <strong>Save button</strong>, and you will be redirect to IPN preferences page where you will see that IPN is now enabled:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167688d7886970b-pi"><img alt="image009" border="0" height="100" src="/assets/image_757958.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image009" width="244" /></a></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0177436872ed970d-pi"><img alt="image010" border="0" height="5" src="/assets/image_616241.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image010" width="244" /></a></p>
<p><span style="font-size: medium;"><strong>Checking for failed purchases</strong><strong></strong></span></p>
<p>If a customer reports to you that a purchase failed, look at your IPN History to get information about the transaction. On IPN history you can see all the IPN notification that were sent and their status.</p>
<p>To access the IPN History page, the publisher has to login to his account and under the menu <strong>history</strong> he/she is going to find the <strong>IPN History link</strong>:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167688d7893970b-pi"><img alt="image008" border="0" height="105" src="/assets/image_318515.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image008" width="244" /></a></p>
<p>In that page are all the IPN notifications sent and their state:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017616825d46970c-pi"><img alt="image009" border="0" height="207" src="/assets/image_608753.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: block; float: none; margin-left: auto; margin-right: auto; padding-top: 0px; border: 0px;" title="image009" width="244" /></a></p>
<p>The retrying notification status indicates that it wasn´t successful so PayPal will try several times until success. Clicking on the message id you can see more details about the transaction.</p>
<p>If a customer reports a failed PayPal transaction after you have correctly set your Auto-Return and IPN settings, please email mail <a href="mailto:AppSubmissions@autodesk.com">AppSubmissions@autodesk.com</a> to report the problem.</p>
