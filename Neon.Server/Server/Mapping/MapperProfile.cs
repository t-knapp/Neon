using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using AutoMapper;
using Neon.Domain;
using Neon.Server.Controllers;

namespace Neon.Server {
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ImageAsset, ImageAssetResource>();
            CreateMap<HtmlAsset, HtmlAssetResource>();

            CreateMap<ImageAsset, Asset>();
            CreateMap<HtmlAsset, Asset>();

            CreateMap<Asset, AssetResource>();

            CreateMap(typeof(JsonPatchDocument<>), typeof(JsonPatchDocument<>));
            CreateMap(typeof(Operation<>), typeof(Operation<>));
        }
    }
}