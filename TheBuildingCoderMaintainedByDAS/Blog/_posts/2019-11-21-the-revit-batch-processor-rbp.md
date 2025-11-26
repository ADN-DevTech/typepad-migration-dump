---
layout: "post"
title: "The Revit Batch Processor RBP"
date: "2019-11-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Batch"
  - "Dynamo"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/11/the-revit-batch-processor-rbp.html "
typepad_basename: "the-revit-batch-processor-rbp"
typepad_status: "Publish"
---

<p>There are so many truly wonderful pieces of software sitting around there that I am unaware of, real works of art, pinnacles of perfection, that I only happen upon by chance.</p>

<p>In this case, <em>@alexandrecafarofr</em> happens to mention a powerful utility in 
his <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/automatic-heating-and-cooling-load-analysis-with-revit-api/m-p/9149375">automatic heating and cooling load analysis</a>:</p>

<p>The <a href="https://github.com/bvn-architecture/RevitBatchProcessor">Revit Batch Processor (RBP)</a> a project maintained
by <a href="https://github.com/DanRumery">Daniel Rumery</a>, ex-<a href="http://www.bvn.com.au">BVN</a>:</p>

<ul>
<li><a href="#2">Revit Batch Processor (RBP)</a></li>
<li><a href="#3">Latest version</a></li>
<li><a href="#4">FAQ</a></li>
<li><a href="#5">Use cases</a></li>
<li><a href="#6">Features</a></li>
<li><a href="#7">Unlimited power</a></li>
<li><a href="#8">Addendum &ndash; questions</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4ed719a200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4ed719a200b img-responsive" style="width: 150px; display: block; margin-left: auto; margin-right: auto;" alt="Captain Dan" title="Captain Dan" src="/assets/image_3041dd.jpg" /></a><br /></p>

<p></center></p>

<p>Here is an excerpt of the GitHub project readme to make your mouth water:</p>

<h4><a name="2"></a> Revit Batch Processor (RBP)</h4>

<p>Fully automated batch processing of Revit files with your own Python or Dynamo task scripts!</p>

<h4><a name="3"></a> Latest version</h4>

<p><a href="https://github.com/bvn-architecture/RevitBatchProcessor/releases/download/v1.5.3/RevitBatchProcessorSetup.exe">Installer for Revit Batch Processor v1.5.3</a></p>

<p>See the <a href="https://github.com/bvn-architecture/RevitBatchProcessor/releases">Releases</a> page for more information.</p>

<h4><a name="4"></a> FAQ</h4>

<p>See the <a href="https://github.com/bvn-architecture/RevitBatchProcessor/wiki/Revit-Batch-Processor-FAQ">Revit Batch Processor FAQ</a>.</p>

<h4><a name="5"></a> Use Cases</h4>

<p>This tool doesn't <em>do</em> any of these things, but it <em>allows</em> you to do them:</p>

<ul>
<li>Open all the Revit files across your Revit projects and run a health-check script against them. Keeping an eye on the health and performance of many Revit files is time-consuming. You could use this to check in on all your files daily and react to problems before they get too gnarly.</li>
<li>Perform project and family audits across your Revit projects.</li>
<li>Run large scale queries against many Revit files.</li>
<li>Mine data from your Revit projects for analytics or machine learning projects.</li>
<li>Automated milestoning of Revit projects.</li>
<li>Automated housekeeping tasks (e.g. place elements on appropriate worksets)</li>
<li>Batch upgrading of Revit projects and family files.</li>
<li>Testing your own Revit API scripts and Revit addins against a variety of Revit models and families in an automated manner.</li>
<li>Essentially anything you can do to one Revit file with the Revit API or a Dynamo script, you can now do to many!</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a49f85f8200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a49f85f8200c image-full img-responsive" alt="Revit batch processor user interface" title="Revit batch processor user interface" src="/assets/image_b01d89.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="6"></a> Features</h4>

