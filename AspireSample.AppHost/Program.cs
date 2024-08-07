var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
/*builder.AddRedis("redis").PublishAsAzureRedis((_, _, cache) =>
{
    var (sku, family) = builder.Environment.EnvironmentName switch
    {
        "Staging" => ("Standard", "C0"),
        "Live" => ("Premium", "C1"),
        _ => ("Basic", "C0")
    };

    cache.AssignProperty(r => r.s);
    cache.AssignProperty(r => r.Sku.Name, sku);
    cache.AssignProperty(r => r.Sku.Family, family);
});*/

var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");

builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
