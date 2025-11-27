---
layout: "post"
title: "Get Ribbon images"
date: "2014-07-28 06:41:59"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/get-ribbon-images.html "
typepad_basename: "get-ribbon-images"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>As shown in a <a href="http://adndevblog.typepad.com/manufacturing/2014/01/modify-ribbon-and-menu-items.html" target="_self">previous post</a>, the <strong>Ribbon API</strong> provided in <strong>Autodesk.Windows</strong> namespace by <strong>AdWindows.dll</strong> is not product specific. It provides an additional way of accessing things from the <strong>Ribbon</strong> that the product API might not have exposed.</p>
<p>In the <strong>Inventor API</strong> you can get back the images from the <strong>ControlDefinition</strong> objects, however, some of the Ribbon controls are not based on a <strong>ContolDefinition</strong>, or even if they are, they might not provide a valid icon. Here is some <strong>VBA</strong> code for testing:</p>
<pre>Sub RibbonControls()
  Dim r As Ribbon
  Set r = ThisApplication.UserInterfaceManager.Ribbons(&quot;Part&quot;)
  
  Dim rt As RibbonTab
  For Each rt In r.RibbonTabs
    Dim rp As RibbonPanel
    For Each rp In rt.RibbonPanels
      Dim cc As CommandControl
      For Each cc In rp.CommandControls
        If cc.ControlDefinition Is Nothing Then
          Debug.Print &quot;No ControlDefinition for &quot; + _
            cc.InternalName + &quot;, ControlType = &quot; + _
            str(cc.ControlType)
        Else
          &#39; The icon properties might throw an error
          On Error Resume Next
          Dim icon As IPictureDisp
          Set icon = cc.ControlDefinition.StandardIcon
          If Err &lt;&gt; 0 Then
            Debug.Print &quot;No StandardIcon for &quot; + _
              cc.InternalName + &quot;, ControlType = &quot; + _
              str(cc.ControlType)
          End If
          On Error GoTo 0
        End If
      Next
    Next
  Next