<ul>
<li>Batch processing of Revit files (.rvt and .rfa files) using either a specific version of Revit or a version that matches the version of Revit the file was saved in. Currently supports processing files in Revit versions 2015 through 2020. (Of course, the required version of Revit must be installed!)</li>
<li>Custom task scripts written in Python or Dynamo! Python scripts have full access to the Revit API. Dynamo scripts can of course do whatever Dynamo can do :)</li>
<li>Option to create a new Python task script at the click of a button that contains the minimal amount of code required for the custom task script to operate on an opened Revit file. The new task script can then easily be extended to do some useful work. It can even load and execute your existing functions in a C# DLL (see <a href="#executing-functions-in-a-c-dll">Executing functions in a C# DLL</a>).</li>
<li>Option for custom pre- and post-processing task scripts. Useful if the overall batch processing task requires some additional setup / tear down work to be done.</li>
<li>Central file processing options (Create a new local file, Detach from central).</li>
<li>Option to process files (of the same Revit version) in the same Revit session, or to process each file in its own Revit session. The latter is useful if Revit happens to crash during processing, since this won't block further processing.</li>
<li>Automatic Revit dialog / message box handling. These, in addition to Revit error messages are handled and logged to the GUI console. This makes the batch processor very likely to complete its tasks without any user intervention required!</li>
<li>Ability to import and export settings. This feature combined with the simple <a href="#command-line-interface">command-line interface</a> allows for batch processing tasks to be setup to run automatically on a schedule (using the Windows Task Scheduler) without the GUI.</li>
<li>Generate a .txt-based list of Revit model file paths compatible with RBP. The <em>New List</em> button in the GUI will prompt for a folder path to scan for Revit files. Optionally you can specify the type of Revit files to scan for and also whether to include subfolders in the scan.</li>
</ul>

<h4><a name="7"></a> Unlimited Power</h4>

<blockquote>
  <p>"With great power come great responsibility"
  <a href="https://quoteinvestigator.com/2015/07/23/great-power/">-- Spiderman</a></p>
</blockquote>

<p>This tool enables you to do things with Revit files on a very large scale. Because of this ability, Python or Dynamo scripts that make modifications to Revit files (esp. workshared files) should be developed with the utmost care! You will need to be confident in your ability to write Python or Dynamo scripts that won't ruin your files en-masse. The Revit Batch Processor's 'Detach from Central' option should be used both while testing and for scripts that do not explicitly depend on working with a live workshared Central file.</p>

<p>Ever so many thanks to Dan for implementing, sharing, and, above all, so wonderfully documenting and maintaining this very impressive and powerful application!</p>

<h4><a name="8"></a> Addendum &ndash; Questions</h4>

<p>We have discussed similar batch processing topics here in the past, e.g.:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2015/08/batch-processing-dwfx-links-and-future-proofing.html#4">Batch processing Revit documents</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/batch-processing-and-aspects-of-asstringvalue.html#2">Batch processing Revit families and documents</a></li>
</ul>

<p>Based on that, I raised <a href="https://github.com/bvn-architecture/RevitBatchProcessor/issues/51">issue #51</a> to
ask some important questions on failure handling and recovery:</p>

<ol>
<li>Are the actions taken automatically logged?
&ndash; Why? In case of a problem, it might be handy to know how far you got successfully before the problem appeared, so that you know where to pick up again.</li>
<li>Is the Revit.exe health monitored?
&ndash; Why? Well, Revit is not built for batch processing, so one might expect it to crash and die if it is force fed too many files in a single session.</li>
<li>Is Revit restarted for each new document? That would reduce the risk of the aforementioned point.</li>
</ol>

<p>Dan very kindly responds:</p>

<ol>
<li>There is a log file that records the console output seen in the GUI. By default, log files by default are written to the folder %APPDATA%\BatchRvt. It logs stuff like process Revit startup and monitoring, files processed, file actions (open, closing, detach, file type, path, etc.). Any custom actions performed by a script would be left up to the script to record action-specific progress. If using a Python script, there is a utility function <code>Output</code> for logging to RBP's console. Dynamo scripts need their own mechanism for logging.</li>
<li>The Revit process(es) are monitored to detect process exit and process busy events. This info is logged to the console. If a Revit session crashes, RBP starts up a fresh Revit session for any files that remain to be processed (so only the Revit file that lead to the crash will be skipped). Indeed, I found this can happen when processing a large amount of Revit files, hence the recovery mechanism. You'll see in the UI there is an option to process the files in the same Revit session (when possible) or process every file in its own session; as far as crash recovery is concerned, however, the behaviour is the same. Note that Dynamo scripts are always made to run in a separate Revit session per file due to some limitations I encountered when closing an active UI document. Python scripts don't have this limitation and hence will process a lot faster!</li>
<li>See 2) :)</li>
</ol>

<p>There is also a per-file time-out feature that can be used to terminate processing of a file if the time limit is reached.
In that scenario, the Revit process is forcibly terminated and a fresh session started up to process the remaining files.</p>
