---
layout: "post"
title: "WPF Window inside an Inventor Add-In"
date: "2015-11-17 13:06:24"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/11/wpf-window-inside-an-inventor-add-in.html "
typepad_basename: "wpf-window-inside-an-inventor-add-in"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>The question could also be: how to use a <strong>WPF Window</strong> from a<strong> Class Library </strong>-<strong>&#0160;</strong>which is&#0160;not specific to <strong>Inventor</strong> programming. However, here is how you can do it.</p>
<p><strong>1)</strong> Create an <strong>Inventor Add-In</strong> - e.g. using the Inventor Add-In template from <strong>File</strong> &gt;&gt; <strong>New</strong> &gt;&gt; <strong>Project...</strong></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb089201c3970d-pi" style="display: inline;"><img alt="WPF1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb089201c3970d image-full img-responsive" src="/assets/image_b5aa80.jpg" title="WPF1" /></a></p>
<p><strong>2)</strong> Add a <strong>WPF</strong> <strong>User Control</strong>&#0160;via&#0160;<strong>Project</strong> &gt;&gt; <strong>Add New Item...</strong></p>
<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7eddc4c970b-pi" style="display: inline;"><img alt="WPF2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7eddc4c970b image-full img-responsive" src="/assets/image_b53570.jpg" title="WPF2" /></a><br /></strong></p>
<p><strong>3)</strong> Change the control to a <strong>Window</strong> in both the <strong>*.xaml</strong> and <strong>*.xaml.cs</strong> files &#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08920243970d-pi" style="display: inline;"><img alt="WPF3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08920243970d image-full img-responsive" src="/assets/image_5a6e17.jpg" title="WPF3" /></a></p>
<p><strong>4)</strong> Add a reference to <strong>System.Xaml</strong> .NET assembly through&#0160;<strong>Project</strong> &gt;&gt; <strong>Add Reference...</strong>&#0160;</p>
<p><strong>5)</strong> Modify the <strong>Window</strong> as you wish - add controls to it, etc</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d177a7a8970c-pi" style="display: inline;"><img alt="WPF4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d177a7a8970c image-full img-responsive" src="/assets/image_b33584.jpg" title="WPF4" /></a></p>
<p><strong>6)</strong> Add an <strong>Inventor</strong> command in the <strong>StandardAddInServer.Activate</strong> function that will show our dialog. If needed you can also use the&#0160;System.Windows.Interop.<strong>WindowInteropHelper</strong> for the reason mentioned here:&#0160;<a href="http://adndevblog.typepad.com/manufacturing/2012/06/space-entered-in-a-text-box-of-a-modeless-dialog-causes-command-to-run-again-c.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2012/06/space-entered-in-a-text-box-of-a-modeless-dialog-causes-command-to-run-again-c.html</a>&#0160;&#0160;</p>
<pre>private Inventor.ButtonDefinition m_btnDef;

public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
{
  // This method is called by Inventor when it loads the addin.
  // The AddInSiteObject provides access to the Inventor Application object.
  // The FirstTime flag indicates if the addin is loaded for the first time.

  // Initialize AddIn members.
  m_inventorApplication = addInSiteObject.Application;

  // TODO: Add ApplicationAddInServer.Activate implementation.
  // e.g. event initialization, command creation etc.
  var cmdMgr = m_inventorApplication.CommandManager;
  m_btnDef = cmdMgr.ControlDefinitions.AddButtonDefinition(
    &quot;ShowWpfDialog&quot;, &quot;ShowWpfDialog&quot;, CommandTypesEnum.kQueryOnlyCmdType);
  m_btnDef.OnExecute += ctrlDef_OnExecute;
  m_btnDef.AutoAddToGUI(); 
}

void ctrlDef_OnExecute(NameValueMap Context)
{
  var wpfWindow = new InvAddIn.MyWpfWindow();

  // Could be a good idea to set the owner for this window
  // especially if used as modeless
  var helper = new WindowInteropHelper(wpfWindow);
  helper.Owner = new IntPtr(m_inventorApplication.MainFrameHWND);

  wpfWindow.ShowDialog(); 
}</pre>
<p>The command in action</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d177a75d970c-pi" style="display: inline;"><img alt="WPF5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d177a75d970c image-full img-responsive" src="/assets/image_c3f1ef.jpg" title="WPF5" /></a></p>
<p>The source code of the sample project:&#0160;<br /><a href="https://github.com/adamenagy/Inventor-AddInWithWPF" target="_self" title="">https://github.com/adamenagy/Inventor-AddInWithWPF</a></p>
