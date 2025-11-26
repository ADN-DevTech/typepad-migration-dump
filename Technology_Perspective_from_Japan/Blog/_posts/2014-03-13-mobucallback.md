---
layout: "post"
title: "MotionBuilderコールバック関数のちょっといい話し"
date: "2014-03-13 01:00:58"
author: "Akira Kudo"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/03/mobucallback.html "
typepad_basename: "mobucallback"
typepad_status: "Publish"
---

<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">Autodesk Developer Networkの工藤　暁です。今回はAutodesk® MotionBuilder®にてTipsになる様な御質問に頂きましたので此方にて御紹介させて頂きます。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">MotionBuilderにてコールバック関数を登録した際に、マニュアルで行った際にはコールバック関数が呼び出されるが、APIにて同様の操作を行うとコールバック関数が呼び出されないという現象を経験された事はないでしょうか。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">例えば下記様なスクリプトにて、コールバック関数を登録し、カメラを作成します。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&lt;code_begin&gt;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">from pyfbsdk import *</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">#----------- カメラ作成----------------------------------</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">lExportCamera = FBCamera(&quot;ExportCamera&quot;)</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">#----------- コールバック関数----------------------------------</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">def SceneChanged(scene, event):</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160; if event.Type == FBSceneChangeType.kFBSceneChangeDetach:</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if event.ChildComponent != None and event.ChildComponent.Is(FBCamera_TypeInfo()):</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if event.ChildComponent.Name == &quot;ExportCamera&quot;:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print event.ChildComponent.Name</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print event.Type</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">#----------- コールバック関数登録----------------------------------</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">FBSystem().Scene.OnChange.Add(SceneChanged)</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&lt;code_end&gt;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">これを実行し、マニュアル操作にてスクリプトにて作成したカメラを削除します。削除の際にPythonのコンソールには下記の様に、このイベントに関連するオブジェクト名とイベント名が表示されます。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">ExportCamera</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">kFBSceneChangeDetach</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">次に下記様なスクリプトにて、カメラを作成後にAPIにてそのカメラを削除します。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&lt;code_begin&gt;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">from pyfbsdk import *</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;#----------- カメラ作成----------------------------------</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">lExportCamera = FBCamera(&quot;ExportCamera&quot;)</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;#----------- カメラ削除----------------------------------</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">for lCamera in FBSystem().Scene.Cameras:</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160; if lCamera.Name == &quot;ExportCamera&quot;:</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lCamera.FBDelete()&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&lt;code_end&gt;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">ここでマニュアル操作にてコールバック関数にて表示された、イベントに関連するオブジェクト名とイベント名が表示される事を期待するのですが、実際は表示されません。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">ではAPIにて削除するとシステムはどの様な動作をするのでしょうか。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">初めにSDKオブジェクトにバインドされたシステムオブジェクトをデタッチしてからコールバック関数が呼び出されます、この時未だSDKオブジェクトは削除されていません。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">よって、下記の様にコールバック関数を修正すると、呼び出された事を確認できますが、オブジェクト名はデタッチの影響により表示されません。</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&lt;code_begin&gt;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">def SceneChanged(scene, event):</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160; if event.Type == FBSceneChangeType.kFBSceneChangeDetach:</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; if event.ChildComponent != None and event.ChildComponent.Is(FBCamera_TypeInfo()):</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print event.ChildComponent.Name</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; print event.Type</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">&lt;code_end&gt;</span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51183c6f5970c-pi" style="display: inline;"><img alt="Capture2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a51183c6f5970c image-full img-responsive" src="/assets/image_850303.jpg" title="Capture2" /></a></span></h2>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 11pt;">何かのお役に立てば幸いです。</span></h2>
