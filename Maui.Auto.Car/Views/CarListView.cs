using Maui.Auto.Car.Abstractions;

#if ANDROID
using Maui.Auto.Car.Platforms.Android.Handlers;
using Maui.Auto.Car.Platforms.Android.Sessions;
#endif

namespace Maui.Auto.Car;

[ContentProperty(nameof(Children))]
public class CarListView : BindableObject, ICarView
{
    public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(
        nameof(Children),
        typeof(IList<ICarView>),
        typeof(CarListView),
        new List<ICarView>());

    public static readonly BindableProperty EmptyMessageProperty = BindableProperty.Create(
        nameof(EmptyMessage),
        typeof(IList<ICarView>),
        typeof(string),
        null);

    public IList<ICarView> Children
    {
        get => (IList<ICarView>)GetValue(ChildrenProperty);
        set => SetValue(ChildrenProperty, value);
    }

    public string? EmptyMessage
    {
        get => (string?)GetValue(EmptyMessageProperty);
        set => SetValue(EmptyMessageProperty, value);
    }

    public object Handler
    {
        get
        {
#if ANDROID
            return new CarListViewHandler(this, AndroidAutoSession.Context ?? throw new InvalidOperationException("Session should be initialized"));
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}
