---
layout: "post"
title: "DUT versus UST"
date: "2009-10-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Parameters"
  - "Settings"
  - "Units"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/10/dut-versus-ust.html "
typepad_basename: "dut-versus-ust"
typepad_status: "Publish"
---

<p>After a wonderful weekend in the Swiss alps climbing the 

<a href="http://www.heinzkarrer.ch/BergtourenUrner/Diechterhorn.html">
Diechterhorn</a>, here is a 

follow-up explanation by Jeremy Sawicki of Autodesk on the difference between DisplayUnitType and UnitSymbolType

in continuation of our last discussion on the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/unit-suffix-and-the-projectunit-sdk-sample.html">
unit suffix</a>.

<p><strong>Question:</strong> What is the exact difference between DisplayUnitType and UnitSymbolType, please?

<p>For instance, the default project unit settings on my system for a length are DUT_Millimetres and UST_None ... I wonder what exactly that means?

<p>Also, can you suggest a way to obtain the display string representation shown in the user interface for either of these?

<p><strong>Answer:</strong> The simplest answer is based on the UI.  
In the Format dialog box, DisplayUnitType corresponds to the Units dropdown:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a5bd4fd6970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a5bd4fd6970b" alt="DisplayUnitType" title="DisplayUnitType" src="/assets/image_732c80.jpg" border="0"  /></a> <br />

</center>

<p>And UnitSymbolType corresponds to the Unit symbol dropdown:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a613f72c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a613f72c970c" alt="UnitSymbolType" title="UnitSymbolType" src="/assets/image_153a8a.jpg" border="0"  /></a> <br />

</center>

<p>In most cases, the DisplayUnitType determines the actual unit, for example feet vs. meters, with a different conversion factor applying to each DisplayUnitType.  UnitSymbolType affects how the units are indicated on the screen, usually with a suffix.  For example, square feet can use a suffix of either SF or ft² or no suffix at all.  UnitSymbolType used to be called UnitSuffixType until we introduced a currency unit type, which uses prefixes in some cases, like $.

<p>The above applies to most normal units which are displayed as a number followed by a suffix, but there are various special cases.  For example, DisplayUnitType is also used to select certain kinds of units with special formatting, like Feet and fractional inches or Degrees minutes seconds.

<p>I don't believe there is currently any API that can get the names of display unit types or unit symbol types.

<p>Many thanks to Jeremy for this helpful clarification!
