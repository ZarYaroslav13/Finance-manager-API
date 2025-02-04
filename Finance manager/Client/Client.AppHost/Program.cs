var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Client_ApiService>("apiservice");

builder.AddProject<Projects.Client_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
