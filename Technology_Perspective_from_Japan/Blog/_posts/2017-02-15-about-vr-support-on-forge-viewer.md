---
layout: "post"
title: "Forge Viewer の VR サポートについて"
date: "2017-02-15 01:13:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/02/about-vr-support-on-forge-viewer.html "
typepad_basename: "about-vr-support-on-forge-viewer"
typepad_status: "Publish"
---

<p>Forge Viewer の新しいバージョン 2.13 がリリースされました。開発フェーズでは、バージョン 2.12 も用意されていましたが、 1 つスキップしてのリリースです。バージョン 2.12 の注目すべき機能に、<strong><a href="https://webvr.info/" rel="noopener noreferrer" target="_blank">WebVR</a></strong>&#0160;を利用する新しい &#0160;’Autodesk.Viewing.webVR’ &#0160;Extension のサポートがあります。</p>
<p>’Autodesk.Viewing.webVR’ Extension を Viewer にロードすれば、追加のコーディングを必要せずに 基本的な VR（Virtual Reality、バーチャル リアリティ）の能力をサポートすることが出来ます。従来、この部分を独自に実装したデモがいくつかありましたが、デザイン ファイルを Model Derivative API で変換して Forge Viewer に表示する従来の方法で、左右視差を持つ VR 表示を実現することが出来るようになります。</p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/02/code-evolution-to-show-models-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer でのモデル表示コードの進化</a></strong> でご紹介した&#0160;<strong>Basic Application</strong> 方式で Extension をロードする場合には、次のようなコード抜粋になるはずです。&#0160;</p>
<pre>  Autodesk.Viewing.Initializer(options, function onInitialized(){<br />    var avp = Autodesk.Viewing.Private;<br />    avp.logger.setLevel( avp.LogLevels.Debug );<br />    var config3d = {<br />      <strong>extensions: [&#39;Autodesk.Viewing.webVR&#39;]</strong><br />    };<br />    viewerApp = new Autodesk.Viewing.ViewingApplication(&#39;viewerDiv&#39;);<br />    viewerApp.registerViewer(viewerApp.k3D, Autodesk.Viewing.Private.GuiViewer3D, config3d);<br />    viewerApp.loadDocument(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);<br />  });</pre>
<p>なお、バージョン 2.12 では、Extension 名&#0160;<strong>Autodesk.Viewing.webVR </strong>の webVR の最初の文字が小文字になっている点に注意してください。Extension 名は、この後説明するバージョン 2.13 で変更されていて、<strong>Autodesk.Viewing.WebVR&#0160;</strong>になっています。 明示的な理由がない限りバージョン 2.13 を利用すべきですが、Forge Viewer 使用時に&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/specifying-version-to-forge-viewer.html" rel="noopener noreferrer" target="_blank">バージョン指定</a></strong>&#0160;が可能なため、あえてお知らせしておきます。</p>
<p>さて、Extension が正しくロードされると、WebVR をサポートする環境でのみ標準ツールバーに VR ボタンが表示されます。Windows PC や Mac の Web ブラウザでは VR ボタンは表示されません。当初、Oculus Rift や HTC Vive もサポートする予定もあったようですが、現時点では Google Cardboard、つまりスマートフォンでの使用がサポートされています。</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8d63a65970b-pi" style="display: inline;"><img alt="IMG_4931" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8d63a65970b image-full img-responsive" src="/assets/image_542353.jpg" title="IMG_4931" /></a></p>
<p>バージョン 2.12 でサポートされるのは、表示したモデルに対しての基本的な VR 機能のみです。まだ、ウォークスルーのような機能は搭載されていないので、モデルの外側を表示した場合には、VR ボタンで VR モードに移行してから、タップ操作等で適切な表示状態を手動で作る必要があります。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/iwS-EiJq1Bk?feature=oembed" width="500"></iframe>&#0160;</p>
<p>もし、建物などの屋内の 状態に表示を切り替えることが出来れば、頭部の動き合わせて周囲を見回す VR らしさを体感することも出来ます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/HRaVv8keS3o?feature=oembed" width="500"></iframe></p>
<p>バージョン 2.13 では、前述のとおり、 Extension 名が&#0160;<strong>Autodesk.Viewing.WebVR&#0160;</strong>に変更されています。同時に新しく導入された&#0160;<strong>’webVR_orbitModel&#39;</strong> フラグが用意され、Cardboard を装着した頭部の動きに合わせて、表示中の3D モデル自身を回転させるオービット処理を実現することが出来ます。直感的な VR とはいえませんが、「実験」として実装されています。</p>
<p><strong>Basic Application</strong> 方式でバージョン 2.13 の Forge Viewer に Autodesk.Viewing.<strong>W</strong>ebVR&#0160;Extension ロードと &#0160;webVR_orbitModel フラグの指定を実装したコード抜粋は次のとおりです。&#0160;</p>
<pre>  Autodesk.Viewing.Initializer(options, function onInitialized(){<br />    var avp = Autodesk.Viewing.Private;<br />    avp.logger.setLevel( avp.LogLevels.Debug );<br />    var config3d = {<br />      <strong>extensions: [&#39;Autodesk.Viewing.WebVR&#39;],</strong><br /><strong>      experimental: [&#39;webVR_orbitModel&#39;]</strong><br />    };<br />    viewerApp = new Autodesk.Viewing.ViewingApplication(&#39;viewerDiv&#39;);<br />    viewerApp.registerViewer(viewerApp.k3D, Autodesk.Viewing.Private.GuiViewer3D, config3d);<br />    viewerApp.loadDocument(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);<br />  });</pre>
<p>webVR_orbitModel フラグの指定時の動作は、次のようなものになります。&#0160;</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/gFioJK_Gw0U?feature=oembed" width="500"></iframe></p>
<p><strong>Autodesk.Viewing.WebVR</strong> Extension を利用するには、明示的に Forge Viewer でバージョン 2.13 の参照を<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/specifying-version-to-forge-viewer.html" rel="noopener noreferrer" target="_blank">指定</a></strong>する必要があります。バージョン指定をせずに使用した場合は、Forge Viewer &#0160;を利用しているオートデスク製品にあわせて、無条件に バージョン 2.10 が適用されてしまいます。バージョン 2.10 には&#0160;<strong>Autodesk.Viewing.WebVR</strong> Extension をロードさせることが出来ませんのでご注意ください。</p>
<p>Forge Viewer での VR サポートでは、experimental フラグの追加も含め、今後の Viewer バージョンで機能が徐々に追加されていく予定です。ご期待ください。</p>
<p>By Toshiaki Isezaki</p>
