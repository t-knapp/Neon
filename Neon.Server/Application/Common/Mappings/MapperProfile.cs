using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using AutoMapper;
using Neon.Domain;

namespace Neon.Application;

public class MapperProfile : Profile
{
    public MapperProfile()
    {

        // Fail! Not allowed to use DTO / Resource types here, because of Onion-Architecture!

        // CreateMap<ImageAsset, ImageAssetResource>();
        // CreateMap<HtmlAsset, HtmlAssetResource>();

        // CreateMap<ImageAsset, Asset>();
        // CreateMap<HtmlAsset, Asset>();

        // CreateMap<Asset, AssetResource>();

        // CreateMap(typeof(JsonPatchDocument<>), typeof(JsonPatchDocument<>));
        // CreateMap(typeof(Operation<>), typeof(Operation<>));
    }
}
