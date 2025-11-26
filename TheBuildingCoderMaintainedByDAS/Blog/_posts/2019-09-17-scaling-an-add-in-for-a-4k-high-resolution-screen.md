---
layout: "post"
title: "Scaling an Add-In for a 4K High Resolution Screen"
date: "2019-09-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Settings"
  - "User Interface"
  - "View"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/09/scaling-an-add-in-for-a-4k-high-resolution-screen.html "
typepad_basename: "scaling-an-add-in-for-a-4k-high-resolution-screen"
typepad_status: "Publish"
---

<p>As high-resolution monitors grow ever more common, an important question arises on handling add-in scaling for 4K high resolution screens.</p>

<p>Below, CoderBoy shares some questions and answers on this topic:</p>

<ul>
<li><a href="#2">Problem adapting a Revit add-in for 4K displays</a></li>
<li><a href="#3">Application properties for stand-alone apps</a></li>
<li><a href="#4">Application manifest suggestion</a></li>
<li><a href="#5">Separate UI component with IPC suggestion</a></li>
<li><a href="#6">Solutions documented</a></li>
<li><a href="#7">Adjusting Revit for 4K displays</a>
<ul>
<li><a href="#7.1">Method 1 &ndash; Run hires monitor in low resolution</a></li>
<li><a href="#7.2">Method 2 &ndash; Adjust how Revit handles 4k displays</a></li>
</ul></li>
<li><a href="#8">Discussion of the results</a></li>
<li><a href="#9">Call to move to WPF</a></li>
<li><a href="#10">WinForms or WPF?</a></li>
</ul>

<h4><a name="2"></a> Problem Adapting a Revit Add-In for 4K Displays</h4>

<p>We are in need of your help.
We have been writing commercial Revit add-ins for over ten years now.
We followed the examples from Autodesk, which were all done using Windows Forms.
We even built our add-in template code from these examples.
The sample code in the Revit SDK also still uses Windows Forms.</p>

<p>However, we have been getting more and more complaints from our customers that our add-ins don’t work properly when they run 4K monitors, because they have to scale up the monitors just to be able to read text on them; the native resolution is too high for the size of these desktop monitors to allow text to be readable at native scale.
For example, most users change the scale to 150% or even 200% in order to be able to read text.
When this happens, our Windows Forms user interfaces get kind of jumbled up.
Scrolling doesn’t work right, buttons get pushed off the edge of the window, etc.
At least some of our add-ins essentially become unusable.</p>

<h4><a name="3"></a> Application Properties for Stand-Alone Apps</h4>

<p>A workaround to this problem for our add-ins is to have whichever monitor is the primary monitor be scaled to 100% zoom.
Then, the add-ins work fine on any screen, but some customers are now balking at that.</p>

<p>In my research on this subject, you can get Windows Forms applications to work well on 4K monitors by changing some of the application properties, for example like this:</p>

<pre class="code">
using System.Runtime.InteropServices;
  [DllImport("Shcore.dll")]
  static extern int SetProcessDpiAwareness(
    int PROCESS_DPI_AWARENESS);

  private enum DpiAwareness {
    None = 0,
    SystemAware = 1,
    PerMonitorAware = 2 }

  SetProcessDpiAwareness(
    (int) DpiAwareness.PerMonitorAware );
</pre>

<p>While these things work well for standalone executables we write, I believe they must be done at the executable level, and as add-in creators for Revit we do not have control over Revit’s executable environment.
So, very sadly, as far as I can tell, these tricks don’t work for our Revit add-ins.</p>

<p>We have a rather breath-taking amount of code in our Revit add-ins, the significant majority of which is in the user interfaces.
To have to rewrite the user interfaces for all of our add-ins to use WPF would be crippling, to say the least.</p>

<p>Can you please advise us on what we would need to do to modify our existing add-ins so they will scale and work correctly on scaled-up 4K monitors in a Revit environment?
For example, are there configuration settings for Revit which control the executable environment that we can advise our customers to change?
Or, are there simple modifications to our code that we would need to implement in order for the WinForms engine on which they depend to run properly in a scaled-up 4K environment under Revit?</p>

<p>What have other long-term add-in creators done with their legacy code to solve this problem, short of a complete rewrite of their add-ins, which would not be feasible for us?</p>

