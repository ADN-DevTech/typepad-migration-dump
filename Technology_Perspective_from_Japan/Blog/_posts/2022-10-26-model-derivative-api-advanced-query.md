---
layout: "post"
title: "Model Derivative API：高度なクエリの作成"
date: "2022-10-26 00:08:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/10/model-derivative-api-advanced-query.html "
typepad_basename: "model-derivative-api-advanced-query"
typepad_status: "Publish"
---

<p>Forge 改め、Autodesk Platform Services の Model Derivative API では、<a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank">Model Derivative API：メタデータの活用</a> でご案内している方法を使用してメタデータを生成して JSON データとして参照することが出来るようになります。もちろん、Viewer 上での対話操作が必要な場面では、<a href="https://adndevblog.typepad.com/technology_perspective/2022/06/forge-viewer-properties-panel.html" rel="noopener" target="_blank">Forge Viewer：プロパティパネル</a> のような情報の取得や表示も可能です。</p>
<p>もし、ユーザによる Viewer 上の対話操作なしにプログラムでメタデータを収集する際には、前者の方法が考えらえます。このとき、対象モデルが大規模なものだと、プロパティ JSON の生成やダウンロード、また、ダウンロード後の JSON から特定のプロパティを得るためのパース処理に時間がかかってしまいます。そこで、以前開催した <a href="https://adndevblog.typepad.com/technology_perspective/2022/08/forge-data-days-tokyo-recordings.html" rel="noopener" target="_blank">Forge Data Days</a> をはじめとした過去のイベントでは、クエリの方法を改善していく計画をご案内していました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308e3a2d3200c-pi" style="display: inline;"><img alt="Data_days" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308e3a2d3200c image-full img-responsive" src="/assets/image_615678.jpg" title="Data_days" /></a></p>
<p>今回、この改善の一環として、<span style="background-color: #ffff00;">ベータ版</span>の新しいエンドポイントを導入しました。モデルのプロパティに対してより詳細なクエリを実行する <strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/metadata/urn-metadata-guid-properties-query-POST/" rel="noopener" target="_blank">POST {urn}/metadata/{modelGuid}/properties:query</a></strong>です。このエンドポイントのサンプルは&#0160;<a href="https://forge-properties-query.herokuapp.com/" rel="noopener" target="_blank">こちら</a>、ソースコードは <a href="https://github.com/libvarun/properties.query" rel="noopener" target="_blank">こちら</a> から、それぞれご確認いただけます。<a href="https://forge.autodesk.com/en/support/get-help"></a></p>
<p>新しくリリースされたエンドポイントは、高度なクエリーオプションをサポートし、ページ分割された結果を返します。また、このエンドポイントでは、次のリクエストボディを指定することができます。</p>
<ul>
<li><strong>pagination</strong>：レスポンスを複数のページに分割し、1 ページずつレスポンスを返す方法を指定します。limit 属性は、1 ページに含まれるオブジェクトの最大数を 20〜1000 個の範囲で指定します。offset 属性では、ページのプロパティ数を指定します。</li>
<li><strong>query</strong>：JSON 形式で定義されたカスタマイズしたクエリ DSL（Domain Specific Languages）です。条件を含む節を SQL 構文のような形式で定義します。いくつかの条件を指定することが出来ます。</li>
<li><strong>fields</strong>：オブジェクトのどのプロパティを返すかを指定します。この属性を指定しない場合、レスポンスはすべてのプロパティを返します。</li>
<li><strong>payload</strong>: レスポンスボディに含まれる数値の形式を指定します。既定値の text で文字列か、unit で ##&lt;VALUE_OF_PROPERTY&gt;&lt;UNIT_OF_VALUE&gt;&lt;PRECISION&gt;&lt;SYSTEM_UNIT&gt; を指定することが出来ます。</li>
</ul>
<p>今回は、query 属性をご紹介します。まず始めに、オブジェクト プロパティのサンプルを見てみましょう。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02ae7b07b0c3200b-pi" style="display: inline;"><img alt="Sample_json" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02ae7b07b0c3200b img-responsive" src="/assets/image_658199.jpg" title="Sample_json" /></a></p>
<p>プロパティ内部では、ダイレクト カテゴリとして「Dimensions」、リーフプロパティとして「Area」を呼んでいます。また、直接の子プロパティとして、「バージョン」、「コンポーネント名」があります。なお、プロパティの値型は、基本的に複数のキーと値のペアがネストされたJSON である。特定のプロパティを見つけるには、properties.{category}.{property} のように JSON-path スタイルのキーを構成します。例えば、「Area」プロパティを参照するには、properties.Dimensions.Area と指定することが出来ます。 <strong>query</strong> は、基本属性、直接の子プロパティ、リーフプロパティによる検索に対応しています。</p>
<p><strong>query</strong> では、条件にマッチする句を定義することが出来ます。この新しいエンドポイントは、$contains、 $gt、$between などのいくつかの条件(ルール)を受け入れます。 &#0160;注意: 現在のリリースでは、リクエストボディに 1 つの句（1 つの条件）のみをサポートしています。</p>
<ul>
<li><strong>$in</strong>： 特定のフィールドに対する正確な値のリストを含むリストクエリ句です。クエリ結果は、指定されたフィールドを含むオブジェクトを返し、フィールドの値は定義された値のいずれかと一致する必要があります。このリリースでは、基本属性である <strong>objectId</strong> （数値）と <strong>externalId</strong>（文字列） がサポートされています。例えば、以下のクエリは、値が123 または 789 であるオブジェクトを返します。</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-0">    { 
         &quot;<span class="hljs-attribute">$in</span>&quot;: <span class="hljs-value">[<span class="hljs-string">&quot;objectid&quot;</span>,<span class="hljs-number">123</span>, <span class="hljs-number">789</span> ] 
    </span>}</code></pre>
