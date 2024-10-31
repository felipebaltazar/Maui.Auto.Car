using Maui.Auto.Car.Abstractions;
#if ANDROID
using Maui.Auto.Car.Platforms.Android.Services;
#endif

namespace Maui.Auto.Car;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder AddMauiAutoCar<TStartPage>(this MauiAppBuilder builder) where TStartPage : CarPage
    {
        builder.Services.AddTransient<TStartPage>();
        builder.Services.AddSingleton<IStartPage>((s) => new StartPageWrapper<TStartPage>(() => s.GetRequiredService<TStartPage>()));

#if ANDROID
        builder.Services.AddSingleton<IContextResolver, ContextResolver>();
        builder.Services.AddSingleton<ICarNavigationService, AndroidAutoNavigationService>();
#endif
        return builder;
    }
}
