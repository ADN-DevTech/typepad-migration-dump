---
layout: "post"
title: "Forge Viewer で利用可能な Extension"
date: "2016-12-05 01:13:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/12/extensions-abailable-on-forge-viewer.html "
typepad_basename: "extensions-abailable-on-forge-viewer"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b8b929970b-pi" style="float: left;"><img alt="Icon - viewer" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b8b929970b img-responsive" src="/assets/image_719592.jpg" style="width: 80px; margin: 0px 5px 5px 0px;" title="Icon - viewer" /></a>Forge Viewer には、Extension と呼ばれるフレームワークがあり、JavaScript で作成したファイル単位で Viewer 自身に機能を追加して拡張していくことが出来るようになっています。</p>
<p><br />Extension について分かりやすく説明するなら、Auto<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2427fa0970c-pi" style="float: right;"><img alt="Plugin" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2427fa0970c img-responsive" src="/assets/image_392260.jpg" style="margin: 0px 0px 5px 5px;" title="Plugin" /></a>CAD や Revit、Inventor のようなデスクトップ製品のアドイン/プラグイン アプリケーションに例えられます。デスクトップ製品の API で作成した アプリケーションは、対象のデスクトップ製品にロードすることで、製品自体の機能を拡張することが出来ます。</p>
<p>Forge Viewer の Extension も、同様の目的を達成するためのメカニズムです。Extension を利用することで、管理者権限を持つメンバだけに Extension をロードして特定の機能を利用したり、権限のないユーザの利用時に Extension をロード解除して特定の機能を無効にするといった制御も可能になります。&#0160;</p>
<p>さて、Forge Viewer 自体も、この Extension を使って拡張されてきています。前述の Live Review Extension のように、他にも Extension をロードするだけで、機能を拡張することが出来るわけです。逆に、ロード中の Extension をロード解除することで、特定のワークフロー用にカスタマイズした Viewer から、不要なツールボタンを削除して機能を無効化することも出来ます。</p>
<p>現在利用可能な Extension は、次のとおりです。この内容は、<a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/extensions/" rel="noopener noreferrer" target="_blank"><strong>デベロッパ ポータル</strong> </a>にも記載されていますが、日本語の説明部分のみ翻訳しています。</p>
<table border="1" class="docutils">
<thead valign="bottom">
<tr class="row-odd">
<th class="head" style="text-align: center;"><span style="font-family: arial, helvetica, sans-serif;">ID</span></th>
<th class="head" style="text-align: center;"><span style="font-family: arial, helvetica, sans-serif;">説明</span></th>
</tr>
</thead>
<tbody valign="top">
<tr class="row-even">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Beeline</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">異なる位置を指定してシーンを移動するウォークスルー&#0160;</span><span style="font-family: arial, helvetica, sans-serif;">ナビゲーションするツールを提供<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b8be08970b-pi" style="display: inline;"><img alt="Autodesk.Beeline" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b8be08970b image-full img-responsive" src="/assets/image_848601.jpg" title="Autodesk.Beeline" /></a><br /></span></p>
</td>
</tr>
<tr class="row-odd">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.CAM360</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">CAM データを含むファイル変換された際に追加 UI を提供</span></p>
</td>
</tr>
<tr class="row-even">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Viewing.Collaboration</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">他のクライアントとのリアルタイム コラボレーションを提供（</span><span style="font-family: arial, helvetica, sans-serif;">Live Review）<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb095bc4fd970d-pi" style="display: inline;"><img alt="Autodesk.Viewing.Collaboration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb095bc4fd970d image-full img-responsive" src="/assets/image_340831.jpg" title="Autodesk.Viewing.Collaboration" /></a><br /></span></p>
</td>
</tr>
<tr class="row-odd">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.DefaultTools.NavTools</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">シーンをナビゲーションする既定のカメラ ツールを提供<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb095bc54e970d-pi" style="display: inline;"><img alt="Autodesk.DefaultTools.NavTools" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb095bc54e970d image-full img-responsive" src="/assets/image_9252.jpg" title="Autodesk.DefaultTools.NavTools" /></a><br /></span></p>
</td>
</tr>
<tr class="row-even">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.FirstPerson</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">周囲を見回しながら W、A、S、D、Q、の各Eキーを使ってシーンをウォークスルーするツールを提供<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb095bc588970d-pi" style="display: inline;"><img alt="Autodesk.FirstPerson" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb095bc588970d image-full img-responsive" src="/assets/image_51127.jpg" title="Autodesk.FirstPerson" /></a><br /></span></p>
</td>
</tr>
<tr class="row-odd">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Fusion360.Animation</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">Fusion 360 のアニメーション データを含むファイルを変換した際に追加 UI を提供</span></p>
</td>
</tr>
<tr class="row-even">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Viewing.FusionOrbit</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">Fusion 360 のようなカメラ オービット ツールを提供</span></p>
</td>
</tr>
<tr class="row-odd">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Fusion360.Simulation</span></code></span></p>
</td>
<td>
<p>Fusion 360 のシミュレーション データを含むファイルを変換した際に追加 UI を提供</p>
</td>
</tr>
<tr class="row-even">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Hyperlink</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">ハイパーリンク&#0160;データを含むファイルを表示した際に追加機能を提供</span></p>
</td>
</tr>
<tr class="row-odd">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.InViewerSearch</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">表示中のファイルのプロパティを使った検索機能のサポートを提供<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b8be8b970b-pi" style="display: inline;"><img alt="Autodesk.InViewerSearch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b8be8b970b image-full img-responsive" src="/assets/image_298124.jpg" title="Autodesk.InViewerSearch" /></a><br /></span></p>
</td>
</tr>
<tr class="row-odd">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Measure</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">2D や 3D モデルを計測するツールを提供<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b8bd9d970b-pi" style="display: inline;"><img alt="Autodesk.Measure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b8bd9d970b image-full img-responsive" src="/assets/image_802123.jpg" title="Autodesk.Measure" /></a><br /></span></p>
</td>
</tr>
<tr class="row-even">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Viewing.RemoteControl</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">ゲームパッドのようなリモート デバイスを使ったシーン ナビゲーションのサポートを提供（機能未実装）<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2428510970c-pi" style="display: inline;"><img alt="Autodesk.Viewing.RemoteControl" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2428510970c image-full img-responsive" src="/assets/image_774954.jpg" title="Autodesk.Viewing.RemoteControl" /></a><br /></span></p>
</td>
</tr>
<tr class="row-odd">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Section</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">3D モデルの断面解析ツールを提供<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2428384970c-pi" style="display: inline;"><img alt="Autodesk.Section" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2428384970c image-full img-responsive" src="/assets/image_181473.jpg" title="Autodesk.Section" /></a><br /></span></p>
</td>
</tr>
<tr class="row-even">
<td>
<p><code class="docutils literal"></code><span style="font-family: arial, helvetica, sans-serif;"><code class="docutils literal"><span class="pre">Autodesk.Viewing.ZoomWindow</span></code></span></p>
</td>
<td>
<p><span style="font-family: arial, helvetica, sans-serif;">ユーザが指定した窓範囲領域のシーンを拡大表示するツールを提供（既存のズーム機能とツールボタンの切り替えが可能）<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb095bc7ee970d-pi" style="display: inline;"><img alt="Autodesk.Viewing.ZoomWindow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb095bc7ee970d image-full img-responsive" src="/assets/image_976609.jpg" title="Autodesk.Viewing.ZoomWindow" /></a><br /></span></p>
</td>
</tr>
</tbody>
</table>
<p>Extension は、今後も新機能とともに提供される予定です。今後導入が予定されている Extension &#0160;には、バーチャル リアリティ機能を実現するものもあります。利用可能になった時点でお知らせしますが、Extension で 3rd Party の開発者も機能拡張出来る点もお忘れなく。</p>
<p>By Toshiaki Isezaki</p>
