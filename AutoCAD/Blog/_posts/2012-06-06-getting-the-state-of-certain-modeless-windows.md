---
layout: "post"
title: "Getting the state of certain modeless windows"
date: "2012-06-06 20:15:53"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/getting-the-state-of-certain-modeless-windows.html "
typepad_basename: "getting-the-state-of-certain-modeless-windows"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To know if certain modeless windows such as the&nbsp;Properties Window, DesignCenter, and DbConnect windows are currently displayed in an AutoCAD session, there are&nbsp;binary system variable that contain this information.</p>
<p>OPMSTATE - Properties Window<br />ADCSTATE - AutoCAD Design Center<br />DBCSTATE - Database Connectivity</p>
<div>
<p>All three system variables are set to 1 when the corresponding window is visible, and are set to 0 when not visible. To programmatically control the visibility of these modeless windows, you can send the AutoCAD commands such as "PROPERTIES", "PROPERTIESCLOSE", "ADC", "ADCCLOSE", DBC" and "DBCCLOSE" commands.</p>
</div>
