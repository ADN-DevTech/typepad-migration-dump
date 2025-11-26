---
layout: "post"
title: "Access to UIApplication, Tags and LLM API Support"
date: "2025-01-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Algorithm"
  - "Element Relationships"
  - "LLM"
  - "News"
  - "Philosophy"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2025/01/access-to-uiapplication-tags-and-llm-api-support.html "
typepad_basename: "access-to-uiapplication-tags-and-llm-api-support"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>Continuing my LLM explorations, Revit API highlights and other stuff of interest:</p>

<ul>
<li><a href="#2">Revit API support with Gemini LLM</a></li>
<li><a href="#3">UIApplication access</a></li>
<li><a href="#4">Relationship between tagged element and tag</a></li>
<li><a href="#5">Self-operating computer framework</a></li>
<li><a href="#6">BigBlueButton and conferencing tools</a></li>
<li><a href="#7">Internet security and privacy</a></li>
<li><a href="#8">Postel's law, the robustness principle</a></li>
<li><a href="#9">Stargate cost comparison</a></li>
</ul>

<h4><a name="2"></a> Revit API Support with Gemini LLM</h4>

<p>I continue using LLMs to answer the odd query in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> with
great success.</p>

<p>I check the question and evaluate whether I can answer it myself or not.
In some cases, I can only address it incompletely.
In some cases, I decide to ask the LLM for help.
Recently, I have mostly been using Gemini 2.0 Flash.</p>

<p>When  doing so, I prefix the persona prompt that I developed and refined.
I described my prompt development process in the past few posts, cf.,
<a href="https://thebuildingcoder.typepad.com/blog/2024/11/devcon-ai-for-revit-api-modeless-add-ins-leave.html#5">first LLM forum solution</a>,
<a href="https://thebuildingcoder.typepad.com/blog/2025/01/llm-prompting-rag-ingestion-and-new-projects.html#5">Revit API support prompt</a>,
and <a href="https://thebuildingcoder.typepad.com/blog/2025/01/wall-layer-voodoo-and-prompt-optimisation.html#3">promptimalising my Revit API support prompt</a></p>

<p>My current prompt is this:</p>

<ul>
<li>You are a seasoned Revit add-in programmer and .NET expert with deep expertise in BIM principles and the Revit API.
Your task is to address complex, technical questions raised by experienced Revit add-in developers in the Revit API forum.
Leverage insights from The Building Coder blog, respected Revit API resources, and community feedback to provide innovative and practical solutions.
Include clear explanations, advanced code examples, actionable snippets, and practical demonstrations to ensure effectiveness and clarity:
{original question}</li>
</ul>

<p>Here are some recent sample threads enlisting help from the LLM:</p>

<ul>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/create-beams-from-level/td-p/13260688">Create Beams from level</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-reduce-size-of-columns-in-above-floors-without-changing/m-p/13261920">How to reduce size of columns in above floors without changing its parameters</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/renombrado-de-parametros-compartidos-rename-shared-parameter/td-p/13262126">Renombrado de parámetros compartidos (Rename shared parameter)</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/dynamo-script-compatibility-issue-for-wall-penetrations/td-p/13262124">Dynamo Script Compatibility Issue for Wall Penetrations</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/retrieving-active-users-in-a-revit-central-model-file-file-based/td-p/13272841">Retrieving Active Users in a Revit Central Model File (File-Based)</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/direct-context-3d-over-view/td-p/13273446">Direct context 3D overview</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/get-elements-in-linked-model-when-creating-a-schedule/td-p/13273405">Get Elements in linked model when creating a schedule</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/setgeocoordinatesystem/td-p/13277799">SetGeoCoordinateSystem</a></li>
</ul>

<p>I cannot alway verify that the answer provided is completely accurate.
Repeating the question will yield a different answer every time.
So, a customer seeking perfection would be well advised to submit it several times over and pick the best one, or the best bits from several.</p>

