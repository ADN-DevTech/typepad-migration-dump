---
layout: "post"
title: "Reality Capture API と関連製品"
date: "2016-08-08 17:30:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/08/reality-capture-api-and-related-products.html "
typepad_basename: "reality-capture-api-and-related-products"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0927e99c970d-pi" style="float: left;"><img alt="Icon - reality capture" class="asset  asset-image at-xid-6a0167607c2431970b01bb0927e99c970d img-responsive" src="/assets/image_396426.jpg" style="width: 180px; margin: 0px 5px 5px 0px;" title="Icon - reality capture" /></a>Forge Platform API のうち、まだ正式にリリースされていないベータ版の API がいくつか存在します。その 1 つに、複数の写真から 3D モデルを合成して生成する <strong><a href="https://developer.autodesk.com/en/docs/reality-capture/v1/overview/" target="_blank">Reality Capture API</a>&#0160;</strong>があります。Reality Capture API は、もともと&#0160;&#0160;ReCap Photo API として提供されていた Web サービス API です。Autodesk Forge に編入することを前提に、いままで得られたフィードバックの反映を加えて名称変更されたものです。</p>
<p>Reality Capture API については、今年 3 月の 3D Robotics 社との協調ビジネスについての&#0160;<strong><a href="http://www.autodesk.co.jp/adsk/servlet/item?siteID=1169823&amp;id=25219669" target="_blank">プレス リリース</a></strong>&#0160;以降、国土交通省の <strong><a href="http://www.mlit.go.jp/tec/tec_tk_000028.html" target="_blank">i-Construction</a></strong>&#0160;の推進もあって、多くのお問い合わせを受けるようになっています。今回は、今後正式にリリースされる予定 Reality Capture API と関連する既存製品について、よく頂戴する疑問点とその回答を、可能な範囲でご紹介しておきたいと思います。なお、繰り返しになってしまいますが、Reality Capture API は、まだベータ版の扱いであるため、ここで記した内容は、正式リリース時に変更される可能性がある点にご注意ください。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q：</span>&#0160;プレス リリースにリンクのある <strong><a href="https://www.youtube.com/watch?v=6BTC1Zjo8Ic&amp;feature=youtu.be" target="_blank">YouTube ビデオ</a></strong> のうち、3D Robotics の Site Scan™ で、具体的にどの部分でオートデスクのテクノロジが利用されているのでしょうか？</p>
<p><span style="font-size: 15pt;">A:&#0160;</span>&#0160;Site Scan™ で利用されている Forge Platform API は、複数の写真から 3D モデルを合成して生成する Reality Capture API のみです（除く、3D データ編集用の既存オートデスク製品）。タブレットでなぞった領域から飛行航路を自動的に導く部分を含め、その他の処理には 3D Robotics 社のテクノロジが使われています。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q：&#0160;</span>Reality Capture API が生成できるデータは 3D メッシュだけでしょうか？また、他の形式の 3D データは生成出来ないのでしょうか？</p>
<p><span style="font-size: 15pt;">A:&#0160;&#0160;</span>Reality Capture API は、3D メッシュの他に、点群データやオルソ画像を生成することが出来ます。ReCap 360 の「超高」 品質を指定した場合と同様です。なお、下記にサンプルとして表示しているのは、米国カルフォリニア州オークランドの旧 <strong><a href="https://commons.wikimedia.org/wiki/Category:16th_Street_Station,_Oakland,_California" target="_blank">16th Street Station</a></strong>&#0160;をドローン空撮したものから生成された成果物です。</p>
<ul>
<li>GeoTIFF ファイル形式でのオルソ画像と DEM （エレベーションマップ）<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20fe15a970c-pi" style="display: inline;"><img alt="Ortho" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20fe15a970c img-responsive" src="/assets/image_190807.jpg" style="width: 420px;" title="Ortho" /></a><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8862b3b970b-pi" style="display: inline;"><img alt="Elevation" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8862b3b970b img-responsive" src="/assets/image_121411.jpg" style="width: 420px;" title="Elevation" /></a></li>
<li>RCP/RCS と LAS ファイル形式での 3D 点群<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20eab73970c-pi" style="display: inline;"><img alt="Pointcloud" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20eab73970c img-responsive" src="/assets/image_803852.jpg" style="width: 420px;" title="Pointcloud" /></a></li>
<li>FBX、RCM（Autodesk ReMake） 、OBJ の各ファイル形式の 3D メッシュ&#0160;<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20eab7a970c-pi" style="display: inline;"><img alt="3dmesh" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20eab7a970c img-responsive" src="/assets/image_534878.jpg" style="width: 420px;" title="3dmesh" /></a>&#0160;</li>
</ul>
<p><span style="font-size: 15pt;">Q：&#0160;</span>Reality Capture API でアップロードすることが出来る画像ファイル形式は何ですか？</p>
<p><span style="font-size: 15pt;">A:&#0160;</span>&#0160;現在のところ、JPEG 画像だけがサポートされています。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q：&#0160;</span>写真から 3D メッシュを生成するクラウド サービスに <strong>ReCap 360</strong>（<strong><a href="https://recap360.autodesk.com/" target="_blank">https://recap360.autodesk.com/</a></strong>） があります。また、自分の PC で写真から 3D メッシュを演算・生成する <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/05/autodesk-remake-released.html" target="_blank">Autodesk ReMake</a></strong>&#0160; もリリースされています。これらのクラウド サービスや製品は、Reality Capture API で生成する 3D メッシュと比べて、その精度に違いがあるのでしょうか？</p>
<p><span style="font-size: 15pt;">A:&#0160;</span>&#0160;ReCap 360、Autodesk ReMake、Reality Capture API が内部的に使用しているテクノロジや API は同じものです。指定した精度やパラメータによって、多少の差異が発生する可能性はありますが、基本的に同じ結果を生成するはずです。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q:</span> Reality Capture API や ReCap 360 クラウド サービスで生成した 3D データをダウンロードして編集する場合、どのような製品が推奨されますか？</p>
<p><span style="font-size: 15pt;">A:</span> ダウンロードしたファイル形式によって異なります。点群データを RCS ファイル形式でダウンロードした場合には、<strong><a href="http://www.autodesk.com/products/recap-360/overview" target="_blank">Autodesk ReCap</a></strong>（または Autodesk&#0160;ReCap&#0160;&#0160;Pro）が推奨されます。3D メッシュ データ（OBJ、RCM ファイル形式等）を編集する場合には、 <strong><a href="http://www.autodesk.com/products/mudbox/overview" target="_blank">Autodesk Mudbox</a>&#0160;</strong>や<strong>&#0160;</strong><strong><a href="https://memento.autodesk.com/about" target="_blank">Autodesk ReMake</a></strong>&#0160; が推奨されます。次の動画は、Autodesk ReMake のワークフローを紹介するショート ビデオです。途中、メッシュの穴埋めの模様を参照することが出来ます。</p>
<p style="text-align: center;">&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/xpb2745MwzM?feature=oembed" width="500"></iframe>&#0160;</p>
<p><span style="font-size: 15pt;">Q:</span> Autodesk ReCap や Autodesk ReMake の体験版は利用できますか？</p>
<p><span style="font-size: 15pt;">A:</span> Autodesk ReCap Pro は<a href=" http://www.autodesk.com/products/recap-360/free-trial " target="_blank">&#0160;http://www.autodesk.com/products/recap-360/free-trial </a>からダウンロードして、30 日間機能評価で Pro 機能を利用することが出来ます。Autodesk ReMake は <a href="https://memento.autodesk.com/try-remake" target="_blank">https://memento.autodesk.com/try-remake</a>&#0160;から&#0160;15 日間機能評価することが出来ます。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q:</span> Autodesk ReMake の評価期間が終了したので、オンラインストアから ReMake を Subscription 契約したのですが、ReMake を起動してアクティベーションしようとしても、起動直後に次のメッセージが表示されて、Yes をクリックしても、No をクリックしても、RaMake が終了してしまいます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20e4bfa970c-pi" style="display: inline;"><img alt="Update_message" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20e4bfa970c img-responsive" src="/assets/image_450382.jpg" style="width: 490px;" title="Update_message" /></a></p>
<p>体験版をダウンロードして再インストールしましたが、状態が変わりません。どうすればアクティベーション画面を表示出来るのでしょうか？</p>
<p><span style="font-size: 15pt;">A:</span> 古いビルド（17.23.0.38）の Autodesk ReMake がインストールされているようです。<a href="http://up.autodesk.com/2017/MEM/ReMake2017_UPD1_v17.23.1.69.msp">http://up.autodesk.com/2017/MEM/ReMake2017_UPD1_v17.23.1.69.msp</a>&#0160;からパッチを適用することで、新しいビルド（17.23.1.69）に ReMake を更新してみてください。製品が起動して適切にアクティベーションに誘導されるはずです。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q:</span> Reality Capture API を利用する際のコストはどれくらいでしょうか？</p>
<p><span style="font-size: 15pt;">A:</span> Reality Capture API はベータ版であるため、価格は未発表の状態です。正式リリースまでお待ちください。なお、3D Robotics 社は、オートデスク本社側で戦略的な個別契約を結んでいるため、今後発表される価格とはまったくの別の扱いになっています。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q: &#0160;</span>Forge Platform では Web ブラウザ上に 3D データを表示、操作する Viewer（旧 View and Data API）を提供していますが、Viewer は3D メッシュデータや点群データを表示出来ますか？</p>
<p><span style="font-size: 15pt;">A：</span> 現在、Viewer は点群データを表示することが出来ません。鋭意、機能を開発・評価中です。また、生成された OBJ ファイル形式（xxx.obj.zip）でダウンロードした場合、Viewer で 3D メッシュを表示するこをは出来ます。ただし、Model Derivative API による変換処理も含めて、今後、より表現を高める目的で実装が変更される予定です。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;">Q:</span> Model Derivative API を利用して、3D メッシュ データとしてダウンロードしたファイル（OBJ、または、RCM）を点群データ（RCS）に変換することは出来ますか？</p>
<p><span style="font-size: 15pt;">A:</span> 残念ながら、Model Derivative API は、Reality Capture API が演算処理で生成する各種データの変換は出来ません。</p>
<p>By Toshiaki Isezaki</p>
