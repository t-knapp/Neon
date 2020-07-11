using AutoMapper;
using Neon.Server.Models;
using Neon.Server.Controllers;

namespace Neon.Server {
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ImageAsset, ImageAssetResource>();
        }
    }
}