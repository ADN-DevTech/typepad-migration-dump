---
layout: "post"
title: "Revit 2018 の新機能 その5"
date: "2017-06-02 01:58:14"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/06/new-features-on-revit-2018-part5.html "
typepad_basename: "new-features-on-revit-2018-part5"
typepad_status: "Publish"
---

<p>Revit 2018 の新機能について解説させて頂きます。これまでの解説記事は下記のリンクをご参照ください。</p>
<ul>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2017/04/new-features-on-revit-2018-part1.html">Revit 2018 の新機能 その1</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2017/04/new-features-on-revit-2018-part2.html">Revit 2018 の新機能 その2</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2017/05/new-features-on-revit-2018-part3.html">Revit 2018 の新機能 その3</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2017/05/new-features-on-revit-2018-part4.html">Revit 2018 の新機能 その4</a></li>
</ul>
<p>今回は、MEP 設計分野の新機能、更新内容、API の対応状況を解説いたします。</p>
<p><strong><br />MEP 製造用モデリング - [複数点経由のルートを作成]ツール</strong><br />モデル内の点をクリックすることにより、接続されている MEP 製造用パーツの経路を自動的に作成できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/RkaW1SKskng?feature=oembed" width="500"></iframe></p>
<p><strong><br />MEP 製造用モデリング - 傾斜配管</strong><br />[配置]ツールを使用して、製造用部品モデル内で傾斜配管をモデリングできるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/KGLqHDmslCI?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p class="asset-video"><strong>冷温水配管系統の流量と圧力損失の計算 - 解析用配管接続</strong></p>
<p>機械設備と配管の間に解析用接続を追加できるようになりました。解析用接続を追加することにより、物理的な接続を作成する前に、配管経路の流量と圧力損失を計算することができます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/lpTpj1YinbY?feature=oembed" width="500"></iframe></p>
<p><br /><br /></p>
<p><strong>冷温水配管系統の流量と圧力損失の計算 - 流量と圧力損失の計算</strong><br />パフォーマンス向上のために、Revit で密閉式冷温水配管系統の流量と圧力損失の計算をバックグラウンド処理として実行できるようになりました。 この新しい計算方式を有効にするには、[機器設定]ダイアログを使用します。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/UzMZUzjF9Ak?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>ユーザが設定できる建物タイプとスペース タイプ</strong><br />エネルギー解析を行う際に、スペースをよりコンロトールしやすくするために、[建物タイプ/スペース タイプの設定]ダイアログから、建物タイプとスペース タイプを作成、複製、名前変更、削除することができるようになりました。</p>
<p>これに伴い、 API では、建物タイプとスペースタイプに対応する新しい基底クラスとして、HVACLoadType クラスが追加されました。そして、このサブクラスとして、それぞれのタイプを表す HVACLoadSpaceType クラス と HVACLoadBuildingType &#0160;クラスが追加されました。</p>
<p style="padding-left: 30px;">HVACLoadType クラス<br />建物タイプとスペースタイプに対応する新しい基底クラス<br />新しい プロパティ: 換気、1 人あたりの面積、潜熱、照明負荷、電力、熱取得</p>
<ul>
<li>HVACLoadSpaceType クラス<br />スペースに関連付けられるエネルギー解析の用途タイプ（ダイニングエリアやロビーなど）</li>
<li>HVACLoadBuildingType クラス<br />建物に関連づけられるエネルギー解析の用途タイプ（ミュージアムやオフィスなど）</li>
</ul>
<p>&#0160;</p>
<p><strong>建物タイプとスペース タイプ の gbXML への書き出し</strong><br />建物タイプ名とスペース タイプ名を、gbXML の建物タイプ名の説明とスペース タイプ名の説明に書き出すことができるようになりました。 また、カスタムのスペース タイプを gbXML に書き出すこともできます。</p>
<p>&#0160;</p>
<p><strong>建物タイプとスペース タイプの外気入力パラメータ</strong></p>
<p>それぞれの建物タイプとスペース タイプについて、[1 人あたりの外気導入量]、[単位面積あたりの外気導入量]、[1 時間あたりの換気回数]、[外気導入方法]を指定できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/I1eZY-U3uK4?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>電気回路パスの編集</strong><br />電気回路のパスを編集して、目的の設計パスを反映させることができるようになりました。 電圧降下の値は、実際の回路パスの長さに基づいて計算されます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Nzlw35_RQuc?feature=oembed" width="500"></iframe></p>
<p>この機能追加に対応するかたちで、API では下記のようなメソッド・プロパティが追加されております。</p>
<ul>
<li>ElectricalSystem.PathOffset</li>
<li>ElectricalSystem.HasPathOffset</li>
<li>ElectricalSystem.HasCustomCircuitPath</li>
<li>ElectricalSystem.CircuitPathMode</li>
<li>ElectricalSystem.GetCircuitPath()</li>
<li>ElectricalSystem.SetCircuitPath()</li>
<li>ElectricalSystem.IsCircuitPathValid()</li>
</ul>
<p>&#0160;</p>
<p><strong>その他の新しい API : 解析用接続の作成</strong></p>
<p>Revit 2018 では、配管ネットワークに機械設備などを接続するための解析用接続を作成する API が追加されました。詳細につきましては、API リファレンスをご参照ください。</p>
<ul>
<li>MEPAnalyticalConnection クラス<br />
<ul>
<li>MEPAnalyticalConnection.Create() -2 つのコネクタ間に解析用接続を作成します。</li>
<li>MEPAnalyticalConnection.CreateMultipleConnections() - 機器のコネクタと配管ネットワーク上で近接するポイントの間に解析用接続を作成します。</li>
<li>MEPAnalyticalConnection.GetFlow() - 最新のフロー値を取得します。</li>
</ul>
</li>
</ul>
<ul>
<li>MEPAnalyticalConnectionType クラス
<ul>
<li>MEPAnalyticalConnectionType.Create() - 特定の名称で解析用接続のタイプを作成します。</li>
<li>MEPAnalyticalConnectionType.IsNameUnused() - その名称が既に利用されているかどうか確認します。</li>
<li>MEPAnalyticalConnectionType.GetAllTypes() - ドキュメント中のすべてのタイプを取得します。</li>
<li>MEPAnalyticalConnectionType.PressureLoss - タイプに関連する圧力損失です。</li>
</ul>
</li>
</ul>
<p><strong>その他の新しい API : 製造パーツ関連</strong></p>
<ul>
<li>FabricationPart.SplitStraight() メソッド
<ul>
<li>指定点で製造パーツを二つに分割できます。</li>
</ul>
</li>
<li>FabricationPart.AlignPartBy*****&#0160;()メソッド
<ul>
<li>製造パーツを特定の条件で位置調整する複数のメソッドが追加されました。</li>
</ul>
</li>
<li>詳細な製造データ
<ul>
<li>製造の詳細な情報にアクセスするために、様々なメソッド、プロパティ、クラス、列挙体が追加されました。</li>
</ul>
</li>
<li>製造パーツ ステータス
<ul>
<li>ユーザーがパーツの製造ステータスを追跡・確認・設定できるように、製造パーツにプロパティが追加されました。</li>
</ul>
</li>
<li>吊材ロッド
<ul>
<li>吊材ロッドの長さをコントロールするためのメソッド・プロパティが追加されました。</li>
</ul>
</li>
</ul>
<p>今回で、Revit 2018 新機能の解説は終了です。様々な新機能をぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>
