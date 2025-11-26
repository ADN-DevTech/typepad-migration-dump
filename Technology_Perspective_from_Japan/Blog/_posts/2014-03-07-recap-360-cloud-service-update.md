---
layout: "post"
title: "ReCap 360 クラウドサービス更新"
date: "2014-03-07 00:12:01"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/03/recap-360-cloud-service-update.html "
typepad_basename: "recap-360-cloud-service-update"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/10/recap-photo-cloud-service.html" target="_blank"><strong>以前</strong></a>ご紹介した Autodesk ReCap Photo が、デスクトップ製品である Autodesk ReCap との連携機能を追加して、名称を Autodesk ReCap Photo から&#0160;<strong>Autodesk ReCap 360</strong> に変更しています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc3b86c970b-pi" style="display: inline;"><img alt="Recap-360-badge-1024px" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc3b86c970b img-responsive" src="/assets/image_366285.jpg" title="Recap-360-badge-1024px" /></a></p>
<p>いくつかの機能更新が加わりましたが、360 度周囲の状況を撮影した複数の写真から、3D モデルを生成する Autodesk ReCapド Photo の機能も健在です。まずは、写真から 3D モデルを生成して、オートデスクのデスクトップ製品で再利用するワークフローをおさらいしましょう。次の動画では、写真から 3D モデルを生成して、生成された点群データをダウンロードしてから、必要な部位のみを AutoCAD 2014 に同梱されていて Autodesk ReCap で編集、その後、AutoCAD 2014 にインポートして再利用しています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/5fNN38di6GY?feature=oembed" width="500"></iframe>&#0160;</p>
<p>今回の更新には、いくつかの新機能が含まれますが、最も 大きな更新は、何と言ってもユーザ インタフェースが日本語化された点です。もともと、Autodesk ReCap Photo はシンプルな操作が可能でしたが、Autodesk ReCap 360 になって日本語でガイドされるので、いくぶん分かり易くなっているはずです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc3b94c970b-pi" style="display: inline;"><img alt="Japanese_ReCap_360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc3b94c970b image-full img-responsive" src="/assets/image_293078.jpg" title="Japanese_ReCap_360" /></a></p>
<p>さて、写真から 3D モデル生成時には、Preview、High、Ultra の&#0160;3 つの品質を選択できましたが、今回の更新で、品質は プレビュー（Preview）と超高（Ultra）の 2 種類になっています、また、超高を選択した場合には、一回の3Dモデル生成で <strong>5 <a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=8032" target="_blank">クラウド クレジット</a></strong>&#0160;が消費されるようになりました。プレビュー品質の場合には、クラウド クレジットは消費されませんが、ダウンロードできるファイル形式に FBX ファイルや RCS ファイルを選択することが出来ませんので注意してください。同様に、プレビュー品質では、「スマートテクスチャを使用」オプションが使用できません。 次の画面は、プレビュー品質と超高品質の選択時で、指定できるオプションに差があることを示すプロジェクトの新規作成画面です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc3c95f970b-pi" style="display: inline;"><img alt="No_Consume_Cloud_Credit" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc3c95f970b img-responsive" src="/assets/image_342580.jpg" style="width: 220px;" title="No_Consume_Cloud_Credit" /></a>&#0160; &#0160; &#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d7ed65d970d-pi" style="display: inline;"><img alt="Consume_Cloud_Credit" class="asset  asset-image at-xid-6a0167607c2431970b01a73d7ed65d970d img-responsive" src="/assets/image_559436.jpg" style="width: 220px;" title="Consume_Cloud_Credit" /></a></p>
<p style="text-align: left;">なお、ダウンロードするファイル形式に依存して、生成した 3D モデルをメッシュ データとして再利用するか、点群データとして再利用するかを決定することになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc3ca43970b-pi" style="display: inline;"><img alt="ReCap_360_Workflow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc3ca43970b image-full img-responsive" src="/assets/image_296122.jpg" title="ReCap_360_Workflow" /></a></p>
<p style="text-align: left;"><a href="http://adndevblog.typepad.com/technology_perspective/2014/02/autodesk-recap-and-recap-pro.html" target="_blank">以前のブログ記事</a>でもご紹介しましたが、Autodesk ReCap 360 には、デスクトップ版の Autodesk ReCap からパブリッシュされた Real View を表示して、距離計測や注釈を追加、確認する機能も備わっています。従来の ReCap Photo の機能だけではなく、3D レーザースキャナから得られたデータをもとに、クラウドを使ったコラボレーションが可能になります。</p>
<p style="text-align: center;">&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/hyJe-nAMd2o?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="text-align: left;">写真から生成された3D モデルは、そのまま Autodesk 360 ストーレジに保存されています。このモデルは、iPhone、iPad や Android 端末にインストールした Autodesk 360 Mobile 上で表示して確認をすることが出来ます。ただし、&#0160;Autodesk ReCap/ReCap Pro からパブリッシュされた&#0160;Real View データは、Autodesk 360 Mobile で表示することは出来ません。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcce51b2970b-pi" style="display: inline;"><img alt="Point_Cloud_on_Autodesk_360_Mobile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcce51b2970b image-full img-responsive" src="/assets/image_80143.jpg" title="Point_Cloud_on_Autodesk_360_Mobile" /></a></p>
<p style="text-align: left;">最後に Autodesk ReCap 360 クラウドサービスのイメージ動画をご紹介しておきます。おおまかに Autodesk ReCap 360 を把握していただけるはずです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/UpiSB0MEFCY?feature=oembed" width="500"></iframe>&#0160;</p>
<p>昨年末、米国で開催された Autodesk University では、ReCap 360 に含まれる ReCap Photo の機能を Web サービス API として公開することが正式にアナウンスされています。つまり、写真から 3D モデル（メッシュや点群）の生成を、API を使って自動化することが可能になります。これについては、また別の機会にご紹介します。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
