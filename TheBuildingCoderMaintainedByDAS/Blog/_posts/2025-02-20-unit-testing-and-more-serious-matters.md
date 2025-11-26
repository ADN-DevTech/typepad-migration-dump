---
layout: "post"
title: "Unit Testing and More Serious Matters"
date: "2025-02-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "APS"
  - "Cloud"
  - "DA4R"
  - "IFC"
  - "Job"
  - "Performance"
  - "Philosophy"
  - "Python"
  - "Testing"
original_url: "https://thebuildingcoder.typepad.com/blog/2025/02/unit-testing-and-more-serious-matters.html "
typepad_basename: "unit-testing-and-more-serious-matters"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>So many exciting things going on, personally, globally, technically and politically:</p>

<ul>
<li><a href="#2">Life, death, turmoil</a></li>
<li><a href="#3">Retirement, recruiting, job offer</a></li>
<li><a href="#4">New Rev API docs</a></li>
<li><a href="#5">Ricaun.RevitTest unit testing framework</a></li>
<li><a href="#6">ForgeTypeId for other parameter group</a></li>
<li><a href="#7">Exporting IFC using DA4R</a></li>
<li><a href="#8">Using Revit API from a web app</a></li>
<li><a href="#9">GenAI impacts critical thinking</a></li>
<li><a href="#10">New AI AGI test suites</a></li>
<li><a href="#11">uv Python package and project manager</a></li>
</ul>

<h4><a name="2"></a> Life, Death, Turmoil</h4>

<p>I am somewhat in turmoil.
My daughter is imminently expecting a baby.
My brother is imminently dying.
The entire world seems to be in upheaval, politically, technologically, in polarisation between continents and socially.
I’m fine myself, physically.</p>

<p>The largest global upheaval that I see looming is the projection by
the <a href="https://thebuildingcoder.typepad.com/blog/2024/01/valid-revit-api-context-llm-and-ltg.html#7">1972 study on the limits to growth</a> that
I pointed out in January.</p>

<p>One sliver of hope on possibly handling the collapse that it predicts in the coming decades is offered
by <a href="https://blog.samaltman.com/three-observations">Sam Altman's Three Observations</a> on the rapid and accelerating AI evolution we are observing,
providing an exciting optimistic outlook into the near future.
He compares the development of AI
with <a href="https://en.wikipedia.org/wiki/Moore%27s_law">Moore's law</a>,
which notes that computing power increased exponentially by a factor of 2 every 18 months.
In AI development, Altman notes that the cost to use a given level of AI falls about 10x every 12 months, and lower prices lead to much more use, cf.,
<a href="https://thebuildingcoder.typepad.com/blog/2024/10/determine-rvt-version-and-add-data-from-exe.html#10">Jevons Paradox</a>.
He concludes:</p>

<blockquote>
  <p>Anyone in 2035 should be able to marshal the intellectual capacity equivalent to everyone in 2025; everyone should have access to unlimited genius to direct however they can imagine. There is a great deal of talent right now without the resources to fully express itself, and if we change that, the resulting creative output of the world will lead to tremendous benefits for us all.</p>
</blockquote>

<p>Let's hope that comes true.</p>

<!--

