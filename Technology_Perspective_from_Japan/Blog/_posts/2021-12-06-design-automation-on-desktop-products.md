---
layout: "post"
title: "デスクトップ製品使った自動化について"
date: "2021-12-06 01:48:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/12/design-automation-on-desktop-products.html "
typepad_basename: "design-automation-on-desktop-products"
typepad_status: "Publish"
---

<p><strong><a href="https://ja.wikipedia.org/wiki/%E3%83%87%E3%82%B8%E3%82%BF%E3%83%AB%E3%83%88%E3%83%A9%E3%83%B3%E3%82%B9%E3%83%95%E3%82%A9%E3%83%BC%E3%83%A1%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3" rel="noopener" target="_blank">デジタル トランスフォーメーション（DX）</a></strong>をきっかけに、最近、「<strong>デスクトップ製品を使った API による自動化は、何が OK で何が NG なのか？</strong>」 といった質問をよく聞かれます。</p>
<p>ネットワークやインターネット、クラウドの使用が一般的になった現在、議論には、デスクトップ製品を使った自動化に至る方法が含まれることになります。もちろん、こういった方法論は多くの具体的な手法に枝分かれしてしまうので、１つ１つのパターンについて精査するのは困難です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39d4edb200b-pi" style="display: inline;"><img alt="Who_can_use_product" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39d4edb200b image-full img-responsive" src="/assets/image_734651.jpg" title="Who_can_use_product" /></a></p>
<p>そこで、ここでは視点を変えてデスクトップ製品を使った自動化を考えてみたいと思います。まず、オートデスク製品の自動化についてまとめてみましょう。</p>
<p>永久ライセンスやサブスクリプション ライセンスにかかわらず、オートデスクのデスクトップ製品には、作図やモデリングを支援する機能を作成したり、特定のタスクを自動化したりする目的で、<a href="https://ja.wikipedia.org/wiki/%E3%82%A2%E3%83%97%E3%83%AA%E3%82%B1%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0%E3%82%A4%E3%83%B3%E3%82%BF%E3%83%95%E3%82%A7%E3%83%BC%E3%82%B9" rel="noopener" target="_blank"><strong>API（Application Programming Interface）</strong></a>が提供されています。</p>
<p>これら API は、主に製品にロードして使用するアドイン（別名、アドオン、プラグイン）アプリを開発するために使用されます。このとき、アドインの実行には製品の実行が必須ということになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39da232200d-pi" style="display: inline;"><img alt="Addin_automation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39da232200d image-full img-responsive" src="/assets/image_857137.jpg" title="Addin_automation" /></a></p>
<p>また、オートデスクが提供する API 以外にも API が存在します。自動化を考えると、ユーザーが毎回、手動で製品を起動してアドインをロード、実装機能を実行するよりも、外部からのリクエストに応じて製品を自動的に起動、アドインの自動ロードさせて機能を実行した場合が効果的です。一部 ActiveX オートメーション（COM API）を持つ AutoCAD や Inventor も自動起動の実装が可能ですが、汎用的に Windows が提供する API で製品を起動させることも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39d5115200b-pi" style="display: inline;"><img alt="Launch_automation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39d5115200b image-full img-responsive" src="/assets/image_210889.jpg" title="Launch_automation" /></a></p>
<p>アドインの自動ロード機能は各製品側の機能で実現可能なので、Windows API＋デスクトップ製品 API を併用することで、製品の起動&#0160; &gt;&gt; アドインのロード &gt;&gt; アドイン実装機能の実行、といった一連の動作を一気通貫に自動化出来ることになります。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3992d03200c-pi" style="display: inline;"><img alt="Who_can_use_product2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3992d03200c image-full img-responsive" src="/assets/image_437313.jpg" title="Who_can_use_product2" /></a></p>
<p>ここで重要なのは、<strong>アドインの実行には製品が必須</strong>という点です。言い換えるなら、<strong>API で実装した機能はデスクトップ製品が持つ標準機能と同様に製品が提供する能力の１つである</strong>、と考えることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13506b0200b-pi" style="display: inline;"><img alt="Api_as_a_feature" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13506b0200b image-full img-responsive" src="/assets/image_915512.jpg" title="Api_as_a_feature" /></a></p>
<p>次に考え及ぶのが、<strong>製品の使用権は誰にあるのか？</strong>という点です。</p>
<p>アドインによる自動化で得られる機能を享受出来るのは、サブスクリプションで提供される製品の場合、使用規約の「<strong><a href="https://www.autodesk.com/company/terms-of-use/jp/subscription-types" rel="noopener" target="_blank">指名ユーザー提供方法</a></strong>」に則してライセンスを割り当てられた個人になります。</p>
<ul>
<li>AutoCAD や Revit、Inventor といったオートデスクのデスクトップ製品は、2016年1月31日で永久ライセンスの販売を終了し、サブスクリプション販売へ移行しています。移行にともなって、製品使用時に適用される規程も変化しています。過去、永久ライセンス時にデスクトップ製品インストール時で表示されていた「使用許諾契約書（EULA）」は、現在、サブスクリプション モデルで「<strong><a href="https://www.autodesk.com/company/terms-of-use/jp/general-terms" rel="noopener" target="_blank">使用規約（Terms Of Use ）</a></strong>」になっています。</li>
</ul>
<p>使用規約上の具体的な記述は次の項目が該当することになります。</p>
<p><a href="https://www.autodesk.com/company/terms-of-use/jp/general-terms" rel="noopener" target="_blank"><strong>Terms</strong> <strong>Of</strong> <strong>Use</strong> <strong>– </strong><strong>一般規約</strong></a></p>
<p style="padding-left: 40px;"><strong>18.定義</strong></p>
<p style="padding-left: 40px;">...</p>
<p style="padding-left: 40px;">「使用許諾対象ユーザー」または「お客様の使用許諾対象ユーザー」とは、(a) お客様 (お客様が個人の場合)、および (b) お客様が提供物のサブスクリプションを取得した<span style="background-color: #ffff00;">特定個人 </span>(お客様の個々の従業員、コンサルタント、および請負業者、ならびに提供物にアクセスし使用するその他の個人など) を意味します。提供物により、お客様が当該提供物の使用許諾対象ユーザーを指定することができる場合、お客様は、当該使用許諾対象ユーザーがかかる提供物にアクセスし使用する前に、当該使用許諾対象ユーザーに本規約の適用について通知を行い、当該使用許諾対象ユーザーから同意を得る責任を負うものとします。</p>
<p style="padding-left: 40px;">...</p>
<p><a href="https://www.autodesk.com/company/terms-of-use/jp/offering-types-and-benefits#single-user" rel="noopener" target="_blank"><strong>Terms Of Use – 提供方法と特典</strong></a><strong><br /></strong></p>
<div class="title">
<div class="cmp-title" id="title-5e4a262064">&#0160;</div>
</div>
<div class="text">
<div class="cmp-text " id="text-7f22f8c164">
<p style="padding-left: 40px;"><strong>シングルユーザー</strong></p>
<p style="padding-left: 40px;"><strong>シングルユーザーは、オートデスク製品を個人で使用しようとするユーザーをサポートします。</strong></p>
<p style="padding-left: 40px;">シングルユーザー向け提供物では、各使用許諾対象ユーザーに固有の Autodesk ID を割り当てる必要があります。使用許諾対象ユーザーが各シングルユーザー向け<span style="background-color: #ffff00;">提供物のインストールやアクセスを行うときは自身の Autodesk ID を使用してログインする必要があり、他のいかなる者も、同一の Autodesk ID を使用して、当該提供物にアクセスまたはこれを使用することはできません。</span></p>
<p style="padding-left: 40px;">お客様の使用許諾対象ユーザーは、ソフトウェアを最大 3 台の電子デバイスにインストールすることができますが、当該ソフトウェアにアクセスするために一度に使用できる電子デバイスは 1 台に限られます。いかなるコレクションにおいても、お客様の使用許諾対象ユーザーは、コレクション内の各提供物タイトルのソフトウェアを最大 3 台の電子デバイスにインストールすることができます。お客様の使用許諾対象ユーザーは、コレクション内の任意のソフトウェアタイトルを同時に使用できますが、コレクション内の任意のソフトウェアタイトルにアクセスするために一度に使用できる電子デバイスは 1 台に限られます。</p>
<p style="padding-left: 40px;">...</p>
</div>
</div>
<p>具体的な例を考えてみます。</p>
<p>コンピュータにインストールされた製品の指名ユーザーが「A さん」の場合、自動化によって得られる結果は「A さん」が機能を享受することになるため、その自動化使用に問題はありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39d4ee8200b-pi" style="display: inline;"><img alt="Pattern1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39d4ee8200b img-responsive" src="/assets/image_648869.jpg" title="Pattern1" /></a></p>
<p>ただ、社内ネットワークやインターネットを介したリクエストで自動化を実行する場合、指名ユーザーの「A さん」以外は、直接的であれ、間接的であれ、その機能を享受することは出来ない、ということになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3992d11200c-pi" style="display: inline;"><img alt="Pattern2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3992d11200c image-full img-responsive" src="/assets/image_190119.jpg" title="Pattern2" /></a></p>
<p>結果を享受する人員が指名ユーザー以外の場合、また、不特定多数になる場合には、クラウドを使った<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/autodesk-platform-services.html" rel="noopener" target="_blank"><strong> Autodesk Platform Services</strong></a>（旧&#0160;Forge ）の <a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-basics.html" rel="noopener" target="_blank"><strong>Design Automation API</strong> </a>による自動化が推奨されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39d4f21200b-pi" style="display: inline;"><img alt="Design_automation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39d4f21200b image-full img-responsive" src="/assets/image_443505.jpg" title="Design_automation" /></a></p>
<p><span style="background-color: #ffff00;">（2023年9月14日更新）</span></p>
<p>By Toshiaki Isezaki</p>
