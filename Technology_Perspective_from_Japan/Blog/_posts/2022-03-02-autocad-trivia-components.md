---
layout: "post"
title: "AutoCAD 雑学：AutoCAD コンポーネント"
date: "2022-03-02 00:04:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/03/autocad-trivia-components.html "
typepad_basename: "autocad-trivia-components"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806ce005200d-pi" style="display: inline;"><img alt="Components" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806ce005200d image-full img-responsive" src="/assets/image_818527.jpg" title="Components" /></a></p>
<p>AutoCAD をインストールすると、指定したフォルダに数多くのファイルがインストールされます。これらのファイルは、AutoCAD の実行時にすべての機能動作のために必要な、実行ファイルや定義ファイルです。</p>
<p>Windows 版 AutoCAD の話題になってしまいますが、ここでは、この重要なコンポーネントである実行ファイルに焦点をあてて、なぜ、異なる拡張子のファイルが存在するのか少し深堀りをしてみたいと思います。</p>
<p><strong>実行ファイル？</strong></p>
<p>実行ファイルの別名は実行可能ファイル（<strong>exe</strong>cutable）で、拡張子 <strong>.exe</strong> を持つファイルを指します。Windows で言えば、デスクトップのショートカット アイコンをクリックしてアプリケーションをが起動する際に「実行される」ファイルです。AutoCAD の場合、実行ファイルは <strong>acad.exe</strong> になります。（AutoCAD LT の場合は <strong>acadlt.exe</strong>）</p>
<p>オートデスク社内で AutoCAD を開発している複数の開発者は、テキスト エディタを使ってプログラムを記述し、コンパイルという作業を経て実行ファイルを作成します。このコンパイル作業によって、人間が判読出来るプログラムが、テキスト エディタで開いても判読出来ないバイナリ ファイルに変換されます。</p>
<p>つまり、acad.exe をメモ帳などのテキスト エディタで開いてみても、内容を理解することは出来ません。バイナリ ファイルとなった実行ファイルを正しく理解し、実行することが出来るのは Windows OS だけです（Windows の場合）。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9aa31f200c-pi" style="display: inline;"><img alt="Vs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9aa31f200c image-full img-responsive" src="/assets/image_526287.jpg" title="Vs" /></a></p>
<p><strong>実行ファイルの分散ファイル？</strong></p>
<p>まもなく誕生から 40 年を迎える AutoCAD。毎年のバージョン アップで多くの機能を持つようになっています。実行ファイルである acad.exe も、最新の AutoCAD 2022 で 5.24 MB あります。</p>
<p>このファイル サイズを大きいと感じるか、小さいと感じるかは人それぞれと思います。ただ、実際は、もっと巨大になってしまった可能性もあります。</p>
<p>現在のサイズで収まっているのは、実行ファイルが分散して用意されているためです。Windows には、.exe の他に <strong>.dll</strong> の拡張子を持つ<strong>ダイナミック リンク ライブラリ</strong>（<strong>d</strong>ynamic <strong>l</strong>ink <strong>l</strong>ibrary） というファイルがあり、.exe ファイルの内容を分散することが出来ます。</p>
<p>.exe ファイルは実行時に .dll ファイルを参照することで、.dll ファイルに実装されている機能を利用することが可能になるというわけです。逆、.exe ファイルの実行時に、必要とする .dll ファイルが見つからないと、機能を実行できなかったり、最悪の場合、アプリケーションがクラッシュ/異常終了してしまいます。</p>
<p>AutoCAD のインストール フォルダを見てみると、このダイナミック リンク ライブラリ ファイルが沢山あるこをがわかると思います。いずれのファイルも、acad.exe の実行に必要なものです。</p>
<p><strong>DLL ファイルの亜種？</strong></p>
<p>ダイナミック リンク ライブラリ（.dll）は、 Microsoft 社が用意している<a href="https://docs.microsoft.com/ja-jp/troubleshoot/windows-client/deployment/dynamic-link-library" rel="noopener" target="_blank">仕組み</a>なので、 Windows では一般的に使用されています。だたし、主体になる実行ファイル（.exe）で参照する .dll は異なるため、Microsoft Excel や Word のインストール フォルダにある .dll ファイルを AutoCAD のインストール フォルダにコピーしても、AutoCAD が Excel ファイルや Word ファイルを開いて編集出来るわけではありません。</p>
<p>AutoCAD に目を戻しましょう。インストール フォルダの .dll にも様々な役割を持つ .dll ファイルが存在します。AutoCAD の特定の機能を実装する .dll のほか、ObjectARX や AutoCAD .NET API を「公開する」 .dll もあり、多種多様です。</p>
<p>また、オートデスクが外部からライセンス供与を受けて AutoCAD に組み込んでいる .dll も存在します。ライセンス供与されたテクノロジは、AutoCAD の ABOUT コマンドで表示されるダイアログ下部のクレジットで確認することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9aa608200c-pi" style="display: inline;"><img alt="About" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9aa608200c image-full img-responsive" src="/assets/image_640471.jpg" title="About" /></a></p>
<p>そんな中、AutoCAD の実行ファイルしか参照出来ない「特殊な」.dll ファイルも存在します。</p>
<p><strong>.arx ファイル</strong></p>
<p style="padding-left: 40px;"><strong>.arx</strong> ファイルは .dll の拡張子を変えたダイナミック リンク ライブラリです。通常の .dll と異なり、AutoCAD のみが参照することが出来るように設計されています。大きな特徴は、AutoCAD 実行ファイル（ acad.exe ）の起動時に参照（ロード）させるだけでなく、必要に応じて参照を開始（ロード）させたり、参照解除（ロード解除）させることが出来る点です。</p>
<p style="padding-left: 40px;">.arx は、AutoCAD API の 1 つである ObjectARX を使ってプログラムを作成、<a href="https://visualstudio.microsoft.com/ja/" rel="noopener" target="_blank">Microsoft Visual Studio</a>（Windows 版）を使ってコンパイル、作成されています。ObjectARX は非常に強力な AutoCAD API で、カスタム コマンドの定義だけでなく、独自の見た目や振る舞いを持つカスタム オブジェクトも定義することが可能です。他の AutoCAD API ではカスタム オブジェクトの定義は実装出来ないようになっています。</p>
<p style="padding-left: 40px;">ObjectARX が登場した AutoCAD R13（1994 年）当初、カスタム コマンドを定義するファイルもカスタム オブジェクトを定義するファイルも、すべて一律に .arx の拡張子を持たせていました。</p>
<p><strong>.dbx ファイル</strong></p>
<p style="padding-left: 40px;">カスタム オブジェクトは、線分や円、寸法や画層など、AutoCAD が図面ファイル（.dwg/.dxf）に保存するのと同じレベルで独自の見た目や振る舞いを持たせたオブジェクトを指します。特定の業種で扱うオブジェクトを用意する手段として非常に有用であるため、オートデスクも AutoCAD Mechanical や Plant3D、Civil3D などで使用しています。もちろん、3rd party の開発者がカスタム オブジェクトを定義することも出来ます。</p>
<p style="padding-left: 40px;">ただ、カスタム オブジェクトを定義する .arx ファイルをロード解除してしまったり、カスタム オブジェクトが含まれる図面を定義する .arx ファイルがない AutoCAD で開くと、AutoCAD が、本来オブジェクトが持つ独自の見た目や振る舞いを参照出来ないため、オブジェクトの運用に問題が起こります。このような状態のカスタム オブジェクトを<strong>プロキシ オブジェクト</strong>と呼びます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e145b899200b-pi" style="display: inline;"><img alt="Proxy_objext" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e145b899200b image-full img-responsive" src="/assets/image_829879.jpg" title="Proxy_objext" /></a></p>
<p style="padding-left: 40px;">カスタム オブジェクトがプロキシ オブジェクトになってしまった場合、オブジェクト自身の削除や、色、画層の変更など、カスタムな情報以外の編集を許可するかは、カスタム オブジェクトの定義で決まります。運用を考えると、最低でもカスタム オブジェクトが持つプロパティは参照出来るようにしたいはずです。</p>
<p style="padding-left: 40px;">そこで考え出されたのが、<strong>オブジェクト イネーブラ</strong>（<strong>Object Enabler</strong>）です。カスタム オブジェクトを定義する .arx ファイルの拡張子を <strong>.dbx</strong> に改め、カスタム オブジェクトを作成（作図）するコマンド定義 .arx ファイルと区別します。</p>
<p style="padding-left: 40px;">オブジェクト イネーブラを無償でダウンロード提供してプロキシ オブジェクトになるケースを低減し、オブジェクトの作成や高度な編集コマンドを定義する .arx ファイルを有償で販売する、といったものです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806d15ff200d-pi" style="display: inline;"><img alt="Object_enabler" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806d15ff200d image-full img-responsive" src="/assets/image_54963.jpg" title="Object_enabler" /></a></p>
<p style="padding-left: 40px;">オートデスクも、この方法でオブジェクト イネーブラ（.dbx）を<a href="https://knowledge.autodesk.com/ja/support/autocad/downloads?release=2022" rel="noopener" target="_blank">無償ダウンロード提供</a>しています。</p>
<p style="padding-left: 40px;">なお、プロキシ オブジェクトは、オブジェクト イネーブラを AutoCAD にロードした時点で、再び、カスタム オブジェクトの振る舞いを取り戻します。データの欠落は起こりません。</p>
<p><strong>.crx ファイル</strong></p>
<p style="padding-left: 40px;">コマンド定義を実装する ObjectARX ファイルを .arx に、カスタム オブジェクト定義を実装する ObjectARX ファイルを .dbx にして長らく販売が続けられてきた AutoCAD ですが、AutoCAD 2013（2012 年）で変化が訪れます。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/releasing-autocad-2019-for-mac-japanese.html" rel="noopener" target="_blank">AutoCAD for Mac</a> の再開計画です。Mac 版の AutoCAD でも、プログラムをコンパイルして実行ファイルやダイナミック リンク ライブラリなどの実行ファイル（バイナリ ファイル）を作成することになります。もちろん、Windows と Mac は異なる OS であるため、実行ファイルやダイナミック リンク ライブラリの拡張子も異なります。</p>
<p style="padding-left: 40px;">Windows 版 AutoCAD と Mac 版 AutoCAD を開発する際、少しでも共通したロジックを利用して開発リソースの増大を避けたい、とオートデスクは考えました。この取り組みを説明したのが、<a href="https://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" rel="noopener" target="_blank"><strong>コンソール バージョンの AutoCAD</strong> </a>のブログ記事です。</p>
<p style="padding-left: 40px;">AutoCAD のコマンドは、コマンド プロンプトでメッセージ表示と入力を繰り返して完結するコマンドと、ダイアログボックスなどのユーザ インタフェースを表示して操作を完結するコマンドに大別されます。後者のタイプでは、プラットフォームによって実装の仕方が大きく異なるため、専用化せざるおえません。一方、前者はロジックや実装を共通化出来るので、プラットフォーム間で開発リソースの増加を抑止することが可能なわけです。</p>
<p style="padding-left: 40px;">そこで、両者を区別するために拡張子の分離が考案されます。ユーザ インタフェースを持たないコマンド定義を実装する <strong>.crx</strong> と、ユーザ インタフェースを持つコマンド定義を実装する .arx です。</p>
<p style="padding-left: 40px;">AutoCAD for Mac と .crx の導入は副次的効果も生み出します。Forge の <strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/effectiveness-on-design-automation-api-for-autocad.html" rel="noopener" target="_blank">Design Automation API for AutoCAD</a></strong> です。ユーザ インタフェースを持たない「エンジン」にアドイン/プラグイン アプリをロードさせて実行する自動化する Design Automation API では、ユーザ インタフェースは不要なため、.crx が最適なわけです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806d17a0200d-pi" style="display: inline;"><img alt="Mac_win" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806d17a0200d image-full img-responsive" src="/assets/image_639100.jpg" title="Mac_win" /></a></p>
<p>もう一度 AutoCAD のインストール フォルダを除いてみましょう。定義ファイルのほかに、.exe 以外に、.dll、.arx、.dbx、.crx が多く存在していることがわかります。どれも AutoCAD の実行に必要なダイナミック リンク ライブラリということになります。一部を削除したり、ファイル名や拡張子を変更すると、AutoCAD が参照出来なくなり問題が発生します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e145b769200b-pi" style="display: inline;"><img alt="Files" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e145b769200b image-full img-responsive" src="/assets/image_75354.jpg" title="Files" /></a></p>
<p>最後にいくつか補足と注意点です。</p>
<ul>
<li>AutoCAD .NET API を使って作成したアドインは、ユーザ インタフェースの実装有無にかかわらず、常に .dll の拡張子を持ちます。Design Automation API for AutoCAD で使用する場合には、ユーザ インタフェースを持たない実装を使用する必要があります。</li>
<li>.dll、.arx、.dbx、.crx は、すべて AutoCAD バージョン毎にコンパイルされ直したものがインストールされます。異なるバージョン間の使用はサポートされないばかりか、これら一部の抜き出し、複製は、使用許諾違反になりますので注意してください。</li>
</ul>
<p>By Toshiaki Isezaki</p>
