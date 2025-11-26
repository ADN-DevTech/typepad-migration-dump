---
layout: "post"
title: "Forge Viewer バージョン 6.5 リリース"
date: "2019-03-11 00:04:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/release-forge-viewer-v6-5.html "
typepad_basename: "release-forge-viewer-v6-5"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44458ca200c-pi" style="display: inline;"></a>Forge Viewer の新バージョン 6.5 がリリースされていますので、その機能や変更点をご紹介しておきます。旧バージョンを利用し続ける場合には、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/another-way-to-specify-forge-viewer-version.html" rel="noopener" target="_blank">Forge Viewer バージョン指定のもう1つの方法</a></strong> をご確認ください。</p>
<h3 id="ReleaseNotes6.5-Added">新機能</h3>
<ul>
<li>
<p>モデルブラウザ上のオブジェクト名横に、同タイプの要素数をカッコ内に表示するようになっています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4424821200c-pi" style="display: inline;"><img alt="Model_browser" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4424821200c image-full img-responsive" src="/assets/image_592526.jpg" title="Model_browser" /></a></p>
</li>
<li>WebGLRenderer は WebGL2 コンテキストを利用出来るようになっています。</li>
<li>
<p>Fusion Animation Extension <code>Autodesk.Fusion360.Animation</code> を使って Forge Viewer で Fusion 360 アニメーションを<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/09/fusion-360-animation-play-on-forge-viewer.html" rel="noopener" target="_blank">再生</a></strong>する際、カメラ（視点の動き）をアニメーションから切り離す新しいメソッドが追加されています。:<br /><code>setFollowCamera(bool)</code><br /><code>isFollowingCamera():bool<br /></code></p>
<p><code>setFollowCamera(false)</code> が呼び出されると。再生アニメーションはカメラ（視点の動き）から分離されます。同様に、ユーザがモデルを周回してもアニメーションは停止しません。 デフォルトの動作は変わりません。 ユーザ インタフェースも変更されていないため、この機能は JavaScript を介してのみ有効にすることが出来ます。下図は GIF アニメーションのため、色域の関係で Viewer 本来の発色から劣化して表示されています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46b6ac3200d-pi" style="display: inline;"><img alt="SetFollowCamera" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46b6ac3200d image-full img-responsive" src="/assets/image_129099.jpg" title="SetFollowCamera" /></a></p>
</li>
<li>
<p>viewer.setThemingColor() 関数への再帰フラグ:</p>
<pre>setThemingColor( dbId, color [,&#0160;model [,&#0160;recursive&#0160;]&#0160;]&#0160;)</pre>
<div class="table-wrap">
<table class="confluenceTable"><colgroup><col /><col /><col /><col /><col /></colgroup>
<tbody>
<tr>
<td class="confluenceTd" colspan="1">&#0160;</td>
<td class="confluenceTd"><code>dbId</code></td>
<td class="confluenceTd"><em><span class="param-type">number</span></em></td>
<td class="confluenceTd">&#0160;</td>
<td class="confluenceTd">&#0160;</td>
</tr>
<tr>
<td class="confluenceTd" colspan="1">&#0160;</td>
<td class="confluenceTd"><code>color</code></td>
<td class="confluenceTd"><em><span class="param-type">THREE.Vector4</span></em></td>
<td class="confluenceTd">&#0160;</td>
<td class="confluenceTd">
<p>(r, g, b, intensity), all in [0,1].</p>
</td>
</tr>
<tr>
<td class="confluenceTd" colspan="1">&#0160;</td>
<td class="confluenceTd"><code>model</code></td>
<td class="confluenceTd"><em><span class="param-type">Autodesk.Viewing.Model</span></em></td>
<td class="confluenceTd">&lt;optional&gt;</td>
<td class="confluenceTd">
<p>For multi-model support.</p>
</td>
</tr>
<tr>
<td class="confluenceTd" colspan="1">NEW!</td>
<td class="confluenceTd"><code>recursive</code></td>
<td class="confluenceTd"><em><span class="param-type">boolean</span></em></td>
<td class="confluenceTd">&lt;optional&gt;</td>
<td class="confluenceTd">
<p>Should apply theming color recursively to all child nodes.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li>
<li><a class="external-link" href="https://jira.autodesk.com/browse/LMV-3934" rel="nofollow noopener" target="_blank"></a><code>model.getPropertyDb().executeUserFunction():Promise</code>&#0160;<br />PropertyDatabase インスタンスに対して、ワーカースレッドでユーザー指定の機能コードを実行することを許可します。 提供された関数から返された値は、返された Promise を解決するために使用されます。<br /><br />例:<br />
<pre>    executeUserFunction(function(pdb) {<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; var dbId = 1;<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pdb.enumObjectProperties(dbId, function(propId, valueId) {<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; // do stuff<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });<br />&#0160;&#0160;&#0160; })</pre>
</li>
<li><a class="external-link" href="https://jira.autodesk.com/browse/LMV-4174" rel="nofollow noopener" target="_blank"></a>新し vector レンダラを使用して PDF をレンダリングするタイミングを決定するための新しい&#0160;<code>totalRasterPixels</code> プロパティが導入されています。 （このプロパティがデリバティブサービスで有効になると、）約1メガピクセル未満のラスターイメージを含む PDF ページは vector レンダラでレンダリングされます。 それ以外の場合は、Leaflet レンダラが引き続き使用されます。</li>
</ul>
<h3 id="ReleaseNotes6.5-Changed">変更点</h3>
<ul>
<li>アニメーションでモデルを適切にサポートするために、Autodesk.Fusion360.Animation が BubbleNode の Extension に存在する場合、オンデマンドロードはviewer.loadDocumentNode（）で無効にされています。</li>
<li><a class="external-link" href="https://jira.autodesk.com/browse/LMV-3886" rel="nofollow noopener" target="_blank"></a>Explodeスライダーの UI コードは、GuiViewer3D から Autodesk.Explode Extensiom に移動しています。</li>
<li><a class="external-link" href="https://jira.autodesk.com/browse/LMV-4069" rel="nofollow noopener" target="_blank"></a>設定パネル（4.1.0で更新されて以来、画面中央に固定されていました）は現在フローティング状態になっており、再びキャンバス上を移動できます。下図は GIF アニメーションのため、色域の関係で Viewer 本来の発色から劣化して表示されています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4430522200c-pi" style="display: inline;"><img alt="Settings_panel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4430522200c image-full img-responsive" src="/assets/image_709372.jpg" title="Settings_panel" /></a></li>
<li><a class="external-link" href="https://jira.autodesk.com/browse/BLMV-3048" rel="nofollow noopener" target="_blank" title="Open issue in JIRA"></a>LevelsExtension の断面平面のコードは、viewerState.cutplanes から viewerState.floorGuid に移動されています。</li>
</ul>
<p>By Toshiaki Isezaki</p>
