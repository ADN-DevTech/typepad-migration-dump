---
layout: "post"
title: "Revit 2021 Unit Types in Family Type Catalogues"
date: "2020-04-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2021"
  - "Content"
  - "Family"
  - "Migration"
  - "Security"
  - "Training"
  - "Units"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/04/revit-2021-unit-types-family-type-catalogues.html "
typepad_basename: "revit-2021-unit-types-family-type-catalogues"
typepad_status: "Publish"
---

<p>Unfortunately, the new unit type name functionality can cause a problem loading a family with a type catalogue:</p>

<ul>
<li><a href="#2">Unit type update affects family type catalogue loading</a></li>
<li><a href="#3">New FreeCodeCamp courses</a></li>
<li><a href="#4">Padlocking The Building Coder</a></li>
</ul>

<h4><a name="2"></a> Unit Type Update Affects Family Type Catalogue Loading</h4>

<p><strong>Question:</strong> We have some families that fail to load in Revit 2021.</p>

<p>We have narrowed it down to a Revit change.</p>

<p>When you export PIPING__FLOW (GPM) data to a type 'Family Types' in previous versions of Revit, you get something like this:</p>

<pre>
Cold Water Flow##PIPING_FLOW##GALLONS_US_PER_MINUTE
</pre>

<p>However, when you do the same in Revit 2021, you get:</p>

<pre>
Cold Water Flow##PIPING_FLOW##US_GALLONS_PER_MINUTE
</pre>

<p>The bad news for us being that Revit 2021 does not accept GALLONS_US_PER_MINUTE anymore.
Instead, it expects (and does not ‘upconvert’) the new US_GALLONS_PER_MINUTE.</p>

<p>This is a breaking change for our existing type catalogues.</p>

<p>Is there a published list of changes for parameters that we can review?</p>

<p><strong>Answer:</strong> Yes, indeed, this is an intentional change.
Sorry that it is affecting you so hard.
Just as you say, no automatic upgrade from the previous version’s DB strings has been implemented.</p>

<p>We documented these changes in the <a href="https://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html">developer guide</a>.</p>

<p>The table of changes to database identifiers can be found at the bottom of the page
on <a href="http://help.autodesk.com/view/RVT/2021/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Application_and_Document_Units_html">Introduction &gt; Application and Document &gt; Document Functions &gt; Units</a>.</p>

<h4><a name="3"></a> New FreeCodeCamp Courses</h4>

<p>I always enjoy browsing through the FreeCodeCamp courses recommended in Quincy Larson's newsletter.</p>

<p>Last week's bunch looked especially useful to me, for instance these:</p>

<ul>
<li><a href="https://www.freecodecamp.org/news/the-python-guide-for-beginners/">The Ultimate Python Beginner's Handbook</a></li>
<li><a href="https://www.freecodecamp.org/news/learn-data-analysis-with-python-course/">Learn Data Analysis with Python &ndash; A Free 4-Hour Course</a></li>
<li><a href="https://www.freecodecamp.org/news/lockdownconf-free-developer-conference/">LockdownConf &ndash; A Free Online Conference to Help You Prepare for a Post-Pandemic World</a></li>
<li><a href="https://www.freecodecamp.org/news/how-to-style-your-react-apps-with-less-code-using-tailwind-css-and-styled-components/">How to Style Your React Apps with Less Code Using Tailwind CSS and Styled Components</a></li>
</ul>

<h4><a name="4"></a> Padlocking The Building Coder</h4>

<p>Last week, colleagues pointed out that some of the Autodesk developer blogs were displaying a message saying 'Not secure' in the browser address bar:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4fd7026200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4fd7026200d img-responsive" alt="Address bar warning 'not secure'" title="Address bar warning 'not secure'" src="/assets/image_964d78.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>The problem is caused by a mixed content error, where some references from the blog are not secure.</p>

<p>You can check the site either in Firefox, or using an external page, e.g., <a href="https://www.whynopadlock.com">whynopadlock.com</a>, which will conveniently show the references causing the mixed content error:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4fd7031200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4fd7031200d image-full img-responsive" alt="Test results" title="Test results" src="/assets/image_8c88c5.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<pre>
TEST RESULTS
Test Information
Tested URL https://thebuildingcoder.typepad.com/
Mixed Content - Errors
Soft Failure An image with an insecure url of "http://thebuildingcoder.typepad.com/tbc_banner6_1200_200.png" was loaded on line: 4572 of https://thebuildingcoder.typepad.com/.
This URL will need to be updated to use a secure URL for your padlock to return.
</pre>

<p>In The Building Coder, it was only the banner image, and it can be simply fixed by adding <code>https</code> to the image reference in the CSS.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a52230b0200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a52230b0200b image-full img-responsive" alt="Adding https to banner image in CSS" title="Adding https to banner image in CSS" src="/assets/image_966ec8.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Now all is well and the site is padlocked again:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4fd7036200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4fd7036200d image-full img-responsive" alt="Padlocked URL" title="Padlocked URL" src="/assets/image_663514.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
