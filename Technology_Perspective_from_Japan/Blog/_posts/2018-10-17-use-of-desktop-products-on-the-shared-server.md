---
layout: "post"
title: "デスクトップ製品の共有サーバー（クラウド）利用について ~ アップデート"
date: "2018-10-17 00:36:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/10/use-of-desktop-products-on-the-shared-server.html "
typepad_basename: "use-of-desktop-products-on-the-shared-server"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3953591200d-pi" style="float: right;"><img alt="Autodesk-licensed-software-icon-gray" class="asset  asset-image at-xid-6a0167607c2431970b022ad3953591200d img-responsive" src="/assets/image_264892.jpg" style="margin: 0px 0px 5px 5px;" title="Autodesk-licensed-software-icon-gray" /></a>以前、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/08/use-of-desktop-products-on-the-shared-server.html" rel="noopener noreferrer" target="_blank">デスクトップ製品の共有サーバー利用について</a></strong> の記事で、AutoCAD や Revit、Inventor などのデスクトップ製品を共有サーバーやクラウド上の仮想環境へインストール、 API カスタマイズを施して自動化させるソリューションの構築について、SLA（Software License Agreement）に照らし合わせながら、コンプライアンス上の問題点についてご紹介したことがあります。</p>
<p>ご存知とは思いますが、このブログ記事の記載と前後して&#0160;<strong><a href="https://www.autodesk.co.jp/products/perpetual-licenses" rel="noopener noreferrer" target="_blank">永久ライセンスの新規販売停止とサブスクリプションへの移行</a>&#0160;</strong><a href="https://www.autodesk.co.jp/products/perpetual-licenses" rel="noopener noreferrer" target="_blank"></a>が発表<a href="https://www.autodesk.co.jp/products/perpetual-licenses" rel="noopener noreferrer" target="_blank"> </a>されています。そして、今年（2018年）の 5 月にデスクトップ製品のサブスクリプション販売にマッチするよう、デスクトップ製品用の SLA が簡素化して改定されています。新しい SLA は&#0160;<a href="https://www.autodesk.com/company/terms-of-use/jp/general-terms" rel="noopener noreferrer" target="_blank"><strong>https://www.autodesk.com/company/terms-of-use/jp/general-terms</strong></a> でご確認いただけます。</p>
<p>新 SLA でも上記運用方法への懸念、ないし問題点、推奨する代替方法に大きな変更はありません。ただ、以前触れた<strong>&#0160;2.1.1&#0160;<u>ライセンス付与の排除、許諾されない行為</u></strong> も含め、新 SLA の内容が大きく変わってしまっているので、今回は、新 SLA での当該部分について言及しておきたいと思います。</p>
<p>前回、懸念、ないし問題点として触れたホスティング ビジネスは、新 SLA 上では <strong>15. 使用制限</strong> &gt;&gt; <strong>15.3 提供物の利用の許容範囲</strong> に記載の<span style="background-color: #ffff00;">項目</span>で禁止されています。また、新しい<span style="background-color: #ffbfff;">項目</span>では、自動化や AI への流用も禁止していることがわかります。</p>
<hr />
<div class="heading section">
<h4>15.3 提供物の利用の許容範囲</h4>
</div>
<div class="text section">
<p class="wd-font-14">お客様は、全ての適用法に従ってのみ提供物のアクセス及び利用（並びにアクセス及び利用の許可）を行います（かつ全ての当該適用法に従います）。本規約（追加規約若しくは特別規約を含む）で明示的に許可された場合を除き、又はオートデスクが書面で明示的に別段の許可を行った場合を除き、お客様は、以下の行為を行いません。</p>
<ul>
<li>提供物の全て又は一部の複製、改変、翻案、翻訳、移植、又はこれの二次的著作物の作成、ただし、反対の趣旨の契約上の禁止に関わらず、適用法で明示的に許可された場合を除く。</li>
<li>提供物（提供物の機能を含む）の全て若しくは一部の第三者へのサブライセンスの許諾、配布、送信、販売、賃貸、貸付、若しくはその他の方法で利用可能とすること、又は（サービスビューロベースその他による）第三者への提供物の機能の提供</li>
<li><span style="background-color: #ffff00;">インターネット、広域ネットワーク（WAN）、その他のローカルでないネットワーク、仮想プライベートネットワーク（VPN）、アプリケーション仮想化技術、リモート仮想化技術、ウェブホスティング、タイムシェアリング、サービスとしてのソフトウェア、サービスとしてのプラットフォーム、サービスとしてのインフラ、クラウドその他のウェブベース、ホスト型等のサービス上又はこれらを通じての提供物のアクセス若しくは利用（オートデスクによるインターネットを通じた提供を除く）</span></li>
</ul>
<p class="wd-font-14">さらに、お客様は以下の行為を行いません。</p>
<ul>
<li>提供物、ドキュメント、又は関係する素材から著作権、商標、機密保持、その他の専有権の通知を取り除くこと</li>
<li>オートデスクが、(i)提供物のインストール、アクセス、若しくは利用の管理、監視、支配、若しくは分析、又は(ii)オートデスクの知的財産権の保護を行うために使用する技術的保護の有効性の除去、無効化、若しくはその他の方法で制限すること</li>
<li>以下に該当するか又は該当する可能性のある情報又は素材を、提供物を利用して、掲載、送信、又はその他の方法で提供すること
<ul>
<li>虚偽、名誉毀損、中傷的、詐欺的、その他違法若しくは不法であるもの</li>
<li>脅迫的、嫌がらせ、名誉毀損、憎むべき、若しくは脅威的、その他、他者の権利及び尊厳を尊重しないもの</li>
<li>わいせつ、下品、ポルノ、その他不愉快なもの</li>
<li>著作権、商標、意匠権、企業秘密に関する権利、肖像権若しくはプライバシーの権利、その他の専有権で保護されている（該当する所有者の事前の書面による明示的な同意を得ていない場合）もの</li>
<li>国家機密、軍事情報、その他の正式な機密保持の扱いを受ける情報若しくは素材（写真、図面、計画、若しくはモデルを含む）</li>
<li>暗号、合言葉、暗号通貨、パスワード、その他同様の情報</li>
<li>広告、スパム、商品若しくはサービスの売買の申し出、「チェーンレター」、その他の形式の勧誘</li>
<li>マルウェア（ウイルス、ワーム、トロイの木馬、イースターエッグ、時限爆弾、若しくはスパイウェア等）、若しくは潜在的に有害若しくは侵略的であり、若しくはハードウェア、ソフトウェア、若しくは機器に損害を与え、それらの稼働を乗っ取り、それらの使用を制限し、若しくはそれらの使用を監視することを目的としたその他のコンピュータコード、ファイル、若しくはプログラム</li>
</ul>
</li>
<li>詐欺的その他違法若しくは不法であり、又は詐欺的その他違法若しくは不法な目的若しくは効果を有する方法で提供物を利用すること</li>
<li>提供物又は提供物の提供に使用されるサーバ若しくはネットワークの稼働を妨げ又は中断させること（提供物の一部のハッキング又は破損によるものを含む）</li>
<li>提供物の脆弱性の精査、検査、若しくは試験、又は提供物が採用するセキュリティ対策若しくは認証対策の突破又は回避を試みること</li>
<li><span style="background-color: #ffff00;">提供物を「遠隔ロード」のための記憶装置として又はその他のウェブページ若しくはインターネットリソースへの「入り口」若しくは「道標」として使用すること（提供物が提供されるサイト内であるか又はサイトの域内を越えるかを問わない）</span></li>
<li>他の個人若しくは事業体になりすまし、又は個人若しくは事業体とお客様との関係を偽って宣言するか若しくはその他の方法で虚偽表示すること</li>
<li>提供物を本質的に危険なアプリケーションに関連して使用すること（死亡、人身傷害、破壊的な被害、又は大量破　壊をもたらす可能性のあるアプリケーションを含む）</li>
<li><span style="background-color: #ffbfff;">自動化手段（ロボット、スパイダー、サイト検索／検索アプリケーション、又は検索、インデックスの作成、「取得」又は「情報採掘」を行うその他の機器等）を利用して、提供物から又はこれに含まれるコンテンツ又は情報を収集すること</span></li>
<li><span style="background-color: #ffbfff;">ニュートラルネットワークの研修、又は機械学習、深層学習、若しくは人工知能システム又はソフトウェアに関連して、提供物又は提供物の出力を利用すること</span></li>
<li>提供物の構成部品を、お互いを別々にして使用するため又は異なる電子機器で使用するために切り離すこと（オートデスクが書面により明示的に許可した場合を除く）</li>
<li>Web サービスの一部として提供されるソフトウェアを該当するWeb サービスと切り離して使用又はアクセスすること（オートデスクが書面により明示的に許可した場合を除く）</li>
</ul>
</div>
<hr />
<p>また、少し違った視点では、下記、<strong>9. ソフトウェア</strong> の<span style="background-color: #ffff00;">項目</span>で、1 ライセンス当たりの複数ユーザ（含む、不特定多数）の利用も禁止していることがわかります。</p>
<hr />
<div class="heading section">
<h3 class="pc-brand wd-font-19">9. ソフトウェア</h3>
</div>
<div class="text section">
<p class="wd-font-14">お客様が引渡しを要するソフトウェアを注文した場合、又はソフトウェアを含む提供物（例えば、Web サービス提供物がクライアント ソフトウェアを要求する場合）を注文した場合には、当該ソフトウェアは、オートデスクの裁量で、お客様のアカウントその他の電子的手段を通じダウンロード向けに提供されるか又はオートデスク若しくはオートデスクが許諾した第三者によりお客様へ引渡しが行われます。ソフトウェアの物理的媒体その他の有体物を用いた引渡しについては、追加料金が適用される場合があります。ソフトウェアの引渡しがどのように行われようと、オートデスクは、引渡し遅延又は誤配送を理由としてお客様その他の者が被った損失その他の責任について、これを負担する義務を負いません。</p>
<p class="wd-font-14">オートデスクがお客様に提供又は引き渡すソフトウェアで構成される提供物については、本規約及び全ての支払い義務の遵守を条件に、オートデスクは、お客様のサブスクリプション期間中、以下に該当する場合に限りソフトウェアのインストール及び使用（並びにお客様の使用許諾対象ユーザーによるソフトウェアのインストール及び使用の許可）を行うことができる非排他的、サブライセンスの許諾不可、譲渡不可のライセンスをお客様に許諾します。(i)提供物に関するドキュメント及び適用される特別規約（該当する場合）に従った場合、かつ(ii)お客様のサブスクリプションの範囲内である場合（許可番号、ライセンス タイプ、領土、並びに提供物契約時にお客様が選択したタイプ及びレベルを規定したその他の属性を含む）。お客様の提供物ID又はお客様のサブスクリプションに関するオートデスクからの確認書に、当該属性の記載が１つ以上ない場合、当該ライセンスは、(a)トライアルバージョンであり、<span style="background-color: #ffff00;">(b)個人としてのお客様向け、又はお客様が企業その他の法人である場合、指名された1名の従業員向けであり、</span>かつ(c) お客様が提供物を取得した国又は裁判管轄内に限った使用向けとなります。お客様は、当該ライセンス及び本規約により許諾された以外のソフトウェアをインストール、アクセス、若しくは使用（又はインストール、アクセス、若しくは使用の許可）を行うことはできず、その他のインストール、アクセス、又は使用については認められていません。</p>
</div>
<hr />
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3739c3b200c-pi" style="float: right;"><img alt="Icon - design automation api" class="asset  asset-image at-xid-6a0167607c2431970b022ad3739c3b200c img-responsive" src="/assets/image_159480.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - design automation api" /></a>このように、従来と変わらず、API カスタマイズでのホスティング運用は現実的ではありません。<br />そして、上記のような SLA 上の問題を回避して、クラウド リソースを適切に活用する API ソリューションとして <a href="https://adndevblog.typepad.com/technology_perspective/2019/06/design-automation-api-for-autocad.html" rel="noopener noreferrer" target="_blank"><strong>Design Automation API</strong></a> を用意しています。現時点で対応するコア プロセスは AutoCAD のみですが、2017 年に Forge DevCon Las Vegas でアナウンスがあったとおり、近い将来、Revit、Inventor、3ds Max のコア エンジンが Design Automation API に加わる予定です。</p>
<p>By Toshiaki Isezaki</p>
