---
layout: "post"
title: "Forge Viewer：State API でビューを更新"
date: "2021-12-22 00:03:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/12/view-change-by-state-api.html "
typepad_basename: "view-change-by-state-api"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2017/12/state-api-in-forge-viewer.html" rel="noopener" target="_blank"><strong>Forge Viewer の State API とビュー</strong></a> でもご紹介した State API を利用すると、カメラ位置やターゲット位置、オブジェクトの選択状態やオブジェクトの表示/非表示の状態、分解の状態や環境など、ビューの状態をまるごと記録して再生（呼び出し）することが出来ます。</p>
<pre>var _view;

function registerView (){
    _view = viewer.getState();
}

function restoreView (){  
    viewer.restoreState(_view);
}</pre>
<p>このとき、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#getstate-filter" rel="noopener" target="_blank">getState(filter)</a> はビューの各種情報を含んだ次のような JSON を返します。（<a href="https://adndevblog.typepad.com/technology_perspective/2021/12/access_viewer_instance.html" rel="noopener" target="_blank">デバッグ コンソールから </a><strong>NOP_VIEWER.getState()</strong> と入力して JSON を取得出来ます。）</p>
<pre>{
    &quot;seedURN&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZGFzLWphcGFuLXJjcGJmZ2dkZzNoemk0ODB5d3FudTN3ZWJ3bWdzeHZlL2NocmlzdG1hc3NfdHJlZS5mM2Q&quot;,
    &quot;objectSet&quot;: [
        {
            &quot;id&quot;: [],
            &quot;idType&quot;: &quot;lmv&quot;,
            &quot;isolated&quot;: [],
            &quot;hidden&quot;: [],
            &quot;explodeScale&quot;: 0
        }
    ],
<span style="color: #0000ff;">    &quot;viewport&quot;: {
        &quot;name&quot;: &quot;&quot;,
        &quot;eye&quot;: [
            -0.1516912418200763,
            -15.431550465791892,
            -0.1549962937528079
        ],
        &quot;target&quot;: [
            -0.1516912418200763,
            2.6183109451435183,
            -0.1549962937528079
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
            5.960464477539063e-8,
            1.1920928955078125e-7,
            0
        ],
        &quot;distanceToOrbit&quot;: 15.431550585001181,
        &quot;aspectRatio&quot;: 2.038216461462691,
        &quot;projection&quot;: &quot;perspective&quot;,
        &quot;isOrthographic&quot;: false,
        &quot;fieldOfView&quot;: 22.61986532341139
    },
</span>    &quot;renderOptions&quot;: {
        &quot;environment&quot;: &quot;Plaza&quot;,
        &quot;ambientOcclusion&quot;: {
            &quot;enabled&quot;: true,
            &quot;radius&quot;: 12,
            &quot;intensity&quot;: 1
        },
        &quot;toneMap&quot;: {
            &quot;method&quot;: 1,
            &quot;exposure&quot;: -14,
            &quot;lightMultiplier&quot;: -1e-20
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
    &quot;cutplanes&quot;: []
}</pre>
<p>この JSON を、そのまま <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#restorestate-viewerstate-filter-immediate" rel="noopener" target="_blank">restoreState(viewerState, filter, immediate)</a> に渡すことで、ビューの状態を再現することが出来るわけです。</p>
<p>ビューの変更を考えた場合、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setviewfromarray-params" rel="noopener" target="_blank">setViewFromArray(params)</a> や <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Navigation/#setview-position-target" rel="noopener" target="_blank">setView(position, target)</a> などのメソッドでも更新が可能ですが、環境やオブジェクトの選択状態、分解などを無視して、State API の JSON からビューのみを変更出来たら便利です。</p>
<p>そんな場面では、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#getstate-filter" rel="noopener" target="_blank">getState(filter)</a> が返す JSON から &quot;<span style="color: #0000ff;">viewport</span>&quot; セクションを抜き出して <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#restorestate-viewerstate-filter-immediate" rel="noopener" target="_blank">restoreState(viewerState, filter, immediate)</a> に渡すことで、ビューだけを更新することが出来ます。</p>
<pre>var view = {
    &quot;view&quot;: {
        &quot;viewport&quot;: {
            &quot;name&quot;: &quot;&quot;,
            &quot;eye&quot;: [
                -0.1516912418200763,
                -15.431550465791892,
                -0.1549962937528079
            ],
            &quot;target&quot;: [
                -0.1516912418200763,
                2.6183109451435183,
                -0.1549962937528079
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
                5.960464477539063e-8,
                1.1920928955078125e-7,
                0
            ],
            &quot;distanceToOrbit&quot;: 15.431550585001181,
            &quot;aspectRatio&quot;: 2.061889217422729,
            &quot;projection&quot;: &quot;perspective&quot;,
            &quot;isOrthographic&quot;: false,
            &quot;fieldOfView&quot;: 22.61986532341139
        } 
    }
}
_viewer.restoreState(view[&quot;view&quot;]);
</pre>
<p>この方法で JSON からビューのみを変更することが出来るようになります。次の例では、この方法でビューを更新するものです。設定で環境を変更しても、[View1] ～ [View3] ボタンは環境を変更せず、ビューだけを更新することがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c902b33200d-pi" style="display: inline;"><img alt="Switch_view" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c902b33200d image-full img-responsive" src="/assets/image_678730.jpg" title="Switch_view" /></a></p>
<p>By Toshiaki Isezaki</p>
