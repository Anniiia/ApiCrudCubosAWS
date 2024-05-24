using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiCrudPersonajes2AWS.Repositories;
using ApiCrudPersonajes2AWS.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiCrudPersonajes2AWS;

public class Functions
{
    private RepositoryCubos repo;
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions(RepositoryCubos repo)
    {
        this.repo = repo;
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public async Task<IHttpResult> Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<Cubo> cubos = await this.repo.GetCubosAsync();
        return HttpResults.Ok(cubos);
    }
    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/find/{id}")]
    public async Task<IHttpResult> Find(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Find' Request");
        Cubo cubo = await this.repo.FindCubosAsync(id);
        return HttpResults.Ok(cubo);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/post")]
    public async Task<IHttpResult> Post([FromBody] Cubo cubo, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'POST' Request");
        Cubo cuboNew = await this.repo.CreateCuboAsync(cubo.Nombre, cubo.Marca,cubo.Imagen, cubo.Precio);
        return HttpResults.Ok();
    }


    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Put, "/put")]
    public async Task<IHttpResult> Put([FromBody] Cubo cubo, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Put' Request");

        await this.repo.UpdateCuboAsync(cubo.IdCubo, cubo.Nombre, cubo.Marca, cubo.Imagen, cubo.Precio);

        return HttpResults.Ok();

    }


    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Delete, "/delete/{id}")]
    public async Task<IHttpResult> Delete(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Delete' Request");
        await this.repo.DeleteCuboAsync(id);
        return HttpResults.Ok();
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/marcas")]
    public async Task<IHttpResult> GetMarca (ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<string> marcas = await this.repo.GetMarcasAsync();
        return HttpResults.Ok(marcas);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/marcas/{marca}")]
    public async Task<IHttpResult> GetCubosMarca(string marca,ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<Cubo> cubos = await this.repo.FindCubosMarcasAsync(marca);
        return HttpResults.Ok(cubos);
    }
}
