Simple library to add services defined with an attribute in the currently executing assembly to the ServiceCollection

<pre>

[AppService(ServiceLifetime.Transient, Key = "principal")
public class FirstService : IFirstService
{
    ...
}

[AppService]
public class SecondService
{
  ...
}

public static class ServiceFactories
{
   [AppService(ServiceLifetime.Scoped)]
   public static IThirdService CreateThird(IServiceProvider sp, object? key)
   { 
      var second - sp.GetRequiredService<SecondService>();
      return new ThirdService(second, "Hello World!", 1234);
   }
}


builder.Services.AddAppServices(); 
//builder.Services.AddKeyedTransient<IFirstService, FirstService>("principal")
//builder.Services.AddSingleton<SecondService>();
//builder.Services.AddScoped<IThirdService>(ServiceFactories.CreateThird);

</pre>
