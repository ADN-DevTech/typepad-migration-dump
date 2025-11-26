---
layout: "post"
title: "AutoCAD コンポーネント間運用の例 ～ その１"
date: "2022-03-16 01:03:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/03/components_operation-1.html "
typepad_basename: "components_operation-1"
typepad_status: "Publish"
---

<p>AutoCAD 2006&#0160;までの AutoCAD では、日本語を扱うために Shift-JIS コードを使用していました。この場合、全角文字を表現する際に上位バイトと下位バイトに分割処理する必要があるため、バイト位置に ASCII コードの制御文字コード（&#39;\n&#39; や &#39;\0&#39; など）が存在してまうと、文字化けや意図しない改行が発生してしまい、都度、発生した機能やコマンド毎の改修で対応してきた経緯があります。この問題を解決するため、AutoCAD 2007 で世界中の言語で一意の文字コードを扱う UNICODE を採用して抜本的な機能改善を図っています。</p>
<p>一方、AutoLISP 環境の場合、LISP プログラム作成で利用する Visual LISP エディタが外部コンポーネントとして組み込まれていたため、AutoCAD 本体の UNICODE 対応に完全に追従出来ていない状態が続いていました。例えば、LISP 上の日本語文字は全角文字 1 つを 1 文字として扱えず、上位バイトと下位バイトを個別に認識するため、2 文字と扱ってしまうことになります。</p>
<p>例えば、AutoCAD 2019 で文字数を返す <a href="https://help.autodesk.com/view/OARX/2019/JPN/?guid=GUID-F16AAC5F-5C87-4DC5-A7B9-BDCD25DC507A" rel="noopener" target="_blank">(strlen)</a> を &quot;全角&quot; という文字に適用すると、上位バイトと下位バイトを個別にカウントしてしまい、4 を返します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e14719d2200b-pi" style="display: inline;"><img alt="2019" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e14719d2200b image-full img-responsive" src="/assets/image_836448.jpg" title="2019" /></a></p>
<p>そこで、AutoCAD 2021 では、AutoCAD R14 から使用し続けていた Visual LISP の継続利用を断念し、Microsoft 社がオープンソースとして公開している Visual Studio Code を採用することで、AutoLISP 環境の完全 UNICODE 対応を実現しています。</p>
<p>AutoCAD 2021 以降（ここでは最新の AutoCAD 2022）、先と同じ内容で (strlen) 関数を行使すると、期待した 2 が返ります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e14719e1200b-pi" style="display: inline;"><img alt="2022" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e14719e1200b image-full img-responsive" src="/assets/image_533034.jpg" title="2022" /></a></p>
<p>残念ながら、AutoCAD 2020 以前の AutoLISP プログラム単体で、文字列操作の問題を完全に解決することは難しい状態です。そこで、既定で UNICODE 対応している .NET Framework ベースの AutoCAD .NET API で代替する AutoLISP 関数を定義して、AutoLISP 側で使用する方法があります。</p>
<p>AutoCAD .NET API で AutoLISP 関数を定義する方法の詳細は、次の Autodesk Knowledge Network 記事くを確認してみてください。</p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/7ziCRKH19yLela4TNqbE1j.html" rel="noopener" target="_blank">AutoCAD .NET API ：AutoLISP 関数の定義</a></p>
<p>内部で String.Length プロパティを使った (strlen2) 関数を AutoCAD .NET API で定義しておくと、AutoCAD 2020 以前のバージョンでも、UNICODE をベースにした文字数を得られるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9c36e9200c-pi" style="display: inline;"><img alt="2019-2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9c36e9200c image-full img-responsive" src="/assets/image_567646.jpg" title="2019-2" /></a></p>
<p>もちろん、AutoLISP 関数を定義したアドイン（DLL ファイル）を AutoCAD にロードしておく必要があるのは言うまでもありません。</p>
<p>By Toshiaki Isezaki</p>
