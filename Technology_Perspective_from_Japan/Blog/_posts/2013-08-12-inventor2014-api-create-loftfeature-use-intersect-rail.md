---
layout: "post"
title: "Inventor2014 APIで「断面」と「レール」を使って「ロフトフィーチャー」を作成する方法"
date: "2013-08-12 03:05:41"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/08/inventor2014-api-create_loftfeature-use-intersect-rail.html "
typepad_basename: "inventor2014-api-create_loftfeature-use-intersect-rail"
typepad_status: "Publish"
---

<p>Inventor2014製品の「ロフト」コマンドでは「断面」と「レール」を指定して「ロフトフィーチャー」が作成できますが、APIを使って「断面」と「レール」が一緒のプロファイル化ではエラーとなり「ロフトフィーチャー」が作成できません。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac7efca6970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac7efca6970d" alt="LoftDialog" title="LoftDialog" src="/assets/image_944538.jpg" /></a><br /> <br />
これはAPIの内部動作仕様から、Profile3Dを作成するために3Dスケッチでスケッチエンティティを指定しても、プロファイル作成時のProfiles3D.AddOpenは常に最初のスケッチ曲線を使用している振る舞いにありますので、一旦ロフトフィーチャーを作成する際に「更新用のスケッチ3D」を準備した状態で「断面」だけの情報で作成し、その後に一旦作成したロフトフィーチャーを用いて「レール」のプロファイル情報を指定し、「断面」＋「レール」情報によるロフトフィーチャーを作成する方法になります。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac7efddb970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac7efddb970d" alt="8330Loft1" title="8330Loft1" src="/assets/image_768064.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac7efe31970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac7efe31970d" alt="8330Loft2" title="8330Loft2" src="/assets/image_372949.jpg" /></a><br />  <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac7efe8c970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac7efe8c970d" alt="8330Loft3" title="8330Loft3" src="/assets/image_862754.jpg" /></a><br /></p>

<p>Public Sub CreateLoftFeature()<br />
	Dim oDoc As PartDocument<br />
	Set oDoc = ThisApplication.ActiveDocument</p>

<p>	' 「断面」のスケッチ3Dの確保<br />
	Dim oSk3dCrossSectionOrg As Sketch3D<br />
	Set oSk3dCrossSectionOrg = oDoc.ComponentDefinition.Sketches3D.Item(2)<br />
	' 「断面」のプロファイル化<br />
	Dim oProfile1 As Profile3D<br />
	Set oProfile1 = oSk3dCrossSectionOrg.Profiles3D.AddOpen</p>

<p>	' スケッチ3Dの作成<br />
	Dim oSk3dCrossSectionOrg1 As Sketch3D<br />
	Set oSk3dCrossSectionOrg1 = oDoc.ComponentDefinition.Sketches3D.Add<br />
	' 更新用のスケッチ3Dとして準備<br />
	Call oSk3dCrossSectionOrg1.Include(oSk3dCrossSectionOrg.SketchLines3D.Item(3))<br />
	' 更新用のスケッチをプロファイル化<br />
	Dim oProfile2 As Profile3D<br />
	Set oProfile2 = oSk3dCrossSectionOrg1.Profiles3D.AddOpen</p>

<p>	' オブジェクトコレクションを作成<br />
	Dim oSections As ObjectCollection<br />
	Set oSections = ThisApplication.TransientObjects.CreateObjectCollection<br />
	Call oSections.Add(oProfile1)<br />
	Call oSections.Add(oProfile2)</p>

<p>	' 一旦、「断面」だけの情報でロフトフィーチャーを作成する<br />
	Dim oLoftDefinition As LoftDefinition<br />
	Set oLoftDefinition = oDoc.ComponentDefinition.Features.LoftFeatures.CreateLoftDefinition(oSections, kSurfaceOperation)</p>

<p>	'「レール」のスケッチ3Dの確保<br />
	Dim oSk3dRailOrg As Sketch3D<br />
	Set oSk3dRailOrg = oDoc.ComponentDefinition.Sketches3D.Item(1)<br />
	' 「レール」のプロファイル化<br />
	Dim oRail As Profile3D<br />
	Set oRail = oSk3dRailOrg.Profiles3D.AddOpen</p>

<p>	' 「断面」だけの情報で作成したロフトフィーチャーに「レール」を指定<br />
	oLoftDefinition.LoftRails.Add oRail</p>

<p>	' 「断面」＋「レール」情報によるロフトフィーチャーの作成<br />
	Call oDoc.ComponentDefinition.Features.LoftFeatures.Add(oLoftDefinition)<br />
End Sub</p>

<p>サンプルファイルは <span class="asset  asset-generic at-xid-6a0167607c2431970b019104b59ee2970c"><a href="http://adndevblog.typepad.com/files/createloftfeature.zip">ここ</a></span> よりダウンロードできます。</p>

<p>by Shigekazu Saito</p>