<p>We really need your help with this!
Thank you very much for any advice you can offer.</p>

<h4><a name="4"></a> Application Manifest Suggestion</h4>

<p>In addition to setting DPI awareness in the code, it is also possible to use an application manifest.
I think that this is worth investigating, although it may not be very straightforward for an add-in DLL.</p>

<p>Here are some links:</p>

<ul>
<li><a href="https://docs.microsoft.com/en-us/windows/win32/sbscs/application-manifests">Application manifests</a></li>
<li><a href="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/mt846517(v%3Dvs.85)">DPI awareness and manifests</a></li>
<li><a href="https://support.microsoft.com/en-us/help/912949/some-third-party-applications-that-use-external-manifest-files-stop-wo">Registry key</a> (for older Windows?)</li>
</ul>

<h4><a name="5"></a> Separate UI Component with IPC Suggestion</h4>

<p>I am convinced that this can be solved without rewriting all your UI code.</p>

<p>For instance, if all else fails, simply disconnect your UI completely from your Revit add-in, run it in a separate process and use IPC to pass the information back and forth, as discussed and successfully implemented in
these <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> threads:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html">Use IPC for disentanglement</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/using-a-geometry-viewer-in-a-revit-addin-to-preview-results/m-p/8868232">Using a geometry viewer in a Revit add-in to preview results</a></li>
</ul>

<p>This assumes, of course, that you have not unnecessarily mixed up your UI code with Revit API stuff.</p>

<p><strong>Response:</strong> That is an extremely interesting idea!</p>

<p>I’m sure some of our oldest apps do embed the Revit code in with the UI.
So, those would need some work to separate out.
But that would still be better and much faster than a whole rewrite.</p>

<p>Thank you so very much!</p>

<h4><a name="6"></a> Solutions Documented</h4>

<p>I did some testing, and we only have a few of our add-ins (or reusable components) that are in pretty bad shape as far as WinForms in Revit on 4K goes.
Very fortunately, most of our add-ins seem to work reasonably well!
Some work, but require making their window large, so are awkward but functional, but a few things are downright unusable.
Of course, it’s the more complicated ones that have the worst problems, but I guess that is to be expected.
So, the scope of the problem isn’t as big as we feared it might be.</p>

<p>However, we think we found at least a short-term workaround for our WinForms on 4K display problems in all of our add-ins!</p>

<p>It turns out you can instruct Windows itself how to handle the scaling on a per-application basis, by modifying the launch icon properties.</p>

<p>Here is a solution document
on <a href="https://thebuildingcoder.typepad.com/files/coderboy_adjusting_revit_for_4k_displays.pdf">adjusting Revit for 4K displays</a> that
describes these simple settings changes:</p>

<h4><a name="7"></a> Adjusting Revit for 4K Displays</h4>

<p>Some add-ins for Revit may not appear correctly when using a 4K display, especially in cases where the primary screen is not set to 100% scaling.</p>

<p>There are 2 approaches which may quickly resolve this:</p>

<ol>
<li>Tell the 4K monitor to run at 1920 x 1080 (or any other resolution) at <strong>100% scaling</strong></li>
<li>Adjust how only Revit is launched and handles 4K displays</li>
</ol>

<h4><a name="7.1"></a> Method 1 &ndash; Run Hires Monitor in Low Resolution</h4>

<p>Right-click on the desktop and select Display settings:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4d2ed0a200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4d2ed0a200b img-responsive" style="width: 257px; display: block; margin-left: auto; margin-right: auto;" alt="Display settings" title="Display settings" src="/assets/image_c86670.jpg" /></a><br /></p>

<p></center></p>

<p>Click on the representation of the 4K monitor (click the “Identify” button if you’re not sure):</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4ae52f8200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4ae52f8200d img-responsive" style="width: 410px; display: block; margin-left: auto; margin-right: auto;" alt="Display settings" title="Display settings" src="/assets/image_9e4c82.jpg" /></a><br /></p>

<p></center></p>

<p>Set the Display resolution you like and, most importantly, set the scaling to 100% --</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a484fedc200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a484fedc200c img-responsive" style="width: 454px; display: block; margin-left: auto; margin-right: auto;" alt="Display settings" title="Display settings" src="/assets/image_f8cb4c.jpg" /></a><br /></p>

