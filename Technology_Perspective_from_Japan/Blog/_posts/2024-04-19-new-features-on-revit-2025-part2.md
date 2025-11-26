---
layout: "post"
title: "Revit 2025 新機能 ～ その２"
date: "2024-04-19 00:43:34"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/04/new-features-on-revit-2025-part2.html "
typepad_basename: "new-features-on-revit-2025-part2"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2025 の建築設計分野に関連する新機能と一部 API とマクロ機能に関する内容をご案内いたします。</p>
<p>Revit 2025 では、地形ソリッド要素を使用するツールと機能が大きく強化されました。ここではその一部をご紹介します。</p>
<p><strong>マス面から地形ソリッドを作成する</strong></p>
<p>マス要素の非垂直面を使用して、地形ソリッド要素を生成します。地形ソリッド要素がマス要素から作成された場合、マス要素を変更すると、新しい面に更新できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3acd2f0200c-pi" style="display: inline;"><img alt="Revit2025_03_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acd2f0200c image-full img-responsive" src="/assets/image_915349.jpg" title="Revit2025_03_01" /></a></p>
<p><a class="asset-img-link" href="/assets/image_847669.jpg" style="display: inline;"><img alt="Revit2025-03-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b09744200b image-full img-responsive" src="/assets/image_847669.jpg" title="Revit2025-03-02" /></a></p>
<p>&#0160;</p>
<p><strong>地形ソリッドの体積を掘削する</strong><br />交差する床、屋根、または地形ソリッド要素を持つ地形ソリッドから体積を掘削します。このツールを使用して、建物の地形ソリッドに掘削する位置を作成するか、1 つ目の地形ソリッドから掘削された駐車場として 2 つ目の交差している地形ソリッドを利用します。</p>
<p>掘削ツールを使用すると、マス要素を使用して地形ソリッドを切り取り、床などの要素を配置する必要がなくなります。掘削機能により、1 つの交差する要素を使用してワンステップで掘削できます。設計を変更するときは、掘削を解除できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b09749200b-pi" style="display: inline;"><img alt="Revit2025_03_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b09749200b image-full img-responsive" src="/assets/image_224423.jpg" title="Revit2025_03_03" /></a></p>
<p><a class="asset-img-link" href="/assets/image_304625.jpg" style="display: inline;"><img alt="Revit2025-03-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acd2f8200c image-full img-responsive" src="/assets/image_304625.jpg" title="Revit2025-03-04" /></a></p>
<p>&#0160;</p>
<p><strong>地形ソリッドのスムーズ シェーディング</strong><br />モデル内の地形ソリッド要素の表示を滑らかにするには、[地形ソリッドのスムーズ シェーディング]を有効にします。[シェーディング]、[ベタ塗り]、[テクスチャ]、[リアリスティック]の表示スタイルを使用することで、地形ソリッドはビューで滑らかな表示になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3acd2fd200c-pi" style="display: inline;"><img alt="Revit2025_03_05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acd2fd200c image-full img-responsive" src="/assets/image_572777.jpg" title="Revit2025_03_05" /></a></p>
<p><a class="asset-img-link" href="/assets/image_463674.jpg" style="display: inline;"><img alt="Revit2025-03-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b09752200b image-full img-responsive" src="/assets/image_463674.jpg" title="Revit2025-03-06" /></a></p>
<hr />
<p><strong>自動結合とロックを使用して壁を作成する</strong></p>
<p>新しく作成した意匠壁を隣接する壁と自動的に結合する、または結合してそれらをロックすることで、複数の壁を使用した作業のモデリング プロセスを高速化できます。</p>
<p>[壁を修正 | 配置]タブの[配置]パネルで、 (自動結合)をクリックして新しい壁と隣接する壁の結合のみを行うか、 (自動結合とロック)をクリックして壁を結合し一緒に移動できるようロックします。壁の結合部に要素が挿入されると、両方の壁に穴が開きます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3acd30a200c-pi" style="display: inline;"><img alt="Revit2025_03_07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acd30a200c image-full img-responsive" src="/assets/image_453621.jpg" title="Revit2025_03_07" /></a></p>
<p><a class="asset-img-link" href="/assets/image_197350.jpg" style="display: inline;"><img alt="Revit2025-03-08" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acd310200c image-full img-responsive" src="/assets/image_197350.jpg" title="Revit2025-03-08" /></a></p>
<hr />
<p><strong>キャンバス内の壁の納まりのコントロール</strong></p>
<p>壁の端部で壁レイヤの納まりを有効にすると、壁の各端部の納まりの動作を、壁の端部の近くにあるコントロールで変更できます。コントロールは、平面図ビューで壁を選択した場合にのみ表示されます。コントロールをクリックして、壁の端部でレイヤの納まりの動作を変更します。</p>
<p><a class="asset-img-link" href="/assets/image_355382.jpg" style="display: inline;"><img alt="Revit2025-03-09" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b09765200b image-full img-responsive" src="/assets/image_355382.jpg" title="Revit2025-03-09" /></a></p>
<hr />
<p><strong>マクロ マネージャ</strong></p>
<p>Revit の前バージョンでマクロを作成および管理するために使用されていた IDE に代わる新しいマクロ管理ツールが導入されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b09780200b-pi" style="display: inline;"><img alt="Revit2025_03_10" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b09780200b img-responsive" src="/assets/image_870807.jpg" title="Revit2025_03_10" /></a></p>
<p>マクロ管理ツールを使用すると、<strong>Visual Studio Code</strong> を使用してマクロを作成し実行できます。<strong>このマクロ マネージャでは、ファイル ベースのマクロがサポートされなくなりました。また、サポート言語は C# のみとなります。</strong></p>
<p>既存のファイル ベースのマクロを使用するには、まずアプリケーション ベースのマクロに変換する必要があります。マクロ マネージャで使用するために既存のマクロをアップグレードする手順については、<a href="https://help.autodesk.com/view/RVT/2025/JPN/?guid=GUID-6BC79239-1447-438B-8BD1-E5DC683B7CF8">「Revit マクロをアップグレードする」</a>を参照してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b09783200b-pi" style="display: inline;"><img alt="Revit2025_03_11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b09783200b image-full img-responsive" src="/assets/image_963655.jpg" title="Revit2025_03_11" /></a></p>
<p>マクロ マネージャを使用してタスクを自動化する方法の詳細については、<a href="https://help.autodesk.com/view/RVT/2025/JPN/?guid=GUID-4DFDA8CD-B0FD-492E-8EDE-A28C29B1E316">「マクロを使用して作業を自動化する」</a>にある各トピックを参照してください。</p>
<hr />
<p><strong>拡張ストレージの改善</strong></p>
<p>拡張ストレージは、スキーマの競合を最小限に抑え、ワークフローを最適化するように再構築されました。</p>
<p>アドオンでスキーマを使用すると、複数のファイルを使用する際に競合が発生することがあります。スキーマの競合が発生した場合の処理を支援する改善が行われました。これらの改善により拡張ストレージの使用方法も再構築され、リスクが軽減し、ワークフローが最適化されました。</p>
<p>潜在的な競合の詳細については、<a href="https://help.autodesk.com/view/RVT/2025/JPN/?guid=GUID-2C2FE8CA-81BD-499D-9AA4-2593FAC6FDDB">「スキーマの競合」</a>を参照してください。</p>
<hr />
<p>By Ryuji Ogasawara</p>
<p>&#0160;</p>
<p>&#0160;</p>
