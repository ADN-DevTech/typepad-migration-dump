---
layout: "post"
title: "Don't perform heavy tasks in the MoBu Real-time engine thread, even if it worked before"
date: "2015-06-18 08:03:00"
author: "Zhong Wu"
categories:
  - "Animation"
  - "Autodesk"
  - "C++"
  - "Motion Capture"
  - "MotionBuilder"
  - "Visual Studio"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2015/06/dont-perform-heavy-tasks-in-the-mobu-real-time-engine-thread-even-if-it-worked-before.html "
typepad_basename: "dont-perform-heavy-tasks-in-the-mobu-real-time-engine-thread-even-if-it-worked-before"
typepad_status: "Publish"
---

<p>Recently we received a question about crashing while calling FBPlayerControl::GotoStart() from within a real-time device engine thread. It seems working fine up to MoBu 2014, but crashes in MoBu 2015.</p>  <p>Actually, it seems working fine in previous MoBu releases, but as the best practice, it’s not recommended because the SDK documentation clearly mentions that some methods like DeviceIONotify() and DeviceEvaluationNotify() are called by the Real-time engine thread, and they should consume as little CPU time as possible and return as soon as possible. So it should contain only minimal operations, and especially should avoid UI related operations like FBPlayerControl::GotoStart().</p>  <p><img alt="" src="/assets/201133154689488.png" /></p>  <p>Back to the issue – what if you’d like to control the player using an external device and access the FBPlayerControls from these methods?</p>  <p>To implement such feature, you should move the UI operation codes (FBPlayerControl::GotoStart()) out of the real-time engine (DeviceIONotify() and DeviceEvaluationNotify()) methods, </p>  <p>and cache the status of the operation from an OnUIIdle callback and call the FBPlayerControl::GotoStart() from there. I wrote a code snippet below to demonstrate how to solve the crash issue mentioned at the beginning of the post.</p>  <p><strong>Initialize:</strong></p>  <pre class="brush:cpp;toolbar: false;">m_NeedToPlay = false;
FBSystem().TheOne().OnUIIdle.Add( this,(FBCallback) &amp;ORDevice_Template::EventUIIdle );</pre>

<p><strong>Cache the status in DeviceEvaluationNotify:</strong></p>

<pre class="brush: cpp;toolbar: false;">bool ORDevice_Template::DeviceEvaluationNotify( kTransportMode pMode, FBEvaluateInfo* pEvaluateInfo )
{
    ... ...
    if(***)
        m_NeedToPlay = true;
    return true;
}</pre>

<p><strong>Call the UI operation in a UI Idle event:</strong></p>

<pre class="brush:cpp;toolbar: false;">void
ORDevice_Template::EventUIIdle(HISender pSender, HKEvent pEvent )
{
    if(m_NeedToPlay)
    {
        FBPlayerControl player;
        player.GotoStart();
    }
    m_NeedToPlay = false;
}</pre>
