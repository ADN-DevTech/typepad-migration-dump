---
layout: "post"
title: "AutoCAD 雑学：アドイン セキュリティ"
date: "2021-09-27 00:15:38"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/09/autocad-trivia-addin-security.html "
typepad_basename: "autocad-trivia-addin-security"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e123bfcc200b-pi" style="display: inline;"><img alt="Title2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e123bfcc200b image-full img-responsive" src="/assets/image_252096.jpg" title="Title2" /></a></p>
<p>AutoCAD API で作成したアドイン アプリ（別名、アドオン、あるいは、プラグイン）を使用するには、それらを AutoCAD にロードする必要があります。アドイン アプリをロードするには、<a href="https://adndevblog.typepad.com/technology_perspective/2013/09/auto-loading-for-autocad-addon-apps.html" rel="noopener" target="_blank"><strong>AutoCAD アドオンの自動ロードの方法あれこれ</strong></a> のとおり、さまざまな方法があります。</p>
<p>アドイン アプリのロード機構は、もともと、AutoCAD を効果的に利用するための仕組みですが、この仕組みを悪用する例が報告されています。例えば、オートデスク ユーザ会のフォーラムに記載されている <strong><a href="http://forums.augi.com/showthread.php?110828-AutoCAD%E6%96%B0%E7%A8%AE%E3%82%A6%E3%82%A3%E3%83%AB%E3%82%B9%E3%81%AE%E5%A0%B1%E5%91%8A" rel="noopener" target="_blank">AutoCAD ウィルス </a></strong>などが知られています。</p>
<p>こうした悪意のあるプログラムは、インターネットや USB デバイス、電子メールによる図面ファイルのやり取りでお使いのコンピュータに入り込み、アドイン アプリのロード機構を使って自身を複製したり、特定のコマンドを無効化したり、置き換えて偽装してしまう可能性があります。</p>
<p>問題なのは、アドイン アプリのファイル処理が悪用されてしまう点です。そこで、AutoCAD には、不正なアドイン アプリのロードを抑止する仕組みが導入されています。</p>
<p><strong>セキィリティの基本コンセプト</strong> <strong>－</strong> <strong>セキュア</strong> <strong>ロード</strong></p>
<p style="padding-left: 40px;">AutoCAD セキュリティの基本コンセプトは、「ユーザが指定した<strong>フォルダ</strong>にあるスタマイズ ファイルしかロードしない」、というものです。このフォルダを&#0160;<strong>信頼できる場所</strong><strong>&#0160;</strong>と呼びます。信頼できる場所は、後述する OPTIONS[オプション] コマンドで表示される [オプション] ダイアログの [ファイル] タブか、SECURITYOPTIONS[セキュリティオプション] &#0160;コマンドで表示される [セキュリティ &#0160;オプション] ダイアログで設定することが出来ます。</p>
<p style="padding-left: 40px;">AutoCAD は、信頼できる場所以外からのカスタム ファイルのロードに対し、警告ダイアログを表示して注意を促します。次のダイアログが表示された場合には、ユーザが選択的にロードするか否かを決定する必要があります。ここでは、不用意なロードを選択しないことが期待されます。</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e123bf38200b-pi" style="display: inline;"><img alt="Load_warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e123bf38200b image-full img-responsive" src="/assets/image_28268.jpg" title="Load_warning" /></a></p>
<p><strong>AutoCAD </strong><strong>のセキィリティ</strong></p>
<p style="padding-left: 40px;">SECURITYOPTIONS[セキュリティオプション] &#0160;コマンドによって、セキィリティ制御に関するシステム変数を包括的に設定出来るようになっています。</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef36e39200c-pi" style="display: inline;"><img alt="Security_options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef36e39200c image-full img-responsive" src="/assets/image_721038.jpg" title="Security_options" /></a></p>
<p style="padding-left: 40px;">表示される [セキュリティ &#0160;オプション] ダイアログを適切に設定、運用することで、作者の不明なアドイン アプリケーションを不用意にロードしてしまうことを効果的に抑止出来るようになっています。このダイアログにある3つのセキュリティ レベルでは、それぞれ、アドイン アプリケーションの扱いが異なってきます。</p>
<p style="padding-left: 40px;"><strong>高</strong></p>
<p style="padding-left: 80px;">信頼できる場所からのみロード<br />信頼できる場所意外の場所からのロード指示は無視<br />システム変数 <strong>SECURELOAD = 2</strong></p>
<p style="padding-left: 40px;"><strong>中</strong></p>
<p style="padding-left: 80px;">信頼できる場所からロード<br />信頼できる場所意外からのロード指示に対しては警告を発してユーザ選択によりロード<br />システム変数 <strong>SECURELOAD = 1</strong></p>
<p style="padding-left: 40px;"><strong>オフ</strong></p>
<p style="padding-left: 80px;">すべてのファイルをロード<br />警告なし<br />システム変数 <strong>SECURELOAD = 0&#0160;</strong></p>
<p style="padding-left: 40px;">※ JavaScript API ファイル（.js）を&#0160;Web サーバーから&#0160;ロードする場合には、システム変数&#0160;<strong>TRUSTEDDIMAINS</strong>&#0160;にURL を設定する必要があります。</p>
<p style="padding-left: 40px;"><strong>信頼される場所</strong></p>
<p style="padding-left: 80px;">信頼できる場所には複数のパスを設定することが出来ます。また、信頼できる場所に設定されたパスは、システム変数&#0160;<strong>TRUSTEDPATHS</strong>&#0160;に格納されます。信頼できる場所に指定するパスは、不用意なファイルの改変やコピー作成を抑止するために、読み込み専用のフォルダ属性を持たせることを推奨しています。</p>
<p style="padding-left: 80px;">なお、AutoCAD には、明示的に信頼できる場所に指定しなくても、自動的に信頼される次のパスがあります。</p>
<p style="padding-left: 80px;"><strong>・</strong>AutoCAD インストール フォルダと配下のサブ フォルダ<br /><strong>・</strong>バンドル パッケージを使った自動ロード メカニズムが利用する ApplicationPlugins フォルダ</p>
<p style="padding-left: 80px;">上記いずれかの場所に保存されているカスタマイズ ファイルをロードする場合、AutoCAD はカスタマイズ ファイルを警告なしでロードします。</p>
<p><strong>セキュリティ設定の注意</strong>&#0160;</p>
<p style="padding-left: 40px;">セキィリティ設定で推奨されるのは、言うまでもなく&#0160;<strong>高</strong>（システム変数 SECURELOAD = 2） または&#0160;<strong>中</strong>（システム変数 SECURELOAD = 1） の値となります。不用意なカスタマイズ ファイルのロードを許容してしまう&#0160;<strong>オフ</strong>（システム変数 SECURELOAD = 0） の設定は推奨されません。</p>
<p style="padding-left: 40px;">なお、これらのセキュリティ設定は、前述のとおり、システム変数&#0160;SECURELOAD と TRUSTEDPATHS を利用することになります。これとは別に、システム変数&#0160;<strong>LEGACYCODESEARCH</strong>&#0160;の設定にも注意を払うべきです。LEGACYCODESEARCH の値が&#0160;<strong>1</strong>&#0160;&#0160;に設定されていると、図面を開く際に、同じフォルダに acad.lsp または、acaddoc.lsp が存在する自動ロード用 AutoLISP ファイルがロードされて不正なコードが実行されてしまう可能性があります（含む、acad.fas、acad.vlx、acaddoc.fas、acaddoc.vls）。なお、図面を開く際に、毎回、acad.lsp（acad.fas、acad.vls） をロードするか否かの設定は、システム変数&#0160;<strong>ACADLSPASDOC</strong>&#0160;で指定することが可能です。</p>
<p><strong>ウィルスに感染してしまったら</strong></p>
<p style="padding-left: 40px;">AutoCAD に起動ショートカットを利用して、/safemode <strong><a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-CE4A928E-FDB1-470A-8176-32ABD1E3C6A1" rel="noopener" target="_blank">コマンド ライン スイッチ</a></strong>を指定します。このスイッチで起動された AutoCAD は、すべての実行可能ファイルのロードおよび実行を禁止します。この間に、ウィルスチェッカーなどで不正なファイルを除去するとともに、SECURITYOPTIONS[セキュリティオプション] &#0160;コマンドで、セキィリティ レベルを&#0160;<strong>高</strong>、または&#0160;<strong>中</strong>&#0160;に設定してください。&#0160;ウィルスや不正なファイルの駆除/除去が完了したら、/safemode&#0160;コマンド ライン スイッチを削除します。</p>
<p>By Toshiaki Isezaki</p>
