---
layout: "post"
title: "Add Custom Panel and Button to Built-in Tab of Navisworks Ribbon"
date: "2016-01-17 23:05:54"
author: "Xiaodong Liang"
categories:
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2016/01/add-custom-panel-and-button-to-built-in-tab-of-navisworks-ribbon.html "
typepad_basename: "add-custom-panel-and-button-to-built-in-tab-of-navisworks-ribbon"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>As we have known, Naivisworks Ribbon API allows developers to add custom Ribbon (Tab) . These are some articles:</p>
<p><a href="http://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-1.html">http://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-1.html</a></p>
<p><a href="http://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-2.html">http://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-2.html</a></p>
<p><a href="http://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-3.html">http://adndevblog.typepad.com/aec/2012/07/custom-ribbon-of-navisworks-part-3.html</a></p>
<p>While sometimes, the requirement is to add button/panel to the built-in tabs. Actually, in&#0160; <strong>Navisworks </strong>the same library of Ribbon is used: <strong>AdWindows.dll </strong>which provides some underlying API to get access to that notification under the namespace <strong>Autodesk.Windows</strong>. We have had some articles for such as Inventor, Revit</p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2013/02/intercept-click-event-on-a-ribbon-tab.html">http://adndevblog.typepad.com/manufacturing/2013/02/intercept-click-event-on-a-ribbon-tab.html</a></p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2014/01/modify-ribbon-and-menu-items.html">http://adndevblog.typepad.com/manufacturing/2014/01/modify-ribbon-and-menu-items.html</a></p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2013/02/intercept-click-event-on-a-ribbon-tab.html">http://adndevblog.typepad.com/manufacturing/2013/02/intercept-click-event-on-a-ribbon-tab.html</a></p>
<p>Note: AdWindows APIs are not a section of the official APIs, though you can access it at your own risk.</p>
<p>Based on the ideas, I made some codes in a custom plugin as below. It will add a new panel to the built-in tab [Home], and a button named [ADN Test]. When the tab is activated, a message box will appear. The same when the button is clicked.</p>
<p>The resource codes is available at <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d1932fa0970c img-responsive"><a href="http://adndevblog.typepad.com/files/adntestbuiltinribbon.zip">Download ADNTestBuiltInRibbon</a></span>. It is tested in Navisworks 2016. The only issue I hit is sometimes the image does not appear. I will update this blog when I figured out.</p>

<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>

<pre class="csharp prettyprint">using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

//Add two new namespaces
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;

using Autodesk.Windows;

namespace BasicPlugIn
{
   [PluginAttribute(&quot;BasicPlugIn.ABasicPlugin&quot;,                   //Plugin name
                    &quot;ADSK&quot;,                                       //4 character Developer ID or GUID
                    ToolTip = &quot;BasicPlugIn.ABasicPlugin tool tip&quot;,//The tooltip for the item in the ribbon
                    DisplayName = &quot;Hello World Plugin&quot;)]          //Display name for the Plugin in the Ribbon

   public class ABasicPlugin : AddInPlugin                        //Derives from AddInPlugin
   {  
       // when the tab is activated
       void Tab_Activated(object sender, EventArgs e)
       {
           System.Windows.Forms.MessageBox.Show(
               &quot;Tab &quot; + ComponentManager.Ribbon.ActiveTab.Id + &quot; Activated!&quot;);
       }

       //delegate the event when ribbon element is activated
       void ComponentManager_UIElementActivated(object sender,UIElementActivatedEventArgs e)
       {
           if (e != null
             &amp;&amp; e.Item != null
             &amp;&amp; e.Item.Id != null
             &amp;&amp; e.Item.Id == &quot;ID_ADNTestButton&quot;)
           {
               System.Windows.Forms.MessageBox.Show(e.Item.Id + &quot; is clicked!&quot;);
           }
       }


      public override int Execute(params string[] parameters)
      { 

          foreach (Autodesk.Windows.RibbonTab Tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
          {
              //get Home tab
              if (Tab.Id == &quot;ID_TabHome&quot;)
              {
                  Tab.Activated += new EventHandler(Tab_Activated);

                  RibbonPanel ADNPanel = null;  
                  //check if the custom panel exsits
                  foreach (RibbonPanel panel in Tab.Panels)
                  {
                      if (panel.Source.Name == &quot;ID_ADNTestPanel&quot;)
                      {
                          ADNPanel = panel;
                          break;
                      }
                  }

                  if (ADNPanel == null)
                  {                     
                      //create custom Panel
                      ADNPanel = new RibbonPanel();
                      //create ribbon panel source and bind it to the panel
                      RibbonPanelSource ADNSource = new RibbonPanelSource();
                      ADNSource.Id = &quot;ID_ADNTestPanel&quot;;
                      ADNSource.Name = &quot;ADN Test Panel&quot;;
                      ADNPanel.Source = ADNSource;

                      //create ribbon button
                      RibbonButton button  = new RibbonButton();
                      button.IsEnabled = true;
                      button.IsVisible = true;
                      button.Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(@&quot;Images/ListAdd.png&quot;, UriKind.RelativeOrAbsolute));
                      button.LargeImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(@&quot;Images/open.png&quot;, UriKind.RelativeOrAbsolute));
                      button.ShowImage = true;
                      button.Size = RibbonItemSize.Standard;
                      button.ShowText = true;
                      button.ResizeStyle = RibbonItemResizeStyles.HideText;
                      button.Id = &quot;ID_ADNTestButton&quot;;
                      button.Name = &quot;ADNTestButtonName&quot;;
                      button.Text = &quot;ADN Test&quot;;
                      button.Orientation = System.Windows.Controls.Orientation.Vertical;

                      //add the button to the panel
                      ADNPanel.Source.Items.Add(button);
                      //delegate the event when ribbon element is activated
                      ComponentManager.UIElementActivated += new EventHandler(ComponentManager_UIElementActivated); 
                      //add the panel to the tab
                      Tab.Panels.Add(ADNPanel);
                  } 

               }
          } 
         return 0;
      }
   }
}
#endregion</pre>
<p>&#0160;</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8093031970b-pi"><img alt="image" border="0" height="158" src="/assets/image_970073.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="244" /></a></p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8093035970b-pi"><img alt="image" border="0" height="132" src="/assets/image_346853.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="244" /></a></p>
