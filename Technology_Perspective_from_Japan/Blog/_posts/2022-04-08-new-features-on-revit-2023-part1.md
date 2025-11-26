---
layout: "post"
title: "Autodesk Revit 2023 の新機能 ～ その１"
date: "2022-04-08 01:58:24"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/04/new-features-on-revit-2023-part1.html "
typepad_basename: "new-features-on-revit-2023-part1"
typepad_status: "Publish"
---

<p>今年も Autodesk Revit の新バージョン Autodesk Revit 2023 がリリースされました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788075961c200d-pi" style="display: inline;"><img alt="Revit2023-01-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788075961c200d image-full img-responsive" src="/assets/image_872747.jpg" title="Revit2023-01-01" /></a></p>
<p>まずは概要をご紹介致します。</p>
<hr />
<p><strong>システム要件</strong></p>
<p>Autodesk Revit 2023 がサポートする OS 環境は下記の通りです。</p>
<ul>
<li>Windows 10、Windows 11</li>
<li>64 ビット版のみの提供（32 ビット版の提供はなし）</li>
<li>.NET Framework Version 4.8 以降</li>
</ul>
<p>詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください。</p>
<ul>
<li><a href="https://knowledge.autodesk.com/support/revit/troubleshooting/caas/sfdcarticles/sfdcarticles/System-requirements-for-Autodesk-Revit-2023-products.html">System requirements for Revit 2023 products</a></li>
</ul>
<p><strong>Revit SDK</strong></p>
<p>なお、Revit SDK は、下記のページで公開されております。</p>
<ul>
<li><a href="https://www.autodesk.com/developer-network/platform-technologies/revit">Revit Developer Center（グローバルサイト）</a></li>
<li><a href="https://www.autodesk.co.jp/developer-network/platform-technologies/revit">Revit デベロッパー センター（※今後、日本語ページでも公開予定です）</a></li>
</ul>
<p><strong>RevitLookup ツール</strong></p>
<p>また、RevitLookup ツールの Revit 2023 対応バージョンも下記のページで公開されております。</p>
<ul>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2023.0.0">RevitLookup 2023</a></li>
</ul>
<hr />
<p>Revit 2023 では、<strong>多数の新機能と機能改善</strong>が追加されております。</p>
<p>次回から複数回にわたってご案内させて頂きますが、今回は、新しい<strong>「データ交換」</strong>の機能をご紹介致します。</p>
<p><strong>データ交換</strong></p>
<p>Revit モデルの一部を、指定したプロジェクトメンバーと共有して、プロジェクトメンバーがモデル全体にアクセスしなくても作業を完了できるようになりました。</p>
<p>Autodesk Docs にパブリッシュすると、モデル内の 3D ビューからデータ交換が作成されます。その交換を、共有メンバーが使用します。データ交換には、次のデータが含まれます。</p>
<ul class="ul" id="GUID-0ED8ABD2-415E-4C74-9D0B-14D515FC8142__UL_79A7125C3A964E38960046AC0B43D294">
<li class="li" id="GUID-0ED8ABD2-415E-4C74-9D0B-14D515FC8142__LI_821C5BACCD2846119CE1B5B16AE12A91">表示されているモデル ジオメトリ</li>
<li class="li" id="GUID-0ED8ABD2-415E-4C74-9D0B-14D515FC8142__LI_87D270C743F54813A84DA2F48A10023C">要素プロパティ。データ交換では、すべてのタイプおよびインスタンスのパラメータにアクセスできます。</li>
<li class="li" id="GUID-0ED8ABD2-415E-4C74-9D0B-14D515FC8142__LI_3C6BC99191EB479F8F7EF6DE46EE77EA">部屋データ</li>
<li class="li" id="GUID-0ED8ABD2-415E-4C74-9D0B-14D515FC8142__LI_5A4F28CEE8C64BEBB91A1E6ED2C3AA65">レベル</li>
<li class="li" id="GUID-0ED8ABD2-415E-4C74-9D0B-14D515FC8142__LI_62CF004AB96A418B81A3122B55354377">通芯</li>
</ul>
<p>現時点では、データ交換は、Revit から Inventor へのワークフロー、及び Revit から Microsoft Power Automate へのワークフローでのみ利用できます。</p>
<p><strong>※また、この機能は Autodesk Construction Cloud の Autodesk Docs に追加された機能で、BIM 360 Document Management には搭載されていません。</strong></p>
<p>ここでは、Revit to Inventor のワークフローをご紹介致します。</p>
<ol>
<li class="li" id="GUID-FA5A2AEB-28EA-4EE6-9271-3F6A8644244E__LI_5F9A0B97F1CA42E68B87522EBBD57E27"><span class="ph msgph prodname" id="GUID-FA5A2AEB-28EA-4EE6-9271-3F6A8644244E__GUID-45405CE4-D629-4299-8061-0EAE22657EB4">Revit</span> で 3D ビューを作成します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fa357bf200c-pi" style="display: inline;"><img alt="Revit2023-01-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa357bf200c image-full img-responsive" src="/assets/image_37266.jpg" title="Revit2023-01-02" /></a><br /><br /></li>
<li>[切断ボックス]と[表示/グラフィックスの上書き]を使用し、ビューに含まれるジオメトリを制限します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fa357c2200c-pi" style="display: inline;"><img alt="Revit2023-01-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa357c2200c image-full img-responsive" src="/assets/image_583757.jpg" title="Revit2023-01-03" /></a><br /><br /></li>
<li>クラウドにパブリッシュするビュー セットを作成します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788075962d200d-pi" style="display: inline;"><img alt="Revit2023-01-04" class="asset  asset-image at-xid-6a0167607c2431970b02788075962d200d img-responsive" src="/assets/image_699830.jpg" title="Revit2023-01-04" /></a><br /><br /></li>
<li>モデルをクラウドに保存して、Autodesk Docs にパブリッシュします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e14e37eb200b-pi" style="display: inline;"><img alt="Revit2023-01-05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e14e37eb200b image-full img-responsive" src="/assets/image_660365.jpg" title="Revit2023-01-05" /></a><br /><br /></li>
<li>Autodesk Docs でパブリッシュされたモデルを開きます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788075966e200d-pi" style="display: inline;"><img alt="Revit2023-01-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788075966e200d image-full img-responsive" src="/assets/image_861610.jpg" title="Revit2023-01-06" /></a><br /><br /></li>
<li>3D ビューを選択し、[データ交換を作成]をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e14e37f7200b-pi" style="display: inline;"><img alt="Revit2023-01-07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e14e37f7200b image-full img-responsive" src="/assets/image_602802.jpg" title="Revit2023-01-07" /></a><br /><br /></li>
<li>Autodesk Docs でデータ交換ファイルが新規に作成されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e14e37f9200b-pi" style="display: inline;"><img alt="Revit2023-01-08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e14e37f9200b image-full img-responsive" src="/assets/image_580112.jpg" title="Revit2023-01-08" /></a><br /><br /></li>
<li>データ交換ファイルを開いて、プロジェクトメンバーに共有します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fa357c9200c-pi" style="display: inline;"><img alt="Revit2023-01-09" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa357c9200c image-full img-responsive" src="/assets/image_903980.jpg" title="Revit2023-01-09" /></a><br /><br /></li>
<li>Inventor 2023 からデータ交換ファイルをロードして使用することができます。Inventor 2023 での使用方法については、下記の Autodesk Inventor 2023 ヘルプをご参照ください。</li>
</ol>
<ul>
<li>
<ul>
<li><a href="https://help.autodesk.com/view/INVNTOR/2023/JPN/?guid=GUID-6E258EB6-F321-4DB1-A2BA-74269B9461D1">Inventor 2023 データ交換を使用するには</a></li>
</ul>
</li>
</ul>
<p>より詳しい機能の解説は、Revit 2023 ヘルプと Autodesk Docs ヘルプをご参照ください。</p>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2023/JPN/?guid=GUID-65D67C3A-BA69-4E99-9882-88F9D50F51B1">Revit 2023 ヘルプ - データ交換</a></li>
<li><a href="https://help.autodesk.com/view/DOCS/JPN/?guid=Data_Exchanges">Autodesk Docs - Data Exchanges</a></li>
</ul>
<p>このように、大規模で複雑になりがちなモデルでも、その一部を必要に応じてプロジェクトメンバーに共有することができるようになりました。ぜひ、Revit 2023 と Autodesk Construction Cloud の新機能をお試しください。</p>
<p>次回は、プラットフォームに共通のコア機能をご紹介します。</p>
<p>By Ryuji Ogasawara</p>
