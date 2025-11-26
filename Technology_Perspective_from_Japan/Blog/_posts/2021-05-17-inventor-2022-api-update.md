---
layout: "post"
title: "Inventor 2022でのAPI Update "
date: "2021-05-17 01:10:11"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/inventor-2022-api-update.html "
typepad_basename: "inventor-2022-api-update"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802344df200d-pi" style="display: inline;"><img alt="Autodesk-inventor-badge-1024" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802344df200d image-full img-responsive" src="/assets/image_852887.jpg" title="Autodesk-inventor-badge-1024" /></a></p>
<p>今回は、Inventor 2022でのAPIの変更点について、主な点をご案内をしたいと思います。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">Inventor 2022 SDKと日本語版Help</span></span></strong></p>
<p>&#0160;</p>
<p>まず、Inventor 2022で、アドインモジュール等の開発に必要となるSDKについては、以下の記事にてご案内をしておりますので、こちらをご一読ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/inventor-2022-whats-new-part1.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_814945.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor 2022 新機能～ その1</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventorの新バージョンとなる、Inventor 2022がリリースされました。 まずは、概要をご紹介したいと思います。 サポートされるプラットフォームは、2021に引き続き Windows 10 の 64 ビット版（32 ビット版の提供はなし）となります。また、対応する.Net Frameworkについては、.NET Framework Version 4.8以降となります。 詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、日本語版のAPI Helpについては、以下の記事にてご案内をしておりますので、ダウロードして取得をしていただければと思います。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/japanese-inventor-2022-programming-api-help.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_269401.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">日本語版 Inventor 2022 API プログラミング用ヘルプ</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventorの新バージョンとなる、Inventor 2022がリリースされました。 新バージョンでの機能の概要については後程別の記事でご紹介をしたいと思いますが、取り急ぎInventor 2022の API の日本語プログラミング用ヘルプ （admapi_26_0.chm）を ZIP 圧縮したファイルをポストしていますので、次のリンクからダウンロードしてください。 Inventor 2022 API の日本語プログラミング 用ヘルプ をダウンロード 日本語版 Inventor 2022 に...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>API Helpの「Inventor APIの新機能」では、Inventor 2022でのAPIの更新内容について記載されておりますので、是非一度ご確認ください。</p>
<p>&#0160;</p>
<p>ご参照いただくとわかりますが更新内容は非常に多岐にわたり、全ての内容をご説明することは難しいため、この記事では、Inventor 2022の主なトピックスである「モデル状態」について解説をしたいと思います。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">モデル状態への対応</span></span></strong></p>
<p>それでは、Inventor 2022で最も大きな変更となる、モデル状態についてAPIでの対応を解説したいと思います。</p>
<p>&#0160;</p>
<p>新しいModelState APIは、アセンブリとパートドキュメントでモデル状態オブジェクトの作成/編集/削除およびモデル状態に関連する機能をサポートしています。</p>
<p>&#0160;</p>
<p>例えば、以下のようなコードで、定義済みのモデル状態(モデル状態名：&quot;モデル状態1&quot;)にアクセスし、現在アクティブなモデル状態を変更することが出来ます。</p>
<pre><code>
Public Sub ChangeCurrentModelState()
    Dim oPartDoc As PartDocument
    Dim oPartCompDef As PartComponentDefinition
    Dim oModelStates As ModelStates
    
    Set oPartDoc = ThisApplication.ActiveDocument
    Set oPartCompDef = oPartDoc.ComponentDefinition
    
    Set oModelStates = oPartCompDef.ModelStates
    
    Dim oModelState As ModelState
    Set oModelState = oModelStates.Item(&quot;モデル状態1&quot;)
    oModelState.Activate

