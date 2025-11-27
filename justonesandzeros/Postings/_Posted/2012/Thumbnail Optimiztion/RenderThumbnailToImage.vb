Public Shared Function RenderThumbnailToImage(propInst As PropInst, width As Integer, height As Integer) As Image
	' convert the property value to a byte array
	Dim thumbnailRaw As Byte() = TryCast(propInst.Val, Byte())

	If thumbnailRaw Is Nothing OrElse 0 = thumbnailRaw.Length Then
		Return Nothing
	End If

	Dim retImage As Image = Nothing

	Using memStream As New System.IO.MemoryStream(thumbnailRaw)
		Using br As New System.IO.BinaryReader(memStream)
			Dim CF_METAFILEPICT As Integer = 3
			Dim CF_ENHMETAFILE As Integer = 14

			Dim clipboardFormatId As Integer = br.ReadInt32()
			'int clipFormat =
			Dim bytesRepresentMetafile As Boolean = (clipboardFormatId = CF_METAFILEPICT OrElse clipboardFormatId = CF_ENHMETAFILE)
			Try

				If bytesRepresentMetafile Then
					' the bytes represent a clipboard metafile

					' read past header information
					br.ReadInt16()
					br.ReadInt16()
					br.ReadInt16()
					br.ReadInt16()

					Dim mf As New System.Drawing.Imaging.Metafile(br.BaseStream)
					retImage = mf.GetThumbnailImage(width, height, New Image.GetThumbnailImageAbort(AddressOf getThumbnailImageAbort), IntPtr.Zero)
				Else
					' the bytes do not represent a metafile, try to convert to an Image
					memStream.Seek(0, System.IO.SeekOrigin.Begin)
					Dim im As Image = Image.FromStream(memStream, True, False)

					retImage = im.GetThumbnailImage(width, height, New Image.GetThumbnailImageAbort(AddressOf getThumbnailImageAbort), IntPtr.Zero)
				End If
			Catch
			End Try
		End Using
	End Using

	Return retImage
End Function

Private Shared Function getThumbnailImageAbort() As Boolean
	Return False
End Function