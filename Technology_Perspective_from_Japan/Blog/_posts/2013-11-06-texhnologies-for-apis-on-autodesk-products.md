---
layout: "post"
title: "オートデスク製品の API が利用するテクノロジ"
date: "2013-11-06 02:17:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html "
typepad_basename: "texhnologies-for-apis-on-autodesk-products"
typepad_status: "Publish"
---

<p>今回はオートデスクの主要デスクトップ CAD 製品である AutoCAD、Inventor、Revit に焦点を当てて、各 API が利用しているテクノロジを比較してみたいと思います。これらの製品は、歴史の古い順から並べると、AutoCAD → Inventor → Revit となります。API の登場は、各製品が登場した時代背景とも密接にかかわっているので、特に AutoCAD の場合など、これから API を使ってアプリケーションの開発を検討されている場合には、どういったテクノロジを使っているかを知ることで、API 選定の参考になるかと思います。</p>
<p>まず、API のベース テクノロジにどのようなものが存在するのか、簡単なスライドを示します。ここでは、<strong>ソフトウェア内実装</strong>、<strong>C++ ライブラリ</strong>、<strong>COM</strong>、<strong>.NET</strong> の4 種類が存在することがわかります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00a5472a970d-pi" style="display: inline;"><img alt="API_Technologies" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b00a5472a970d image-full" src="/assets/image_646138.jpg" title="API_Technologies" /></a></p>
<p>ベース テクノロジにも、歴史があります。パソコン用ソフトウェア用にテクノロジが標準されていなかった20年くらい前、実装はソフトウェア開発ベンダーが独自に開発していて、互いに競い合った時代がありました。その当時の形態が&#0160;<strong>ソフトウェア内機能</strong>&#0160;の実装形態です。 この方法は、開発環境だけでなく、開発言語も独自に開発されていたため、その利用は製品利用者に限定されてしまい、普及は思うように進みませんでした。</p>
<p>時期をほぼ同じくして、エンジニアリング ワークステーション（UNIX）やパソコン（DOS）の普及に伴ない、大型コンピュータやミニコン用の CAD アプリケーション開発で主流だった <a href="http://ja.wikipedia.org/wiki/Fortran" target="_blank">FORTRAN</a> から、C 言語や <a href="http://ja.wikipedia.org/wiki/C%2B%2B" target="_blank">C++言語</a>&#0160;が主流になりました。C/C++ 言語は、<a href="http://ja.wikipedia.org/wiki/ANSI" target="_blank">ANSI</a>&#0160;によって標準化作業が進んでいたたため、CAD 以外の開発者にも把握しやすい開発言語です。実際には、ソフトウェア毎に SDK という形で <strong><a href="http://ja.wikipedia.org/wiki/%E3%83%A9%E3%82%A4%E3%83%96%E3%83%A9%E3%83%AA" target="_blank"><strong>C++ ライブラリ</strong></a></strong> を提供して利用する方法がとられました。</p>
<p>その後、Microsoft Windows がビジネスの世界に浸透することで、Microsoft 社のテクノロジが主流になってきました。そして、その頃登場したのが <strong><a href="http://ja.wikipedia.org/wiki/Component_Object_Model" target="_blank"><strong>COM（Component Object Model）</strong></a></strong> です。登場当初は、OLE オートメーション と呼ばれていて、その後 ActiveX オートメーションと呼ばれた時期もありました（厳密には少々差がありますが）。COM テクノロジを採用したソフトウェアは、同じテクノロジをベースにして互いの機能をコンポーネントと言う単位で公開、再利用することが可能になったため、Windows 上ではソフトウェアを連携させて動作するソフトウェア/アプリケーションが登場して、利便性や生産性が以前に比べると格段に向上した時期でもあります。</p>
<p>ソフトウェア連携が得意な COM テクノロジは、企業内システムを構築する上で多大な貢献を果たします。ところが、そんな時期に COM の弱点が見出されます。インターネットの登場です。企業内の閉じたネットワークだけでなく、オープンなインターネット環境で簡単、かつ、セキュアに利用できるテクノロジの策定が急務になり、そこで Microsoft 社が登場させたのが <a href="http://ja.wikipedia.org/wiki/.NET" target="_blank"><strong>.NET</strong></a> です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00a50cfe970b-pi" style="display: inline;"><img alt="API_Tech_History" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b00a50cfe970b image-full" src="/assets/image_978505.jpg" title="API_Tech_History" /></a></p>
<p>次に、それぞれのデスクトップ製品とその API を図式化したスライドをご覧ください。最初は AutoCAD です。</p>
<p><strong>AutoCAD</strong></p>
<p>ご存じのように、オートデスクの設立が AutoCAD の開発・販売を目的としたものであっため、AutoCAD がオートデスク製品の中で最も歴史のある製品となります。最新の AutoCAD 2014 では、5 つの API が用意されていて、 AutoCAD を利用したアプリケーション開発ができます。AutoLISP/Visual LISP の開発用エディタには、Visual LISP エディタが用意されています。また、Microsoft 社からライセンス提供を受けている VBA エディタ環境もダウンロードしてインストールすることで、<a href="http://ja.wikipedia.org/wiki/Visual_Basic_for_Applications" target="_blank">VBA</a> マクロを開発することもできます。ObjectARX や AutoCAD .NET API をつかった開発作業には、Microsoft 社の Visual Studio を別途用意していただく必要があります。ObjectARX の開発用にのみ、<a href="http://www.autodesk.com/objectarx" target="_blank">http://www.autodesk.com/objectarx</a> から ObjectARX SDK がダウンロード提供されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00a4daf1970d-pi" style="display: inline;"><img alt="AutoCAD_API" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b00a4daf1970d image-full" src="/assets/image_691512.jpg" title="AutoCAD_API" /></a></p>
<p><strong>Inventor</strong></p>
<p>アドオン アプリケーションの他、<a href="http://ja.wikipedia.org/wiki/Visual_Basic_for_Applications" target="_blank">VBA</a> エディタによるマクロ開発環境も提供しています。リボン インタフェースをカスタマイズしたい場合には、アドオン アプリケーションを開発する必要があります。VBA マクロからのリボン インタフェース カスタマイズはサポートされていません。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00a50a19970d-pi" style="display: inline;"><img alt="Inventor_API" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b00a50a19970d image-full" src="/assets/image_989867.jpg" title="Inventor_API" /></a></p>
<p><strong>Revit</strong></p>
<p>デスクトップ CAD 製品としては比較的新しい製品です。API の実装も、製品の歴史に比べると、最近実装されたものであることがわかります。他の製品と同様にアドオン アプリケーション開発が可能で、Revit SDK が製品に同梱されてます。また、Revit SDK は、<a href="http://www.autodesk.com/developrevit" target="_blank">http://www.autodesk.com/developrevit</a> からダウンロードすることも出来ます。アドオン アプリケーションの他にも、<a href="http://ja.wikipedia.org/wiki/SharpDevelop" target="_blank">SharpDevelop</a> と言われるオープンソースの開発環境が組み込まれていて、マクロを開発することも出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00a4e597970b-pi" style="display: inline;"><img alt="Revit_API" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b00a4e597970b image-full" src="/assets/image_775680.jpg" title="Revit_API" /></a></p>
<p>最後に、紹介したオートデスク製品の API が、どのテクノロジを使っているのかお知らせしましょう。次がその答えとなります。</p>
<ul>
<li><strong><span style="font-size: 11pt;">ソフトウェア内機能</span></strong></li>
</ul>
<p style="padding-left: 60px;"><strong><span style="font-size: 11pt;">AutoCAD AutoLISP</span></strong></p>
<ul>
<li><strong><span style="font-size: 11pt;">C++ ライブラリ</span></strong></li>
</ul>
<p style="padding-left: 60px;"><strong><span style="font-size: 11pt;">AutoCAD ObjectARX</span></strong></p>
<ul>
<li><strong><span style="font-size: 11pt;">COM（Component Object Model）</span></strong></li>
</ul>
<p style="padding-left: 60px;"><strong><span style="font-size: 11pt;">AutoCAD COM API</span></strong></p>
<p style="padding-left: 60px;"><strong><span style="font-size: 11pt;">Inventor API</span></strong></p>
<ul>
<li><strong><span style="font-size: 11pt;">.NET</span></strong></li>
</ul>
<p style="padding-left: 60px;"><strong><span style="font-size: 11pt;">AutoCAD .NET API</span></strong></p>
<p style="padding-left: 60px;"><strong><span style="font-size: 11pt;">Revit API</span></strong></p>
<p>テクノロジといった側面で違った視点で見てみると、AutoCAD の歴史が際立つことにもなります。Inventor や Revit は、前者が COM、後者は.NET で選択肢はありませんが、.NET は COM との親和性が高くシームレスに実装可能なので、Inventor アドオンの開発は、Visual Studio を使って .NET 環境で行うことになります。</p>
<p><strong>API による API の拡張</strong></p>
<p>ここまで、テクノロジの視点で主要 CAD 3 製品の API を見てきましたが、最後にそれぞれの拡張性について言及しておきたいと思います。ここで言う拡張性とは、オートデスク製品に用意された標準&#0160;API に、利用したい機能が含まれていない場合の拡張性を示します。</p>
<p>AutoCAD の場合、C++ ライブラリとして AutoCAD 本体の開発に用いるクラス ライブラリを一部切り出した ObjectARX 環境を提供しているため、AutoCAD の COM API や .NET API に搭載されていない機能を、<a href="http://msdn.microsoft.com/ja-jp/library/5dxz80y2(v=vs.110).aspx" target="_blank"><strong>COM ラッパー</strong></a> や .NET ラッパーを作成して補うことが出来ます。別の言い方をすれば、ObjectARX でしか実現できない機能をラッパーを介して別の&#0160;API として公開することで、COM や .NET 環境でも、その機能を利用することが出来るようになります（クラスやメソッド、プロパティを独自に用意できる）。この点では、ObjectARX が持つネイティブな C++ （アンマネージ C++）の柔軟性と強みを活かせます。</p>
<p>一報、最初から COM API や .NET API で用意されている Inventor API や Revit API では、AutoCAD のような拡張は望めません。Inventor API と Revit API で最初から用意されていない機能は、C++ レベルから拡張することは出来ないのです。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
