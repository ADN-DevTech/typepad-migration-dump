---
layout: "post"
title: "DevCon 2025, Developer Guide, AI, AI, and AI"
date: "2025-05-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2026"
  - "AI"
  - "APS"
  - "DevCon"
  - "Docs"
  - "Events"
  - "MCP"
  - "Python"
  - "RAG"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2025/05/devcon-2025-developer-guide-ai-ai-and-ai.html "
typepad_basename: "devcon-2025-developer-guide-ai-ai-and-ai"
typepad_status: "Publish"
---

<p>I am in Amsterdam today, participating in the DevCon 2025.
I came here directly from Berlin, starting at 2 o'clock this morning.
Long day, much coffee, much excitement and adrenalin.
Here are some other topics I need to share:</p>

<ul>
<li><a href="#2">DevCon 2025 Amsterdam</a></li>
<li><a href="#3">Shared projects for multi-version add-in</a></li>
<li><a href="#4">Revit 2026 Developer Guide</a></li>
<li><a href="#5">Revit API and SDK documentation RAG</a></li>
<li><a href="#6">Revit-IFC DeepWiki</a></li>
<li><a href="#7">RevitGeminiRAG</a></li>
<li><a href="#8">Yet another Revit MCP</a></li>
<li><a href="#9">A2A and MCP towards implicit programming</a></li>
<li><a href="#10">AutoGenLib on-the-fly coding</a></li>
<li><a href="#11">M$ and Google code 30% AI-generated</a></li>
<li><a href="#12">AI masters geoguessing</a></li>
<li><a href="#13">Neuronal biology and AI algorithms</a></li>
<li><a href="#14">Induced atmospheric vibration</a></li>
</ul>

<h4><a name="2"></a> DevCon 2025 Amsterdam</h4>

<p>I am in Berlin on the way to the Autodesk DevCon 2025 in Amsterdam.
My last Autodesk and my last month of employment at Autodesk.
Looking forward to seeing what the new exciting times will bring for me personally and for the world at large.
Above all, I'm looking forward to meeting you here today and tomorrow!</p>

<h4><a name="3"></a> Shared Projects for Multi-Version Add-In</h4>

<p>A thread that We already mentioned
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on the <a href="https://forums.autodesk.com/t5/revit-api-forum/optimal-add-in-code-base-approach-to-target-multiple-revit/m-p/13614983">optimal add-in code base approach to target multiple Revit releases</a>.
It was resurrected
by Jeff <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/15371876">JeffroH</a> <a href="https://github.com/Hpad">Hpad</a> Hornsby, who suggests:</p>

<p>I'm surprised more developers aren't utilizing shared projects.
Shared projects seem to offer the greatest flexibility, in my opinion.
Maybe not too surprised since shared projects came out less than 10 years ago.
The AutoCAD API adheres better to established API design principles, particularly in not breaking existing code (changing signatures, etc.), and works really well with shared projects for multiple year releases.
Centralizing your code in a shared project and using each year's project solely for build configurations, references, build events, and related setup seems easier and more efficient in my opinion.
To demonstrate, I created
the <a href="https://github.com/Hpad/RevitSharedProject">RevitSharedProject example of a Revit shared project</a>.
I took the 2020 ExtensibleStorageManager and SchemaWrapperTools (referenced by ExtensibleStorageManager) projects from the SDK samples and made them ready to build for 2020-2025 by adding conditional compilation symbols to separate code changes between the years.
The only change is you will have to change build output path for the 2025 projects as the relative paths converted to full paths.
To add a 2026 project:</p>

<ul>
<li>Add new project using correct .NET framework (guessing .NET 8.0 for 2026)</li>
<li>Reference the RevitAPI.dll, RevitUIAPI.dll, the shared projects</li>
<li>Add Release and Debug conditional compilations symbols</li>
<li>Update code with symbols (e.g., Revit 2026) if required</li>
</ul>

<p>This suggestion sparked some controversial discussion, with other developers disagreeing with this approach, so check out the original thread for the full view.</p>

<p>Many thanks to Jeff for raising this.</p>

<h4><a name="4"></a> Revit 2026 Developer Guide</h4>

<p>This year's <a href="https://help.autodesk.com/view/RVT/2026/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html_">Revit API Developers Guide</a> is
now available.</p>

<h4><a name="5"></a> Revit API and SDK Documentation RAG</h4>

