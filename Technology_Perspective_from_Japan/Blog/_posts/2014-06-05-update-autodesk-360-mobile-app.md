---
layout: "post"
title: "Autodesk 360 Mobile アプリの更新"
date: "2014-06-05 01:22:50"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/06/update-autodesk-360-mobile-app.html "
typepad_basename: "update-autodesk-360-mobile-app"
typepad_status: "Publish"
---

<p>先週お知らせした <strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/05/update-autocad-360-mobile-app.html" target="_blank">AutoCAD 360 Mobile</a></strong> や <a href="http://adndevblog.typepad.com/technology_perspective/2014/05/autodesk-360-technical-preview.html" target="_blank"><strong>Autodesk 360 Tech Preview</strong></a> のリリースを受けて、新しく Autodesk 360 Mobile が公開されました。位置づけとしては、従来のアプリの新バージョンではなく、新しいアプリになっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd17de4f970b-pi" style="display: inline;"><img alt="Autodesk_360_Mobile_Update" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd17de4f970b img-responsive" src="/assets/image_518811.jpg" title="Autodesk_360_Mobile_Update" /></a></p>
<p>新しい Autodesk 360 Mobile も、従来のフォルダ ベースからプロジェクト ベースへ管理方法が大きく変更されているため、Autodesk 360（<a href="http://360.autodesk.com" target="_blank">http://360.autodesk.com</a>）と Autodesk 360 Tech Preview（<a href="http://www.autodesk360.com" target="_blank">http://www.autodesk360.com</a>） が異なる URL で個別にアクセス出来るように、アプリも旧 Autodesk 360 Mobile と共存インストールすることが出来るようになっています。このため、旧 Autodesk 360 Mobile アプリをインストールしていても、App Store アプリの更新通知は表示されませんので注意が必要です。自身で検索してインストールしてみてください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511c760ce970c-pi" style="display: inline;"><img alt="New_Old_Apps" class="asset  asset-image at-xid-6a0167607c2431970b01a511c760ce970c img-responsive" src="/assets/image_725957.jpg" title="New_Old_Apps" /></a></p>
<p>残念ながら、今回、リリースされたのは iOS 版のみです。また、あまり違和感を感じることないように感じていますが、ユーザ インタフェースは英語のままです。Android 版やユーザ インタフェースの日本語化は、少し遅れてリリースされるはずです。</p>
<p>なお、従来の Autodesk 360 でアップロードしたデータへは、My Drive リンクをクリックしてアクセスすることも出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73dd2a91e970d-pi" style="display: inline;"><img alt="My Drive" class="asset  asset-image at-xid-6a0167607c2431970b01a73dd2a91e970d img-responsive" src="/assets/image_859894.jpg" style="width: 450px;" title="My Drive" /></a></p>
<p>表示できるファイルは、Autodesk 360 Tech Preview とほぼ同じです。アップロードした各種設計データは、表示するための中間形式に Translation Service を介して変換されます。このため、モバイル デバイスの Autodesk 360 Mobile 上で、多様なファイルを表示することが出来るようになっています。Power Point スライド（.pptx）や、Inventor アセンブリ、ReCap Photo で生成したプロジェクトなども、次のように表示することが出来ます。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73dd2a23d970d-pi" style="display: inline;"><img alt="Autodesk_360_Mobile_Viewer1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73dd2a23d970d image-full img-responsive" src="/assets/image_184118.jpg" title="Autodesk_360_Mobile_Viewer1" /></a></p>
<p>新しい Autodesk 360 Mobile アプリでは、表示できるコンテンツの品質が向上しています。ただし、調整できる設定やオブジェクト選択などの機能が未実装なので、Web 版 Autodesk 360 Tech Preview のビューワー機能に比べれば、見劣りしてしまう箇所もありますが、それでも、ガラスなどの半透明なマテリアルなど、表現できる範囲が大分広がっています。下記の画面キャプチャでは、ワイヤー入りのガラス板の状態も把握することが出来ます（画像をクリックして拡大できます）。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd17e8fd970b-pi" style="display: inline;"><img alt="Autodesk_360_Mobile_Viewer2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd17e8fd970b image-full img-responsive" src="/assets/image_964826.jpg" title="Autodesk_360_Mobile_Viewer2" /></a></p>
<p>一方、Autodesk 360 Tech Preview にはない機能もあります。Autodesk 360 Tech Preview のビューワー機能は、2D 図面や 3D モデルを表示する際には、表示に必要となる差分データをメモリ上に転送する<strong>&#0160;<a href="http://ja.wikipedia.org/wiki/%E3%82%B9%E3%83%88%E3%83%AA%E3%83%BC%E3%83%9F%E3%83%B3%E3%82%B0" target="_blank">ストリーミング</a></strong>&#0160;を利用します。これが故に、大規模データも比較的短い時間で表示することが出来るわけです。ただ、このストリーミングにはインターネット接続が必須なので、ネット環境がない環境での運用では支障が出てしまいます。</p>
<p>Autodesk 360 Mobile アプリには、<span style="text-decoration: underline;">一度表示したデータを</span>キャッシュする機能があるので、インターネット接続のない環境でも、2D 図面や 3D モデルを表示することが出来ます。このバージョンでは、キャッシュするデータの領域サイズや、キャッシュしたデータの消去などの設定がまだありません。AutoCAD 360 Mobile アプリのように、それらが指定可能になることを期待したいところです。</p>
<p>Autodesk 360 Mobile には、Survey（サーベイ）ボタンが用意されています。簡単な選択式の要望アンケートのようなものなので、欲しい機能を選択/入力して Survey 画面最下部の [Send Response] をタップしていただくことをお勧めします。入力項目は日本語でも結構です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd17ee3b970b-pi" style="display: inline;"><img alt="Survey" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd17ee3b970b image-full img-responsive" src="/assets/image_426481.jpg" title="Survey" /></a>&#0160;</p>
<p>&#0160;By Toshiaki Isezaki</p>
<p>&#0160;</p>
