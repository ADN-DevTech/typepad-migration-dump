---
layout: "post"
title: "Invoking commands in localized versions"
date: "2012-10-30 03:56:57"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/invoking-commands-in-localized-versions.html "
typepad_basename: "invoking-commands-in-localized-versions"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>You can append the following prefixes to all AutoCAD commands and options in order to avoid localization issues:</p>
<p>An underscore _<br />This calls the English version of the command. For example, the command, _LINE, can be issued from all localized releases and English AutoCAD.</p>
<p>A period . <br />This calls the original command when a command was redefined. For example, whena user redefines the LINE command. In such a case, _.line can be issued from all localized releases and English AutoCAD. It will always invoke the original LINE command.</p>
<p>A hyphen -<br />This calls the command-line version of the command (when available). For example, _.-layer calls the command line version of the original layer command in all AutoCAD releases, independent of the localization.</p>
