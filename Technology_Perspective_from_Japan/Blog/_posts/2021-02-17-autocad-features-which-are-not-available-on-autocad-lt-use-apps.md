---
layout: "post"
title: "AutoCAD LT にない AutoCAD 機能：アプリ利用"
date: "2021-02-17 00:05:38"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/02/autocad-features-which-are-not-available-on-autocad-lt-use-apps.html "
typepad_basename: "autocad-features-which-are-not-available-on-autocad-lt-use-apps"
typepad_status: "Publish"
---

<p>AutoCAD LT にない AutoCAD の機能に、アプリの実行環境の有無があります。ここで言うアプリ（App）とは、いわゆるアドイン アプリケーションを指しています。アドインは「アドオン」、あるいは、「プラグイン」とも呼ばれることもあります。&#0160;</p>
<p>ご存じのとおり、AutoCAD は「カスタマイズ」が出来る CAD です。リボンやツールバーといったグラフィカル ユーザ インタフェースのカスタマイズとは別に、独自のコマンド（カスタム コマンド）を作成、利用することが出来ます。</p>
<p>LINE コマンドや LAYERコマンドといった作図や編集で用いる AutoCAD 標準の 組み込みコマンドではなく、業務でよく使用する反復タスクをオリジナルのコマンドにまとめて利用していくことが出来るのです。</p>
<p>カスタム コマンドは、AutoCAD の機能にアクセスする <a href="https://adndevblog.typepad.com/technology_perspective/2021/02/autocad-features-which-are-not-available-on-autocad-lt-api.html" rel="noopener" target="_blank"><strong>API</strong></a>（Application Programming Interface）を使って作成します。設計者がボタンやメニューなどの&#0160;<strong>GUI</strong>（Graphical User Interface）を駆使して製図や 3D モデリングしていくのとは違い、API はプログラマと呼ばれるプログラム開発者が使用します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebd7c3c200c-pi" style="display: inline;"><img alt="2_ways_access" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdebd7c3c200c image-full img-responsive" src="/assets/image_624404.jpg" title="2_ways_access" /></a></p>
<p>説明が長くなってしまうので API の詳細は割愛しますが、AutoCAD の<a href="https://www.autodesk.co.jp/products/autocad/included-toolsets" rel="noopener" target="_blank"><strong>業種別ツールセット</strong></a>も、API で作られた複数の業種別カスタム コマンドをカスタマイズ メニュー とともに構成されたものと考えることが出来ます。</p>
<p>業界別ツールセットとは別に、AutoCAD にはサードパーティが提供するアドイン アプリケーションを導入（インストール）、利用していく環境が整えられています。そのようなサードパーティ製アドイン アプリケーション公開の場が、<strong>Autodesk App Store（アプリ ストア）</strong>です。</p>
<p style="padding-left: 40px;">※オートデスクも App Store から特定のアドインやコンテンツを配布することがあります。</p>
<p>App Store には、タイトルバー右上のボタンから簡単にアクセスすることが可能になっているはずです。API が利用出来ない AutoCAD LT には、このボタンはありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278801528be200d-pi" style="display: inline;"><img alt="Acad_vs_aclt" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801528be200d image-full img-responsive" src="/assets/image_715871.jpg" title="Acad_vs_aclt" /></a></p>
<p>App Store ボタンをクリックすると、Autodesk App Store ページが Web ブラウザ上に表示されます。ページ上には、AutoCAD バージョンを明記したサードパーティ製アドイン アプリケーションが数多く記載されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880152fc3200d-pi" style="display: inline;"><img alt="App_store" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880152fc3200d image-full img-responsive" src="/assets/image_209004.jpg" title="App_store" /></a></p>
<p>個々のバナーをクリックすると詳細ページにジャンプするので、Autodesk ID でサインインすれば、アドインのインストーラをダウロードしてお使いの AutoCAD に導入することで、カスタム コマンドを利用することが出来るようになります。</p>
<p>アドインモジュールはサードパーティの各企業が作成するものですが、インストーラはオートデスクが同じルック＆フィールを持つよう作成しているので、ほぼ同じ操作でインストールしていくことが出来るはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98fff95200b-pi" style="display: inline;"><img alt="Installers" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98fff95200b image-full img-responsive" src="/assets/image_144851.jpg" title="Installers" /></a></p>
<p>App Store には、アドイン アプリケーション以外にも、ブロック ライブラリやラーニング コンテンツなども公開されています。各コンテンツには、無償版の他、体験版、有償版も存在しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9903502200b-pi" style="display: inline;"><img alt="Block_library" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9903502200b image-full img-responsive" src="/assets/image_692589.jpg" title="Block_library" /></a></p>
<p>App Store には AutoCAD 以外のオートデスク製品用のアドインやコンテンツも公開されています。ご興味いただいたアドイン アプリケーションやコンテンツが、お使いの製品やバージョンをサポートしているか、注意してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebd8902200c-pi" style="display: inline;"><img alt="Supported_versions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdebd8902200c image-full img-responsive" src="/assets/image_687500.jpg" title="Supported_versions" /></a></p>
<p>App Store で業務に合ったアドインを見つけて AutoCAD の機能を拡張していくことで、作業効率を高めていくことが出来ます。一度、App Store を覗いてみてください。</p>
<p>By Toshiaki Isezaki</p>
