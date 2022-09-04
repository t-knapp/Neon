using System;
using System.Threading.Tasks;
using Neon.Domain;

namespace Neon.Application;

public interface IAssetRepository : IRepository<Asset>
{
    // Task<Asset> GetByIdSometingSpecial(string somethingSpecial, CancellationToken cancellationToken = default);
}