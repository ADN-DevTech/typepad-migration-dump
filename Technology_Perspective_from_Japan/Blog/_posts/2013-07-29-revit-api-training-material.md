---
layout: "post"
title: "Revit 2014 API トレーニング マテリアル"
date: "2013-07-29 18:12:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/07/revit-api-training-material.html "
typepad_basename: "revit-api-training-material"
typepad_status: "Publish"
---

<p>だいぶお待たせしましたが、今回、Revit API トレーニング マテリアルを公開しました。このマテリアルは、従来、Revit Developer Center ページ（<a href="http://www.autodesk.com/developrevit" target="_blank">http://www.autodesk.com/developrevit</a>）で英語版で公開していたものを日本語化したものです。日本語版の公開にあたり、対象を Revit 2014 として下記の <a href="https://knowledge.autodesk.com/ja/" target="_blank"><strong>Autodesk Knowledge Network</strong> </a>でダウンロードしていただくことが出来ます。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/revit-products/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u09x.html" target="_blank">Revit 2014 API のトレーニングリソース</a></strong></p>
<p>ダウンロードした ZIP ファイルをフォルダ付きで解凍すると、Revit 2014 API Training フォルダが作成され、直下に次のフォルダが用意されます。</p>
<ul>
<li><span style="font-size: 10pt;"><strong>Labs フォルダ</strong><br /></span><span style="font-size: 10pt;">入門実習、UI実習、ファミリAPI実習の3つのコース内容について、C# と VB.NET 毎の開発言語別の実習ドキュメントと完成したVisual Studioプロジェクトが格納されています。実習ドキュメントには、オンラインヘルプ形式の Revit API Training.chm と、印刷用の PDF があり、後者は、コース別のフォルダ内に言語別に格納されています。PDFファイルは印刷に便利です。</span></li>
</ul>
<p style="text-align: left; padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac428ff3970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CHMMaterial" class="asset  asset-image at-xid-6a0167607c2431970b0192ac428ff3970d" src="/assets/image_270790.jpg" title="CHMMaterial" /></a></p>
<p style="text-align: left; padding-left: 120px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e832464970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PDFMaterial" class="asset  asset-image at-xid-6a0167607c2431970b01901e832464970b" src="/assets/image_936280.jpg" title="PDFMaterial" /></a></p>
<ul>
<li><span style="font-size: 10pt;"><strong>Presentation フォルダ</strong><br /></span><span style="font-size: 10pt;">クラス トレーニング実施時の PowerPoint プレゼンテーションの PDF 版です。1ページ1スライドのものと、1ページ2ページ（印刷用）のものが用意されています。</span></li>
</ul>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e832602970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="PPTxTopPage" class="asset  asset-image at-xid-6a0167607c2431970b01901e832602970b" src="/assets/image_32967.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="PPTxTopPage" /></a></p>
<ul>
<li><span style="font-size: 10pt;"><strong>Sample Drawing フォルダ</strong><br /></span><span style="font-size: 10pt;">実習トレーニング中で利用する Revit プロジェクトファイルが格納されています。日本語版 Revit や含まれるテンプレートを使用すると、必要とするファミリ名などが日本語表現になっているため、実習で作成するコードの実行に失敗することがあります。</span></li>
</ul>
<ul>
<li><span style="font-size: 10pt;"><strong>Wizards フォルダ</strong><br /></span><span style="font-size: 10pt;">オートデスク ブログ<a href="(http://thebuildingcoder.typepad.com/blog/2013/05/add-in-wizards-for-revit-2014-1.html" target="_blank">(http://thebuildingcoder.typepad.com/blog/2013/05/add-in-wizards-for-revit-2014-1.html</a>)に記載されている Revit アドイン用の Visual Studio スケルトンプロジェクト作成のウィザードです。開発言語別に C# 版と VB.NET 版が用意されています。</span></li>
</ul>
<p>トレーニング マテリアル内でも触れていますが、Revit API の習得には、もちろん Revit SDK も必要になります。Revit SDK は、Revit Developer Center ページ（<a href="http://www.autodesk.com/developrevit" target="_blank">http://www.autodesk.com/developrevit</a>）からダウンロードしていただくことが出来ます。</p>
<p>Revit SDK の Samples フォルダに含まれるサンプル プロジェクトにも当てはまりますが、日本語版 Revit や、日本語版 Revit に含まれるプロジェクト テンプレートやファミリ テンプレートを利用した場合、アドインのコードを参照するファミリタイプ名やファミリ名が日本語化されていないため、実行途中でエラーになってしまうことがあります。また、Revit のバージョンアップに伴って Revit API の内容、特にクラス/メソッド/プロパティの利用方法が変わってしまうことがあります。</p>
<p>Labs フォルダのソースコードには、英語版 Revit/テンプレート との差異、Revit 2013 との差異などををコメントで残してあります。このマテリアルを参照いただいて、日本語版 Revit 2014 での API の使用方法を改めてご確認いただければと思います。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
