---
layout: "post"
title: "AutoCAD のカスタマイズを手助けする AutoCAD API"
date: "2013-02-06 01:47:02"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/02/autocad-api-helps-cutomizing-autocad.html "
typepad_basename: "autocad-api-helps-cutomizing-autocad"
typepad_status: "Publish"
---

<p>AutoCAD をカスタマイズするために、AutoCAD には、現在 4 つの API が用意されています。それぞれ、<strong>AutoLISP</strong>、<strong>COM</strong>（ActiveX オートメーション）、<strong>ObjectARX</strong>、<strong>.NET API</strong> がこれにあたります。各 API は、異なる AutoCAD バージョンで採用されたので、使用する AutoCAD によっては、利用できないものがあるかもしれません。年表のようにまとめてみると、次のようになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b4b1aa970d-pi" style="display: inline;"><img alt="API_History" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b4b1aa970d image-full img-responsive" src="/assets/image_637322.jpg" title="API_History" /></a></p>
<p style="text-align: left;">API の主な特徴は次のとおりです。</p>
<p><strong>AutoLISP</strong></p>
<p style="padding-left: 30px;">AutoCAD のカスタマイズ API として最も 長い歴史を持ち、アプリケーションとしても多くの資産が利用されています。AutoLISP は、もともと人口知能の研究用に作られた CommonLISP 言語の特徴であるリスト操作を、CAD 上の図形データに適用させた AutoCAD 固有のプログラム言語です。AutoCAD 上では 、プログラムを実行時に順次解釈して実行してく インタプリタ言語 として動作します。インタプリタの利点は、ソースファイルを実行ファイルに変換するコンパイル作業が必要がない点です。反面、不具合が潜在しているプログラムがある場合には、実際に実行してみないとエラーが検出されないというマイナス点もあります。 <br />　 <br />AutoLISP を使用するには、テキストエディタでプログラムを編集して拡張子 .lsp ファイルとして保存し、そのファイルを AutoCAD 上にロードします。AutoLISP では、アプリケーション固有のコマンドを定義したり、再利用可能な AutoLISP 関数を作成することが可能です。過去の AutoCAD バージョンでは、一時期、DOS、Windows、UNIX、Macintosh の複数のプラットフォーム OS をサポートしていましたが、AutoLISP のソースコードはテキスト形式だったため、OS に依存ぜずにソースコードの互換性を維持できました。AutoCAD R12 からは、ダイアログ ボックスを制御するための DCL 言語も扱えるようになりました。</p>
<p style="padding-left: 30px;">このように、長い歴史を持つ AutoLISP ですが、利用することができるのは AutoCAD 内部に限定されています。Windows 上の他のソフトウェアを AutoLISP でカスタマイズすることはできません。また、AutoCAD が標準で提供する AutoLISP 関数以外の機能を利用することはできません。例えば、オペレーティング システム(OS) の機能にアクセスしたり、Web にアクセスするなど、AutoCAD 外部との連携のために AutoLISP を 使用することはできません。</p>
<p style="padding-left: 30px;">AutoLISP を拡張した Visual LISP になって、Windows で一般的な COM (Component Object Model) にアクセスできる機能が追加されました。COM は、VBA などが利用する ActiveX オートメーション の基盤技術で、多くのソフトウェアでも採用されています。Visual LISP から COM を使ったプログラムを作成することで、Microsoft Excel、Word といった、COM を使って機能を公開するアプリケーション(COM サーバー) と連携することができます。</p>
<p style="padding-left: 30px;">Visual LISP は、AutoLISP と互換性を持つ開発環境です。Visual LISP は、VBA と同じように AutoLISP 言語を編集したり評価することができる IDE(統合開発環境) を備えています。この IDE では、従来のテキスト エディタでは不可能だったデバッグ作業や、DCL 言語で定義されたダイアログの評価も可能になりました。また、AutoLISP のソールファイル(.lsp) のコンパイル機能も追加され、単独でロード可能なソースファイル単位のバイナリファイル(.fas) 、DCL リソースファイルを含む複数のソースファイルを１つにまとめたバイナリファイル(.vlx) を作成することもできます。</p>
<p style="padding-left: 30px;"><span style="color: #ff0000; font-size: 11pt;"><strong>※</strong></span><strong>AutoLISP の学習リソース</strong>は、AutoCAD に同梱されるオンライン ヘルプの <strong><a href="http://docs.autodesk.com/ACD/2013/JPN/files/GUID-FC204E55-2CC9-4B89-905D-E7BC2250D9BC.htm" target="_blank">こちら</a></strong> から参照することができます。</p>
<p><strong>COM（ActiveX オートメーション）</strong></p>
<p style="padding-left: 30px;">ActiveX オートメーションは、Mircrosoft の提供するコンポーネント テクノロジ COM (Component Object Model) を利用した開発環境です。COM には、ソフトウェアの持つさまざまな機能を公開する COM サーバー と、公開された機能を参照して再利用する COM クライアント の 2 つの機能があります。AutoCAD の場合、AutoCAD が COM サーバーの役割を持ち、COM クライアント機能を持った VBA や、Visual Studio (別売) などから AutoCAD の機能を利用するプログラムを開発することができます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36a13aa1970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;">&#0160;</a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80ff350970b-pi" style="display: inline;"><img alt="COM_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80ff350970b img-responsive" src="/assets/image_609287.jpg" title="COM_1" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36a13aa1970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a></p>
<p style="padding-left: 30px; text-align: left;">COM は、Windows プラットフォーム上では共通の仕様となるため、他のソフトウェアが COM サーバーとして情報を公開していれば、COM クライアントとして AutoCAD VBA から他のアプリケーションを制御することができます。例えば、Microsoft Office 製品は、自身の機能を COM サーバー として公開しているので、内部の VBA を COM クライアントとして簡単にプログラミングを行うことができます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36a13730970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;">&#0160;</a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80ff364970b-pi" style="display: inline;"><img alt="COM_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80ff364970b image-full img-responsive" src="/assets/image_58999.jpg" title="COM_2" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c36a13730970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a></p>
<p style="padding-left: 30px;">COM サーバーが持つ情報を公開しているのが、タイプ ライブラリ と呼ばれるファイルです。COM クライアントからタイプ ライブラリを参照することで、COM サーバーを参照する手続きが完了します。AutoCAD VBA 内の VBA プロジェクトは、すぐに AutoCAD のオブジェクトを扱えるように、AutoCAD のタイプ ライブラリが最初から参照された状態で作成されます。同じように、Excel VBA のプロジェクトは、Excel のタイプ ライブラリが自動的に参照設定されています。ただし、上図のように、異なるソフトウェアの COM サーバーを参照する場合は、明示的に相手のタイプ ライブラリを参照設定する必要があります。参照設定は、VBA エディタの [ツール] メニューから [参照設定] メニューを選択しておこないます。</p>
<p style="padding-left: 30px;">AutoCAD は、簡単に ActiveX オートメーションを使ったプログラグの作成をおこなえるように、Micsoroft から提供を受けた <strong>VBA</strong>(Visual Basic for Applications) を IDE (統合開発環境) として搭載しています。なお、AutoCAD が VBA を正式に採用したのは、AutoCAD R14 の後期バージョンとなった AutoCAD R14.01 からです。&#0160;</p>
<p style="padding-left: 30px;">VBA では、AutoCAD のカスタム コマンドを直接作成するのではなく、いわゆる VBA マクロ を作成することになります。マクロを実行するためには、VBARUN コマンドを使う必要があります。また、マクロ内で AutoCAD のコマンドをそのまま利用するのではなく、”オブジェクト” に対する操作を処理していく “オブジェクト指向” なプログラミングをおこないます。オブジェクト指向プログラミングは、コマンドや DXF データを扱う従来の AutoLISP とは、構造的に まったく異なる考え方といえます。</p>
<p style="padding-left: 30px;">AutoCAD 2010 から、VBA コンポーネントは <a href="http://www.autodesk.com/vba-download">http://www.autodesk.com/vba-download</a> で無償でダウンロード提供されています。製品のインストール メディア（DVD-ROM や USB）には、VBA コンポーネントは含まれていないため、AutoCAD のインストールと同時に VBA をインストールすることはできません。なお、ダウンロードした VBA は個別インストーラでインストールすることができますが、インストール先の環境に AutoCAD 2010 以降の AutoCAD 製品があらかじめインストールされている必要があります。</p>
<p><strong>ObjectARX</strong></p>
<p style="padding-left: 30px;">AutoCAD R13 から登場した C++ 言語によるオブジェクト指向 API です。AutoCAD R13 からは AutoCAD 自身の開発言語も C++ 言語に切り替わっています。当初、単に ARX (AutoCAD Runtime eXtension の略) と呼ばれていましたが、AutoCAD R14 から名称が変更されて ObjectARX になりました。&#0160; <br />　 <br />ObjectARX を利用するには、ObjectARX SDK を <a href="http://www.autodesk.com/objectarx">http://www.autodesk.com/objectarx</a> からダウンロードして入手する必要があります。入手と利用は無償です。また、C++ 言語で記述したプログラムをコンパイルするために、Microsoft Visual Studio 2010 SP1 を別途購入して、含まれる Visual C++ 2010 を使用する必要があります(AutoCAD 2013 用のアドオン アプリケーション開発環境として)。 <br />　 <br />ObjectARX が持つ C++ クラスライブラリは、一部を除いて、AutoCAD 自身を構成するクラス ライブラリと同一です。このため、他の AutoCAD API 中でも最も強力に AutoCAD を強化、拡張することが可能です。最終生成ファイルである .arx&#0160; ファイルは、拡張子を変更したダイナミック リンク ライブラリ(.dll) です。AutoCAD にロードされた ObjectARX アプリケーション(.arx ファイル) は、実行中の AutoCAD とメモリを共有し、同一プロセスで動作するので、実行スピードが高速になる利点がありますが、反面、ObjectARX アプリケーションの不具合によって、実行中の AutoCAD のメモリ空間を破壊してしまうリスクが伴います。この場合、即 AutoCAD のクラッシュを誘発するため、アプリケーションには高い品質が求められます。&#0160; <br />　 <br />ObjectARX クラス ライブラリは、より広範なカスタマイズができるように、図面アクセスから AutoCAD 固有の MFC UI クラス群まで大規模なものになっています。下図はその一部です。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8448ec4970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;">&#0160;</a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b4b1c2970d-pi" style="display: inline;"><img alt="ObjectARX_Classes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b4b1c2970d image-full img-responsive" src="/assets/image_798258.jpg" title="ObjectARX_Classes" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8448ec4970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a></p>
<p style="padding-left: 30px;">ObjectARX は、アプリケーション固有のカスタム コマンド定義を主体としたカスタマイズも行えます。加えて、ユーザが行った操作をイベントとして取得して、アプリケーション固有の受動処理を実行させるリアクタの実装や、LINE や CIRCLE といった標準オブジェクトと同じように振る舞う カスタム オブジェクト を定義することもできます。また、同じく Visual C++ に添付されているクラス ライブラリである MFC を利用できる点も、ObjectARX の大きな強みとなっています。ObjectARX には、AutoCAD に特化した MFC からの派生クラスが用意されていので、VBA や AutoLISP で実現出来ないパレット形式のダイアログを作成することもできます。</p>
<p style="padding-left: 30px;">ObjectARX アプリケーションで MFC を利用することができますが、使用できる MFC のバージョンは AutoCAD に依存してしまいます。AutoCAD と同じバージョンの MFC ランタイムを使用しないと、予期せぬ結果を招く危険性があります。例えば、AutoCAD 2013 が使用する MFC と同じバージョンの MFC を使用するためには、Visual Studio 2010 + Service Pack1 に含まれる Visual C++ 2010 SP1 を使用しなければなりません。このため、AutoCAD 2013 用の ObjectARX アプリケーション開発には、この Visual C++ 2010 SP1 のみサポートがされています。旧バージョンとなる Visual Studio .NET 2002、2003、Visual Studio 2005 や 2008、2012 は利用することはできません。</p>
<p style="padding-left: 30px;"><strong><strong><span style="color: #888888; font-size: 11pt;"><strong>※</strong></span>ObjectARXの開発者用ガイド</strong></strong>は、Autodesk Knowledge Network &#0160;<strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u04F.html" target="_blank">ObjectARX 2013 開発者用ガイド</a></strong> に記載しています。</p>
<p style="padding-left: 30px;"><strong><span style="color: #ff0000; font-size: 11pt;">※<span style="color: #000000;">ObjectARX</span></span> の学習リソース</strong>は、 Autodesk Knowledge Network&#0160;<strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u047.html" target="_blank">ObjectARX 2013 のトレーニングリソース</a></strong> に記載しています。</p>
<p><strong>.NET API</strong></p>
<p style="padding-left: 30px;">.NET Framework を利用する、AutoCAD 2005 から導入されはじめた新しい API です。Visual Studio を使ってビルドされたアプリケーションは、拡張子 .dll を持つ AutoCAD のロードモジュールとなります.NET API は、ObjectARX の機能を .NET ラッパーとして公開する形をとっているため、ObjectARX とほぼ同等の機能を持っていて、その操作方法も似ています。</p>
<p style="padding-left: 30px;">開発者の視点から見た .NET API の利点は、ObjectARX と異なり、開発環境となる Visual Studio のバージョンに厳密に依存しなくなる点です。AutoCAD 2013 用の ObjectARX 開発では、Visual Studio 2010 SP1 内の Visual C++ しか使用できませんが、.NET API では AutoCAD 2013 が採用する.NET Framework 4 をサポートする Visual Studio 2012 でも開発を行うことができます(プロジェクトのターゲット フレームワーク設定の変更が必要)。また、利用できる開発言語も C++ だけではなく、C# や Visual Basic(VB.NET)、Managed C++ など、Visual Studio に含まれるすべての言語での開発が可能になります。さらに、アプリケーションは、マネージ コードとして実装される .dll ファイルとして動作します。AutoCAD は、.NET API を用いてビルドされた DLL ファイル(アセンブリ) をロードするために、NETLOAD コマンドを提供しています。 <br />　 <br />AutoCAD .NET API は、.NET Framework が持つ豊富な機能を利用できるほかに、いままで ObjectARX でしか実現できなかった高度な AutoCAD カスタマイズを、習得が容易な Visual Basic で実装することが可能なことになります。C++ でポインタを扱うような知識はもう必要ありません。 <br />　 <br />AutoCAD 2013 が提供する .NET API では、メモリ上に展開された図面データベースとオブジェクトへのアクセス、コマンドの登録、 リアクタと同じイベント処理機能など、ObjectARX が持つ機能のほとんどを .NET API でも実現できるようになっています。AutoCAD 2013 の .NET API で操作できないのは、カスタム オブジェクトの定義やデータリスト操作など、ObjectARX でも一部の機能のみです。</p>
<p style="padding-left: 30px;">AutoCAD .NET API は、.NET Framework クラス ライブラリと同様、機能別に 名前空間 を使ってクラス群を公開しています。このクラス群は、ObjectARX の C++ クラスライブラリに類似した階層構造を持ち、非常に膨大なものです。このため、.NET API に関するドキュメントとサンプル プロジェクトは、現在、ObjectARX SDK に同梱する形で提供されています。ObjectARX SDK は、<a href="http://www.autodesk.com/objectarx">http://www.autodesk.com/objectarx</a> から無償でダウンロードして利用することができます。</p>
<p style="padding-left: 30px;">下記は、AutoCAD .NET API が提供する代表的な機能と名前空間です。</p>
<table border="2" cellpadding="2" cellspacing="1" id="table13" style="width: 400px; height: 258px; margin-right: auto; margin-left: auto;">
<tbody>
<tr>
<td align="left" bgcolor="#c0c0c0" valign="top">
<p><span style="font-size: 8pt;"><strong><span style="font-family: Arial;">公開機能</span></strong></span></p>
</td>
<td align="left" bgcolor="#c0c0c0" valign="top">
<p><span style="font-size: 8pt;"><strong><span style="font-family: Arial;">名前空間</span></strong></span></p>
</td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">例外処理など基本機能関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.Runtime</span></td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">ドキュメント、ユーティリティ機能関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.ApplicationServices</span></td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">グラフィカル、非グラフィカル <br />オブジェクト等図面データベース関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.DatabaseServices</span></td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">幾何演算関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.Geometry</span></td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">選択、入力等ユーザ対話関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.EditorInput</span></td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">ユーザ <br />インタフェース関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.Windows</span></td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">印刷サービス関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.PlottingServices</span></td>
</tr>
<tr>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">ベクトル描画機構関連</span></td>
<td align="left" valign="top"><span style="font-family: Arial; font-size: 8pt;">Autodesk.AutoCAD.GraphicsInterface</span></td>
</tr>
</tbody>
</table>
<p style="padding-left: 30px;">ActiveX オートメーションでタイプ ライブラリを使って COM サーバーを参照したように、.NET テクノロジでは、他のアプリケーションやソフトウェアへの機能公開と参照を アセンブリ ファイル を通しておこないます。AutoCAD .NET API を使用するアプリケーションは、使用する開発言語に関係なく、Visual Studio から AutoCAD が提供する 3 つのアセンブリ ファイル、acdbmgd.dll と acmgd.dll、accoremgd.dll を最低でも参照しなければなりません。これらのファイルは、AutoCAD インストール フォルダにインストールされています。AutoCAD 2013 の場合は、既定値で C:\Program Files\AutoCAD 2013 フォルダ直下にインストールされることになります。実際の開発には、32 ビット Windows と 64 ビット Windows のプラットフォーム差に依存しないアセンブリ参照が必要です。プラットフォーム非依存のアセンブリは、ObjectARX SDK に含まれています。 <br />　 <br />もちろん、これら系統化されたクラスの中に更に細分化されたクラスが含まれています。代表的なクラスには次のようなものがあります。それぞれのクラスには、VBA に代表される COM (Component Object Model) と同じように、メソッド、プロパティ、イベント が含まれているので、開発者はそれら適切に呼び出すことでプログラムを完成させることになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40d03a00970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="DotNETNameSpace" class="asset  asset-image at-xid-6a0167607c2431970b017d40d03a00970c" src="/assets/image_405180.jpg" title="DotNETNameSpace" /></a></p>
<p style="padding-left: 30px;"><span style="font-size: 11pt;"><strong><span style="color: #ff0000;"><strong>※<span style="color: #000000; font-size: 10pt;"><strong>AutoCAD .NET API </strong>の開発者用ガイド</span></strong></span></strong><span style="color: #ff0000;"><span style="color: #000000; font-size: 10pt;">は、</span></span><span style="color: #ff0000;"><span style="color: #000000; font-size: 10pt;">AutoCAD のオンライン ヘルプの <strong><a href="http://docs.autodesk.com/ACD/2013/JPN/files/GUID-390A47DB-77AF-433A-994C-2AFBBE9996AE.htm" target="_blank">こちら</a></strong> から参照することができます。</span></span></span></p>
<p style="padding-left: 30px;"><span style="font-size: 11pt;"><strong><span style="color: #ff0000;">※<span style="color: #000000; font-size: 10pt;"><strong>AutoCAD .NET API </strong></span></span></strong></span><strong><span style="font-size: 10pt;">の</span>学習リソース</strong>は、Autodesk Knowledge Network <strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u04j.html" target="_blank">AutoCAD 2013 .NET API のトレーニングリソース</a></strong> に記載しています。</p>
<p>By Toshiaki Isezaki</p>
