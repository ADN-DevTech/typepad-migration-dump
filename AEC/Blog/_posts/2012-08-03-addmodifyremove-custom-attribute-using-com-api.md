---
layout: "post"
title: "Add/Modify/Remove custom attribute using COM API"
date: "2012-08-03 10:25:27"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "COM"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/08/addmodifyremove-custom-attribute-using-com-api.html "
typepad_basename: "addmodifyremove-custom-attribute-using-com-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you&#39;d like to add user data to the selected item or change its existing data, then you need to populate a property vector with all the properties the user data will contain and then add it to the selected item&#39;s GUI property node.</p>
<p>In case of modifying user data you need to recreate the use data with the required modifications and then overwrite the existing one. If first parameter of SetUserDefined() is 0, then a new User Data will be created, otherwise an exisiting one will be overwritten.</p>
<p>If you want to remove a single item in a user data, then you just have to omit it when you recreate the property vector, but if you want to remove the whole user data property collection then you need to call RemoveUserDefined() with the index of the user data you are trying to remove.</p>
<p>Here is a sample code written in .NET using the COM API of Navisworks:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> NavisWorks8</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> NavisworksAPI8</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> NavisworksAPI8.nwEObjectType</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Form1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwDoc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> Document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwState </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOpState10</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Open a Navisworks document</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> btnOpen_Click( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ) </span><span style="color: blue; line-height: 140%;">Handles</span><span style="line-height: 140%;"> btnOpen.Click</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwDoc.OpenFile( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;C:\Program Files\Autodesk\Navisworks Manage 2011\api\COM\&quot;</span><span style="line-height: 140%;"> + _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;examples\gatehouse.nwd&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwState = nwDoc.State</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwDoc.Visible = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Add User Data MyAttributeUserName with two items in it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> btnAddUserData_Click( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ) </span><span style="color: blue; line-height: 140%;">Handles</span><span style="line-height: 140%;"> btnAddUserData.Click</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> nwState.CurrentSelection.Paths.Count &lt;&gt; 1 </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Select an item first&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwPath </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPath = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.CurrentSelection.Paths(1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwProps </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPropertyVec = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaPropertyVec)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwProp1 </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaProperty = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaProperty)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwProp1.name = </span><span style="color: #a31515; line-height: 140%;">&quot;MyPropertyName1&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwProp1.value = </span><span style="color: #a31515; line-height: 140%;">&quot;MyPropertyValue1&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwProps.Properties.Add(nwProp1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwProp2 </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaProperty = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaProperty)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwProp2.name = </span><span style="color: #a31515; line-height: 140%;">&quot;MyPropertyName2&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwProp2.value = </span><span style="color: #a31515; line-height: 140%;">&quot;MyPropertyValue2&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwProps.Properties.Add(nwProp2)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwNode </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwGUIPropertyNode2 = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.GetGUIPropertyNode(nwPath, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; nwNode.SetUserDefined(0, </span><span style="color: #a31515; line-height: 140%;">&quot;MyAttributeUserName&quot;</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;MyAttributeInternalName&quot;</span><span style="line-height: 140%;">, nwProps)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Modify the value of MyPropertyName1 in MyAttributeUserName</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> btnModifyUserDataItem_Click( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ) </span><span style="color: blue; line-height: 140%;">Handles</span><span style="line-height: 140%;"> btnModifyUserDataItem.Click</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> nwState.CurrentSelection.Paths.Count &lt;&gt; 1 </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Select an item first&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwPath </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPath = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.CurrentSelection.Paths(1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwProps </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPropertyVec = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaPropertyVec)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwNode </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwGUIPropertyNode2 = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.GetGUIPropertyNode(nwPath, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> index </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = 1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> nwAtt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwGUIAttribute2 </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> nwNode.GUIAttributes()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> nwAtt.UserDefined </span><span style="color: blue; line-height: 140%;">Then</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Continue For</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> nwAtt.ClassUserName = </span><span style="color: #a31515; line-height: 140%;">&quot;MyAttributeUserName&quot;</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; index += 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Continue For</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> nwProp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaProperty </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> nwAtt.Properties()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwNewProp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaProperty = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaProperty)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwNewProp.name = nwProp.name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwNewProp.value = nwProp.value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> nwNewProp.name = </span><span style="color: #a31515; line-height: 140%;">&quot;MyPropertyName1&quot;</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; nwNewProp.value += </span><span style="color: #a31515; line-height: 140%;">&quot;-modified&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwProps.Properties.Add(nwNewProp)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwNode.SetUserDefined(index, nwAtt.ClassUserName, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwAtt.ClassName, nwProps)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Exit For</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Remove one of the items in the user data we previously added</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> btnRemoveUserDataItem_Click( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ) </span><span style="color: blue; line-height: 140%;">Handles</span><span style="line-height: 140%;"> btnRemoveUserDataItem.Click</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> nwState.CurrentSelection.Paths.Count &lt;&gt; 1 </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Select an item first&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwPath </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPath = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.CurrentSelection.Paths(1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwProps </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPropertyVec = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaPropertyVec)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwNode </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwGUIPropertyNode2 = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.GetGUIPropertyNode(nwPath, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> index </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = 1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> nwAtt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwGUIAttribute2 </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> nwNode.GUIAttributes()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> nwAtt.UserDefined </span><span style="color: blue; line-height: 140%;">Then</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Continue For</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> nwAtt.ClassUserName = </span><span style="color: #a31515; line-height: 140%;">&quot;MyAttributeUserName&quot;</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; index += 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Continue For</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> nwProp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaProperty </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> nwAtt.Properties()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> nwProp.name = </span><span style="color: #a31515; line-height: 140%;">&quot;MyPropertyName1&quot;</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Continue For</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwNewProp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaProperty = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaProperty)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwNewProp.name = nwProp.name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwNewProp.value = nwProp.value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwProps.Properties.Add(nwNewProp)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwNode.SetUserDefined(index, nwAtt.ClassUserName, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; nwAtt.ClassName, nwProps)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Exit For</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Remove MyAttributeUserName completely</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> btnRemoveUserData_Click( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">Object</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ) </span><span style="color: blue; line-height: 140%;">Handles</span><span style="line-height: 140%;"> btnRemoveUserData.Click</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> nwState.CurrentSelection.Paths.Count &lt;&gt; 1 </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Select an item first&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwPath </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPath = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.CurrentSelection.Paths(1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwProps </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwOaPropertyVec = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.ObjectFactory(eObjectType_nwOaPropertyVec)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> nwNode </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwGUIPropertyNode2 = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwState.GetGUIPropertyNode(nwPath, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> index </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = 1</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> nwAtt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> InwGUIAttribute2 </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> nwNode.GUIAttributes()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> nwAtt.UserDefined </span><span style="color: blue; line-height: 140%;">Then</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Continue For</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> nwAtt.ClassUserName = </span><span style="color: #a31515; line-height: 140%;">&quot;MyAttributeUserName&quot;</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; index += 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Continue For</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; nwNode.RemoveUserDefined(index)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Exit For</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span></p>
</div>
