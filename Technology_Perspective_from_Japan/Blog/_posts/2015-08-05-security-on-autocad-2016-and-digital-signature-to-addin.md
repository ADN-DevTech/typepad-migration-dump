---
layout: "post"
title: "AutoCAD 2016 のセキュリティとアドインのデジタル署名"
date: "2015-08-05 00:19:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/08/security-on-autocad-2016-and-digital-signature-to-addin.html "
typepad_basename: "security-on-autocad-2016-and-digital-signature-to-addin"
typepad_status: "Publish"
---

<p>AutoCAD LT を除く AutoCAD と AutoCAD ベースの業種別製品は、API カスタマイズによって独自に機能を拡張出来る仕組みが備わっています。また、API を使ったカスタマイズでは、しばしば AutoCAD の起動時に API カスタマイズで作成したファイルを自動ロードさせて、すぐに実行できる環境を構築するのが一般的です。</p>
<p>AutoCAD 起動時にカスタマイズ ファイルを自動ロードするには、いくつかの方法が存在します。詳細は、以前のブログ記事 <a href="http://adndevblog.typepad.com/technology_perspective/2013/09/auto-loading-for-autocad-addon-apps.html" rel="noopener" target="_blank"><strong>AutoCAD アドオンの自動ロードの方法あれこれ</strong></a>&#0160;に譲りますが、ここで気になる問題があります。自動ロードは、もともと、AutoCAD を効果的に利用する目的で用意された仕組みですが、インターネットの普及にともない、この仕組みを悪用する例が出始めているのです。実例として、オートデスク ユーザ会のフォーラムに記載されている <a href="http://forums.augi.com/showthread.php?110828-AutoCAD%E6%96%B0%E7%A8%AE%E3%82%A6%E3%82%A3%E3%83%AB%E3%82%B9%E3%81%AE%E5%A0%B1%E5%91%8A" rel="noopener" target="_blank">AutoCAD ウィルス</a>や、トレンドマイクロ社のブログに記載されている<a href="http://blog.trendmicro.co.jp/archives/8186" rel="noopener" target="_blank">マルウェア</a>などが報告されています。</p>
<p>こうした悪意のあるプログラムは、たとえ、インターネットに接続していない環境でなくても、USB デバイスや EMail による図面ファイルのやり取りでも感染を誘発する可能性があります。重要なのは、AutoCAD が本来持つ自動ロード用のファイルが偽装されて悪用されている点です。このため、AutoCAD の利用者が感染に気が付かない場合が出てきてしまう点です。そこで、AutoCAD は過去数バージョンに渡って、徐々にセキュリティ対策を講じてきています。</p>
<p>ここでセキィリティの対象になるファイルには、次のようなカスタマイズ ファイルがあります。</p>
<ul>
<li>ObjectARX ファイル（.arx、.dbx、.crx）&#0160;</li>
<li>AutoLISP ファイル（.lsp、.fas、.vlx）</li>
<li>Heidi システム プリンタ ドライバ ファイル（.hdi）</li>
<li>メニュー マクロ ファイル（.mnl）</li>
<li>スクリプト ファイル（.scr）&#0160;</li>
<li>.NET アセンブリ ファイル（.dll）</li>
<li>VBA マクロ(DVB ファイル)&#0160;</li>
<li>acad.rx&#0160;</li>
<li>JavaScript ファイル（.js、.html、.htm）</li>
<li>その他 DLL ファイル</li>
</ul>
<p><strong>セキィリティの基本コンセプト － セキュア ロード</strong></p>
<p style="padding-left: 30px;">AutoCAD セキュリティの基本コンセプトは、ユーザが指定した特定のフォルダに保存されたアドイン アプリケーション ファイル等のカスタマイズ ファイルしかロードしない、というものです。この特定のフォルダを <strong>信頼できる場所&#0160;</strong>と呼びます。信頼できる場所は、OPTIONS[オプション] コマンドで表示される [オプション] ダイアログの [ファイル] タブか、SECURITYOPTIONS[セキュリティオプション] &#0160;コマンドで表示される [セキュリティ &#0160;オプション] ダイアログで設定することが出来ます。</p>
<p style="padding-left: 30px;">信頼できる場所には複数のパスを設定することが出来、設定されたパスは、システム変数&#0160;<strong>TRUSTEDPATHS</strong> に格納されます。なお、信頼できる場所に指定するパスは、不用意なファイルの改変やコピー作成を抑止するために、読み込み専用のフォルダ属性を持たせるのが一般的です。</p>
<p style="padding-left: 30px;">AutoCAD は、信頼できる場所以外からのカスタム ファイルのロードに対し、警告ダイアログを表示して注意を促します。次のダイアログが表示された場合には、ユーザが選択的にロードするか否かを決定します。</p>
<p style="text-align: center; padding-left: 30px;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08592a03970d-pi"><img alt="Warning1" src="/assets/image_674637.jpg" title="Warning1" /></a>&#0160;</p>
<p style="padding-left: 30px; text-align: left;">セキュリティ関連のシステム変数には、もう1つ重要なシステム変数 <strong>SECURELOAD</strong> が存在します。この値が、0 に設定されている場合、信頼できる場所 にパスが設定されていても、AutoCAD は警告なしでカスタマイズ ファイルをロードしてしまいます。システム変数 SECURELOAD に設定可能な値と内容は、次のとおりです。</p>
<p style="padding-left: 60px;"><span style="font-size: 13pt;"><strong>0</strong> </span></p>
<p style="padding-left: 90px;">警告を表示せずに、すべての場所から実行可能ファイルをロードします。旧バージョンと同じ動作を保持しますが、お勧めできません。</p>
<p style="padding-left: 60px;"><span style="font-size: 13pt;"><strong>1</strong>&#0160;</span></p>
<p style="padding-left: 90px;">実行可能ファイルが、システム変数 TRUSTEDPATHS で指定された信頼できる場所にある場合にのみロードします。信頼できる場所意外のパスにある実行可能ファイルのロード要求に対しては警告が表示されます。</p>
<p style="padding-left: 60px;"><span style="font-size: 13pt;"><strong>2</strong> </span></p>
<p style="padding-left: 90px;">実行可能ファイルの場所が、システム変数 TRUSTEDPATHS で指定されている場合にのみロードすることができます。</p>
<p><strong>セキュア ロードで自動的に信頼される場所</strong></p>
<p style="padding-left: 30px;">AutoCAD には、明示的に信頼できる場所に指定しなくても、自動的に信頼される次のパスがあります。</p>
<ul>
<ul>
<li>AutoCAD インストール フォルダと配下のサブ フォルダ</li>
<li>バンドル パッケージを使った自動ロード メカニズムが利用する ApplicationPlugins フォルダ</li>
</ul>
</ul>
<p style="padding-left: 30px;">上記いずれかの場所に保存されているカスタマイズ ファイルをロードする場合、AutoCAD はカスタマイズ ファイルを警告なしでロードします。</p>
<p style="text-align: left;"><strong>AutoCAD 2016 のセキィリティ</strong></p>
<p style="padding-left: 30px;">最新の AutoCAD 2016 では、SECURITYOPTIONS[セキュリティオプション] &#0160;コマンドによって、セキィリティ制御に関するシステム変数を包括的に設定出来るようになっています。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb085da7f6970d-pi" style="display: inline;"><img alt="Security_options" class="asset  asset-image at-xid-6a0167607c2431970b01bb085da7f6970d img-responsive" src="/assets/image_265254.jpg" style="width: 420px;" title="Security_options" /></a>&#0160;</p>
<p style="padding-left: 30px;">表示される [セキュリティ &#0160;オプション] ダイアログを適切に設定、運用することで、作者の不明なアドイン アプリケーションを不用意にロードしてしまうことを効果的に抑止出来るようになっています。このダイアログにある 3 つのセキュリティ レベルでは、それぞれ、アドイン アプリケーションの扱いが異なってきます。</p>
<p style="padding-left: 60px;"><span style="font-size: 13pt;"><strong>高</strong></span></p>
<ul>
<ul>
<ul>
<li>信頼できる場所からのみロード</li>
<li>信頼できる場所意外の場所からのロード指示は無視</li>
<li>システム変数 SECURELOAD = 2</li>
</ul>
</ul>
</ul>
<p style="padding-left: 60px;"><span style="font-size: 13pt;"><strong>中</strong></span></p>
<ul>
<ul>
<ul>
<li>信頼できる場所からロード</li>
<li>信頼できる場所意外からのロード指示に対しては警告を発してユーザ選択によりロード</li>
<li>システム変数 SECURELOAD = 1</li>
</ul>
</ul>
</ul>
<p style="padding-left: 60px;"><span style="font-size: 13pt;"><strong>オフ</strong></span></p>
<ul>
<ul>
<ul>
<li>すべてのファイルをロード</li>
<li>警告なし</li>
<li>システム変数 SECURELOAD = 0&#0160;</li>
</ul>
</ul>
</ul>
<p style="padding-left: 30px;">※ JavaScript API ファイル（.js）を&#0160;Web サーバーから&#0160;ロードする場合には、<br />&#0160; &#0160; システム変数 <strong>TRUSTEDDIMAINS</strong> にURL を設定する必要があります。</p>
<p><strong>セキュリティ設定の注意</strong>&#0160;</p>
<p style="padding-left: 30px;">セキィリティ設定で推奨されるのは、言うまでもなく <strong>高</strong>（システム変数 SECURELOAD = 2） または <strong>中</strong>（システム変数 SECURELOAD = 1） の値となります。不用意なカスタマイズ ファイルのロードを許容してしまう <strong>オフ</strong>（システム変数 SECURELOAD = 0） の設定は、推奨されません。</p>
<p style="padding-left: 30px;">なお、これらのセキュリティ設定は、前述のとおり、システム変数&#0160;SECURELOAD と TRUSTEDPATHS を利用することになります。これとは別に、システム変数&#0160;<strong>LEGACYCODESEARCH</strong> の設定にも注意を払うべきです。LEGACYCODESEARCH の値が <strong>1</strong> &#0160;に設定されていると、図面を開く際に、同じフォルダに acad.lsp または、acaddoc.lsp が存在する自動ロード用 AutoLISP ファイルがロードされて不正なコードが実行されてしまう可能性があります（含む、acad.fas、acad.vlx、acaddoc.fas、acaddoc.vls）。なお、図面を開く際に、毎回、acad.lsp（acad.fas、acad.vls） をロードするか否かの設定は、システム変数&#0160;<strong>ACADLSPASDOC</strong> で指定することが可能です。</p>
<p><strong>よりセキュアな環境の構築</strong>&#0160; － アドイン アプリケーション開発者への要求事項</p>
<p style="padding-left: 30px;">AutoCAD 2016 では、アドイン アプリケーションに用いられる次のファイルに対して、デジタル署名を施すことを推奨しています。<span style="background-color: #ffff00;">デジタル署名されたアドイン アプリケーション&#0160;ファイルは、ロード時に信頼できる場所になくても、警告なしで AutoCAD にロードすることが出来ます。</span></p>
<ul>
<ul>
<li>ObjectARX ファイル（.arx、.dbx、.crx）&#0160;</li>
<li>AutoLISP ファイル（.lsp、.fas、.vlx）</li>
<li>Heidi システム プリンタ ドライバ ファイル（.hdi）</li>
<li>メニュー マクロ ファイル（.mnl）</li>
<li>.NET アセンブリ ファイル（.dll）</li>
<li>その他 DLL ファイル&#0160;</li>
</ul>
</ul>
<p style="padding-left: 60px;">※ VBA ファイル（.dvb） と JavaScript API ファイル（.js） はデジタル署名の<br />&#0160; &#0160; 対象外です。</p>
<p style="padding-left: 30px;">デジタル署名が施されたアドイン アプリケーションは、過去のブログ <a href="http://adndevblog.typepad.com/technology_perspective/2014/11/security-for-drawing-file-2.html" rel="noopener" target="_blank"><strong>図面ファイルのセキュリティ － デジタル署名</strong></a>&#0160;で紹介した図面ファイルへのデジタル署名と同様に、アドイン アプリケーションの作成者を明確するだけでなく、不正な改ざんを把握することも可能になります。不正な改ざんがおこなわれたファイルでは、デジタル署名が無効になってしまいます。 例えば、次のダイアログは、デジタル署名後に内容を修正した AutoLISP ファイル ロード時の警告ダイアログです。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1460ee7970c-pi" style="display: inline;"><img alt="Invalid_signature" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1460ee7970c img-responsive" src="/assets/image_877164.jpg" style="width: 450px;" title="Invalid_signature" /></a></p>
<p style="padding-left: 30px;">AutoCAD のアドイン アプリケーション ファイルにデジタル署名を施すには、まず、認証局からデジタル証明書を取得（購入）、または、<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-26D7B31C-4165-410C-9FC4-2D556749D517" rel="noopener" target="_blank">独自に作成</a>してから 、個人情報交換（.pfx）ファイルを<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-DC1B25FE-E063-486C-B90C-565AB5E87BBC" rel="noopener" target="_blank">作成</a>後に<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-19D6716A-0AD1-4A7A-82BA-A067E6D65F66" rel="noopener" target="_blank">インポート</a>します。続いて、次のいずれかのツールを利用して、ファイルに署名します。</p>
<ul>
<ul>
<li><strong>AcSignApply.exe</strong></li>
</ul>
</ul>
<p style="padding-left: 90px;">AutoLISP ファイル（.lsp、.fas、.vlx） とメニュー マクロ ファイル（.mnl）へデジタル署名します。AcSignApply.exe&#0160;は、AutoCAD のインストール フォルダ内に格納されています。</p>
<ul>
<ul>
<li><a href="https://msdn.microsoft.com/ja-jp/library/8s9b9yaz%28v=VS.110%29.aspx" rel="noopener" target="_blank"><strong>SignTool.exe</strong></a></li>
</ul>
</ul>
<p style="padding-left: 90px;">ObjectARX ファイル（.arx、.dbx、.crx） と .NET アセンブリ ファイル（.dll）や、その他 DLL ファイルへデジタル署名します。SignTool.exe は、Windows SDK の最新バージョンは、Microsoft のWeb サイト（<a href="http://msdn.microsoft.com/ja-JP/windows/desktop" rel="noopener" target="_blank">http://msdn.microsoft.com/ja-JP/windows/desktop</a>）から入手することができます。</p>
<p style="padding-left: 30px;">なお、ここで利用するデジタル証明書は、図面ファイルへのデジタル署名と同様、<a href="https://ja.wikipedia.org/wiki/X.509" rel="noopener" target="_blank">X.509 規格</a>に準拠している必要があります。 個々の署名方法については、AutoCAD 2016 の<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-0A93626D-8389-45FC-969B-B86A2F37D691" rel="noopener" target="_blank">オンラインヘルプ</a>を参照してください。</p>
<p style="padding-left: 30px;">なお、AutoLISP ファイルへ署名する際に利用する AcSignApply.exe&#0160;ツールは、DWG ファイルへデジタル署名を加えるツールと同一です。操作は [デジタル署名をアタッチ] ダイアログで完了することが出来ます。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7bb50dd970b-pi" style="display: inline;"><img alt="Sign_to_lisp" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7bb50dd970b img-responsive" src="/assets/image_199370.jpg" style="width: 450px;" title="Sign_to_lisp" /></a></p>
<p style="padding-left: 30px;">一方、SignTool.exe を利用した ObjectARX や .NET API ファイルへの署名は、Windows のコマンド プロンプトからおこないます。例えば、.NET API で作成した MyAddin1.dll への署名は、次のような書式で署名します（黄色部は可変です）。&#0160;</p>
<p style="padding-left: 60px;">signtool.exe sign /f <span style="background-color: #ffff00;">xxxx.pfx</span> /p <span style="background-color: #ffff00;">パスワード</span> /t http://timestamp.verisign.com/scripts/timstamp.dll <span style="background-color: #ffff00;">MyAddin1.dll</span></p>
<p><strong><span style="background-color: #ffffff;">ウィルスに感染してしまったら</span></strong></p>
<p style="padding-left: 30px;">&#0160;AutoCAD に起動ショートカットを利用して、/safemode <a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-CE4A928E-FDB1-470A-8176-32ABD1E3C6A1" rel="noopener" target="_blank">コマンド ライン スイッチ</a>を指定します。このスイッチで起動された AutoCAD は、すべての実行可能ファイルのロードおよび実行を禁止します。この間に、ウィルスチェッカーなどで不正なファイルを除去するとともに、SECURITYOPTIONS[セキュリティオプション] &#0160;コマンドで、セキィリティ レベルを <strong>高</strong>、または <strong>中</strong> に設定してください。&#0160;ウィルスや不正なファイルの駆除/除去が完了したら、/safemode&#0160;コマンド ライン スイッチを削除します。</p>
<p><span style="background-color: #ffffff;">By Toshiaki Isezaki</span>&#0160;</p>
