---
layout: "post"
title: "A360 Drive と Autodesk Drive"
date: "2019-12-02 00:13:41"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/a360-drive-autodesk-drive.html "
typepad_basename: "a360-drive-autodesk-drive"
typepad_status: "Publish"
---

<p>以前、このブログでオートデスクのクラウド ストレージについて、<a href="https://adndevblog.typepad.com/technology_perspective/2018/10/transition-on-autodesk-cloud-storage-service.html" rel="noopener" target="_blank"><strong>オートデスク クラウド ストレージ サービスの遷移</strong></a> と題して経緯をご紹介したことがありました。この際、ブログ記事では現在の有償サブスクリプション アカウント（<strong><a href="https://accounts.autodesk.com/" rel="noopener" target="_blank">https://accounts.autodesk.com/</a></strong>）の「製品とサービス」で表示される <strong>Autodesk Drive</strong> の言及はしていませんでした。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ccc978200d-pi" style="display: inline;"><img alt="Storage_options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ccc978200d image-full img-responsive" src="/assets/image_764520.jpg" title="Storage_options" /></a></p>
<p>Autodesk Drive は、A360 や A360 Drive と同じように、デザイン データや関連図書（ファイル）の保存、表示、共有に利用することが出来るストレージ サービスです。A360 のように、Autodesk ID をお持ちであれば利用可能ということではなく、前述のとおり、デスクトップ製品のサブスクリプション契約をお持ちの方のアカウントに<span style="text-decoration: underline;">表示される</span>サービスです。（2019 年 11 月時点ではブスクリプション契約と関係なくアクセスは出来てしまいます。）</p>
<p>A360 はプロジェクト ベースのファイル管理を、A360 Drive は Autodesk Cloud Documents の流れを汲むフォルダ ベースのファイル管理を、それぞれ提供します。</p>
<p>それでは、Autodesk Drive の位置づけはどうでしょう？そもそも A360 や A360 Drive との違いは何なのでしょう？</p>
<p>実は、Autodesk Drive は、A360 Drive と同じようにフォルダ ベースのファイル管理機能を提供します。A360 Drive との大きな違いは、Autodesk Drive が新しいアーキテクチャのもとに作成されているという点です。新しいアーキテクチャとは、オートデスクがクラウド開発の中核基盤として推進・利用している Forge を意味します。（Autodesk Drive の API アクセス提供の有無は現在未定です。）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a3f7c1200c-pi" style="display: inline;"><img alt="Autodesk_storagr" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a3f7c1200c image-full img-responsive" src="/assets/image_419703.jpg" title="Autodesk_storagr" /></a></p>
<p>Autodesk Drive は、<strong><a href="https://drive.autodesk.com/" rel="noopener" target="_blank">https://drive.autodesk.com/</a></strong> からアクセスすることが出来ます。Autodesk ID で Autodesk Drive にサインインすると、アカウントメニューから「<strong>Desktop Connector for Drive をインストール</strong>」を選択することが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cccd43200d-pi" style="display: inline;"><img alt="Desktop_connetor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cccd43200d image-full img-responsive" src="/assets/image_48298.jpg" title="Desktop_connetor" /></a></p>
<p>Desktop Connector for Drive をインストールすると、ローカル コンピュータのドライブに Autodesk Drive の仮想領域が作成されて、インターネット接続が利用可能な場合に、クラウド ストレージとローカル ドライブを同期サービスを開始します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f1783a200b-pi" style="display: inline;"><img alt="Drive_sync" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f1783a200b image-full img-responsive" src="/assets/image_147031.jpg" title="Drive_sync" /></a></p>
<p>Windows Explorer にも、Autodesk Drive がマッピングされて表示されるので、デスクトップ製品内から Autodesk Drive の内容に透過的なアクセスが可能になり、とても便利です。もし、サインインした Autodesk ID に BIM 360&#0160; や Fusion 360/Team のような他の製品サービスが紐づいていれば、それらもローカル ドライブにマッピングされてきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cccebc200d-pi" style="display: inline;"><img alt="Drive_mapping" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cccebc200d image-full img-responsive" src="/assets/image_558114.jpg" title="Drive_mapping" /></a></p>
<p>お気づきの方もいらっしゃるはずですが、以前、A360 Drive とローカル ドライブを同期する A360 Desktop という機能が提供されていました。A360 Desktopの同期先は A360 Drive に限定されていて、Forge ベースではなかったため、発展性の検討過程で 2018 年 6 月に<strong><a href="https://knowledge.autodesk.com/ja/search-result/caas/downloads/downloads/JPN/content/autodesk-360-desktop-download-and-release-notes.html" rel="noopener" target="_blank">廃止</a></strong>されています。つまり、Desktop Connector は、A360 Desktop の代替として登場した新しいストレージ同期サービス、ということになります。Forge ベースで開発されているので、柔軟な発展性を望むことが出来るかと思います。</p>
<p>なお、Autodesk Drive の登場にともない、以前のブログ記事 <strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/01/about-obscure-things-on-a360-storage-service.html" rel="noopener" target="_blank">A360 ストレージ サービスのわかりにくいところ</a></strong> で触れた A360 と A360 Drive との 相互切り替え機能は、すでに削除されていますのでご注意ください。</p>
<p>プロジェクト ベースの A360（<strong><a href="https://a360.autodesk.com/" rel="noopener" target="_blank">https://a360.autodesk.com/</a></strong>）の今後については未定ですが、新規の Autodesk ID を取得して A360 にアクセスすると、自動的に Fusion Team にマッピングされて、30 日間のトライアル期間が適用されるようになっています。</p>
<p>By Toshiaki Isezaki</p>