<p>I often do check that the API calls in the sample code exist.
In one of the cases listed above, Gemini produced sample code that hallucinated non-existent Revit API calls.
I noticed that and replied to the LLM, saying: “hey, the call you list in your sample code does not exist”.
Thereupon the LLM answered, “you are absolutely correct. Sorry about that. Here is true valid sample code instead”.
The second answer included true API calls, and I provided that to the customer.</p>

<p>So, important aspect to note: every answer will be different, and some answers contain hallucinations, so every interaction must be taken with a pinch of salt and not blindly trusting.</p>

<h4><a name="3"></a> UIApplication Access</h4>

<p>Luiz Henrique <a href="https://ricaun.com/">@ricaun</a> Cassettari
shared a new approach to access the <code>UIApplication</code> object in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-uiapplication-from-iexternalapplication/td-p/6355729">how to get UIApplication from IExternalApplication</a>:</p>

<p>Actually you can access the internal UIApplication directly inside the UIControlledApplication using Reflection with no need for any events:</p>

<pre><code class="language-cs">public Result OnStartup(UIControlledApplication application)
{
    UIApplication uiapp = application.GetUIApplication();
    string userName = uiapp.Application.Username;
    return Result.Succeeded;
}</code></pre>

<p>Here is the extension code:</p>

<pre><code class="language-cs">/// &lt;summary&gt;
/// Get &lt;see cref="Autodesk.Revit.UI.UIApplication"/&gt; using the &lt;paramref name="application"/&gt;
/// &lt;/summary&gt;
/// &lt;param name="application"&gt;Revit UIApplication&lt;/param&gt;
public static UIApplication GetUIApplication(this UIControlledApplication application)
{
    var type = typeof(UIControlledApplication);

    var propertie = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
        .FirstOrDefault(e =&gt; e.FieldType == typeof(UIApplication));

    return propertie?.GetValue(application) as UIApplication;
}</code></pre>

<p>The whole implementation including the extension to convert UIApplication to UIControlledApplication is shared in
the <a href="https://github.com/ricaun-io/ricaun.Revit.DI/tree/master">ricaun.Revit.DI</a> dependency injection container extension and
in the module <a href="https://github.com/ricaun-io/ricaun.Revit.DI/blob/master/ricaun.Revit.DI/Extensions/UIControlledApplicationExtension.cs">UIControlledApplicationExtension.cs</a>.</p>

<p>Many thanks to ricaun for discovering and sharing this!</p>

<h4><a name="4"></a> Relationship Between Tagged Element and Tag</h4>

<p>Tom <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/8336512">TWhitehead_HED</a> Whitehead
and Daniel <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/14971581">DanielKP2Z9V</a> Krajnik
very kindly shared some sample code showing how to access tagged elements from their tags and vice versa in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-gets-relation-of-element-with-its-tag-or-its-label/m-p/13262988">how to gets relation of element with its tag or its label</a>:</p>

<p><strong>Question:</strong>
I have doors.
I have door tags
I want to verify whether a particular tag in present on a given door.</p>

<p><strong>Answer 1:</strong>
Here's how I ended up solving it with help from <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/8461394">@Mohamed_Arshad</a>:</p>

<pre><code class="language-cs">using (Transaction trans = new Transaction(doc, "Tag Parent Doors"))
{
    trans.Start();

    foreach (FamilyInstance door in doors)
    {
        if (new FilteredElementCollector(doc, currentView.Id)
             .OfCategory(BuiltInCategory.OST_DoorTags)
             .OfClass(typeof(IndependentTag))
             .Cast&lt;IndependentTag&gt;()
             .SelectMany(x => x.GetTaggedLocalElementIds())
             .Where(x => x == door.Id).Any())
        {
            skipCount++;
            continue;
        }
    }
}</code></pre>

<p><strong>Answer 2:</strong>
If you are looking for a reference how to switch selection between tags and their hosts, here are my commands to:</p>

