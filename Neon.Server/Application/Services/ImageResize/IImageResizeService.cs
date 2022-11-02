namespace Neon.Application;

public interface IImageResizeService {
    Stream Resize( Stream raw, System.Drawing.Size targetSize );
}