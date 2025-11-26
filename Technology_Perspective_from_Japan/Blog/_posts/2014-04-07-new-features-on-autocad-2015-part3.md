---
layout: "post"
title: "AutoCAD 2015 の新機能 ～ その 3"
date: "2014-04-07 03:25:25"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/04/new-features-on-autocad-2015-part3.html "
typepad_basename: "new-features-on-autocad-2015-part3"
typepad_status: "Publish"
---

<p>AutoCAD 2015 新機能の紹介の最後に、地理的位置と点群についてご紹介しましょう。</p>
<p><strong>地理的位置</strong></p>
<p>AutoCAD 2014 では、図面に地理的位置を設定すると、モデル空間の背景に道路地図や航空写真を表示させることが出来ました。航空写真を表示させた場合、ズームして一定の倍率に達してしまうと、航空写真の表示が出来ない場合がありました。AutoCAD 2015 では、高解像度の航空写真が利用可能な場合にのみ、ズーム倍率のよって自動的に解像度を切り替え、高解像度の航空写真を表示できるようになっています。</p>
<p>また、AutoCAD 2014 では背景として表示された道路地図や航空写真は、図面の印刷時に印刷対象にはなっていませんでしたが、AutoCAD 2015 では、表示されている背景箇所を指定した矩形範囲やビューポート単位でキャプチャすることで、印刷対象とすることが出来るようになりました。</p>
<p>キャプチャした領域は、ラスター画像のように選択して回転させたり、移動させたりすることが出来ると同時に、移動先の背景に合わせてキャプチャされる表示内容が自動更新されます。グリップ編集でサイズを変更した場合には、画像内容が伸長するのではなく、キャプチャ領域が広がって表示内容が更新されます。</p>
<p>下記の図は、背景に航空写真を表示して矩形領域をキャプチャ後、背景を道路地図に変更した状態のものです。矩形領域は水平にしかキャプチャ出来ないので、キャプチャ後に領域を ROTATE コマンドで回転させていますが、キャプチャ内の領域が自動更新されているので、背景の道路地図と航空写真の道路や橋の部分が一致しています。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcd67192970b-pi" style="display: inline;"><img alt="Geolocation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcd67192970b image-full img-responsive" src="/assets/image_332317.jpg" title="Geolocation" /></a></p>
<p style="text-align: left;"><strong>点群サポート</strong></p>
<p>AutoCAD 2015 には、昨年に続いてAutodesk ReCap が同梱されています。 同梱されている ReCap は、レンタル ライセンスを購入することで ReCap Pro として動作させることが出来ます（起動後 30日間はトライアル期間として ReCap Pro の機能を利用できます）。ReCap 自体の機能は、少し前の少し前のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2014/02/autodesk-recap-and-recap-pro.html" target="_blank"><strong>Autodesk ReCap/ReCap Pro</strong></a> でもご案内していますので、そちらもご参照ください。</p>
<p>今回のバージョンでは、レーザースキャナのスキャンデータのインデックス化を AutoCAD では行わず、Autodesk ReCap に読み込んで出力した 点群プロジェクト ファイル（.rcp）と 点群スキャン ファイル（.rcs）のみをアタッチするように変更されています。旧バージョンの AutoCAD でインデックス化して図面にアタッチ済の DWG ファイルは、従来通り、そのまま開いて利用可能です。</p>
<p>Autodesk ReCap&#0160;で読み込んだ点群ファイルには、セグメンテーション データと呼ばれるデータが埋め込まれているため、UCS[ユーザ座標系] コマンドの OB[オブジェクト] オプションを利用することで、点群で構成される面に対して垂直にユーザ座標を設定することが出来ます。残念ながら、ダイナミック UCS は利用できませんが、点群面を利用できるので、点群で取り込んだ 3D モデルに対して、効果的にモデリングしていくことが出来るようになります。なお、<a href="http://adndevblog.typepad.com/technology_perspective/2014/03/recap-360-cloud-service-update.html" target="_blank"><strong>Autodesk ReCap 360</strong></a> を使って写真から生成した点群スキャン ファイル（.rcs）にも、セグメンテーション データが含まれます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdc6442970b-pi" style="display: inline;"><img alt="セグメンテーションデータ" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcdc6442970b image-full img-responsive" src="/assets/image_374750.jpg" title="セグメンテーションデータ" /></a></p>
<p>アタッチされて図面に配置された点群データには、さまざまな表現方法が与えられています。例えば、従来の強度、法線などに加えて、高度、分類、などのオプションを利用できるようになりました。 各表現時の設定内容は、カラーマッピング設定で変更することも出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdbc5c3970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="float: left;"><br /></a> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdbc6be970b-pi" style="display: inline;"><img alt="点群高度" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcdbc6be970b img-responsive" src="/assets/image_158297.jpg" style="width: 240px;" title="点群高度" /></a>&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d969e28970d-pi" style="display: inline;"><img alt="点群分類" class="asset  asset-image at-xid-6a0167607c2431970b01a73d969e28970d img-responsive" src="/assets/image_26168.jpg" style="width: 240px;" title="点群分類" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d969de9970d-pi" style="display: inline;"><br /><br /> </a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdc57dc970b-pi" style="display: inline;"><img alt="Point_Cloud_Representation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcdc57dc970b image-full img-responsive" src="/assets/image_461833.jpg" title="Point_Cloud_Representation" /></a></p>
<p style="text-align: left;">この他にも、3D オブジェクト設定が変更されて、点群データに対するモデリングがさらに便利になっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdc65d1970b-pi" style="display: inline;"><img alt="3D_Snap_Settings" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcdc65d1970b img-responsive" src="/assets/image_992789.jpg" style="width: 380px;" title="3D_Snap_Settings" /></a></p>
<p style="text-align: left;">ここまでの内容をまとめた動画がありますので、ご参照ください。&#0160;</p>
<p style="text-align: center;">&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/1gMCTXuklLE?feature=oembed" width="500"></iframe></p>
<p>点群データの扱いでは、Autodesk ReCap、また、クラウド サービスである Autodesk ReCap 360 と組み合わせて運用することで、現況データの活用がさらに広がります。&#0160;</p>
<p><strong>設計フィード</strong>&#0160;</p>
<p>設計フィードは、Autodesk 360 を介したコラボレーション機能です。AutoCAD 2015 では、閉じたネットワーク環境下やローカル コンピュータ内に保存した図面でも、Autodesk 360 に投稿された設計フィードの投稿データを保持出来るようになりました。図面のアクセス権限を持つ閉じた環境で、設計フィードの運用が出来るようになり、利便性がさらに高まることになります。 名前を付けて保存する際や、eトランスミットで図面を転送する場合には、閉じた環境で運用していた図面から、埋め込まれている設計フィードの情報を除去することも出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d973c96970d-pi" style="display: inline;"><img alt="設計フィード" class="asset  asset-image at-xid-6a0167607c2431970b01a73d973c96970d img-responsive" src="/assets/image_413882.jpg" style="width: 400px;" title="設計フィード" /></a>&#0160;</p>
<p><strong>BIM 360 との連携</strong></p>
<p>AutoCAD 2015 では、BIM 360 Glue とのコラボレーションが出来るように、BIM 360 プラグインがインストールされるようになりました。BIM 360 Glue を使用しているプロジェクト チームと AutoCAD モデルを共有したり、クラッシュの結果を表示するツールに簡単にアクセスできるようになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdc679e970b-pi" style="display: inline;"><img alt="BIM_360" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcdc679e970b img-responsive" src="/assets/image_115294.jpg" title="BIM_360" /></a></p>
<p style="text-align: left;">この他にも、AutoCAD 2015 には細かい修正が多く加えられています。詳細は、AutoCAD 2015 の<a href="http://adndevblog.typepad.com/technology_perspective/2014/02/autocad-preview-guides.html" target="_blank">ピレビューガイド</a>を参照してみてください。</p>
<p>By Toshiaki Isezaki&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
