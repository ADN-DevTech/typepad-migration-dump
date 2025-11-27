---
layout: "post"
title: "Highlight New Feature for Custom Commands"
date: "2018-09-20 05:02:53"
author: "Sajith Subramanian"
categories:
  - "Inventor"
  - "Sajith Subramanian"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/09/highlight-new-feature-for-custom-commands.html "
typepad_basename: "highlight-new-feature-for-custom-commands"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p>
<p>Inventor 2019.1 introduced a new feature for highlighting new commands.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b25861200b-pi"><img width="261" height="365" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_247c53.jpg" border="0"></a></p>
<p>This feature can also be used to highlight custom commands using the Inventor API.</p>
<p>Below is a code snippet that explains the new API’s that have been added for this feature.</p>
<pre><p>public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)<br>&nbsp;&nbsp; {<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; m_inventorApplication = addInSiteObject.Application;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ControlDefinitions controlDefs = m_inventorApplication.CommandManager.ControlDefinitions;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; stdole.IPictureDisp img = PictureConverter.ImageToPictureDisp(Resources.bmp_img);<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; m_buttonDef = controlDefs.AddButtonDefinition("", "UIRibbonSampleApp",<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CommandTypesEnum.kNonShapeEditCmdType, m_clientID,null,null, img, img);<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; // API for fetching all available comparison versions<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>string[] allversions = m_inventorApplication.AvailableComparisonVersions;</strong></p>
<p>      // API for setting the version against which the command will be compared against.<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; // Value from m_inventorApplication.AvailableComparisonVersions can be used for this.<br>&nbsp;&nbsp;&nbsp; <strong>&nbsp; m_inventorApplication.ComparisonVersion = "2018.1"; </strong>// as shown in above image<br>&nbsp;&nbsp;&nbsp;&nbsp; </p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; // Version at which this command was introduced.<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; // Value from m_inventorApplication.AvailableComparisonVersions can be used for this.<br>&nbsp;&nbsp; <strong>&nbsp;&nbsp;&nbsp; m_buttonDef.IntroducedInVersion = "2019";<br>&nbsp;&nbsp; </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br>&nbsp;&nbsp;&nbsp;&nbsp; // Version which this command was last updated.<br>&nbsp;&nbsp;&nbsp;&nbsp; // Value from m_inventorApplication.AvailableComparisonVersions can be used for this.<br>&nbsp;&nbsp; <strong>&nbsp;&nbsp; m_buttonDef.LastUpdatedVersion = "2019";</strong></p>
<p>&nbsp;&nbsp;&nbsp;&nbsp; m_buttonDef.ToolTipText = "This is a sample application for testing the Highlight Feature";<br>&nbsp;&nbsp;&nbsp;&nbsp; m_buttonDef.ProgressiveToolTip.Title = "Highlight Feature";<br>&nbsp;&nbsp;&nbsp;&nbsp; m_buttonDef.ProgressiveToolTip.Description = "This is a sample application for testing the Highlight Feature";<br>&nbsp;&nbsp;&nbsp;&nbsp; m_buttonDef.ProgressiveToolTip.ExpandedDescription = "This is the expanded description of the ToolTip";
</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; m_buttonDef.OnExecute += M_buttonDef_OnExecute;<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;<br>&nbsp;&nbsp; </p><p>&nbsp;&nbsp;&nbsp;&nbsp; if (firstTime)<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CreateUserInterface();<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }<br>&nbsp;&nbsp;&nbsp; //refreshes the ribbon controls display when the comparison is set. <br>&nbsp; // The ribbon control will not display highlight badge, until RefreshRibbonForComparison is called<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong> m_inventorApplication.RefreshRibbonForComparison();</strong><br>&nbsp;&nbsp; }</p></pre>
<p>In the above code, both the <strong>IntroducedInVersion</strong> and <strong>LastUpdatedVersion </strong>properties are set to the latest, which means that this command is new and has not been available on previous versions of Inventor.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b25869200b-pi"><img width="589" height="257" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_0f273f.jpg" border="0"></a></p>
<p>In this case, the highlight badge would be displayed as <strong>‘NEW’.</strong></p>
<p>In the above code, if you change the <strong>IntroducedInVersion</strong> property to an earlier version, it would mean that an existing command has been updated.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad392b095200d-pi"><img width="593" height="284" title="image" style="display: inline; background-image: none;" alt="image" src="/assets/image_dc89f0.jpg" border="0"></a></p>
<p>In this case, the highlight badge would be displayed as <strong>‘UPDATED’</strong></p>
