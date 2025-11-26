---
layout: "post"
title: "雑学：Design Automation for Fusionでカスタム処理を記述する言語 TypeScriptとは"
date: "2025-06-16 01:01:08"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/06/trivia-design-automation-for-fusion-typescript.html "
typepad_basename: "trivia-design-automation-for-fusion-typescript"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ecc37c200b-pi"><img alt="TypeScriptTitle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ecc37c200b img-responsive" src="/assets/image_78743.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="TypeScriptTitle" /></a></p>
<p>ブログ記事「<a href="https://adndevblog.typepad.com/technology_perspective/2025/06/design-automation-api-for-fusion-generally-available.html">Design Automation API for Fusion : 一般リリース</a>」でご案内いたしましたが、Design Automation for Fusionが一般リリースとなっております。</p>
<p>デスクトップ版のFusionではAPIを用いたカスタムスクリプト、アドインの開発ではC++またはPythonをご利用いただけますが、Design Automation for Fusionでのカスタム処理は<strong>TypeScript</strong>コードのみをサポートしています。デスクトップ版Fusionのカスタムスクリプトやアドイン開発に利用していたPythonまたはC++は利用することが出来ません。</p>
<p>TypeScriptはMicrosoft社が開発したJavaScriptを拡張して作られた言語となりますが、Web開発のご経験がない方にはあまりなじみがないかもしれませんので、このブログ記事では簡単にJavaScriptの歴史とTypeScriptについて解説をしたいと思います。</p>
<p>&#0160;</p>
<h3>JavaScriptとは</h3>
<p>JavaScriptはブラウザ上で動作する動的なWebページの表示を制御する言語となります。JavaScriptが登場した時代はMicrosoft社やNetscape社がブラウザの覇権争いをしている真っ只中の時代であったこともあり、言語仕様の統一も不十分で動作するブラウザの独自色が強いものでした(Microsoft社のInternet ExploreではJavaScriptに準拠したJScriptと呼ばれる言語となっていました)。</p>
<p>異なるブラウザのみならず、同じブラウザの間でもバージョン間での差異がある場合もあり、複数のブラウザに対応するためには各ブラウザ向けに処理を記述するといった対処が必要となるため、開発・メンテナンスに非常に手間がかかることから大規模な開発をすることは推奨されておらず、Webページの一部分の表示を変える、といったような限定的な使われ方をしておりました。</p>
<p>当時を知る方の中には、大規模な動的WebページにはAdobe社製のFlashが使われていたことを記憶している方も多いかと思います。</p>
<h3>&#0160;</h3>
<h3>Ajaxの登場による爆発的な普及</h3>
<p>そんなJavaScriptの状況が変わったのは、その誕生から10年ほど経ちJavaScriptとXMLを用いてサーバと非同期通信を行い、HTMLのページ遷移を伴わない動的なページ書き換えを行うAjax(Asynchronous JavaScript And XML)技法の登場によるものでした。</p>
<p>当時のWebページは（Adobe社製のFlash等を用いたものを除き）、基本的にはページ遷移をしながら動作する紙芝居の様な形式となっていました。このためユーザの入力に応じた細やかな動作を実現することが難しく、デスクトップアプリケーションと比較するとユーザエクスペリエンスの面で非常に不満が残るものが大多数を占めておりました。</p>
<p>ところがAjax技法の登場によりHTMLを使ったWebアプリケーションのユーザエクスペリエンス改善への道が開け、そこで使われるJavaScriptは大きな注目を集め爆発的に利用が拡大していくことになります。</p>
<p>JavaScriptの需要が高まるにつれブラウザ間の動作の差異を吸収するようなprototype.jsやJQuery等のオープンソースライブラリが登場、さらに利用が拡大しくこととなります。</p>
<p>JavaScriptのライブラリ・フレームワークはその後も発展続けておりReact、Veu、Angular等の多種多様なフレームワークが開発され利用され続けています。</p>
<p>&#0160;</p>
<h3>サーバサイド・クライアントサイドでのJavaScriptの利用</h3>
<p>ここまでの説明でもお判りかと思いますが、元々JavaScriptはブラウザ上で動作する言語として開発がされておりました。</p>
<p>ところが、JavaScriptの利用者の増加に伴いブラウザの外でJavaScriptを動作させるという需要が出てきました。特に、当時主流だったApache HTTP ServerなどのWebサーバは同時接続が1万を超えるような要求が発生した場合リクエストが受け付けられなくなるという、所謂10k問題を抱えており、これに対応できるWebサーバが求められておりました。</p>
<p>このような大規模リクエストにも対処が可能なWebサーバを実装することを目標の一つに掲げ、Node.jsというJavaScriptをブラウザ外で利用してWebサーバを構築できるモジュール（正確には、JavaScriptの実行環境）が開発されました。</p>
<p>このNode.jsを用いることによって、JavaScriptを用いてWebサーバを開発することができるようになったのですが、このNode.jsはWebサーバ用途以外にもクライアントで処理を実行するといった事にも利用することが出来るため、クライアントサイドでJavaScriptを動作させる様々なライブラリが開発・整備され活用されていき、JavaScriptの利用はますます広がっていきます。</p>
<h3>JavaScriptの弱点を補うTypeScript</h3>
<p>この様に、JavaScriptが大規模な開発で利用されるようになると、次第に(関係者の間では当初から言われていた事ではありますが)その弱点が明らかになっていきます。ブラウザで小規模な動的ページ実現するようなコードを記述する場合にはあまり大きな問題にはなりませんが、サーバサイドで大規模なコードを書こうとすると、型システムが不在(≒コンパイラによる静的なチェックが出来ない）、Javaの様なクラス指向のオブジェクトではなくプロトタイプベースのオブジェクト指向が馴染みにくい、モジュール化機能が無い、といったような弱点が浮き彫りになってきます。</p>
<p>特に型システムの不在はコードが実行されるまで型不整合によるエラーが分からないため、常に実行時エラーの恐怖に悩まされることとなります。</p>
<p>JavaScriptの利用拡大と大規模な開発に対応するべく業界では様々なアプローチ対策が考えられましたが、そのうちの一つがMicrosoft社が開発したJavaScriptを拡張して作られた言語であるTypeScriptとなります。</p>
<p>※なおJavaScript自身も機能拡張が行われており、ECMAScript 2015(ES6)にてモジュールやクラスへの対応が行われております。</p>
<p>&#0160;</p>
<h3>まとめ</h3>
<p>今回の記事では、Design Automation for Fusionのカスタム処理の開発で使用するTypeScriptについて、その元となっているJavaScriptの歴史を振り返りました。</p>
<p>現在でもTypeScriptはWebアプリケーション、クライアントサイド、サーバサイドの開発でも広く利用されている言語で、JavaScriptの知識があれば記述が容易であるため比較的学習コストが緩やかな言語となります。</p>
<p>Design Automation for Fusionをご利用される場合はTypeScriptの利用が必須となりますので、ご留意ください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
<p>&#0160;</p>
