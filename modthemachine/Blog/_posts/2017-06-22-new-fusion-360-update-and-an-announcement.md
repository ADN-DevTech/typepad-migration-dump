---
layout: "post"
title: "New Fusion 360 Update and an Announcement"
date: "2017-06-22 21:51:49"
author: "Adam Nagy"
categories:
  - "Announcements"
  - "Brian"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2017/06/new-fusion-360-update-and-an-announcement.html "
typepad_basename: "new-fusion-360-update-and-an-announcement"
typepad_status: "Publish"
---

<p>This week a new Fusion 360 update went out.&#0160; There are a few API related enhancements in this update and one big <span style="background-color: #ffff00;">announcement</span>.</p>
<h4><strong>Enhancements</strong></h4>
<ul>
<li><strong>User Specified Program Path </strong>- You can now specify the location where Fusion will look for existing scripts and add-ins and where new scripts and add-ins will be created. You do this using a new setting in the Preferences dialog, as shown below. The default path was deep and in a system folder. With this you can choose the location of your programs. This path is not saved as part of your cloud preferences but is a local preference. This allows you to have a different path on different computers.<br /><br /><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c904ee87970b-pi"><img alt="ScriptPath" border="0" height="180" src="/assets/image_560361.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border-width: 0px;" title="ScriptPath" width="570" /></a><br />A big benefit of being able to choose your script and add-in location is that this can be a folder that is synced to the cloud using any of the available file syncing services.<br />
<p>&#0160;</p>
</li>
<li>
<p><strong>Change in Adding Script or Add-In </strong>- A small change has been made to how you add a script or add-in to the list of known scripts and add-ins. The &quot;Scripts and Add-Ins&quot; dialog allows you to select a script or add-in anywhere on your computer and Fusion 360 will remember it after that. You do that by using the green &quot;+&quot; button on the &quot;Scripts and Add-In&quot; dialog, as shown below. This also helps to resolve a security issue on Mac where selecting just the file didn&#39;t provide access to the other files in the folder. Selecting a folder provides access to its entire contents.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c904ee8b970b-pi"><img alt="AddScript" border="0" height="212" src="/assets/image_330504.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border-width: 0px;" title="AddScript" width="290" /></a></p>
<p>Clicking the green plus will display a browse dialog where you can browse to and select the script or add-in. The change is that previously you had to pick the file that represented the script or add-in. This tended to be confusing because these files will have different file extensions depending on which language the script or add-in was written and it was possible that there could be multiple code files that the main file depended on. The change is that you no longer select the file but now choose the folder that contains all of the files of the script or add-in.</p>
<p>&#0160;</p>
</li>
<li><strong>Change to ModelParameter.createdBy property</strong> - The createdBy property of the ModelParameter object was changed in the previous update so that it will return the object that caused the parameter to be created. There are cases where the creating object isn&#39;t currently supported by the API so the createdBy property doesn&#39;t have anything to return. In the last update, when this occurs it results in an exception. This has been changed in this update to that the property returns null instead.</li>
<li><strong>Clearing a List</strong> - A problem was reported on the forum quite a while ago regarding being able to empty the list associated with a DropDownCommandInput. This has been resolved in this update with the addition of the clear method to the ListItems object.</li>
<li><strong>Additional Feature Support</strong> - Support for copying or cutting and pasting a body is now supported through the API. Basic query support it now also available for the offset face feature (which is created in the UI using the &quot;Press Pull&quot; command).</li>
<li><strong>External References</strong> - Additional functionality related to external references has been added to the API.</li>
</ul>
<p>The full list of changes can be found on the <a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-36B1FFB5-5291-4532-8F11-90E912769B34">What’s New page of the Fusion API help</a>.</p>
<p>&#0160;</p>
<h4><strong>Announcement</strong></h4>
<p>The Fusion 360 product team constantly monitors analytics data to get information about how Fusion 360 is being used to help make decisions about where to focus resources for future development. This information is also useful in identifying areas of Fusion 360 that have very little usage. In some cases these areas are retired in order to focus resources on the portions of the product that will benefit the most people. One area that has been identified as have very little usage is the JavaScript API interface. <span style="background-color: #ffff00;">With the upcoming July 24th update, Fusion 360 will be moving the JavaScript API interface into maintenance mode.</span></p>
<p>&quot;Maintenance mode&quot; in this case means that documentation for the JavaScript API will be removed from the online help and the capability to create new JavaScript scripts and add-ins will be removed from the Scripts and Add-Ins dialog. However, the actual JavaScript interface will continue to be delivered, but without any new enhancements, to allow any existing JavaScript scripts and add-ins to continue to run and be used and they will still appear in the Scripts and Add-Ins dialog so they can be run.</p>
<p>This was a difficult decision to make and we understand it is not good news if you&#39;ve been using the JavaScript API. It takes continued resources from the Fusion 360 team to create, test, document, and support any API and based on usage numbers there are very few users using JavaScript to program Fusion 360. So, the decision was made to move it into maintenance mode and concentrate our efforts on the Python and C++ interfaces.</p>
<p>As mentioned above, existing programs will continue to run as they do today. It&#39;s possible the JavaScript API will be retired and completely removed from Fusion 360 at some point in the future but that has not yet been determined. You&#39;re encouraged to re-write your existing programs in Python or C++. Please feel free to contact us at <a href="mailto:fusion.apps@autodesk.com&amp;subject=Fusion 360 JavaScript Retirement">fusion.apps@autodesk.com</a> with your concerns and comments.</p>
<p>-Brian</p>
