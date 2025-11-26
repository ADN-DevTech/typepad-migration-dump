---
layout: "post"
title: "Autodesk Revit 2023 の新機能 ～ その4"
date: "2022-05-06 02:21:19"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/05/new-features-on-revit-2023-part4.html "
typepad_basename: "new-features-on-revit-2023-part4"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/new-features-on-revit-2023-part3.html">前回の記事</a>に引き続き Autodesk Revit 2023 の新機能として、今回は、MEP 設計に関する新機能と機能改善をご紹介致します。</p>
<hr />
<p><strong>電気解析</strong></p>
<p>電気設計者は物理的な電気モデル要素を作成せずに建物の電力要件を見積もって仮の概念的な配電システムを定義できるようになり、最小限のモデリングで概念解析と初期設備のサイズ設定を行うことができるようになりました。</p>
<p><a class="asset-img-link" href="/assets/image_701339.jpg" style="display: inline;"><img alt="Revit2023-04-00" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e15420ee200b image-full img-responsive" src="/assets/image_701339.jpg" title="Revit2023-04-00" /></a></p>
<p>電気解析のワークフローは、次の 2 つの機能で構成されます。</p>
<ol>
<li>電気解析用負荷を定義する<br />領域ベースの電気負荷を定義する前に、まず[領域ベースの負荷の境界]を使用して閉じた領域を定義する必要があります。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1542173200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="/assets/image_637392.jpg" style="display: inline;"><img alt="Revit 2023 Part4-1-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807b93d8200d image-full img-responsive" src="/assets/image_637392.jpg" title="Revit 2023 Part4-1-1" /></a><br />領域ベースの負荷の境界を設定すると、領域ベースの電気負荷を定義できます。<br />領域ベースの負荷を追加して、電力/領域の密度に基づいて電力要件を定義します。<br /><a class="asset-img-link" href="/assets/image_92632.jpg" style="display: inline;"><img alt="Revit 2023 Part4-2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1542501200b image-full img-responsive" src="/assets/image_92632.jpg" title="Revit 2023 Part4-2" /></a><br />また、領域ベースの負荷を小さい負荷に分割することで、配電システムからの負荷の供給計画や設備定格の事前決定を適切に行うことができます。<br /><br /></li>
<li>コンセプト配電システムを定義する<br />冷却機などの設備にあるような特定の負荷の場合に、設備負荷を定義できます。<br /><a class="asset-img-link" href="/assets/image_962628.jpg" style="display: inline;"><img alt="Revit2023-04-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa9503e200c image-full img-responsive" src="/assets/image_962628.jpg" title="Revit2023-04-03" /></a><br /><br /></li>
</ol>
<p><strong>解体された MEP 要素の接続を維持</strong></p>
<p>生産性を向上させるため、配管、ダクト、電線管、ケーブル ラック、配線、製造用パーツの各 MEP 要素は、解体されてもシステムへの接続と割り当てが維持されるようになりました。</p>
<p><a class="asset-img-link" href="/assets/image_2373.jpg" style="display: inline;"><img alt="Revit 2023 Part4-4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807b934c200d image-full img-responsive" src="/assets/image_2373.jpg" title="Revit 2023 Part4-4" /></a></p>
<p>要素の各側面で異なるフェーズが接続された状態で要素が自動的に追加された場合、それらの要素は現在のビューのフェーズに割り当てられます。<br />すべての側面で解体された要素が接続された状態で要素が自動的に追加された場合、それらの要素は解体されます。</p>
<p>&#0160;</p>
<p><strong>機械システムおよび衛生設備配管システムのカテゴリ</strong></p>
<p>機械制御装置および衛生設備の新しい MEP カテゴリにより、ビューやシートでの要素の表示と外観をより詳細にコントロールできるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807b9357200d-pi" style="display: inline;"><img alt="Revit2023-04-05" class="asset  asset-image at-xid-6a0167607c2431970b0278807b9357200d img-responsive" src="/assets/image_246604.jpg" title="Revit2023-04-05" /></a></p>
<p>新しいツールを使用して、モデルに機械制御装置および衛生設備を配置します。<br />サーモスタット、CO2 センサ、恒湿器などのモデル内にダクト システムの機械制御装置を配置できます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fa95089200c-pi" style="display: inline;"><img alt="Revit2023-04-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa95089200c image-full img-responsive" src="/assets/image_127500.jpg" title="Revit2023-04-06" /></a></p>
<p>&#0160;</p>
<p><strong>MEP 要素の高さパラメータ </strong></p>
<p>MEP 要素の立面図プロパティが更新され、新しいプロパティも追加されて、設計要素と製造要素の整合性が向上しました。<br />詳細は、<a href="https://help.autodesk.com/view/RVT/2023/JPN/?guid=GUID-87FE1918-8AD7-4987-9958-D1C7FC7E5ABE">こちらのページ</a>をご参照ください。</p>
<p>&#0160;</p>
<p><strong>MEP コネクタのホストを変更する</strong></p>
<p>生産性を向上させるために、ファミリ エディタで作業しながら、面または作業面上の MEP コネクタのホストを変更できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807b9370200d-pi" style="display: inline;"><img alt="Revit2023-04-07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807b9370200d image-full img-responsive" src="/assets/image_344701.jpg" title="Revit2023-04-07" /></a></p>
<p>ホストを変更すると、Revit は既存の接続を保持しようとします。ホストを変更したコネクタにアタッチされているダクト要素と配管要素は、ファミリがプロジェクトに再びロードされるときに、コネクタとともに移動します。この移動により、接続された他の要素を通じて伝播されるため、接続が解除される可能性があります。</p>
<p>&#0160;</p>
<p><strong>一般注釈の向き<br /></strong></p>
<p>方向性のあるファミリを作成する際の生産性を向上させるために、一般注釈の方向が各ビューで統一されるようになりました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/fes4GUqPHzM?feature=oembed" width="712"></iframe></p>
<p>&#0160;</p>
<p><strong>MEP 製造要素のキャンバス内コントロール</strong></p>
<p>キャンバス内のコントロールを使用して、製造用ダクト部品、配管部品、フリップ継手の高さを変更することができます。<br />製造要素の高さを変更すると、階段経路に勾配が適用される設計要素とは異なり、階段経路全体が移動します。</p>
<hr />
<p>By Ryuji Ogasawara</p>
<p>&#0160;</p>
