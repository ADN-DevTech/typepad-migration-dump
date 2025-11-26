---
layout: "post"
title: "A360 ビューワーの新機能"
date: "2015-04-29 02:20:40"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html "
typepad_basename: "new-capabilitie-a360-viewer"
typepad_status: "Publish"
---

<p>以前ご紹介した <a href="http://adndevblog.typepad.com/technology_perspective/2015/03/easy-way-to-test-a360-viewer.html" target="_blank"><strong>A360 ビューワー</strong></a>の機能に、新しい機能が追加されました。この ビューワー公開の目的には、A360 に本格搭載する前の試験的な要素も含まれます。なので、まだ <strong>A360 クラウド サービス</strong>（<strong><a href="https://autodesk360.com" target="_blank">https://autodesk360.com</a></strong>） 本体には組み込まれていませんが、近い将来、実装されるものです。もちろん、View and Data Web サービス API でも同じように考えられます。</p>
<p><strong>Live Review</strong></p>
<p style="padding-left: 30px;">リアルタイム コラボレーションの機能です。同等の機能は、以前、AutoCAD 360 上でAdobe Flash Player を使って実現されていましたが、今回の実装は <a href="http://ja.wikipedia.org/wiki/WebSocket" target="_blank"><strong>WebSocket</strong></a> と呼ばれるプロトコルを利用して実現しています。このビューワ自体も、HTML5 と WebGL といった Web テクノロジのみで実装されているので、WebGL 対応の Web ブラウザがあれば、プラグインなどをインストールする必要はありません。また、モバイル デバイスに搭載されている Web ブラウザでも同様に利用することが出来る点も重要です。</p>
<p style="padding-left: 30px;">コラボレーションを動画にまとめましたので、下記の動画をご参照ください。動画中でも説明していますが、3D モデルだけでなく、2D 図面でもリアルタイム コラボレーションをお試しいただけます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/9EG2JnkboaM?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>アニメーション再生</strong></p>
<p style="padding-left: 30px;">さて、もう 1 つの機能はアニメーション再生機能です。アップロードするデザイン ファイルが Autodesk Fusion 360 で作成された .f3d ファイル（Fusion 360 アーカイブ）で、アニメーションが定義されていれば、その内容を A360 ビューワー内で再生することが出来ます。もちろん、追加のプラグイン インストールは必要ありません。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7805ad9970b-pi" style="display: inline;"><img alt="Fusion_animation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7805ad9970b image-full img-responsive" src="/assets/image_400704.jpg" title="Fusion_animation" /></a></p>
<p style="padding-left: 30px;">このモデルを A360 ビューワーで表示させている動画も、下記からご覧いただけます。マテリアル表示がオリジナルと異なっていますが、将来的に改善されるものと思います。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="344" src="http://www.youtube.com/embed/xL2EN6gNNI0?feature=oembed" width="459"></iframe>&#0160;</p>
<p><strong>外部参照ファイル</strong></p>
<p style="padding-left: 30px;">AutoCAD の DWG ファイルや Inventor の IAM ファイルなど、親ファイルが他の子ファイルを参照する形態を持つファイルを扱えるようになりました。このような構造は、外部参照やアセンブリといった名称で呼ばれますが、DWG ファイルと IAM ファイルについて、アセンブリ構造を持つファイルを表示できるようになりました。</p>
<p style="padding-left: 30px;">親ファイルとなるファイルをドラッグ&amp;ドロップで A360 ビューワーにアップロード指定すると、他に従属するファイルがあるかどうかを画面で確認されます。もし、子ファイルが存在している場合には、それらをアップロードすることが出来ます。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d10a6526970c-pi" style="display: inline;"><img alt="External_files" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d10a6526970c image-full img-responsive" src="/assets/image_351444.jpg" title="External_files" /></a></p>
<p><strong>セキュリティ</strong></p>
<p style="padding-left: 30px;">A360 ビューワーは、各種データやチームメンバをプロジェクト ベースで管理、運用していく A360 クラウド サービスのビューワー機能を抜き出したものです。A360 にサインアップしなくても、簡単に 3D モデルや 2D 図面を表示して、ここまでに紹介したさまざまな機能を利用できます。セキュリティに関しても、A360 と同等です。</p>
<p style="padding-left: 30px;">アップロードしたファイルは SSL 通信でクラウド ストレージ上に保存されますが、ここでも暗号化された状態で保存されています。また、アップロードしたファイルを変換して<a href="http://ja.wikipedia.org/wiki/%E3%82%B9%E3%83%88%E3%83%AA%E3%83%BC%E3%83%9F%E3%83%B3%E3%82%B0" target="_blank"><strong>ストリーミング</strong></a>で表示するため、ファイルとしてどこかにダウンロードして保存されるようなことはありません。アップロードされたファイルは、View and Data API で利用するのと同じ <strong>Temporary</strong> バケット ポリシーが適用されるので、アップロード後、<strong>30 日間で自動的に削除されます</strong>。バケット ポリシーについては、以前のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/a360-view-data-service-api-startup-guide.html" target="_blank"><strong>A360 View and Data サービス API 利用の手引き</strong></a> の&#0160;<strong>ステップ 3: バケットの作成</strong> で紹介しています。</p>
<p>&#0160;</p>
<p>A360 を含めたオートデスクのクラウド サービスは、定期的にアップデートされて機能改善や新機能が順次搭載されていきます。もし、お試しいただいたファイルの表示で問題がありましたら、ビューワ画面右下の拡声器アイコンをクリックしてメッセージをお送りください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7806032970b-pi" style="display: inline;"><img alt="Feedback2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7806032970b img-responsive" src="/assets/image_15303.jpg" title="Feedback2" /></a></p>
<p>もちろん、日本語で結構です。先日も特定の日本語文字が IFC ファイル名に含まれていると、変換処理が中断してしまう点がレポートされていて、現在、開発チームが調査、修正をしている最中です。皆様のフィードバックで、A360 をより良いサービスに方向に導いてください。ご協力をお願いします。</p>
<p>By Toshiaki Isezaki</p>
