---
layout: "post"
title: "Revit 2020 の新機能 その2"
date: "2019-04-22 03:14:28"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-revit-2020-part2.html "
typepad_basename: "new-features-on-revit-2020-part2"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-revit-2020-part1.html"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a455fa4c200c-pi" style="float: right;"><img alt="Revit-2020-badge-256px" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a455fa4c200c img-responsive" src="/assets/image_736745.jpg" style="margin: 0px 0px 5px 5px;" title="Revit-2020-badge-256px" /></a><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-revit-2020-part1.html">前回に</a>引き続き、Revit 2020 の新機能についてご紹介させて頂きます。</p>
<p>今回は、<strong>「最適化機能」</strong>をテーマとした新機能と更新内容についてご案内いたします。</p>
<p>ソフトウェアを使用する上で、その使いやすさと生産性、そして設計上の意思決定を手助けする分析能力は非常に重要です。<br />Revit 2020 では、これからご紹介する新機能と更新によって、繰り返しのタスクに費やす時間を削減し、効率化を図りました。</p>
<p><strong>移動パス</strong></p>
<p>ルート解析を使用して、モデル内の選択した点の間の移動距離と移動時間を解析して、デザインのレイアウトを最適化できるようになりました。<br />最短経路をシミュレートすることにより、人々が建物の中に移動したり、スペースを移動する方法を理解することができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a455e9b1200c-pi" style="display: inline;"><img alt="Revit 2020 Part2-5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a455e9b1200c img-responsive" src="/assets/image_787009.jpg" title="Revit 2020 Part2-5" /></a></p>
<p>この[移動パス]ツールは、平面図ビュー上で、始端と終端を選択することによって移動パスを設定します。<br />モデルのジオメトリが解析され、移動パス上にある障害物として作用するモデル要素は迂回し、ドアは通過するようナビゲートされて移動パスが生成されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a455e9b5200c-pi" style="display: inline;"><img alt="Revit 2020 Part2-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a455e9b5200c image-full img-responsive" src="/assets/image_124543.jpg" title="Revit 2020 Part2-1" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a47f1935200d-pi" style="display: inline;"><img alt="Revit 2020 Part2-3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a47f1935200d image-full img-responsive" src="/assets/image_353425.jpg" title="Revit 2020 Part2-3" /></a></p>
<p>また、ルート解析の設定により、モデル要素が移動パスに与える影響をコントロールします。移動パスの始端と終端間のモデル要素は、解析によって生成されるルートに影響します。</p>
<p><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a47f193b200d-pi" style="display: inline;"><img alt="Revit 2020 Part2-2" class="asset  asset-image at-xid-6a0167607c2431970b0240a47f193b200d img-responsive" src="/assets/image_215806.jpg" title="Revit 2020 Part2-2" /></a></p>
<p>移動パスのインスタンス プロパティでは、移動パスの始端から終端までの歩行にかかる時間(計算値)や、移動パスのすべてのセグメントの実際の長さも計算されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a47f195e200d-pi" style="display: inline;"><img alt="Revit 2020 Part2-4" class="asset  asset-image at-xid-6a0167607c2431970b0240a47f195e200d img-responsive" src="/assets/image_997864.jpg" title="Revit 2020 Part2-4" /></a><br />これにより、設計上の意思決定に役立つための経路を最適化します。<br />移動パス要素はビュー固有であり、配置されるビュー内の詳細要素として機能します。モデル内で移動パスの線にタグ付けし、集計できます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/cuJTxUe3UE0?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>シート間で凡例ビューをコピーする</strong></p>
<p>凡例ビューを 1 つのシート ビューからコピーして、他のシート ビューに貼り付けることができるようになりました。<br />これにより、複数のシートに表示される凡例ビューをレイアウトするときの効率を高めることができるようになります。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/8FOSMx1mAtc?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>規則によるビュー フィルタの拡張 OR 条件</strong></p>
<p>Revit 2019 では、OR および AND 演算子を使用してビュー フィルタの複雑な規則を作成できるようになりました。ただし、要素の複数のカテゴリに対する OR 規則は、両方のカテゴリに共通のパラメータに制限されていました。<br />Revit の 2020 では、さらに、OR フィルタの規則を作成する際、選択したカテゴリの「すべてのパラメータ」にアクセスし、規則内で使用することができるようになります。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/UT1B1jn8AZw?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>バックグラウンドでの計算処理</strong></p>
<p>以前のリリースでは、Revitがより速く動作するように最適化しました。<br />Revit 2020を使用して3Dモデルで詳細な鉄骨接合部を編集すると、これらのパフォーマンス重視のタスクを実行する際の応答時間が短縮されます。<br />バックグラウンドプロセスが実行されている間、Re​​vitプロジェクトで作業を続けることができます。</p>
<p>&#0160;</p>
<p><strong>MEP コンポーネントの[高さ]パラメータの変更</strong></p>
<p>注釈およびドキュメントの使いやすさを向上させるために、次のファミリ カテゴリでタグ、集計表およびビュー フィルタを使用するための[高さ]パラメータが組み込みパラメータになりました。</p>
<ul>
<li>機械設備</li>
<li>衛生器具</li>
<li>照明器具</li>
<li>照明装置</li>
<li>スプリンクラ</li>
<li>電気器具</li>
<li>電気機器</li>
<li>データ装置</li>
<li>通信装置</li>
<li>火災報知装置</li>
<li>ナース コール装置</li>
<li>警備装置</li>
<li>電話装置</li>
<li>特殊設備</li>
<li>制気口</li>
<li>一般モデル</li>
<li>家具</li>
<li>Plant</li>
<li>収納家具</li>
</ul>
<p>[高さ]パラメータは[レベルからの高さ]に変更されました。また、[オフセット値]パラメータは[ホストからのオフセット]に変更されました。<br />さらに、集計表レベル パラメータは、[プロパティパレット]に移動しました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/E9ACS0LESN4?feature=oembed" width="500"></iframe></p>
<p>レベルからの高さパラメータを編集する機能は、要素の配置方法によって異なります。詳細については、<a href="http://help.autodesk.com/view/RVT/2020/JPN/?guid=GUID-AB9E985D-3D4B-44AA-82BA-1303D0693A77">こちらのページ</a>をご参照ください。</p>
<p>&#0160;</p>
<p><strong>読み込まれた橋梁ジオメトリとトンネル ジオメトリから作成されたパーツ</strong></p>
<p>InfraWorks から読み込まれた橋梁ジオメトリ、トンネル ジオメトリ、およびその他の DirectShape をパーツに分割し、詳細設計や解析で利用できるようになりました。<br />パーツに開口や切断などの調整を加えることができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a455ea16200c-pi" style="display: inline;"><img alt="Revit 2020 Part2-9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a455ea16200c image-full img-responsive" src="/assets/image_192494.jpg" title="Revit 2020 Part2-9" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/vuZMCANjkbE?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>Dynamo Revit 2.1</strong></p>
<p>次の更新内容が含まれています。詳細については、<a href="http://help.autodesk.com/view/RVT/2020/JPN/?guid=GUID-A45F5AF9-6FCF-4F0C-90FA-309A964833B2">こちらのページ</a>をご参照ください。</p>
<ul>
<li>新しいノードおよび更新されたノード。</li>
<li>より高速で、正確な結果のために検索機能が改善。</li>
<li>ライブラリの再編成とカスタム コンテンツの分離による、ブラウザ体験の向上。</li>
<li>Python エディタ、レーシング オプション、および Revit ノードに対するグラフィック オーサリングの改善。</li>
<li>Package Manager の機能拡張のサポートによる、ユーザ インタフェースのカスタマイズ機能の追加。</li>
<li>信頼性、安定性、パフォーマンスの改善。</li>
</ul>
<p>&#0160;</p>
<p><strong>Dynamo の鉄骨接合</strong></p>
<p>Dynamo が鉄骨接合部のモデリングをコントロールできるようにする新しい Autodesk Steel Connections パッケージです。Revit の鉄骨建築のモデリングを高速化するのに役立ちます。これは標準の接合およびカスタム接合を配置するための同じようなジオメトリ条件を識別でき、Dynamo プレーヤーですぐに使用できるスクリプトがパッケージとともに提供されます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/68-cZP8B_UQ?feature=oembed" width="500"></iframe></p>
<p>次回は、<strong>「接続機能」</strong>をテーマとした新機能と更新内容についてご案内いたします。</p>
<p>By Ryuji Ogasawara</p>
