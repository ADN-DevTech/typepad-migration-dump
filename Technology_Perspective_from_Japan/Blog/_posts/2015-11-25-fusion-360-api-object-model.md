---
layout: "post"
title: "Fusion 360 API：オブジェクト モデル"
date: "2015-11-25 01:37:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/11/fusion-360-api-object-model.html "
typepad_basename: "fusion-360-api-object-model"
typepad_status: "Publish"
---

<p>今回は Fusion 360 API を理解し易くするために、オブジェクト モデルについて、少し深く説明していきます。オブジェクト モデルを見ることで、Fusion 360 API で扱うオブジェクトの特性やオブジェクト間の関係を把握することが出来ます。</p>
<p><img alt="Object_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0887e087970d image-full img-responsive" src="/assets/image_216495.jpg" style="width: 599.458px;" title="Object_model" /></p>
<p>なお、現在入手できる<a href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/images/Fusion.pdf" target="_blank"><strong>オブジェクト モデル</strong></a>は、現時点で API からコントロール出来る範囲のオブジェクトだけが表示されています。今後のリリースで API でサポートされるオブジェクトが増加する予定なので、順次内容が更新されていくはずです。</p>
<p><strong>オブジェクトモデルの凡例</strong></p>
<p>具体的な説明の前に、このオブジェクト モデルの凡例について触れておきたいと思います。10月のアップデートで Fusion 360 自体は日本語化されていますが、残念ながら、オンライヘルプは英語のままです。オブジェクト モデルも同様なので、今後の説明のために、オブジェクト モデルの凡例だけを日本語化しておきましょう。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f02263970b-pi" style="display: inline;"><img alt="Legend" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f02263970b image-full img-responsive" src="/assets/image_464467.jpg" title="Legend" /></a></p>
<p><strong><strong>オブジェクト モデルの見方</strong></strong></p>
<p>Fusion 360 API のオブジェクト モデルでは、その見方によって複数の意味を見出すことが出来ます。オブジェクト間が線分で結ばれている関係は、後述するコレクション オブジェクトと、コレクション オブジェクトに所有されるオブジェクトの関係を表しています。この関係は、Fusion 360 がモデルを表示して際に、メモリに展開されたオブジェクトの関係と考えることが出来ます。この関係を<strong>オーナーシップ</strong>と呼びます。API では、このオーナーシップを利用して、関連するオブジェクトにアクセスしていきます。</p>
<p>オーナーシップとは別に、個々のオブジェクト枠内の&#0160;&#0160;<strong>[x]</strong>&#0160;印や (x) 印で、オブジェクト間の派生関係を読み取ることが出来ます。この派生関係、あるいは、継承関係も後述することにします。</p>
<p>オブジェクト モデルの一部の背景が着色されていますが、これは、Fusion 360 上の機能別の色分けです。凡例のとおり、モデリング機能やユーザ インタフェース機能などに分けられています。今後のリリースでは、現在、API でサポートされていない CAM やシミュレーション、レンダリングなどの機能により、この色分け部分が増加する予定です。</p>
<p><strong><strong>オブジェクトの種別</strong></strong></p>
<p>オブジェクトモデル内のオブジェクトには、いくつか論理的に異なる種別があります。</p>
<p style="padding-left: 30px;"><strong>標準オブジェクト</strong></p>
<p style="padding-left: 30px;">オブジェクト モデルに表示されている枠付きの表記は、すべてオブジェクトを示しています。すべてのオブジェクトは、Fusion 360 で扱うタイムラインやデータパネルといった機能やユーザ インタフェース要素、また、モデル内のフィーチャやボディなどのオブジェクトに 1:1 で対応しています。</p>
<p style="padding-left: 30px;">個々のオブジェクトには、オブジェクトの振る舞いを指定する<strong>メソッド</strong>、オブジェクトの外観などの属性を指定する<strong>プロパティ</strong>にアクセスすることが出来ます。すべてのオブジェクトには、次の基本的なメソッドとプロパティが備わっています。</p>
<p style="padding-left: 60px;"><strong> objectType</strong><br />オブジェクトのタイプを示す文字列を返すプロパティです。</p>
<p style="padding-left: 60px;"><strong>classType</strong><br />特定のクラスに関連付けられた名前を返すメソッドです。このメソッドを objectType プロパティとともに参照することで、オブジェクトの特定のタイプを把握することが出来ます。</p>
<p style="padding-left: 60px;"><strong>IsValid</strong><br />オブジェクトが有効な状態になっているか、ブール値（true か false）で返すプロパティです。オブジェクトが削除された状態だったり、不正なアクションで無効になってる場合には false を返します。</p>
<p style="padding-left: 30px;"><strong>コレクション オブジェクト</strong></p>
<p style="padding-left: 30px;">標準オブジェクトを複数個格所有するオブジェクトです。Fusion 360 には、多くのフィーチャが存在しますが、これらはタイプ別に同じ系統のコレクション オブジェクトが所有するかたちをとっています。例えば、&#0160;ExtrudeFeature オブジェクトは、ExtrudeFeatures コレクションに所有されていると考えることができます。この「<strong>つながり</strong>」が<strong>オーナーシップ</strong>です。</p>
<p style="padding-left: 30px;">すべてのコレクション オブジェクトは、所有しているオブジェクトをインデックス化して管理しています。このインデックスを利用してオブジェクトにアクセスするため、次のメソッドとプロパティが用意されています。</p>
<p style="padding-left: 60px;"><strong>count</strong><br />コレクションにいくつのオブジェクトが格納されているか、その数を返すプロパティです。</p>
<p style="padding-left: 60px;"><strong>item</strong><br />コレクションが所有するオブジェクトにインデックスを使ってアクセスするためのメソッドです。コレクション内の最初のオブジェクトには、インデックス値 0（ゼロ） でアクセスすることが出来ます。このため、count プロパティでコレクション内のオブジェクト数を取得した場合には、最後のオブジェクトには、<em>xxx.</em><strong>item</strong>(<em>xxx.</em><strong>count-1</strong>) のような書き方でアクセスすることが出来ます（xxx はオブジェクトを示します）。</p>
<p style="padding-left: 30px;">コレクション内のオブジェクトには、一意な名前や ID が与えられている場合があります。そのような場合には、<strong>itemByName</strong> メソッドや <strong>itemById</strong> メソッドを使って、直接コレクション内のオブジェクトにアクセスすることも出来ます。</p>
<p style="padding-left: 30px;">コレクション オブジェクトには、所有するオブジェクトを作成するためのメソッドが用意されてい場合もあります。例えば、ExtrudeFeatures コレクションには、ExtrudeFeature オブジェクトを新規にコレクションに追加する Add メソッドが用意されています。</p>
<p style="padding-left: 30px;"><strong>リスト オブジェクト</strong></p>
<p style="padding-left: 30px;">凡例には明記されていませんが、コレクション オブジェクトの一種です。所有するオブジェクトを返すだけで、新規に追加する機能は用意されていません。例えば、パラメータには、ユーザが定義することが出来るユーザ パラメータと、モデリング中に自動的に生成されるモデル パラメータがあります。このモデル パラメータには複数のパラメータが含まれます。</p>
<p style="padding-left: 30px;">Fusion 360 API では、モデル パラメータ コレクションを ModelParamters コレクション オブジェクト、個々のモデル パラメータを ModelParameter オブジェクトとして表現しますが、前述のとおり、モデル パラメータはモデリング操作によって生成されるので、Model Parameters コレクションには、ModelParameter オブジェクトを追加するためのメソッドが用意されていません。あくまで、所有する ModelParameter オブジェクトを返すのみです。これが、リスト オブジェクトです。</p>
<p style="padding-left: 30px;"><strong>インプット オブジェクト</strong></p>
<p style="padding-left: 30px;">凡例には明記されていませんが、インプット オブジェクトは、複雑なオブジェクトを生成するために必要となる入力値を集約、管理するためのオブジェクトです。例えるなら、押し出しフィーチャを作成する際のダイアログには、押し出しフィーチャの作成に必要な入力値が集約されています。このような情報を一括して扱うのが、インプット オブジェクトです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f117b7970b-pi" style="display: inline;"><img alt="Extrude_dialog" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f117b7970b img-responsive" src="/assets/image_944868.jpg" title="Extrude_dialog" /></a></p>
<p style="padding-left: 30px;">[押し出し] ダイアログでは、選択するオプションによって、入力すべき項目や値が変化します。API 上のインプット オブジェクトも同様です。</p>
<p style="padding-left: 30px;">押し出しフィーチャを API で作成する場合には、ExtrudeFeatures コレクションの createInput メソッドで押し出しフィーチャ用のインプット オブジェクトを生成することが出来ます。</p>
<p style="padding-left: 30px;">インプット オブジェクトには、特定タイプの入力を扱う ValueInput オブジェクトも存在します。ValueInput オブジェクトが扱うことが出来るのは 実数値 と 文字列 で、それぞれ createByReal メソッドと createByString メソッドで作成します。いずれかの値を持った ValueInput オブジェクトは、前述の&#0160;ExtrudeFeatures.createInput メソッドで作成した&#0160;ExtrudeFeatureInput オブジェクトに設定して利用することになります。例えば、押し出しフィーチャは、次にようなコードで作成していきます（JavaScript 例）。</p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">// スケッチ プロファイルを取得<br />var prof = sketch.profiles.item(0);</span></p>
<p style="padding-left: 30px;"><span style="font-size: 10pt;"><span style="font-family: &#39;courier new&#39;, courier;">//&#0160;ExtrudeFeatures&#0160;コレクションを取得<br /></span><span style="font-family: &#39;courier new&#39;, courier;">var extrudes = rootComp.features.extrudeFeatures;</span></span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">//&#0160;ExtrudeFeatureInput オブジェクトを作成<br />var extInput = extrudes.createInput(prof, <br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; adsk.fusion.FeatureOperations.NewBodyFeatureOperation);<br /></span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">// 押し出し距離をインプット オブジェクトで生成<br />var distance = adsk.core.ValueInput.createByReal(5);</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">//&#0160;ExtrudeFeatureInput オブジェクトにインプット オブジェクトを設定<br />xtInput.setDistanceExtent(false, distance);</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">// 押し出しフィーチャを作成<br />var ext = extrudes.add(extInput);</span></p>
<p style="padding-left: 30px;"><strong>定義オブジェクト</strong></p>
<p style="padding-left: 30px;">これも凡例には明記されていませんが、インプット オブジェクトの一種です。フィーチャの作成時ではなく、既存のフィーチャの編集で使用するオブジェクトです。フィーチャ オブジェクトを作成する際に利用するインプット オブジェクトは各種値の設定が可能ですが、定義オブジェクトは、フィーチャを編集するための ModelParameter オブジェクトを読み取り専用プロパティを介して返します。</p>
<p style="padding-left: 30px;">例えば、ExtendFeature.distance プロパティは、延長サーフェス フィーチャが持つ距離値を示す ModelParameter オブジェクトを返します。</p>
<p><strong>基本オブジェクトと派生オブジェクト</strong></p>
<p>オブジェクト モデルでは、すべてのオブジェクトが枠付きで表現されています。Fuison 360 API のオブジェクト モデルは、AutoCAD の COM API の<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-A809CD71-4655-44E2-B674-1FE200B9FE30" target="_blank"><strong>オブジェクト モデル</strong></a>と似ていますが、AutoCAD COM API のオブジェクト モデルが図面を AutoCAD 上に開いた際のメモリ内のデータの所有状況を表すのに対して、各オブジェクトの派生構造も示す点が少し異なります。</p>
<p>オブジェクトモデル内のオブジェクトには、凡例にある <strong>[x]</strong> 印や (x) 印のあるオブジェクトが存在します。&#0160;<strong>[x]</strong>&#0160;印のあるオブジェクトは基本オブジェクト、(x) 印のあるオブジェクトが派生オブジェクトと呼ばれます。少し難しくなりますが、ちょうど、C++ クラス ライブラリの基本クラス（基底クラス）と派生クラスの関係と同じです。</p>
<p>つまり、次のような関係を説明することが出来ます。</p>
<ul>
<li>派生オブジェクトは、同じ系統の基本オブジェクトを持つ。</li>
<li>ここで言う同じ系統とは、[] や <em>()</em> 内のアルファベットで識別できる。</li>
<li>派生オブジェクトとは、同じ系統の基本オブジェクトが持つ特性を<a href="https://ja.wikipedia.org/wiki/%E7%B6%99%E6%89%BF_(%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0)" target="_blank"><strong>継承</strong></a>する。</li>
<li>ここで言う特性とは、API &#0160;上ではメソッドやプロパティと言い換えることが出来る。</li>
<li>派生オブジェクトには、基本オブジェクトにない特性が追加されていて、基本オブジェクトとは異なる固有の振る舞いを持つ。</li>
</ul>
<p>例えば、下記のようにオブジェクトモデルを抜粋してみます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d17ad9ef970c-pi" style="display: inline;"><img alt="Inheritance" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d17ad9ef970c img-responsive" src="/assets/image_844037.jpg" title="Inheritance" /></a></p>
<p>この抜粋からは、<a href="http://fusion360.autodesk.com/learning/learning.html?guid=GUID-A92A4B10-3781-4925-94C6-47DA85A4F65A" target="_blank"><strong>Feature</strong></a> オブジェクトが基本オブジェクトであることがわかります。また、<a href="http://fusion360.autodesk.com/learning/learning.html?guid=GUID-A92A4B10-3781-4925-94C6-47DA85A4F65A" target="_blank"><strong>BaseFeature</strong></a> オブジェクト、<strong><a href="http://fusion360.autodesk.com/learning/learning.html?guid=GUID-A92A4B10-3781-4925-94C6-47DA85A4F65A" target="_blank">BondaryFillFeature</a></strong> オブジェクト、<a href="http://fusion360.autodesk.com/learning/learning.html?guid=GUID-A92A4B10-3781-4925-94C6-47DA85A4F65A" target="_blank"><strong>BoxFeature</strong></a> オブジェクト、<a href="http://fusion360.autodesk.com/learning/learning.html?guid=GUID-A92A4B10-3781-4925-94C6-47DA85A4F65A" target="_blank"><strong>ChamferFeature</strong></a> オブジェクトが、Feature オブジェクトから派生していて、Feature オブジェクトが持つメソッドとプロパティを継承しています。オンラインヘルプで個々のオブジェクトの詳細を見てみると、派生オブジェクトは、基本オブジェクトが持つメソッドやプロパティをすべて持ちますが、派生オブジェクトには、派生オブジェクトにしかない固有のメソッドやプロパティを持っていることも分かります。</p>
<p><strong>[x]</strong>&#0160;印や (x) 印の意味が分かれば、そのオブジェクトの特性をオンラインヘルプなしで概要把握できます。</p>
<p>By Toshiaki Isezaki</p>