Sam Altman shared [Three Observations](https://blog.samaltman.com/three-observations) offering insights likely related to AI developments, industry trends, or human potential. The content emphasises the ongoing evolution and impact of technology.

Sam Altman shared [Three Observations](https://blog.samaltman.com/three-observations) offering insights likely related to AI developments, industry trends, or human potential. The content emphasises the ongoing evolution and impact of technology.

the most interesting observation, i think, is that there is an exponential development in AI, similar to Moore's law.

2x computation power every 18 months.

well, sam altmans says that the AI intelligence has a similar exponential growth, much more extreme:

10x intelligence increase every 12 months.

he says, by 2035, every human being will have more intelligence at their disposal that the entire humanity has today.

crazy prospect.

maybe that will help us handle &ndash; and solve? &ndash; the problems that we are scheduled to run into in the next couple of decades according to the limits to growth?

-->

<p>Today, only 199 human programmers are better than <code>o3</code>, and <code>r1</code> can produce the best kernel code, cf.,
<a href="https://buttondown.com/ainews/archive/ainews-reasoning-models-are-near-superhuman/">reasoning models are near-superhuman coders</a>:</p>

<ul>
<li>RL is all you need</li>
<li>o3 achieves a gold medal at the 2024 IOI and obtains a Codeforces rating on par with elite human competitors &ndash; in particular, the Codeforces score is at the 99.8-tile</li>
<li>In Automating GPU Kernel Generation with DeepSeek-R1 and Inference Time Scaling, Nvidia found that DeepSeek r1 could write custom kernels that "turned out to be better than the optimized kernels developed by skilled engineers in some cases"; in the Nvidia case, the solution was also extremely simple, causing much consternation.</li>
</ul>

<p>A number of developers reacted to an initial post
saying <a href="https://www.reddit.com/r/ChatGPT/comments/1iosoyp/im_in_my_50s_and_i_just_had_chatgpt_write_me_a/?rdt=52104">I'm in my 50's and I just had ChatGPT write me a javascript/html calculator for my website. I'm shook.</a>.</p>

<p>Exciting times indeed, with huge changes ahead.</p>

<h4><a name="3"></a> Retirement, Recruiting, Job Offer</h4>

<p>I will be retiring before those calamities arrive.</p>

<p>We are recruiting a replacement for me.
The replacement should be based in Europe.
Here is the public job posting for a <a href="https://autodesk.wd1.myworkdayjobs.com/en-US/Ext/job/Senior-Developer-Advocate-Engineer_25WD85215-2">Senior Developer Advocate Engineer</a>.</p>

<p>If you are interested in this opportunity, I suggest you do not apply directly through the link above.
Instead, send me a message to my Autodesk email address and let me know your contact details.
Then, I can submit a referral for you, and the recruiters will contact you directly.</p>

<p>Good luck!</p>

<h4><a name="4"></a> New Rev API Docs</h4>

<p>I just discovered a new online version of the Revit API documentation,
<a href="https://revapidocs.com/">Rev API docs</a> &ndash; <a href="https://revapidocs.com/">revapidocs.com</a>.</p>

<p>It was created by the Revit API consulting company <a href="https://nonica.io/">Nonica.io</a>.
It includes coverage for the Revit 2025 API, which was (and still is) lacking in <a href="https://www.revitapidocs.com/">revitapidocs.com</a>.</p>

<p>I like it.
It is free of advertising.
It is fast.
It has good search functionality with immediate feedback.</p>

<p>Many thanks to Nonica.io for creating and sharing this resource.</p>

<h4><a name="5"></a> Ricaun.RevitTest Unit Testing Framework</h4>

<p>I discussed
the <a href="https://github.com/ricaun-io/ricaun.RevitTest">ricaun.RevitTest unit testing framework</a>
with Luiz Henrique <a href="https://ricaun.com/">@ricaun</a> Cassettari to clarify some aspects; he says:</p>

<p>When I started researching about unit tests inside Revit, I had a hard time setting up
the <a href="https://github.com/DynamoDS/RevitTestFramework">DynamoDS/RevitTestFramework</a> inside my Revit;
the project looks abandoned, and the last updates are six years old.</p>

<p>In the end, I started using
the <a href="https://github.com/geberit/Revit.TestRunner">geberit/Revit.TestRunner</a> version
that was a little easier to install.
I submitted PRs to fix some issues, and the project is alive on GitHub and supports more recent versions of Revit.</p>

<p>When I started using/playing with the <a href="https://aps.autodesk.com/">Autodesk Platform Services APS</a>
<a href="https://aps.autodesk.com/design-automation-apis">Design Automation API for Revit, DA4R</a>,
I also wanted to be able to use DA4R to run tests use inside a GitHub Action.</p>

<p>That was the main goal: run tests using both Revit for Desktop and Revit for Design Automation.</p>

<p>Plus, I found a way to use the default Test Explorer inside Visual Studio to run tests inside Revit.</p>

<p>No need to manually install the plugin in the machine:
the <code>ricaun.RevitTest.TestAdapter</code> does the work to install the plugin in the machine, find Revit folder based on the Revit installation, open Revit, run the tests, show the results inside Visual Studio and close Revit.</p>

<p>It is easy and convenient; you can download the sample project and just run the tests directly inside Visual Studio.</p>

<ul>
<li><a href="https://github.com/ricaun-io/RevitTest">github.com/ricaun-io/RevitTest</a></li>
</ul>

<p>Furthermore, knowing that the Revit 2025 API was based on .NET Core, I designed the whole project with that in mind.</p>

<p>Supporting Revit versions from 2019 to 2025, and also, yes, ricaun.RevitTest works with the Revit Preview.</p>

<p>For running tests in DA4R, I still need to share the main project,
<a href="https://github.com/ricaun-io/ricaun.DA4R.NUnit">ricaun-io/ricaun.DA4R.NUnit</a>.</p>

<p>I have a class session coming up
at <a href="https://aps.autodesk.com/topics/autodesk-devcon">Autodesk DevCon Europe</a> this year,
taking place on May 20–21 2025 in Amsterdam; that's gonna be fun:</p>

<ul>
<li><a href="https://events.autodesk.com/flow/autodesk/devcon25emea/mainevent/page/agenda/session/1734703627846001oL4U">Multi-Version RevitTest Framework: Unit Testing Revit API using Design Automation</a></li>
</ul>

<blockquote>
  <p>In the class, "Multi-Version RevitTest Framework: Unit Testing Revit API using Design Automation." you'll learn the intricacies of executing unit tests for Revit add-ins both locally and remotely. Using the versatile RevitTest Framework, you'll discover how to create consistent and reliable unit tests that can be run on your local machine as well as through Design Automation for Revit. Elevate your unit testing practices with Revit API by joining us and unlock the potential of running tests for multiple Revit versions using a unique and unified RevitTest Framework.</p>
</blockquote>

<p>The ricaun.RevitTest features include:</p>

<ul>
<li>Support multiple Revit versions (2019-2025) (Revit Preview)</li>
<li>Run inside Visual Studio Test Explorer (dotnet test)</li>
<li>Do not require any manual plugin installation.</li>
<li>Support to run tests using Design Automation for Revit.</li>
</ul>

<p>Have a great week!</p>

<p>Many thanks to ricaun for the very helpful and detailed in-depth explanation!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3cc780b200c-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3cc780b200c image-full img-responsive" alt="RevitTest.Feature.Open.Close" title="RevitTest.Feature.Open.Close" src="/assets/image_f6a44c.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p><a href="https://github.com/jeremytammik/tbc/tree/gh-pages/a/img/ricaun_revittest.gif"><p style="font-size: 80%; font-style:italic">Click for animation</p></a></p>

<p></center></p>

<h4><a name="6"></a> ForgeTypeId for Other Parameter Group</h4>

<p>Andrea <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/8492813">@andrea.tassera</a> Tassera shared a new insight on
the <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-2024-other-parameter-group/m-p/13326968#M83989">Revit 2024 <code>Other</code> parameter group</a>:</p>

<blockquote>
  <p>Apparently, using</p>

<pre><code class="language-cs">new ForgeTypeId(string.Empty)</code></pre>
  
  <p>only works for Revit 2024 and above.
  I was just testing what's on this post in my code, and it was working with Revit 2025, but not in Revit 2023.
  The <code>ForgeTypeId</code> change seems to be applied from Revit 2021 onwards, so I thought it was strange that it wasn't working in 2023.
  I did some experimentation and if you use <code>null</code> instead of <code>new ForgeTypeId(string.Empty)</code>, then it works in all versions of Revit.
  Thought you guys might be interested &nbsp; :-)</p>
</blockquote>

<p>Thank you, Andrea, for sharing this.</p>

<h4><a name="7"></a> Exporting IFC Using DA4R</h4>

<p>Eason Kang published two blog posts about exporting IFC using DA4R,
the Autodesk Platform Services APS <a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/">Design Automation for Revit API</a>:</p>

<ul>
<li><a href="https://aps.autodesk.com/blog/export-ifc-rvt-using-design-automation-api-revit-part-i">Export IFC from RVT using Design Automation API for Revit &ndash; Part I</a></li>
<li><a href="https://aps.autodesk.com/blog/export-ifc-rvt-using-design-automation-api-revit-part-ii">Export IFC from RVT using Design Automation API for Revit &ndash; Part II</a></li>
</ul>

<h4><a name="8"></a> Using Revit API from a Web App</h4>

<p>People regularly ponder driving Revit from outside, and now this question came up again,
how to <a href="https://forums.autodesk.com/t5/revit-api-forum/use-revit-api-from-a-web-app/m-p/13314320">use Revit API from a web app</a>:</p>

<p><strong>Question:</strong>
Is it possible to create a web app that manipulates a simple object in Revit "Beam for example".</p>

<p><strong>Answer:</strong>
The pure Revit API is a Windows .NET API that requires a running session of Revit and a valid Revit API context to execute, which is only provided within a running session of Revit.exe on a Windows desktop PC.
You can however also use the Revit API in the cloud on a virtual machine directly from a web app by making use of the Autodesk Platform Services <a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/">APS Design Automation for Revit API</a>.</p>

<p>Chuong Ho adds another idea to play with:</p>

<ul>
<li>Build the add-in and open a listener to get data from Revit:
<a href="https://github.com/BIMrxLAB/Revit2GraphQL">Revit2GraphQL</a>;
this Revit project contains a GraphQL endpoint that can be accessed locally as well as remotely over the web.
Check out <a href="https://github.com/gregorvilkner/Revit2GraphQL/blob/master/BIMrx.Marconi%20SinglePage%201.1.pdf">BIMrx.Marconi.pdf</a> for more information.</li>
</ul>

<p>Chuong also points out this tutorial for getting started with 
the <a href="https://aps.autodesk.com/">Autodesk Platform Services APS</a>
<a href="https://aps.autodesk.com/design-automation-apis">Design Automation API for Revit, DA4R</a>,
to update a Revit Model 
in <a href="https://construction.autodesk.com/">ACC, the Autodesk Construction Cloud</a>:</p>

<ul>
<li><a href="https://dev.to/chuongmep/use-revit-design-automation-update-revit-model-in-acc-part-1-1o31">Use Revit Design Automation Update Revit Model In ACC Part 1</a></li>
<li><a href="https://dev.to/chuongmep/use-revit-design-automation-update-revit-model-in-acc-part-2-58kc">Use Revit Design Automation Update Revit Model In ACC Part 2</a></li>
</ul>

<p>Thank you for the pointers, Chuong!</p>

<h4><a name="9"></a> GenAI Impacts Critical Thinking</h4>

<p>Back to AI-related topics, a report
on <a href="https://www.microsoft.com/en-us/research/uploads/prod/2025/01/lee_2025_ai_critical_thinking_survey.pdf">the impact of generative AI on critical thinking: self-reported reductions in cognitive effort and confidence effects from a survey of knowledge workers</a>:</p>

<blockquote>
  <p>The rise of Generative AI (GenAI) in knowledge workflows raises questions about its impact on critical thinking skills and practices. We survey 319 knowledge workers to investigate 1) when and how they perceive the enaction of critical thinking when using GenAI, and 2) when and why GenAI affects their effort to do so. Participants shared 936 first-hand examples of using GenAI in work tasks. Quantitatively, when considering both task- and user-specific factors, a user’s task-specific self-confidence and confidence in GenAI are predictive of whether critical thinking is enacted and the effort of doing so in GenAI-assisted tasks. Specifically, higher confidence in GenAI is associated with less critical thinking, while higher self-confidence is associated with more critical thinking. Qualitatively, GenAI shifts the nature of critical thinking toward information verification, response integration, and task stewardship. Our insights reveal new design challenges and opportunities for developing GenAI tools for knowledge work.</p>
