---
layout: "post"
title: "Revit 2017 の新機能 ～ その2"
date: "2016-05-13 01:55:12"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/05/new-features-on-revit-2017-part2.html "
typepad_basename: "new-features-on-revit-2017-part2"
typepad_status: "Publish"
---

<div>前回は、<a href="http://adndevblog.typepad.com/technology_perspective/2016/04/new-features-on-revit-2017-part1.html" target="_blank">Revit 2017 の各専門分野に共通の新機能</a>について解説致しました。</div>
<div>今回は、建築分野の新機能と更新内容、API の対応状況をご紹介していきます。</div>
<div><strong>&#0160;</strong></div>
<div><span style="font-size: 14pt;"><strong>デプス キューイング</strong></span></div>
<div>Revit 2017 の新機能として、建築ビューとコーディネーション ビューに、デプス キューイング機能が導入されました。</div>
<div>この機能を使用すると、立面図ビューと断面図ビューで、ビューの前面から最も離れた位置にある要素と、ビューの前面に最も近い要素を、グラデーションをかけて表示することができます。オブジェクトが遠ざかるほど背景色に溶け込むように処理することで、奥行きの効果が生まれます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08fdd3e1970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Depth1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08fdd3e1970d image-full img-responsive" src="/assets/image_79599.jpg" title="Depth1" /></a></div>
<div>&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08fdd23d970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Depth2" class="asset  asset-image at-xid-6a0167607c2431970b01bb08fdd23d970d img-responsive" height="300" src="/assets/image_451483.jpg" title="Depth2" width="230" /></a></div>
<div>&#0160;</div>
<div>デプス キューイングの機能は、API では ViewDisplayDepthCueing クラスが追加されました。</div>
<div style="padding-left: 30px;">ViewDisplayDepthCueing.EnableDepthCueing(): 有効/無効の設定</div>
<div style="padding-left: 30px;">ViewDisplayDepthCueing.StartPercentage / EndPercentage: フェードを開始/終了する位置における、前方クリップ面からの％のオフセット値</div>
<div style="padding-left: 30px;">ViewDisplayDepthCueing.FadeTo: フェードの％値の最大値を定義する</div>
<div style="padding-left: 30px;">&#0160;</div>
<div style="padding-left: 30px;">DBView.GetDepthCueing(),&#0160;DBView.SetDepthCueing(): ViewDisplayDepthCueing オブジェクトを取得、設定することができます。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>点群の表示設定</strong></span></div>
<div>次に Revit 2017 では、点群データの表示機能が強化されました。1 つ以上の点群をモデルに読み込むと、各ビューの個々のスキャン位置とスキャン領域の表示設定をコントロールできるようになります。 この機能により、特定のビュー内の重要ではない点群情報や関係のない点群情報を非表示にすることができます。また、モデルで作業する際のパフォーマンスを向上させることもできます。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>レンダリング</strong></span></div>
<div>Autodesk Raytracer は現在、すべてのレンダリング機能に使用されています。レンダリング エンジンを選択する必要はなくなりました。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1e40f8f970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Rendering" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1e40f8f970c image-full img-responsive" src="/assets/image_801462.jpg" title="Rendering" /></a></div>
<div>&#0160;</div>
<div style="clear: both;">ウォークスルーを書き出す際、表示スタイルがレンダリングに設定されていると、ウォークスルー ビューに指定した Autodesk Raytracer のレンダリング設定が書き出しに使用されます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c85a4722970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Walkthrough" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c85a4722970b image-full img-responsive" src="/assets/image_820318.jpg" title="Walkthrough" /></a></div>
<div>&#0160;</div>
<div>モデルをアップグレードすると、NVIDIA mental ray エンジン用に指定したレンダリング設定が、Autodesk Raytracer の適切な設定にマップされます。</div>
<div>&#0160;</div>
<div>[レンダリング]ダイアログでは、新しいスタイルの背景を使用できます。[透明]を選択すると透明な背景が作成されます。この背景は、イメージを PNG または TIFF 形式で書き出す際に維持されます。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>ホストされた手すり</strong></span></div>
<div>床、スラブ、スラブ ハンチ、壁、屋根の上面に手すりをスケッチできるようになりました。 手すり子および手すりは、不規則なサーフェスの勾配に合わせて調整されます。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/XLcF2gHHfoA?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>API では、Railing クラスが追加されました。</div>
<div style="padding-left: 30px;">Railing.Create():&#0160;手摺のパスから新規に手摺を作成する</div>
<div style="padding-left: 30px;">Railing.SetPath():&#0160;手摺のパスをセットする</div>
<div style="padding-left: 30px;">Railing.RailingCanBeHostedByElement():&#0160;手摺のホストとして使用できる要素か確認する</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>インプレイス要素</strong></span></div>
<div>インプレイス ジオメトリの使用可能なファミリ カテゴリのリストに階段が追加されました。 モデルのコンテキスト内に階段のインプレイス要素を作成できます。</div>
<div>&#0160;</div>
<div>インプレイス要素とは、プロジェクトのコンテキスト内で作成するカスタムの要素です。</div>
<div>&#0160;</div>
<div>使用するプロジェクトで、再使用を想定しない独特のジオメトリや、他のプロジェクト ジオメトリとの 1 つまたは複数の関係を維持することが必要なジオメトリが必要な場合には、インプレイス要素を作成します。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>パース ビュー</strong></span></div>
<div>Revit 2016 R2&#0160;では、いくつかのモデリング機能がパース ビューで使用できるようになりました。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/MCQ3J6HZLyw?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>壁接合部</strong></span></div>
<div>壁を配置するときに、[結合のステータス]オプションを使用して壁の結合を許可/禁止できるようになりました。</div>
<div>複数の壁結合部を選択して、選択したすべての結合部の構成を[突合せ]、[留め継ぎ]、[直角にする]に変更できます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1e40e08970c-pi" style="display: inline;"><img alt="Wallcombine" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1e40e08970c image-full img-responsive" src="/assets/image_32421.jpg" title="Wallcombine" /></a></div>
<div>&#0160;</div>
<div>
<div>
<div>次回は、構造分野の機能強化についてご紹介いたします。</div>
<div>&#0160;</div>
<div>By Ryuji Ogasawara</div>
</div>
<div>&#0160;</div>
</div>
