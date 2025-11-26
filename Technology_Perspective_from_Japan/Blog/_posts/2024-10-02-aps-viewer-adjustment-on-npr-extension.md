---
layout: "post"
title: "APS Viewer：NPR エクステンションの調整"
date: "2024-10-02 00:59:57"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/10/aps-viewer-adjustment-on-npr-extension.html "
typepad_basename: "aps-viewer-adjustment-on-npr-extension"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bd97ec200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d4e77a200b-pi" style="display: inline;"><img alt="Npr" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d4e77a200b img-responsive" src="/assets/image_841602.jpg" title="Npr" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bd97ec200c-pi" style="display: inline;"></a>以前、<a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-extension.html" rel="noopener" target="_blank">Forge Viewer：Viewer を拡張する Extension</a> で触れたことがある &#0160;Autodesk.NPR エクステンションについて、その後、設定パネルが拡張されていますのでご紹介しておきたいと思います。<strong>NPR</strong> とは <strong>N</strong>on-<strong>P</strong>hotorealistic <strong>R</strong>endering の略で、日本語にすると <strong>非フォトグラフィックス レンダリング&#0160;</strong>になります。</p>
<p>API で&#0160; Autodesk.NPR エクステンションをロードして利用すると、元々、APS Viewer に表示している 3D モデルに、‘<strong>edging</strong>’、‘<strong>cel</strong>’、‘<strong>grapite</strong>’、‘<strong>pencil</strong>’ の 4 つの表示スタイルを適用して表示することが出来ました。</p>
<p>この際、設定パネルの [環境設定] タブ下部に [非フォトグラフィックス レンダリング] ボタンが表示されるようになります。</p>
<p>同ボタンから [非フォトグラフィックス レンダリング] パネルを表示させると、各表示スタイルの切り替えの他に、表示スタイルを構成するパラメーターを個別に変更出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c113ac200b-pi" style="display: inline;"><img alt="Settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c113ac200b image-full img-responsive" src="/assets/image_142083.jpg" title="Settings" /></a></p>
<p><a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Extensions/NPR/#setparameter-param-value" rel="noopener" target="_blank">[非フォトグラフィックス レンダリング] パネルのパラメーター</a>は、API からのコントロールも可能です。必要なパラメーター UI を用意すれば、効果的に意匠上の表示効果を得ることが出来ます。</p>
<p>例えば、brightness パラメーターで明るさ、constrast パラメーターでコントラスト、outlineNoise パラメーターでアウトライン ノイズを変更すると、従来、プリセットされたスタイルの内容を変更して、表現する内容の幅が広がります。</p>
<pre>...
$(document).on(&quot;change&quot;, &quot;[id^=&#39;brightness&#39;]&quot;, () =&gt; {
    _viewer.getExtensionAsync(&#39;Autodesk.NPR&#39;).then(ext =&gt; {
        ext.setParameter(&#39;brightness&#39;, $(&quot;#brightness&quot;).val());
    });
});

$(document).on(&quot;change&quot;, &quot;[id^=&#39;contrast&#39;]&quot;, () =&gt; {
    _viewer.getExtensionAsync(&#39;Autodesk.NPR&#39;).then(ext =&gt; {
        ext.setParameter(&#39;contrast&#39;, $(&quot;#contrast&quot;).val());
    });
});

$(document).on(&quot;change&quot;, &quot;[id^=&#39;noise&#39;]&quot;, () =&gt; {
    _viewer.getExtensionAsync(&#39;Autodesk.NPR&#39;).then(ext =&gt; {
        ext.setParameter(&#39;outlineNoise&#39;, $(&quot;#noise&quot;).prop(&quot;checked&quot;));
    });
});
...
</pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bdd5c2200c-pi" style="display: inline;"><img alt="Npr_parameters" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bdd5c2200c image-full img-responsive" src="/assets/image_150933.jpg" title="Npr_parameters" /></a></p>
<p>By Toshiaki Isezaki</p>
