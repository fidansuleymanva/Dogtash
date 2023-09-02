using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Dogtas.MVC.Localization.Services
{
    public class SharedResource { }
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;
        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name); // §REVIEW_DJE: "SharedResource" or "ShareResource"
        }


        public string this[string index]
        {
            get
            {
                var a = _localizer[index];
                return a;
            }
        }

        public LocalizedString Getkey(string key)
        {
            var a = _localizer[key];
            return a;
        }
    }
}
