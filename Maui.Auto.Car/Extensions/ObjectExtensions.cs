namespace Maui.Auto.Car.Extensions;

internal static class ObjectExtensions
{
    public static IServiceProvider GetServices(this object obj) =>
        Application.Current?.Handler?.MauiContext?.Services ?? throw new NullReferenceException("MauiContext cant be null");
}
