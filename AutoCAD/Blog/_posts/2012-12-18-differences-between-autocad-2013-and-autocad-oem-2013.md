---
layout: "post"
title: "Differences between AutoCAD 2013 and AutoCAD OEM 2013"
date: "2012-12-18 11:51:43"
author: "Fenton Webb"
categories:
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/differences-between-autocad-2013-and-autocad-oem-2013.html "
typepad_basename: "differences-between-autocad-2013-and-autocad-oem-2013"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: small;">Differences in Features and Utilities</span></span></strong></p>
<a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD9"></a>
<p>AutoCAD OEM does not support the following features: </p>
<p><a name="UL_BC99BB8734D64A7A8E5CB5D439CB3F51"></a></p>
<ul>
<li>Autodesk Exchange Apps </li>
<li>InfoCenter </li>
<li>Welcome Screen </li>
<li>Customization and Drawing Sync </li>
<li>AutoCAD WS </li>
<li>Content Explorer </li>
<li>Inventor Fusion </li>
<li>Model Documentation </li>
<li>Batch Plotting utility </li>
<li>VBA for 64-bit version </li>
<li>Express Tools </li>
</ul>
<p><span style="font-size: small;"><strong><span style="text-decoration: underline;">Differences in Commands and System Variables</span></strong></span></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD7"></a>Most AutoCAD commands and system variables are available for use in your product, including the ETRANSMIT, RENDER, and DBCONNECT commands (see the bottom of this post for a full listing). You must bind any ObjectARX, AutoLISP<sup> ® </sup>, and .NET applications that are used by your AutoCAD OEM product, including those that define commands.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD6"></a>The interface to system variables and the ability to change them depends on what you want to “publish” to the end user. Some system variables are published by default, based on their inclusion in various dialog boxes. However, as the AutoCAD OEM product vendor, you determine whether to publish the commands that call the dialog boxes.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD5"></a>For example, by default the DIMSTYLE command provides a user interface for changing dimension variables. If your product doesn&#39;t support dimensioning, or if you wish to set all dimension variables internally in your product, you should not publish the DIMSTYLE command. </p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD4"></a>Any system variables that you want to be available to end users from the menu, toolbar, or keyboard must be listed as allowed commands.</p>
<p><strong><span style="text-decoration: underline;">Command Difference in AutoCAD OEM 2013</span></strong></p>
<p>The following AutoCAD OEM 2013 command is different from the AutoCAD 2013 command: </p>
<p><a name="WS73099CC142F48755-381799D411F6FB5AF15-6366"></a></p>
<dl><a name="WS73099CC142F487551DC3623711DA7B58889-150E"></a><dt><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DFC"></a>OPTIONS </dt><dd>
<p><a name="GUID-AC302CCE-63B8-4162-9924-F1C7948A514F"></a></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD2"></a>This configuration dialog box is simplified for AutoCAD OEM. A major difference is that the Profiles tab is not provided. </p>
</dd></dl><span style="display: none;">
<p><strong><span style="text-decoration: underline;">System Variable Difference in AutoCAD OEM 2013</span></strong></p>
<a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD1"></a></span>
<p>The following <span class="charspan-msgph">AutoCAD OEM 2013</span> system variable is different from the corresponding AutoCAD 2013 system variable: </p>
<span style="display: none;"><a name="WS73099CC142F48755-381799D411F6FB5AF15-6365"></a></span><dl><span style="display: none;"><a name="WS73099CC142F487551DC3623711DA7B58889-150D"></a></span><dt><span class="term"><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DFB"></a></span>REPORTERROR</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-DF7E4B19-1793-4648-8B9E-1CCE39A586D6"></a></span>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DD0"></a></span>If set to 1, an error report can be generated, but it is not sent to Autodesk. If set to 0, AutoCAD OEM defers to the operating system to take action during an unhandled exception. </p>
</div>
</dd></dl><span style="display: none;"><a name="WS1A9193826455F5FF-30857B3612671CDEC18-5794"></a></span><dl><span style="display: none;"><a name="WS1A9193826455F5FF-30857B3612671CDEC18-5793"></a></span><dt><span class="term"><span style="display: none;"><a name="WS1A9193826455F5FF-30857B3612671CDEC18-5792"></a></span>ACADLSPASDOC</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-C026A0C9-4771-4618-B25D-07689A1F63D8"></a></span>
<p><span style="display: none;"><a name="WS1A9193826455F5FF-30857B3612671CDEC18-5791"></a></span>It is listed in the MakeWizard and needs to be enabled to work. Its value is not persisted in the registry. It controls whether the aoem.lsp file is loaded into every drawing or just the first drawing opened in a session.</p>
</div>
</dd></dl><span style="display: none;">
<p><span style="text-decoration: underline;"><strong>Unsupported System Variables</strong></span></p>
<a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DCF"></a></span>
<p>An <span class="charspan-msgph">AutoCAD OEM</span> product can use most of the standard AutoCAD system variables; however, the following system variables are not supported:</p>
<span style="display: none;"><a name="WS1A9193826455F5FF18CB41610EC0A2E719-727C"></a></span>  
<ul>
<li>ACADVER </li>
<li>ACADPREFIX </li>
<li>LOGINNAME </li>
<li>MENUCNTL </li>
<li>POPUPS </li>
<li>SCREENBOXES </li>
<li>SCREENMODE </li>
</ul>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DCE"></a></span>Use <span class="code">(getenv &quot;Support&quot;)</span> from the Visual LISP<sup> <span class="charspan-emphasis">®</span> </sup>console to access the same information provided by ACADPREFIX. The argument to <span class="code">(getenv)</span> is case sensitive. For more information about <span class="code">(getenv)</span>, see the AutoLISP Reference.</p>
<p><span style="font-size: small;"><span style="text-decoration: underline;"><strong>Differences in the User Interfaces </strong></span></span></p>
<h3><span style="font-size: x-small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">AutoCAD OEM Command Line</span></span></span></h3>
<p>The AutoCAD OEM command line supports input from keyboard, script files, pasted text, and even the Windows SendMessage() API. Like the AutoCAD command line, the AutoCAD OEM command line also supports transparent commands that interrupt the command currently running. However, an AutoCAD OEM command or system variable works in an AutoCAD OEM stamped product only if the product is configured to support it.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DCB"></a>Because the AutoCAD OEM Make Wizard allows you to toggle support for individual commands, you can present to the user only those commands required by your product, and you can provide the documentation needed for only those commands.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DCA"></a>You can customize the command line interface to suit your product&#39;s needs. For example, to simplify navigation for the user, you may want to present only three of the eleven different options of the ZOOM command. You do this with the Redefine option in the AutoCAD OEM Make Wizard.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DC9"></a>To implement this ZOOM command redefinition, you could include the following example:</p>
<pre>(command &quot;UNDEFINE&quot; &quot;ZOOM&quot;)
(defun C:ZOOM ( / zinput )
   (initget 1 &quot;All Extents Previous&quot;)
   (setq zinput (getkword &quot;\nZoom to (All/Extents/Previous): &quot;))
   (command &quot;.ZOOM&quot; zinput)
   (princ)
)&#0160; </pre>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DC8"></a>For the ZOOM command, you would specify Redefine on the AutoCAD OEM Commands page of the AutoCAD OEM Make Wizard. When the user invokes the ZOOM command in your product, your redefined command is called.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DC7"></a>You can also turn off the command line in your product. If you do so, users will be able to invoke commands only from the menu or a toolbar.</p>
<h3><span style="font-size: x-small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">Text Window in AutoCAD OEM</span></span></span></h3>
<p>AutoCAD OEM lets you disable the AutoCAD text window.</p>
<p><strong><span style="text-decoration: underline;">AutoCAD OEM Menus</span></strong></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DC5"></a>With AutoCAD OEM, you can have multiple menus. AutoCAD OEM uses all AutoCAD menu sections except the Screen menu section. AutoCAD OEM also does not support LISP in menus. The only other limitations are those imposed by command and system variable changes for AutoCAD OEM (see Differences in Commands and System Variables above). The <em>AutoCAD Customization Guide</em> and <em>Command Reference</em> provide information about menu customization. With AutoCAD OEM you can also enable menu customization for the end user by supporting the CUI command.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DC4"></a>In a stamped AutoCAD OEM product, if you support the CUI command, the user can load an alternate menu. You can also support the CUILOAD and CUIUNLOAD commands so that users can manage partial menus. </p>
<h1><span style="font-size: x-small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">AutoCAD OEM Toolbars</span></span></span></h1>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DC3"></a>You can provide the CUI and QuickCUI commands so that users can customize toolbars. They launch the Customize User Interface dialog box, with which users can create, modify, or delete toolbars. When the CUI command is supported, end users can create new buttons, change button bitmaps, or modify the command that a button calls.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DC1"></a>AutoCAD OEM maintains toolbar state identically to AutoCAD. Changes to toolbars are written to the customization file, and the location and status of the toolbars are saved in the Windows system registry. Partial menu load information is also saved in the registry. AutoCAD OEM automatically reloads all visible toolbars.</p>
<p><span style="font-size: small;"><strong><span style="text-decoration: underline;">Differences in the Help Files</span></strong></span></p>
<p><span class="charspan-msgph">AutoCAD OEM</span> supports only HTMLHelp. AutoLISP applications can use the <span class="code">help</span> function to call the Help file. If an end user presses F1, the <em class="mild">&lt;program name&gt;.chm</em> file is called.</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DBE"></a></span>ObjectARX applications can use the <span class="code">acedHelp()</span> function to call a Help file. For more information, see the <em class="mild">ObjectARX Reference</em>.</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DBD"></a></span>To ensure that the appropriate Help topic is called when the user opens Help using the F1 key, the Help menu, the command line, or an <span class="charspan-msgph">AutoCAD OEM</span> dialog box, you must use the proper topic ID for that topic in the Help file.</p>
<p>See this <a href="http://adndevblog.typepad.com/autocad/2012/08/how-to-create-your-own-autocad-oem-help-chm-from-the-2013-html-source.html">blog entry</a> for information on how to create Help for OEM</p>
<h3><span style="font-size: small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">Differences in ObjectARX Interaction</span></span></span></h3>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DBC"></a>The following sections explain differences in the behavior of ObjectARX applications in the AutoCAD OEM development environment.</p>
<h3><span style="font-size: x-small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">Automatic Loading of ObjectARX Applications</span></span></span></h3>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DBB"></a>You can use the following methods to load an ObjectARX application into your AutoCAD OEM product:</p>
<p><a name="WS1A9193826455F5FF18CB41610EC0A2E719-7273"></a></p>
<ul>
<li>ARX command with the Load option </li>
<li><em>&lt;program name&gt;.rx</em> file containing a list of applications to load at startup </li>
<li>Demand loading by registry settings </li>
<li><em>aoem.lsp</em> file </li>
<li>Launch your product from a Windows command prompt using the /ld startup switch </li>
<li>Required option in the AutoCAD OEM Make Wizard </li>
</ul>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DBA"></a>The demand loading feature in AutoCAD OEM automatically attempts to load a non-memory-resident ObjectARX application when it is needed. Implement demand loading for applications that are not used in every session. For more information about demand loading, see “Demand Loading” in the <em>ObjectARX Developer&#39;s Guide</em>.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB9"></a>To load an ObjectARX application when starting your AutoCAD OEM product, you can use demand loading, or you can add the application to the <em>&lt;program name&gt;doc.lsp</em> file or to a <em>&lt;program name&gt;.rx</em> file. Sample autoloading code is provided in the <em>aoemdoc.lsp</em> file for you to use or to edit to suit your requirements. You may add new commands or delete the existing commands included in the example. The (autoload) and (autoxload) functions are defined in the <em>aoemdoc.lsp </em>file. See “Using AutoLISP Applications” in the AutoCAD <em>Customization Guide</em>.</p>
<p><a name="WS73099CC142F487551DC3623711DA7B58889-14FD"></a></p>
<p>Warning You must place startup commands within the S::STARTUP() section of <em>&lt;program name&gt;doc.lsp</em>. The S::STARTUP() function is used by AutoCAD OEM to store the functions that must be run before users can do anything but after the AutoCAD OEM engine is ready to accept commands. You cannot have a command in <em>&lt;program name&gt;doc.lsp</em> that is executed before the S::STARTUP() function is executed, because AutoCAD OEM will fail.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB8"></a>If you already have a startup AutoLISP file, you can create a <em>&lt;program name&gt;doc.lsp</em> file that has only an S::STARTUP function that loads the startup file. This way, you do not have to make substantial changes to the startup file.</p>
<h3><span style="font-size: x-small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">Disabling of Drawing Save</span></span></span></h3>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB7"></a>AutoCAD OEM provides the following ObjectARX function:</p>
<pre>void
acedDisableDbMod(&#0160;&#0160;&#0160;&#0160; Adesk::Boolean disable,&#0160;&#0160;&#0160;&#0160; Adesk::Boolean clearDBMOD);&#0160; </pre>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB6"></a>This function is available only in AutoCAD OEM. It lets you control the use of the DBMOD system variable, which indicates whether the drawing has been changed. You can use this function to reset DBMOD and to force the AutoCAD OEM engine to disregard DBMOD.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB5"></a>The first argument to this function is a Boolean, indicating whether the AutoCAD OEM engine will check the state of the DBMOD system variable. Setting it to Adesk::kFalse enables checking, and setting it to Adesk::kTrue disables checking.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB4"></a>The second argument to this function resets the value of DBMOD. Setting it to Adesk::kTrue resets DBMOD to zero, indicating that the drawing has not been modified. Setting it to Adesk::kFalse leaves DBMOD unchanged.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB3"></a>The primary use of this function is to keep the AutoCAD OEM engine from prompting end users to save drawings. You might want to do this when the drawing information is not important, such as in a demonstration program, or when the information has been saved in a format other than the standard AutoCAD drawing format. If the AutoCAD OEM product is a demonstration program and you are disabling the save features, you can disable DBMOD checking by calling acedDisableDbMod(Adesk::kTrue, Adesk::kFalse).</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB2"></a>If you are implementing custom save functions, check the DBMOD variable to see if there is information that needs to be saved, and save it in your desired format. You can then call acedDisableDbMod(Adesk::kFalse, Adesk::kTrue), which specifies to the AutoCAD OEM engine that all the necessary information has been saved.</p>
<p><a name="WS73099CC142F487551DC3623711DA7B58889-14FC"></a></p>
<p>Note: To use the acedDisableDbMod() function, you must declare it in your source code because the function does not appear in any of the AutoCAD OEM header files. You can supply any default values for the arguments, but the following declaration is recommended:</p>
<pre>void acedDisableDbMod(Adesk::Boolean disable, Adesk::Boolean clearDBMOD = Adesk::kTrue);</pre>
<h3><span style="font-size: small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">Differences in Visual LISP Interaction</span></span></span></h3>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB1"></a>AutoCAD OEM developers can use the Visual LISP IDE to develop AutoLISP applications that can be used within a stamped pure AutoCAD OEM product (aoem.exe). AutoCAD OEM developers have access to the full feature set of Visual LISP, with a few exceptions noted. Some restrictions have been placed on the use of AutoLISP applications by end users of AutoCAD OEM products.</p>
<h3><span style="font-size: x-small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">Visual LISP for Developers</span></span></span></h3>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DB0"></a>The Visual LISP IDE is available to AutoCAD OEM developers, with some exceptions.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DAF"></a>Specifically, although bound FAS files are supported in AutoCAD OEM, you cannot create or use VLX files (Visual LISP packed applications). Moreover, you cannot use Visual LISP functions related to ActiveX<sup> ® </sup>, including vl‑load-com, vlax-* and vlr-* functions.</p>
<h3><span style="font-size: x-small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">AutoLISP for Stamped Products</span></span></span></h3>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DAE"></a>The following restrictions on the use of AutoLISP applications apply to all stamped AutoCAD OEM products:</p>
<p><a name="WS1A9193826455F5FF18CB41610EC0A2E719-726E"></a></p>
<ul>
<li>Stamped AutoCAD OEM products allow loading of only compiled AutoLISP files that are bound to the specific AutoCAD OEM product. FAS and FSL files can be loaded, but VLX files cannot. </li>
<li>End users cannot enter AutoLISP expressions using the keyboard, menu, or script files. </li>
<li>Stamped AutoCAD OEM products cannot access the debug-enabled, run-time expression evaluator. </li>
</ul>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DAD"></a>The AutoCAD OEM Make Wizard can be configured to bind an AutoLISP file so it runs only with the specified AutoCAD OEM product. You can also use the <em>bindlisp</em> program for this purpose.</p>
<h3><span style="font-weight: bold; font-size: small;"><span style="text-decoration: underline;">Differences in Command Line Switches</span></span></h3>
<p>AutoCAD OEM supports the following command line switches for startup:</p>
<p><a name="WS1A9193826455F5FF18CB41610EC0A2E719-726C"></a></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DAB"></a></p>
<p>Command line switches supported by AutoCAD OEM</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DAA"></a></p>
<p>/c - Specifies the location in which AutoCAD OEM searches for the startup configuration file</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DA6"></a></p>
<p>/t - Creates a new drawing based on the specified template file (This switch is not supported when the Initial Drawing Mode feature is turned off.)</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DA4"></a></p>
<p>/nologo&#0160; - Starts AutoCAD OEM without displaying the splash screen</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DA2"></a></p>
<p>/v - Designates a particular view of the drawing for display at startup</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DA0"></a></p>
<p>/b - Starts AutoCAD OEM with a script file when the command line is enabled</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D9E"></a></p>
<p>/s - Starts AutoCAD OEM with a specific support directory path specified</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D9C"></a></p>
<p>/set - Opens AutoCAD OEM with the specified sheet set</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D9A"></a></p>
<p>/nossm - Suppresses the Sheet Set dialog box from being displayed at AutoCAD OEM startup when no sheet set is specified</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D98"></a></p>
<p>/ld - Loads a specified ObjectARX or ObjectDBX application</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D96"></a></p>
<p>/pl - Performs a background plot of the specified DSD file</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D94"></a>Normally, AutoCAD OEM places the configuration file (<em>&lt;program name&gt;&lt;release&gt;.cfg</em>) in the executable directory. You can use the /c switch to specify a different location for the file. For example, from Windows Explorer, you can create a new program shortcut and enter the following command:</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D93"></a>c:\Program Files\Autodesk\AutoCAD OEM 2013\aoem /c c:\tmp</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D92"></a>This causes the configuration file to be placed in the <em>c:\tmp</em> directory. The new configuration directory must exist and be writable.</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D91"></a>The /pl switch is the command line background plot switch. It accepts the name of a DSD (Drawing Set Descriptions) file, and publishes the specified file without displaying the AutoCAD OEM application window. You use the following format to specify the file name:</p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D90"></a>&lt;path&gt;\&lt;drawing set descriptions file&gt;.DSD </p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D8F"></a>The following switches are not supported in AutoCAD OEM:</p>
<p><a name="WS1A9193826455F5FF18CB41610EC0A2E719-726B"></a></p>
<ul>
<li>/r </li>
<li>/p </li>
</ul>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D8E"></a>For more information about command line switches, see the AutoCAD <em>User&#39;s Guide</em>.</p>
<p><span style="font-size: small;"><span style="font-weight: bold;"><span style="text-decoration: underline;">Difference Regarding Personalization</span></span></span></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D8D"></a>AutoCAD OEM does not support personalization and serialization of end-user program executables.</p>
<p><span style="font-size: small;"><strong><span style="text-decoration: underline;">AutoCAD OEM Native Commands and System Variables</span></strong></span></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D8C"></a>This section describes commands and system variables that are unique to AutoCAD OEM. These commands and system variables do not exist in AutoCAD.</p>
<h3><span style="font-weight: bold; font-size: x-small;"><span style="text-decoration: underline;">Commands Specific to AutoCAD OEM</span></span></h3>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D8B"></a>The following commands are specific to AutoCAD OEM:</p>
<p><a name="WS73099CC142F48755-381799D411F6FB5AF15-6364"></a></p>
<dl><a name="WS73099CC142F487551DC3623711DA7B58889-14F6"></a><dt><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DFA"></a>..SYSSTATUS </dt><dd>
<p><a name="GUID-4CBE7CF0-CF1A-4FF4-B608-A1AE6282186B"></a></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D8A"></a>Writes status and system information to a <em>&lt;program name&gt;.slg</em> file. This is useful for product support and troubleshooting. (The two dots at the beginning of ..SYSSTATUS are part of the command name.) You can add this command to your product by first adding any ObjectARX module to your product, and then, on the Your Module Settings page of the AutoCAD OEM Make Wizard, adding “.SYSSTATUS” to the ObjectARX module. Note that in this case, there is just one preceding period.</p>
<p><a name="WS73099CC142F487551DC3623711DA7B58889-14F5"></a></p>
</dd><dt><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF9"></a>LISTCOMMANDS </dt><dd>
<p><a name="GUID-B427DAB7-6974-4B23-A78B-05DBE0BE0DBF"></a></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D89"></a>Lists commands you have enabled in your product. Commands that are the same for global and local are shown as global, and prefixed with an underscore. (For more information about global and local commands, see <a href="mk:@MSITStore:C:%5CProgram%20Files%5CAutodesk%5CAutoCAD%20OEM%202013%5CHelp%5Coemdev.chm::/GUID-057EBC5C-D613-4D8C-AB8B-A5137B78D30B.htm#WS4B0506698C46277ACF40F5105C1C92E71-7FF2">AutoCAD OEM Commands Page</a> .) Built-in command names are described as “internal,” and non-built-in commands as “external.”Below is an example of such an output:</p>
<p>Command: <em>listcommands</em></p>
<pre>_+VIEW external
_+OPTIONS internal
_+DSETTINGS external
_+CUSTOMIZE internal
_+PUBLISH external
_+VPORTS internal
_+UCSMAN internal
_-HYPERLINK external
_-PUBLISH external
_-SHADERMODE external
_-RENDER external
_-TEXT internal</pre>
<p><a name="WS73099CC142F487551DC3623711DA7B58889-14F4"></a></p>
</dd><dt><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF8"></a>PKFSTGROUP (PICKFIRST GROUP) </dt><dd>
<p><a name="GUID-92B2C0EF-118E-476B-93DE-FCD30144A13D"></a></p>
<p><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D88"></a>Provides an alternate means of accessing the group feature. The group feature for AutoCAD OEM comes in two versions. The first is identical to the GROUP command in AutoCAD, where a dialog box is displayed and a user can select objects. The second version of the group feature allows end users to make a pickfirst selection set, and then issue the PKFSTGROUP command.</p>
</dd></dl>
<p><strong><span style="text-decoration: underline;">System Variables Specific to AutoCAD OEM</span></strong></p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D87"></a></span>The following system variables are specific to <span class="charspan-msgph">AutoCAD OEM</span>:</p>
<span style="display: none;"><a name="WS73099CC142F48755-381799D411F6FB5AF15-6363"></a></span><dl><span style="display: none;"><a name="WS73099CC142F487551DC3623711DA7B58889-14F3"></a></span><dt><span class="term"><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF7"></a></span>BANNER</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-AB0299FF-27DA-4033-A742-087D19D82A19"></a></span>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D86"></a></span>Read-only system variable that returns the text of the developer-specified banner. Each line is separated by a newline character (<span class="code">\n</span>), and multiple white-space characters are reduced to one space. This string is truncated at 500 -characters.</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D85"></a></span>Using BANNER, you can retrieve the banner lines from the <em class="mild">&lt;program name&gt;res2.dll</em> file, where <em class="mild">&lt;program name&gt;</em> represents the first four letters of your <span class="charspan-msgph">AutoCAD OEM</span> program&#39;s name.</p>
</div>
<span style="display: none;"><a name="WS73099CC142F487551DC3623711DA7B58889-14F2"></a></span></dd><dt><span class="term"><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF6"></a></span>CLIPBOARD</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-0AF57C46-D875-4FAE-B86E-391291624C1C"></a></span>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D84"></a></span>Read-only system variable that indicates the status of the Windows® Clipboard. Returns the sum of one or more of the following bit values:</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D83"></a></span>0 Nothing available</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D82"></a></span>1 ASCII text available</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D81"></a></span>4 AutoCAD format data available</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D80"></a></span>5 Windows metafile data available</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D7F"></a></span>You can use the CLIPBOARD system variable to enable or disable Clipboard commands on your menu. For example, the following DIESEL expression disables the Paste Special menu item when the Clipboard is empty:</p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D7E"></a></span><span class="code">[$(If,$(getvar, clipboard),,~)/Paste &amp;Special...]</span> </p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D7D"></a></span><span class="code">^C^C_pastespec</span> </p>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D7C"></a></span>For more information about DIESEL string functions and customizing menus, see the AutoCAD <em class="mild">Customization Guide.</em></p>
</div>
<span style="display: none;"><a name="WS73099CC142F487551DC3623711DA7B58889-14F1"></a></span></dd><dt><span class="term"><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF5"></a></span>EXEDIR</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-6DC79900-38FC-4106-AF36-12D3E3508259"></a></span>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D7B"></a></span>Executable directory.</p>
</div>
<span style="display: none;"><a name="WS73099CC142F487551DC3623711DA7B58889-14F0"></a></span></dd><dt><span class="term"><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF4"></a></span>PROGRAM</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-A83F8EC9-04AD-45D9-AA55-14751B3AA188"></a></span>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D7A"></a></span>Read-only hidden system variable that returns the program name.</p>
</div>
<span style="display: none;"><a name="WS73099CC142F487551DC3623711DA7B58889-14EF"></a></span></dd><dt><span class="term"><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF3"></a></span>PRODUCT</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-344A8A4C-C7B6-4104-90DF-8C5948B9C8EF"></a></span>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D79"></a></span>Read-only hidden system variable that returns the product name.</p>
</div>
<span style="display: none;"><a name="WS73099CC142F487551DC3623711DA7B58889-14EE"></a></span></dd><dt><span class="term"><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1DF2"></a></span>VERSION</span> </dt><dd>
<div class="definition"><span style="display: none;"><a name="GUID-001A36EB-3652-4E3B-95AF-AAF62C39BE62"></a></span>
<p><span style="display: none;"><a name="WSFACF1429558A55DE8821C21057FBEBC2B-1D78"></a></span>Read-only system variable that returns the version of the <span class="charspan-msgph">AutoCAD OEM</span> engine. Replaces ACADVER.</p>
</div>
</dd></dl>
<p><strong><span style="text-decoration: underline;"><span style="font-size: small;">Listing of available commands for AutoCAD OEM 2013</span></span></strong></p>
<ul>
<li><a href="http://adndevblog.typepad.com/autocad/2012/12/full-listing-of-available-commands-for-autocad-oem-2013.html">Full listing of available commands for AutoCAD OEM 2013 - A to D</a></li>
<li><a href="http://adndevblog.typepad.com/autocad/2012/12/full-listing-of-available-commands-for-autocad-oem-2013-e-to-l.html">Full listing of available commands for AutoCAD OEM 2013 - E to L</a></li>
<li><a href="http://adndevblog.typepad.com/autocad/2012/12/full-listing-of-available-commands-for-autocad-oem-2013-m-to-s.html">Full listing of available commands for AutoCAD OEM 2013 - M to S</a></li>
<li><a href="http://adndevblog.typepad.com/autocad/2012/12/full-listing-of-available-commands-for-autocad-oem-2013-t-to-z.html">Full listing of available commands for AutoCAD OEM 2013 - T to Z</a></li>
</ul>
<p><strong><span style="text-decoration: underline;">For further information regarding Autodesk OEM products</span></strong> </p>
<p>Please contact <a href="http://www.techsoft3d.com/our-products/autodesk-autocad-oem">TechSoft3d</a></p>
