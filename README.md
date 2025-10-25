##  Report for the Course “Web Service Development (CSE5031adw)”
### Project Title
### Development of a Web API Service with JWT Authentication, Logging, and Swagger Documentation

#### Completed by
#### Temirlan Daumov
#### Student, Satbayev University
#### Major: Information Technology

#### Date
#### October 2025



## Introduction
The purpose of this project is to study and implement the server-side part of a web application using ASP.NET Core, applying modern approaches to building RESTful services.
This includes implementing CRUD operations, JWT authentication, the Repository pattern, and setting up logging (Serilog + Seq) and API documentation with Swagger (OpenAPI).

The project demonstrates practical skills in API development, dependency injection, configuration management, and integration of monitoring and analysis tools.

## Main Part

### 1. Project Structure
The project WebServiceProject was developed using the ASP.NET Core Web API template.

Its main components include:

* Program.cs — main configuration and application startup file
* appsettings.json — configuration file (database connections, JWT, Serilog, etc.)
* Controllers — classes handling HTTP requests
* Repositories — data access layer using the Repository pattern
* Models — data models
* Data — database contexts (AppDbContext, AuthDbContext)

### 2. Database Configuration
The project uses two databases:
* WebServiceDB — main database for storing product information
* AuthDB — separate database for user authentication and roles (Identity)

Connection strings are defined in `appsettings.json:`

`"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=WebServiceDB;Trusted_Connection=True;TrustServerCertificate=True;",
  "AuthConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AuthDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}`

### 3. Repository Implementation

To ensure scalability and code clarity, the Repository pattern was implemented.

Example from `ProductRepository.cs:`

```csharp
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll() => _context.Products.ToList();
    public Product? GetById(int id) => _context.Products.Find(id);

    public void Add(Product product) => _context.Products.Add(product);
    public void Update(Product product) => _context.Products.Update(product);

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
            _context.Products.Remove(product);
    }

    public void Save() => _context.SaveChanges();
}
```

This approach separates business logic from data access logic, improving code maintainability and testability.

### 4. Product Controller

The `ProductsController` implements CRUD operations:

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_repo.GetAll());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _repo.GetById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _repo.Add(product);
        _repo.Save();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        _repo.Update(product);
        _repo.Save();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        _repo.Save();
        return NoContent();
    }
}
```

This controller handles all API endpoints under `/api/products` and interacts with the database through the repository layer.

### 5. JWT Authentication

To secure the API, JWT (JSON Web Token) authentication was implemented.

The token is generated during login and validated for each request.

Configuration in `appsettings.json:`

```json
"Jwt": {
  "Key": "ThisIsASecretKeyForJWTAuthentication1234567",
  "Issuer": "https://localhost:44308",
  "Audience": "https://localhost:44308"
}
```

JWT is configured in `Program.cs` using `JwtBearerDefaults.AuthenticationScheme`, ensuring secure access to protected endpoints.

### 6. Logging (Serilog and Seq)

Logging is implemented using Serilog, which records system events and API activity.
Logs are saved:

* to the console,
* to text files `(Logs/log-.txt),`
* and to the Seq server at `http://localhost:5341.`

Configuration in `Program.cs:`
```csharp
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Host.UseSerilog();
```
Serilog provides structured and detailed logs, while Seq allows real-time viewing and filtering through a web interface.

### 7. API Documentation (Swagger)
Swagger UI is used for documentation and testing of API endpoints.

It is available at:

`https://localhost:7024/swagger`

Swagger automatically generates documentation for all endpoints and supports JWT authentication via the Authorize button.

### 8. HTTP Request Pipeline

The application pipeline configuration in `Program.cs includes:`
```csharp
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

Swagger is automatically enabled in the development environment.

## Project Results

As a result of the work, the following were implemented:
* Fully functional REST API with CRUD operations
* JWT authentication and authorization
* Logging with Serilog and Seq
* Entity Framework Core integration with SQL Server
* Swagger (OpenAPI) documentation
* Clean architecture using Repository pattern and Dependency Injection

## Conclusion

In conclusion, a fully working Web API project was developed and configured using ASP.NET Core.
The project demonstrates the practical application of modern web service technologies, including secure authorization, structured logging, automatic API documentation, and database interaction.

This project provided valuable experience in building reliable, scalable, and well-structured web applications using Microsoft technologies.
