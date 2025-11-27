---
layout: "post"
title: "Texture Image of Asset"
date: "2015-12-11 01:12:23"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/12/texture-image-of-asset.html "
typepad_basename: "texture-image-of-asset"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>Texture Image of Asset </p>  <p><b>Question:</b></p>  <p>I have made simple part and assign to one face &quot;Norman - One-Third Running&quot; material from &quot;Autodesk Material Library&quot; . Then I have run VBA sample code &quot;Write out all document appearances API Sample&quot; of API help. The code result does not retrieve texture image. How it can be done ?</p>  <p>&#160;</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d182253a970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_8bd533.jpg" width="331" height="286" /></a></p>  <p><b>Solution:</b></p>  <p>The texture may be connected to the ColorAssetValue and FloatAssetValue, you can find the two asset values have HasConnectedTexture, but also a texture might also be in the TextureAssetValue. So now there are three asset value object can contain texture info.</p>  <ul>   <li>ColorAssetValue </li>    <li>FloatAssetValue</li>    <li>TextureAssetValue</li> </ul>  <p>In another word, the texture can be in different levels in the asset values, it depends on the type of the asset, like below asset type is Masonry, it has two textures:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f87434970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="clip_image002[5]" border="0" alt="clip_image002[5]" src="/assets/image_4a041c.jpg" width="280" height="351" /></a></p>  <p>Textures are at different locations in different types of asset, however there is not API that tells the type(Note, this is not Asset.AssetType), so we can only iterate all levels of the asset values to get the texture info.</p>  <p><b></b></p> <script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>  <pre class="csharp prettyprint" name="code">Sub AssetTextureSample()
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument
    
    Dim oAppearance As Asset
    For Each oAppearance In oDoc.AppearanceAssets
        Dim oColor As ColorAssetValue, oFloat As FloatAssetValue, oTypeTexture As TextureAssetValue
        
        Dim oValue As AssetValue
        If oAppearance.HasTexture Then
            For Each oValue In oAppearance
   
                 'TextureAssetValue
                If oValue.ValueType = kAssetValueTextureType Then
                    Set oTypeTexture = oValue
                    
                    Dim textureSubValue As AssetValue
                    For Each textureSubValue In oTypeTexture.value
                          Select Case textureSubValue.ValueType
                           Case kAssetValueTypeFilename
                                
                                Dim filenameValue As FilenameAssetValue
                                Set filenameValue = textureSubValue
                                Debug.Print &quot;kAssetValueTextureType &quot; &amp; filenameValue.value
                        End Select
                    Next
                     
                End If
                
                'ColorAssetValue
                If oValue.ValueType = kAssetValueTypeColor Then
                    Set oColor = oValue
                    If Not (oColor.ConnectedTexture Is Nothing) Then
                        Debug.Print &quot;---------------------------&quot;
                        Debug.Print oAppearance.DisplayName
                        
                        Dim oTexture As AssetTexture
                        Set oTexture = oColor.ConnectedTexture
                        
                        Dim oTextureValue As AssetValue
                        For Each oTextureValue In oTexture
                            If oTextureValue.ValueType = kAssetValueTypeFilename Then
                                Dim oFilename As FilenameAssetValue
                                Set oFilename = oTextureValue
                                
                                If oFilename.HasMultipleValues Then
                                    Dim sFiles() As String
                                    sFiles = oFilename.Values
                                    
                                    Dim i As Long
                                    For i = 0 To UBound(sFiles)
                                        Debug.Print &quot;kAssetValueTypeColor &quot; &amp; sFiles(i)
                                    Next
                                    
                                Else
                                    Debug.Print &quot;kAssetValueTypeColor &quot; &amp; oFilename.value
                                End If
                            End If
                        Next
                    End If
                End If
                ' FloatAssetValue
                If oValue.ValueType = kAssetValueTypeFloat Then
                    Set oFloat = oValue
                    If Not (oFloat.ConnectedTexture Is Nothing) Then
                        Debug.Print &quot;---------------------------&quot;
                        Debug.Print oAppearance.DisplayName
                        
                        Set oTexture = oFloat.ConnectedTexture

                        For Each oTextureValue In oTexture
                            If oTextureValue.ValueType = kAssetValueTypeFilename Then
                                Set oFilename = oTextureValue
                                
                                If oFilename.HasMultipleValues Then
                                    sFiles = oFilename.Values
                    
                                    For i = 0 To UBound(sFiles)
                                        Debug.Print &quot;kAssetValueTypeFloat &quot; &amp; sFiles(i)
                                    Next
                                    
                                Else
                                    Debug.Print &quot;kAssetValueTypeFloat &quot; &amp; oFilename.value
                                End If
                            End If
                        Next
                    End If
                End If
            Next
        End If
    Next
End Sub

 </pre>
