using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;

namespace core_strength_yoga_products_api.Interfaces
{
    public interface IProxyResolver<T1, T2>
    {
        T1 Resolve(T2 src);
    }
}
