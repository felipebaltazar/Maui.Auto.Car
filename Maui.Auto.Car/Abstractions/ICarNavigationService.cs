namespace Maui.Auto.Car.Abstractions;

public interface ICarNavigationService
{
    IList<CarPage> NavigationStack { get; }
    Task PopAsync();
    Task PushAsync(CarPage page);
}
