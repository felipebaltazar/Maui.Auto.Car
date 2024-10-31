# Maui.Auto.Car
You can now draw your UI centered at a cross-platform level, for Android Auto and Apple CarPlay.

Based on this amazing tutorial from Christian Strydom

https://github.com/christian-strydom/MauiForCars

![image](https://github.com/user-attachments/assets/3685a33c-48d5-4f9f-9e06-9b6c85932406)


## Setup

In the MauiProgram, you must add `AddMauiAutoCar` and define the startup page.

```csharp
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .AddMauiAutoCar<MenuPage>() //Define the startpage
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
```


### Android

You will need to include these meta-data to your android manifest

```xml
    <meta-data android:name="com.google.android.gms.car.application" android:resource="@xml/automotive_app_desc" />
    <meta-data android:name="androidx.car.app.minCarApiLevel" android:value="1" />
````

Example:

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
  <application android:allowBackup="true" android:icon="@mipmap/appicon" android:roundIcon="@mipmap/appicon_round" android:supportsRtl="true">
    <meta-data android:name="com.google.android.gms.car.application" android:resource="@xml/automotive_app_desc" />
    <meta-data android:name="androidx.car.app.minCarApiLevel" android:value="1" />
  </application>
</manifest>
```

-------------

## Environment

Here you will find some tips to debug and run the Android Auto and Apple Car Play applications.

### Android Auto Emulator

https://developer.android.com/training/cars/testing/dhu?hl=pt-br

### Apple Car Play Simulator

https://medium.com/@paereson/how-to-open-carplay-simulator-on-real-device-in-xcode-14-6a8399564b8b
