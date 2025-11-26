---
layout: "post"
title: "Web ブラウザの能力評価"
date: "2019-03-20 00:26:21"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/capabilities-evaluation-for-web-browsers.html "
typepad_basename: "capabilities-evaluation-for-web-browsers"
typepad_status: "Publish"
---

<p>オートデスクのクラウド サービスや Forge Viewer では、ストリーミング配信された 2D 図面や 3D モデルを表示するために、<strong><a href="https://ja.wikipedia.org/wiki/WebGL" rel="noopener" target="_blank">WebGL</a></strong> をサポートする Web ブラウザが必要なのはご存知のとおりです。</p>
<p>過去にも<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/01/unable-to-use-a360-viewer-on-firefox.html" rel="noopener" target="_blank">ご紹介</a></strong>した内容ですが、使いの Web ブラウザが WebGL をサポートしているかチェックするには、<strong><a href="http://get.webgl.org/" rel="noopener" target="_blank">http://get.webgl.org/</a></strong> へのアクセスが簡単です。WebGL をサポートする Web ブラウザであれば、下図のように立方体がアニメーションで回転表示されるます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a446332e200c-pi" style="display: inline;"><img alt="Welgl_supported" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a446332e200c image-full img-responsive" src="/assets/image_624262.jpg" title="Welgl_supported" /></a></p>
<p>Forge の利用が広まるにつれて、<strong><a href="http://get.webgl.org/" rel="noopener" target="_blank">http://get.webgl.org/</a></strong> で WebGL をサポートしていると表示されたのに Forge Viewer が期待した動作をしない、といった質問を稀にいただきます。そのような場面では、ラウザ キャッシュのクリアや、他のブラウザで動作検証と同時に、Web ブラウザに搭載されている <a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener" target="_blank"><strong>デベロッパーツール</strong></a>で原因を特定していくことになります。ただし、その前にチェックしていただきたいサイトがあります。<strong><a href="https://html5test.com/" rel="noopener" target="_blank">HTML5 TEST（https://html5test.com/）</a></strong>サイトです。</p>
<p>HTML5 TEST サイトは、WebGL サポートも含めた Web ブラウザの総合評価をスコアとともに表示してくれます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a493fe30200b-pi" style="display: inline;"><img alt="Html5test" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a493fe30200b image-full img-responsive" src="/assets/image_127841.jpg" title="Html5test" /></a></p>
<p>Forge Viewer の運用では、WebGL のサポート以外にも、Web ブラウザ の能力によって、ブラウザ内で動作させる JavaScript プログラムの実行そのものが影響を受けている可能性があります。特に日本の企業で標準とされていた（されている）Internet Explorer 11 は、 WebGL をサポートしていても、最新の JavaScript 仕様に対応出来ていません。クライアント側で思い切った実装をしてしまうと、正しく動作しない、といった結果になってしまっているようです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46f66c4200d-pi" style="display: inline;"><img alt="Html5test_ie11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46f66c4200d image-full img-responsive" src="/assets/image_281375.jpg" title="Html5test_ie11" /></a></p>
<p>Forge Viewer は Internet Explorer 11 をサポート ブラウザの 1 つとして明記していて、事実、<strong><a href="https://viewer.autodesk.com/" rel="noopener" target="_blank">Autodesk Viewer</a> </strong>も動作します。しかし、Forge Viewer 用にカスタム処理を施した JavaScript が正しく動かない可能性があります。サポート機能が最新ではない点は Microsoft 社も懸念を表明（<strong><a href="https://techcommunity.microsoft.com/t5/Windows-IT-Pro-Blog/The-perils-of-using-Internet-Explorer-as-your-default-browser/ba-p/331732" rel="noopener" target="_blank">https://techcommunity.microsoft.com/t5/Windows-IT-Pro-Blog/The-perils-of-using-Internet-Explorer-as-your-default-browser/ba-p/331732</a></strong>）しているので、その利用は積極的にお勧めしていません。</p>
<p>Forge Viewer が正しく動作しない、という場面では、ぜひ、HTML5 TEST もお試しください。スコアはあくまで参考ですが、オートデスクがお勧めしている Google Chrome が比較的高い値を表示するのは事実です。</p>
<p>By Toshiaki Isezaki</p>