</blockquote>

<h4><a name="10"></a> New AI AGI Test Suites</h4>

<p>As we seem to be nearing AGI, it is interesting to look at more challenging tests that the current LLMs cannot yet handle:</p>

<ul>
<li><a href="https://lastexam.ai/">Humanity's Last Exam</a></li>
<li><a href="https://scale.com/research/enigma_eval">EnigmaEval: A Benchmark of Long Multimodal Reasoning Challenges</a></li>
</ul>

<h4><a name="11"></a> uv Python Package and Project Manager</h4>

<p>Do you work with Python?
If so, the following tool may be of interest:
<a href="https://docs.astral.sh/uv/">uv</a>, an extremely fast Python package and project manager sporting the following impressive features:</p>

<ul>
<li>A single tool to replace pip, pip-tools, pipx, poetry, pyenv, twine, virtualenv, and more.</li>
<li>10-100x faster than pip.</li>
<li>Provides comprehensive project management, with a universal lockfile.</li>
<li>Runs scripts, with support for inline dependency metadata.</li>
<li>Installs and manages Python versions.</li>
<li>Runs and installs tools published as Python packages.</li>
<li>Includes a pip-compatible interface for a performance boost with a familiar CLI.</li>
<li>Supports Cargo-style workspaces for scalable projects.</li>
<li>Disk-space efficient, with a global cache for dependency deduplication.</li>
<li>Installable without Rust or Python via curl or pip.</li>
<li>Supports macOS, Linux, and Windows.</li>
</ul>

<p>If you like how blazingly fast it is, you might be interested in learning
the <a href="https://youtu.be/gSKTfG1GXYQ">smart engineering behind it (40 minute video)</a>.
It also enables some interesting ways to run scripts, cf.,
some <a href="https://youtu.be/jXWIxk2brfk">tricks with UV and a new Python project: uvtrick</a>.</p>
