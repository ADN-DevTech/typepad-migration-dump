---
layout: "post"
title: "AU 2016 と Forge アップデート情報 ～ その１"
date: "2016-11-16 07:48:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/11/au-japan-and-forge-uodate-information-part1.html "
typepad_basename: "au-japan-and-forge-uodate-information-part1"
typepad_status: "Publish"
---

<p>Autodesk University 2016 と、それに先立つ Developer Day では、Forge に関していくつかの更新点が公表されました。Developer Day の内容は、本来、ADN メンバのみの非公開情報が含まれますが、公開可能な情報に関してのみ、この場で内容を共有したいと思います。</p>
<p><strong>Forge アップデート</strong></p>
<p style="padding-left: 30px;">Developer Day での最初の話題は、課金方法がクレジットカード決済からクラウド クレジットに変更になったというビジネス モデルの変更です。これについては、FAQ も含め、先のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/11/forge_business_model_and_changes_for_charging_scheme.html" rel="noopener noreferrer" target="_blank">Forge ビジネスモデルと課金方法の変</a>更</strong> ですでにご案内していますので、ここでは割愛します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b0499a970b-pi" style="display: inline;"><img alt="Devdays" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b0499a970b image-full img-responsive" src="/assets/image_149464.jpg" title="Devdays" /></a></p>
<p style="padding-left: 30px;">機能面は、API 別に次の点を挙げることが出来ます。</p>
<p style="padding-left: 60px;"><strong>Moel Derivative API</strong></p>
<p style="padding-left: 90px;">Model Derivative API は、デザイン ファイルを Web ブラウザでストリーミング配信で表示するための SVF ファイルへの変換や、別のデザイン データファイル形式に変換するための API です。今回、クラウドにアップロードした Revit の RVT ファイルを、直接、IFC ファイルに変換できるようになりました。</p>
<p style="padding-left: 60px;"><strong>Data Management API</strong></p>
<p style="padding-left: 90px;">OAuth2 に対応する Forge の OAuth API を利用して 3 legged 認証を経ることで、Data Management API が BIM 360 Docs のユーザ ストレージにアクセス出来るようになっています。もともと、A36o と Fusion 360 のプロジェクト配下のストレージにアクセスすることが出来ていましたが、新たに BIM 360 に対応したことになります。</p>
<p style="padding-left: 60px;"><strong>Viewer</strong></p>
<p style="padding-left: 90px;">パネルを表示してビューア内検索を可能にする Search Extension、マークアップを可能にする Markup extension による機能拡張が出来るようになりました。同様に、ジオメトリがハイパーリンクによるジャンプを実現出来るようになっています。</p>
<p style="padding-left: 60px;"><strong>Desgn Automation API</strong></p>
<p style="padding-left: 90px;">いままで、Model Derivative API で AutoCAD の DWG を表示用に変換した場合、残念ながら、AutoCAD アドインによってオブジェクトに付加された拡張エンティティ データ（XData）は、すべて削除されていました。今回、AutoCAD アドイン アプリケーションによって付加された XData が、Design Automation API で変換することで、そのまま SVF ファイルに表示されるようになりました。&#0160;</p>
<p style="padding-left: 30px;">なお、今回、初めて Forgeを利用した HoloLens のデモがありました。HoloLens は、もともと Unity を利用する仕組みですが、あくまで実験的な取り組みです。正式に HoloLens をサポートするか未定の状態です。</p>
<p>次回は、AU 2016 でのホットなトピックをお伝えします。</p>
<p>By Toshiaki Isezaki</p>
