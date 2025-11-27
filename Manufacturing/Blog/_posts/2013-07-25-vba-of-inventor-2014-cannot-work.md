---
layout: "post"
title: "VBA of Inventor 2014 cannot work"
date: "2013-07-25 20:04:55"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/vba-of-inventor-2014-cannot-work.html "
typepad_basename: "vba-of-inventor-2014-cannot-work"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Issue</strong>:    <br />I migrated my VBA macro to Inventor 2014. If I click the VBA Editor button on the Tools tab of the Inventor Ribbon NOTHING happens, the editor does not appear. If I click the Macros button on the Tools tab of the Inventor Ribbon NOTHING happens. I do not get the Macros dialog to select any of my macros.</p>  <p><strong>Solution</strong>:    <br />This should be the known issue with Office 365. </p>  <p>From the article:   <br /><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=21910314&amp;linkID=9242018">http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=21910314&amp;linkID=9242018</a></p>  <p> Installing Office 365 will remove VBA 7 modules from your computer, leading to the following:</p>  <li>Error messages are shown during Inventor 2014 launch </li>  <li>Unable open any document from Inventor 2014 </li>  <li>Error messages are shown when executing VBA commands from AutoCAD   <br /><em>     <br />Standalone</em><strong>:</strong></li>  <p>Close Inventor session and manually install VBA7 from Inventor source media:</p>  <p>64-bit </p>  <p>Install VBA7 from &quot;3rdParty\x64\VBA\Vba7.MSI&quot; .</p>  <p>32-bit</p>  <p>Install VBA7 from &quot;3rdParty\x86\VBA\Vba7.MSI&quot; .</p>  <p><em>Deployment</em><strong>:</strong></p>  <p>64-bit</p>  <p>Deploy VBA7 installer to the client from &quot;3rdParty\x64\VBA\Vba7.MSI&quot;.</p>  <p>32-bit</p>  <p>Deploy VBA7 installer to the client from &quot;3rdParty\x86\VBA\Vba7.MSI&quot;.</p>