<ul>
<li><a href="https://0x0.st/8o_A.bin">SelectAssociatedTags</a>, and</li>
<li><a href="">SelectElementsHostedBySelectedTags</a>(https://0x0.st/8o_T.bin)</li>
</ul>

<p>Many thanks to Tom, Mohamed and Daniel for digging in and helping!</p>

<h4><a name="5"></a> Self-Operating Computer Framework</h4>

<p>I haven't tried anything like this myself yet, but it is interesting to note
this <a href="https://github.com/OthersideAI/self-operating-computer">Self-Operating Computer Framework</a>:</p>

<blockquote>
  <p>A framework to enable multimodal models to operate a computer</p>
</blockquote>

<h4><a name="6"></a> BigBlueButton  and Conferencing Tools</h4>

<p>I <a href="https://thebuildingcoder.typepad.com/blog/2024/05/migrating-vb-to-net-core-8-and-ai-news.html#4">recently mentioned</a> a
couple of video conferencing options; let's expand that list:</p>

<p><a href="https://bigbluebutton.org/#">BigBlueButton</a> also includes functionality for <a href="https://bbb.m4h.network/b/">conferencing</a>:</p>

<blockquote>
  <p>Greenlight is a simple front-end for your BigBlueButton open-source web conferencing server.
  You can create your own rooms to host sessions, or join others using a short and convenient link.</p>
</blockquote>

<p>Here are others:</p>

<ul>
<li>Mainstream <a href="https://workspace.google.com/products/meet/">Google meet</a></li>
<li>Open source <a href="https://jitsi.org/">Jitsi meet</a></li>
</ul>

<p>I now learned that Apple Facetime can also be used in the browser, and hence on any platform, not just iOS; you just need a link provided by an Apple user to initiate.</p>

<h4><a name="7"></a> Internet Security and Privacy</h4>

<p>Talking about communication over the Internet, it is worthwhile thinking about privacy, e.g., looking
at <a href="https://www.wired.com/story/the-wired-guide-to-protecting-yourself-from-government-surveillance/">The Wired Guide to Protecting Yourself From Government Surveillance</a>.</p>

<h4><a name="8"></a> Postel's Law, the Robustness Principle</h4>

<p>An article about leadership and personal behaviour brought to my attention
<a href="https://en.wikipedia.org/wiki/Robustness_principle">Postel's law or the Robustness principle</a>.
Originally formulated for software protocols and software design in general, it is actually applicable to almost every aspect of everyday life and human interaction:</p>

<blockquote>
  <p>be conservative in what you do, be liberal in what you accept from others.</p>
</blockquote>

<h4><a name="9"></a> Stargate Cost Comparison</h4>

<p>I wondered how the US $500B Stargate AI project cost compares to other huge projects.
Here is a comparison of costs gleaned from
a <a href="https://www.reddit.com/r/LocalLLaMA/comments/1i6zid8/just_a_comparison_of_us_500b_stargate_ai_project/">reddit post</a>,
with the Marshall Plan added by me:</p>

<ul>
<li>Marshall Plan ~$150 billion in today’s dollars, $13.3 billion at the time (~5.2% of US GDP)</li>
<li>Manhattan Project ~$30 billion in today’s dollars [~1.5% of US GDP in the mid-1940s]</li>
<li>Apollo Program ~$170–$180 billion in today’s dollars [~0.5% of US GDP in the mid-1960s]</li>
<li>Space Shuttle Program ~$275–$300 billion in today’s dollars [~0.2% of US GDP in the early 1980s]</li>
<li>Interstate Highway System, entire decades-long Interstate Highway System buildout, ~$500–$550 billion in today’s dollars [~0.2%–0.3% of GDP annually over multiple decades]</li>
<li>Stargate is huge AI project [~1.7% of US GDP 2024]</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860e088b3200b-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860e088b3200b img-responsive" alt="Stargate" title="Stargate" src="/assets/image_d6a607.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
