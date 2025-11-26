---
layout: "post"
title: "Visual Studio Code での AutoLISP 開発"
date: "2020-04-08 00:00:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/04/autolisp-development-using-vs-code.html "
typepad_basename: "autolisp-development-using-vs-code"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51e5e6d200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51e5e6d200b-pi" style="display: inline;"></a>既にご紹介していますが、AutoCAD 2021 では AutoLISP の開発環境が大きく変わりました。今回は、AutoLISP の歴史を振り返り、AutoCAD 2021 で利用出来るようになった新しい開発方法に言及していきたいと思います。</p>
<hr />
<p><strong>あらためて AutoLISP とは？</strong></p>
<p style="padding-left: 40px;">AutoLISP は、AutoCAD のカスタマイズ API として最も 長い歴史を持ち、いまも多くのアプリケーション資産が利用されています。もともと人口知能の研究用に作られた <a href="https://ja.wikipedia.org/wiki/Common_Lisp" rel="noopener" target="_blank">CommonLISP 言語</a>の特徴であるリスト操作を、図形と画層など、CAD 固有情報の関連性を維持しながら操作出来るよう作られた AutoCAD 固有のプログラム言語です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f6789a200d-pi" style="display: inline;"><img alt="Autocad_api_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f6789a200d image-full img-responsive" src="/assets/image_780042.jpg" title="Autocad_api_history" /></a></p>
<p style="padding-left: 40px;">AutoLISP を利用すると、独自のカスタム コマンドを定義したり、AutoLISP 自身を拡張する関数を作成することが出来ます。過去、DOS、Windows、UNIX、Macintosh（当時名）の複数のプラットフォーム OS をサポートしていた AutoCAD ですが、OS を気にせず、そのまま実行出来た AutoLISP は、とても便利なクロスプラットフォーム開発環境でもありました。AutoCAD R12 からは、AutoLISP のプログラム ファイルと同じ ASCII テキスト形式を持つ DCL 言語も登場し、ダイアログ ボックスを定義、動作をコントロールできるようになっています。</p>
<p style="padding-left: 40px;">AutoLISP を使用するには、テキストエディタでプログラムを編集して拡張子 .lsp ファイルで保存し、そのファイルを AutoCAD 上にロードするだけです。DOS 版の AutoCAD が主流だった頃、AutoLISP の編集に使うエディタは特に提供されていなかったため、開発者が好みのテキスト エディタが利用されていました。</p>
<p style="padding-left: 40px;">Windows が主流になった頃、登場した AutoCAD が AutoCAD R14 です。このバージョンでは、AutoLISP の編集、デバッグの機能を持つ Visual LISP エディタが導入されています。この際、Visual LISP という名前を使い始め、AutoLISP を拡張した Visual LISP として、Windows で一般的な COM (Component Object Model) にアクセスできる機能が追加されています。COM は、VBA などが利用する ActiveX オートメーション の基盤技術で、多くのソフトウェアでも採用されています。Visual LISP から COM を使ったプログラムを作成することで、Microsoft Excel、Word といった、COM を使って機能を公開するアプリケーション(COM サーバー) と連携することができます。</p>
<p style="padding-left: 40px;">Visual LISP エディタは、VBA と同じように AutoLISP 言語を編集したりデバッグすることができる IDE(統合開発環境) です。従来のテキスト エディタでは不可能だったステップ実行、ウォッチ式による変数評価などの機能に加え、DCL 言語で定義されたダイアログ ボックスの評価も可能になりました。また、AutoLISP のソールファイル(.lsp) のコンパイル機能も追加され、単独でロード可能なソースファイル単位のバイナリファイル(.fas) 、DCL リソースファイルを含む複数のソースファイルを１つにまとめたバイナリファイル(.vlx) を作成することも出来るようになっていました。</p>
<p style="padding-left: 40px;">唯一の欠点は、UNICODE に完全対応していなかった点です。このため、プログラム内の日本語メッセージやデバッグ ウィンドウ出力が文字化けしてしまうことがありました。</p>
<p style="padding-left: 40px;">そんな AutoLISP&#0160; ですが、過去の枯れた API になってしまった訳ではありません。ビルド/コンパイル作業が不要であるため、クロス プラットフォームで、そのまま利用可能な唯一の AutoCAD API でもあり、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/07/releasing-autocad-2020-for-mac-japanese.html" rel="noopener" target="_blank">AutoCAD for Mac</a></strong> や Forge の <strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/06/design-automation-api-for-autocad.html" rel="noopener" target="_blank">Design Automation API for AutoCAD</a></strong> でも使える「<strong>現役の AutoCAD API</strong>」という側面も持っています。</p>
<p style="padding-left: 40px;">AutoCAD 2021 になって、従来の Visual LISP エディタに代わって、Visual Studio Code で AutoLISP の編集やデバッグが可能になりました。</p>
<hr />
<p><strong>Visual Studio Code とは？</strong></p>
<p style="padding-left: 40px;">今回、AutoLISP の編集に、Microsoft 社が<a href="https://github.com/microsoft/vscode" rel="noopener" target="_blank"><strong>オープン ソース</strong></a>として<strong>無償</strong>で公開している <strong><a href="https://azure.microsoft.com/ja-jp/products/visual-studio-code/" rel="noopener" target="_blank">Visual Studio Code</a></strong>（通称、VS Code）を採用し、UNICODE に完全対応出来るようになりました。Microsoft Azure との親和性が高く、JavaScript や Python など、主にクラウド・Web 開発をおこなうための統合開発環境です。</p>
<p style="padding-left: 40px;">VS Code は、自身を拡張する Extension という仕組みを持っているため、適切な Extension を入手出来れば、クラウドや Web 開発以外でも利用することが出来る、とても柔軟性を持った「エディタ」と言えます。</p>
<p style="padding-left: 40px;">AutoCAD 2021 の登場にあわせて、オートデスクは <strong><a href="https://marketplace.visualstudio.com/items?itemName=Autodesk.autolispext" rel="noopener" target="_blank">AutoCAD AutoLISP Extension</a> （無償）</strong>の提供を開始しています。VS Code に AutoCAD AutoLISP Extensionをインストールすると、VS Code 上で AutoLISP プログラムの作成と編集、また、AutoCAD とともにデバッグ出来るようになります。もちろん、ウォッチ式を使った変数の評価など、一般的におこなう開発作業が可能です。つまり、いままでの Visual LISP エディタの代替が出来るわけです。</p>
<p style="padding-left: 40px;">※ VLIDE コマンドで Visual LISP エディタを利用するか、VS Code を利用するかは、システム変数 <strong><a class="xref" href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-1853092D-6E6D-4A06-8956-AD2C3DF203A3">LISPSYS</a> </strong>で変更することが出来ます。</p>
<hr />
<p><strong>Visual Studio Code （VS Code）の準備</strong></p>
<p style="padding-left: 40px;">AutoCAD 2021 をインストール後に VLIDE コマンドを実行すると、Visual Studio Code と AutoCAD AutoLISP Extension のインストールを促すダイアログ ボックスが表示されます。まずは、Visual Studio Code を準備しましょう。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51b2baa200b-pi" style="display: inline;"><img alt="Vlide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51b2baa200b image-full img-responsive" src="/assets/image_500946.jpg" title="Vlide" /></a></p>
<p style="padding-left: 40px;">このダイアログ ボックスで「Microsoft Visual Studio Code」右の ダウンロード リンクをクリックすると、<strong><a href="https://visualstudio.microsoft.com/ja/">https://visualstudio.microsoft.com/ja/</a></strong> ページが表示されるので、AutoCAD 2021 Windows 版の実行環境に合わせて、[<strong>Download Visual Studio Code</strong>]ドロップ ダウン メニューから [<strong>Windows x64</strong> User Installer] をクリックして、インストーラをダウンロード、画面の指示に従ってインストールしてください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f6ac07200d-pi" style="display: inline;"><img alt="Vs_code_download" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f6ac07200d image-full img-responsive" src="/assets/image_416416.jpg" title="Vs_code_download" /></a></p>
<p style="padding-left: 40px;">※ VS Code は、<strong><a href="https://azure.microsoft.com/ja-jp/products/visual-studio-code/">https://azure.microsoft.com/ja-jp/products/visual-studio-code/</a></strong> からダウンロードすることが出来ます。</p>
<p style="padding-left: 40px;">もし、AutoCAD 2021 をインストールした環境に、既に Visual Studio Code が存在していると、次のダイアログ ボックスが表示されて、VLIDE コマンドで起動するエディタの選択を促されます。AutoCAD 2021 では、いまのところ、従来の Visual LISP エディタも利用することが出来ますが、特に理由がない限り、Visual Studio Code の利用をお勧めします。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b410e9e200c-pi" style="display: inline;"><img alt="LISPSYS" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b410e9e200c image-full img-responsive" src="/assets/image_636874.jpg" title="LISPSYS" /></a></p>
<p style="padding-left: 40px;">※ VLIDE コマンドで起動するエディタは、<strong><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-1853092D-6E6D-4A06-8956-AD2C3DF203A3" rel="noopener" target="_blank">LISPSYS</a></strong> システム変数で、いつでも変更することが出来ます。</p>
<p style="padding-left: 40px;">Visual Studio Code のインストール直後は、起動後に表示されるユーザインタフェースが英語のままになっています。日本語表示に切り替えるには、[View] メニューから [Command Palette ...] を選択して、プロンプト下に候補から <strong>configure display language</strong> を見つけてクリックするか、直接 <strong>configure display language</strong> を入力してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f6af18200d-pi" style="display: inline;"><img alt="Configure_display_language" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f6af18200d image-full img-responsive" src="/assets/image_413896.jpg" title="Configure_display_language" /></a></p>
<p style="padding-left: 40px;">[Select Display Launguage] と表示されたら、[Install additional languages...] を選択してください。その後、画面に左側に一覧表示される言語パックの「<strong><span class="name clickable" title="Extension name">Japanese Language Pack for Visual Studio Code</span></strong>」して、表示されるページ内の [Install] から日本語言語パックをインストールしてください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51b62cc200b-pi" style="display: inline;"><img alt="Configure_display_language_ja" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51b62cc200b image-full img-responsive" src="/assets/image_256035.jpg" title="Configure_display_language_ja" /></a></p>
<p style="padding-left: 40px;">VS Code を再起動すると、ユーザインタフェースが日本語表示になるはずです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b4146b0200c-pi" style="display: inline;"><img alt="Vs_code_ja" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b4146b0200c image-full img-responsive" src="/assets/image_175230.jpg" title="Vs_code_ja" /></a></p>
<hr />
<p><strong>AutoCAD AutoLISP Extensionの準備</strong></p>
<p style="padding-left: 40px;">VS Code で AutoCAD 2021 とともに AutoLISP プログラムの開発、デバッグを進めるには、AutoCAD AutoLISP Extension が必要です。AutoCAD AutoLISP Extension は、<a href="https://marketplace.visualstudio.com/items?itemName=Autodesk.autolispext">https://marketplace.visualstudio.com/items?itemName=Autodesk.autolispext</a> からダウンロードしたり、VS Code 内から直接、ダウンロードとインストールをおこなうことが出来ます。</p>
<p style="padding-left: 40px;">VS Code 内で AutoCAD AutoLISP Extension をインストールするには、画面左の「<strong>Extensions</strong>」をクリックしてから（①）、<em><strong>AutoCAD AutoLISP</strong></em> と入力して（②）、リストされてきた AutoCAD AutoLISP Extension を選択後（③）、表示されるページ内の <span style="color: #ffffff; background-color: #ff0000; font-family: arial, helvetica, sans-serif;"><strong>[Install]</strong></span> でインストールをしてください（④）。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f6b185200d-pi" style="display: inline;"><img alt="Autolisp_extension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f6b185200d image-full img-responsive" src="/assets/image_20765.jpg" title="Autolisp_extension" /></a></p>
<hr />
<p><strong>Visual Studio Code を使った AutoLISP 開発</strong></p>
<p style="padding-left: 40px;">Visual Studio Code と AutoCAD AutoLISP Extension を<span style="text-decoration: underline;">インストール後</span>、 AutoCAD 2021 を起動して VLIDE コマンドを実行すると、次のダイアログが表示されるはずです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f6b2d6200d-pi" style="display: inline;"><img alt="Vs_code_acceptance" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f6b2d6200d img-responsive" src="/assets/image_901264.jpg" title="Vs_code_acceptance" /></a></p>
<p style="padding-left: 40px;">これは、AutoCAD AutoLISP Extension がデバッグ作業時に AutoCAD をコントロールする許可を求める画面なので、許可する意味で [開く(O)] ボタンをクリックしてください。VS Code が表示され、次のダイアログが表示されるはずです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51b6759200b-pi" style="display: inline;"><img alt="Iniial_message" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51b6759200b image-full img-responsive" src="/assets/image_452337.jpg" title="Iniial_message" /></a></p>
<p style="padding-left: 40px;">VS Code を使った開発作業は、このメッセージのとおり、VS Code 上で開いた AutoLISP ファイルや、VS Code で新規ファイルとして作成した AutoLISP ファイルのデバッグ実行が可能です。</p>
<p style="padding-left: 40px;">※ 現在、メニュー名が [デバッグ] から [実行] に変わっています。<br />※ 新規作成した AutoLISP ファイルは、一旦、拡張子 .lsp で保存する必要があります。</p>
<p style="padding-left: 40px;">VS Code で可能なのは、おおまかに次の内容です。</p>
<ul>
<li>
<ul class="ul" id="GUID-A343CF22-706E-4F58-9CE4-B48CBCA3589B__UL_30CC6B269D95472EB81387A209758677">
<li class="li" id="GUID-A343CF22-706E-4F58-9CE4-B48CBCA3589B__LI_27625B7B1E694A17B901808C523337D8"><a href="https://ja.wikipedia.org/wiki/%E8%87%AA%E5%8B%95%E8%A3%9C%E5%AE%8C">オートコンプリート（自動補完）</a>や<a href="https://ja.wikipedia.org/wiki/%E3%82%B9%E3%83%8B%E3%83%9A%E3%83%83%E3%83%88" rel="noopener" target="_blank">コード スニペット</a>などの機能を使用して、.lsp ファイルを作成および修正</li>
<li class="li" id="GUID-A343CF22-706E-4F58-9CE4-B48CBCA3589B__LI_C20120285DA54F6C9FA102DC51635896">.lsp ファイル内のすべてまたは選択したソース コードを<a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-FD9081C8-0ADA-459F-924E-555642D1A39A" rel="noopener" target="_blank">整形</a></li>
<li class="li" id="GUID-A343CF22-706E-4F58-9CE4-B48CBCA3589B__LI_B659974EF7E34DA3AB7026E6524D6269">.lsp ファイルのデバッグ時にウォッチ式やブレークポイントを追加</li>
<li class="li" id="GUID-A343CF22-706E-4F58-9CE4-B48CBCA3589B__LI_FC8E4E6E12D64AA58D3792CBD72E0629">デバッグ コンソールで AutoLISP コード文や AutoCAD コマンドを実行</li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;">下記に、VS Code を使ったデバッグの様子を動画にしましたので、ご確認ください。</p>
<p class="asset-video" style="padding-left: 40px;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/0wIWSM3YAbc?feature=oembed" width="500"></iframe></p>
<p class="asset-video" style="padding-left: 40px;">編集した AutoLISP プログラムは、新しく用意された <a class="xref" href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-1A8B50AA-1DEA-4853-AAA8-09AF0827A0ED">MAKELISPAPP[LISP アプリを作成]</a> コマンドを使って、配布に適したアプリケーション ファイル（.vlx ファイル）にコンパイルすることが出来ます。コンパイル時には、複数の AutoLISP ファイル（.lsp ファイル）を 1 つの .vlx ファイルにすることが出来るだけでなく、同時にバイナリ ファイル化されるので、ソース コードを保護することも出来ます。なお、コンパイル時には、従来通り、ウィザードが用意されています。</p>
<p class="asset-video" style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b415496200c-pi" style="display: inline;"><img alt="Vl_wizard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b415496200c img-responsive" src="/assets/image_975703.jpg" title="Vl_wizard" /></a></p>
<p class="asset-video" style="padding-left: 40px;">VS Code の利用には、オンライン ヘルプ「<strong><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-7BE00235-5D40-4789-9E34-D57685E83875" rel="noopener" target="_blank">Visual Studio Code を使用する(AutoLISP/VS Code)</a></strong>」もご確認ください。</p>
<hr />
<p>By Toshiaki Isezaki</p>
