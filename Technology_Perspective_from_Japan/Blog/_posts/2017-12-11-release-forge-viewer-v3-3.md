---
layout: "post"
title: "Forge Viewer バージョン 3.3 リリース"
date: "2017-12-11 00:00:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/12/release-forge-viewer-v3_3.html "
typepad_basename: "release-forge-viewer-v3_3"
typepad_status: "Publish"
---

<p>Forge Viewer の新バージョン&#0160; 3.3 がリリースされています。今回はバージョン 3.3 での API 上の新機能をご紹介したいと思います。</p>
<hr />
<p><strong>非フォトグラフィックス レンダリング スタイルの追加</strong></p>
<p>このバージョンでは、3D モデル表示時のフォトリアル表現に加えて、非フォトリアルなアーティスティックな表現が可能になりました。これによって、印象をやわらげ、デザインの提案などにも利用出来るようになります。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c5210b970c-pi" style="display: inline;"><img alt="Standard" class="asset asset-image at-xid-6a0167607c2431970b01b8d2c5210b970c img-responsive" src="/assets/image_848618.jpg" title="Standard" /></a></p>
<p>例えば、環境を「広場」に設定した上記のようなフォトリアル表現は、<strong>edging</strong>、<strong>cel</strong>、<strong>graphite</strong>、<strong>pencil</strong>&#0160; のいずれかのスタイル指定によって、次のように表示スタイルを変化させることが出来ます。もちろん、このな表現のまま、3D モデルをリアルタイムにナビゲーションすることが出来るようになります。従来のように一定の演算時間が必要なレンダリング画像の生成は不要ですが、モバイル デバイスのようにグラフィックス機能が高くないデバイスでは、フレームレートが自動調整される場合があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09dde57f970d-pi" style="display: inline;"><img alt="Styles" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09dde57f970d image-full img-responsive" src="/assets/image_748185.jpg" title="Styles" /></a></p>
<p>スタイルの変更はビューアでモデルを表示した後のポスト処理（事後処理）になります。当該メソッドは、Viewer3D クラスに追加されています。</p>
<p>ビューア表示後に必要なポスト処理は次のようになります。</p>
<pre>&#0160;viewer.impl.preloadPostProcessStyle();<br /> viewer.impl.setPostProcessParameter( &quot;style&quot;, &quot;pencil&quot; );</pre>
<p>preloadPostProcessStyle() を呼び出さずに、graphite スタイルや pencil スタイルなどのテクスチャ情報が必要な表現を指定すると、最初の表示が完了するまで若干時間が必要になる場合がありますのでご注意ください。</p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="https://www.youtube.com/embed/mW5nSgNwyiA" width="480"></iframe></p>
<p>setPostProcessParameter() に指定可能なパラメータは、”style” の各値以外にも、下記のパラメータとの組み合わせで個別に値を指定することが出来ます（setPostProcessParameter()&#0160; の複数行呼び出し）。</p>
<p style="padding-left: 30px;"><strong>style</strong></p>
<p style="padding-left: 60px;">許容されるパラメータ:</p>
<p style="padding-left: 90px;">&quot;off&quot; あるいは ””（未指定）<br /> &quot;edging&quot;<br /> &quot;cel&quot;<br /> &quot;graphite&quot;<br /> &quot;pencil&quot;</p>
<p style="padding-left: 30px;"><strong>brightness</strong></p>
<p style="padding-left: 60px;">値の範囲: -1.0 から 1.0 (既定値: 0.0)<br />輝度の強さを調整します。</p>
<p style="padding-left: 30px;"><strong>contrast</strong></p>
<p style="padding-left: 60px;">値の範囲: -1.0 から 1.0 (既定値: 0.0)<br />コントラストを調整します。</p>
<p style="padding-left: 30px;"><strong>preserveColor</strong></p>
<p style="padding-left: 60px;">ブール値: true または false (既定値: false)<br />スケール カラーを有効化、または、クランプ カラーを無効化します。</p>
<p style="padding-left: 60px;">例:<br />false 指定時にカラー (4.0,1.0,0.5) は (1.0,1.0,0.5) に変換<br />true 指定時にカラー (4.0,1.0,0.5) は (1.0,0.25,0.125) に変換 ー 各要素は最大4.0で除算されます。</p>
<p style="padding-left: 30px;"><strong>grayscale</strong></p>
<p style="padding-left: 60px;">ブール値: true または false (既定値: false)<br />上記操作後で処理前に画像の彩度を上げることができます。<br />&quot;graphite&quot; スタイルには効果がないことに注意してください。</p>
<p style="padding-left: 30px;"><strong>edges</strong></p>
<p style="padding-left: 60px;">ブール値: true または false (既定値: true)<br />エッジを表示する（スクリーンベース）を有効にします。 （反射された画像にエッジが表示されないことに注意してください）<br />edges パラメータは、すべてのエッジをオンまたはオフに設定します。 特定のエッジタイプには、3つの個別のトグルがあります。</p>
<p style="padding-left: 30px;"><strong>idEdges</strong></p>
<p style="padding-left: 60px;">ブール値: true または false (既定値: true)<br />個別のIDまたは背景を持つオブジェクトが一致するエッジを表示するようにします。 これらのオブジェクトの内部エッジは表示されません。</p>
<p style="padding-left: 30px;"><strong>normalEdges</strong></p>
<p style="padding-left: 60px;">ブール値: trueまたはfalse (default: true)<br />法線が「十分」であるエッジを表示して、鋭いエッジが表示されるようにします。</p>
<p style="padding-left: 30px;"><strong>depthEdges</strong></p>
<p style="padding-left: 60px;">ブール値: true または false (既定値: true)<br />深度が「十分」異なるエッジを表示して、遠くのオブジェクト間のエッジが表示されるようにします。 これは、IDと法線が同じになるように、単一オブジェクト内の平面が平行であるケースをキャッチします。</p>
<p style="padding-left: 30px;"><strong>levels (&quot;cel&quot; スタイルのみ)</strong></p>
<p style="padding-left: 60px;">値の範囲: 2.0 から 256.0 (既定値: 6.0)<br />ポスタリゼーションのレベル数を設定します。通常値の範囲: 2.0 から 8.0<br />分数値は有効であり、1つのレベルから別のレベルへのスムーズな移行が行われることに注意してください。</p>
<p style="padding-left: 30px;"><strong>repeats (&quot;graphite&quot; と &quot;pencil&quot; スタイルのみ)</strong></p>
<p style="padding-left: 60px;">値の範囲: 0.001 から 100.0 (既定値: 3.0)<br />これらのスタイルで使用されるテクスチャパターンがビュー内で繰り返される回数を設定します。： top から bottom. 通常値の範囲: 1.0 - 5.0.<br />この数を増やすと、パターンは小さくなり、繰り返しとして検出される可能性が高くなります。</p>
<p style="padding-left: 30px;"><strong>rotation (&quot;graphite&quot; と &quot;pencil&quot; スタイルのみ)</strong></p>
<p style="padding-left: 60px;">値の範囲: 0.0 から 1.0 (既定値: 0.0)<br />これらのスタイルで使用されるテクスチャパターンの時計回りの回転を設定します。 0.0 は回転なしを意味し、1.0 は 180度です。<br />既定値のパターンは、左下から右上の向きになります。</p>
<hr />
<p><strong>選択時とマウスホバー時のハイライト</strong></p>
<p>既定値では、ビューアに表示した 3D オブジェクトの上にマウスを置いたり（マウス ホバー）、ビュー、または、モデル ブラウザで 3D オブジェクトをクリックして選択すると、対象の 3D オブジェクトをハイライト表示します。 このハイライト表示は、モデルを選択したり操作したりするときに便利ですが、プレゼンテーションモードを好む場合には邪魔になってしまいます。 今回のバージョンでは、これらの動作を切り替えることができるようになりました。 当該メソッドは、スタイル変更と同様に Viewer3D クラスに追加されています。</p>
<pre> viewer.disableHighlight(boolean); // trueでホバー時のハイライトを無効化（既定値はfalse）<br /> viewer.disableSelection(boolean); // trueで選択時のハイライトを無効化（既定値はfalse）</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09de8ce1970d-pi" style="display: inline;"><img alt="Highlight" class="asset  asset-image at-xid-6a0167607c2431970b01bb09de8ce1970d img-responsive" src="/assets/image_320538.jpg" title="Highlight" /></a></p>
<hr />
<p>表示する 3D モデルやプレゼンテーションシーンに合わせて適宜お使いいただければと思います。</p>
<p>By Toshiaki Isezaki&#0160;</p>
