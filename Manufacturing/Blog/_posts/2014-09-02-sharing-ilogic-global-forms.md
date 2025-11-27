---
layout: "post"
title: "Sharing iLogic Global Forms"
date: "2014-09-02 10:05:00"
author: "Xiaodong Liang"
categories:
  - "iLogic"
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/09/sharing-ilogic-global-forms.html "
typepad_basename: "sharing-ilogic-global-forms"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question:     <br /></strong>I created a global form manually by iLogic. An XML is generated at C:\Users\Public\Documents\Autodesk\Inventor 2014\Design Data\iLogic\UI. I made a copy of the XML file and renamed the tags of Form and Guid. Finally I restarted Inventor. I thought iLogic can load the copied XML as one more global form. But nothing happened. </p>  <p><strong>Solution</strong>:    <br />You can share a global form to another machine.In 2015 and earlier there are two ways to do it.&#160; I’d recommend the first way.</p>  <p><strong><em>First way:       <br /></em></strong>- Right-click on the form in the Global Forms tab and select Copy Form.    <br />- Create an empty part, go to its Forms tab and select Paste Form.    <br />- Save the part, and send it to the other system.    <br />- On the other system, reverse the procedure: copy from the part Forms and paste to Global Forms.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0616a75970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="clip_image002" border="0" alt="clip_image002" src="/assets/image_88d3a3.jpg" width="267" height="259" /></a></p>  <p>   <br /><strong><em>Second way</em></strong>:&#160; (this is similar to the procedure you followed)    <br />-&#160;&#160; On the destination system, create a Global Form with the exact same name as the form you want to copy.&#160; This form can be empty: it does not need to have any controls in it.    <br />-&#160;&#160;&#160; Close the form definition.&#160; It will show up in the browser.    <br />-&#160;&#160;&#160; Copy the [Form Name].xml and [Form Name].state.xml file from the source system to the destination system. It will overwrite the file(s) for the form that you have just created.    </p>
