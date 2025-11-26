---
layout: "post"
title: "Custom Ribbon of Navisworks part 1"
date: "2012-07-12 02:15:33"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-1.html "
typepad_basename: "custom-ribbon-of-navisworks-part-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>A plug-in type called <em>CommandHandlerPlugin</em> has been introduced in Navisworks 2012. It allows you to create a plug-in with multiple commands, organize them in a custom tab that will be added to the ribbon user interface of the main product. It provides more sophistication than the basic <em>AddInPlugin</em>.</p>
<p><strong>Overview</strong></p>
<p>This plug-in has the following structure:</p>
<p>- plug-in class</p>
<p>- ribbon interface file (xaml)</p>
<p>- name file for localization</p>
<p><span style="text-decoration: underline;">&#0160;</span></p>
<p>This <em>plug-in class</em> provides attributes to specify the basic properties, ribbon interface file, name file for localization, ribbon tabs and commands. Several functions are usually overridden to execute the command, update the command specified by the command Id, or toggle the visibility of the ribbon tab etc. The <em>ribbon interface file</em> uses <em>Extensible Application Markup Language </em>(<a href="http://en.wikipedia.org/wiki/Extensible_Application_Markup_Language">XAML</a>) to configure the ribbon layout. The <em>name file for localization</em> defines the localized text (display name, tooltip etc) for ribbon tabs and commands.</p>
<p><span style="text-decoration: underline;">&#0160;</span></p>
<p>To work with this plug-in, we first need to create a class dll. – In this article, C# is used as the programming language and the sample is named ADNRibbonDemo.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b0167686e3040970b"><a href="http://adndevblog.typepad.com/files/adnribbondemo-1.zip">Download ADNRibbonDemo</a></span></p>
<p><strong>Plug-In Class</strong></p>
<p>This class must be derived from <em>CommandHandlerPlugin</em>.</p>
<p><span style="text-decoration: underline;">Main plug-in attributes</span></p>
<p>Plugin: defines basic attributes such as name, developer ID etc.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">Plugin</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;ADNRibbonDemo&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&quot;ADSK&quot;</span><span style="line-height: 140%;">, DisplayName = </span><span style="line-height: 140%; color: #a31515;">&quot;ADNRibbonDemo&quot;</span><span style="line-height: 140%;">)]</span></p>
</div>
<p>Strings: identifies the *.name file which defines the localized text for ribbon (see section <strong>Name File for Localization)</strong>. e.g. <br /><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">Strings</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;ADNRibbonDemo.name&quot;</span><span style="line-height: 140%;">)]</span></p>
<p>RibbonLayout: identifies an xaml file that defines the layout of associated custom ribbon(see section <strong>Ribbon Interface File</strong>). e.g.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">RibbonLayout</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;ADNRibbonDemo.xaml&quot;</span><span style="line-height: 140%;">)]</span></p>
</div>
<p>RibbonTab: defines a ribbon tab. It must be accompanied by a RibbonLayout attribute in order for the tab to appear in the GUI. The attributes include:</p>
<ul>
<li>Id: unique in the plug-in, and must correspond to the tab Id used in the xaml layout file.</li>
<li>DisplayName: The text to display for this Ribbon tab</li>
<li>LoadForCanExecute: if True, the plug-in is fully loaded to ensure that CanExecuteRibbonTab method is called. The method can be used to make a ribbon tab contextual i.e. only visible when specified conditions are met. Otherwise the tab is visible by default.</li>
<li>CallCanExecute: determines the conditions in which CanExecuteRibbonTab should be called</li>
</ul>
<p>e.g.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">RibbonTab</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;ID_CustomTab_1&quot;</span><span style="line-height: 140%;">, DisplayName = </span><span style="line-height: 140%; color: #a31515;">&quot;Custom Tab 1 - non-localised&quot;</span><span style="line-height: 140%;">)]</span></p>
</div>
<ul>
<li>Command: defines a command that will perform an action within the application. The attributes include:</li>
<li>Id: unique in the plug-in and must correspond to the command Id used in the xaml layout file</li>
<li>DisplayName: The text to display for command</li>
<li>Icon: defines the standard image</li>
<li>LargeIcon: defines the large image used for the command </li>
<li>CanToggle: defines whether the button as it appears in the ribbon can toggle on and off.</li>
<li>ToolTip: text that will appear when the user hovers over the command</li>
<li>ExtendedToolTip: additional text that describes the purpose of the command</li>
<li>Shortcut: a keyboard shortcut that can be used to activate the command</li>
<li>CallCanExecute: determines the conditions in which CanExecuteCommand should be called. If the CanExecuteCommand is not called the default command state is disabled. </li>
<li>LoadForCanExecute: commands are enabled by default, but if this is False the plug-in will not be fully loaded until the first time a command is executed. If this is True the plug-in will be fully loaded at application startup in order to call the CanExecuteCommand method. </li>
</ul>
<p>e.g.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">Command</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;ID_Button_1&quot;</span><span style="line-height: 140%;">,Icon=</span><span style="line-height: 140%; color: #a31515;">&quot;One_16.ico&quot;</span><span style="line-height: 140%;">, LargeIcon = </span><span style="line-height: 140%; color: #a31515;">&quot;One_32.ico&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CanToggle = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">, DisplayName = </span><span style="line-height: 140%; color: #a31515;">&quot;Button 1 non-localized&quot;</span><span style="line-height: 140%;">)</span></p>
</div>
<p>Some attributes specified in the plug-in class can be overridden by the localized name file or ribbon interface file (xaml), such as display name, images, tooltip, extended tooltip.</p>
<p><span style="text-decoration: underline;">Main plug-in overridden functions</span></p>
<p>ExecuteCommand: Executes a command when a button is pressed. It identifies the command by the Id defined in the command attribute. e.g.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> ExecuteCommand(</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> commandId,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">params</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;">[] parameters)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">switch</span><span style="line-height: 140%;"> (commandId)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;ID_Button_1&quot;</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the flag is used to </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// set status of other commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_toEnableButton = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; !m_toEnableButton;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// dropdown commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;ID_Button_4&quot;</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;ID_Button_5&quot;</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MessageBox</span><span style="line-height: 140%;">.Show(</span><span style="line-height: 140%; color: #a31515;">&quot;he command with ID is clicked = &#39;&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; + commandId + </span><span style="line-height: 140%; color: #a31515;">&quot;&#39;&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>CanExecuteCommand: updates the command specified by the command Id. The argument <em>CommandState</em> indicates if the command is enabled, checked (if a toggle command) and visible. e.g.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">CommandState</span><span style="line-height: 140%;"> CanExecuteCommand(</span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> commandId)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">CommandState</span><span style="line-height: 140%;"> state = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">CommandState</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">switch</span><span style="line-height: 140%;"> (commandId)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;ID_Button_3&quot;</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// button3 is disabled/enabled by a flag</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; state.IsEnabled = m_toEnableButton;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">default</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// other commands are all visible and enabled</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; state.IsVisible = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; state.IsEnabled = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; state.IsChecked = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> state;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>CanExecuteRibbonTab: The return flag will tell if display the corresponding tab or not.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> CanExecuteRibbonTab(</span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> ribbonTabId)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The second ribbon tab is visible or not</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// by Button 2 toggles on/off</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ribbonTabId.Equals(</span><span style="line-height: 140%; color: #a31515;">&quot;ID_CustomTab_2&quot;</span><span style="line-height: 140%;">))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> m_toShowTab;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>In our sample, we define two tabs. Tab1 contains two panels (called Panel1 and Panel2). Panel1 has Button1 of large size. Panel2 has two buttons of small size (called Button2 Button3). Tab2 has one panel with two commands (called Button4, Button5), grouped as dropdown menu. Button1 enables/disables Button3. Button2 toggles the visibility of Tab2 to be on or off.</p>
<p>(To be continued)</p>
