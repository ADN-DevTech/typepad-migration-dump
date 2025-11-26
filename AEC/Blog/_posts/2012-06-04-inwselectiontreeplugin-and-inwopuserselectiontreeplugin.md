---
layout: "post"
title: "InwSelectionTreePlugin and InwOpUserSelectionTreePlugin"
date: "2012-06-04 22:40:55"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/inwselectiontreeplugin-and-inwopuserselectiontreeplugin.html "
typepad_basename: "inwselectiontreeplugin-and-inwopuserselectiontreeplugin"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><b>Issue</b></p>  <p>I found InwSelectionTreePlugin. What is its functionality and the difference with InwOpUserSelectionTreePlugin?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <ul>   <li>InwSelectionTreePlugin     <br />This is a Navisworks COM plugin that loads by an entry in the “COM Plugins” registry section.It can be used to dynamically add a selection tree completely from scratch without using find specs.&#160; It will add a new tab into the normal selection tree location.      <br />The skeleton of InwSelectionTreePlugin is similar to other kind of plugins. i.e. Implement the base plug-in: InwPlugin and specify the DisplayNames etc. Implement the functions of InwSelectionTreePlugin. And, you need to create a custom tree interface from InwOpSelectionTreeInterface. In its interfaces, specify the behavior of the tree. e.g.&#160; iOnCollapsed is an event when the nodes is collapse; iOnBeginContextMenu is an event when the context menu invokes; iGetName specifies the name of the tree . Finally, in InwSelectionTreePlugin.iCreateInterface, create an instance of the class above. Thus the new tree will behavior as defined       <br /></li>    <li>InwOpUserSelectionTreePlugin     <br />Despite being called ‘*plugin’ this is NOT a Navisworks COM plugin that loads by an entry in the “COM Plugins” registry section. It’s a way of programmatically defining a selection tree using find specs. This can then be saved to file and redistributed onto other PCs.&#160; Please refer to SDK sample Plugin_07.</li> </ul>
