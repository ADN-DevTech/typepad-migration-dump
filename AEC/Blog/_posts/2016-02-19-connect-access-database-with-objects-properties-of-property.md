---
layout: "post"
title: "Connect Access Database with Objects Properties of Property"
date: "2016-02-19 10:00:00"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2016/02/connect-access-database-with-objects-properties-of-property.html "
typepad_basename: "connect-access-database-with-objects-properties-of-property"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>We are often asked about how to connect external database with the model items properties of Navisworks, like DataTools. Actually, since Navisworks .NET API is based on the native .NET framework, you can simply read out the data from database by relevant libraries such as Access, SQL, Oracle etc, then use properties API of Navisworks to attach the data to the corresponding model items.</p>  <p>Since long time ago, we have provided a COM plugin sample that shows how to connect Access database. This sample is available at SDK\api\COM\examples\Plugin\<strong>PluginPropertyDatabaseExample</strong>. When Navisworks is being lunched, it will ask to select a Access database (as a demo, the gatehouse.mdb and gatehouse.nwd are provided at SDK\api\COM\examples). It uses the name of the part to associate fields in the database with the item's properties list. They are <strong>transient</strong> properties which will not be saved with document. </p>  <p>COM is quite old. Recently, one customer reported the COM plugin does not work on Win8. This pushed me to think about the alternative whether it is possible to convert the workflow to .NET plugin. I did and successfully made a working sample on Win8.1. It contains a few things of .NET API.</p>  <p>1. This is an EventWatcherPlugin plugin which can be loaded and delegates relevant events on startup of Navisworks</p>  <p>2. On startup, Access database is connected like what COM plugin does. </p>  <p>3. On startup, document changing event (by Models.CollectionChanged) is delegated, in which current selection changed event is also delegated.</p>  <p>4. In current selection changed event, if it is the sample document gatehouse.nwd, check the selected node, if it is a layer, add custom property tab and the two properties from Access database. </p>  <p>5. even though with .NET plugin, we still need to use COM API (though a COM interop instead) to add/modify/remove custom properties. </p>  <p>6. If the custom properties tab has been created, the code will read the Access database again and update the properties.</p>  <p>However, these are <strong>permanent</strong> properties which means if you save the document, they will be saved as well. So we will need to consider to remove them timely. The first I thought of is to remove the custom property in CurrentSelection.Changing. i.e. remove the custom property of old selection. And in CurrentSelection.Changed, add/update custom property to new selection. By this way, the custom tab can be cleaned timely. Unfortunately. It looks there is an issue with CurrentSelection.Changing. Finally, the workaround is to remove all when the document is being saved in Document.FileSaving. It can work. This might not be a good idea because if the end user has clicked many items the process of removing might take time. While since it is in saving, the end user would not be much annoy about the slowness. In addition, in reality,the end user will only click limited number of items (normally happens when manually clicking), there should not be much slowness.</p>  <p>We have logged wish to expose similar workflow like COM plugin for .NET plugin. i.e. a kind of transient properties. While at this moment, the solution above could be of a bit help.</p>  <p>The whole project is available at Github:</p>  <h3><strong><a href="https://github.com/xiaodongliang/Navisworks-Net-Plugin-Property-Database-Example">Navisworks-Net-Plugin-Property-Database-Example</a></strong></h3>  <p>The following is some core codes that is related with Navisworks API.</p> <script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>  <pre class="csharp prettyprint" name="code"> string mytabname = &quot;MyCustomTabUserName&quot;;
          void oDoc_FileSaving(object sender,
                          System.EventArgs e)
         {
             
             //in theory, since we have removed the tab timely in selection changing, 
             // there should be only one node that still contains the custom tab. this node is the last selected node.
             //so get it out and remove the properties. 

             //in case there are more nodes, this code can also remove their custom tabs.

             if (sender != null)
             {
                 Document oDoc = sender as Document;

                 if (oDoc.Title == &quot;gatehouse.nwd&quot;)
                 {
                     try
                     {
                         //firstly use .NET API to get the items with custom tab 
                         Search search = new Search();
                         search.Selection.SelectAll();
                         search.SearchConditions.Add(SearchCondition.HasCategoryByDisplayName(mytabname));
                         ModelItemCollection items = search.FindAll(oDoc, false);

                         ComApi.InwOpState9 oState = ComApiBridge.State;

                         foreach (ModelItem oitem in items)
                         {

                             //convert .NET items to COM items
                             ComApi.InwOaPath3 oPath = ComApiBridge.ToInwOaPath(oitem) as ComApi.InwOaPath3;

                             if ((oPath.Nodes().Last() as ComApi.InwOaNode).IsLayer)
                             {
                                 //check whether the custom property tab has been added. 
                                 int customProTabIndex = 1;
                                 ComApi.InwGUIPropertyNode2 nodePropertiesOwner = oState.GetGUIPropertyNode(oPath, true) as ComApi.InwGUIPropertyNode2;
                                 ComApi.InwGUIAttribute2 customTab = null;
                                 foreach (ComApi.InwGUIAttribute2 nwAtt in nodePropertiesOwner.GUIAttributes())
                                 {
                                     if (!nwAtt.UserDefined) continue;

                                     if (nwAtt.ClassUserName == mytabname)
                                     {
                                         //remove the custom tab
                                         nodePropertiesOwner.RemoveUserDefined(customProTabIndex);
                                         customTab = nwAtt;
                                         break;
                                     }
                                     customProTabIndex += 1;
                                 }
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.ToString());
                     }

                 }
             }
         }
   void CurrentSelection_Changed(object sender,
                                      System.EventArgs e)
        {
            if (sender != null)
            {
                Document oDoc = sender as Document;


                if (oDoc.Title == &quot;gatehouse.nwd&quot;)
                {
                    //this is the new selection after selecting
                    if (oDoc.CurrentSelection.SelectedItems.Count &gt; 0)
                    {
                        ComApi.InwOpState9 oState = ComApiBridge.State;

                        try
                        {
                            ComApi.InwOaPath3 oPath = oState.CurrentSelection.Paths()[1];
                            if ((oPath.Nodes().Last() as ComApi.InwOaNode).IsLayer)
                            {
                                //check whether the custom property tab has been added.

                                string mytabname = &quot;MyCustomTabUserName&quot;;
                                int customProTabIndex = 1;
                                ComApi.InwGUIPropertyNode2 nodePropertiesOwner = oState.GetGUIPropertyNode(oPath, true) as ComApi.InwGUIPropertyNode2;
                                ComApi.InwGUIAttribute2 customTab = null;
                                foreach (ComApi.InwGUIAttribute2 nwAtt in nodePropertiesOwner.GUIAttributes())
                                {
                                    if (!nwAtt.UserDefined) continue;

                                    if (nwAtt.ClassUserName == mytabname)
                                    {
                                        customTab = nwAtt;
                                        break;
                                    }
                                    customProTabIndex += 1;
                                }


                                if (customTab == null)
                                {
                                    ////create the tab if it does not exist
                                    ComApi.InwOaPropertyVec newPvec =
                                         (ComApi.InwOaPropertyVec)oState.ObjectFactory(
                                               ComApi.nwEObjectType.eObjectType_nwOaPropertyVec, null, null);

                                    ComApi.InwOaProperty prop1 = oState.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwOaProperty) as ComApi.InwOaProperty;
                                    ComApi.InwOaProperty prop2 = oState.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwOaProperty) as ComApi.InwOaProperty;
                                    prop1.name = &quot;Finish&quot;;
                                    prop1.UserName = &quot;Finish Date&quot;;
                                    object linkVal = m_dblink.read(&quot;Finish&quot;, (oPath.Nodes().Last() as ComApi.InwOaNode).UserName);
                                    if (linkVal != null)
                                    {
                                        prop1.value = linkVal;
                                        newPvec.Properties().Add(prop1);
                                    }

                                    prop2.name = &quot;Notes&quot;;
                                    prop2.UserName = &quot;Notes&quot;;
                                    linkVal = m_dblink.read(&quot;Notes&quot;, (oPath.Nodes().Last() as ComApi.InwOaNode).UserName);
                                    if (linkVal != null)
                                    {
                                        prop2.value = linkVal;
                                        newPvec.Properties().Add(prop2);
                                    }

                                    //the first argument is always 0 if adding a new tab
                                    nodePropertiesOwner.SetUserDefined(0, mytabname, mytabname, newPvec);
                                }
                                else
                                {
                                    ////update the properties in the tab with the new values from database
                                    ComApi.InwOaPropertyVec newPvec =
                                       (ComApi.InwOaPropertyVec)oState.ObjectFactory(
                                             ComApi.nwEObjectType.eObjectType_nwOaPropertyVec, null, null);

                                    foreach (ComApi.InwOaProperty nwProp in customTab.Properties())
                                    {
                                        ComApi.InwOaProperty prop = oState.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwOaProperty) as ComApi.InwOaProperty;
                                         prop.name = nwProp.name;
                                        prop.UserName = nwProp.UserName;
                                        object linkVal = m_dblink.read(prop.name, (oPath.Nodes().Last() as ComApi.InwOaNode).UserName);
                                        if (linkVal != null)
                                        {
                                            prop.value = linkVal;
                                            newPvec.Properties().Add(prop);
                                        } 
                                    }
                                    nodePropertiesOwner.SetUserDefined(customProTabIndex, mytabname, mytabname, newPvec);

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    
                }
            }
        }</pre>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8177b23970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_32550.jpg" width="340" height="308" /></a></p>
