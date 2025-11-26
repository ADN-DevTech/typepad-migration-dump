---
layout: "post"
title: "Navisworks 2014 SDK は何処に "
date: "2013-05-03 23:59:33"
author: "Mikako Harada"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/05/navisworks-2014-sdk-%E3%81%AF%E4%BD%95%E5%87%A6%E3%81%AB.html "
typepad_basename: "navisworks-2014-sdk-は何処に"
typepad_status: "Publish"
---

<p>これまで、Navisworks の&#0160;SDKは製品のインストーラの中に含まれていましたが、今回2014のリリースから、別途提供されるようになりました。ADN Open のNaviswork のページ（<a href="http://www.autodesk.com/developnavisworks">http://www.autodesk.com/developnavisworks</a>）からダウンできます。また、以下のリンクからも直接ダウンロードできます。</p>
<p>&#0160;<a href="http://images.autodesk.com/adsk/files/Navisworks_API_SDK.exe" target="_self" title="Navisworks 2014 SDK">Navisworks 2014 SDK</a>（EXE - 217 MB）</p>
<p>また、SDKをインストールする際、ファイルが展開される場所ですが、以下の3つの場合が考慮されています：</p>
<p>１）すでにNavisworks 2014がインストールされている場合 --- &#0160;&lt;Navisworksのインストールフォルダ&gt;\ api\　（以前と同じ）。</p>
<p>２）Navisworksの2014をインストールしていない場合 --- &lt;My Documents&gt;\Autodesk\Navisworks 2014\api\</p>
<p>３）任意のフォルダ</p>
<p>SDKの変更点ですが、以下の点です。</p>
<p>a) 新しいサンプル</p>
<p style="padding-left: 30px;">- InputAndRenderHandling&#0160;&#0160;--- RenderPlugin、ToolPluginとInputPlugin のAPI の機能を紹介するNETのサンプルです。</p>
<p style="padding-left: 30px;">- ActiveXWebpageExample --- 以前存在したACTX_01に類似したCOMのサンプルです。これは、再配布可能x86のActiveXコントロールを使用しています。また、他のActiveXコントロールのタイプとそれをテストすることができます。</p>
<p>b)&#0160; APIリファレンス ガイドNET API.chmは、新機能の説明を追加しており、特に、「Takeoff」の章は積算（Quantification）のAPIを使用する方法について説明しています。<br /><br />c)&#0160; 再配布可能なActiveXコントロールがx86とx64の両方でインストーラとマージモジュールが用意され、\ API\ COM\ binに用意されています。</p>
<p>原田</p>
