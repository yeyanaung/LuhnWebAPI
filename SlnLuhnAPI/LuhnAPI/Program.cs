using Microsoft.OpenApi.Models;
using System.Reflection;

// Create a new web application builder with default configurations
var builder = WebApplication.CreateBuilder(args);

// Generate an XML filename based on the current assembly name for XML documentation
var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

// Add basic MVC controllers to the dependency injection container
// This allows the application to handle HTTP requests using controller classes
builder.Services.AddControllers();

// Add API exploration services for Swagger
// These services help generate OpenAPI (Swagger) documentation for the API
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger generation with custom settings
builder.Services.AddSwaggerGen(c =>
{
    // Define the API version and metadata
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "LuhnAPI",
        Description = "Luhn API V1.0"
    });

    // Use full type names for schema IDs to avoid naming conflicts
    c.CustomSchemaIds(type => type.FullName);

    // Include XML comments from the generated XML documentation file
    // This adds descriptions from XML comments in code to the Swagger documentation
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add logging services to the application
// Allows capturing and managing log events throughout the application
builder.Services.AddLogging();

// Build the web application based on the configured services
var app = builder.Build();

// Configure the HTTP request pipeline
// Only enable Swagger UI and Swagger endpoint in Development environment
if (app.Environment.IsDevelopment())
{
    // Enable Swagger endpoint for generating API documentation
    app.UseSwagger();

    // Enable Swagger UI for interactive API documentation
    app.UseSwaggerUI();
}

// Add authorization middleware to the request pipeline
// This allows implementing authentication and authorization if needed
app.UseAuthorization();

// Map controller routes 
// This connects the controller classes to their corresponding HTTP endpoints
app.MapControllers();

// Start the web application and begin listening for incoming HTTP requests
app.Run();
