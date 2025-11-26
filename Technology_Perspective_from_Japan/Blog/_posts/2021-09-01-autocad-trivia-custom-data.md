---
layout: "post"
title: "AutoCAD 雑学：カスタムデータと図面ファイル保存"
date: "2021-09-01 00:00:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/09/autocad-trivia-custom-data.html "
typepad_basename: "autocad-trivia-custom-data"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeed9c08200c-pi" style="display: inline;"><img alt="Custom_data2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeed9c08200c image-full img-responsive" src="/assets/image_720616.jpg" title="Custom_data2" /></a></p>
<p>AutoCAD の図面ファイルは、幾何情報（図形）を持つデータベースに例えることが出来ます。目に見える図形だけでなく、背後にあるさまざまな情報と<span style="text-decoration: underline;">つながり</span>合うことで、図面情報が維持されています。この「つながり」については、<a href="https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener" target="_blank"><strong>AutoCAD API から見た図面構造と破損</strong></a> でもご案内していますのでご確認ください。</p>
<p>AutoCAD でアドイン アプリケーションを運用していく際には、特定のオブジェクトに固有のデータを付加して、どこかのタイミングで再利用したい場面が多くあります。いわゆる「カスタム データ」の利用、ということになりますが、AutoCAD API で付加した独自のカスタム データは、図面内の「つながり」によって管理されることになるので、カスタム データの付加後に図面ファイルに保存して、再び図面ファイルを開いても、付加したカスタム データを取り出して再利用することが出来るようになっています。</p>
<p>それでは、図面ファイルに保存されるカスタム データには、どのようなものがあるか見ていきたいと思います。</p>
<p>カスタム データを利用する場合、ブロック属性、カスタム オブジェクト、拡張エンティティ データ、ディクショナリの内から選択することが可能です。それぞれの概要、利点と欠点、Autodesk Knowledge Network 時期に記載している AutoCAD .NET API サンプルは次のとおりです。</p>
<hr />
<p><strong>ブロック属性　</strong></p>
<p style="padding-left: 40px;">BLOCK[ブロック登録] コマンドでブロック定義を作成する際に、ATTDEF[属性定義] コマンドで作図してブロック定義に加えると、ブロック参照としてモデル空間やレイアウトに挿入する際、属性値を指定して配置することが出来ます。1 つのブロック定義に複数の属性値を持たせりことが出来るだけでなく、集計などで活用することが可能です。</p>
<p style="padding-left: 40px;"><strong>&lt;ブロック属性の<span style="color: #0000ff;">利点</span>と<span style="color: #ff0000;">欠点</span>&gt;</strong></p>
<p style="padding-left: 80px;"><span style="color: #0000ff;">✔</span> ユーザ操作で属性を付加することができる<br /><span style="color: #ff0000;">✘</span> ユーザ操作で属性を消されてしまう可能性がある<br /><span style="color: #ff0000;">✘</span> 付加できるのはブロック単位のみでバイナリデータの扱いは不可能</p>
<p style="padding-left: 40px;"><strong>&lt;Autodesk Knowledge Network 記事にある AutoCAD .NET API 例&gt;</strong></p>
<p style="padding-left: 80px;"><strong><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/660B2qacg1uAuqlIrcjwt2.html" rel="noopener" target="_blank">AutoCAD .NET API ：属性付きブロック定義とブロック参照の挿入</a></strong></p>
<p style="padding-left: 80px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/6ixWQiy7IpfnKaTEbL7XN9.html" rel="noopener" target="_blank"><strong>AutoCAD .NET API ：ブロック参照の属性値の変更</strong></a></p>
<hr />
<p><strong>カスタム オブジェクト</strong></p>
<p style="padding-left: 40px;">カスタム オブジェクト とは、C++ 言語のクラス継承を用いて、ObjectARX アプリケーションによって定義された独自のオブジェクトを指します。</p>
<p style="padding-left: 40px;">AutoCAD の標準オブジェクトのように振る舞うようになり、図面ファイルにも保存されるので、データ コンテナとしての活用が出来ます。目に見える図形オブジェクトとしても、目に見えないオブジェクトしても定義が可能ですカスタム オブジェクトの定義時には、内包するカスタム データを自由に定義することが可能です。</p>
<p style="padding-left: 40px;">残念ながら、AutoCAD .NET API を含め、他の AutoCAD API ではカスタム オブジェクトを<span style="text-decoration: underline;">定義</span>することが出来ません。</p>
<p style="padding-left: 40px;"><strong>&lt;カスタム オブジェクトの<span style="color: #0000ff;">利点</span>と<span style="color: #ff0000;">欠点</span>&gt;</strong></p>
<p style="padding-left: 80px;"><span style="color: #0000ff;">✔</span>&#0160;あらゆるデータを内包できる自由度の高いオブジェクト作成が可能<br /><span style="color: #0000ff;">✔</span>&#0160;ユーザ操作で内包するデータだけが削除されてしまう恐れはない<br /><span style="color: #0000ff;">✔</span>&#0160;オブジェクトの振る舞い（動作、挙動）も同時に定義することができる<br /><span style="color: #0000ff;">✔</span>&#0160;グラフィカル オブジェクトと非グラフィカル オブジェクトの両者を実装可能<br /><span style="color: #ff0000;">✘</span> ObjectARX でしか定義できない（AutoCAD .NET API のみの実現は不可能）<br /><span style="color: #ff0000;">✘ <span style="color: #000000;">カスタム オブジェクトの認識には定義した ObjectARX アプリのロードが必須。<br /></span></span><span style="color: #ff0000;">✘<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/03/migrate-autocad-api-addon-apps.html" rel="noopener noreferrer" target="_blank">移植性の問題</a>&#0160;</strong>から、<span style="color: #000000;">メインテナンスに手間がかかる</span></span></p>
<p style="padding-left: 40px;">ObjectARX を利用すると、後述する Named Object Dictionary に格納するディクショナリ要素にカスタム オブジェクトを充てることができます。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/06/autocad-2022-objectarx-training-material.html" rel="noopener" target="_blank"><strong>AutoCAD 2022 ObjectARX トレーニング マテリアル</strong></a> では、目に見える図形（カスタム エンティティ）と、目に見えないカスタム オブジェクトを利用した例を実装方法とともに詳しくご案内しています。この例では、カスタム オブジェクト MYDICTIONARY を格納する ディクショナリを不変リアクタとして利用して、関連付けたオブジェクト（下記の例では上部の図形）の色を変更すると、カスタム エンティティ ASDKMYCIRCLE の色も連動して変化する実装を扱っています。これらのオブジェクトの「つながり」（<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener noreferrer" target="_blank">ソフトポインタ参照</a></strong>）は図面ファイルに保存され維持されることになります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e11cc539200b-pi" style="display: inline;"><img alt="Prsistent_reactor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e11cc539200b image-full img-responsive" src="/assets/image_312531.jpg" title="Prsistent_reactor" /></a></p>
<hr />
<p><strong>拡張エンティティ データ</strong></p>
<p style="padding-left: 40px;">拡張エンティティ データは、目に見える図形オブジェクトにも、目に見えないオブジェクトにも、固有のカスタム データの付加、参照する手法を提供します。</p>
<p style="padding-left: 40px;">AutoCAD の標準コマンドは、付加された拡張エンティティ データを認識しないので、標準ユーザ インタフェースに値が表示されることはありませんが、逆に、ユーザから付加された固有データを隠蔽することが可能です。また、プログラムを介在させない限り、拡張エンティティ データを削除したり、値を変更したりすることができないので、ユーザによる改変を防止することもできます。</p>
<p style="padding-left: 40px;"><strong>&lt;拡張エンティティ データの<span style="color: #0000ff;">利点</span>と<span style="color: #ff0000;">欠点</span>&gt;</strong></p>
<p style="padding-left: 80px;"><span style="color: #0000ff;">✔</span>&#0160;バイナリを含むあらゆる情報をあらゆるオブジェクトに付加できる<br /><span style="color: #0000ff;">✔</span>&#0160;ユーザ操作でデータが削除されてしまう恐れはない<br /><span style="color: #ff0000;">✘</span> 1 オブジェクトへの付加サイズ総量が 16 KByte を越えられない</p>
<p style="padding-left: 40px;"><strong>&lt;Autodesk Knowledge Network 記事にある AutoCAD .NET API 例&gt;</strong></p>
<p style="padding-left: 80px;"><strong><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/VsSfI9Igoxd6kXjfDIFB3.html" rel="noopener" target="_blank">AutoCAD .NET API ：拡張エンティティ データの付加・取得・削除</a></strong></p>
<hr />
<p><strong>ディクショナリ</strong></p>
<p style="padding-left: 40px;">AutoCAD のディクショナリには、拡張ディクショナリと Named Object Dictionary の２種類が用意されています。いずれの場合も XRecord オブジェクトにリザルトバッファを割り当ててカスタム データを扱うことになります。</p>
<p style="padding-left: 40px;">拡張エンティティ データと比べると、文字列を除いて<span style="text-decoration: underline;">実質</span>、サイズ制限がないに等しいので、大規模なカスタム データの運用に向いています。</p>
<p style="padding-left: 40px;">単に文字列や実数、整数などのデータを維持するだけでなく、「つながり」（<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener noreferrer" target="_blank">ソフトポインタ参照</a></strong>）を利用した情報を維持したり、独自の画像データを格納することが出来るので、ObjectARX より容易に、柔軟なデータ運用が可能です。</p>
<p style="padding-left: 40px;"><strong>&lt;ディクショナリの<span style="color: #0000ff;">利点</span>と<span style="color: #ff0000;">欠点</span>&gt;</strong></p>
<p style="padding-left: 80px;"><span style="color: #0000ff;">✔</span>&#0160;バイナリを含むあらゆる情報をあらゆるオブジェクトに付加できる<br /><span style="color: #0000ff;">✔</span>&#0160;ユーザ操作でデータが削除されてしまう恐れはない<br /><span style="color: #0000ff;">✔</span>&#0160;1 オブジェクトへの付加サイズに実質総量制限はない（オブジェクト全体で4GByte）<br /><span style="color: #ff0000;">✘</span> バイナリ データは 127 Bytes 単位に分割しなければならない<br /><span style="color: #ff0000;">✘</span> 文字列領域 1 つに保存可能な最大文字数は 65535 文字</p>
<p style="padding-left: 40px;"><strong>&lt;Autodesk Knowledge Network 記事にある AutoCAD .NET API 例&gt;</strong></p>
<p style="padding-left: 80px;"><strong><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/5ESdLAImQUKP2TSy4G7dl7.html" rel="noopener" target="_blank">AutoCAD .NET API ：拡張ディクショナリの付加・取得・削除</a></strong></p>
<p style="padding-left: 80px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/6W8k9hwQZMLOIo6475T3ZW.html" rel="noopener" target="_blank"><strong>AutoCAD .NET API ：画像データの図面への格納</strong></a></p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278804521ac200d-pi" style="display: inline;"><img alt="Images" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278804521ac200d image-full img-responsive" src="/assets/image_135534.jpg" title="Images" /></a></p>
<p style="padding-left: 80px;"><strong><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/50cXUNbSGm44JPZkA5VO2H.html" rel="noopener" target="_blank">AutoCAD .NET API ：選択セットのセッション間の維持</a></strong></p>
<p style="padding-left: 80px;"><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788045228c200d-pi" style="display: inline;"><img alt="Selection_state" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788045228c200d image-full img-responsive" src="/assets/image_160413.jpg" title="Selection_state" /></a><br /></strong></p>
<hr />
<p>By Toshiaki Isezaki</p>
