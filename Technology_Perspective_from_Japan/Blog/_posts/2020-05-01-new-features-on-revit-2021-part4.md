---
layout: "post"
title: "Revit 2021 の新機能 その 4"
date: "2020-05-01 01:19:48"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/05/new-features-on-revit-2021-part4.html "
typepad_basename: "new-features-on-revit-2021-part4"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/new-features-on-revit-2021-part3.html">前回に</a>引き続き、今回は、Revit 2021 の MEP 設計分野の新機能と機能向上についてご紹介致します。</p>
<p><strong>システム解析</strong><br /><a href="https://help.autodesk.com/view/RVT/2021/JPN/?guid=GUID-2043E09F-40E5-4155-AE28-134F62E54F54">Energy Optimization for Revit</a> は、コンセプト デザインから詳細設計や建物運用にいたるまで、建物のエネルギー パフォーマンスをクラウドで解析できる機能（サブスクリプションメンバー限定）です。</p>
<p>Revit の建築モデルからエネルギー 解析モデルを自動生成し、シミュレーション エンジン(DOE 2.2 および EnergyPlus)を使用してクラウドで解析し、Autodesk Insight を使用して、設計要素や運用要素を検討、評価、調整することにより、建物のパフォーマンスを改善することができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263d6d1c1d1200b-pi" style="display: inline;"><img alt="Revit2021_23" class="asset  asset-image at-xid-6a0167607c2431970b0263d6d1c1d1200b img-responsive" src="/assets/image_518983.jpg" title="Revit2021_23" /></a></p>
<p>この機能は Revit 2011 からご利用いただけますが、さらに Revit 2020.1 では、エネルギー解析モデルを MEP のシステム解析で利用できるように拡張した新機能<strong>「システム解析」</strong>が追加されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263d6d1c1c2200b-pi" style="display: inline;"><img alt="Revit-2020.1-mep-01-Systems-Analysis-Features-and-Framework-1024x576" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263d6d1c1c2200b img-responsive" src="/assets/image_809329.jpg" title="Revit-2020.1-mep-01-Systems-Analysis-Features-and-Framework-1024x576" /></a></p>
<p>MEP エンジニアは、スケッチ モードを使用して、建物のゾーン設備機器、空調システムおよび水の循環を設ける部分を定義します。<br />エネルギー解析モデルを作成すると、解析スペースがゾーン設備機器に自動的に割り当てられ、[システム解析]ツールを使用して、冷暖房および風量についての負荷計算や年間のシミュレーションのレポートを作成します。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Ul1Bh7TLKG8?feature=oembed" width="500"></iframe></p>
<p>より詳しい解説にご興味ある方は、下記の Webinar 動画や Revit ヘルプをご参照ください。</p>
<ul>
<li><a href="https://www.youtube.com/watch?v=8kvSB5abVH4">Revit 2020.1 Webinar: Revit Systems Analysis for MEP</a></li>
<li><a href="https://help.autodesk.com/view/RVT/2021/JPN/?guid=GUID-A262F53F-B389-4846-89EF-5855F55476A5">Revit のシステム解析</a></li>
</ul>
<p>API では、建物のエネルギー要件を満たすための空調または水循環の MEP 解析システムの要素として、MEPAnalyticalSystem クラスが追加されました。</p>
<p>水循環システムのデータ、空調システムのデータを保持するクラスが追加されました。<br />MEPAnalyticalSystem.GetWaterLoopData() 及び、GetAirSystemData()メソッドを通じて取得できます。</p>
<ul>
<li>Mechanical.WaterLoopData</li>
<li>Mechanical.AirSystemData</li>
</ul>
<p>ゾーン設備機器は、ZoneEquipment クラスで表されます。<br />ZoneEquipment.GetZoneEquipmentData() メソッドから取得できる ZoneEquipmentData オブジェクトを通じて、ゾーン設備機器に関連付けられるデータにアクセスできます。</p>
<p>新しい GenericZone クラスは、物理的なモデルがなくても、特定の機器、空調システム、および水循を、建物のどのゾーンに割り当てるか指定できます。</p>
<p>そして、Analysis.ViewSystemsAnalysisReport クラスを通じて、システム解析レポートを作成することができます。</p>
<p>&#0160;</p>
<p><strong>配電システムの単相2線L-N電圧に対応</strong><br />米国以外でよく見られる配電システムに対応するために、Revit は単相 2 線の L-N 分電盤をサポートしました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/PpoCMNoR35M?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>電気回路の命名</strong><br />米国以外での回路 ID の規則に適切に対応するために、[電気設定]ダイアログで回路の命名スキームを定義できます。パネルの[回路名称]インスタンス パラメータを使用して、スキームを選択します。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Lo10pM4YINA?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p class="asset-video"><strong>配電盤の回路の最大数</strong><br />配電盤の[1 ポール ブレーカの最大数]パラメータが[回路の最大数]パラメータに変わりました。<br />また、スペア回路で[フレーム]パラメータが使用できるようになりました。パネル集計表に表示されます。同様に、スペース回路では、極数および集計表の回路注記パラメータがサポートされるようになりました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/qXiRhRzH_8Q?feature=oembed" width="500"></iframe><br /><br /></p>
<p><strong>配電盤のフェーズ選択</strong><br />配電盤のパネル集計表ビューに、フェーズを切り替えるコマンドが追加されました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/IgnUMbZnZAk?feature=oembed" width="500"></iframe></p>
<p class="asset-video"><br /><strong>各シートに関連の集計表をプロジェクトブラウザ内で表示</strong><br />シート ビューに、各シートに表示されるパネル集計表が一覧表示されます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/06Yma88sr7g?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p class="asset-video"><strong>MEP ワークシェア機能の向上</strong><br />MEP 要素でのワークシェアリングの使用が改善され、より一貫性のあるエクスペリエンスが提供され、関係者間のコラボレーション機能が向上しました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/hDbm31JeYs4?feature=oembed" width="500"></iframe></p>
<p class="asset-video">今回は、Revit 2021 の MEP 設計分野の新機能と機能向上について、ご紹介いたしました。ぜひお試しください。</p>
<p class="asset-video">By Ryuji Ogasawara</p>
