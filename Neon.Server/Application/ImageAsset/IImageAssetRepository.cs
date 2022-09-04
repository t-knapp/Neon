using System;
using System.Threading.Tasks;
using Neon.Domain;

namespace Neon.Application;

public interface IImageAssetRepository : IRepository<ImageAsset>
{
    // Task<Asset> GetByIdSometingSpecial(string somethingSpecial, CancellationToken cancellationToken = default);
}