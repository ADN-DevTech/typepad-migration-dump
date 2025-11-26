---
layout: "post"
title: "アドイン ロード時の警告ダイアログ抑止"
date: "2015-08-17 00:46:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/08/preventing-warning-dialog-when-loading-addin.html "
typepad_basename: "preventing-warning-dialog-when-loading-addin"
typepad_status: "Publish"
---

<p style="text-align: left;"><a href="http://adndevblog.typepad.com/technology_perspective/2015/08/security-on-autocad-2016-and-digital-signature-to-addin.html" target="_blank"><strong>AutoCAD 2016 のセキュリティとアドインのデジタル署名</strong></a> でご紹介したように、AutoCAD 2016 が採用するセキュア ロード システムでは、すべてのアドイン ファイルのロード時に、一定の条件を満たさないと、警告ダイアログを表示してユーザに不正なカスタマイズ機能の実行に注意を促すようになっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1450ea3970c-pi" style="display: inline;"><img alt="Secure_load" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1450ea3970c img-responsive" src="/assets/image_170192.jpg" title="Secure_load" /></a></p>
<p>逆に、ここに記載されている条件に合致しなければ、警告ダイアログは表示されることはありません。すなわち、次の条件が該当します。&#0160;</p>
<ol>
<li>システム変数 SECURELOAD が <span style="font-family: arial, helvetica, sans-serif;">0</span> &#0160;に設定されている。</li>
<li>アドイン ファイルに認証局によって発行された証明書がデジタル署名されている。</li>
<li>アドイン ファイルのある場所が、信頼できる場所（システム変数 TRUSTEDPATHS）に含まれている。</li>
<li>アドイン ファイルのある場所が、既定で AutoCAD のインストール フォルダとサブフォルダ、及び、自動ローダのアドイン格納フォルダとサブフォルダも含まれまている。</li>
<li>デジタル署名されていないファイルをロードする際に表示される [ファイルのロード - セキュリティの問題] ダイアログで、「<strong>常にこのアプリケーションをロード</strong>」チェックボックスにチェックを入れた。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1497451970c-pi" style="display: inline;"><img alt="Warning1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1497451970c image-full img-responsive" src="/assets/image_450504.jpg" title="Warning1" /><br /></a>※ 「<strong>常にこのアプリケーションをロード</strong>」チェックボックスの情報は暗号化されて保存されるため、第三者が API などで値をコントロールすることは出来ません。</li>
</ol>
<p>この仕組みは、不正なアプリケーションのロードを抑止するセキュリティ維持を目的としていますので、当然、最初の「システム変数 SECURELOAD が <span style="font-family: arial, helvetica, sans-serif;">0</span> に設定」はお勧めしません。可能であれば、認証局より証明書を入手して、アドイン モジュールに署名することをご検討ください。</p>
<p>なお、デジタル署名を正しく識別する機能は、AutoCAD 2016 以降のバージョンに実装されています。AutoCAD 2016 は、前バージョンである AutoCAD 2015 とバイナリ互換を持ちますが、保証されるのは上位互換のみです。AutoCAD 2016 用に作成した ObjectARX ファイルや .NET アセンブリ ファイルは、場合によって、デジタル署名を施した後でも AutoCAD 2015 にロードすることが出来ますが、このような下位互換を意図した運用はサポートされていません。AutoLISP ファイルにデジタル署名をした場合には、AutoCAD 2015 でロード出来ない場合もありますので、ご注意ください。AutoCAD 2015 と AutoCAD 2016 のバイナリ互換については、ブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2015/04/autocad-2016-interoperability-for-customization.html" target="_blank"><strong>AutoCAD 2016 のカスタマイズ互換性</strong></a> をご確認ください。</p>
<p>オートデスクでは、安全にアドイン アプリケーションをお使いいただくために、<strong><a href="https://apps.exchange.autodesk.com/ja" target="_blank">Autodesk Exchange Apps ストア</a>&#0160;</strong>で公開されるアプリにも、デジタル署名を標準化しようとしています。Autodesk Exchange Apps ストアからインストールしたアドイン アプリケーションは、前述の警告ダイアログは表示されない条件 4. に合致しますが、デジタル署名を施すことによって、更にセキュアな構築するのが目的です。</p>
<p>もしろん、これから&#0160;Autodesk Exchange Apps ストアでアプリを公開する方には、デジタル署名を強く推奨しています。アプリ公開の方法については、ブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2015/07/publish-to-autodesk-exchange-apps-store.html" target="_blank"><strong>Autodesk Exchange Apps ストアへの公開</strong></a> で説明しています。</p>
<p>By Toshiaki Isezaki&#0160;</p>
