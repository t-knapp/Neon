using System;
using System.Threading.Tasks;
using Neon.Domain;

namespace Neon.Application;

public interface IHtmlAssetRepository : IRepository<HtmlAsset>
{
    // Task<Asset> GetByIdSometingSpecial(string somethingSpecial, CancellationToken cancellationToken = default);
}