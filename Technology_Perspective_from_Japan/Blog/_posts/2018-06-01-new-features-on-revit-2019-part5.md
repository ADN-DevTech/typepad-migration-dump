---
layout: "post"
title: "Revit 2019 の新機能 その5"
date: "2018-06-01 01:26:34"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/05/new-features-on-revit-2019-part5.html "
typepad_basename: "new-features-on-revit-2019-part5"
typepad_status: "Publish"
---

<p>Revit 2019 の新機能についてご紹介しております。今回は、MEP 設計分野の新機能、更新内容、API の対応状況を解説いたします。</p>
<p><span style="font-size: 14pt;"><strong>冷温水配管系統で油圧分離を追加する</strong></span></p>
<p>冷温水配管系統で配管のサイズを改善するために、1 次、2次、3次の各ループに温水配管を分離して、各ループの流量および圧力損失を計算することができるようになります。</p>
<p>分離前の冷温水配管系統 - 1 次ループ(左)、および低損失ヘッダの 1 次ループ(右)<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03e4099200d-pi" style="display: inline;"><img alt="GUID-CD89CE99-6263-45D8-87B9-3013614C3D63" class="asset  asset-image at-xid-6a0167607c2431970b0224e03e4099200d img-responsive" src="/assets/image_562335.jpg" title="GUID-CD89CE99-6263-45D8-87B9-3013614C3D63" /></a><br /><br /></p>
<p>ループは解析により必要に応じて 1 次から 2 次、2 次から 3 次、というように区切られます。各ループは、給気および還気のため独自のシステムに割り当てられます。結果として、1 次システムと 2 次システムで同じシステム タイプを使用しますが、異なるシステムのインスタンスが割り当てられます。</p>
<p>分離後の冷温水配管系統 - 1 次ループ(左)および低損失ヘッダの 1 次ループ(右)</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84f9abc200c-pi" style="display: inline;"><img alt="GUID-AC182553-1B8E-4EFE-98D4-2668D3C790FB" class="asset  asset-image at-xid-6a0167607c2431970b0223c84f9abc200c img-responsive" src="/assets/image_217817.jpg" title="GUID-AC182553-1B8E-4EFE-98D4-2668D3C790FB" /></a><br /><br />詳細は下記のヘルプドキュメントをご参照ください。</p>
<p style="padding-left: 30px;"><a href="http://help.autodesk.com/view/RVT/2019/JPN/?guid=GUID-D2362C11-C604-4B32-AB76-19E6EEF75439">冷温水配管系統での水圧分離について</a></p>
<p><span style="font-size: 11pt;"><strong>油圧分離 API</strong></span></p>
<ul>
<li>油圧分離システムは、各油圧ループに対する個別の流量と圧力の解析をサポートします。</li>
<li>作成、削除、油圧 分離システムの様々なメンバ バリデーションをサポートします。
<ul>
<li>PipingSystem.Create/DeleteHydraulicSeparation()&#0160;</li>
<li>PipingSystem.Is/CanBeHydraulicLoopBoundary()&#0160;</li>
</ul>
</li>
</ul>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>並列ポンプの稼働と待機</strong></span></p>
<p>並列ポンプが含まれているシステムでパイプのサイズを改善するために、冷温水系統の流量および圧力損失の計算において稼働中のポンプの数を考慮したポンプセットを作成できるようになりました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03e40a2200d-pi" style="display: inline;"><img alt="GUID-887C74CD-CE2D-443A-8573-8F4D541EDECB" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0224e03e40a2200d img-responsive" src="/assets/image_487893.jpg" title="GUID-887C74CD-CE2D-443A-8573-8F4D541EDECB" /></a></p>
<p>詳細は下記のヘルプドキュメントをご参照ください。</p>
<p style="padding-left: 30px;"><a href="http://help.autodesk.com/view/RVT/2019/JPN/?guid=GUID-20A1E507-65BB-4629-AA54-F49E2DF205A9">機械設備セットにポンプを追加または削除する</a></p>
<p><span style="font-size: 11pt;"><strong>機械設備セット API</strong></span></p>
<ul>
<li>MEP システムと相互関係にある機械設備セットの作成と編集をサポートします。<br />
<ul>
<li>Mechanical.MechanicalEquipmentSet</li>
<li>Mechanical.MechanicalEquipmentSetType</li>
</ul>
</li>
<li>システムに関連するポンプセットのリストを取得します。
<ul>
<li>PipingSystem.GetPumpSets()</li>
</ul>
</li>
</ul>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>製造用パーツのコネクタを合わせる</strong></span></p>
<p>モデリング時の時間を短縮するために、Revit は一致しない製造用コネクタを自動的に互換性のあるものと一致させます。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>製造の詳細のための下向きステップ コネクタ</strong></span></p>
<p>下向きステップ コネクタを使用して単線ダクトを非単線要素に接続する場合、Revit は下向きステップ サイズをライニングの厚さと一致させます。</p>
<p>&#0160;</p>
<p>今回で、Revit 2019 新機能の解説は終了です。様々な新機能をぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>
