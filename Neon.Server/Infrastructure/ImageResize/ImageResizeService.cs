using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using Neon.Application;

namespace Neon.Infrastructure;

public class ImageResizeService : IImageResizeService {
    
    public Stream Resize( Stream raw, System.Drawing.Size targetSize ) {
        try {
            using ( Image image = Image.Load( raw, out IImageFormat mime ) )
            {
                int destWidth = targetSize.Width;
                int destHeight = targetSize.Height;
                double xScaleFactor = image.Width / (double) destWidth;
                double yScaleFactor = image.Height / (double) destHeight;
                if ( xScaleFactor >= yScaleFactor )
                    destHeight = 0;
                else
                    destWidth = 0;

                image.Mutate( x => x.Resize(destWidth, destHeight ) );

                var output = new MemoryStream();
                image.Save( output, mime );
                return output;
            }
        } catch ( Exception ) {
            return raw;
        }
    }
}