End Sub</pre>
<p>It seems using the <strong>Ribbon API </strong>you can get back all the images. If you have a <strong>C#</strong> Inventor AddIn then you can add a <strong>Form</strong> to it (id: <strong>MyForm</strong>) and a ListView (id: <strong>ltvImages</strong>) on top of it along with two buttons with id <strong>btnSmall</strong> and <strong>btnLarge</strong>, and provide the following code for the Form:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.Drawing;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.Windows.Forms;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.Windows.Media.Imaging;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.Windows.Interop;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.Windows;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">namespace</span> SimpleAddIn</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">partial</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">MyForm</span> : <span style="color: #2b91af;">Form</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">public</span> MyForm()</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; InitializeComponent();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">private</span> <span style="color: #2b91af;">Bitmap</span> BitmapImage2Bitmap(<span style="color: #2b91af;">BitmapSource</span> bitmapImage)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">using</span> (<span style="color: #2b91af;">MemoryStream</span> outStream = <span style="color: blue;">new</span> <span style="color: #2b91af;">MemoryStream</span>())</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">BitmapEncoder</span> enc = <span style="color: blue;">new</span> <span style="color: #2b91af;">BmpBitmapEncoder</span>();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; enc.Frames.Add(<span style="color: #2b91af;">BitmapFrame</span>.Create(bitmapImage));</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; enc.Save(outStream);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; System.Drawing.<span style="color: #2b91af;">Bitmap</span> bitmap = <br /><span style="color: blue;">&#0160; &#0160; &#0160; &#0160; &#0160; new</span> System.Drawing.<span style="color: #2b91af;">Bitmap</span>(outStream);</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">return</span> <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(bitmap);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> AddItem(Autodesk.Windows.<span style="color: #2b91af;">RibbonItem</span> i)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: #2b91af;">Bitmap</span> bmp;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (i.Image != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; bmp = BitmapImage2Bitmap((<span style="color: #2b91af;">BitmapSource</span>)i.Image);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.SmallImageList.Images.Add(bmp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; bmp = BitmapImage2Bitmap((<span style="color: #2b91af;">BitmapSource</span>)i.LargeImage);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.LargeImageList.Images.Add(bmp);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.Items.Add(i.Id, ltvImages.Items.Count);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (i <span style="color: blue;">is</span> Autodesk.Windows.<span style="color: #2b91af;">RibbonListButton</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; Autodesk.Windows.<span style="color: #2b91af;">RibbonListButton</span> li =</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; (Autodesk.Windows.<span style="color: #2b91af;">RibbonListButton</span>)i;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (Autodesk.Windows.<span style="color: #2b91af;">RibbonItem</span> si <span style="color: blue;">in</span> li.Items)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; AddItem(si);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ShowIcons(<span style="color: #2b91af;">Boolean</span> small)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; <span style="color: blue;">if</span> (ltvImages.SmallImageList == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.Items.Clear();</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.SmallImageList = <span style="color: blue;">new</span> <span style="color: #2b91af;">ImageList</span>();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.SmallImageList.ImageSize = <br /><span style="color: blue;">&#0160; &#0160; &#0160; &#0160; &#0160; new</span> System.Drawing.<span style="color: #2b91af;">Size</span>(16, 16);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.SmallImageList.TransparentColor = <span style="color: #2b91af;">Color</span>.Black;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.LargeImageList = <span style="color: blue;">new</span> <span style="color: #2b91af;">ImageList</span>();</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.LargeImageList.ImageSize = <br /><span style="color: blue;">&#0160; &#0160; &#0160; &#0160; &#0160; new</span> System.Drawing.<span style="color: #2b91af;">Size</span>(32, 32);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; ltvImages.LargeImageList.TransparentColor = <span style="color: #2b91af;">Color</span>.Black;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; Autodesk.Windows.<span style="color: #2b91af;">RibbonControl</span> r = <br />&#0160; &#0160; &#0160; &#0160; &#0160; Autodesk.Windows.<span style="color: #2b91af;">ComponentManager</span>.Ribbon;</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (Autodesk.Windows.<span style="color: #2b91af;">RibbonTab</span> t <span style="color: blue;">in</span> r.Tabs)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (Autodesk.Windows.<span style="color: #2b91af;">RibbonPanel</span> p <span style="color: blue;">in</span> t.Panels)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">foreach</span> (Autodesk.Windows.<span style="color: #2b91af;">RibbonItem</span> i <span style="color: blue;">in</span> p.Source.Items)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; AddItem(i);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ltvImages.View = small ? <span style="color: #2b91af;">View</span>.SmallIcon : <span style="color: #2b91af;">View</span>.LargeIcon;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">private</span> <span style="color: blue;">void</span> btnSmall_Click(<span style="color: blue;">object</span> sender, <span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ShowIcons(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160;</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; <span style="color: blue;">private</span> <span style="color: blue;">void</span> btnLarge_Click(<span style="color: blue;">object</span> sender, <span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; {</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; &#0160; ShowIcons(<span style="color: blue;">false</span>);</p>
<p style="margin: 0px; line-height: 120%;">&#0160; &#0160; }</p>
<p style="margin: 0px; line-height: 120%;">&#0160; }</p>
<p style="margin: 0px; line-height: 120%;">}&#0160;</p>
</div>
<p>Now you can show this form from the event handler of any of the commands you registered inside Inventor, e.g.:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: #2b91af;">MyForm</span> myForm;</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px; line-height: 120%;"><span style="color: blue;">override</span> <span style="color: blue;">protected</span> <span style="color: blue;">void</span> ButtonDefinition_OnExecute(<br /><span style="color: #2b91af;">&#0160; NameValueMap</span> context)</p>
<p style="margin: 0px; line-height: 120%;">{</p>
<p style="margin: 0px; line-height: 120%;"><span style="line-height: 120%; font-size: 8pt;">&#0160; myForm = </span><span style="color: blue;">new&#0160;</span><span style="color: #2b91af;">MyForm</span><span style="line-height: 120%; font-size: 8pt;">();</span></p>
<p style="margin: 0px; line-height: 120%;">&#0160; myForm.Show();</p>
<p style="margin: 0px; line-height: 120%;">}</p>
</div>
<p>When the command is run and the &quot;<strong>Small Images</strong>&quot; button is clicked we&#39;ll get this:</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511ea66ab970c-pi" style="display: inline;"><img alt="Ribbonimages" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511ea66ab970c image-full img-responsive" src="/assets/image_6a0b5c.jpg" title="Ribbonimages" /></a></p>
<p>&#0160;</p>
