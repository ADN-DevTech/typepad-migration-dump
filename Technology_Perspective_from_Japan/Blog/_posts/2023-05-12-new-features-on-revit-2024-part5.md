---
layout: "post"
title: "Revit 2024 新機能 ～ その５"
date: "2023-05-12 00:52:00"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/05/new-features-on-revit-2024-part5.html "
typepad_basename: "new-features-on-revit-2024-part5"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2024 の システム（MEP） 設計分野に関連する新機能をご紹介いたします。</p>
<p>&#0160;</p>
<p><strong>3Dビューに表示した特定の要素からエネルギー解析モデルを生成する</strong></p>
<p>エネルギー解析モデルを生成する際に、切断ボックス、ビュー フィルタ、または表示/グラフィックスの上書きを使用して、3D ビューに表示した特定の要素のみを含めることができるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a49d9f200c-pi" style="display: inline;"><img alt="Revit2024-05-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a49d9f200c image-full img-responsive" src="/assets/image_708871.jpg" title="Revit2024-05-01" /></a></p>
<p>3D ビューでは、切断ボックス、ビュー フィルタ、表示/グラフィックスの上書きなどを使用して、目的の要素を表示することができます。</p>
<p>[エネルギー設定]ダイアログで、[現在のビューに表示されている要素のみを使用]を有効にして、[エネルギー設定]を設定します。現在アクティブなビューは、エネルギー解析モデルを生成するためのフィルタとして使用されます。ビューに表示されている要素のみが含まれます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7518038fa200b-pi" style="display: inline;"><img alt="Revit2024-05-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7518038fa200b image-full img-responsive" src="/assets/image_180681.jpg" title="Revit2024-05-02" /></a></p>
<p>注: [現在のビューに表示されている要素のみを使用]設定は、3D ビューにのみ適用され、[エネルギー解析モデル]のモードが[部屋またはスペースを使用]に設定されている場合は使用できません。また、このオプションが有効になっている場合、フェーズとデザイン オプションの設定は適用されません。</p>
<p>&#0160;</p>
<hr />
<p><strong>制気口ファミリで[注釈の向きを維持]パラメータをサポート</strong></p>
<p>[注釈の向きを維持]ファミリ パラメータを使用して、制気口カテゴリに注釈記号を表示できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6853734bf200d-pi" style="display: inline;"><img alt="Revit2024-05-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6853734bf200d image-full img-responsive" src="/assets/image_152887.jpg" title="Revit2024-05-03" /></a></p>
<p>&#0160;</p>
<hr />
<p><strong>設計ダクトでネットワーク ベースの計算をサポート</strong></p>
<p>設計ワークフローと製造ワークフローを統合するために、設計ダクトで流量と圧力損失のネットワーク ベースの計算を使用できるようになりました。</p>
<p>ダクトの計算エンジンを有効にするには、[機械設定]ダイアログの[ネットワーク]タブを使用します。有効にした場合、計算はバックグラウンドで実行されるため、作業を続行することができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751803791200b-pi" style="display: inline;"><img alt="Revit2024-05-04" class="asset  asset-image at-xid-6a0167607c2431970b02b751803791200b img-responsive" src="/assets/image_311078.jpg" title="Revit2024-05-04" /></a></p>
<p>&#0160;</p>
<hr />
<p><strong>MEP 製造用パーツの流量と圧力の計算が追加</strong></p>
<p>設計ワークフローと製造ワークフローを統合するために、直線セグメントの製造用パーツに流量と圧力損失の計算が追加されました。これらの結果は、直線セグメントでのみ使用できます。</p>
<hr />
<p><strong>一致しない負荷の負荷セットを定義する</strong></p>
<p>需要負荷の計算から一致しない負荷を除外する場合は、負荷セットを追加し、「スタンバイの量」を設定します。</p>
<p>負荷セット内の最小負荷は、バックアップ負荷として識別されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751803799200b-pi" style="display: inline;"><img alt="Revit2024-05-05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751803799200b image-full img-responsive" src="/assets/image_46255.jpg" title="Revit2024-05-05" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7518037a2200b-pi" style="display: inline;"><img alt="Revit2024-05-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7518037a2200b image-full img-responsive" src="/assets/image_997109.jpg" title="Revit2024-05-06" /></a></p>
<hr />
<p><strong>電気解析用コンポーネントの需要負荷と需要率</strong></p>
<p>電気解析用コンポーネントに対して、解析負荷ごとに負荷分類を指定し、需要率を適用できます。</p>
<p>電気設備負荷および領域ベースの電気負荷タイプごとに負荷分類を設定します。</p>
<p>需要負荷と需要電流は、電気解析用コンポーネントごとに計算され、システム ブラウザに表示されます。</p>
<p>負荷分類ごとに、接続負荷と需要負荷の合計を電力と電流の単位で集計できます。</p>
<hr />
<p>By Ryuji Ogasawara</p>
