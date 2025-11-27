---
layout: "post"
title: "How to reverse an Insert Constraint Axes?"
date: "2013-12-18 01:05:26"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/12/how-to-reverse-an-insert-constraint-axes.html "
typepad_basename: "how-to-reverse-an-insert-constraint-axes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>The <em>AxesOpposed</em> property of an <em>InsertConstraint</em> is a ReadOnly property, however it is possible to workaround that limitation using the “<em>ConvertToInsertConstraint</em>” method.</p>  <p>Here is the VBA code for that. It assumes that the constraint(1) is an insert:</p>  <p><em>Sub ToggleInsertAxes()</em></p>  <p><em>&#160;&#160;&#160; Dim doc As AssemblyDocument     <br />&#160;&#160;&#160; Set doc = ThisApplication.ActiveDocument      <br />&#160;&#160;&#160; <br />&#160;&#160;&#160; Dim insert As InsertConstraint      <br />&#160;&#160;&#160; Set insert = doc.ComponentDefinition.Constraints(1)</em></p>  <p><em>&#160;&#160;&#160; Call insert.ConvertToInsertConstraint( _     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; insert.EntityOne, _      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; insert.EntityTwo, _      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; Not insert.AxesOpposed, _      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; insert.Distance.value)      <br />&#160;&#160; <br />End Sub</em></p>
