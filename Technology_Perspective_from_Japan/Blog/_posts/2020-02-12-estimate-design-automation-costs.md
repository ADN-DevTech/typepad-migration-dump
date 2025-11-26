---
layout: "post"
title: "Design Automation API の課金とコスト算出について"
date: "2020-02-12 00:05:14"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/02/estimate-design-automation-costs.html "
typepad_basename: "estimate-design-automation-costs"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/10/design-automation-api-v3-release.html" rel="noopener" target="_blank">Design Automation API v3 リリース</a></strong> でご案内のとおり、2019 年 10 月 28 日（日本時間 29 日）に Design Automation API v3 が正式にリリースにリリースされています。<span style="text-decoration: line-through;">クラウドクレジットでの課金は、コアエンジン毎に Revit、Inventor、3ds Max が 1 CPU 時間当たり <strong>6 クラウド クレジット</strong>、AutoCAD コアエンジンのみ、従来通り、 <strong>4 クラウド クレジット</strong>になっています。</span></p>
<p><span style="background-color: #ffff00;">※ 2022年3月29日の<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/upcoming-forge-pricing-changes.html" rel="noopener" target="_blank"><strong>クラウドクレジット価格改定</strong></a>に合わせて、消費クラウドクレジット数はコアエンジンに関係なく、一律 <strong>2 クラウドクレジット</strong>に変更されました。</span></p>
<hr />
<p><strong>計測方法は？</strong></p>
<p style="padding-left: 40px;">それでは、この課金方法となるコアエンジンの「1 CPU 時間当たり」とは、具体的にどのように計測しているのでしょうか？</p>
<p style="padding-left: 40px;">Design Automation API への課金時間は、処理時に実行されることになる WorkItem（ワークアイテム）の成功時の処理時間（秒数）に基づいて算出されています。 WorkItem の処理が完了すると、onComplete コールバック、または、GET Workitem のレスポインスに開始時刻と終了時刻が次のようにレポートとして記されます。</p>
<p style="padding-left: 40px;"><em>&#0160; &#0160;“timeQueued”: “2019-11-13T20:22:26.1972643Z”,<br />&#0160; &#0160;<strong>“timeDownloadStarted”: “2019-11-13T20:22:25.587368Z”,</strong><br />&#0160; &#0160;“timeInstructionsStarted”: “2019-11-13T20:24:01.3217437Z”,<br />&#0160; &#0160;“timeInstructionsEnded”: “2019-11-13T20:24:35.4545907Z”,<br />&#0160; &#0160;<strong>“timeUploadEnded”: “2019-11-13T20:26:03.3608278Z”,</strong></em></p>
<p style="padding-left: 40px;">コストの算出に必要な数値は、<strong>timeDownloadStarted</strong> と <strong>timeUploadEnded</strong> の値です。 この例では、合計時間は 217 秒になり、この時間に相当するクラウド クレジットが、お手持ちのクラウド クレジット残高から減算されることになります。</p>
<p style="padding-left: 40px;">なお、現在のところ、<strong>Forge ポータル（<a href="https://forge.autodesk.com/">https://forge.autodesk.com/</a>）</strong>の <strong>Usage Analytics</strong> で表示されるのは、消費したクラウド クレジットの累計が 1 クラウド クレジット以上になった場合です。０ と表示された場合でも、消費したクラウド クレジットは保持しています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a50ae4a3200b-pi" style="display: inline;"><img alt="Usage_analytics_da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a50ae4a3200b image-full img-responsive" src="/assets/image_611781.jpg" title="Usage_analytics_da" /></a></p>
<hr />
<p><strong>ファイルのダウンロード時間とアップロード時間？</strong></p>
<p style="padding-left: 40px;">レポートには、ファイルのダウンロードやアップロードにかかった正確な時間も表示されるはずです。ここで注意していただきたいのは、AppBundle（アプリバンドル ＝ コアエンジンにロード・実行させるアドイン パッケージ）が、WorkItem が実行時に使用する 素材ファイルのダウンロードと、成果ファイルのアップロードにかかる時間もカウントされる点です。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a50aea37200b-pi" style="display: inline;"><img alt="Da_storage_use" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a50aea37200b image-full img-responsive" src="/assets/image_708496.jpg" title="Da_storage_use" /></a></p>
<p style="padding-left: 40px;">素材ファイルのダウンロードと成果ファイルのアップロードには、任意に用意したクラウド ストレージを利用することになりますが、どのストレージをどのように選択するかによって、課金対象となる WorkItem の処理時間に影響を与える可能性があります。</p>
<p style="padding-left: 40px;">オートデスクがテストした限りでは、主要な AWS S3、Azure Blob Storage、Forge OSS（BIM 360 Docsでも使用）では、大きな違いはありませんでした。世界中のさまざまな地域にホストされているファイルでテストしましたが、結果は同様です。ただし、インターネットトラフィックによる変動が大きくなっています。例えば、400MBファイルのテストでは、ファイルを Design Automation API サーバーにダウンロードするのに約 30 〜 40 秒かかり、ファイルを開いて操作を開始するのに 3 〜 4 秒かかりました。</p>
<p style="padding-left: 40px;">オンプレミスなサーバーとともにファイルのダウンロードやアップロードをおこなう場合や、非常に大きなファイルを扱う場合などは、さらに時間を要することが予想されます。出来れば、より高可用性なストレージの利用をご検討いただくことをお勧めします。</p>
<p style="padding-left: 40px;">もう一点、考慮すべき点があります。ストレージ サービス利用時のファイル転送のコストです。 少なくとも、ファイルが Forge OSS Bucket や BIM 360 Docs、Fusion Team などにホストされている場合には、それらのアクセス（ダウンロード/アップロード）に費用はかかりません。</p>
<hr />
<p>実行に 1 時間必要なアドイン アプリケーションの単一処理というのは、一般的に、あまり多くない印象がありますが、WorkItem 内でクラウドストレージからの素材ファイルのダウンロードと成果物となるファイルの保存（アップロード）に要する時間も、クラウドクレジットによる課金対象となっていますのでご承知おきください。</p>
<p>By Toshiaki Isezaki</p>
