using AndroidX.Car.App;
using AndroidX.Car.App.Model;

namespace Maui.Auto.Car.Platforms.Android.Handlers;

public class CarPageHandler : Screen
{
    private ITemplate? _nativeTemplate;

    public virtual CarPage VirtualView
    {
        get;
        set;
    }

    public CarPageHandler(CarPage page, CarContext carContext) : base(carContext) =>
        VirtualView = page;

    public override ITemplate OnGetTemplate()
    {
        if(_nativeTemplate is not null)
            return _nativeTemplate;

        var paneBuilder = new Pane.Builder();
        var contentView = VirtualView.Content;

        if (contentView?.Handler is ICarViewHandler<ITemplate> handler)
        {
            return handler.BuildView(VirtualView, (t) =>
            {
                _nativeTemplate = t;
                this.DispatchLifecycleEvent(AndroidX.Lifecycle.Lifecycle.Event.OnStart);
            });
        }
        else
            throw new InvalidOperationException("Invalid car page content handler");
    }
}
