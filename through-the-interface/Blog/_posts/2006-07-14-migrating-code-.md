---
layout: "post"
title: "Migrating ObjectARX applications to support Unicode - some resources"
date: "2006-07-14 17:46:56"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://www.keanw.com/2006/07/migrating_code_.html "
typepad_basename: "migrating_code_"
typepad_status: "Publish"
---

<p>The work we did to migrate AutoCAD 2007 to use <a href="http://en.wikipedia.org/wiki/Unicode">Unicode</a> (rather than <a href="http://en.wikipedia.org/wiki/Multi-byte_character_set">MBCS</a>), has impacted many developers around the world. For those that are yet to go through the pain themselves, I thought I'd talk about the resources that are available to ObjectARX developers needing to port their applications to Unicode.</p>

<p>Firstly, you should check out the Migration Guide that ships with the ObjectARX 2007 SDK (docs/acad_xmg.chm):</p>

<p><a onclick="window.open(this.href, '_blank', 'width=799,height=649,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/unicode_migration_docs.png"><img title="Unicode_migration_docs" height="81" alt="Unicode_migration_docs" src="/assets/unicode_migration_docs.png" width="100" border="0" style="FLOAT: left; MARGIN: 0px 5px 5px 0px" /></a> </p><br /><br /><br /><br /><br /><br /><p>There's a whole section called &quot;Upgrading to Unicode&quot;, with lots of useful information. A good deal of the material in the guide was compiled during the development phase of AutoCAD 2007, as our own engineering teams got to grips with porting AutoCAD to support Unicode.</p>

<p>Here's a quick outline listing of the topics, to give you a feel for the contents:</p>

<ul><li>Upgrading to Unicode<ul><li>Why Convert AutoCAD to Unicode? </li>

<li>Terminology and Basic Concepts<ul><li>Characters and Glyphs </li>

<li>MBCS, Multi-byte, DBCS, ANSI, ASCII, and Code Pages</li>

<li>Unicode and its Formats (UTF-8, UTF-16, and UTF-32)</li>

<li>Compiler Types: char, wchar_t</li>

<li>Autodesk Type: ACHAR</li>

<li>Affected AutoCAD-based Products</li>

<li>Effect of Unicode Conversion on AutoCAD File Types</li></ul></li>

<li>Creating Unicode-compatible Source Code<ul><li>Unicode Porting Tool: Visual Teefy </li>

<li>Text File Utilities Provided in the ObjectARX SDK</li>

<li>Outline of Autodesk Porting Process</li>

<li>Updating Large ObjectARX and ObjectDBX Applications</li>

<li>Project Definitions for Unicode Compilation<ul><li>Command Line Processing Tip</li></ul></li>

<li>Basic Coding Tasks<ul><li>Change char to ACHAR or TCHAR for ObjectARX APIs</li>

<li>Guidelines for Using TCHAR and ACHAR</li>

<li>Wrap Literal Strings and Characters with _T() or ACRX_T()</li>

<li>Replace ANSI String Pointer Types</li>

<li>Update String Formatting Functions</li>

<li>Check String Allocations for TCHAR Compatibility</li>

<li>Check Usage of Win32 APIs With No Unicode Equivalents</li>

<li>Check Usage of Lead Byte APIs</li>

<li>Use _TUCHAR in Unicode Character Classification Functions</li>

<li>Use #ifdef to Call the Correct Unicode Function</li>

<li>Replace strlen() With _tcslen, Rather Than _tcsclen, in Dual Build Code</li>

<li>Use Unicode Code Page Descriptors in Win32 APIs</li>

<li>Use Native wchar_t</li>

<li>AcArray of std::wstring Type Requires Special Allocator Argument</li>

<li>DCL Dialogs Use Unicode, but Definition Files are ANSI</li></ul></li>

<li>Secondary Coding Tasks<ul><li>Centralize Recurring Literal Strings</li>

<li>Replace Char Buffers with String Classes</li>

<li>Revisit Low-Level Win32 API Calls</li>

<li>Avoid Calling CRichEditCtrl::GetSelText()</li>

<li>Evaluate Usage of wctombs() And mbstowcs()</li></ul></li>

<li>Prepare User Interface for Unicode Compatibility<ul><li>Fine-Tuning System Font Usage for Special Cases</li></ul></li>

<li>Prepare File I/O for Unicode Compatibility</li>

<li>Tips on Supporting Supplementary Plane (UTF-32) Characters</li>

<li>Linker Errors Caused By Conflicting Definitions of wchar_t</li></ul></li>

<li>Frequently Asked Questions</li>

<li>Resources</li></ul></li></ul>

<p>Additionally I'd recommend using a tool called Visual Teefy, which is distributed via the ADN website. The name comes from the fact it helps add the T() macro into string literals (among other things), hence &quot;T()-ify&quot; =&gt; Teefy. The tool hooks off the Visual Studio IDE's search &amp; replace mechanism to find potentially problematic pieces of code, and provide suggestions on how to address them. It's not recommended to use the automatic settings (Teefy really just makes suggestions), as clearly a&nbsp; search &amp; replace-based tool is inevitably going to have trouble - as an example - differentiating a string literal in a compiler directive (such as #include &quot;acdb.h&quot; - which does not need any modification) from a string literal that does require the use of the T() macro.</p>

<p>As for other resources, I'd recommend searching the ADN KB, if you have access to it, or submitting your questions via the ObjectARX discussion group or DevHelp Online: at this stage it's almost certain that someone in our development community has experienced the same migration issues you're hitting, and it can certainly save time to ask.</p>