End Sub
</code></pre>
<p>&#0160;</p>
<p>さて、以前のこちらの記事で、モデル状態機能を用いて、パラメーター値、フィーチャ、コンポーネント、部品表、iProperty、パラメーター、部品表、材料/色、といった内容をモデル状態毎に設定をすることが可能であるとご案内をいたしました。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/inventor-2022-whats-new-part2.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_40062.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor 2022 新機能～ その2</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor 2022では、ユーザー様からの要望が多く寄せられていた『モデル状態』機能が実装されています。 『モデル状態』により、既存機能のワークフローをよりシンプルな形に置き換え、かつ新しいワークフローを実現できるようになります。 今回の記事では、Inventor 2022の新機能『モデル状態（Model State）』について、ご紹介していきたいと思います。</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>それでは、APIでこれらの設定にアクセスした場合どのようになるのか？について、iPropertyを例にしてどのようになるかを見ていきたいと思います。</p>
<p>&#0160;</p>
<p>以下の様に、3つのモデル状態（マスター、モデル状態1、モデル状態2）を持ち、それぞれのモデル状態でiPropertyの「部品番号」と「ストック番号」に異なる値を設定したパートファイルを用意しました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded22c72200c-pi" style="display: inline;"><img alt="1" class="asset  asset-image at-xid-6a0167607c2431970b026bded22c72200c img-responsive" src="/assets/image_897890.jpg" title="1" /></a></p>
<p>&#0160;</p>
<p>このパートファイルで、”マスター”モデル状態をアクティブ化して、以下のようなコードで部品番号を取得した場合、スクリーンショットのように”マスター”モデル状態での設定値が取得されます。</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802a1b73200d-pi" style="display: inline;"><img alt="2" class="asset  asset-image at-xid-6a0167607c2431970b0278802a1b73200d img-responsive" src="/assets/image_165629.jpg" title="2" /></a></p>
<pre><code>
Public Sub ShowPartNumberProperty()
    
    Dim oPartDoc As PartDocument
    Dim oPartCompDef As PartComponentDefinition
    
    Set oPartDoc = ThisApplication.ActiveDocument
    
    &#39; Get the design tracking property set.
    Dim invDesignInfo As PropertySet
    Set invDesignInfo = oPartDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;)
    
    &#39; Get the part number property.
    Dim invPartNumberProperty As Property
    Set invPartNumberProperty = invDesignInfo.Item(&quot;Part Number&quot;)
    
    MsgBox &quot;Part Number: &quot; &amp; invPartNumberProperty.Value


End Sub
</code></pre>
<p>&#0160;</p>
<p>次に、アクティブなモデル状態を”モデル状態1”に変更してから、部品番号にアクセスすると、以下の様に”モデル状態1”での値が取得されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802a1b99200d-pi" style="display: inline;"><img alt="3" class="asset  asset-image at-xid-6a0167607c2431970b0278802a1b99200d img-responsive" src="/assets/image_108397.jpg" title="3" /></a></p>
<p>&#0160;</p>
<p>このように、2021以前からのAPIを用いて、モデル状態に依存して値が変わる内容にアクセスした場合は、”現在アクティブ”なモデル状態の値が取得されることが分かるかと思います。</p>
<p>&#0160;</p>
<p>それでは、”現在アクティブ”なモデル状態を変更せずに、各モデル状態での内容にアクセスしたい場合はどのようにすればよいのでしょうか？</p>
<p>&#0160;</p>
<p>Inventorの内部的には、各モデル状態での内容は、次の図のようなModelStateTableという表形式で、それぞれのモデル状態での設定値を保持しています。</p>
<p>表の列に対応するModelStateTableColumnは、モデル状態毎に内容が異なる設定（例えば、iPropertyの部品番号）を表し、行に対応するModelStateTableRowは、モデル状態を表します。そして表の各セルに対応するModelStateTableCellは、その行（モデル状態）の、列（設定項目）に対応する、設定内容に対応します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1027e77200b-pi" style="display: inline;"><img alt="4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1027e77200b image-full img-responsive" src="/assets/image_643692.jpg" title="4" /></a></p>
<p>例えば、以下の様なコードにより、現在アクティブなモデル状態にかかわらず、全モデル状態の部品番号を取得することができます。</p>
<pre><code>

Public Sub ShowModelStatePartNumberProperty()
    Dim oPartDoc As PartDocument
    Dim oPartCompDef As PartComponentDefinition
    Dim oModelStates As ModelStates
    
    Set oPartDoc = ThisApplication.ActiveDocument
    Set oPartCompDef = oPartDoc.ComponentDefinition
    
    Set oModelStates = oPartCompDef.ModelStates
    
    
    Dim oModelStateTable As ModelStateTable
    Set oModelStateTable = oModelStates.ModelStateTable
    
    Dim oModelStataTableRow As ModelStateTableRows
    
    Dim oModelStateTableColumn As ModelStateTableColumn
    For Each oModelStateTableColumn In oModelStateTable.TableColumns
        
        If oModelStateTableColumn.DisplayHeading = &quot;部品番号&quot; Then
            Dim i As Integer
            For i = 1 To oModelStateTableColumn.Count
                Debug.Print &quot;Index=&quot; &amp; i &amp; &quot; Value= &quot; &amp; oModelStateTableColumn.Item(i).Value
                
            Next
        End If
        
    Next
    
