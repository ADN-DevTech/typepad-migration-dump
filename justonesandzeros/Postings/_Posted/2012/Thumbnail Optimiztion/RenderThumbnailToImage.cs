public static Image RenderThumbnailToImage(PropInst propInst, int width, int height)
{
    // convert the property value to a byte array
    byte[] thumbnailRaw = propInst.Val as byte[];

    if (null == thumbnailRaw || 0 == thumbnailRaw.Length)
        return null;

    Image retImage = null;

    using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(thumbnailRaw))
    {
        using (System.IO.BinaryReader br = new System.IO.BinaryReader(memStream))
        {
            int CF_METAFILEPICT = 3;
            int CF_ENHMETAFILE = 14;

            int clipboardFormatId = br.ReadInt32(); /*int clipFormat =*/
            bool bytesRepresentMetafile = (clipboardFormatId == CF_METAFILEPICT || clipboardFormatId == CF_ENHMETAFILE);
            try
            {
                        
                if (bytesRepresentMetafile)
                {
                    // the bytes represent a clipboard metafile

                    // read past header information
                    br.ReadInt16();
                    br.ReadInt16();
                    br.ReadInt16();
                    br.ReadInt16();

                    System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(br.BaseStream);
                    retImage = mf.GetThumbnailImage(width, height, new Image.GetThumbnailImageAbort(getThumbnailImageAbort), IntPtr.Zero);
                }
                else 
                {
                    // the bytes do not represent a metafile, try to convert to an Image
                    memStream.Seek(0, System.IO.SeekOrigin.Begin);
                    Image im = Image.FromStream(memStream, true, false);

                    retImage = im.GetThumbnailImage(width, height, new Image.GetThumbnailImageAbort(getThumbnailImageAbort), IntPtr.Zero);
                }
            }
            catch
            {
            }
        }
    }

    return retImage;
}

private static bool getThumbnailImageAbort()
{
    return false;
}