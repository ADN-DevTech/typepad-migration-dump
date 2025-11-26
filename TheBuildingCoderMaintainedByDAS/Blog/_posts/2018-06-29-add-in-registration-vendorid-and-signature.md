---
layout: "post"
title: "Add-In Registration &ndash; VendorId and Signature"
date: "2018-06-29 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Apps"
  - "AppStore"
  - "Getting Started"
  - "Settings"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/06/add-in-registration-vendorid-and-signature.html "
typepad_basename: "add-in-registration-vendorid-and-signature"
typepad_status: "Publish"
---

<p>I am taking lots of time off in July, so this may be my last post for a while.</p>

<p>Before leaving, I will share some answers to a list of pertinent questions on add-in registration, especially how to populate the add-in manifest <code>VendorId</code> tag and handle the trusted digital DLL signature:</p>

<ul>
<li><a href="#2">Add-In Registration &ndash; <code>VendorId</code></a></li>
<li><a href="#3">Add-In Registration &ndash; Trusted Digital Add-in Signature</a></li>
<li><a href="#4">Vacation in July</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3570dd0200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3570dd0200c img-responsive" style="width: 280px; display: block; margin-left: auto; margin-right: auto;" alt="Identification" title="Identification" src="/assets/image_83b53d.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a> Add-In Registration &ndash; VendorId</h4>

<p><strong>Question:</strong>  What should we be specifying for our <code>VendorId</code>?</p>

<p>Can it be something like the explicit app id used for iOS and Android?</p>

<p>Apple recommends using a 'reverse-domain style' string for the app id suffix, e.g., 'com.yourcompany.yourapp'. </p>

<p><strong>Answer:</strong> Yes, exactly! You can see this very recommendation in
the <a href="http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Add_in_Registration_html">developer guide instructions on Add-in Registration</a>:</p>

<ul>
<li><code>VendorId</code>: A unique vendor identifier that may be used by some operations in Revit (such as identification of extensible storage). This must be unique, and thus we recommend using a reversed version of your domain name, for example, com.autodesk or uk.co.autodesk.</li>
</ul>

<p><strong>Question:</strong>  Does this need to be registered with Autodesk somewhere? </p>

<p><strong>Answer:</strong> No.</p>

<p>You can use any symbol you like, and you are responsible yourself for its uniqueness.</p>

<p>There used to be a different system, the <em>Autodesk Registered Developer Symbol, <b>RDS</b></em>, limited to four characters and registered with Autodesk. That system was invented by Jeremy Tammik in the timeframe of ADGE, the AutoCAD Developers Group Europe, in the early 1980's. It had to be short and somewhat user compatible, since it was included in numerous AutoCAD symbol names, which were limited to 32 characters at that time. Therefore, it required a centralised registration agency. It has since been terminated.</p>

<p>Using the inverted Internet URL requires no registration, since the real Internet URL is unique in itself.</p>

<h4><a name="3"></a> Add-In Registration &ndash; Trusted Digital Add-in Signature</h4>

<p><strong>Question:</strong>  Do we need to digitally sign the app if we are going through the Autodesk App Store to generate our installer? </p>

<p><strong>Answer:</strong> Not necessarily, and yes, personally, I would highly recommend doing so.</p>

<p>The main repository of all public knowledge on this topic, the trusted digital add-in signature, is in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="http://forums.autodesk.com/t5/revit-api/code-signing-of-revit-addins/m-p/5981560">code signing of Revit addins</a>.</p>

<p>Please also refer to the help documentation on the topic, including a section in the developer guide
on <a href="http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Digitally_Signing_Your_Revit_Add_in_html">digitally signing your add-in and making your own certificate for testing and internal use</a>.</p>

<h4><a name="4"></a> Vacation in July</h4>

<p>I am off on vacation in July.</p>

<p>I'll start next week, spending some time with some friends in a hut in the Swiss mountains in Sulwald in the Lauterbrunnental.</p>

<p>Later, I will do some leisurely travelling and camping in France, on my way to a one-week visit to practice awareness, care and attentiveness in
the <a href="https://plumvillage.org">Buddhist monastery Plum Village</a> near Bordeaux, founded
by the Vietnamese monk and Zen master <a href="https://plumvillage.org/about/thich-nhat-hanh">Thich Nhat Hanh</a>.</p>

<p>Please take good care of yourself during my absences &nbsp; :-)</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3570dc6200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3570dc6200c image-full img-responsive" alt="View of the Jungfrau Mountain from Sulwald" title="View of the Jungfrau Mountain from Sulwald" src="/assets/image_f833a8.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
