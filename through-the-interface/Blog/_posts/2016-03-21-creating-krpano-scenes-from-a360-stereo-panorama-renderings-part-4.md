---
layout: "post"
title: "Creating krpano scenes from A360 stereo panorama renderings &ndash; Part 4"
date: "2016-03-21 16:38:47"
author: "Kean Walmsley"
categories:
  - "Virtual Reality"
original_url: "https://www.keanw.com/2016/03/creating-krpano-scenes-from-a360-stereo-panorama-renderings-part-4.html "
typepad_basename: "creating-krpano-scenes-from-a360-stereo-panorama-renderings-part-4"
typepad_status: "Publish"
---

<p>Right then, time for the big reveal. We’ve had <a href="http://through-the-interface.typepad.com/through_the_interface/2016/03/creating-krpano-scenes-from-a360-stereo-panorama-renderings-part-1.html" target="_blank">three</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2016/03/creating-krpano-scenes-from-a360-stereo-panorama-renderings-part-2.html" target="_blank">parts</a> in this <a href="http://through-the-interface.typepad.com/through_the_interface/2016/03/creating-krpano-scenes-from-a360-stereo-panorama-renderings-part-3.html" target="_blank">series</a> discussing one basic approach for aggregating stereo panoramas from A360 cloud rendering into <a href="http://keanw.com/a360vr" target="_blank">a site that uses krpano</a>.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb08cbfbd0970d-pi" target="_blank"><img style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border: 0px;" title="Welcome view of the krpano sample site" src="/assets/image_498830.jpg" alt="Welcome view of the krpano sample site" width="500" height="467" border="0" /></a></p>
<p>Today we’re going to see the code itself, and discuss where the project might go from here. If you’re more interested in this discussion, scroll down past the code. :-)</p>
<p>Firstly, though, here’s a quick demo of the site, recorded using my Nexus 5X mobile phone.</p>
<p>&nbsp;</p>
<p><iframe height="281" src="https://www.youtube.com/embed/qsa-2FyTUm8?rel=0&amp;showinfo=0" frameborder="0" width="500" allowfullscreen=""></iframe></p>
<p>&nbsp;</p>
<p>Something I like about the <a href="http://krpano.com" target="_blank">krpano</a> viewer is that you can view in VR as well as non-VR mode, and tweak settings that make sure your phone works well in VR mode with your particular Cardboard device (this was a one-time setup – the viewer remembered it subsequently).</p>
<p>Here’s <a href="https://github.com/KeanW/RaaS2Krpano" target="_blank">the project on GitHub</a>. I’ve posted krpano runtime files that I’ve bound specifically to my own web-site: to run the output on your own site, you’ll need to <a href="http://krpano.com/buy/" target="_blank">license</a> krpano and generate your own .js file(s).</p>
<p>Here’s the C# code-behind for the main form:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Collections.Generic;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Drawing;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Drawing.Drawing2D;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Drawing.Imaging;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Net;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Reflection;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Text;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Threading.Tasks;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Windows.Forms;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> RaaS2Krpano</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">partial</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">MainForm</span> : <span style="color: #2b91af;">Form</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> MainForm()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; InitializeComponent();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">int</span> _total = 0;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">int</span> _processed = 0;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> ProcessButton_Click(<span style="color: blue;">object</span> sender, <span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ProcessButton.Enabled = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Set up some paths based on the location of the executable</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> asm = <span style="color: #2b91af;">Assembly</span>.GetExecutingAssembly();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> loc = <span style="color: #2b91af;">Path</span>.GetDirectoryName(asm.Location);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> output = loc + <span style="color: #a31515;">"\\output"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> template = loc + <span style="color: #a31515;">"\\template"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> panos = output + <span style="color: #a31515;">"\\panos"</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Copy the template files across to our output folder</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: #2b91af;">Directory</span>.Exists(template))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DirectoryCopy(template, output, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Copied template files"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (!<span style="color: #2b91af;">Directory</span>.Exists(panos))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">Directory</span>.CreateDirectory(panos);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Create the overall welcome text image</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SaveLabelText(welcomeBox.Text, 72, output + <span style="color: #a31515;">"\\label.png"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Created welcome text"</span>);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Collections for the names and URLs in our form</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> names = <span style="color: blue;">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">String</span>&gt;();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> urls = <span style="color: blue;">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: #2b91af;">String</span>&gt;();</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// The controls containing the names and URLs</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> nameBoxes =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">TextBox</span>[] { name1, name2, name3, name4, name5, name6, name7 };</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> urlBoxes =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">TextBox</span>[] { url1, url2, url3, url4, url5, url6, url7 };</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// We'll filter out any null entries (either name or URL)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; nameBoxes.Length; i++)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; !<span style="color: #2b91af;">String</span>.IsNullOrEmpty(nameBoxes[i].Text) &amp;&amp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; !<span style="color: #2b91af;">String</span>.IsNullOrEmpty(urlBoxes[i].Text)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; names.Add(nameBoxes[i].Text);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; urls.Add(urlBoxes[i].Text);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Generate the strings to insert into our XML scene</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> callWiths = <span style="color: blue;">new</span> <span style="color: #2b91af;">StringBuilder</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> panels = <span style="color: blue;">new</span> <span style="color: #2b91af;">StringBuilder</span>();</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// We'll fill the thumbnails within the angle specified in our form</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">double</span> fovThumbs = (<span style="color: blue;">double</span>)thumbnailAngle.Value;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Calculate the start angle and the increnement based on the</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// number of scenes we have and the overall angle to fill</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">double</span> fovStart = (names.Count == 1 ? 0 : fovThumbs / -2);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">double</span> fovInc =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (names.Count == 1 ? fovThumbs : fovThumbs / (names.Count - 1));</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Set a total entries target at 2 x the number of panos to process</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// (we process a file for both Left and Right)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _total = 2 * names.Count;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Loop through, extracting the cube data and gathering entries</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// for our XML scene</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; names.Count; i++)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ExtractCube(panos, names[i], urls[i], () =&gt; ProcessButton.Enabled = <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Firstly the JavaScript animation for when the cursor is hovering</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; callWiths.AppendFormat(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"\t\tcallwith(hotspot[p{0}],&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; copy(ty,start_ty); tween(alpha|ty,"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">" 0.5|75, get(start_tt), easeOutQuad|easeOutQuint); );\r\n"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"\t\tcallwith(hotspot[p{0}_thumb], copy(ty,start_ty); tween(alpha|ty,"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">" 1.0|75, get(start_tt), easeOutQuad|easeOutQuint, set(enabled,true) ); );\r\n"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"\t\tcallwith(hotspot[p{0}_txt],&nbsp;&nbsp; copy(ty,start_ty); tween(alpha|ty,"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">" 1.0|75, get(start_tt), easeOutQuad|easeOutQuint); );\r\n"</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; i + 1</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Secondly the panel hotspot elements themselves</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; panels.AppendFormat(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"\t&lt;!-- Panel {0} --&gt;\r\n"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"\t&lt;hotspot name=\"p{0}\" style=\"panel\" ath=\"{1}\" atv=\"0\" /&gt;\r\n"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"\t&lt;hotspot name=\"p{0}_thumb\" style=\"thumb\" zorder=\"3\" ath=\"{1}\""</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">" atv=\"0\" url=\"panos/{2}/thumb.png\" scale=\"0.3\" ox=\"0\" oy=\"-10\""</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">" onclick=\"changepano( loadpanoscene('%CURRENTXML%/panos/{2}/tour.xml',"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">" 0, null, MERGE|KEEPVIEW|KEEPMOVING, BLEND(1));&nbsp; set(webvr.worldscale,0.5); );\" /&gt;\r\n"</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"\t&lt;hotspot name=\"p{0}_txt\"&nbsp;&nbsp; style=\"thumb\" zorder=\"2\" ath=\"{1}\""</span> +</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">" atv=\"0\" url=\"panos/{2}/text.png\" scale=\"0.3\" oy=\"+82\" enabled=\"false\" /&gt;\r\n\r\n"</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; i + 1, (<span style="color: blue;">int</span>)(fovStart + (i * fovInc)), names[i]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Now we'll write our XML scene file</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> xmlFile = output + <span style="color: #a31515;">"\\krpano.xml"</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// We'll replace some placeholders with our collected strings</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">string</span> callWithPlaceholder = <span style="color: #a31515;">"%%CALLWITH%%"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">string</span> panelsPlaceholder = <span style="color: #a31515;">"%%PANELS%%"</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (<span style="color: #2b91af;">File</span>.Exists(xmlFile))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Read in the file, then loop through it</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> xml = <span style="color: #2b91af;">File</span>.ReadAllLines(xmlFile);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i=0; i &lt; xml.Length; i++)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Replace our placeholders, if they exist</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (xml[i].Contains(callWithPlaceholder))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; xml[i] = xml[i].Replace(callWithPlaceholder, callWiths.ToString());</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (xml[i].Contains(panelsPlaceholder))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; xml[i] = xml[i].Replace(panelsPlaceholder, panels.ToString());</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Write the file back</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">File</span>.WriteAllLines(xmlFile, xml);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Updated XML file"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">async</span> <span style="color: blue;">void</span> ExtractCube(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">string</span> root, <span style="color: blue;">string</span> name, <span style="color: blue;">string</span> panoUrl, <span style="color: #2b91af;">Action</span> afterLast = <span style="color: blue;">null</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Extracting cube map for "</span> + name);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> dir = root + <span style="color: #a31515;">"\\"</span> + name;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (!<span style="color: #2b91af;">Directory</span>.Exists(dir))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">Directory</span>.CreateDirectory(dir);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> leftFile = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\{1}-L.jpg"</span>, dir, name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rightFile = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\{1}-R.jpg"</span>, dir, name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> leftDir = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\{1}-L.tiles"</span>, dir, name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rightDir = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\{1}-R.tiles"</span>, dir, name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> leftUrl = GetImageUrl(panoUrl, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rightUrl = GetImageUrl(panoUrl, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SaveLabelText(name, 56, dir + <span style="color: #a31515;">"\\text.png"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Created label text"</span>);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; GeneratePanoXml(name, dir);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Created XML file"</span>);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> files = <span style="color: blue;">new</span> <span style="color: #2b91af;">String</span>[] { leftFile, rightFile };</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> urls = <span style="color: blue;">new</span> <span style="color: #2b91af;">String</span>[] { leftUrl, rightUrl };</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> dirs = <span style="color: blue;">new</span> <span style="color: #2b91af;">String</span>[] { leftDir, rightDir };</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Downloading files"</span>);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> wc = <span style="color: blue;">new</span> <span style="color: #2b91af;">WebClient</span>())</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; files.Length; i++)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url = urls[i];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> file = files[i];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> tgtDir = dirs[i];</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">await</span> ExtractCubeSides(wc, dir, url, file, tgtDir);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _processed++;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Peform the "everything's done" operation to re-enable the UI</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_processed == _total &amp;&amp; afterLast != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; afterLast();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"We are done!"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Refresh the UI from time to time</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DoEvents();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">File</span>.Delete(file);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Downloaded file deleted"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Cube map extracted"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">async</span> <span style="color: #2b91af;">Task</span> ExtractCubeSides(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">WebClient</span> wc, <span style="color: blue;">string</span> dir, <span style="color: blue;">string</span> url, <span style="color: blue;">string</span> file, <span style="color: blue;">string</span> tgtDir</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">int</span> mobRes = 1024;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">int</span> thumbWidth = 512;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">int</span> thumbHeight = 288;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> cube = <span style="color: blue;">new</span> <span style="color: #2b91af;">String</span>[] { <span style="color: #a31515;">"l"</span>, <span style="color: #a31515;">"f"</span>, <span style="color: #a31515;">"r"</span>, <span style="color: #a31515;">"b"</span>, <span style="color: #a31515;">"d"</span>, <span style="color: #a31515;">"u"</span> };</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> preview = <span style="color: blue;">new</span> <span style="color: #2b91af;">String</span>[] { <span style="color: #a31515;">"l"</span>, <span style="color: #a31515;">"f"</span>, <span style="color: #a31515;">"r"</span>, <span style="color: #a31515;">"b"</span>, <span style="color: #a31515;">"u"</span>, <span style="color: #a31515;">"d"</span> };</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">await</span> wc.DownloadFileTaskAsync(url, file);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Downloaded "</span> + url);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> orig = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(file))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">int</span> x = orig.Height;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (orig.Width == orig.Height * cube.Length)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (!<span style="color: #2b91af;">Directory</span>.Exists(tgtDir))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">Directory</span>.CreateDirectory(tgtDir);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Created folder: "</span> + tgtDir);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Create a cropped thumbnail (a JPG)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rect = <span style="color: blue;">new</span> <span style="color: #2b91af;">Rectangle</span>(0, x / 4, x, x / 2);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> cropped = orig.Clone(rect, orig.PixelFormat))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> thumb = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(cropped, thumbWidth, thumbHeight))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> thumbFile = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\thumb.jpg"</span>, dir);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; thumb.Save(thumbFile, <span style="color: #2b91af;">ImageFormat</span>.Jpeg);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Saved thumbnail image"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// And also a rounded thumbnail (a PNG for the transparent border)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rect2 = <span style="color: blue;">new</span> <span style="color: #2b91af;">Rectangle</span>(0, 0, x, x);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> thumb2 = orig.Clone(rect, orig.PixelFormat))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span>(<span style="color: blue;">var</span> thumb3 = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(thumb2, thumbWidth, thumbWidth))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> thumb5 = RoundCorners(thumb3, thumbWidth / 2, <span style="color: #2b91af;">Color</span>.Transparent))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> roundFile = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\thumb.png"</span>, dir);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; thumb5.Save(roundFile, <span style="color: #2b91af;">ImageFormat</span>.Png);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Saved rounded thumbnail image"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Extract each side of the cube map</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> sides = <span style="color: blue;">new</span> <span style="color: #2b91af;">Dictionary</span>&lt;<span style="color: blue;">string</span>, <span style="color: #2b91af;">Bitmap</span>&gt;();</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> j = 0; j &lt; cube.Length; j++)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; rect = <span style="color: blue;">new</span> <span style="color: #2b91af;">Rectangle</span>(j * x, 0, x, x);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> pano = orig.Clone(rect, orig.PixelFormat))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> panoFile = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\pano_{1}.jpg"</span>, tgtDir, cube[j]);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; pano.Save(panoFile, <span style="color: #2b91af;">ImageFormat</span>.Jpeg);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Extracted pano cube side: "</span> + cube[j]);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> mobile = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(pano, mobRes, mobRes);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> mobFile = <span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">"{0}\\mobile_{1}.jpg"</span>, tgtDir, cube[j]);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; mobile.Save(mobFile, <span style="color: #2b91af;">ImageFormat</span>.Jpeg);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LogText(<span style="color: #a31515;">"Extracted mobile cube side: "</span> + cube[j]);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Add the smaller image to a map to create the preview at the end</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sides.Add(cube[j], mobile);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Generate a lower-resolution preview cube map image</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> px = 256;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> prev = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(px, px * preview.Length);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> prevFile = tgtDir + <span style="color: #a31515;">"\\preview.jpg"</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> g = <span style="color: #2b91af;">Graphics</span>.FromImage(prev))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> j = 0; j &lt; preview.Length; j++)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; rect = <span style="color: blue;">new</span> <span style="color: #2b91af;">Rectangle</span>(0, j * px, px, px);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g.DrawImage(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sides[preview[j]], rect, <span style="color: blue;">new</span> <span style="color: #2b91af;">Rectangle</span>(0, 0, mobRes, mobRes),</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">GraphicsUnit</span>.Pixel</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; prev.Save(prevFile, <span style="color: #2b91af;">ImageFormat</span>.Jpeg);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> LogText(<span style="color: blue;">string</span> message)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; logText.AppendText(message + <span style="color: #a31515;">"\r\n"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> DirectoryCopy(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">string</span> src, <span style="color: blue;">string</span> dest, <span style="color: blue;">bool</span> copySubDirs = <span style="color: blue;">true</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; )</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Get the subdirectories for the specified directory</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> dir = <span style="color: blue;">new</span> <span style="color: #2b91af;">DirectoryInfo</span>(src);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (!dir.Exists)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">throw</span> <span style="color: blue;">new</span> <span style="color: #2b91af;">DirectoryNotFoundException</span>(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"Source directory does not exist or could not be found: "</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; + src);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> dirs = dir.GetDirectories();</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If the destination directory doesn't exist, create it</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (!<span style="color: #2b91af;">Directory</span>.Exists(dest))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">Directory</span>.CreateDirectory(dest);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Get the files in the directory and copy them to the new location</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> files = dir.GetFiles();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> file <span style="color: blue;">in</span> files)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">string</span> temppath = <span style="color: #2b91af;">Path</span>.Combine(dest, file.Name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; file.CopyTo(temppath, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If copying subdirectories, copy them and their contents to new location</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (copySubDirs)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">DirectoryInfo</span> subdir <span style="color: blue;">in</span> dirs)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">string</span> temppath = <span style="color: #2b91af;">Path</span>.Combine(dest, subdir.Name);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DirectoryCopy(subdir.FullName, temppath, copySubDirs);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> GeneratePanoXml(<span style="color: blue;">string</span> name, <span style="color: blue;">string</span> dir)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> xmlFile = dir + <span style="color: #a31515;">"\\tour.xml"</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> xml =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">@"&lt;krpano&gt;</span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;scene name='{0}'&gt;</span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;preview url='{0}-L.tiles/preview.jpg' /&gt;</span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;image stereo='true' stereolabels='L|R'&gt;</span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;cube url='{0}-%t.tiles/pano_%s.jpg' /&gt;</span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;cube url='{0}-%t.tiles/mobile_%s.jpg' devices='iOS' /&gt;</span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/image&gt;</span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/scene&gt;&nbsp; </span></p>
<p style="margin: 0px;"><span style="color: #a31515;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/krpano&gt;"</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> sw = <span style="color: blue;">new</span> <span style="color: #2b91af;">StreamWriter</span>(xmlFile))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; sw.Write(<span style="color: #2b91af;">String</span>.Format(xml, name));</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">string</span> GetImageUrl(<span style="color: blue;">string</span> panoUrl, <span style="color: blue;">bool</span> left = <span style="color: blue;">true</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> url = <span style="color: #a31515;">""</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">const</span> <span style="color: blue;">string</span> prefix = <span style="color: #a31515;">"?url="</span>;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// We'll use the Uri object to analyse the resource information</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> uri = <span style="color: blue;">new</span> <span style="color: #2b91af;">Uri</span>(panoUrl);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// If we have exactly 2 Uri segments, we'll assume we have a preview URL</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> preview = uri.Segments.Length == 2;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (uri.Query.StartsWith(prefix))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Create the URL string based on our Uri</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; url =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">String</span>.Format(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"{0}://{1}/{2}{3}{4}.jpg"</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; uri.Scheme,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; uri.Host,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; preview ? <span style="color: #a31515;">""</span> : uri.Segments[1],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; uri.Query.Substring(prefix.Length),</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">String</span>.Format(preview ? <span style="color: #a31515;">"{0}"</span> : <span style="color: #a31515;">"/image{0}.0"</span>, left ? <span style="color: #a31515;">"L"</span> : <span style="color: #a31515;">"R"</span>)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> url;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Image</span> RoundCorners(<span style="color: #2b91af;">Image</span> img, <span style="color: blue;">int</span> rad, <span style="color: #2b91af;">Color</span> col)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; rad *= 2;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> b = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(img.Width, img.Height);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> g = <span style="color: #2b91af;">Graphics</span>.FromImage(b))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g.Clear(col);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g.SmoothingMode = <span style="color: #2b91af;">SmoothingMode</span>.HighQuality;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g.CompositingQuality = <span style="color: #2b91af;">CompositingQuality</span>.HighQuality;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g.InterpolationMode = <span style="color: #2b91af;">InterpolationMode</span>.HighQualityBicubic;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> brush = <span style="color: blue;">new</span> <span style="color: #2b91af;">TextureBrush</span>(img))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> gp = <span style="color: blue;">new</span> <span style="color: #2b91af;">GraphicsPath</span>())</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; gp.AddArc(-1, -1, rad, rad, 180, 90);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; gp.AddArc(0 + b.Width - rad, -1, rad, rad, 270, 90);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; gp.AddArc(0 + b.Width - rad, 0 + b.Height - rad, rad, rad, 0, 90);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; gp.AddArc(-1, 0 + b.Height - rad, rad, rad, 90, 90);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g.FillPath(brush, gp);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> b;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> SaveLabelText(<span style="color: blue;">string</span> txt, <span style="color: blue;">int</span> size, <span style="color: blue;">string</span> file)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> font =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">new</span> <span style="color: #2b91af;">Font</span>(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">FontFamily</span>.GenericSansSerif, size, <span style="color: #2b91af;">FontStyle</span>.Bold, <span style="color: #2b91af;">GraphicsUnit</span>.Pixel</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Draw our text with a transparent background and save to a file format</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// that supports transparency</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> label = DrawText(txt, font, <span style="color: #2b91af;">Color</span>.White, <span style="color: #2b91af;">Color</span>.Transparent);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; label.Save(file, <span style="color: #2b91af;">ImageFormat</span>.Png);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: #2b91af;">Image</span> DrawText(<span style="color: #2b91af;">String</span> text, <span style="color: #2b91af;">Font</span> font, <span style="color: #2b91af;">Color</span> textCol, <span style="color: #2b91af;">Color</span> bgCol)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: #2b91af;">SizeF</span> textSize;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Create a dummy bitmap to get a graphics object</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> b = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(1, 1))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> g = <span style="color: #2b91af;">Graphics</span>.FromImage(b))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Measure the string to see how big the image needs to be</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; textSize = g.MeasureString(text, font);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Create a new image of the right size</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> b2 = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>((<span style="color: blue;">int</span>)textSize.Width, (<span style="color: blue;">int</span>)textSize.Height);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> g2 = <span style="color: #2b91af;">Graphics</span>.FromImage(b2))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Paint the background</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g2.Clear(bgCol);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Create a brush for the text</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> textBrush = <span style="color: blue;">new</span> <span style="color: #2b91af;">SolidBrush</span>(textCol))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g2.DrawString(text, font, textBrush, 0, 0);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; g2.Save();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> b2;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
</div>
<p>The previous posts in the series should go some may towards explaining the approach taken. The code comments should also help. Otherwise please post a comment and I’ll explain what’s going on.</p>
<p>Regarding future directions…</p>
<p>Right now we have a single scene aggregating multiple panoramas… one possible future direction is technology-centric: rather than having an executable that does all this work on a desktop, why not have a web-site (and associated web-service) that generates the requisite files using Node.js, not only creating a ZIP for download but hosting them directly for preview?</p>
<p>The other direction I’m thinking of is a more fundamental shift. We’ve really focused on linking multiple scenes into a single top-level scene. But what if we allowed the linking of scenes together in a more flexible way? For example: we could show a building floorplan and allow selection of locations for which to generate stereo panoramas. The knowledge of how these rooms relate to each other spatially could let us decide how to connect the rooms together automatically via hotspot “portals”. Or get us part of the way there, allowing the user to specify the location of such portals manually, too.</p>
<p>What do you think? How else might we connect – and encourage navigation between – stereo panoramas to help compensate for their somewhat fixed nature?</p>
