---
layout: "post"
title: "ご意見募集：Model Derivative API による DWG 変換の変更について"
date: "2022-07-06 01:22:11"
author: "Toshiaki Isezaki"
categories: []
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/07/call-feedback-model-derivative-dwg-translation-changes.html "
typepad_basename: "call-feedback-model-derivative-dwg-translation-changes"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d403e14200b-pi" style="float: right;"><img alt="Feedback" class="asset  asset-image at-xid-6a0167607c2431970b02a30d403e14200b img-responsive" src="/assets/image_735531.jpg" style="width: 200px; margin: 0px 0px 5px 5px;" title="Feedback" /></a>Model Derivative API による DWG ファイル変換では、パフォーマンスの向上と処理時間の短縮を目指して、出力に関するいくつかの変更を計画しています。この際、みなさまのご意見を伺い、DWG 変換が使用されているワークフローを把握したいと考えています。ぜひ、<a href="mailto:forge.sales@autodesk.com" rel="noreferrer noopener" target="_blank">forge.sales@autodesk.com</a> までご意見をお願いします。</p>
<p>現在、DWG ファイルを変換すると、2D 部分の内容は内部的に&#0160; F2D として変換処理されるようになっています。この際、すべてのジオメトリが持つ膨大な量のプロパティはすべ抽出されることになります。これが起因となり、変換時間に時間がかかってしまうケースが見受けられています。また、建設業の現場では、ブロック属性や画層などの参照がおこなわれるものの、AutoCAD の個々のジオメトリが持つすべてのプロパティ、例えば、ポリラインの頂点座標など、はあまり参照されていないことが報告されています。</p>
<p style="padding-left: 40px;">※ ここでいう F2D は Fusion 360 図面形式とは無関係です。</p>
<p>そこで、Autodesk Docs（BIM 360、Autodesk Construction Cloud プロジェクト）にアップロードされたすべての DWG に次の変更を加え、同時に、Model Derivative API にも変換オプションを加えて公開することを計画しています。（3rd party アプリは、<strong>従来からの変換方法に加え</strong>、次の変換パイプラインから変換方法を選択できるようになります。</p>
<p><strong>オプション１：製図レベルのプロパティを排除して 現場でも参照される「意味」を持つプロパティ のみ保持</strong>&#0160;</p>
<ul>
<li>文字列プロパティと ObjectId（dbId）以外のすべてのプロパティ（ブロック名、 属性、 寸法、 MText、 引き出し文字、画層など）&#0160;を削除&#0160;</li>
<li>ブロックとカスタム エンティティの「Atributes（属性）」カテゴリの全プロパティ抽出を継続&#0160;</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d41c91e200b-pi" style="display: inline;"><img alt="Attributes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d41c91e200b image-full img-responsive" src="/assets/image_841766.jpg" title="Attributes" /></a></p>
<p><strong>オプション２：2D モデルの抽出を Smart PDF に変更</strong></p>
<ul>
<li>建設業では PDF が多く利用されているため、2D の内容を F2D から PDF ベースの内容に移行することで、次のような利点があります。&#0160;
<ul>
<li>ネイティブ PDF 閲覧&#0160;</li>
<li>変換時間の短縮</li>
<li>AutoCAD からの PDF パブリッシングに対応&#0160;</li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;">※ Smart PDF は Forge Viewer 表示時にハンドル番号/externalId を維持します。</p>
<p>※ 本記事は <a href="https://forge.autodesk.com/blog/call-feedback-model-derivative-dwg-translation-changes" rel="noopener" target="_blank">Call for feedback: Model Derivative DWG translation changes | Autodesk Forge</a>&#0160;から転写・翻訳して一部加筆したものです。</p>
<p>By Toshiaki Isezaki</p>
