---
layout: "post"
title: "AutoCAD .NET API の利点"
date: "2013-12-18 00:11:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/12/benefit-by-autocad-dotnet-api.html "
typepad_basename: "benefit-by-autocad-dotnet-api"
typepad_status: "Publish"
---

<p><span style="font-family: Arial; font-size: small;">AutoCAD .NET API が利用している .NET Framework は、Microsoft 社が 2000 年に提唱し始めたたテクノロジです。現在販売されている Windows OS には、あらかじめ OS に組み込まれて提供されていますが、Windows 2000 やリリース直後の Windows XP など、古い OS には .NET Framework は組み込まれていませんでした。この .NET Framework を説明する際には、次の 2 つに分けて解説するのが理解しやすいと思います。</span></p>
<ol>
<li><strong style="font-family: Arial;">実行環境としての .NET Framework</strong></li>
<li><strong style="font-family: Arial;">機能提供者としての .NET Framework</strong></li>
</ol>
<p>今回は、「いまさら聞けない AutoCAD .NET API」的に、.NET Framework の仕組みや AutoCAD アドオン開発で利用するメリットを、先の 2 つの役割別にご紹介したいと思います。</p>
<p><strong>実行環境としての .NET Framework</strong></p>
<p style="padding-left: 30px; text-align: left;">.NET Framework で動作するソフトウェアやアドオン アプリケーションを開発するには、Microsoft 社が販売する Visual Studio の使用が前提になります。.NET Framework を利用する AutoCAD .NET API では、Visual Studio を使って AutoCAD にロードする .dll ファイルを最終的に作成します。Visual Studio では、DLL のほかにも、独立した実行ファイル(拡張子 .exe) を作成することができます。 <br />　 <br />作成したソフトウェアやアドオン アプリケーションを実行する OS には、必ず、開発時に使用した .NET Framework バージョンがインストールされている必要があります。もし、実行環境に .NET Framework がインストールされていないと、次のようなエラーが表示されるはずです。もちろん、実行は出来ません。 <br />　 <br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b02382b63970b-pi" style="display: inline;"><img alt="Execution_error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b02382b63970b image-full img-responsive" src="/assets/image_717404.jpg" title="Execution_error" /></a></p>
<p style="padding-left: 30px; text-align: left;">Visual Studio 2010 がインストールされている環境には、.NET Framework 4 が同時にインストールされています。同様に、AutoCAD 2014 がインストールされている環境にも .NET Framework 4 がインストールされているので、AutoCAD .NET API で作成した .dll ファイルの実行に支障はありません。独立した実行ファイルを作成した場合などは、配布先となるコンピュータに .NET Framework がインストールされているかチェックしておく必要があります。&#0160;　&#0160;</p>
<p style="padding-left: 30px; text-align: left;">Visual Studio 2010 で作成した .dll や .exe ファイルは、<strong>アセンブリ ファイル</strong>と呼ばれています。アセンブリは、ActiveX オートメーション(COM) のタイプライブラリのように、他のアプリケーションに自身のもつ機能を外部に公開する機能をもっています。AutoCAD は、この機能を使って AutoCAD .NET API の機能を公開しています。</p>
<p style="padding-left: 30px;"><strong>マネージ コード と アンマネージ コード</strong></p>
<p style="padding-left: 30px; text-align: left;">NET Framework は、オペレーティング システム(OS) に抽象処理層で、これが、実行環境としての .NET Framework の実体です。Visual Studio で .NET Framework を利用して作成されたアプリケーションは、実行時にこの中間処理層によって実装内容が解釈されて実行に移されます。逆に、Visual Basic 6.0 や Visual C++ 6.0 などで作成した古いアプリケーションは、直接 OS が解釈できるバイナリで構成されているので、実行時に .NET Framework を必要としていません。&#0160;</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b033536cb970d-pi" style="display: inline;"><img alt="2013-12-18 17-08-36" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b033536cb970d image-full img-responsive" src="/assets/image_734776.jpg" title="2013-12-18 17-08-36" /></a></p>
<p style="padding-left: 30px; text-align: left;">.NET Framework を使うアプリケーションと使わないアプリケーションを言葉で表現するため、実行時に ,NET Framework を必要とする最近のアプリケーションを&#0160;<strong>マネージ コード</strong>(マネージ アプリケーション)&#0160;と呼んでいます。同じように、.NET Framework に管理されない古いタイプのアプリケーションを<strong>&#0160;アンマネージ コード</strong>(アンマネージ アプリケーション)&#0160;と呼ぶことがあります。どちらも実行ファイルとして同じ拡張子 .exe や .dll などを持つようになるため、一見すると区別がつきません。&#0160;　&#0160;</p>
<p style="padding-left: 30px; text-align: left;">現在の Windows 上で動作しているソフトウェアやアドオン アプリケーションは、マネージ コードとアンマネージ コードが混在した状態と推測できます。AutoCAD アドオン アプリケーションの場合も、ObjectARX アドオンはアンマネージコード、AutoCAD .NET API で作成したアドオンはマネージ コードとなるので、両者が混在しています。&#0160;</p>
<p style="padding-left: 30px;"><strong>アセンブリの実行メカニズム</strong></p>
<p style="padding-left: 30px;">実は、Visual Studio などで作成されたマネージ コードには、従来のように CPU が直接解釈するような命令は含まれていません。Visual Studio に含まれる開発言語は、標準で C++、VB.NET、C#、などがありますが、どの開発言語からビルドしたアセンブリにも、内部に記述されているのは中間言語(IL コード) での命令になります。この中間コードは、実際の実行時に .NET Framework に含まれる <strong>共通言語ランタイム</strong>(Common Language Runtime、略して CLR) によって解釈されて、Just-In-Time コンパイラが CPU が解釈できる命令群に変換して実行に移されます。</p>
<p style="padding-left: 30px;"><span style="text-align: center;">1. Visual Studio でプロジェクトをビルドしてアセンブリ(.dll、.exe) を生成</span></p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b023b1cdc970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Build_phase" class="asset  asset-image at-xid-6a0167607c2431970b019b023b1cdc970d" src="/assets/image_326828.jpg" style="width: 400px;" title="Build_phase" /></a><br style="text-align: center;" /><br style="text-align: center;" /></p>
<p style="padding-left: 30px;">2.実行時に中間コード(IL コード) が Just-In-Time コンパイラによって CPU 命令に変換されて実行</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b023a9e69970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Execution_phase" class="asset  asset-image at-xid-6a0167607c2431970b019b023a9e69970b" src="/assets/image_950342.jpg" style="width: 400px;" title="Execution_phase" /></a></p>
<p style="padding-left: 30px;">ここまでの説明でお分かりのご理解いただける思いますが、.NET API で作成した AutoCAD アドオンは、中間コードで構成されたアセンブリです。AutoCAD にロードされて最初に実行される際に、JIT コンパイルされて初めて実行可能な状態になります。逆に言えば、中間コード形式のアセンブリは、32 ビット版の AutoCAD でも、64 ビット版の AutoCAD でもロードして実行することが出来る、プラットフォーム非依存のアドオン モジュールなのです。</p>
<p style="padding-left: 30px;">ObjectARX の場合には、Visual Studio 上の開発段階のソースコードと、ビルドされたモジュールが、完全にプラットフォーム依存になってしまいます（ソースコードはある程度共通化することが出来ますが）。アドオンの利用者が使うコンピュータに 32 ビット版と 64&#0160;ビット版が混在している場合には、配布前に調査をする必要があり、ソースコード管理も含めて、極めて煩雑な管理を求められることになります。</p>
<p style="padding-left: 30px;">※ 64 ビット版 AutoCAD に 32 ビット版用 ObjectARX アドオンをロードすることは出来ません（その逆も）。</p>
<p><strong>機能提供者としての .NET Framework</strong></p>
<p style="padding-left: 30px;">.NET Framework は、オペレーティング システムが持つ機能を開発者に開発ライブラリ提供する機能も持っています。ライブラリは .NET Framework クラス ライブラリという形で公開されていて、すべての機能がクラス単位でまとめられています。.NET Framework クラス ライブラリは非常に膨大なので、すべてを説明することはできませんが、大まかに次の項目に分けることができます。</p>
<ul>
<li>データ型、属性、数値演算など基本機能</li>
<li>データベース (ADO.NET) アクセス関連機能</li>
<li>Web サービスを含む Web 関連機能</li>
<li>XML 処理関連機能</li>
<li>グラフィックス関連機能</li>
<li>Windows フォームなど UI 関連機能</li>
</ul>
<p style="padding-left: 30px;">.NET では、これら大規模な機能を公開するにあたって、名前空間 という考え方を導入しています。名前空間は、機能別に公開されるクラスを系統化してわかりやすくするものです。上記の機能は、次の名前空間から参照することができます。 　</p>
<p style="padding-left: 30px; text-align: center;">&#0160;<img alt="Class_Table" class="asset  asset-image at-xid-6a0167607c2431970b019b023bad2a970d" src="/assets/image_803093.jpg" style="width: 400px;" title="Class_Table" />&#0160;</p>
<p style="padding-left: 30px;">もちろん、これら系統化されたクラスの中に、更に細分化されたクラスが含まれています。代表的なクラスには下記のようなものがあります。それぞれのクラスには、VBA に代表される COM (Component Object Model) と同じように、メソッド、プロパティ、イベント が含まれているので、それらを開発者が適切に呼び出すことで、プログラムを完成させることができます。　&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b023b8b6d970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Class_library" class="asset  asset-image at-xid-6a0167607c2431970b019b023b8b6d970d" src="/assets/image_775498.jpg" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" title="Class_library" /></a></p>
<p style="padding-left: 30px;">AutoCAD .NET API を使ったアプリケーションからも、.NET Framework クラス ライブラリが提供する機能を利用することができます。また、AutoCAD .NET API も .NET Framework クラス ライブラリと同じように名前空間を使った クラス ライブラリを提供しています。このクラス ライブラリは、ObjectARX の C++ クラス階層に沿ったもので、VBA が使用する ActiveX オートメーション(COM) のオブジェクト階層とは異なるものです。&#0160;&#0160;</p>
<p>.NET Framework を利用することで実現できた拡張性と移植性の高い AutoCAD API が、AutoCAD .NET API です。VB.NET やC#、C++ といった、開発言語の選択肢も魅力です。また、COM との親和性も高いので、AutoCAD や Excel の COM API を呼び出すことも出来ます。これから、AutoCAD アドオンを開発する場合には、AutoCAD .NET API は、最良の選択肢と言えます。</p>
<p><strong>AutoCAD 自身も .NET Framework を利用</strong></p>
<p>AutoCAD が .NET Framework を利用するようになったのは、AutoCAD .NET API が導入された AutoCAD 2005 からです。その後も同梱する .NET Framework のバージョンを更新しながら、現在に至っています。最近では、.NET Framework の一部である Windows Presentation Foundation（WPF）を利用したユーザ インタフェースも積極的に導入しています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b02853d9b970d-pi" style="display: inline;"><img alt="DotNETinAutoCAD" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b02853d9b970d image-full img-responsive" src="/assets/image_784761.jpg" title="DotNETinAutoCAD" /></a></p>
<p>.NET Framework の導入は、ユーザ インタフェースだけでなく、AutoCAD の機能自体にも及んでいます。例えば、Layer コマンドで表示されるパレット形式の [画層プロパティ管理] ダイアログの機能や、CUI コマンドで利用する [ユーザ インタフェースをカスタマイズ] ダイアログとその機能などです。これらは、個別のアセンブリとして提供されているので、AutoCAD 起動直後に LAYER コマンドや CUI コマンドを実行すると、先に紹介した Just In Time コンパイラによってバイナリにコンパイルされる処理が実行されます。2 度目以降のコマンド実行時には、キャッシュが実行されることになるので、最初の実行のみ、ダイアログの表示までに少しタイムラグを感じるはずです。この振る舞いも、AutoCAD が .NET Framework を利用している証と言えます。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