<p></center></p>

<p>You must log out and log back in for this change to take effect.</p>

<h4><a name="7.2"></a> Method 2 &ndash; Adjust how Revit Handles 4K Displays</h4>

<p>Adjustments can be made to how Revit is launched that should correct these display issues.</p>

<p>With no Revit sessions running, right-click on the icon that launches Revit and select Properties:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a484fefa200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a484fefa200c img-responsive" style="width: 355px; display: block; margin-left: auto; margin-right: auto;" alt="Taskbar icon" title="Taskbar icon" src="/assets/image_c33859.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Taskbar icon</p>

<p></center></p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a484ff0c200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a484ff0c200c img-responsive" style="width: 329px; display: block; margin-left: auto; margin-right: auto;" alt="Desktop icon" title="Desktop icon" src="/assets/image_f6cebb.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Desktop icon</p>

<p></center></p>

<p>On the next screen, select the Compatibility tab and click on “Change settings for all users” button at the bottom:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4d2ed6f200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4d2ed6f200b img-responsive" style="width: 298px; display: block; margin-left: auto; margin-right: auto;" alt="Change settings" title="Change settings" src="/assets/image_0c2619.jpg" /></a><br /></p>

<p></center></p>

<p>Near the bottom of the next screen, click on the “Change high DPI settings” button:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4ae5351200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4ae5351200d img-responsive" style="width: 293px; display: block; margin-left: auto; margin-right: auto;" alt="Change settings" title="Change settings" src="/assets/image_2d63be.jpg" /></a><br /></p>

<p></center></p>

<p>On the last screen, check the “Override high DPI scaling behavior. Scaling performed by:” checkbox and select <strong>System</strong>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4d2ed8c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4d2ed8c200b img-responsive" style="width: 274px; display: block; margin-left: auto; margin-right: auto;" alt="Change settings" title="Change settings" src="/assets/image_3b2ec0.jpg" /></a><br /></p>

<p></center></p>

<p>Click OK on each dialog to save the settings.</p>

<h4><a name="8"></a> Discussion of the Results</h4>

<p>We haven’t observed any ill effects in Revit in our limited testing, but if you know of a reason for a user NOT to make these changes to their Revit start-up icon, please let us know.</p>

<p>One can copy the original start-up icon, modify the properties of the copy for a problematic add-in, and go back to the regular icon for normal use.</p>

<p>We haven’t heard any complaints other than the text being small on PDF exports.</p>

<p>I ran a test, and it’s true, as seen in the following two images.</p>

<p>“Default” is with no scaling adjustment of Revit when it’s launched:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4d2ecd2200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4d2ecd2200b image-full img-responsive" alt="4K default" title="4K default" src="/assets/image_257520.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>“Adjusted” is with the scaling adjustment:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4ae7c0d200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4ae7c0d200d image-full img-responsive" alt="4K adjusted" title="4K adjusted" src="/assets/image_b6b3a4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Much of the text is small.</p>

<p>Most actual Revit end users are happy to just set their screen resolution to 1920 x 1080 at 100% scaling.</p>

<p>That’s obviously the best solution to the issue.</p>

<p>Some people say, “we have 4K, we should use it at 4K”, however, who don’t want to do that.</p>

<p>Most of our apps are fine on 4K (80%?), but I’ve been spending some time trying to adjust our troublesome code to work better on 4K without a rewrite.</p>

<p>I’ve had a little success, but there are still some real stumpers too, simple designs that just don’t want to cooperate.  </p>

<p>To clarify 'default' versus 'adjusted':</p>

<p>The “Default” PDF is with no scaling adjustment on the icon definition for how Revit is launched.  It’s what would appear if you just installed Revit for the first time on your 4K display and launched it immediately with no changes.  The text size on the PDFs comes out correctly.</p>

<p>The “Adjusted” PDF was generated by following Method 2 and doing nothing else. </p>

<p>If you look at the “Adjusted” PDF, the text in the bubbles is very small.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4ae529f200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4ae529f200d img-responsive" style="width: 248px; display: block; margin-left: auto; margin-right: auto;" alt="4K adjusted detail" title="4K adjusted detail" src="/assets/image_05476d.jpg" /></a><br /></p>

