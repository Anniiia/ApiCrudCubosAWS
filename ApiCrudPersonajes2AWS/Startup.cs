using Amazon.Lambda.Annotations;
using ApiCrudPersonajes2AWS.Data;
using ApiCrudPersonajes2AWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCrudPersonajes2AWS;

[LambdaStartup]
public class Startup
{
    /// <summary>
    /// Services for Lambda functions can be registered in the services dependency injection container in this method. 
    ///
    /// The services can be injected into the Lambda function through the containing type's constructor or as a
    /// parameter in the Lambda function using the FromService attribute. Services injected for the constructor have
    /// the lifetime of the Lambda compute container. Services injected as parameters are created within the scope
    /// of the function invocation.
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true);

        var configuration = builder.Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddTransient<RepositoryCubos>();
        services.AddSingleton<IConfiguration>(configuration);

        string connectionString = configuration.GetConnectionString("MySql");

        services.AddDbContext<CubosContext>

            (options => options.UseMySql(connectionString,

            ServerVersion.AutoDetect(connectionString)));
    }
}
