---
layout: "post"
title: "Autodesk 360 クラウドのサポート ファイル"
date: "2013-09-11 00:27:48"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/09/supported-files-on-autodesk-360.html "
typepad_basename: "supported-files-on-autodesk-360"
typepad_status: "Publish"
---

<p>Autodesk 360 には様々なクラウド サービスが存在しますが、Autodesk 360 自身にも役割があります。それは、データを保存するストレージ サービス機能、クラウド ストレージに保存されたデータを他のユーザと共有、あるいは、コラボレーションする機能、そして、データそのものを表示する表示、あるいは、閲覧させる機能です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff418648970c-pi" style="display: inline;"><img alt="Autodesk360Services" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff418648970c image-full" src="/assets/image_446899.jpg" title="Autodesk360Services" /></a></p>
<p>ストレージに保存された各種データを正しく表示するために、Autodesk 360 では、データをネイティブに扱うのではなく、一旦中間ファイルに変換して表示する仕組みを採用しています。この中間ファイルを今まで DWF ファイルとして紹介してきましたが、実際には、主に 2D 情報には DWF ファイル形式を、3D 情報には TPF（Transitional Packet Format） というファイル形式を用いています（ファイルによって異なります）。このため、Autodesk 360 にデータ ファイルをアップロードすると、しばらく次のような表示でデータを閲覧できない時間が出てしまいます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff428f6b970b-pi" style="display: inline;"><img alt="2013-09-08 18-48-34" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff428f6b970b image-full" src="/assets/image_56576.jpg" title="2013-09-08 18-48-34" /></a></p>
<p>この生成に時間がかかるケースも出てきますが、Autodesk 360 の登場時に比べると、大分短い時間で変換が完了するようになってきたように思います。ただ、クラウドやネットワークの負荷によっては、まだ生成そのものに失敗してしまうケースもあるようです。これらは徐々に改善されてくるはずです。</p>
<p>さて、この変換対象のファイル形式は、Autodesk 360 の登場依頼、着実に増えてきています。当初は、DWF だけだったものに DWG ファイルが加わり、といった具合に、オートデスクのデスクトップ製品の各種ファイル形式に広まりつつあります、また、一部、モバイル デバイスのみになりますが、Office 製品のファイルなども加わっています。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff416371970b-pi" style="display: inline;"><img alt="Autodesk360file" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff416371970b image-full" src="/assets/image_168845.jpg" title="Autodesk360file" /></a></p>
<p>ここにきて、 サポートするファイル形式にも業界で一般的に利用されるものが加わってきています。特に、製造業で利用される IGES ファイルや STEP ファイル、オートデスクのデスクトップ CAD 製品で利用されている ACIS 由来の Autodesk Shape Manager から出力可能な SAT ファイルなども表示させることが出来るようになりました。</p>
<p>そして、もう1つ、新しいクラウド サービスである<strong> Autodesk Fusion 360</strong> のファイル形式、F3D ファイル形式もサポートされています。Fusion というと、AutoCAD 2011～2012 に同梱されていた Inventor Fusion を思い浮かべる方もいらっしゃると思いますが、Inventor Fusion がネイティブに扱っていたファイル形式は DWG ファイルでした。昨年になって、<strong><a href="https://itunes.apple.com/jp/app/autodesk-inventor-fusion/id529580720?mt=12" target="_blank">Inventor Fusion for Mac</a></strong> が登場して、初めて F3D ファイルを扱えるようになった経緯があります。</p>
<p>Inventor Fusion は徐々にクラウド ベースの Autodesk Fusion 360 へ移行していくもの思います（2013年9月8日現在<strong><a href="http://labs.autodesk.com/technologies/fusion/" target="_blank">まだダウンロードできます</a></strong>ので、あしからず）。</p>
<p>さて、前置きが長くなりましたが、この Invenor Fusion 360 は製造業の方のための次世代 3D CAD サービスです。現段階では日本での正式なリリースはありませんが、90日間のお試し期間が用意されているので、どなたでもお試しいただくことができます。ユーザ インタフェースも英語のままの提供ですが、興味をお持ちの方は&#0160;<strong><a href="http://autodesk.com/tryfusion360" target="_blank">http://autodesk.com/tryfusion360</a></strong> にアクセスしてみてください。クライアント ツールのダウンロード後に、Autodesk ID を使ってサインインすればお試しいただけます。</p>
<p>AutoCAD Fusion 360 がデータ保存に Autodesk 360 を用いているのは言うまでもありませんが、CAD としての機能ももちろん充実しています。個別の機能紹介は別の機会に譲りますが、ここでは、データ ファイルの着目してみたいと思います。前述のとおり、Autodesk Fusion 360 のネイティブなファイル形式は F3D ファイルです。データをクライアントにダウンロードしたいときには、F3D ファイルとしてダウンロードすることができます。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff4163d0970b-pi" style="display: inline;"><img alt="Fusion360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff4163d0970b image-full" src="/assets/image_796315.jpg" title="Fusion360" /></a></p>
<p>この他にも、Autodesk Fusion 360 で作成、編集したデータは IGES や STEP ファイルとしてダウンロードすることもできます。ダウンロードしたファイルは、そのまま Autodesk 360 で表示させることが出来るので、Web ブラウザを使って Autodesk 360 側で閲覧するながりでなく、<strong>Autodesk 360 Mobile（<a href="https://itunes.apple.com/jp/app/autodesk-360-mobile/id459112753?mt=8" target="_blank">iOS 用</a>、<a href="https://play.google.com/store/apps/details?id=com.autodesk.ADRViewer&amp;hl=ja" target="_blank">Android 用</a>）</strong>でも、F3D、IGES、STEP のいずれのファイルも閲覧してマークアップを加えるなどの処理を実行できます。もちろん、ファイルを別のユーザ間で共有すれば、3D データであっても、設計フィードを交えたリアルタイム コラボレーションが可能です。<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff42ccdf970c-pi" style="display: inline;"><img alt="2013-09-08 18-20-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff42ccdf970c image-full" src="/assets/image_308827.jpg" title="2013-09-08 18-20-03" /></a></p>
<p>個別のファイル形式を意識しだすと、なかなか話を前にすすめることが難しくなってしまいがちです。データそのものの編集には、ネイティブなファイル形式を操作できるサービスが必須となりますが、Autodesk 360 では、ファイル形式を意識せずに表示/閲覧する仕組みが整いつつあります。</p>
<p>設計データの新しい運用がすでに可能です。ぜひ、お試しください。</p>
<p><span style="background-color: #ffff00;">この記事を投稿した時点からの変更があります。Autodesk 360 は A360 に名称を変更されて、クラウドでサポートされるファイル形式も増加しています。詳細は、<a href="http://adndevblog.typepad.com/technology_perspective/2014/09/autodesk-360-and-supported-files.html" target="_blank"><span style="background-color: #ffff00;"><strong>こちら</strong></span></a>&#0160;をご確認ください。</span></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
<p>&#0160;</p>
