---
layout: "post"
title: "Getting the ObjectARX Wizard to work with Visual Studio Express editions"
date: "2006-07-05 15:20:45"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "ObjectARX"
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/07/getting_the_obj.html "
typepad_basename: "getting_the_obj"
typepad_status: "Publish"
---

<p>This topic was suggested by one of our ADN members - Paul Richardson, from CAD System Engineering - and answered by Cyrille Fauvel, from DevTech EMEA. So all I really had to do was copy/paste and some minor editing... now that's my kind of blogging. :-)<br /><br />The ObjectARX Wizard's installer targets the Microsoft Visual Studio 2005 platform rather than the Microsoft Visual C++/C#/VB.NET Express Editions. The main reasons for this are the limitations of the Express Editions' IDE - particularly due to its lack of support for AddIns. The ObjectARX Wizard is actually made up of a number of components:</p>

<ul><li>AppWizards - these are &quot;project templates&quot; with an HTML interface that allows basic set-up of projects</li>

<li>Class Wizard - another HTML interface that allows creation and modification of classes, etc.</li>

<li>AddIn - this is a more complex application (not defined in HTML), allowing addition of commands and including functionality such as the Class Explorer</li></ul>

<p>While the Express Editions support applications based on HTML (e.g. the AppWizards and the Class Wizard), they do not support AddIns. Which means only part of the functionality of the ObjectARX Wizard can be made to work.</p>

<p>Having said that, the below procedure will allow you to get this subset of the overall functionality working for Visual C++ Express (and it should be comparable to get it working for C#/VB.NET, but for now this approach has only been verified for Visual C++ Express):<br /><br />1. First we need to fool the ObjectARX Wizards' installer into thinking Visual Studio is installed.<br /><br />Go to the Registry and create the following key:<br /><br />HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\8.0\Setup\VS<br /><br />Add a string value: name= 'ProductDir', value = where we want to install the ObjectARX Wizards files to. It's suggested to make this 'C:\Program Files\Microsoft Visual Studio 8\' - the below procedure assumes that. Note: do not forget the trailing '\'.</p>

<p>This registry change will allow you to install the ObjectARX Wizards.<br /><br />2. The installer will create some new files in the above location, which need to be copied to your Express installation.</p>

<p>Go into 'C:\Program Files\Microsoft Visual Studio 8\VC\vcprojects' and copy the ObjectARX directory into 'C:\Program Files\Microsoft Visual Studio 8\VC\Express\vcprojects'.<br />Copy the 'C:\Program Files\Microsoft Visual Studio 8\VC\vcprojects\ArxAppWiz.*' files into 'C:\Program Files\Microsoft Visual Studio 8\VC\Express\vcprojects'.<br />Finally copy the C:\Program Files\Microsoft Visual Studio 8\VC\VCAddClass\ObjectARX' directory into 'C:\Program Files\Microsoft Visual Studio 8\VC\Express\VCAddClass'.<br /><br />After these steps all the templates-based Wizards will be available in the Visual C++ Express Edition.</p>
