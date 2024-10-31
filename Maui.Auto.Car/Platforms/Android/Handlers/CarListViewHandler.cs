using AndroidX.Car.App;
using AndroidX.Car.App.Model;
using System.ComponentModel;
using Action = AndroidX.Car.App.Model.Action;

namespace Maui.Auto.Car.Platforms.Android.Handlers;

public class CarListViewHandler : ICarViewHandler<ITemplate>
{
    private CarPage? _parent;
    private Action<ITemplate>? _onPropertyUpdated;

    protected virtual CarListView VirtualView { get; set; }

    protected CarContext Context { get; private set; }

    protected ListTemplate? NativeView { get; private set; }

    public CarListViewHandler(CarListView carListview, CarContext carContext)
    {
        VirtualView = carListview;
        Context = carContext;

        VirtualView.PropertyChanged += OnVirtualViewPropertyChanged;
    }

    private void OnVirtualViewPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var newView = BuildView(_parent, _onPropertyUpdated);
        _onPropertyUpdated?.Invoke(newView);
    }

    public ITemplate BuildView(CarPage? parent, Action<ITemplate>? onPropertyUpdated)
    {
        _parent = parent;
        _onPropertyUpdated = onPropertyUpdated;
        var itemListBuilder = new ItemList.Builder();

        if (!string.IsNullOrWhiteSpace(VirtualView.EmptyMessage))
            itemListBuilder.SetNoItemsMessage(VirtualView.EmptyMessage);

        ItemList? itemList = null;
        for (var i = 0; i < VirtualView.Children.Count; i++)
        {
            if (VirtualView.Children[i].Handler is ICarViewHandler<IItem> iItemHandler)
            {
                var item = iItemHandler.BuildView(parent, (nv) =>
                {
                    if (itemList is not null)
                        itemList.Items[i] = nv;
                });

                itemListBuilder.AddItem(item);
            }
        }

        itemList = itemListBuilder.Build();
        var templateBuilder = new ListTemplate.Builder()
            .SetSingleList(itemList);

        if (parent?.HasNavigationBar ?? true)
        {
            templateBuilder.SetTitle(parent?.Title ?? string.Empty);
            if (parent?.HasBackButton ?? true)
            {
                if (parent?.Navigation?.NavigationStack?.Count > 1)
                    templateBuilder.SetHeaderAction(Action.Back);
                else
                    templateBuilder.SetHeaderAction(Action.AppIcon);
            }
        }

        NativeView = templateBuilder.Build();
        return NativeView;
    }
}