<p></center></p>

<p>On screen, the text looks correctly sized, but, in a PDF, it comes out small.</p>

<p>In the “Default” PDF, the text in the bubbles is normal sized, and comes out the same size as seen on-screen:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4ae5292200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4ae5292200d img-responsive" style="width: 398px; display: block; margin-left: auto; margin-right: auto;" alt="4K default detail" title="4K default detail" src="/assets/image_8cec27.jpg" /></a><br /></p>

<p></center></p>

<p>I think the best solution is Method 1. It incurs no issues at all.</p>

<p>Second-best is Method 2 plus setting two icons to launch Revit, as described above: One for “normal” on-screen use (applying Method 2) and one just for generating PDFs, which has no special scaling applied in the icon properties.  </p>

<p>Many thanks to CoderBoy for the in-depth research and precise and complete documentation of the solution!</p>

<h4><a name="9"></a> Call to Move to WPF</h4>

<p>Jason Masters added several relevant thoughts to this topic in
the <a href="https://thebuildingcoder.typepad.com/blog/2019/09/scaling-an-add-in-for-a-4k-high-resolution-screen.html#comment-4619001048">comments below</a>,
a wish list entry
to <a href="https://forums.autodesk.com/t5/revit-ideas/update-sdk-developer-documentation-to-wpf/idi-p/9030473">update the SDK and developer documentation to WPF</a> in
the Revit Idea Station, and 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/winforms-or-wpf/m-p/9030525">WinForms or WPF?</a></p>

<p><strong>Question:</strong> Is Autodesk planning on updating the official developer guides, samples and documentation?</p>

<p>Isn't the root issue here that Autodesk's official documentation is recommending a UI Framework that's 17 years old?</p>

<p>If the documentation recommended and walked you through WPF from the start, no one would have this problem, because WPF scales properly across mixed and high resolution monitors...</p>

<p>I know that I was burned by the same issue as CoderBoy and was very frustrated when I realized that I never should have bothered with WinForms to begin with.</p>

<p><strong>Answer:</strong> I am very sorry to hear your completely understandable frustration. Unfortunately, the best I can do is suggest that you file a wish list request for a revamped SDK in the Revit Idea Station, and discuss your Revit API plans with your peers in the Revit API discussion forum before investing too much into any obsolete frameworks. Personally, I am not a fan of graphical user interfaces in any shape and form. As far as I am concerned, the only user interface I ever need is an ASCII text editor. I am sure you can very effectively drive Revit
from <a href="https://en.wikipedia.org/wiki/Emacs">emacs</a> &nbsp; :-)</p>

<p><strong>Response:</strong> Haha well I agree with you wholeheartedly on that, convincing our users that all they need is a terminal might be a bit more of a challenge though! I have submitted a wish list item to the Revit Idea Station idea requesting
to <a href="https://forums.autodesk.com/t5/revit-ideas/update-sdk-developer-documentation-to-wpf/idi-p/9030473">update the SDK and developer documentation to WPF</a>.</p>

<h4><a name="10"></a> WinForms or WPF?</h4>

<p>This is an old thread, but it's still highly ranked on Google, so I just wanted to chime in that I went down the WinForms path for about a year and I really regretted it. I would highly recommend that everyone go down the WPF route instead.</p>

<p>For one, WinForms often have some serious and practically unsolvable scaling issues on high resolution monitors, as discussed above. While Inter-Process Communication (IPC) is a good option for fixing this user's existing code base, implementing IPC is way more work than just using WPF to begin with, since WPF apps don't have scaling issues.</p>

<p>Additionally, WPF UIs are built in a much more modern way, with XML layout documents, separate style documents, and separate code / logic documents. This is much more similar to how UIs are built on other frameworks (like Android / iOS / macOS  / web development) and will better prepare you for expanding your development knowledge. This separation also tends to produce much cleaner, more flexible, and more reusable code.</p>

<p>And lastly, because WPF is a more modern framework, I find that it's just far easier to produce apps that actually look good, and that my users actually enjoy opening up. As developers it's easy to focus on the back end data, but if you want to make a tool that people actually use, it needs to have a pleasing UI, and the styling / dynamic binding nature of WPF makes it far, far, far easier to produce at least halfway modern UXs.</p>
