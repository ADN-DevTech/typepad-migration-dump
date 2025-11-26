---
layout: "post"
title: "Forge Viewer の State API とビュー"
date: "2017-12-18 00:07:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/12/state-api-in-forge-viewer.html "
typepad_basename: "state-api-in-forge-viewer"
typepad_status: "Publish"
---

<p>Forge Viewer を利用する際、表示中の場面（ビュー）を後でもう一度表示したい場合があります。いろいろ考えられますが、カメラ位置などを変更する必要がなければ、もっとも簡単な方法があります。Viewer3D.getState() と Viewer3D.restoreState() を利用して、カメラ位置やターゲット位置を含む ViewerState を記録・復元する方法です。</p>
<p>次の JSON は、Viewer3D.getState()&#0160; から返された ViewState オブジェクトを、 JSON.stringify() で JSON 化して見やすく整形した例です。</p>
<pre>{
  &quot;seedURN&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6N2lyeHdxMnViZ2x4YTI5d2lqdnNqc215YXJreDJpc2ItdHJhbmllbnQvRGllc2VsX0Nhcl9KTlIuZjNk&quot;,
  &quot;objectSet&quot;: [
    {
      &quot;id&quot;: [

      ],
      &quot;isolated&quot;: [

      ],
      &quot;hidden&quot;: [

      ],
      &quot;explodeScale&quot;: 0,
      &quot;idType&quot;: &quot;lmv&quot;
    }
  ],
  &quot;viewport&quot;: {
    &quot;name&quot;: &quot;&quot;,
    &quot;eye&quot;: [
      31589.3310546875,
      12721.4580078125,
      -3796.1991577148438
    ],
    &quot;target&quot;: [
      -0.0009765625,
      -1.395263671875,
      0.0001220703125
    ],
    &quot;up&quot;: [
      0,
      0,
      1
    ],
    &quot;worldUpVector&quot;: [
      0,
      0,
      1
    ],
    &quot;pivotPoint&quot;: [
      -0.0009765625,
      -1.395263671875,
      0.0001220703125
    ],
    &quot;distanceToOrbit&quot;: 34266.1352142333,
    &quot;aspectRatio&quot;: 1.588235294117647,
    &quot;projection&quot;: &quot;perspective&quot;,
    &quot;isOrthographic&quot;: false,
    &quot;fieldOfView&quot;: 22.61986532341139
  },
  &quot;renderOptions&quot;: {
    &quot;environment&quot;: &quot;(Custom: Model defined)&quot;,
    &quot;ambientOcclusion&quot;: {
      &quot;enabled&quot;: true,
      &quot;radius&quot;: 10,
      &quot;intensity&quot;: 0.4
    },
    &quot;toneMap&quot;: {
      &quot;method&quot;: 1,
      &quot;exposure&quot;: -9,
      &quot;lightMultiplier&quot;: -1.0e-20
    },
    &quot;appearance&quot;: {
      &quot;ghostHidden&quot;: true,
      &quot;ambientShadow&quot;: true,
      &quot;antiAliasing&quot;: true,
      &quot;progressiveDisplay&quot;: true,
      &quot;swapBlackAndWhite&quot;: false,
      &quot;displayLines&quot;: true,
      &quot;displayPoints&quot;: true
    }
  },
  &quot;cutplanes&quot;: [

  ]
}&#0160;</pre>
<p>この内容から、カメラ位置やターゲット位置以外にも、オブジェクトの選択状態や個別オブジェクト表示/非表示の状態、分解の状態や環境など、ビューの状態やレンダリングの状態まで記録出来ることがわかります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c93da776970b-pi" style="display: inline;"><img alt="Viewerstate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c93da776970b image-full img-responsive" src="/assets/image_626634.jpg" title="Viewerstate" /></a></p>
<p>例えば、次のようなコードがあったとすると、rsgisterView() 呼び出し後に手動でビューを操作しても、restoreView() を呼び出せば元のビューの状態を復元出来ることになります。</p>
<pre>var _view;

function registerView (){
    _view = viewer.getState();
}

function restoreView (){  
    viewer.restoreState(_view);
}</pre>
<p>getState() では、ViewerState に含まれるパラメータ名に true、または、false を設定したフィルタを JSON 形式で指定することで、記録する状態の種類を選別することが出来ます。例えば、次のコードでは分解係数とビューの状態のみを記録し、オブジェクトの選択状態や環境などレンダリング情報は除外されます。もちろん、ここで返された ViewerState（ここでは変数 _view）を restoreView() に指定すれば、分解係数とビュー（カメラやターゲット等）の状態のみが復元されることになります。</p>
<pre>var _view;
var filter = {
  seedURN: false,
  objectSet: [
      {
          explodeScale: true
      }
  ],
  viewport: true,
  renderOptions: false
};

function registerView (){
    _view = viewer.getState(filter);
}
</pre>
<p>フィルタは restoreView() にも適用することが出来ます。すべての情報を持つ ViewerState（ここでは変数 _view）とともに、次のようなフィルタを指定すると、記録した状態からビューの状態と環境を含むレンダリングの状態だけを復元します。</p>
<pre>var filter = {
  seedURN: false,
  objectSet: false,
  viewport: true,
  renderOptions: true
};

function registerView (){
    _view = viewer.getState();
}

function restoreView (){  
    viewer.restoreState(_view, filter);
}
</pre>
<p>restoreView() の第 3 パラメータに true を指定すると（既定値は true）、ビューの復元時にアニメーション効果が適用されます。false を指定するとアニメーション効果を除外してビューが即座に復元されます。</p>
<p>ViewerState には、基本的に Extension の状態やポスト処理（事後処理）で施された効果は含まれない点に注意してください。<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/12/mesh-representation-on-viewer.html" rel="noopener noreferrer" target="_blank">Viewer でのメッシュ状表示 </a></strong>や&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/12/release-forge-viewer-v3_3.html" rel="noopener noreferrer" target="_blank">Forge Viewer バージョン 3.3 リリース</a></strong>で ご案内したメッシュ表示や&#0160;非フォトグラフィックス レンダリング スタイルは記録されません。</p>
<p>By Toshiaki Isezaki</p>
