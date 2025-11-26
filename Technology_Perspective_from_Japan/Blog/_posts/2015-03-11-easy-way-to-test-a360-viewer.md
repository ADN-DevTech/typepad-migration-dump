---
layout: "post"
title: "簡単に A360 ビューワー機能を評価する方法"
date: "2015-03-11 00:18:08"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/03/easy-way-to-test-a360-viewer.html "
typepad_basename: "easy-way-to-test-a360-viewer"
typepad_status: "Publish"
---

<p>簡単に A360 が持つビューワ機能を試すことが出来るようになりました。この機能をお試し頂く場合には、WebGL が利用できる Web ブラウザから、直接&#0160;<strong><a href="https://360.autodesk.com/Viewer" target="_blank">https://360.autodesk.com/Viewer</a>&#0160;</strong>にアクセスしたり、オートデスクのトップページ <a href="http://www.autodesk.co.jp" target="_blank">http://www.autodesk.co.jp</a> 左下にある「すべてのビューア」リンクを利用することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c75b094e970b-pi" style="display: inline;"><img alt="Viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c75b094e970b image-full img-responsive" src="/assets/image_416372.jpg" title="Viewer" /></a></p>
<p>お試しページが表示されたら、表示させたいファイルをドラッグ&amp;ドロップするだけで、A360 にログインしなくても、自由に A360 のビューワー機能を利用して、任意の図面やモデルを表示することが出来ます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/wO0cPP-7iwM?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>オートデスク製品も含め、現在、非常に多くのデザイン データが流通しています。当然、別々の CAD/CG ソフトで出力、保存されているので、対応するビューワ製品を見つけるのも大変です。</p>
<p>もちろん、無償ビューワが流通するすべてのデータ ファイル形式をサポートしているわけではありません。ユーザー側の視点で考えれば、データを見るだけの目的で、ファイル形式毎に異なるビューワ製品をインストールする手間が発生します。ファイル形式も製品バージョンで変わっていくので、そのたびにビューワ製品を入れ替える必要があり、管理も面倒です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c75b0504970b-pi" style="display: inline;"><img alt="Problems" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c75b0504970b image-full img-responsive" src="/assets/image_91703.jpg" title="Problems" /></a></p>
<p>そこで登場したのがクラウドを利用してさまざまなファイル形式に対応するビューワ サービスであり、Autodesk A360 が持つ 2D 図面や 3D モデルの表示機能です。</p>
<p>もちろん、A360 にログインすれば、プロジェクトに毎にプロジェクト参加者の間でだけ、特定の図面や 3D モデルを共有して注釈を加えるなどの運用をしていくことが出来ます。また、ビューワ機能だけを抜き出してカスタマイズしたい場合には、<a href="https://developer.autodesk.com/" target="_blank"><strong>View and Data API</strong></a> を利用することが出来ます。</p>
<p>A360 の登場もあり、Design Review は 2013 バージョンでバージョンアップが止まっていますが、DWG TrueView などの無償ビューワは、今日でもダウンロードして利用することが出来ますので、その点はご注意ください。&#0160;</p>
<p style="text-align: left;">By&#0160;Toshiaki Isezaki</p>
<p style="text-align: left;">&#0160;</p>
