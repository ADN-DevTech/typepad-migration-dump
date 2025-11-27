---
layout: "post"
title: "Space entered in a text box of a modeless dialog - causes command to run again (C#)"
date: "2012-06-08 08:25:59"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/space-entered-in-a-text-box-of-a-modeless-dialog-causes-command-to-run-again-c.html "
typepad_basename: "space-entered-in-a-text-box-of-a-modeless-dialog-causes-command-to-run-again-c"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>I have a custom command that displays a modeless dialog with a textbox. If I enter a space in the textbox the message invokes my custom command so the modeless dialog appears again.&#0160; Is there a way to block the key message being sent to the Inevntor message loop when the key is a space?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>To avoid this problem provide Inventor&#39;s mainframe window as the owner of the modeless dialog. This code snippet shows the idea:</p>
<p>In C#.Net</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">oDialog.Show(</span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> InventorMainFrame(m_inventorApplication.MainFrameHWND));</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas; color: #000000;">&#0160;</span></p>
<p style="margin: 0px;">And the definition for InventorMainFrame is as below:</p>
<p style="margin: 0px;">&#0160;</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">internal</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">class</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">InventorMainFrame</span></span><span style="color: #000000;"> : System.Windows.Forms.IWin32Window</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000; font-size: 10pt;">&#0160;&#0160;&#0160; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">public</span></span><span style="color: #000000;"> InventorMainFrame(</span><span><span style="color: #0000ff;">long</span></span><span style="color: #000000;"> hWnd) { m_hWnd = hWnd; }</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">public</span></span><span style="color: #000000;"> System.</span><span><span style="color: #2b91af;">IntPtr</span></span><span style="color: #000000;"> Handle { </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="font-size: 10pt;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span><span style="color: #0000ff;">get</span></span><span style="color: #000000;"> { </span><span><span style="color: #0000ff;">return</span></span><span style="color: #000000;"> (System.</span><span><span style="color: #2b91af;">IntPtr</span></span><span style="color: #000000;">)</span></span></span><span style="font-family: Consolas;"><span style="font-size: 10pt;"><span style="color: #000000;">m_hWnd; } }</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span style="font-size: 10pt;"><span><span style="color: #0000ff;">private</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">long</span></span><span style="color: #000000;"> m_hWnd;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="color: #000000; font-size: 10pt;">&#0160;&#0160;&#0160; }</span></span></p>
</div>
</div>
