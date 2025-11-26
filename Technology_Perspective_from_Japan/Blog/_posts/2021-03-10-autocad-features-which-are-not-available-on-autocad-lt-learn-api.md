---
layout: "post"
title: "AutoCAD LT にない AutoCAD 機能：API の習得"
date: "2021-03-10 00:14:57"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/03/autocad-features-which-are-not-available-on-autocad-lt-learn-api.html "
typepad_basename: "autocad-features-which-are-not-available-on-autocad-lt-learn-api"
typepad_status: "Publish"
---

<p>あと数年で 40 年を迎える AutoCAD。その歴史の中で、AutoCAD を拡張するために用意されている <strong>API</strong>（<strong>A</strong>pplication <strong>P</strong>rogramming <strong>I</strong>nterface）も継続して進化を続けています。</p>
<p style="padding-left: 40px;">※ AutoCAD 2021 は誕生から 38 年目の製品です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec9f959200c-pi" style="display: inline;"><img alt="Api_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec9f959200c image-full img-responsive" src="/assets/image_464604.jpg" title="Api_history" /></a></p>
<p>カスタムコマンドの作成から業務アプリケーションとのデータ連携まで、設計業務を取り巻く環境で強みを発揮しますが、その習得には少し時間が必要かと思います。なぜなら、プログラム知識の他に、一定程度、AutoCAD の機能や操作上の振る舞いを把握していないと、使いにくいコマンドを作ってしまう原因にもなるためです。</p>
<p>「AutoCAD の機能を知らずに、AutoCAD の標準コマンドと同じ機能を持つカスタムコマンドを API で作ってしまった」、といった例もあります。また、「後年の AutoCAD に使い勝手のよい機能が追加されても、古い時期に API で開発した同等機能のカスタマイズ資産を使い続けている」、といった例も少なからず耳にすることがあります。</p>
<p>例えば、部品表を作図する機能は、<a href="https://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-39165928-A505-464B-8536-5817BA9DEC27" rel="noopener" target="_blank"><strong>データ書き出し</strong></a>（AutoCAD LT では利用不可）で実現することが出来ます。データ書き出しが自社業務に合わない場合には、独自のルールを盛り込むカタチで、API を使って表オブジェクトを作図するカスタムコマンドを作ってしまうことも出来ます。表オブジェクトを使えば、表の<a href="https://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-CE2CC84F-944A-4EE6-B824-9B81692F5B7B" rel="noopener" target="_blank"><strong>編集</strong></a>や<a href="https://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-A4B8BDF9-5DDA-48F6-B535-B8801A66871E" rel="noopener" target="_blank"><strong>更新</strong></a>が容易になったり、<strong><a href="https://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-505EF5BB-5347-43DA-91A6-03141A9134C8" rel="noopener" target="_blank">計算式</a></strong>を埋め込むなど、高度な運用も可能になります。AutoCAD で API を提供するのは AutoCAD の機能を補っていただくのが目的なので、やはり、定期的な運用の見直しは必要かと思います。</p>
<p>それでは、<strong>どういった方法で API を使ったカスタマイズに、習得、着手していけばいいのでしょう？</strong></p>
<p>AutoCAD 自体をあまりご存じない場合には、<strong><a href="https://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-7AE6809D-BEC7-4E86-A19E-3D496F0F44BD" rel="noopener" target="_blank">OPTIONS[オプション]</a></strong> コマンドで AutoCAD の設定項目を確認してみてください。ソフトウェア全般をおおまかに把握したい場合には、どのような設定項目があるのかを理解するのが近道です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e994ec61200b-pi" style="display: inline;"><img alt="Options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e994ec61200b image-full img-responsive" src="/assets/image_835920.jpg" title="Options" /></a></p>
<p>AutoCAD の場合、設定値の多くがシステム変数に格納されています。「システム変数にどのようなものがあるか」、「値を書き込むことで設定が可能なのか」、を確認すると、API でコントロール可能な範囲がおぼろげながら把握できます。同時に、数多く用意されている標準コマンドの内容を見ておくことで、重複する機能の作成の防止につながります。</p>
<p>もちろん、システム変数とコマンドの一覧は、オンラインヘルプからたどり着くことが出来ます。</p>
<p>特定の手順に沿った複雑な処理が必要なら、API の使用を検討すべきです。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/02/autocad-features-which-are-not-available-on-autocad-lt-api.html" rel="noopener" target="_blank">5 つある AutoCAD API</a></strong> の詳細は、オンライン ヘルプの「<strong><a href="https://help.autodesk.com/view/OARX/2021/JPN/" rel="noopener" target="_blank">開発者用ドキュメント</a></strong>」からアクセス出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e994e87a200b-pi" style="display: inline;"><img alt="Api_documents" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e994e87a200b image-full img-responsive" src="/assets/image_940430.jpg" title="Api_documents" /></a></p>
<p>ただ、個々の API の開発者用ガイドに目を通すのは至難の業です。おそらく、次に課題になっていくのは、<strong>どの API を使用すべきなのか？</strong> という点です。</p>
<p>おすすめの API という意味では、習得が容易な Visual Basic 言語が使える&#0160;<strong>VBA</strong>（ActiveX オートメーション、COM API）が適切でしょう。ただし、VBA では <strong>マクロ</strong> という単位でカスタム コマンドを作成することが出来ません。</p>
<p>カスタム コマンドの作成が可能で、パレットやリボンといったインタフェースも API で利用することをお考えなら、<strong>AutoCAD .NET API</strong> をお勧めします。</p>
<p>他のアプリケーションやサービスと「接続」することをお考えなら、AutoCAD をインストールした<span style="text-decoration: underline;">コンピュータ内の他のソフトウェアとの連携</span>が容易な <strong>ActiveX オートメーション/COM API</strong> を、インターネットを使ったクラウド連携をカスタム コマンドに取り入れるような場合は <strong>AutoCAD .NET API</strong>（別名 <strong>Managed .NET API</strong>）が最適です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278801a2034200d-pi" style="display: inline;"><img alt="Api_documents" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801a2034200d image-full img-responsive" src="/assets/image_889518.jpg" title="Api_documents" /></a></p>
<p>もし、VBA をお使いで、AutoCAD .NET API を使って <strong><a href="https://ja.wikipedia.org/wiki/Visual_Basic_.NET" rel="noopener" target="_blank">VB.NET</a> </strong>言語（Visual Basic）で API カスタマイズしてみたい、というご要望をお持ちなら、オンラインヘルプとは別のトレーニング マテリアルも用意されています。</p>
<p style="padding-left: 40px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/06/autocad-2022-dotnet_api-training-materials.html" rel="noopener noreferrer" target="_blank">AutoCAD 2022 .NET API トレーニング マテリアル</a></strong><span style="background-color: #ffff00;">（2021年6月4日追記）</span></p>
<p style="padding-left: 40px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/08/autocad-2021-dotnet_api-training-materials.html" rel="noopener noreferrer" target="_blank">AutoCAD 2021 .NET API トレーニング マテリアル</a></strong></p>
<p>過去のカスタマイズ資産のうち、業務上、ObjectARX を習得しなければならない、といった場合には、同じくトレーニング マテリアルが利用可能です。</p>
<p style="padding-left: 40px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/06/autocad-2022-objectarx-training-material.html" rel="noopener noreferrer" target="_blank">AutoCAD 2022 ObjectARX トレーニング マテリアル</a></strong><span style="background-color: #ffff00;">（2021年6月4日追記）</span></p>
<p style="padding-left: 40px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/08/autocad-2021-objectarx-training-material.html" rel="noopener noreferrer" target="_blank">AutoCAD 2021 ObjectARX トレーニング マテリアル</a></strong></p>
<p>少し古いバージョンですが、AutoCAD 2013 時に翻訳した ObjectARX 開発者用ガイドをご参照いただくことも出来ます。基本的な内容は今も変わりないので、ObjectARX の理解を深めるのに役立ちます。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0278801adedc200d img-responsive"><a href="https://adndevblog.typepad.com/files/arxdev2013_j.zip"><strong>ObjectARX 開発者用ガイド</strong>をダウンロード</a></span></p>
<p>歴史の古い AutoCAD と AutoCAD API。なかなか「とっつきにくい」印象を持たれがちですが、1 つ 1 つの理解と把握が必要なのは製品操作と同じです。</p>
<p>もちろん、開発業務を外部委託する受託開発業務をオートデスク認定販売店にご相談いただく手もあります。自社で開発される場合には、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/12/adn-faq.html#_support" rel="noopener" target="_blank">Autodesk Developer Network（ADN）</a></strong>の利用もご検討いただけます。</p>
<p>AutoCAD と AutoCAD API、ぜひ、末永くお付き合いください。 Happy Coding！</p>
<p>By Toshiaki Isezaki</p>
