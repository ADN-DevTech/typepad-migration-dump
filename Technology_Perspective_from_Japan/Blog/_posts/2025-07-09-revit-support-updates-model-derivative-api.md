---
layout: "post"
title: "Model Derivative API：Revit ファイル サポートについて"
date: "2025-07-09 00:35:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/07/revit-support-updates-model-derivative-api.html "
typepad_basename: "revit-support-updates-model-derivative-api"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861045ef8200d-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861045ef8200d image-full img-responsive" src="/assets/image_618171.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p class="text-align-justify">過去に Model Derivative API による変換対象から Revit 2013 と Revit 2014 ファイルのサポートが除外された点を <a href="https://adndevblog.typepad.com/technology_perspective/2018/04/model-derivative-updates-revit-support.html" rel="noopener" target="_blank">Model Derivative API と Revit ファイルについて重要なお知らせ</a> の記事でご案内したことがあります。その後、継続する Revit のバージョンアップを経て、現在の最新バージョンは Revit 2026 になっています。</p>
<p class="text-align-justify">今回、オートデスク デスクトップ製品の<a href="https://www.autodesk.com/support/account/manage/versions/previous-versions?_gl=1*1vrgvo9*_ga*MTAwNzI3ODAwNS4xNzUwODA5Njcw*_ga_NZSJ72N6RX*czE3NTA4MTIxMzgkbzEkZzEkdDE3NTA4MTMxMzMkajUyJGwwJGgw" rel="noopener" target="_blank">サポート ライフサイクル</a>に沿って、Revit 製品のサポート範囲は、<u>Revit 2023 から Revit 2026 までの 4 つのバージョン</u>を含むように更新されました。この変更には、その依存関係にあるコンポーネント、アドインとアドイン開発のサポートも含まれます。</p>
<p class="text-align-justify">この変更を受けて、Model Derivative API の変換で .rvt ファイルのサポート形式にも影響があります。ただし、前述の Revit 製品のサポート範囲とは異なる部分があります。</p>
<p class="text-align-justify">Model Derivative API では、古い Revit バージョンのファイル形式を持つ変換・サポートについて、現行のまま継続していて変更はありません。正確には、Revit 変換パイプラインの古い Revit バージョンの関連コンポーネント (<a  _istranslated="1" href="https://aps.autodesk.com/blog/export-ifc-rvt-using-design-automation-api-revit-part-ii">Revit IFC アドイン</a>を含む) はメンテナンス モードになるものの、古いバージョンの .rvt ファイルを SVF、SVF2、DWG、IFC、およびその他のサポートされている形式に引き続き変換することが出来ます。ただし、古いバージョンに関連する問題が見つかった場合、メジャー バージョンほど頻繁に更新や修正を受け取ることはありません。</p>
<p class="text-align-justify">.rvt ファイル形式のバージョン区分は次のとおりです。</p>
<ul>
<li class="text-align-justify">メンテナンス モード： Revit 2016 ～ Revit 2022</li>
<li class="text-align-justify">メジャー バージョン：Revit 2023 ～ Revit 2026</li>
</ul>
<p class="text-align-justify"><strong>ご注意：&#0160;</strong>上記のバージョン範囲は、本記事が書かれた時点での Revit デスクトップ製品サポートライフサイクルに沿うものです。将来、新バージョンの Revit がリリースされると。順次変更される予定です。</p>
<p class="text-align-justify">現在、メンテナンス モード移行した旧バージョンの Revit ファイルの変換時に問題が発生した場合には、ファイルをメジャー バージョンのものにアップグレードする必要があります。例えば、アプリが Model Derivative API を使用して Revit 2022 の .rvt ファイルから.ifc に変換した際に問題が発生したと仮定した場合、その .rvt ファイルを Revit 2023 以降のバージョンの Revit にアップグレードしてから、再度、変換リクエストを送信してください。Model Derivative API は、一致した Revit のメジャー バージョンの変換パイプラインを使用して、変換ジョブを処理します。</p>
<p class="text-align-justify">不明点やご質問をお持ちの場合には、<a  _istranslated="1" href="https://aps.autodesk.com/get-help" rel="noopener noreferrer" target="_blank">Get-Help</a> までお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/revit-support-updates-model-derivative-api" rel="noopener" target="_blank">Revit Support Updates in Model Derivative API | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
