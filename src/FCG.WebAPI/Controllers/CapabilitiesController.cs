using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CapabilitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCapabilities()
        {
            var capabilities = new
            {
                Name = "FIAP Cloud Games API",
                Version = "1.0.0",
                Description = "API for managing digital games, users, and game library",
                Features = new[]
                {
                    "User Management - Register, update, and manage users",
                    "Authentication & Authorization - JWT-based with User and Admin roles",
                    "Game Catalog - CRUD operations for digital games",
                    "User Library - Purchase and list acquired games"
                },
                Endpoints = new
                {
                    Auth = new
                    {
                        Login = "POST /api/Auth/login - Authenticate and receive JWT token"
                    },
                    Users = new
                    {
                        Create = "POST /api/Users - Create a new user",
                        GetAll = "GET /api/Users - List all users",
                        GetById = "GET /api/Users/{id} - Get user by ID",
                        Update = "PUT /api/Users/{id} - Update user",
                        Delete = "DELETE /api/Users/{id} - Delete user"
                    },
                    Games = new
                    {
                        GetAll = "GET /api/Games - List all games (requires authentication)",
                        GetById = "GET /api/Games/{id} - Get game by ID (requires authentication)",
                        Create = "POST /api/Games - Create a new game (Admin only)",
                        Update = "PUT /api/Games/{id} - Update game (Admin only)",
                        Delete = "DELETE /api/Games/{id} - Delete game (Admin only)",
                        Acquire = "POST /api/Games/{id}/acquire - Purchase a game (requires authentication)"
                    },
                    Capabilities = new
                    {
                        Get = "GET /api/Capabilities - Get API capabilities (this endpoint)"
                    }
                },
                TechnicalStack = new[]
                {
                    ".NET 8",
                    "C# 12",
                    "ASP.NET Core Web API",
                    "Entity Framework Core",
                    "JWT Authentication",
                    "Swagger/OpenAPI"
                }
            };

            return Ok(capabilities);
        }
    }
}
