---
layout: "post"
title: "AutoCAD + クラウド レンダリング ＝ バーチャル リアリティ"
date: "2015-11-27 01:37:20"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/11/autocad-cloud-rendering-virtual-reality.html "
typepad_basename: "autocad-cloud-rendering-virtual-reality"
typepad_status: "Publish"
---

<p>A360 サービスの 1 つにレンダリング画像を集中演算でおこなうレンダリング サービスが存在します。現在は、<strong>Rendering in Autodesk A360</strong> 名で&#0160;<a href="http://rendering.360.autodesk.com" target="_blank"><strong>http://rendering.360.autodesk.com</strong></a> からアクセスすることが出来ます。サービスの利用には、他の A360 サービスと同じように、Autodesk ID でのサインインが必要です。</p>
<p>さて、この Rendering in Autodesk A360 サービスでレンダリング出来るコンテンツには、3D モデルを含む AutoCAD 図面（.dwg ファイル）、Revit のプロジェクト（.rvt ファイル）、Fusion 360 のアーカイブ（.f3d ファイル）があり、それぞれ、製品内からレンダリング対象のファイルをアップロードして、レンダリング指示することが可能です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f1dff4970b-pi" style="display: inline;"><img alt="Autocad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f1dff4970b image-full img-responsive" src="/assets/image_752528.jpg" title="Autocad" /></a></p>
<p>もし、AutoCAD で 3D モデリングを完了させているなら、AutoCAD のレンダリング機能を使ってフォトリアリスティックな（写真のような）静止画をレンダリングすることが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0895fddd970d-pi" style="display: inline;"><img alt="Reception" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0895fddd970d image-full img-responsive" src="/assets/image_4656.jpg" title="Reception" /></a></p>
<p>この場合、レンダリング演算は AutoCAD がインストールされたコンピュータの CPU やメモリを利用することになるので、レンダリング中は AutoCAD で他の作業をおこなうことは出来ません。もちろん、夜間にレンダリングを実行させれば、この問題は解決出来るとも言えます。</p>
<p>ただ、せっかく作成した 3D モデルは、さらに別のコンテンツ作成でも活用することが出来る事も忘れないでください。バーチャル リアリティを使ったプレゼンテーションです。AutoCAD の [ビジュアライズ] リボンタブをアクティブにして、[クラウドでレンダリング] ボタンをクリックするだけで、単なる静止画だけでないステレオ パノラマ画像を作成ステップに進めます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f1e067970b-pi" style="display: inline;"><img alt="Cloud_rendering" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f1e067970b image-full img-responsive" src="/assets/image_875591.jpg" title="Cloud_rendering" /></a></p>
<p>このボタンをクリックすると、まず、静止画としてレンダリングする <a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-6FB611DC-25CD-488E-ADDF-400118894018" target="_blank"><strong>名前の付いたビュー</strong></a> を指定して、[レンダリングを開始] ボタンでクラウドでのレンダリング処理を開始することが出来ます。このとき、AutoCAD 製品内で Autodesk ID を使ってサインインされていることが前提となります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d17b99d1970c-pi" style="display: inline;"><img alt="Specify_view" class="asset  asset-image at-xid-6a0167607c2431970b01b8d17b99d1970c img-responsive" src="/assets/image_925520.jpg" style="width: 400px;" title="Specify_view" /></a></p>
<p>クラウド上でのレンダリング処理が完了すると、Autodesk ID に登録されているメール アドレスに通知が届きます。続いて、<a href="http://rendering.360.autodesk.com" target="_blank"><strong>http://rendering.360.autodesk.com</strong></a>&#0160;にアクセスして、ブラウザ画面からステレオ パノラマの作成を指示します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0895febf970d-pi" style="display: inline;"><img alt="Specify_stereo_panorama" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0895febf970d image-full img-responsive" src="/assets/image_44402.jpg" title="Specify_stereo_panorama" /></a></p>
<p>適宜、周辺光を含む背景画像を反映する「環境」やレンダリング品質、解像度などを設定したら、[レンダリング開始] ボタンをクリックして、再び、クラウドでのレンダリング処理を開始してください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0895fed4970d-pi" style="display: inline;"><img alt="Specify_stereo_panorama_settings" class="asset  asset-image at-xid-6a0167607c2431970b01bb0895fed4970d img-responsive" src="/assets/image_252047.jpg" style="width: 400px;" title="Specify_stereo_panorama_settings" /></a></p>
<p>「完了したら私に電子メールで通知する」にチェックをした場合には、ステレオ パノラマ画像の作成が完了時に通知メールを受け取れます。</p>
<p>作成されたステレオ パノラマ画像は、スマートフォンで活用することを前提にチューニングされています。まず、Web ブラウザで&#0160;Rendering in Autodesk A360 ページを表示して、当該画像の左下にある <strong>Preview on your phone</strong> チェックボックスにチェックを入れてみてください。しばらくすると、隣にあるテキスト ボックスに、この画像を参照表示するための URL が表示されるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f1e1e1970b-pi" style="display: inline;"><img alt="Stereo_panorama" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f1e1e1970b image-full img-responsive" src="/assets/image_468048.jpg" style="width: 596.547px;" title="Stereo_panorama" /></a></p>
<p>この URL 直接利用するか、URL と同時に生成される QR コードを読み取るなどして、スマートフォンの Web ブラウザでステレオ パノラマ画像を表示すると、スマートフォンを上下左右に傾けることで、AutoCAD の名前の付いたビューの周囲 360 を俯瞰することが出来ます。画像は左右に分かれていて、互いに視差を持つため、立体的なモデルに見えるはずです。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/gd3oWXj4Nzw?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>参考までに、Rendering in Autodesk A360 で作成したステレオ パノラマ画像を 3 つ記載しておきます。それぞれの画像に URL を埋め込んでいますので、スマートフォン上でタップしてみてください。可能であれば、Google Cardboard キットにスマートフォンを差し込んでお使いいただくと、より立体感を感じられます。Google Cardboard キットは、大手文具店や<a href="http://hacosco.com/" target="_blank"><strong>ハコスコ</strong></a>などから 1000 円から 2000円の価格でご購入いただけます。</p>
<p><strong>&lt;オフィスタワー エントランス&gt;</strong><br /><a class="asset-img-link" href="http://pano.autodesk.com/pano.html?url=jpgs/1d7e65d7-675a-4755-a8e0-8d62dc3d1504" style="display: inline;" target="_blank"><img alt="図1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d17b9bab970c image-full img-responsive" src="/assets/image_427025.jpg" title="図1" /></a></p>
<p><strong>&lt;LRT 車内&gt;</strong><br /><a class="asset-img-link" href="http://pano.autodesk.com/pano.html?url=jpgs/a91a587b-fd16-43c0-975c-448ccdce8e14" style="display: inline;" target="_blank"><img alt="図2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d17b9bb1970c image-full img-responsive" src="/assets/image_616850.jpg" title="図2" /></a></p>
<p><strong>&lt;受付エリア入口&gt;<br /></strong><a class="asset-img-link" href="http://pano.autodesk.com/pano.html?url=jpgs/595f53bb-8c0f-439a-aa31-e47cf0af61a8" style="display: inline;" target="_blank"><img alt="図3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0896002f970d image-full img-responsive" src="/assets/image_271225.jpg" title="図3" /></a></p>
<p>スマートフォンがあれば、AutoCAD で作成した 3D モデルでバーチャル リアリティを体感できる時代です。</p>
<p>もちろん、Revit や Fusion 360 で作成したモデルに対しても、ステレオ パノラマ画像を活用可能です。<br />ぜひ、お試しください。</p>
<p>By Toshiaki Isezaki</p>
