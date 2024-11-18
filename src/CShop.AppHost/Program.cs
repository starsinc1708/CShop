var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CShop_CatalogService>("cshop-catalog-api");

builder.Build().Run();
