---
layout: "post"
title: "TinkerCAD の Sim Lab 機能"
date: "2023-04-26 00:10:42"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/04/-tinkercad-sim-lab.html "
typepad_basename: "-tinkercad-sim-lab"
typepad_status: "Publish"
---

<p>このブログでも、過去、わずかですが <a href="https://adndevblog.typepad.com/technology_perspective/2017/01/release-japanese-version-of-tonkercad.html">日本語版 TinkerCAD リリース</a>&#0160;のように、<a href="https://www.tinkercad.com/" rel="noopener" target="_blank">ThinkerCAD</a> について触れています。どちらかというと教育素材として利用されている ThinkerCAD ですが、今回、簡易的なものですが、ユニークな重力シミュレーション「Sim Lab」が加わりましたので、ご紹介しておきましょう。</p>
<p>重力シミュレーションと言っても複雑な操作や設定が必要なものではありません。3D デザインにプリセットされたマテリアル（材質）を指定して、「Play Simulation」ボタンをクリックするだけです。</p>
<p>例えば、「3D デザイン」ワークスペースで、プリセットのバスケットボール シェイプを作業平面から上部に移動配置、「リンゴの落下」アイコンで「Sim Lab」ワークスペースに移動して、マテリアルを指定後、「Play Simulation」するだけです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751787f95200b-pi" style="display: inline;"><img alt="Gravity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751787f95200b image-full img-responsive" src="/assets/image_939524.jpg" title="Gravity" /></a></p>
<p>作業平面が地面に相当するので、作業平面からがずれたシェイプは、そのまま落下していきます。</p>
<p>まだ、「Sim Lab」ワークスペースは日本語化されていませんが、簡単な操作なので、さほどハードルにはならないように思います。（2023年3月25日現在）</p>
<p>作業平面上にいくつかシェイプを配置すると、落下したバスケットボール同様、それらシェイプへの影響もシミュレーション出来ることがわかります。</p>
<p>落下するバスケットボールのマテリアルを変更すると、弾性や質量にともなう落下後の違いも理解出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852fa4b4200d-pi" style="display: inline;"><img alt="Differences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852fa4b4200d image-full img-responsive" src="/assets/image_379842.jpg" title="Differences" /></a></p>
<p>「Sim Lab」では、シェイプ毎に重力や衝突の影響を受けないように指定することも出来ます。シェイプ選択後に画面右上の「Make static」をクリックするだけです。逆に、static（静的）な設定のシェイプを選択すると、同じボタンに「Make dynamic」と表示されるので、クリックして dynamic（動的）な設定にすることで、再度、重力や衝突の影響を受けるように変更することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751788101200b-pi" style="display: inline;"><img alt="Make_static" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751788101200b image-full img-responsive" src="/assets/image_487422.jpg" title="Make_static" /></a></p>
<p>また、シミュレーションの再生中にマウスの左ボタンをクリックすると、マウスカーソルの表示位置に合わせてランダムなシェイプを投げつけることも出来ます。「Make static」に指定した静的なシェイプは、投げつけたシェイプが当たっても倒れません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7519d125b200c-pi" style="display: inline;"><img alt="Throw" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7519d125b200c image-full img-responsive" src="/assets/image_833942.jpg" title="Throw" /></a></p>
<p>プロフェッショナル ツールではないので、詳細な条件を設定したり、何らかの数値を出力したりすることは出来ませんが、作成した 3D モデルが重力や慣性にどう影響を受けるのか、簡単に視覚化出来るユニークな機能と思います。</p>
<p><a href="https://www.tinkercad.com/blog/tinkercad-sim-lab" rel="noopener" target="_blank">Tinkercad ブログ: Put Designs in Motion with Tinkercad Sim Lab | Tinkercad</a>&#0160;のブログ記事（英語）では、さまざまな「Sim Lab」サンプルが記載されています、ぜひ、覗いてみてください。</p>
<p>By Toshiaki Isezaki</p>
