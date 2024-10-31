using AndroidX.Car.App;
using AndroidX.Car.App.Model;

namespace Maui.Auto.Car.Platforms.Android.Handlers;

public class CarLabelHandler : ICarViewHandler<IItem>
{
    private CarPage? _parent;
    private Action<IItem>? _onPropertyUpdated;

    protected virtual CarLabel VirtualView { get; set; }

    protected CarContext Context { get; private set; }

    protected Row? NativeView { get; private set; }

    public CarLabelHandler(CarLabel carLabel, CarContext carContext)
    {
        VirtualView = carLabel;
        Context = carContext;
    }

    protected virtual void OnTextUpdated(string? newText)
    {
        if (NativeView is null)
            return;

        var newView = BuildView(_parent, _onPropertyUpdated);
        _onPropertyUpdated?.Invoke(newView);

    }

    public IItem BuildView(CarPage? parent, Action<IItem>? onPropertyUpdated)
    {
        _parent = parent;
        _onPropertyUpdated = onPropertyUpdated;

        var builder = new Row.Builder();
        builder.SetTitle(VirtualView.Text ?? "-");
        NativeView = builder.Build();
        
        return NativeView;
    }
}

public interface ICarViewHandler<TNativeTemplate>
{
    TNativeTemplate BuildView(CarPage? parent, Action<TNativeTemplate>? onPropertyUpdated);
}
