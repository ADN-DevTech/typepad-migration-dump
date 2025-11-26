---
layout: "post"
title: "MotionBuilder　ちょっといいはなし"
date: "2014-02-12 23:16:26"
author: "Akira Kudo"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/02/motionbuider-tips.html "
typepad_basename: "motionbuider-tips"
typepad_status: "Publish"
---

<h3>Autodesk Developer Networkの工藤　暁です。今回はAutodesk® MotionBuilder®にてTipsになる様な御質問に頂きましたので此方にて御紹介させて頂きます。</h3>
<h3>MotionBuilderにて取り扱うデータが大きくなるとアニメーションの再生がスムーズに行われなくなるといった現象に遭遇されたご経験は無いでしょうか。この問題に遭遇されたユーザー様が偶然Storyタブを閉じるとこの現象が起こらなくなる事を発見され、アニメーションを再生した際に自動でStoryタブを閉じたい旨ご要望を受けました。残念ながら通常のオペレーションにてこの指定を行う事は出来ません。そこでプログラムで解決されたいというご質問でした。</h3>
<h3>Python又はAPIにてこのタブを変更するには,FBPopNormalTool()を御利用頂きます。引数として変更したいタブ名を下記の様にご指定頂きます。</h3>
<h3>FBPopNormalTool(&quot;Story&quot;)</h3>
<h3>FBPopNormalTool(&quot;Navigator&quot;)</h3>
<h3>このコマンドを下記のスクリプトの様にユーザーインターフェイスのアイドル時に再生ボタンのＯＮ／ＯＦＦを確認して切り替えられては如何でしょうか。</h3>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01a3fcbbb905970b img-responsive"><a href="http://adndevblog.typepad.com/files/test.py">Download Test.py</a></span></p>
<p>&lt;code_begin&gt;</p>
<p>from pyfbsdk import *</p>
<p>from datetime import *</p>
<p>from time import *</p>
<p>import threading</p>
<p>&#0160;</p>
<p>lSystem = FBSystem()</p>
<p>lSysOnUIIdle = lSystem.OnUIIdle</p>
<p>lSysOnUIIdle.RemoveAll()</p>
<p>lPlayer = FBPlayerControl()</p>
<p>lPlayMode = -1</p>
<p>&#0160;</p>
<p>def test(pOjbect, pEventName):</p>
<p>&#0160;&#0160;&#0160; global lPlayMode</p>
<p>&#0160;&#0160;&#0160; if lPlayMode == -1:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if lPlayer.IsPlaying:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lPlayMode =&#0160; 0</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FBPopNormalTool(&quot;Navigator&quot;)</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print &quot;Play Start&quot;</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;else:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lPlayMode =&#0160; -1&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<p>&#0160;&#0160;&#0160; else:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if lPlayer.IsPlaying:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lPlayMode =&#0160; 0</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; else:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; #lSysOnUIIdle.Remove(test)</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lPlayMode =&#0160; -1</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; FBPopNormalTool(&quot;Story&quot;)&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print &quot;Play Stop&quot;</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<p>lSysOnUIIdle.Add(test)</p>
<p>&lt;code_end&gt;</p>
<p>&#0160;</p>
<p>何かのお役に立てば幸いです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcbbb8e0970b-pi" style="display: inline;"><img alt="Story" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcbbb8e0970b image-full img-responsive" src="/assets/image_856261.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Story" /></a></p>
