---
layout: "post"
title: "Design Automation：Revit 2019 ベースエンジンの削除について"
date: "2024-08-14 01:21:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/08/design-automation-revit-2019-engine-removal.html "
typepad_basename: "design-automation-revit-2019-engine-removal"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bba498200b-pi" style="display: inline;"><img alt="Aps2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bba498200b image-full img-responsive" src="/assets/image_859029.jpg" title="Aps2" /></a></p>
<p>Design Automation API をお使いの場合、<a href="https://adndevblog.typepad.com/technology_perspective/2024/03/design-automation-2019-based-engine-removal.html" rel="noopener" target="_blank">Design Automation：2019 ベースエンジンの削除について </a> でご案内したとおり、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/engine-lifecycle/" rel="noopener" target="_blank">ライフサイクル ポリシー</a>では 2019 ベースのコアエンジンは削除済になっています。ただし、諸般の事情により、Revit 2019 エンジンのみが、まだ利用出来る状態になっています。</p>
<p>この Revit 2019 コアエンジンについて、改めて、<strong>2024年9月15日に削除</strong>されることになりましたので、念のため、ご案内しておきたいと思います。もし、まだ Revit 2019 コアエンジンをお使いのアプリをお使いの場合には、すみやかに新しいバージョンのコアエンジンに移行をお願いいたします。</p>
<p>移行に必要となるおおまかなアクションは次のとおりです。</p>
<p><strong>AppBundle：</strong></p>
<ol>
<li><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/appbundles-id-versions-POST/">POST appbundles/:id/versions</a> をエンドポイントを使用して新しい AppBundle バージョンを作成します。使用可能なRevitエ コアンジンを指定していることをご確認ください。</li>
<li>（オプション）ターゲットの Revit コアバージョンにあわせてAppBundle のアドイン ファイルを再ビルドします。ビルドには、ターゲットバージョンとなるデスクトップ版の Revit 用 Revit SDK、ないし、アセンブリ ファイルを利用する必要があります。Revit アドイン アプリが更新出来たら、パッケージ バンドル（.zip）内 PackageContents.xml の&#0160;<strong>SeriesMin</strong>&#0160;と&#0160;<strong>SeriesMax</strong> をアドインがサポートするバージョン値で更新します。すべてが完了したら、パッケージ バンドル（.zip）を作成してアップロードします。</li>
<li><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/appbundles-id-aliases-aliasId-PATCH/">PATCH appbundles/:id/aliases/:aliasId</a> エンドポイントを使用して、作業エイリアスを新しい AppBundle バージョンに割り当てます。</li>
</ol>
<p><strong>Actibity：</strong></p>
<ol>
<li><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/activities-id-versions-POST/">POST activities/:id/versions</a> エンドポイントを使用して、新しい Activity バージョンを作成します。使用可能な Revit コアエンジンを指定していることをご確認ください。</li>
<li><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/activities-id-aliases-aliasId-PATCH/">PATCH activities/:id/aliases/:aliasId</a> エンドポイントを使用して、新しい Activity バージョンに作業エイリアスを割り当てます。</li>
</ol>
<p>By Toshiaki Isezaki</p>
