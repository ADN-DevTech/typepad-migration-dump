---
layout: "post"
title: "Inventor アドインの自動ロードについて"
date: "2020-01-27 01:07:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/01/about-automatic-inventor-add-in-load.html "
typepad_basename: "about-automatic-inventor-add-in-load"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a505087a200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a505087a200b-pi" style="display: inline;"></a>既にご承知のとおり、Inventor、AutoCAD、Revit などのオートデスクの主要製品では、セキュリティ向上を目的に、2016 バージョン以降、アドイン アプリケーションにデジタル署名を施すことを推奨しています。AutoCAD については、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2015/08/security-on-autocad-2016-and-digital-signature-to-addin.html" rel="noopener" target="_blank">AutoCAD 2016 のセキュリティとアドインのデジタル署名</a></strong> のブログ記事で概要を、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2015/08/preventing-warning-dialog-when-loading-addin.html" rel="noopener" target="_blank">アドイン ロード時の警告ダイアログ抑止</a></strong> のブログ記事で、未署名のアドイン アプリケーションに対するセキュリティ警告ダイアログの抑止方法について、それぞれお伝えしています。</p>
<p>今回は、Inventor アドインについて、同様の内容をご案内しておきたいと思います。</p>
<hr />
<p><strong>Inventor のアドイン ロード メカニズム</strong></p>
<p>Inventor のアドイン ロードに関しては、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/japanese-inventor-2020-programming-api-help.html" rel="noopener" target="_blank">日本語版 Inventor 2020 API プログラミング用ヘルプ</a></strong> からダウンロード可能な「Inventor 2020 API プログラミング用ヘルプ」の <strong>Inventor API のユーザ マニュアル</strong> &gt;&gt; <strong>API の概要</strong> &gt;&gt; <strong>Inventor のアドインを作成する</strong> に記載されています。</p>
<p>ここで重要なのは、次の点です。</p>
<h3 style="padding-left: 40px;">アドインを Inventor に認識させる</h3>
<p class="normal" style="padding-left: 40px;">Inventor は、起動されるたびに、インストールされているアドインの検索を開始し、そのアドインをロードします。バージョン 2012 より前の Inventor では、アドインを検索するのにレジストリが使用されていました。このレジストリベースの検索は、レガシー アドインで引き続きサポートされていますが、Inventor に既知のアドインを作成するという新しいレジストリ フリーの方法がサポートされるようになり、使用する方法として推奨されています。アドイン テンプレートを使用してアドインを作成すると、レジストリ フリーのアドインが作成されます。</p>
<p class="normal" style="padding-left: 40px;">Inventor がアドインを検出できるようにするには、いくつかの異なるフォルダのいずれかに、特殊なファイルを配置する必要があります。このファイルは、アドインについて説明したもので、アドイン dll がコンピュータのどこにあるかを示します。Inventor は、起動されると、これらのフォルダをスキャンし、これらのファイルを読み込んで、ロードするアドインを判断します。</p>
<p class="normal" style="padding-left: 40px; text-align: center;">&lt;中略&gt;</p>
<h3 style="padding-left: 40px;">ファイルの配置場所</h3>
<p class="normal" style="padding-left: 40px;">ここまでで、アドイン dll が用意され、.addin ファイルが作成されました。Inventor がアドインを検索して、ロードできるように、これらのファイルの配置場所を理解する必要があります。</p>
<p class="Normal" style="padding-left: 40px;">次の 4 つの場所がサポートされています。アドインのニーズに応じて、次の 4 つのいずれかを選択することができます。.addin ファイルは、次の 4 つの場所またはそのサブフォルダのいずれかに配置できます。各パスの &quot;%XXXX%&quot; 部分は、オペレーティング システムで定義された変数です。エクスプローラーを使用する場合は、パスの一部として入力できます。エクスプローラーでこれが評価され、変数によって定義される実際のパスが使用されます。</p>
<ul>
<li><strong>すべてのユーザ、バージョン非依存<br /></strong>Windows 7/8.1/10: %ALLUSERSPROFILE%\Autodesk\Inventor Addins\<br /><br /></li>
<li><strong>すべてのユーザ、バージョン依存<br /></strong>Windows 7/8.1/10: %ALLUSERSPROFILE%\Autodesk\Inventor 20xx\Addins\</li>
<li><strong>ユーザごと、バージョン依存<br /></strong>Window 7/8.1/10: %APPDATA%\Autodesk\Inventor 20xx\Addins\<br /><br /></li>
<li><strong>ユーザごと、バージョン非依存<br /></strong>Window 7/8.1/10: %APPDATA%\Autodesk\ApplicationPlugins\<br /><br /></li>
</ul>
<p style="padding-left: 40px;">アドインを配置する場所を決定する際に考慮すべき事項がいくつかあります。すべてのユーザがアクセスできる場所を選択する場合は、アドインをインストールするのに管理者権限が必要になります。ほとんどの場合、複数のユーザでコンピュータを共有することはないので、通常はユーザごとのインストレーションで十分です。</p>
<p style="padding-left: 40px;">Inventor の各バージョンについてアドインを更新しようとしている場合は、ユーザが使用する Inventor のバージョン用に記述され、このバージョンでテストされたアドインにのみアクセスできるように、アドインをバージョン依存にするとよいでしょう。API の上位互換性を実現するためにかなりの労力が注がれているため、新しいバージョンの Inventor でも古いバージョンのアドインを実行できるはずです。このため、新しいバージョンの Inventor がリリースされてもアドインが引き続き動作すると想定して、特定のバージョンに結びつけずにアドインを供給できます。</p>
<p style="padding-left: 40px;">また、.addin ファイルでアドインが互換性を持つバージョンを指定できるため、バージョンに依存する .addin の場所を使用でき、.addin ファイルを使用してバージョンをコントロールできます。</p>
<p>つまり、Inventor は、起動時に特定のパスにある .addin ファイルを検索して、<strong>LoadBehavior</strong> の内容に応じて、記載されているアドイン アプリケーションをロードしようとする、という点です。</p>
<hr />
<p><strong>Inventor の振る舞い</strong></p>
<p>Inventor がアドイン アプリケーションをロードする際、もし、Inventor アドイン アプリケーション ファイル（.dll）に<span style="text-decoration: underline; background-color: #ffff00;">デジタル署名が施されていないと</span>、一旦、アドインのロードをブロックして次のセキュリティ警告ダイアログを表示します。</p>
<p style="text-align: left;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a5050da1200b-pi" style="display: inline;"><img alt="Addin_load_warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a5050da1200b image-full img-responsive" src="/assets/image_498779.jpg" title="Addin_load_warning" /></a></p>
<p>この警告ダイアログ上で、[アドイン マネージャを起動] ボタンをクリックすると、[アドイン マネージャ] ダイアログを表示して、ブロックされたアドイン アプリケーション ファイルのブロックを解除、アドインをロードすることが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4e06895200d-pi" style="display: inline;"><img alt="Addin_manager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4e06895200d img-responsive" src="/assets/image_806591.jpg" title="Addin_manager" /></a></p>
<p style="text-align: left;">一度ブロックを解除してしまと、その記録は暗号化された <strong>AddInLoadRules</strong> ファイルに記入されて、以後、Inventor は前述のセキュリティ警告ダイアログを表示しなくなります。AddInLoadRules ファイルはバイナリ ファイルで、仕様は公開していないので、ファイルを開いて内容を書き換えるようなことは出来ません（サポートされません）。</p>
<p style="text-align: left;">なお、Inventor のインストール直後には AddInLoadRules ファイルは存在しません。この AddInLoadRules&#0160; ファイルは、[アドイン マネージャ] ダイアログでブロックされたアドインのブロック解除をすると、C:\Users\&lt;username&gt;\AppData\Roaming\Autodesk\Inventor 2020\Addins\ フォルダ直下に生成されます。このため、一度、アドイン アプリケーションのロードをブロック解除した環境でも、AddInLoadRules ファイルを削除してしまうと、次回の Inventor 起動から、再び、セキュリティ警告ダイアログが表示されるようになります。</p>
<hr />
<p style="text-align: left;"><strong>セキュリティ警告ダイアログの抑止</strong></p>
<p>手動操作ですが、前述のとおり、[アドイン マネージャ] ダイアログを開いてブロックされたアドイン アプリケーション ファイルのブロックを解除してしまえば、セキュリティ警告ダイアログの表示を抑止することが出来ます。</p>
<p>もし、セキュリティ警告ダイアログの表示を一切、除去してしまいたいなら、次の 2 つの方法を考えることが出来ます。</p>
<p style="padding-left: 40px;"><strong>アドイン ファイルにデジタル署名を施す：</strong></p>
<p style="padding-left: 40px;">セキュリティ上、推奨される方法です。<a href="https://ja.wikipedia.org/wiki/X.509" rel="noopener" target="_blank">X.509 規格</a>に準拠しているデジタル証明書を購入の上、Microsoft 社が提供する <a href="https://msdn.microsoft.com/ja-jp/library/8s9b9yaz%28v=VS.110%29.aspx" rel="noopener" target="_blank"><strong>SignTool.exe</strong></a> を利用して、アドイン ファイルに署名してください。</p>
<p style="padding-left: 40px;"><strong>AddInLoadRules.xml を設定する：</strong></p>
<p style="padding-left: 40px;">セキュリティ上、非推奨の方法ですが、アドインの社内利用などでデジタル証明書の購入が難しい場合などは有効化と思います。</p>
<p style="padding-left: 40px;">Inventor のインストール フォルダ直下の Preferences フォルダ（%INSTALLDIR%\Preferences、C:\Program Files\Autodesk\Inventor 20xx\Preferences）直下の <strong class="ph b" id="GUID-84B221D3-979B-420D-B955-9DCBDC0C5619__GUID-F9A268AE-B754-42CA-A686-C0C6C4F93665">AddInLoadRules.xml</strong> ファイルを開いて、次のように、&lt;TrustedPath &gt; タグでアドイン アプリケーション ファイルを配置しているパスを追加すると、Inventor が未署名のアドイン ファイルを見つけてもロードするようになります。</p>
<p style="padding-left: 40px;"><strong>例：</strong>&lt;TrustedPath Policy=&quot;Allow&quot;&gt;C:\Users\&lt;username&gt;\AppData\Roaming\Autodesk\ApplicationPlugins\&lt;/TrustedPath&gt;</p>
<p style="padding-left: 40px;">AddInLoadRules.xml ファイルは ASCII 形式なので、メモ帳などのテキスト エディタで開いて内容を編集することが出来ます。詳細は、Inventor のオンラインヘルプ「<strong>アドインの動作をコントロールする</strong>（<a href="http://help.autodesk.com/view/INVNTOR/2020/JPN/?guid=GUID-84B221D3-979B-420D-B955-9DCBDC0C5619" rel="noopener" target="_blank">http://help.autodesk.com/view/INVNTOR/2020/JPN/?guid=GUID-84B221D3-979B-420D-B955-9DCBDC0C5619</a>）」を確認してみてください。</p>
<p style="padding-left: 40px;">なお、ファイルが保存されているパスへの書き込みには、管理者権限が必要なのでご注意くさい。もし、AddInLoadRules.xml ファイルの上書き保存時に次のようなエラーが表示されたら、管理者権限がないことを意味しています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4b74060200c-pi" style="display: inline;"><img alt="Permission_error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4b74060200c img-responsive" src="/assets/image_238399.jpg" title="Permission_error" /></a></p>
<p style="padding-left: 40px;">このような場面では、テキストエディタの起動時に [管理者として実行] で起動すると、内容を編集、上書き保存することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4e06e2d200d-pi" style="display: inline;"><img alt="Admin_permission" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4e06e2d200d image-full img-responsive" src="/assets/image_786680.jpg" title="Admin_permission" /></a></p>
<p>By Toshiaki Isezaki</p>
