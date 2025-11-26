---
layout: "post"
title: "Revit 2015 マス スタディで分割サーフェスとパターンコンポーネントを活用する"
date: "2015-03-20 04:14:27"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/03/revit-2015-surface-pattern.html "
typepad_basename: "revit-2015-surface-pattern"
typepad_status: "Publish"
---

<p>以前、<a href="http://adndevblog.typepad.com/technology_perspective/2015/03/revit-2015-form.html" target="_blank">Revit 2015 のマス スタディにおけるフォーム操作について</a>ご紹介しましたが、今回は、フォームのサーフェスを分割することで、パターンを適用してデザインをプレビューする方法を解説いたします。<br /> 単純なフォーム操作だけでは、ボリュームを組み合わせることしかできませんが、面（サーフェス）を構成する要素（コンポーネント）をパターンに適用することで、より複雑なサーフェスをデザインすることができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080b25eb970d-pi" style="display: inline;"><img alt="Surface_example" class="asset  asset-image at-xid-6a0167607c2431970b01bb080b25eb970d img-responsive" src="/assets/image_498676.jpg" title="Surface_example" /></a></p>
<p>Revit 2015 では、フォームのサーフェスを分割すると、 UV グリッドが作成されます。<br />これはパターンを適用するためのガイドのようなものです。UV グリッドの分割数や回転角度等はプロパティで設定変更することができます。</p>
<p>APIでは、DividedSurface.GetReferencesWithDividedSurface() メソッドと &#0160;DividedSurface.GetDividedSurfaceForReference() メソッドを使用して、分割サーフェスのデータにアクセスしたり、新しく分割サーフェスを作成することができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f0c8e8970c-pi" style="display: inline;"><img alt="Surface_uv" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f0c8e8970c img-responsive" src="/assets/image_17519.jpg" title="Surface_uv" /></a></p>
<p>フォームの分割サーフェスは、デフォルトではパターンが適用されておりませんが、様々な組み込みのレイアウトパターンに変更することができます。またさらに、このレイアウトパターンを独自に作成することもできます。<br />API では、 TilePatternsBuiltIn クラスを使用すれば組み込みのパターンを適用できますし、テンプレートファイルを利用すれば独自のパターンを作成することもできます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c76731f9970b-pi" style="display: inline;"><img alt="Pattern2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c76731f9970b img-responsive" src="/assets/image_364551.jpg" title="Pattern2" /></a></p>
<p>パターンは、個別のマス ファミリで管理されており、これをパターンコンポーネントファミリと呼びます。パターンコンポーネントは、スケッチ線分だけではなく、フォーム等で立体形状として作成することができます。</p>
<p>したがって、ファサードのテクスチャとしてだけではなく、カーテンパネルやストラクチャとしてパターンコンポーネントファミリを設計し、これをマス スタディでプレビューするといった用途でもご利用いただけます。<br />また分割サーフェスに適用された後、パターンコンポーネントの一部を個別に修正することもできます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f0c91c970c-pi" style="display: inline;"><img alt="Pattern_component" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f0c91c970c img-responsive" src="/assets/image_31111.jpg" title="Pattern_component" /></a><br /><br />分割サーフェスとパターンコンポーネントの機能をAPIを利用してカスタマイズする際は、以下の開発者マニュアルをご参照ください。<br /><a href="http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-63B37608-4112-4424-B46A-ED61ACE95E7F" target="_blank">http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-63B37608-4112-4424-B46A-ED61ACE95E7F</a></p>
<p>By Ryuji Ogasawara</p>
<p>&#0160;</p>
<p>&#0160;</p>
