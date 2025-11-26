---
layout: "post"
title: "デスクトップ製品の共有サーバー利用について"
date: "2016-08-12 01:21:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/08/use-of-desktop-products-on-the-shared-server.html "
typepad_basename: "use-of-desktop-products-on-the-shared-server"
typepad_status: "Publish"
---

<p><strong><a href="https://knowledge.autodesk.com/support/system-requirements" target="_blank">動作環境となる OS のリスト</a>&#0160;</strong>には記載されてはいないのでサポート対象とはなりませんが、AutoCAD や Inventor、Revit などのデスクトップ製品は、Windows Server OS を搭載する共有サーバーで動作させることが出来る場合があります。</p>
<p>また、デスクトップ製品が持つ&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html" target="_blank">各種 API やテクノロジ</a>&#0160;</strong>を組み合わせることで、共有サーバーにインストールされたデスクトップ製品を自動的に起動して、与えた情報に基いて、図面や 3D &#0160;モデルを自動生成したり、既存の図面/3D モデル編集してしまうことも可能です。</p>
<p>ただ、このような手法で Web サーバーとなるコンピュータにオートデスク デスクトップ製品をインストールし、インターネットを介して社外のユーザが Web ページに入力したパラメータに沿って図面や 3D &#0160;モデルを自動生成するようなシステムは、オートデスクの使用許諾に抵触してしまう可能性があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20f10ec970c-pi" style="display: inline;"><img alt="Web_server_use" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20f10ec970c img-responsive" src="/assets/image_342494.jpg" title="Web_server_use" /></a></p>
<p>特に、オートデスクの製品を購入して同システムを運用する企業（または、個人）と、エンドユーザとなる企業（または個人）が異なっている場合には、ホスティング ビジネスと見なされてしまいます。</p>
<p>このような使用を禁止しているのは、<strong>Software License Agreement</strong>、通称、<strong>SLA</strong> と呼ばれる「<strong>使用許諾およびサービス契約</strong>」です。SLA は、通常、デスクトップ製品のインストール時に同意を求めるかたちで表示されます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09285313970d-pi" style="display: inline;"><img alt="Sla_during_installation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09285313970d image-full img-responsive" src="/assets/image_391875.jpg" title="Sla_during_installation" /></a></p>
<p>SLA は、Web で <strong><a href="http://www.autodesk.com/company/legal-notices-trademarks/software-license-agreements" target="_blank">公開</a> </strong>されているので、インストール後にはいつでもその内容を確認することが出来ます。最新の 2017 シリーズの SLA は、<strong><a href="http://download.autodesk.com/us/FY17/Suites/LSA/ja-JP/lsa.html" target="_blank">http://download.autodesk.com/us/FY17/Suites/LSA/ja-JP/lsa.html</a>&#0160;</strong>から参照していただくことが出来ます。</p>
<p>前述のようなホスティング ビジネスは、SLA の&#0160;2. 許諾事項、<strong>禁止事項</strong> &gt;&gt;&#0160;<strong>2.1 禁止事項</strong> &gt;&gt;&#0160;<strong>2.1.1 </strong><u><strong>ライセンス付与の排除、許諾されない行為</strong></u>&#0160;の (e)、(g) で禁止されています。</p>
<hr />
<p style="padding-left: 30px;">2. <strong>許諾事項、禁止事項</strong></p>
<p style="padding-left: 30px;">2.1 <u>禁止事項</u></p>
<p style="padding-left: 30px;">2.1.1 <u>ライセンス付与の排除、許諾されない行為</u> 本契約における別段の定めにかかわらず、以下に掲げるライセンスは、本契約のもとで（明示、黙示、その他の態様を問わず）付与されない（また、本契約は、以下に掲げるライセンスを明示的に排除する）ことについて、両当事者は了解し、同意するものとします：(a) 対象外マテリアルに対するライセンス、(b) ライセンシーが合法的に取得せず またはライセンシーが本契約に違反して取得し、もしくは本契約に適合しない仕方で取得したオートデスク マテリアルに対するライセンス、(c) 適用されるライセンス期間（固定の期間か、リレーションシップ・プログラム期間かを問いません）を超えて、または該当するライセンス タイプもしくは許可数の範囲外でライセンス対象マテリアルをインストールしてアクセスすることができるライセンス、(d) オートデスクが書面で別段の許諾をした場合を除き、ライセンシーが所有またはリースしかつライセンシーが管理しているコンピュータ以外のコンピュータにライセンス対象マテリアルをインストールすることができるライセンス、(e) 本契約中で明示的に定めまたはオートデスクが書面をもって明示的に許諾する以外に、いかなる者または法的実体に対してであれオートデスク マテリアルの全部または一部を配布、レンタル、貸与、リース、販売、サブライセンス、譲渡、その他提供することができるライセンス、(f) オートデスク マテリアルが有する特性または機能を、ネットワーク上またはホスト方式によるものかどうかにかかわらず、（該当するライセンス タイプにおいて定められている目的のためにライセンシー自身にかつライセンシー自身のために利用可能にする以外に）いかなる者または法的実体に対してであれ利用可能にすることができるライセンス、<span style="background-color: #ffffff;">(g) 特定のライセンス タイプに関して別段の明示的定めのある場合を除き、ワイド エリア ネットワーク（WAN）、仮想プライベート ネットワーク（VPN）、仮想化、ウェブ ホスティング、タイム シェアリング、サービス ビューロー、サービスとしてのソフトウェア、クラウド サービス、クラウド技術またはその他のサービスもしくは技術に関連しての使用を含め、インターネットその他の非ローカル ネットワークでのオートデスク マテリアルのインストールもしくはアクセスまたはかかるインストールもしくはアクセスを許容するライセンス、(</span>h) オートデスク マテリアルに付された財産権表示、ラベルまたは標章を除去、改変しまたは閲読困難にすることができるライセンス、(i) オートデスク マテリアルのデコンパイル、逆アセンブル、その他のリバース エンジニアリングをすることができるライセンス、(j) 目的の如何によらずオートデスク マテリアルを翻訳、翻案もしくは編集し、またはオートデスク マテリアルに基づく二次的著作物を創作し、またはオートデスク マテリアルにその他の変更を加えることができるライセンス。</p>
<hr />
<p>最近では、大手ベンダーが構築したクラウド インフラを含め、第三者が所有するコンピュータ上の仮想 OS に製品をインストールして、Web サーバー化するようなこともできます。この点は 、オートデスク製品購入者とハードウェアを所有する企業、つまり、クラウド ベンダーが異なることになるので、(d) によって問題になる場合も考えられます。</p>
<p>このような使用許諾上の問題を回避して、クラウド リソースを適切に利用できる Web サービス API を用意しています。特に、図面を自動生成するようなご要望には、<a href="http://adndevblog.typepad.com/technology_perspective/2014/12/autocad-io-web-service.html" target="_blank"><strong>Design Automation API</strong>（旧名 <strong>AutoCAD I/O</strong>）</a>&#0160;を利用することが可能です。Design Automation API は、Forge Platform API の 1&#0160;つで、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" target="_blank">AcCoreConsole.exe</a></strong>&#0160;をクラウド上で動作させるため、ユーザ インタフェースの表示を含め、オーバーヘッドを除去したかたちでビジネス ロジックのみを実行させることが出来る Web サービス API です。</p>
<p>少し視点を変える必要がありますが、「AutoCAD でホスティング ビジネスで利用できればいいのに！」と考えている方は、ぜひ、Design Automation API の評価をお勧めします。Design Automation API の詳細は、<strong><a href="https://developer.autodesk.com/en/docs/design-automation/v2" target="_blank">https://developer.autodesk.com/en/docs/design-automation/v2</a></strong>&#0160;から把握いただくことが出来ます。</p>
<p>By Toshiaki Isezaki</p>
