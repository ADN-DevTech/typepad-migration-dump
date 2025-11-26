---
layout: "post"
title: "Viewer 表示時の Revit 2D シートの 「部屋」の選択"
date: "2023-07-03 00:10:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/07/room-selection-when-viewing-revit-2d-sheet.html "
typepad_basename: "room-selection-when-viewing-revit-2d-sheet"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/11/rvt-translation-enhancement-on-model-derivative-api.html" rel="noopener" target="_blank">Model Derivative API での RVT ファイル変換について</a> の記事でご紹介していますが、Revit&#0160; プロジェクト（.rvt）を Model Derivative API で SVF/SVF2 変換、APS Viewer（旧名 Forge Viewer）に表示する際、&quot;generateMasterViews&quot; advanced オプションを指定することで、「部屋」情報を変換して 3D ビューで「部屋」を識別、プロパティを表示することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a8d403200c-pi" style="display: inline;"><img alt="Advanced_option" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a8d403200c image-full img-responsive" src="/assets/image_962392.jpg" title="Advanced_option" /></a></p>
<p>一方、2D シート表示時には &quot;generateMasterViews&quot; advanced オプション指定は影響しないため、APS Viewer で表示しても「部屋」を選択してプロパティを表示することが出来ません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a8d4a5200c-pi" style="display: inline;"><img alt="No_color_scheme" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a8d4a5200c image-full img-responsive" src="/assets/image_22480.jpg" title="No_color_scheme" /></a></p>
<p>2D シートで部屋を選択出来るようにするには、Revit 側でシートのビュー テンプレートに<a href="https://help.autodesk.com/view/RVT/2024/JPN/?guid=GUID-4809E31D-8385-4EB9-89C2-B58D7FB25B00" rel="noopener noreferrer" target="_blank">カラースキーム</a>を指定しておく必要があります。（「部屋」カテゴリを割り当て）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cb4a2e200b-pi" style="display: inline;"><img alt="View_template" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cb4a2e200b image-full img-responsive" src="/assets/image_453650.jpg" title="View_template" /></a></p>
<p>カラースキームを割り当てた Revit プロジェクト（.rvt）を、再度、Model Derivative API で変換して APS Viewer で表示すると、「部屋」を選択してプロパティを表示出来るようになります。</p>
<p>※ 同じ名前の Revit プロジェクトを繰り返し変換、Viewer に表示していると、ブラウザ キャッシュの影響で Revit プロジェクトに施した変更が Viewer 表示に反映されない場合があります。その場合には、ブラウザ キャッシュを<a href="https://www.autodesk.co.jp/support/technical/article/caas/sfdcarticles/sfdcarticles/kA93g000000L56L.html" rel="noopener" target="_blank">クリア</a>してブラウザを再起動後、再度、Viewer 表示を試してみてください。<br />&#0160;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cb4a26200b-pi" style="display: inline;"><img alt="Color_scheme" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cb4a26200b image-full img-responsive" src="/assets/image_782292.jpg" title="Color_scheme" /></a><br />By Toshiaki Isezaki</p>
