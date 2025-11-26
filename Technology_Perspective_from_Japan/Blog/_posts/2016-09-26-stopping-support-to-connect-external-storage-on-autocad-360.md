---
layout: "post"
title: "AutoCAD 360 外部ストレージ接続のサポート停止について"
date: "2016-09-26 02:10:05"
author: "Toshiaki Isezaki"
categories:
  - "その他カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/09/stopping-support-to-connect-external-storage-on-autocad-360.html "
typepad_basename: "stopping-support-to-connect-external-storage-on-autocad-360"
typepad_status: "Publish"
---

<p>AutoCAD 360 をお使いの方には、「<strong>Important information about your AutoCAD 360 account</strong>」のタイトルでメールが届いているかもしれません。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d220ed5e970c-pi" style="display: inline;"><img alt="Email_from_autocad_360_support" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d220ed5e970c image-full img-responsive" src="/assets/image_739822.jpg" title="Email_from_autocad_360_support" /></a></p>
<p>突然の通知で少し驚いていますが、要約すると「<strong><a href="https://ja.wikipedia.org/wiki/WebDAV" target="_blank">WebDAV プロトコル</a></strong>&#0160;<strong>を用いて外部ストレージに接続する機能について、あまり利用されていないため、11月1日でサポートを終了する</strong>」というものです。同じ内容の説明は、<strong><a href="http://help.autocad360.com/customer/en_us/portal/articles/2447343-autocad-360-support-of-egnyte-buzzsaw-box-and-custom-webdav-services" target="_blank">AutoCAD 360 Help</a></strong>（英語） や下記の Mobile アプリのヘルプ ドキュメントにも記載されているようです（後者のほうは日付が間違っていますが）。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb093a99ac970d-pi" style="display: inline;"><img alt="Online_help" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb093a99ac970d image-full img-responsive" src="/assets/image_162881.jpg" title="Online_help" /></a></p>
<p>この機能では、Buzzsaw、Egnyte、Box や、社内の WebDAV サーバーに接続する機能を提供してきましたが、WebDAV が低速であることも要因になっているようです。もし、これらと接続して AutoCAD 360 （現在では Mobile アプリ のみ利用可）を運用されている場合には、外部ストレージ内の図面を AutoCAD 360 側に移動することが推奨されています。</p>
<p>もちろん、AutoCAD 360 は、外部ストレージにあるデータを透過的に AutoCAD 360 上に表示していただけなので、外部ストレージ上のデータが消えてしまうようなことはありません。</p>
<p>なお、WebDAV プロトコルを利用しない外部ストレージ サービスへの接続は、現時点でもサポートされています。現時点では、Dropbox、Google Drive、OneDrive が対象になっています。AutoCAD 360 Mobile をお使いの方は、AutoCAD 360 Mobile ログイン後に右上の (＋) アイコンから接続をすることが出来ます。<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d220ef99970c-pi" style="display: inline;"><img alt="Connect_external_storage_on_mobile_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d220ef99970c image-full img-responsive" src="/assets/image_522207.jpg" title="Connect_external_storage_on_mobile_app" /></a></p>
<p>この機能をお使いの方は、ご注意いただきたいと思います。</p>
<p>By Toshiaki Isezaki</p>
