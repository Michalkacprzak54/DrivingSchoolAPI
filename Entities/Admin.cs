using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace DrivingSchoolAPI.Entities
{
    public class Admin
    {
        [Column("id_administrator")]
        public int IdAdmin { get; set; }

        [Column("haslo")]
        public string AdminPassword { get; set; }
        
        [Column("email")]
        public string AdminEmail { get; set; }
        [Column("telefon")]
        public string AdminPhoneNumber { get; set; }

        [Column("data_utworzenia")]
        public DateTime CreationDate { get; set; }

        [Column("czy_aktwyny")]
        public bool IsActive { get; set; }
    }


public static class AdminEndpoints
{
	public static void MapAdminEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Admin").WithTags(nameof(Admin));

        group.MapGet("/", () =>
        {
            return new [] { new Admin() };
        })
        .WithName("GetAllAdmins")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Admin { ID = id };
        })
        .WithName("GetAdminById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Admin input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateAdmin")
        .WithOpenApi();

        group.MapPost("/", (Admin model) =>
        {
            //return TypedResults.Created($"/api/Admins/{model.ID}", model);
        })
        .WithName("CreateAdmin")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Admin { ID = id });
        })
        .WithName("DeleteAdmin")
        .WithOpenApi();
    }
}}
