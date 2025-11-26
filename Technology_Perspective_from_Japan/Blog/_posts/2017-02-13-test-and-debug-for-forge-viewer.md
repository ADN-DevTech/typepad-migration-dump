---
layout: "post"
title: "Forge Viewer のテストとデバッグ"
date: "2017-02-13 00:13:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/02/test-and-debug-for-forge-viewer.html "
typepad_basename: "test-and-debug-for-forge-viewer"
typepad_status: "Publish"
---

<p>Forge Viewer を利用した開発では、実装した JavaScript や表示しようとするモデルに問題あると、実行が中断してしまう場合があります。また、Forge Viewer が新しいバージョンになったタイミングで、それまでと異なるう動きをするようなこともある可能性があります。</p>
<p>このような場面で役立つサイトがあります。LMV Ninja と名づけられた <a href="http://lmv.ninja.autodesk.com/" rel="noopener noreferrer" target="_blank">http://lmv.ninja.autodesk.com/ </a>サイトです。このサイトを利用すると、Forge Viewer のバージョンを指定してモデルを表示させたり、ページ上に用意されたログ情報の出力レベルを変えて、表示動作を詳細に確認することが出来ます。</p>
<p>残念ながら自身で作成した JavaScript コードをテストすることは出来ませんが、モデルに問題があるのか、バージョン差によって振る舞いが異なるかなどを手早く切り分ける手助けとなります。オートデスクの開発チームが開発中の Viewer 機能や Extension も垣間見ることができるので、知っておくと同じような Extension 開発を避けることが出来るかも知れません。</p>
<p>もし、デバッグ情報を参照する場合には、ブラウザの F12 キーを押してデベロッパー ツールを表示して、[Console] タブを表示させてみてください。&#0160;Viewer 上にモデルが表示された場合でも、選択したオブジェクトとは異なるオブジェクトが選択されてしまったり、プロパティやモデル階層が空になっているなど、稀にモデルのロード時にエラーが発生している場合があります。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/2yABsIwg5WU?feature=oembed" width="500"></iframe></p>
<p>独自に用意したモデルや図面を A360 プロジェクトに保存しておけば、LMV Ninja 上で表示させてテストすることも可能です。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/4zh7ldRkAtg?feature=oembed" width="500"></iframe></p>
<p>デベロッパーツールは LMV Ninja 以外のページでも利用出来る Web ブラウザ側の機能です。[Source] タブに JavaScript コードを表示させてステップ実行をしながら変数の中身を確認したり、[Console] タブに表示されるエラーや &#0160;[Network] タブに表示される REST 呼び出しのステータスをチェックしたりするなど、何かと役に立ちます。</p>
<p>by Toshiaki Isezaki</p>
