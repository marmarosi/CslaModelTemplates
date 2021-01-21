using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CslaModelTemplates.Dal
{
    /// <summary>
    /// Defines the functionality of the registrar object for a data access layer.
    /// </summary>
    public interface IDalRegistrar
    {
        void AddDalContext(
            IConfiguration configuration,
            IServiceCollection services
            );
    }
}