<ul>
<li><strong>$prefix</strong>：フィールドに特定のプレフィックス文字列を含むオブジェクトに一致するための句です(大文字小文字を区別しない)。基本的な <strong>name&#0160;</strong>属性のみがサポートされています。 例えば 名前が「System Panel」で始まるオブジェクトを検索する場合は、次のように定義します。</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-1">    {
       &quot;<span class="hljs-attribute">$prefix</span>&quot;: <span class="hljs-value">[<span class="hljs-string">&quot;name&quot;</span>, &#39;System Panel&#39;]
    </span>}
</code></pre>
<ul>
<li><strong>$contains</strong>：特定のプロパティに対して複数の条件を指定します。結果は、指定されたフィールドを含むべきオブジェクトを返し、フィールドの値は定義された 1 つ以上の含むはずです（大文字小文字を区別しない）。各項目は、ASCII の空白文字で区切る必要があります。例えば、次の例では、&quot;Aluminum Steel&quot; というテキストマッチング・ルールは、別々の条件である Aluminum と Steel に変換されます。つまり、前述のクエリでは、プロパティ JSON がリーフプロパティ（Material）を含み、その値がサブストリング「Aluminum」または「Steel」、あるいはその両方を含むオブジェクトを返すことを意味します。</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-2">     {
        &quot;<span class="hljs-attribute">$contains</span>&quot;:<span class="hljs-value">[<span class="hljs-string">&quot;properties.Constrains.Material&quot;</span>, <span class="hljs-string">&quot;Aluminum Steel&quot;</span>]
     </span>}
</code></pre>
<ul>
<li><strong>$eq</strong>：この条件では、基本的な name 属性、またはユニットプロパティを受け入れます。<strong>name</strong> の場合は、その名前の文字列と完全に一致することを条件とします。</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-3">       {  
         &quot;<span class="hljs-attribute">$eq</span>&quot;:<span class="hljs-value">[<span class="hljs-string">&quot;name &quot;</span>, “Door [<span class="hljs-number">34567</span>]” ]
       </span>}
</code></pre>
<div class="clipboard-container" data-clipboard-target="#snippet-3" data-on-clipboard="tooltip" data-placement="left" data-title="Copied!" data-trigger="manual" data-trigger-clipboard="data-trigger-clipboard" style="padding-left: 40px;">unit プロパティの場合、2 つ目の値は<strong>数値</strong>である必要があります。さらに、値は常に統一された標準単位で、長さの単位はメートル（m）になります。ソース・モデルの正確な値がセンチメートル（cm）であると仮定すると、その値は 500 になりますが、$eq 句を使ってproperties.Constrains.height=500 のオブジェクトを探すには、標準単位（値=5）を使って検索する必要があります。</div>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;<code class="language-json code-overflow-x hljs " id="snippet-4">      {  
          &quot;<span class="hljs-attribute">$eq</span>&quot;:<span class="hljs-value">[<span class="hljs-string">&quot;properties.Constrains.height&quot;</span>, <span class="hljs-number">5</span>]
      </span>}
</code></p>
<ul>
<li><strong>$between</strong>：この条件は、単位プロパティのみを受け入れます。この条件では、値が 2 つの値の間にあるオブジェクトを返します（境界値も含まれます）。$eq と同様、標準単位で検索します。例えば、次の条件では、高さが 34.567 メートル以上、 123.789 メートル以下のオブジェクトを見つけます。</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-5">    {
       &quot;<span class="hljs-attribute">$between</span>&quot;: <span class="hljs-value">[<span class="hljs-string">&quot;properties.Constrains.height &quot;</span>,<span class="hljs-number">34.567</span>, <span class="hljs-number">123.789</span>]
    </span>}
</code></pre>
<ul>
<li><strong>$le</strong>：<strong>unit</strong> プロパティのみを受け付けます。この条件では、値がある値より小さいオブジェクトを返します（境界の値も含まれます）。例えば、高さが 123.789 メートル以下のオブジェクトを検索します。&#0160;</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-6">      {
           &quot;<span class="hljs-attribute">$le</span>&quot;: <span class="hljs-value">[<span class="hljs-string">&quot;properties.Constrains.height&quot;</span>,<span class="hljs-number">123.789</span>]
      </span>}
</code></pre>
<ul>
<li>&#0160;<strong>$ge</strong>：<strong>unit</strong> プロパティのみを受け付けます。この条件では、値がある値より小さいオブジェクトを返します（境界値も含まれます）。同じように、標準単位で検索します。例えば、高さが 34.567 メートル以下のオブジェクトを見つけます。</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-7">       {
          &quot;<span class="hljs-attribute">$ge</span>&quot;: <span class="hljs-value">[<span class="hljs-string">&quot;properties.Constrains.Length&quot;</span>,<span class="hljs-number">34.567</span>]
       </span>}
</code></pre>
<div class="clipboard-container" data-clipboard-target="#snippet-7" data-on-clipboard="tooltip" data-placement="left" data-title="Copied!" data-trigger="manual" data-trigger-clipboard="data-trigger-clipboard">内部メカニズムでは、すべての型のフィールドは、$contains に文字列インデックスを持ちます。元の型が数値の場合は、$le、$ge、$between、$eq にもインデックス付されます。&#0160;</div>
<p>By Toshiaki Isezaki</p>
