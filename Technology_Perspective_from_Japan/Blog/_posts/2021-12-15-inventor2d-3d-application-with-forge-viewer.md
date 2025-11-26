---
layout: "post"
title: "Forge ViewerによるInventor 2D図面 / 3Dモデル連携表示"
date: "2021-12-15 00:02:24"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/inventor2d-3d-application-with-forge-viewer.html "
typepad_basename: "inventor2d-3d-application-with-forge-viewer"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1388521200b-pi"><img alt="Title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1388521200b image-full img-responsive" src="/assets/image_165654.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Title" /></a></p>
<p>以前のこちらの記事で、Forge Viewewを使った2D図面と3Dモデルの連携方法についてご紹介をいたしました。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-forge-viewer-event-and-2d-3d.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 500px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_95501.jpg" style="width: 100%; height: auto; max-height: 500px; min-width: 0; border: 0 none; margin: 0;" width="500" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Forge Viewer：イベント処理と 2D / 3D 連携</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Forge Viewer：2D シート/レイアウト のブログ記事でご案内したとおり、2D シートと 3D モデルが 1 つのシードファイルに存在する場合、同ファイルを Model Derivative API で変換して 2D 情報を Forge Viewer 上で表示することが出来ます。この際、変換された viewables には 3D 情報も同時ぬ含まれているので、表示するカンバス領域（&amp;lt;div&amp;gt;～&amp;lt;/div&amp;gt;）を個別に定義することで...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>この記事では、2D シートと 3D モデルが 1 つのシードファイルに存在する場合に、Forge Viewerでの一意の識別子であるdbidが2D、3D双方で同一であることを利用して、相互に連携をする方法について、Revitのデータを例にしてご紹介をしています。</p>
<p>&#0160;</p>
<p>それでは、Inventorの2D図面と3Dモデルについても同様の手法で2D図面と3Dモデルを連携させられるでしょうか？</p>
<p>&#0160;</p>
<p>残念ながらInventorの2D図面（*.idw, <em>.dwg)と、3Dモデル(*</em>.iam, *.ipt)をForgeのModel Derivative API で変換した場合、変換されたSVFファイル中のdbidはそれぞれ別のidとなってしまいます。このため、ForgeのViewerで2D図面と3Dモデルを連携をする情報としてdbidを利用することが出来ません。</p>
<p>&#0160;</p>
<p>また、Inventorの2D図面（*.idw, <em>.dwg)をSVFに変換した場合、図面上の図形の情報はSVFに渡りますが、図面を構成する元の3Dモデルに対応する情報はSVFに渡されず、パートやアセンブリに対応する図形がどれなのかが分からなくなってしまうため、dbid以外のパート、アセンブリ名等の情報を元に、3Dモデルと連携を行うことも</em><em>できない状況です。</em></p>
<p>&#0160;</p>
<p>そこで、今回の記事では、Forge ViewerでInventorの2D図面 / 3Dモデルの連携を行う方法について解説をしたいと思います。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">Inventorの2D図面 / 3Dモデルの連携アプリケーション</span></span></strong></p>
<p>まず初めに、今回紹介する手法でInventorの2D図面/3Dモデル連携アプリケーションを紹介したいと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13887f9200b-pi" style="display: inline;"><img alt="Inventor-2D-3Dwhite" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13887f9200b image-full img-responsive" src="/assets/image_363500.jpg" title="Inventor-2D-3Dwhite" /></a></p>
<p>&#0160;</p>
<p>左側のForge Viewerで表示されている2D図面で選択をすると、右側のViewer表示している3Dモデルの中で、対応するコンポーネントが選択・ズーム表示されていることが分かるかと思います。</p>
<p>&#0160;</p>
<p>また、右側の3Dモデルでコンポーネントを選択すると、選択したコンポーネントを含む図形が左側の2D図面で選択されていることが分かるかと思います。</p>
<p>&#0160;</p>
<p>なお、このアプリケーションの連携表示には、一点制限事項があります。</p>
<p>&#0160;</p>
<p>左側の2D図面で選択可能となるコンポーネントは、2D図面でViewとして配置したアセンブリの直下のコンポーネントとなるということです。つまり、トップアセンブリ配下に、アセンブリを配置していた場合、配置したアセンブリを構成するアセンブリやパートの単位では選択することが出来ません。</p>
<p>&#0160;</p>
<p>それでは、どのようにInventorの2D図面と3Dモデルを連携させるかについて解説をしたいと思います。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">Inventor 2D図面の加工</span></span></strong></p>
<p>前述したように、Inventor 2D図面をそのまま、ForgeのModel Derivative API で変換しても連携表示をするための情報が付加されません。</p>
<p>&#0160;</p>
<p>このため、Inventor 2D図面をAutoCAD図面に変換、Inventor図面でのコンポーネントをAutoCADでブロックとする加工を行うことで、連携表示に必要な情報がModel Derivative APIで変換後のSVFに渡るようにします。詳細な方法については、こちらのAutodesk Knowledge Networkの記事で紹介していますので、ご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://knowledge.autodesk.com/ja/community/article/383726" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_98872.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventorの2D図面に配置したView内の各コンポーネントを、AutoCADのブロックにする方法 | Inventor | Autodesk Knowledge Network</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventorの2D図面に配置したView内の各コンポーネントを、AutoCAD図面のブロックにする方法はありますか？<br />Solution：<br />次のStepでInventorとAutoCADのAPIを用いてInventorの2D図面を加工することで、2D図面に配置したView内の各コンポーネントをAutoCADのブロック化することが可能です...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">Forge Viewerでの連携</span></span></strong></p>
<p>さて、HTML ページに 2D と 3D を表示する 2 つの &lt;div&gt; セクションが Forge Viewer カンバスとして用意されていて、Inventorの2D図面を加工したAutoCAD図面と、Inventorのアセンブリを表示すると仮定してください。</p>
<p>&#0160;</p>
<p>この時、前者カンバスのタグ id が viewer2ddiv、後者カンバスのタグ id が viewer3ddiv になっていて、それぞれの Viewer3D インスタンスを格納する JavaScript 変数が、viewer2dと viewer3dとします。</p>
<p>&#0160;</p>
<p>この状態で、viewer2d カンバスのドキュメント ロード成功時に、次のように、ジオメトリ選択 イベント SELECTION_CHANGED_EVENT をフックします。</p>
<pre><code>            viewer2d.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, function (event) {
                if (onSync) {
                    return;
                }

                if (event.dbIdArray.length === 1) {
                    viewer2d.getProperties(event.dbIdArray[0], function (data) {
                        console.log(&#39;selected data name is &#39; + data.name);
                        var modelName = data.name.replace(&#39;Occurence_&#39;, &#39;&#39;).replace(&#39; [&#39; + data.externalId + &#39;]&#39;, &#39;&#39;).replace(/#(\d+$)/, &#39;:$1&#39;);
                        
                        console.log(&#39;Search for [&#39; + modelName + &#39;] in 3d view.&#39;);

                        viewer3d.search(modelName, (dbIds) =&gt; {
                            // success
                            viewer3d.model.getBulkProperties(
                                dbIds,
                                [&#39;Name&#39;],
                                (elements) =&gt; {
                                    let dbIdsToSelect = [];
                                    for (var i = 0; i &lt; elements.length; i++) {
                                        if (elements[i].properties[0].displayValue === modelName)
                                            dbIdsToSelect.push(elements[i].dbId);
                                    }

                                    onSync = true;
                                    viewer3d.select(dbIdsToSelect);
                                    viewer3d.fitToView(dbIdsToSelect);
                                    onSync = false;
                                },
                                (e) =&gt; {
                                    // error, handle here...
                                },
                                [&#39;Name&#39;]);
                        });
                    })
                }
            });

</code></pre>
<p>SELECTION_CHANGED_EVENT イベントハンドラ関数のパラメータには、選択したジオメトリの dbId が格納されているので、dbidから選択されたモデルの名前を取得し、その名前を用いて、3dモデルを表示するカンバスで、その名前を持つモデルをsearch()及びgetBulkProperties()を用いて検索して、取得したジオメトリのdbidをselect() - 選択表示とfitToView()－ 拡大 する をすることで、連携処理を実現することが出来るようになります。<code></code></p>
<p>&#0160;</p>
<p>同じく、viewer3d カンバスのドキュメント ロード成功時に、次のように、ジオメトリ選択 イベント SELECTION_CHANGED_EVENT をフックします。</p>
<pre><code>    viewer3d.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, function (event) {
                if (onSync)
                {
                    return;
                }

                if (event.dbIdArray.length === 1) {
                    viewer3d.getProperties(event.dbIdArray[0], function (data) {
                        console.log(data.name);
                        if (null == data.properties.find(prop =&gt; prop.attributeName == &#39;Component Name&#39;)) {
                            var instanceTree = viewer3d.model.getData().instanceTree;
                            var parentId = instanceTree.getNodeParentId(event.dbIdArray[0])
                            viewer3d.select([parentId]);

                            console.log(&quot;Selected ParentId is &quot; + parentId + &quot;.&quot;);
                            return;
                        }
                    })
                }

                var instanceTree = viewer3d.model.getData().instanceTree;
                var selecteddbid = event.dbIdArray[0];
                var dbids = [];
                dbids.push(selecteddbid);

                while (selecteddbid)
                {

                    var parentId = instanceTree.getNodeParentId(selecteddbid);
                    dbids.push(parentId);

                    viewer3d.getProperties(parentId, function (data) {
                        console.log(data.name)
                    });

                    if (parentId == viewer3d.model.getRootId()) {

                        dbids.pop();
                        dbids.pop();
                        selecteddbid = dbids.pop();
                        break;
                    }
                    else
                    {
                        selecteddbid = parentId;
                    }
                }
                
                if (event.dbIdArray.length === 1) {
                    viewer3d.getProperties(selecteddbid, function (data) {
                        console.log(&#39;target data name is &#39; + data.name);
                        var modelName = &#39;Occurence_&#39; + data.name.replace(&#39; [&#39; + data.externalId + &#39;]&#39;, &#39;&#39;).replace(/:(\d+$)/, &#39;#$1&#39;);
                        console.log(&#39;Search for [&#39; + modelName + &#39;] in 2d view.&#39;);
                        viewer2d.search(modelName, (dbIds) =&gt; {
                            // success
                            viewer2d.model.getBulkProperties(
                                dbIds,
                                [&#39;Name&#39;],
                                (elements) =&gt; {
                                    let dbIdsToSelect = [];
                                    for (var i = 0; i &lt; elements.length; i++) {
                                        if (elements[i].properties[0].displayValue.startsWith(modelName))
                                            dbIdsToSelect.push(elements[i].dbId);
                                    }
                                    onSync = true;
                                    viewer2d.select(dbIdsToSelect);
                                    //viewer2d.fitToView(dbIdsToSelect);
                                    onSync = false;
                                    
                                },
                                (e) =&gt; {
                                    // error, handle here...
                                },
                                [&#39;Name&#39;]);
                        });
                    })
                }
            });
</code></pre>
<p>&#0160;</p>
<p>SELECTION_CHANGED_EVENT イベントハンドラ関数のパラメータには、選択したジオメトリの dbId が格納されているので、まず、選択したジオメトリの親で属性に’Component Name’を持つモデルを選択するようにします。これにより、3dモデル上では、Forge Viewerでプロパティを持つ(≒Inventorでのパートやアセンブリ)モデルが選択されるようになります。</p>
<p>&#0160;</p>
<p>次に、選択したモデルを親にさかのぼっていき、最上位のモデルの直下のモデルのdbidを取得します。これは、2D図面側では、Inventorでの最上位のアセンブリ直下のコンポーネントのみが、選択可能なモデルとなっているためです。</p>
<p>&#0160;</p>
<p>そして最後に、特定した最上位のモデルの直下のモデルのdbidから、モデルの名前を取得しsearch()及びgetBulkProperties()を用いて検索して、取得したジオメトリのdbidをselect() - 選択表示 をすることで、連携処理を実現することが出来るようになります。</p>
<p>なお、モデル名で検索をする際には、2D側のモデルでは、末尾の”:” + 数値が”#” + 数値に置き換える加工をしている事に合わせて、検索に使用する文字列を加工しています。</p>
<p>&#0160;</p>
<p>また、2D図面側ではモデル選択を容易にするためにBox選択ExtensionをロードしてBox選択をできるようにします。</p>
<pre><code>            viewer2d.addEventListener(
                Autodesk.Viewing.EXTENSION_LOADED_EVENT,
                (event) =&gt; {
                    if (event.extensionId != &#39;Autodesk.BoxSelection&#39;) return;

                    var viewer = event.target;
                    var boxSelExt = viewer.getExtension(&#39;Autodesk.BoxSelection&#39;);
                    boxSelExt.addToolbarButton(true); //!&lt;&lt;&lt; Show the toolbar button for this extension
                });
</code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdf05c626200c-pi" style="display: inline;"><img alt="BoxSelectionExtension" class="asset  asset-image at-xid-6a0167607c2431970b026bdf05c626200c img-responsive" src="/assets/image_841225.jpg" title="BoxSelectionExtension" /></a></p>
<p>&#0160;</p>
<p>以上で、Inventorの2D図面と3DモデルをForge Viewerで連携表示することが出来るようになります。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">まとめ</span></span></strong></p>
<p>いかがでしたでしょうか？</p>
<p>&#0160;</p>
<p>Inventorの2D図面を加工し、AutoCADのブロックにする処理を行うことで、2D図面側でトップアセンブリ直下のオカレンスしか選択できないという制約事項はあるものの、Forge Viewerで2D図面と3Dモデルの連携表示ができることが確認できたかと思います。</p>
<p>&#0160;</p>
<p>今回の記事ではInventorの2D図面を手動で加工しましたが、Design Automationを用いて自動化することで、手作業での加工なしでForge Viewerで連携表示をさせることも可能かと思いますので、要件に合わせてご活用いただければと思います。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
