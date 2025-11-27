---
layout: "post"
title: "Accessing iProperties through iLogic code"
date: "2018-04-12 22:16:04"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/04/accessing-iproperties-through-ilogic-code.html "
typepad_basename: "accessing-iproperties-through-ilogic-code"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html" rel="noopener noreferrer" target="_blank">Chandra shekar Gopal</a></p>
<p>iProperty is a set of attributes for each Inventor file such as part number, description and physical material. You can also create custom iProperties.</p>
<p>From each document you can access its associated iProperties. iProperties are used to track and manage files, create reports, and automatically update assembly bills of materials, drawing parts lists, title blocks, and other information.</p>
<p>For more details on importance and structure of iProperties, you can refer this <a href="http://modthemachine.typepad.com/my_weblog/2010/02/accessing-iproperties.html" rel="noopener noreferrer" target="_blank">link</a>. Now, many people used to ask iLogic code to access iPropeties. Below table gives list of iProperties and respective iLogic code.</p>
<table border="1" cellspacing="0" style="width: 876px; height: 1498px; border-color: #000000;">
<tbody>
<tr>
<td colspan="3" style="width: 866px; text-align: center; background-color: #c0c0c0;"><strong>Inventor Summary Information</strong>&#0160;</td>
</tr>
<tr>
<td style="width: 235.66px; text-align: left;"><strong>Property Name</strong></td>
<td style="width: 109.81px; text-align: left;"><strong>Type</strong></td>
<td style="width: 508.53px; text-align: left;"><strong>iLogic code</strong></td>
</tr>
<tr>
<td style="width: 235.66px;">Author</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Author&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Comments</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Comments“)</td>
</tr>
<tr>
<td style="width: 235.66px;">Keywords</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Keywords&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Last Saved By</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Last Saved By&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Thumbnail</td>
<td style="width: 109.81px;">IPictureDisp</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Thumbnail&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Revision Number</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Revision Number&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Subject</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Subject&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Title</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Title&quot;)</td>
</tr>
<tr>
<td colspan="3" style="width: 866px; text-align: center; background-color: #c0c0c0;"><strong>Inventor Document Summary Information</strong></td>
</tr>
<tr>
<td style="width: 235.66px;"><strong>Property Name</strong></td>
<td style="width: 109.81px;"><strong>Type</strong></td>
<td style="width: 508.53px;"><strong>iLogic code</strong></td>
</tr>
<tr>
<td style="width: 235.66px;">Category</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Category&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Company</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Company“)</td>
</tr>
<tr>
<td style="width: 235.66px;">Manager</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Summary&quot;, &quot;Manager&quot;)</td>
</tr>
<tr>
<td colspan="3" style="width: 866px; text-align: center; background-color: #c0c0c0;"><strong>Design Tracking Properties</strong></td>
</tr>
<tr>
<td style="width: 235.66px;"><strong>Property Name</strong></td>
<td style="width: 109.81px;"><strong>Type</strong></td>
<td style="width: 508.53px;"><strong>iLogic code</strong></td>
</tr>
<tr>
<td style="width: 235.66px;">Authority</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Authority&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Catalog Web Link</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Catalog Web Link&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Categories</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Categories&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Checked By</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Checked By&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Cost</td>
<td style="width: 109.81px;">Currency</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Cost&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Cost Center</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Cost Center&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Creation Time</td>
<td style="width: 109.81px;">Date</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Creation Time&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Date Checked</td>
<td style="width: 109.81px;">Date</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Date Checked&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Defer Updates</td>
<td style="width: 109.81px;">Boolean</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Defer Updates&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Description</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Description&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Design Status</td>
<td style="width: 109.81px;">Long</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Design Status&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Designer</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Designer&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Document SubType</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot; Document SubType&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Document SubType Name</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot; Document SubType Name&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Engineer</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Engineer&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Engr Approved By</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Engr Approved By&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Engr Date Approved</td>
<td style="width: 109.81px;">Date</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Engr Date Approved&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">External Property Revision Id</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;External Property Revision Id&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Language</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Language&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Manufacturer</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Manufacturer&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Material</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Material&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Mfg Approved By</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Mfg Approved By&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Mfg Date Approved</td>
<td style="width: 109.81px;">Date</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Mfg Date Approved&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Parameterized Template</td>
<td style="width: 109.81px;">Boolean</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Parameterized Template&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Part Icon</td>
<td style="width: 109.81px;">IPictureDisp</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Part Icon&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Part Number</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Part Number&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Part Property Revision Id</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Part Property Revision Id&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Project</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Project&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Proxy Refresh Date</td>
<td style="width: 109.81px;">Date</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Proxy Refresh Date&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Size Designation</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Size Designation&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Standard</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Standard&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Standard Revision</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Standard Revision&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Standards Organization</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Standard Organization&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Stock Number</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Stock Number&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Template Row</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Template Row&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">User Status</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;User Status&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Vendor</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Vendor&quot;)</td>
</tr>
<tr>
<td style="width: 235.66px;">Weld Material</td>
<td style="width: 109.81px;">String</td>
<td style="width: 508.53px;">iProperties.Value(&quot;Project&quot;, &quot;Weld Material&quot;)</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
