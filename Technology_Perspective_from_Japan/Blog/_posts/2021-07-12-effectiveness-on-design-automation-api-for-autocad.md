---
layout: "post"
title: "Design Automation API for AutoCAD の効果"
date: "2021-07-12 02:51:40"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/07/effectiveness-on-design-automation-api-for-autocad.html "
typepad_basename: "effectiveness-on-design-automation-api-for-autocad"
typepad_status: "Publish"
---

<p>Forge の Design Automation API はオートデスクがクラウド上に用意したコアエンジン（AutoCAD、Revit、Inventor、3ds Max）にアドイン アプリをロードして実行出来る環境です。</p>
<p>実行時の作業領域は処理毎に動的に仮想環境上に用意され、指定したクラウド ストレージから処理に必要な素材ファイルを作業環境へダウンロード、アドインによる処理で作成、あるいは、編集された成果ファイルを同じく指定したクラウド ストレージにアップロードします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded207ed200c-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bded207ed200c image-full img-responsive" src="/assets/image_506180.jpg" title="Da" /></a></p>
<p>素材ファイルは必要に応じてローカル コンピュータからクラウド ストレージへアップロードしたり、成果ファイルをクラウド ストレージからローカル コンピュータにダウンロードすることが出来ます。セキュリティ上、ローカル コンピュータと Design Automation API の作業領域の間で、直接、ファイルをアップロード/ダウンロードすることは出来ません。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/06/design-automation-api-for-autocad.html" rel="noopener" target="_blank"><strong>Design Automation API for AutoCAD 概説</strong></a> でも触れていますが、Design Automation API にはユーザ インタフェースはありませんので、同 API を利用する Forge アプリ（＝ Web アプリ）は、HTML で必要な情報を入力、表示する Web ページを用意する必要があります。</p>
<p><strong>それでは、どのようなアドイン処理が考えられるのでしょう？</strong></p>
<p>Design Automation API for AutoCAD で考えてみます。非常にシンプルで基本的なシナリオを 3 つご紹介します。</p>
<p><strong>例１）図面の作成・編集</strong></p>
<p style="padding-left: 40px;">次の例では、与えられた値に沿ってコイル状のスイープ ソリッドを作成して図面レイアウトを作成しています。作成した図面ファイルは、クラウド ストレージに保存されているので、ローカル コンピュータにダウンロードすると、ローカル コンピュータの AutoCAD で開いて編集することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788029f601200d-pi" style="display: inline;"><img alt="Coil_creation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788029f601200d image-full img-responsive" src="/assets/image_554585.jpg" title="Coil_creation" /></a></p>
<p><strong>例２）PDF 印刷・別ファイル形式への変換</strong></p>
<p style="padding-left: 40px;">DWG ファイルは、AutoCAD や DWG TrueView などの設計者用のソフトウェアがインストールされていないと、ワークフローに組み込んで閲覧や朱書きを実施することが難しいデータ形式です。その点、PDF ファイルは一般化していて、各種ワークフローでの利用や配布に便利な場合があります。AutoCAD コアエンジンも AutoCAD と同じように PDF へ &quot;印刷&quot; が出来るので、ローカル コンピュータからアップロードした DWG ファイルを PDF ファイル化してローカル コンピュータにダウンロードすることが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788029f0f5200d-pi" style="display: inline;"><img alt="Pdf_output" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788029f0f5200d image-full img-responsive" src="/assets/image_893212.jpg" title="Pdf_output" /></a></p>
<p><strong>例３）図面内容の解析</strong></p>
<p style="padding-left: 40px;">ローカル コンピュータの AutoCAD を使って設計者が作成した図面ファイルは、関係者の間で ”流通” していくことになります。その間にブロックの挿入や配置ブロックの削除を繰り返すと、モデル空間やペーパ空間（レイアウト）にブロックが挿入されていなくても、ブロックの定義情報が蓄積されて、図面ファイルのサイズが肥大化してしまうことがよくあります。</p>
<p style="padding-left: 40px;">AutoCAD には不要になったブロック定義を削除する <a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-D68BA47B-A79D-4F58-9715-0569CC24BCEF" rel="noopener" target="_blank">PURGE [名前削除] コマンド</a>&#0160;が用意されています。アドインを開発するための AutoCAD API にも同等の API の用意があるので、これをコアエンジンにロード・実行させて図面ファイルのサイズ肥大化を抑止していくことが出来ます。この際、公開されているオープンソースや他社の Web API を利用すると、削除したブロック定義に含まれていた図形数をグラフ化してレポート機能を持たせるようなことも可能です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded203fd200c-pi" style="display: inline;"><img alt="Block_cleaner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bded203fd200c image-full img-responsive" src="/assets/image_518007.jpg" title="Block_cleaner" /></a></p>
<p><strong>ローカル vs. クラウド</strong></p>
<p style="padding-left: 40px;">ここでご紹介した例は、細分化された単一のタスクを Design Automation API for AutoCAD を使ってクラウド上で実行しています。いずれの内容も、ローカル コンピュータにインストールした AutoCAD でも実現出来るものです。もちろん、すべてのタスクを 1 つにまとめたバッチ処理も可能ですし、他の処理を加えることもアドイン次第で自由です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded22889200c-pi" style="display: inline;"><img alt="Various_scenario" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bded22889200c image-full img-responsive" src="/assets/image_901176.jpg" title="Various_scenario" /></a></p>
<p style="padding-left: 40px;"><strong>では、これらをクラウドで実行する利点とはどのようなものでしょう？</strong></p>
<p style="padding-left: 40px;">多くの場合、アドイン アプリに求められるのは、図面作成・編集の生産性を向上させるカスタム コマンドの作成です。それらカスタム コマンドの内容は、通常、次のようなものです。</p>
<ul>
<li>作図補助機能の提供</li>
<li>反復タスクの自動化の提供</li>
<li>図面内情報を利用した外部システムの連携（図面内の情報抽出を含む）</li>
</ul>
<p style="padding-left: 40px;">ここで、Forge が Web テクノロジをベースにした、インターネットに非常に高い親和性の高い開発プラットフォームであることを思い出してください。このため、ローカル コンピュータの AutoCAD で処理させるよりも、いくつかの点で優れた点を見出すことが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802a16c9200d-pi" style="display: inline;"><img alt="Web_technologies" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802a16c9200d image-full img-responsive" src="/assets/image_601225.jpg" title="Web_technologies" /></a></p>
<ul>
<li>成果物を得るのに AutoCAD のライセンス購入や AutoCAD の操作知識は不要</li>
<li>HTML5、CSS3、JavaScript や RESTful API といった Web の標準テクノロジで統一した開発が可能なばかりか、他社やオープンソースの利用が出来るため、選択肢が多く望める</li>
<li>Forge アプリは Web アプリでもあり、アドイン処理自体もクラウドで実行されるので、Web ブラウザがあればデバイスを問わず利用出来る。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788029f872200d-pi" style="display: inline;"><img alt="Multiple_devices" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788029f872200d image-full img-responsive" src="/assets/image_505387.jpg" title="Multiple_devices" /></a></li>
</ul>
<p style="padding-left: 40px;">このように考えてみると、従来のローカル環境でのアドイン アプリの運用は、設計部を主体としたものになっていたことに気が付きます。逆に、クラウド環境を使った Design Automation API の運用は、設計業務を超えた他部署や関係企業、エンドユーザを含めた連携と自動化が可能なことになります。つまり、<strong>デジタル トランスフォーメーション（DX）</strong>に直結することが理解出来るかと思います。具体的には、次ような内容を容易に実現することが可能になはずです。</p>
<ul>
<li>営業担当者がお客様の要望に応じて製品の仕様（サイズ、タイプ、数量など）を Web ページに入力、DA4A を起動、アドインをロード・実行して、入力値に沿った見積もり図面を生成して返す。（出先で直ぐに検討図面や見積もりが出せれば、競合業者に対する強みになるのだが。）</li>
<li>外出先（在宅）で引き合いのあるお客様から取り扱い製品の PDF 図面提出を依頼された。（会社に戻って設計担当者に PDF 図面の出力を依頼しなければならない。）</li>
<li>多数の製品図面からブロック（ブロック属性）を集計して月に一回コストを算出したい。（経理 システム連携が出来れば便利なのだが。）</li>
<li>異なるオフィスに分散している設計部間で、外部へ図面を持ち出すための出図承認管理のワークフローを確立したい。</li>
<li>自社が販売する製品の図面を外部企業の設計用に DWG や DXF でダウンロード提供したいが、希望する形式やバージョン分を事前に用意しておくには数量が多く現実的ではない。提供前にファイル形式や対応バージョンを入力させて、機動的に適切な形式・バージョンの図面を提供したい。</li>
<li>3D モデルを Web コンフィギュレーターに用いて、選択された仕様に基づいた取扱説明書を自動生成させたい。</li>
</ul>
<p>もし、すでに AutoCAD アドイン アプリをお持ちであれば、Design Automation API for AutoCAD で可能になるワークフローを想像してみることをお勧めします。</p>
<p>By Toshiaki Isezaki</p>
