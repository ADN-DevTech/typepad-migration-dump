---
layout: "post"
title: "Design Automation API for Civil 3D: Public Beta"
date: "2020-07-03 02:48:50"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/07/design-automation-for-civil-3d-public-beta.html "
typepad_basename: "design-automation-for-civil-3d-public-beta"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e45468200d-pi" style="display: inline;"><img alt="Da4c" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2e45468200d image-full img-responsive" src="/assets/image_830244.jpg" title="Da4c" /></a></p>
<p>まだ、ベータ版の扱いですが、AutoCAD Civil 3D のコアエンジンが Design Automation API に加わりました。Design Automation API v3 には、既に AutoCAD、Inventor、Revit、3ds Max のコアエンジンがサポートされているため、実質、５つめのコアエンジンです。</p>
<p>Design Automation API for Civil 3D では、Civil 3D .NET API サポートが含まれています。この場合、AutoCAD 機能をカバーする AcCoreMgd.dll、AcDbMgd.dll に加え、AeccDbMgd.dll と AecBaseMgd.dll アセンブリ参照したアドイン（AppBundle）がサポートされることになります。</p>
<p>Design Automation API for Civil 3D では、デスクトップ開発と同様、最新の AutoCAD エンジン（現在は<strong>Autodesk.AutoCAD+24</strong>）を選択していただくだけです。他の指定は不要です。つまり、AutoCAD コアエンジンが、Civil 3D をサポートするようになっています。</p>
<p>なお、COM オブジェクトを含む ActiveX オートメーション API（COM API） はサポートされませんのでご注意ください。例えば、パイプ パーツフ ァミリとサイズの追加と削除、コリドー マクロ、再構築など、ActiveX に依存する一部の機能はサポートされていません。</p>
<p>Design Automation API for Civil 3D の WorkItemを実行して、追加情報を抽出、プロパティ パネルに追加する <strong><a href="https://github.com/Autodesk-Forge/forge-civil3d-properties" rel="noopener" target="_blank">サンプル</a> </strong>が用意されていますので、ぜひご確認ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec23dd0e200c-pi" style="display: inline;"><img alt="Da4c_sample" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec23dd0e200c image-full img-responsive" src="/assets/image_38869.jpg" title="Da4c_sample" /></a></p>
<p><span style="background-color: #ffff00;">なお、Design Automation API for Civil 3D の利用時には、Design Automation API for AutoCAD として処理されるため、Forge ポータルの <strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/11/forge-usage-analytics-page-changes.html" rel="noopener" style="background-color: #ffff00;" target="_blank">Usage Analytics</a></strong> には Design Automation API for AutoCAD として消費クラウドクレジットをカウント、表示されることになります。このため、Design Automation API for Civil 3D（Civil 3D 機能を使った AppBundle）の WorkItem 処理は Beta 扱いなのですが、クラウドクレジットによる課金対象となってしまいますので、ご注意ください。</span>可能であれば、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/introducing-90-day-forge-trials.html" rel="noopener" target="_blank">トライアル</a></strong> アカウントでの評価をお勧めいたします。</p>
<p>By Toshiaki Isezaki</p>
