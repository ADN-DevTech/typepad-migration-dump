---
layout: "post"
title: "Autodesk Informed Design API パブリック ベータ"
date: "2025-06-04 01:24:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/06/autodesk-informed-design-api-now-public-beta.html "
typepad_basename: "autodesk-informed-design-api-now-public-beta"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d45a83200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d45a87200c-pi" style="display: inline;"><img alt="Informed design api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d45a87200c image-full img-responsive" src="/assets/image_892053.jpg" title="Informed design api" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d45a83200c-pi" style="display: inline;"><br /></a></p>
<p>昨年導入された <a href="https://blogs.autodesk.com/autodesk-news-japan/autodesk-informed-design/" rel="noopener" target="_blank">Autodesk Informed Design</a> に新たにパブリック ベータ版の API が登場、Autodesk Platform Services（APS）の一部として利用可能になりました。</p>
<p><strong>Autodesk Informed Design とは？</strong></p>
<p><a  _istranslated="1" href="https://www.autodesk.com/campaigns/informed-design-industrialized-construction" rel="noopener" target="_blank">Autodesk Informed Design</a> は、設計プロセスの早い段階で詳細な建物コンポーネント情報を利用出来るようにすることで、製品メーカーと建築設計者を結びつけます。建築設計者は、製造可能性に対処するために建設段階や製造段階まで待つのではなく、モデリング中に寸法、コンフィギュレーション ルール、材料などの製品データを直接操作出来るようになります。</p>
<p>中核製品として使用する Inventor と Revit に専用のアドイン アプリを介在させ、両製品の設計データをプロジェクト単位で管理する Web アプリと併用することで、設計の確実性の向上、手戻りの低減、メーカーが定義した仕様と制約に準拠していることを確認することで、構成コンポーネントの正当性をチェック、製造中の再設計のリスクが軽減されます。<br /><br />Autodesk Informed Design を使用すると、次の点を実現することが出来るようになります。</p>
<ul>
<li><a  _istranslated="1" href="https://www.autodesk.com/products/informed-design-inventor" rel="noreferrer noopener" target="_blank">Informed Design for Inventor</a>（Inventor アドイン - 英語 UI）を使用して、設定可能な建物コンポーネント データ（ドア、バルコニーなど）をパブリッシュします。</li>
<li>これらのコンポーネントを構成し、<a  _istranslated="1" href="https://www.autodesk.com/products/informed-design-revit" rel="noreferrer noopener" target="_blank">Informed Design for Revit</a>（Revit アドイン - 英語 UI）を使用して製造上の制約内で建築設計に統合します。</li>
<li><a  _istranslated="1" href="http://informeddesign.autodesk.com/" rel="noreferrer noopener" target="_blank">Informed Design</a>（Web アプリ - 英語 UI）を使用して、最小限のやり取りで BOM 内の製品を効率的に製造します。</li>
</ul>
<p>Autodesk Informed Design の概要については、次のデモ動画をご確認ください。</p>
<div class="video-embed-field-provider-wistia video-embed-field-responsive-video"><iframe allowfullscreen="allowfullscreen" class="wistia_embed" frameborder="0" height="480" src="https://fast.wistia.com/embed/iframe/vsl0hxgmre?autoPlay=0" width="854"></iframe></div>
<p><strong>Autodesk Informed Design API の紹介</strong></p>
<p>Autodesk Informed Design API は、Informed Design の機能をクラウドに拡張し、開発者が製品設計および製造データの読み取り、書き込み、拡張をおこなえるようにします。これにより、パートナー、アプリ開発者、およびエンドユーザーが、オートデスク製デザイン ソフトウェアと直接統合するカスタム ワークフローを構築することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d45a98200c-pi" style="display: inline;"><img alt="Aid_chart" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d45a98200c image-full img-responsive" src="/assets/image_628201.jpg" title="Aid_chart" /></a></p>
<p><strong>Informed Design API で何ができるのか？</strong></p>
<p>Informed Design API を使用すると、製品機能と同じように販売とエンジニアリングの設定、自動材料管理、調達のコンフィギュレーターなどのプロセスを含む、製造と建設の間のワークフローを自動化することが出来るようになります。</p>
<p>現在、実現可能な点は次のとおりです。</p>
<p>自動製品作成</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="2" data-aria-posinset="0" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:1440,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[9675],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="10">
<p>Inventor を使用して、構成可能な建物コンポーネントを公開および管理します。</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="2" data-aria-posinset="1" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:1440,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[9675],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="10">
<p>Revit ユーザー向けに製品の可変性と共有ルールを設定します。</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="2" data-aria-posinset="2" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:1440,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[9675],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="10">
<p>製品のリリースを管理し、共同作業者に適切なデータが表示されるようにします。</p>
</li>
</ul>
<p>コンフィギュレーション主導型設計</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="2" data-aria-posinset="0" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:1440,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[9675],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="11">
<p>設計者は、Inventor で定義されたルール内で Revit で製品を設定できます。</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="2" data-aria-posinset="1" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:1440,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[9675],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="11">
<p>設定可能な入力に基づいて Revit アセットを自動的に生成します。</p>
</li>
</ul>
<p>製造出力 – Revit または Inventor を使用しない</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="2" data-aria-posinset="0" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:1440,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[9675],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="12">
<p>メタデータ製品でタグ付けされたアクセスは、ACC、BIM 360、または Fusion Team を介しておこなえます。</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="2" data-aria-posinset="1" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:1440,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[9675],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="12">
<p>BOM、図面、製造の詳細を API から直接取得します。</p>
</li>
</ul>
<p><strong>はじめ方</strong></p>
<p>パブリック ベータ API をどのようにお使いかをぜひお聞かせください。皆様からのフィードバックは、Informed Design&#0160; の未来を形作るために重要です。</p>
<p>現在参照可能なリソース：</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="6">
<p><a href="https://aps.autodesk.com/developer/overview/informed-design-api">API Documentation</a></p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="6">
<p><a href="https://aps.autodesk.com/en/docs/informed-design/v1/developers-guide/overview/en/docs/informed-design/v1/tutorials/getting_started/" rel="noreferrer noopener" target="_blank">Step-by-Step Tutorials</a>&#0160;</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="6">
<p><a href="https://aps.autodesk.com/en/docs/informed-design/v1/developers-guide/overview/en/docs/informed-design/v1/code_samples/code_samples" rel="noreferrer noopener" target="_blank">Code Samples</a>&#0160;</p>
</li>
</ul>
<p>フィードバック：</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="0" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="7">
<p><a  _mstmutation="1" href="https://forums.autodesk.com/t5/informed-design-forum/bd-p/informed_design" rel="noreferrer noopener" target="_blank">Autodesk Informed Design Forum</a>&#0160;に参加</p>
</li>
</ul>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Aptos" data-leveltext="-" data-list-defn-props="{&quot;335551671&quot;:0,&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Aptos&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="7">
<p><a  _mstmutation="1" href="https://forums.autodesk.com/t5/informed-design-ideas/idb-p/informed_design_ideas" rel="noreferrer noopener" target="_blank">IdeaStation</a>&#0160;にアイデアを提出</p>
</li>
</ul>
<p>Autodesk Informed Design を独自のワークフローにどのように拡張するかを楽しみにしています。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/autodesk-informed-design-api-now-public-beta" rel="noopener" target="_blank">Autodesk Informed Design API is now in Public Beta | Autodesk Platform Services</a>&#0160;から転写・意訳、補足したものです。</p>
<p>By Toshiaki Isezaki</p>
