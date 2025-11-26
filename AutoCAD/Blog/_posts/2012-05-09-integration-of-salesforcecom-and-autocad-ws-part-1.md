---
layout: "post"
title: "Integration of Salesforce.com and AutoCAD WS - Part 1"
date: "2012-05-09 19:06:25"
author: "Daniel Du"
categories:
  - "AutoCAD"
  - "Cloud"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/integration-of-salesforcecom-and-autocad-ws-part-1.html "
typepad_basename: "integration-of-salesforcecom-and-autocad-ws-part-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html">Daniel Du</a></p>
<p>If you are member of Autodesk Developer Network, you probably have known that our case system has been migrated to <a href="http://www.salesforce.com/" target="_blank">Salesforce.com</a>(SFDC), which is a cloud based CRM system. Apart from SaaS(Software as a Service), Salesforce is also a PaaS(Platform as a Service) provider. Force.com is a cloud platform, which enables developers to create applications to extend Salesforce.com and add value to your cloud based system.&#0160; At the same time, <a href="http://www.autocadws.com/" target="_blank">AutoCAD WS</a> as a cloud based CAD system has gained good reputation among CAD users, it allows user to view/edit DWG drawing from a browser or even a mobile device for free, without downloading and installing the AutoCAD software.</p>
<p>So how about to connect the two clouds? I was assigned a task to investigate the possibilities of integrating Salesforce.com and AutoCAD WS. We have many DWG files as attachments in our SFDC based case system, What I am trying to do is, find a way to open the DWG attachment in AutoCAD WS directly, without downloading it to local disc.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bd768970c-pi"><img alt="image" border="0" height="118" src="/assets/image_790990.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="367" /></a></p>
<p>In following serials of posts, I will demonstrate how to do it, this is the first part - setup the force.com development environment.</p>
<p>&#0160;</p>
<p>Firstly we need to get a <a href="http://developer.force.com/join?d=70130000000EjHb">Free Developer Edition</a> at <a href="http://developer.force.com/" target="_blank">developer.force.com</a>.&#0160; Please click <a href="http://developer.force.com/join?d=70130000000EjHb" target="_blank">here</a> to create a developer account, you will be direct to a registration page like below, just follow the instruction, you can get involved very easily.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bd80b970c-pi"><img alt="image" border="0" height="455" src="/assets/image_609518.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="488" /></a></p>
<p>After registration, you will get a free <a href="http://wiki.apexdevnet.com/index.php/Developer_Edition">Developer Edition (DE) environment</a>, you can login DE environment from developer.force.com by clicking “DE LOGIN” from the top:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676659e1ca970b-pi"><img alt="image" border="0" height="65" src="/assets/image_60573.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="355" /></a></p>
<p>Once you log in, you will be directed to the first visual force page – “start here”. It looks like below:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bd99d970c-pi"><img alt="image" border="0" height="321" src="/assets/image_900036.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="516" /></a></p>
<p>We need to enable development mode, so that we can start our development work. To develop with force.com, you do not need any specific tools, just a browser is good enough.</p>
<ol>
<li><var>Click <strong>Your Name</strong></var> | <strong>Setup</strong> | <strong>My Personal Information</strong> | <strong>Personal Information</strong>, and click <strong>Edit</strong>. </li>
<li>Check <strong>Development Mode</strong> </li>
<li>You can also check <strong>Show View State in Development Mode</strong> to show “View State” tab </li>
<li>Click <strong>Save</strong> </li>
</ol>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01630565f9e3970d-pi"><img alt="image" border="0" height="206" src="/assets/image_282174.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="444" /></a></p>
<p>After doing this and returning back to the “Start Here” page, you will see the Visualforce Development Mode Tools show up on the bottom of the page, you may need to drag it up a little if it is too narrow.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01630565faff970d-pi"><img alt="image" border="0" height="267" src="/assets/image_683723.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="486" /></a></p>
<p>If you examine the script closer, you will notice that it is exactly the code the “start here” page. It is pretty complicated for a starter like me, so let’s create a simple one.</p>
<p>As always, let’s start from Hello World. Open a new tab on your browser and input following address: <a href="https://na12.salesforce.com/apex/helloworld_demo">https://na12.salesforce.com/apex/helloworld_demo</a>. Please notice that na12 is the instance I am working on, you may use a different one.&#0160; What I am trying to to is to access a visual force page, named as ”helloworld_demo”. Since it does not exit, you will get following error message:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676659e577970b-pi"><img alt="image" border="0" height="250" src="/assets/image_593056.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="477" /></a></p>
<p>We can create the page just by clicking the link in this page. You will get a very basic visual force page like below:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01630565fc78970d-pi"><img alt="image" border="0" height="338" src="/assets/image_798462.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="475" /></a></p>
<p>Now let’s do some coding work. This development tool even supports code intelligence, pretty good. <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bddd1970c-pi"><img alt="image" border="0" height="173" src="/assets/image_970002.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="469" /></a></p>
<p>I will just copy/paste following code to script window, it is to show the account information:</p>
<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">apex:page</span> <span class="attr">standardController</span><span class="kwrd">=&quot;Account&quot;</span><span class="kwrd">&gt;</span>
<span class="kwrd">  &lt;</span><span class="html">apex:pageBlock</span> <span class="attr">title</span><span class="kwrd">=&quot; Hello {!$User.FirstName}!&quot;</span><span class="kwrd">&gt;</span> 
    You are viewing the {!account.name} account. 
<span class="kwrd">  &lt;/</span><span class="html">apex:pageBlock</span><span class="kwrd">&gt;</span> 
<span class="kwrd">  &lt;</span><span class="html">apex:detail</span> <span class="kwrd">/&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">apex:page</span><span class="kwrd">&gt;</span></pre>

<p>When pressing Ctrl+S, the script will be saved and compiled, but it seems nothing happens, as we need to pass an account ID to this page.</p>
<p>To get an account, please click the “+” button and select “Account” from the page.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bdded970c-pi"><img alt="image" border="0" height="327" src="/assets/image_90724.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="474" /></a></p>
<p>The account list will show up as below, click one of them to get the Account ID, it look like : 001U0000002NWDT</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bde35970c-pi"><img alt="image" border="0" height="249" src="/assets/image_146608.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="470" /></a></p>
<p>Now change the URL to <a href="https://c.na12.visual.force.com/apex/helloworld_demo?id=001U0000002NWDT">https://c.na12.visual.force.com/apex/helloworld_demo?id=001U0000002NWDT</a>, please note that your instance and account id may be different with mine. Congratulations, your first visual force page is running!</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bde78970c-pi"><img alt="image" border="0" height="216" src="/assets/image_568670.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="472" /></a></p>
<p>OK if you examine the snippet closely, you will see some tags starting from &lt;apex:, they are visual force page tags. force.com will render the VFPage tags into HTML and send to browser. Here is the architecture of visual force system. Does it look like ASP.NET ? <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_419846.jpg" style="border-style: none;" />&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bdef2970c-pi"><img alt="image" border="0" height="252" src="/assets/image_828404.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="473" /></a></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb5bdf7d970c-pi"><img alt="image" border="0" height="241" src="/assets/image_500164.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="471" /></a></p>
<p>If you want to know more about force.com development, please refer to <a href="http://www.salesforce.com/us/developer/docs/pages/index_Left.htm">http://www.salesforce.com/us/developer/docs/pages/index_Left.htm</a></p>
<p>Did you follow me to setup the environment successfully? If you prefer an IDE at desktop, you can setup force.com IDE, which is an plug-in of Eclipse, I will introduce how to use it next time.</p>
<p>Stay tuned and have fun!</p>