End Sub
</code></pre>
<p>&#0160;</p>
<p>なお、ModelStateTableには、各モデル状態で”個別に設定をした”内容のみが格納されております。</p>
<p>このため、全モデル状態で、共通の設定内容を持つ場合、その内容はModelStateTableには表れないこととなります。</p>
<p>&#0160;</p>
<p>また、編集操作で各モデル状態で差異がある内容が追加された場合は、その設定がModelStateTableのModelStateTableColumnとして追加されます。</p>
<p>また、新しいモデル状態自体が追加された場合はModelStateTableRowに行が追加されることとなります。</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>さて、ここまでモデル状態で差異のある内容の取得について確認をしてきましたが、「2021以前の、既存APIを用いた編集操作についての注意点」について触れておきたいと思います。</p>
<p>&#0160;</p>
<p>例えばですが、以下のようなコードを用いて、iPropertyの部品番号の値を更新した場合、部品番号の変更は、”現在アクティブなモデル状態”になるでしょうか？それとも”すべてのモデル状態”になるでしょうか？</p>
<pre><code>
Public Sub UpdatePartNumberProperty()
    
    Dim oPartDoc As PartDocument
    Dim oPartCompDef As PartComponentDefinition
    Dim oModelStates As ModelStates
    
    Set oPartDoc = ThisApplication.ActiveDocument
    
    &#39; Get the design tracking property set.
    Dim invDesignInfo As PropertySet
    Set invDesignInfo = oPartDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;)
    
    &#39; Get the part number property.
    Dim invPartNumberProperty As Property
    Set invPartNumberProperty = invDesignInfo.Item(&quot;Part Number&quot;)
    
    invPartNumberProperty.Value = &quot; Part1 Updated from API&quot;
End Sub
<br /></code></pre>
<p>&#0160;</p>
<p>多くの方は、”現在アクティブなモデル状態”に反映されることを期待されているかと思いますが、実はこの編集の結果がどのモデル状態に反映されるかは、ModelStates.MemberEditScopeプロパティの設定値に依存します。</p>
<p>&#0160;</p>
<p>ModelStates.MemberEditScopeプロパティの設定値がkEditActiveMemberの場合、編集の結果は”現在アクティブなモデル状態”にのみ反映されます。</p>
<p>一方で、ModelStates.MemberEditScopeプロパティの設定値がkEditAllMembersの場合、編集の操作はすべてのモデル状態に対して反映されます。</p>
<p>&#0160;</p>
<p>これはちょうど、InventorのGUI上で&#0160;モデル状態の編集範囲を設定することに相当します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded23159200c-pi" style="display: inline;"><img alt="5" class="asset  asset-image at-xid-6a0167607c2431970b026bded23159200c img-responsive" src="/assets/image_379950.jpg" title="5" /></a></p>
<p>このため、モデル状態毎に違う値を持つ内容を、kEditAllMembersの状態で変更すると、その変更は<strong><span style="text-decoration: underline;">すべてのモデル状態に対して行われ、すべてのモデル状態で同じ状態を持つようになります</span></strong>。</p>
<p>&#0160;</p>
<p>実際、各モデル状態で差異が無い状態となるため、ModelStateTableから、その内容に対応するModelStateTableColumnが削除されます。</p>
<p>&#0160;</p>
<p>このため、各モデル状態で異なる内容を設定していたにもかかわらず、APIでの編集の結果、意図せずに全モデル状態を同じ値にしてしまうといったことが無いようにご留意ください。</p>
<p>&#0160;</p>
<p>なお、今回ご紹介した、モデル状態、VBSコードを含むパートファイルは、こちらのリンクから取得 <span class="asset  asset-generic at-xid-6a0167607c2431970b026bded231ab200c img-responsive"><a href="https://adndevblog.typepad.com/files/part1.ipt">Part1をダウンロード</a></span>いただけますので、必要に応じてご利用ください。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">レジストリ登録アドインのリタイア</span></span></strong></p>
<p>さて、ここまでモデル状態について解説をしてきましたがもう一つ、Inventor 2022での注意が必要な変更についてご案内をしたいと思います。</p>
<p>&#0160;</p>
<p>Inventor 2010で、レジストリ登録が不要なアドインが導入されて以降、ご案内をしておりましたがInventor のアドインを作成には、レジストリ登録が不要なRegfree形式で作成することを推奨しておりました。</p>
<p>今回、Inventor 2022からは、レジストリ登録を行う形式で作成されたアドインは、<strong><span style="text-decoration: underline;">Inventorの Add-Insマネージャ ダイアログに表示されなく</span></strong>なります。</p>
<p>&#0160;</p>
<p>レジストリ登録を行うアドインから、レジストリ登録が不要なアドインへの変換方法については以下のURLにてご案内をしております。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2022/ENU/?guid=GUID-CFFA5CC6-38E6-4ACD-A2BC-8B8732727996" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_405411.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Converting an Existing Add-In to be Registry-Free</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;"><br />The following describes the process of converting a standard add-in into a registry-free add-in. Since the process is different for the different programming languages, the process is described for Visual Basic, C#, and VC++.</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>Inventor 2022で レジストリ登録を行う形式で作成されたアドインを引き続き使用する場合は、レジストリ キーの値を作成して有効にする必要があります。</p>
<p>キーの場所: [HKEY_CURRENT_USER\SOFTWARE\Autodesk\Inventor\RegistryVersion26.0\System\Preferences]<br />キー値の名前: LoadRegisterBasedAddins<br />キー値データ: dword:00000001</p>
<p>&#0160;</p>
<p>なお、レジストリ登録が必要なアドインは、将来のリリースでサポートされなくなる可能性がありますので、レジストリ登録が不要なRegfree形式への変換を行うことを強く推奨します。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
