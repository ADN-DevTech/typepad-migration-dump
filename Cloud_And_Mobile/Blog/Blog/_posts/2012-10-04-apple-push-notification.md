---
layout: "post"
title: "Apple Push Notification"
date: "2012-10-04 11:48:55"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iOS"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2012/10/apple-push-notification.html "
typepad_basename: "apple-push-notification"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As I understand, if you want to receive notifications inside an iOS application even when it goes into the background or gets terminated, then the only way is via Apple Push Notification (APN). If you only need those notifications while your program is running then there seem to be other webservices that provide such functionality and enable you to get notified about certain events without having to keep polling the service.</p>
<p>Here is a good overview of this process on the Apple Developer site:<br /><a href="https://developer.apple.com/library/mac/#documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/ApplePushService/ApplePushService.html%23//apple_ref/doc/uid/TP40008194-CH100-SW9" target="_self">Apple Push Notification Service</a>&#0160;</p>
<p>In order to get started with APN and test this functionality you need three things:</p>
<ol>
<li>Enable your device for APN though the Apple Developer portal</li>
<li>Register your application for APN when it starts up</li>
<li>Send an APN message to your device&#0160;</li>
</ol>
<p><strong>Enable your device for APN though the Apple Developer portal</strong></p>
<p>There are quite a few articles that take you through the necessary steps. I found the following ones useful:<br /><a href="http://www.ibm.com/developerworks/java/library/mo-ios-push/" target="_self">http://www.ibm.com/developerworks/java/library/mo-ios-push/<br /></a><a href="http://42spikes.com/post/Sending-Apple-Push-Notifications-from-a-C-Application.aspx" target="_self">http://42spikes.com/post/Sending-Apple-Push-Notifications-from-a-C-Application.aspx</a></p>
<p><a href="http://42spikes.com/post/Sending-Apple-Push-Notifications-from-a-C-Application.aspx" target="_self"></a>When you are registering a new App ID on the iOS Povisioning Portal then you cannot use a wildcard character (*) in its name if you want to enable APN for it. <br />Because previously in all my applications I was using an App ID with a wildcard character in it, I did not realize that Xcode automatically adds the Bundle Seed ID to the project - i.e. I did not read this section on the iOS Provisioning Portal properly:<br />&quot;The Bundle Seed ID portion of your App ID does not need to be input into Xcode.&quot; &#0160;</p>
<p>Not only does it <strong>not need to be</strong> input in Xcode, it <strong>should not be</strong> input there, because in that case after Xcode automatically adds the <strong>Bundle Seed ID</strong> to your project&#39;s <strong>Bundle Identifier</strong> it will not match the exact <strong>App ID</strong> string that your provisioning profile is for and so you won&#39;t be able to compile your application with that provisioning profile.</p>
<p><strong>Register your application for APN when it starts up</strong></p>
<p>The Apple Developer site provides a good overview of the steps needed to achieve this:<br /><a href="https://developer.apple.com/library/mac/#documentation/NetworkingInternet/Conceptual/RemoteNotificationsPG/IPhoneOSClientImp/IPhoneOSClientImp.html#//apple_ref/doc/uid/TP40008194-CH103-SW1" target="_self">Registering for remote notifications</a> &#0160;&#0160;</p>
<p>When you get the message that you successfully registered for APN in your program, then you&#39;ll get the token that others need to use in order to send you an APN. This token only changes if  the user restores backup data to a new device or reinstalls the operating system.</p>
<p>You&#39;ll get this device token inside <span style="margin: 0px; font-size: 11px; font-family: Menlo;">didRegisterForRemoteNotificationsWithDeviceToken</span>, from where you could send it on to the application that wants to be able to send you notifications. For testing purposes you could simply get this token as a string when debugging your app on your iPad, and then copy/paste this string somewhere, so you could reuse it in the application that will send your iPad notifications:</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)application:(<span style="color: #703daa;">UIApplication</span> *)app didRegisterForRemoteNotificationsWithDeviceToken:(<span style="color: #703daa;">NSData</span> *)devToken</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160;&#0160;<span style="color: #703daa;">NSString</span> * str = [devToken <span style="color: #3d1d81;">description</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #000000;">&#0160;&#0160;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #000000;">&#0160;</span><span style="color: #008400;">&#0160; // now copy/paste its value somewhere for later use</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p>The first time your application will try to register for APN there will be a message popping up on the iPad where you&#39;ll have to enable this functionality by hand.&#0160;</p>
<p>Note: since the token is device specific, therefore the APN registration will fail when testing your iOS application on the simulator.</p>
<p><strong>Send an APN message to your device</strong></p>
<p>Now I want to send a message to my iPad from my .NET application. I&#39;ve found a nice project which makes creating these notifications quite easy: <a href="https://github.com/Redth/APNS-Sharp" target="_self">APNS-Sharp</a></p>
<p>This also has a .NET test project that I could use straight away. I just had to modify the <strong>testDeviceToken</strong>,&#0160;<strong>p12File</strong> and <strong>p12FilePassword</strong>&#0160;string variables in the program and it was ready to go. For&#0160;<strong>testDeviceToken</strong> I used the string that I previously saved to a text file. Note that you need to remove all spaces from it so that you&#39;ll end up with a 64 characters long string containing only letters and numbers. </p>
<p>If you followed the above mentioned articles that take you through the process of enabling your device for accepting APN&#39;s, then there you&#39;ve seen that you&#39;ll need to download the <strong>Development Push SSL Certificate</strong>. Once you install that on your Mac, then you&#39;ll be able to export it from <strong>Keychain Access</strong> as a <strong>p12</strong> file with a password of your choice. This is the password that <strong>p12FilePassword</strong>&#0160;needs to be set to.</p>
<p>I also changed the test project to only send a single notification. When you run the program, then in the console it provides information about the notification data that has been sent. If all goes well, then your iOS application will get the notification like so:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3251f7e0970b-pi" style="display: inline;"><img alt="APN" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017c3251f7e0970b image-full" src="/assets/image_23e129.jpg" title="APN" /></a></p>
<p>Depending on whether your application is running (inc. in background) or not, a different function of your iOS app will be called. If you get a notification while your application is running, the <span style="margin: 0px; font-size: 11px; font-family: Menlo;">didReceiveRemoteNotification</span> function will be called, and that&#39;s where you can get the message text from:</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)application:(<span style="color: #703daa;">UIApplication</span> *)application</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">didReceiveRemoteNotification:(<span style="color: #703daa;">NSDictionary</span> *)userInfo</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSString</span> * message = <span style="color: #bb2ca2;">nil</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSDictionary</span> * apsDict = [userInfo <span style="color: #3d1d81;">objectForKey</span>:<span style="color: #d12f1b;">@&quot;aps&quot;</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">id</span> alert = [apsDict <span style="color: #3d1d81;">objectForKey</span>:<span style="color: #d12f1b;">@&quot;alert&quot;</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">if</span> ([alert <span style="color: #3d1d81;">isKindOfClass</span>:[<span style="color: #703daa;">NSString</span> <span style="color: #3d1d81;">class</span>]]) {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; &#0160; message = alert;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; } <span style="color: #bb2ca2;">else</span> <span style="color: #bb2ca2;">if</span> ([alert <span style="color: #3d1d81;">isKindOfClass</span>:[<span style="color: #703daa;">NSDictionary</span> <span style="color: #3d1d81;">class</span>]]) {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; &#0160; message = [alert <span style="color: #3d1d81;">objectForKey</span>:<span style="color: #d12f1b;">@&quot;body&quot;</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p> Otherwise you&#39;ll have to do it from <span style="margin: 0px; font-size: 11px; font-family: Menlo;">didFinishLaunchingWithOptions</span>: &#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">BOOL</span>)application:(<span style="color: #703daa;">UIApplication</span> *)application didFinishLaunchingWithOptions:(<span style="color: #703daa;">NSDictionary</span> *)launchOptions</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// ...</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&#0160; </span>NSDictionary&#0160;<span style="color: #000000;">* userInfo = [launchOptions&#0160;<br /></span><span style="color: #3d1d81;">&#0160; &#0160; objectForKey</span><span style="color: #000000;">:</span>UIApplicationLaunchOptionsRemoteNotificationKey<span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSString</span> * message = <span style="color: #bb2ca2;">nil</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #703daa;">NSDictionary</span> * apsDict = [userInfo <span style="color: #3d1d81;">objectForKey</span>:<span style="color: #d12f1b;">@&quot;aps&quot;</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">id</span> alert = [apsDict <span style="color: #3d1d81;">objectForKey</span>:<span style="color: #d12f1b;">@&quot;alert&quot;</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; <span style="color: #bb2ca2;">if</span> ([alert <span style="color: #3d1d81;">isKindOfClass</span>:[<span style="color: #703daa;">NSString</span> <span style="color: #3d1d81;">class</span>]]) {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; &#0160; message = alert;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; } <span style="color: #bb2ca2;">else</span> <span style="color: #bb2ca2;">if</span> ([alert <span style="color: #3d1d81;">isKindOfClass</span>:[<span style="color: #703daa;">NSDictionary</span> <span style="color: #3d1d81;">class</span>]]) {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; &#0160; message = [alert <span style="color: #3d1d81;">objectForKey</span>:<span style="color: #d12f1b;">@&quot;body&quot;</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&#0160; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&#0160; </span>// You may want to clear the notification badge</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&#0160; application.</span>applicationIconBadgeNumber<span style="color: #000000;"> = </span><span style="color: #272ad8;">0</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&#0160;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;"><span style="color: #000000;">&#0160; </span>return<span style="color: #000000;"> </span>YES<span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p>One thing to note is that if a notification arrives when your application is not running in the foreground and the user either ignores the notification banner (when the <strong>Alert Style</strong> setting of the application is set to&#0160;<strong>Banners</strong>) or clicks <strong>Close</strong> in the notification dialog instead of <strong>Launch</strong> (when the <strong>Alert Style</strong> of the application is set to&#0160;<strong>Alerts</strong>) then the application will not know about the notification and will not be able to get its message text either.</p>
