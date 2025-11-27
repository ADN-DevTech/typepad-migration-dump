---
layout: "post"
title: "Keep Your Interop Up to Date"
date: "2015-01-22 18:19:42"
author: "Adam Nagy"
categories:
  - "Brian"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/01/keep-your-interop-up-to-date.html "
typepad_basename: "keep-your-interop-up-to-date"
typepad_status: "Publish"
---

<p>I just finished working with a customer that was having a problem accessing certain functionality in the API.&#0160; He had written an initial prototype in VBA that worked as expected but then when he started to convert the program to .Net some of the Inventor API objects he was using weren’t available.&#0160; The reason is that he was using an old .Net <em>interop</em>.</p>
<p>The Inventor API is exposed using Microsoft’s COM technology.&#0160; The contents of the API (it’s objects, methods, properties, and events) are described in a file known as a type library.&#0160; When you use VBA it directly references this type library to know what’s in Inventor’s API and uses this to support Intellisense and to be able to compile and run the program.&#0160; VB.Net and C# are .Net based languages and don’t support directly calling a COM API.&#0160; To provide support for COM API’s .Net supports the creation of an interop library that serves as a translation layer between COM and .Net. The Interop provides the same functionality to VB.Net or C# that the type library does to VBA.&#0160; It describes all of the objects and functions in the Inventor API and converts the .Net API call into a native COM call.&#0160; As of the last few .Net releases this technology works very good and in almost all cases makes it completely transparent that you’re not working directly with the COM API.</p>
<p>When you create a new .Net or C# program to work with Inventor you need to add a reference to Inventor’s COM interop in order to have access to the API.&#0160; You do this by using the <strong>References</strong> command in Visual Studio.&#0160; In the dialog, use the “Browse” option to the left and then click the “Browse…” button at the bottom of the dialog.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb07e0469e970d-pi"><img alt="InteropAddReference" border="0" height="194" src="/assets/image_725209.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="InteropAddReference" width="504" /></a>&#0160;</p>
<p>Browse to “C:\Programs Files\Autodesk\Inventor XXXX\Bin\Public Assemblies”, (where XXXX is the version of Inventor you want to use) and select “autodesk.inventor.interop.dll”.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d0c62157970c-pi"><img alt="InteropPickAssembly" border="0" height="272" src="/assets/image_137722.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="InteropPickAssembly" width="504" /></a></p>
<p>&#0160;</p>
<p>Your program is now dependent on the interop for that version of Inventor.&#0160; Let’s say that you create a reference to Inventor 2014 and then later you uninstall Inventor and install Inventor 2015.&#0160; The API is backward compatible so when you installed Inventor 2015 it installed the interop for 2015 but also installed interops for several previous versions so programs dependent on those older version will still continue to work.&#0160; But it your program needs to use some functionality that is new in Inventor 2015 you need to change the reference to the newer interop where this new functionality is defined.</p>
<p>You can see what references an existing program has in the Solution Explorer in Visual Studio.&#0160; By default they’re not shown but by clicking the “Show All Files” button a new “References” folder will appear in the tree, as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb07e046a7970d-pi"><img alt="InteropShowReferences" border="0" height="395" src="/assets/image_874285.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border-width: 0px;" title="InteropShowReferences" width="282" /></a></p>
<p>&#0160;</p>
<p>You can see more information about a specific reference by selecting it and looking in the Properties window, as shown below.&#0160; In this case I’ve selected the “autodesk.inventor.interop” and it’s showing me that it’s version 19.0.0.0.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb07e046ae970d-pi"><img alt="InteropShowVersion" border="0" height="340" src="/assets/image_795536.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="InteropShowVersion" width="504" /></a></p>
<p>&#0160;</p>
<p>The version numbers are confusing because they don’t directly correlate with the public version of Inventor.&#0160; Here’s a table showing the public version, the corresponding software version, and the internal development name.</p>
<div style="text-align: center;">
<table align="center" border="1" cellpadding="2" cellspacing="0" width="399">
<tbody>
<tr>
<td align="center" valign="top" width="137"><strong>Public Version</strong></td>
<td align="center" valign="top" width="142"><strong>Software Version</strong></td>
<td align="center" valign="top" width="118"><strong>Internal Name</strong></td>
</tr>
<tr>
<td align="center" valign="top" width="137">2011</td>
<td align="center" valign="top" width="142">15</td>
<td align="center" valign="top" width="119">Sikorsky</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2012</td>
<td align="center" valign="top" width="142">16</td>
<td align="center" valign="top" width="120">Brunel</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2013</td>
<td align="center" valign="top" width="141">17</td>
<td align="center" valign="top" width="120">Goodyear</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2014</td>
<td align="center" valign="top" width="141">18</td>
<td align="center" valign="top" width="120">Franklin</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2015</td>
<td align="center" valign="top" width="141">19</td>
<td align="center" valign="top" width="120">Dyson</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2016</td>
<td align="center" valign="top" width="142">20</td>
<td align="center" valign="top" width="121">Shelby</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2017</td>
<td align="center" valign="top" width="142">21</td>
<td align="center" valign="top" width="121">Enzo</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2018</td>
<td align="center" valign="top" width="142">22</td>
<td align="center" valign="top" width="121">Elon</td>
</tr>
<tr>
<td align="center" valign="top" width="136">2019</td>
<td align="center" valign="top" width="142">23</td>
<td align="center" valign="top" width="121">Zora</td>
</tr>
</tbody>
</table>
</div>
<p>-Brian</p>
