---
layout: "post"
title: "Forge API 用語について"
date: "2016-07-06 02:22:29"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/07/forge-api-glossary.html "
typepad_basename: "forge-api-glossary"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb091b1153970d-pi" style="display: inline;"><img alt="Forge_platform" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb091b1153970d image-full img-responsive" src="/assets/image_942168.jpg" title="Forge_platform" /></a></p>
<p>Forge API の各 API については、<strong>Forge ポータル（<a href="https://forge.autodesk.com/" rel="noopener" target="_blank">https://forge.autodesk.com/</a>）</strong>から、その概要は利用方法を記したドキュメントにアクセスすることが出来ます。もちろん、開発サイクルの速いクラウド ベースの API なので、ドキュメントは英語でのみの提供になっています。</p>
<p>最近では、完璧とは言えないまでも、以前より簡単に Web 上の翻訳サービスを利用することも出来るので、適宜、それらを利用しながら内容を把握していただくことも可能かと思います。</p>
<p>ただ、ドキュメント上には、Forge ならではの解釈で使用される「用語」が存在するのも事実です。ここでは、Forge Platform ドキュメントで利用される用語について、簡単に説明を加えておくことにします。なお、「用語」は、英語ドキュメント参照を目的にするため、英語表記のままにしてあります。</p>
<p><strong>API</strong></p>
<p style="padding-left: 30px;">もちろん、<a href="https://ja.wikipedia.org/wiki/%E3%82%A2%E3%83%97%E3%83%AA%E3%82%B1%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%BF%E3%83%95%E3%82%A7%E3%83%BC%E3%82%B9" rel="noopener" target="_blank">Application Programming Interface</a> の略ですが、Forge Platform では、&quot;Web API&quot; &#0160;として知られている HTTP REST API、いわゆる <a href="https://ja.wikipedia.org/wiki/REST" rel="noopener" target="_blank">RESTful API</a> を指します。同時に、個々の API 名称の基で提供される endpoint のコレクション（集合）として利用されることもあります。</p>
<p style="padding-left: 30px;">例: &quot;Model Derivative API&quot;、&quot;Data Management API&quot;、&quot;Reality Capture API&quot;、&quot;Fusion Connect API&quot;</p>
<p style="padding-left: 30px;">注意事項:</p>
<ul>
<li>&quot;API&quot; と言う用語は、AutoCAD のようなオートデスク製のデスクトップ製品用 API も含め、かなり広義に利用されています。Forge では、混乱や矛盾を避ける目的で、より限定的に RESTful API のみを指すように使用されています。</li>
<li>JavaScript ライブラリによってWeb &#0160;ブラウザ上でサポートされる &quot;Viewer&quot; は、HTTP REST API &#0160;を利用しないため、その名称に &quot;API&quot; は含まれていません。&quot;Viewer API&quot; や &quot;Viewing API&quot; とは呼びません。</li>
<li><a href="http://forge.autodesk.com" rel="noopener" target="_blank">Forge ポータル</a>&#0160;にリストされている&#0160;&quot;OAuth&quot; は、内部的に RESTful API を使用しますが、一般に流通している「用語」であるため、厳密には、その名称の後に &quot;API&quot; は付けることはありません。決して &quot;OAuth API&quot; とは言いません。</li>
</ul>
<p style="padding-left: 30px;">個々の API の中に定義された個々の機能には、&quot;API&quot; を付けることはありません。例えば、Model Derivative API の 1 つの機能である変換処理（Translation）について、”Translation API” のように使用をすることはありません。</p>
<p style="padding-left: 30px;">個々のサービスを示す名称に &quot;API&quot; を付けることはありません。例えば、Data Management API 内のサービスには、&quot;Forge-DM API&quot; や OSS API&quot; のように &quot;API&quot; を付けることはありません。</p>
<p style="padding-left: 30px;">コード ライブラリ名に &quot;API&quot; を付けることはありません。&quot;JavaScript API&quot; のように利用することはありません。</p>
<p><a name="_apiproxy"></a><strong>API Proxy</strong></p>
<p style="padding-left: 30px;">API Proxy は、論理的に関係する endpoint をグループ化したものです。API Proxy には、endpoint URI の名前空間にあたる独自のベースパスがあります。例えば、&#0160;&quot;oauth-oss-v2-service&quot; は、ベースパスとして https://developer.api.autodesk.com/oss/v2 にマップされています。同様に、&quot;oauth-dm-service&quot; は、ベースパスとして https://developer.api.autodesk.com/data/v1 にマップされています。</p>
<p><strong>Platform Service API</strong></p>
<p style="padding-left: 30px;">Platform Service API は、Forge プラットフォーム API のサブセットで、従来からの存在する購入製品やサブスクリプションに<span style="text-decoration: underline;">依存しない</span>ものです。例えば、Model Derivative API &#0160;は、ファイルへのアクセスを得るために developer（開発者）が利用する&#0160;Platform Service API です。API の使用のためにオートデスク製品が直接介在するこはありません。Platform Service API は、その名称にオートデスク製品名やブランド名が含まれることはありません。</p>
<p><strong>Product API</strong></p>
<p style="padding-left: 30px;">Platform Service API は、Forge プラットフォーム API のサブセットで、従来からの存在する購入製品やサブスクリプションに<span style="text-decoration: underline;">依存する</span>ものです。例えば、Fusion Connect API は、既存の Fusion Connect（旧名 &quot;SeeControl&quot;）サブスクリプションの環境で使用する Product API です。特定の機能を一般化したり、<a href="https://en.wikipedia.org/wiki/Microservices" rel="noopener" target="_blank">Microservice</a>&#0160;指向の API を指すものではありません。また、Platform Service API と重複することはありません。</p>
<p><a name="_endpoint"></a><strong>endpoint</strong></p>
<p style="padding-left: 30px;">endpoint は、単一の <a href="https://ja.wikipedia.org/wiki/Uniform_Resource_Identifier" rel="noopener" target="_blank">URI</a> で示される単一の HTTP リソースです。API proxy は、与えられたベースパス配下の URI 名前空間で、1 つ以上の endpoint から構成されています。また、endpoint は、デベロッパ ポータル上では個々の API を表現していて、機能別に通常グループ化されています。</p>
<p style="padding-left: 30px;">例えば、次の endpoint は、OSS bucket の詳細を取得するためのものです。<br /><a href="https://developer.api.autodesk.com/oss/v2/buckets/:bucketKey/details">https://developer.api.autodesk.com/oss/v2/buckets/:bucketKey/details</a></p>
<ul>
<li>この endpoint の表示名は: GET buckets/:bucketKey/details</li>
<li>この endpoint のドキュメント中の&#0160;<em>url_path</em>&#0160;は: bucket-:bucketKey-details-GET</li>
<li>この endpoint のドキュメント上のフルパスは: <a href="https://developer.autodesk.com/en/docs/data/v2/reference/http/bucket-:bucketKey-details-GET">https://developer.autodesk.com/en/docs/data/v2/reference/http/bucket-:bucketKey-details-GET</a></li>
<li>この endpoint 用にドキュメントに含まれる RST ファイルの <a href="https://git.autodesk.com/cloudplatform-apim/api_documentation/">api_documentation repository</a>&#0160;内の&#0160;<em>source</em> ファイル名は: bucket-bucketKey-details-GET.rst</li>
<li>RST ファイルのフルパスは: <a href="https://git.autodesk.com/cloudplatform-apim/api_documentation/blob/dev/data/v2/reference/http/buckets-bucketKey-details-GET.rst">cloudplatform-apim/api_documentation:dev/data/v2/reference/http/bucket-bucketKey-details-GET.rst</a></li>
<li><em>endpoint grouping</em>（endpoint グループ）&#0160;は: Object Storage</li>
<li>この endpoint の&#0160;<em>API proxy</em>&#0160;は: oauth-oss-v2-service</li>
<li>この endpoint のベースパスは: /oss/v2/</li>
<li>この endpoint にアクセスすることが出来る&#0160;<em>API Product</em>&#0160;は: Data Management API&#0160;</li>
<li>この endpoint の&#0160;<em>service</em>（サービス）は: OSS (v2)</li>
</ul>
<p style="padding-left: 30px;">いくつかのケースでは、サービス/API が API 呼び出しを満たすべく、開発者（developer）に地理的な地域を指定させるために、個別の endpoint が分離した URI を持つようデザインされたものがあります。これは、前述のルールの例外で、ドキュメント上に明記されているはずです。</p>
<p><strong>endpoint grouping</strong></p>
<p style="padding-left: 30px;">デベロッパー ポータルに適用される、機能によって endpoint を論理的にグループ化するコンセプトです。endpoint グループ化は、 API ドキュメントの &quot;HTTP Specification&quot; セクションに明記されています。</p>
<p><strong>service</strong></p>
<p style="padding-left: 30px;">文脈によっては、&quot;service&quot; は、オートデスク社内でサービスを開発している開発チーム &quot;service team&quot; と同義語となる場合もあります。多くの場合、各 API の一連の機能（論理的な機能群）を表します。稀に 1 つのサービスが複数の他のサービスで作られる場合もあります。これらのサービスは、オートデスクが外部に公開している API を直接サポートしたり、個別の API 内で機能となるバックエンド サービスです。&#0160;&#0160;</p>
<p><strong>library</strong></p>
<p style="padding-left: 30px;">library は、クラス、メソッド、その他、 Viewer を含むオートデスクの API の利用に便利な、再利用可能な言語固有セットです。それらは、内部で素の HTTP&#0160;REST 呼び出しを おこない、HTTP REST 呼び出しをラップします。</p>
<p style="padding-left: 30px;">言語固有のライブラリは GitHub.com 上に&#0160;&quot;Autodesk-Forge&quot; 配下のリポジトリでホストされています。歴史的には、それらを &quot;SDK&quot; と呼ぶかも知れませんが、正確には、library は&#0160;&quot;software development kits&quot; ではありません。</p>
<p><strong>tutorial</strong></p>
<p style="padding-left: 30px;">API 使用時の個別の手順やワークフローを解説するもので、&quot;guide&quot; や &quot;step-by-step tutorial&quot;、または、 &quot;step-by-step guide&quot; と同義です。はじめに習得すべき、&quot;Hello, world!&quot; サンプル的な内容を達成するアクションが記されています。例えば、<a href="https://developer-dev.autodesk.com/en/docs/viewer/v2/tutorials/extensions/">Extensions tutorial for the Viewer</a>&#0160;を確認してみてください。</p>
<p style="padding-left: 30px;">REST API では、tutorials は&#0160;cURL コマンドを使用しているはずです。 言語固有のライブラリが存在する場合には、それらも使用することもあります。より複雑な例を示す場合には、tutorial ではなく、<em>code sample</em>&#0160;が用意されます。</p>
<p><strong>code sample</strong></p>
<p style="padding-left: 30px;">tutorial とは異なり、ステップ毎のガイドは提供ません。 通常、developer（開発者）の API&#0160;習得のスタートポイントとなったり、実際の開発作業に活用したり出来る、より複雑で強力な例を含んでいます。例えば、ある code sample では、iOS に Viewer を統合するサンプルが提供されています。このようなサンプルは、tutorial には複雑すぎます。なお、提供される code sample は、すべてのシナリオにおいて完全でない場合もありますのでご注意ください。</p>
<p style="padding-left: 30px;">code sample は、 GitHub.com 上の &quot;Autodesk-Forge&quot; 名前空間にホストされています。また、code sample は、デベロッパー ポータルの&#0160;&quot;Code Samples&quot; セクションから参照することが出来ます。各 code sample リポジトリのトップ ページには、README.md にサンプルの機能と使用手順などの概要が提供されています。</p>
<p><strong>StakOverflow tag</strong></p>
<p style="padding-left: 30px;">各 API は、固有の StackOverflow タグを持ち、developer は質問を適切にタグ付けすることが出来ます。これらのタグは、デベロッパー ポータル上の StackOverflow 検索機能に統合されています。</p>
<p><strong>developer</strong></p>
<p style="padding-left: 30px;">Forge API を利用する 3rd party、つまり、オートデスク以外の外部の開発者を意味します。</p>
<p><strong>3D , 2D</strong></p>
<p style="padding-left: 30px;">3 次元や 2 次元を意味するもので、3d や 2d（d が小文字）の用語では用いません。&#0160;</p>
<p>デベロッパー ポータル上のドキュメントを読み解く上で参考にしてみてください。</p>
<p>By Toshiaki Isezaki</p>
