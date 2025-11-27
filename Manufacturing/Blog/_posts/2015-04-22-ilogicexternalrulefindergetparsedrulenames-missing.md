---
layout: "post"
title: "iLogic.ExternalRuleFinder.GetParsedRuleNames missing"
date: "2015-04-22 01:51:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/04/ilogicexternalrulefindergetparsedrulenames-missing.html "
typepad_basename: "ilogicexternalrulefindergetparsedrulenames-missing"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>iLogic.ExternalRuleFinder.GetParsedRuleNames</strong>&#0160;was never documented and has been moved to a different class and became internal (no longer public) in <strong>2016</strong>.&#0160;</p>
<p>Instead of using that function you can just do a scan of the <strong>iLogic</strong> external rule directories for rule files. The directory names are available in the <strong>iLogic API</strong> property <strong>IiLogicAutomation.FileOptions.ExternalRuleDirectories</strong>. Here’s a sample rule to list the directory names:</p>
<pre><strong>For</strong> <strong>Each</strong> directoryPath <strong>As</strong> String <strong>In</strong>
iLogicVb.Automation.FileOptions.ExternalRuleDirectories
  Trace.WriteLine(&quot;Directory name: &quot; + directoryPath)
<strong>Next</strong></pre>
<p>In Inventor <strong>2016</strong>&#0160;all the rules in these directories (and subdirectories, if any) are shown on the <strong>External Rules</strong> tab in the <strong>iLogic</strong> browser - in previous versions, only manually selected rules were shown.</p>