<p>We also already mentioned <a href="https://thebuildingcoder.typepad.com/blog/2025/03/revit-gen-ai-mcp-rag-and-vibe.html#4">converting Revit API help file to RAG for LLM</a> and
the corresponding <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/convert-revit-sdk-documentation-for-local-rag-aka-use-it-with-a/m-p/13624544#M85020">convert Revit SDK documentation for local RAG (aka use it with a LLM)</a>.</p>

<p><a href="https://chuongmep.com/">Chuong Ho</a> shared some new input on this as well:</p>

<blockquote>
  <p>I have done some with CHM files, cf. [RevitAPIDocGen][https://github.com/chuongmep/RevitAPIDocGen).</p>
  
  <p>The <a href="https://github.com/ADN-DevTech/revit-api-chms">revit-api-chms</a> repository contains many Revit API CHMs for processing.</p>
  
  <p>The <a href="https://github.com/DTDucas/chm-to-markdown">CHM to Markdown converter</a> provides
  another open source oiption for processing them.</p>
</blockquote>

<p>Thanks to Chuong Ho for sharing this.</p>

<h4><a name="6"></a> Revit-IFC DeepWiki</h4>

<p>I recently mentioned that
<a href="https://thebuildingcoder.typepad.com/blog/2025/04/revit-api-agents-mcp-copilot-and-codex.html#14">DeepWiki analyses all GitHub repos</a>
One nice Revit related example is provided by
the <a href="https://github.com/Autodesk/revit-ifc">Autodesk revit-ifc</a> project, producing
the <a href="https://deepwiki.com/Autodesk/revit-ifc">revit-ifc DeepWiki</a>.</p>

<h4><a name="7"></a> RevitGeminiRAG</h4>

<p>Most of the following is AI related, often in conjunction with the Revit API.</p>

<p><a href="https://www.linkedin.com/posts/ismailseleit_revitapi-opensource-ai-ugcPost-7323350614141247488-CuNn">Ismail Seleit on linkedin</a> shares: Really excited to share something I've been tinkering with: <a href="https://github.com/ismail-seleit/RevitGeminiRAG">RevitGeminiRAG is now out on GitHub</a>!
This project started with a simple question: How can we give LLMs full access to the Revit API so they become specialized Revit software developers and interact flawlessly with Revit?
RevitGeminiRAG is a Revit plugin that allows users to interact with their Revit models using natural language queries processed by Google's Gemini models.
It leverages a RAG system to provide the LLM with relevant context from the Revit API documentation, enabling it to understand and respond to Revit-specific requests more effectively.</p>

<h4><a name="8"></a> Yet Another Revit MCP</h4>

<p><a href="https://www.linkedin.com/posts/paulogiavoni_revit-bim-constructiontech-ugcPost-7323342978582757377-D6Bo">Paulo Giavoni on LinkedIn</a>:
Today I try a quick visualization technique for a client's Revit model using MCP Claude AI.
Would this be the end of BIM modelers?</p>

<h4><a name="9"></a> A2A and MCP Towards Implicit Programming</h4>

<p>Nine minutes with <a href="https://natebjones.com/">Nate B. Jones</a> (<a href="https://linktr.ee/natebjones">links</a>, <a href="https://natesnewsletter.substack.com/">substack</a>)
on <a href="https://youtu.be/cPdVbVx5Z3Q">MCP, A2A, and the Beginning of the End of Explicit Programming</a>:</p>

<p>My note on MCP: <a href="https://natesnewsletter.substack.com/p/how-i-think-about-mcp-a-practical">How I Think About MCP: A Practical Guide to Tool Use in AI Agents</a> takeaways:</p>

<ul>
<li>A New Software Substrate: Google’s A2A (agent-to-agent) protocol and Model Context Protocol (MCP) signal a shift from deterministic, pre-programmed systems to dynamic, emergent, agent-driven software architectures.</li>
<li>MCP Enables Capability Descriptions: Instead of hardcoding instructions, MCP allows developers to describe tool capabilities in structured ways, enabling AI to figure out how best to use them.</li>
<li>A2A Unlocks Agent Collaboration: A2A allows agents to discover, understand, and collaborate with other agents dynamically, forming workflows on the fly rather than following static rules.</li>
<li>New Challenges in State, Cost, and Security: Agent-based systems raise complex issues around state management, resource consumption from reasoning overhead, and new security risks like authorization and auditability.</li>
<li>Optimization Is No Longer Straightforward: Unlike traditional software where pathways are known, agent-based systems must optimize in unpredictable, dynamic contexts, requiring entirely new approaches.</li>
<li>Open Standards for the Agentic Era: A2A is built on open, observable, and debuggable standards like HTTP and JSON-RPC, inviting broader ecosystem collaboration and development.</li>
<li>Delegating to Intelligence, Not Software: The shift means intelligence becomes part of the software fabric. We no longer delegate tasks to predefined logic &ndash; we delegate them to autonomous agents capable of learning and adaptation.</li>
</ul>

<p>Quotes:</p>

<ul>
<li>“We’re not just adding new features to the same old software &ndash; we’re fundamentally rethinking what software can be.”</li>
<li>“Functionality isn’t just called, it’s negotiated now.”</li>
<li>“MCP gives agents the ability to understand and use tools; A2A gives them the ability to work together.”</li>
</ul>

<p>Summary:</p>

<p>Google’s A2A protocol and Model Context Protocol (MCP) mark a profound shift in AI architecture, moving us from deterministic software to dynamic, agent-based systems. MCP enables AI to understand and use tools without explicit instructions, while A2A allows agents to discover and collaborate with one another. This paradigm demands rethinking state management, security, and optimization strategies. It’s not just a technical evolution—it’s a redefinition of how we build and scale intelligent software. We are entering a world where we delegate to intelligence itself, not just code.</p>

<h4><a name="10"></a> AutoGenLib On-The-Fly Coding</h4>

<p>More automatic code generation tools &ndash; Vibe coding on steroids:</p>

<p><a href="https://github.com/cofob/autogenlib">AutoGenLib</a> is a Python library that automatically generates code on-the-fly using OpenAI's API. When you try to import a module or function that doesn't exist, AutoGenLib creates it for you based on a high-level description of what you need.</p>

<ul>
<li><strong>Dynamic Code Generation</strong>: Import modules and functions that don't exist yet</li>
<li><strong>Context-Aware</strong>: New functions are generated with knowledge of existing code</li>
<li><strong>Progressive Enhancement</strong>: Add new functionality to existing modules seamlessly</li>
<li><strong>No Default Caching</strong>: Each import generates fresh code for more varied and creative results</li>
<li><strong>Full Codebase Context</strong>: LLM can see all previously generated modules for better consistency</li>
<li><strong>Caller Code Analysis</strong>: The LLM analyzes the actual code that's importing the module to better understand the context and requirements</li>
<li><strong>Automatic Exception Handling</strong>: All exceptions are sent to LLM to provide clear explanation and fixes for errors.</li>
</ul>

<h4><a name="11"></a> M$ and Google Code 30% AI-Generated</h4>

<p>Already today,
<a href="https://techcrunch.com/2025/04/29/microsoft-ceo-says-up-to-30-of-the-companys-code-was-written-by-ai/">AI writes 30% of Code for both M$ and Google</a>.</p>

<h4><a name="12"></a> AI Masters Geoguessing</h4>

<p><a href="https://sampatt.com/blog/2025-04-28-can-o3-beat-a-geoguessr-master">o3 beats a master-level geoguesser player</a>,
in spite of attempts to trick it by adding fake EXIF data.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860ea9d32200b-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860ea9d32200b image-full img-responsive" alt="Geoguessing" title="Geoguessing" src="/assets/image_43d34c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="13"></a> Neuronal Biology and AI Algorithms</h4>

<p>AI can also help us better understand the functionality of real live neuronal biology,
by <a href="https://youtu.be/l-OLgbdZ3kk">learning algorithm of biological networks</a>.</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/l-OLgbdZ3kk?si=1zGVI3r2E_noC4GJ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</center></p>

<h4><a name="14"></a> Induced Atmospheric Vibration</h4>

<p>Finally, an interesting physics question that has nothing whatsoever to do with either Revit or AI,
discussing what took down the Iberian power system and explaining
<a href="https://physics.stackexchange.com/questions/848666/what-is-induced-atmospheric-vibration">What is "Induced Atmospheric Vibration"?</a></p>

<!--

####<a name="15"></a> [AI 2027](https://ai-2027.com/)

[AI 2027](https://ai-2027.com/)

> We predict that the impact of superhuman AI over the next decade will be enormous, exceeding that of the Industrial Revolution.
We wrote a scenario that represents our best guess about what that might look like.1 It’s informed by trend extrapolations, wargames, expert feedback, experience at OpenAI, and previous forecasting successes.2

-->
