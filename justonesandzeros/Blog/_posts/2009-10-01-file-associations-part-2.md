---
layout: "post"
title: "File Associations - Part 2"
date: "2009-10-01 09:33:47"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2009/10/file-associations-part-2.html "
typepad_basename: "file-associations-part-2"
typepad_status: "Publish"
---

<img src="/assets/Concepts2.png"><br>
Welcome to part 2 in my 2 part series on File Associations.&nbsp; In
<a href="http://justonesandzeros.typepad.com/blog/2009/09/file-associations-part-1.html">part 1</a>, I went over the concepts in base Vault.&nbsp; In this part,
I will go over concepts added by Vault Workgroup.<br>
<br>
<b>New concepts:</b><br>
Again we have 2 concepts that are simple on their own, but become
complex when added to everything else.<br>
<br>
First we have <b>Revisions</b>, which is a way to give meaning to a
set of versions.&nbsp; For purposes of this posting, I'll be using
alpha characters for revisions and numeric characters for versions.<br>
<img alt="" src="/assets/figure5.png" height="64" width="348"><br>
<br>
<br>
The next concept is <b>Release States</b>, which is a way of saying
that a file is completed if it is in a certain lifecycle state.&nbsp;
At the API level, there is a boolean property on LfCycState which tells
if the state is a release state or not.&nbsp; A lifecycle definition
usually has only one release state, but it is possible to have 0 or
multiple release states. For the purposes of this posting, release
states will have a darker color than the other states.<br>
<img alt="" src="/assets/figure6.png" height="130" width="352"><br>
<br>
<br>
One final note:&nbsp; The Vault clients are set up to update files
associations only with the physical file changes.&nbsp; If it's just a
change to the Vault data (for example, changing lifecycle state) then
the new file version points to the same files that the old version did.<br>
<br>
<br>
<b>Example:</b><br>
Let's go over an example.&nbsp; Here we have 2 files, a parent and a
child.&nbsp; Both files have gone through revision and lifecycle
changes.&nbsp; The arrows represent direct associations between the
file versions.<br>
<img alt="" src="/assets/figure7.png" height="181" width="305"><br>
<br>
<br>
The Vault API provides 5 ways to traverse this data.&nbsp; In all these
cases, we are starting at the Parent and asking for the entire
dependency tree.<br>
<ol>
  <li><b>Get Latest - no release bias</b><br>
    <img alt="" src="/assets/figure8.png" height="124" width="303"><br>
This is useful for CAD engineers who want the latest version of all the
files so that they can work on them.<br>
At the API level, you call <b>GetLatestFileAssociationsByMasterIds </b>in
the Document Service and pass in 'false' for the <i>releasedBiased </i>parameter.<br>
    <br>
  </li>
  <li><b>Get Latest - release bias</b><br>
    <img alt="" src="/assets/figure9.png" height="124" width="303"><br>
This is useful for people outside the CAD department who are not
interested in the 'work in progress' data.&nbsp; They just want to see
the latest completed product.<br>
At the API level, you call <b>GetLatestFileAssociationsByMasterIds </b>in
the Document Service and pass in 'true' for the <i>releasedBiased </i>parameter.<br>
    <br>
  </li>
  <li><b>Snapshot in Time by Version</b><br>
    <img alt="" src="/assets/figure10.png" height="124" width="303"><br>
This is useful in cases where you don't care about revisions.&nbsp; The
result is what you would get in base Vault, you only get the direct
relationships between file versions.<br>
At the API level, you call <b>GetFileAssociationsByIds </b>in the
Document Service.<br>
    <br>
  </li>
  <li><b>Snapshot in Time by Revision - no release bias</b><br>
    <img alt="" src="/assets/figure11.png" height="128" width="304"><br>
This gives you the latest version within a revision, regardless of the
lifecycle state.<br>
At the API level, you call <b>GetRevisionFileAssociationsByIds </b>in
the Document Service and pass in 'false' for the <i>releasedBiased </i>parameter.<br>
    <br>
  </li>
  <li><b>Snapshot in Time by Revision - release bias</b><br>
    <img alt="" src="/assets/figure12.png" height="126" width="304"><br>
This gives you the latest release version within a revision.&nbsp; If
no release version exists, the latest one in the revision is returned.<br>
At the API level, you call <b>GetRevisionFileAssociationsByIds </b>in
the Document Service and pass in 'true' for the <i>releasedBiased </i>parameter.<br>
    <br>
  </li>
</ol>
<b>Putting it All Together:</b><br>
You should be properly confused by now.&nbsp; However there are a rule that will simplify things.&nbsp; <br>
<table border="1" cellpadding="2" cellspacing="2" width="100%">
  <tbody>
    <tr>
      <td bgcolor="#ffffcc" valign="top">Rule:&nbsp; When dealing
with revisions, think in terms of revisions.&nbsp; Don't think in terms
of versions.<br>
      </td>
    </tr>
  </tbody>
</table>
<br>
Behind the scenes, revisions only care about 2 versions:&nbsp; the
latest version in the revision and the latest release version in the
revision.&nbsp; Many times these 2 are the same thing.&nbsp; When
thinking in terms of revisions, you need to forget about all other
versions.&nbsp; <br>
<br>
So let's redo the dependency tree for 4 and 5, this time viewing
things in terms of revisions.<br>
<ol start="4">
  <li><b>Snapshot in Time by Revision - no release bias<br>
    </b><img alt="" src="/assets/figure13.png" height="129" width="304"><br>
    <br>
  </li>
  <li><b>Snapshot in Time by Revision - release bias<br>
    </b><img alt="" src="/assets/figure14.png" height="126" width="305"><br>
  </li>
</ol>
There, much better.
