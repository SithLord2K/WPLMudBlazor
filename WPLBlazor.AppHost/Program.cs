var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WPLBlazor>("wplblazor");

builder.Build().Run();
