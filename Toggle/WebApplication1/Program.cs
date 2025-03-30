using System.Security.Claims;
using Common.Models.ActivityModels;
using Common.Models.PersonModels;
using Common.Models.ProjectModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Toggle.BL;
using Toggle.BL.Facades.Interfaces;
using Toggle.DAL;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDataAccessLayer();
            builder.Services.AddBussinessLayer();

            builder.Services.AddHttpContextAccessor();


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters.ValidateAudience = false;
                });

            builder.Services.AddAuthorization(
                config =>
                {
                    config.AddPolicy("adminpolicy", policyBuilder => policyBuilder.RequireRole("admin"));

                    config.AddPolicy("activityreadpolicy", policyBuilder => policyBuilder.RequireClaim("activities", "read"));
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            /////////////////////////////////////////////////////////////////////
            ////////////////////////       ACTIVITY      ////////////////////////
            /////////////////////////////////////////////////////////////////////

            var activities = app.MapGroup("/").WithTags("Activity");

            //-----------------------------------------------------------------//
            //----------------------          GET        ----------------------//
            //-----------------------------------------------------------------//

            activities.MapGet("/ActivityGetAll", (IActivityFacade activityFacade, IHttpContextAccessor contextAccessor) =>
            {
                var result = activityFacade.GetAll();
                var id = contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                return Results.Ok(result);
            })
            .WithName("ActivityGetAll")
            .RequireAuthorization("adminpolicy");

            activities.MapGet("/ActivityGetById/{Id}", async (IActivityFacade activityFacade, Guid Id) =>
            {
                var result = await activityFacade.GetById(Id);
                if (result is not null)
                {
                    return Results.Ok(result);
                }


                return Results.NotFound("Nenalezeno");

            })
            .WithName("ActivityGetById");

            //-----------------------------------------------------------------//
            //----------------------        DELETE       ----------------------//
            //-----------------------------------------------------------------//

            activities.MapDelete("/ActivityDelete", async (IActivityFacade activityFacade, Guid Id) =>
            {
                var result = await activityFacade.DeleteById(Id);
                if (result)
                {
                    return Results.Ok(result);
                }
                return Results.NotFound();
            })
            .WithName("ActivityDelete");

            //-----------------------------------------------------------------//
            //----------------------         POST        ----------------------//
            //-----------------------------------------------------------------//

            activities.MapPost("/ActivityPost", async (IActivityFacade activityFacade, ActivityCreateModel model) =>
            {
                try
                {
                    var result = await activityFacade.Create(model);
                    var response = new { Id = result, Name = model };
                    return Results.Created($"/ActivityGetById/{result}", response);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("ActivityPost");

            //-----------------------------------------------------------------//
            //---------------------         UPDATE        ---------------------//
            //-----------------------------------------------------------------//

            activities.MapPut("/ActivityPut", async (IActivityFacade activityFacade, ActivityDetailModel model) =>
            {
                try
                {
                    await activityFacade.Update(model);
                    return Results.Created($"/ActivityGetById/", model);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("ActivityPut");

            /////////////////////////////////////////////////////////////////////
            /////////////////////////       PERSON      /////////////////////////
            /////////////////////////////////////////////////////////////////////

            var people = app.MapGroup("/").WithTags("People"); ;

            //-----------------------------------------------------------------//
            //----------------------          GET        ----------------------//
            //-----------------------------------------------------------------//


            people.MapGet("/PersonGetAll", (IPersonFacade personFacade) =>
            {
                var result = personFacade.GetAll();
                return Results.Ok(result);
            })
            .WithName("PersonGetAll");

            people.MapGet("/PersonGetById/{Id}", async (IPersonFacade personFacade, Guid Id) =>
            {
                var result = await personFacade.GetById(Id);
                if (result is not null)
                {
                    return Results.Ok(result);
                }
                return Results.NotFound("Nenalezeno");
            })
            .WithName("PersonGetById");

            //-----------------------------------------------------------------//
            //----------------------        DELETE       ----------------------//
            //-----------------------------------------------------------------//

            people.MapDelete("/PersonDelete", async (IPersonFacade personFacade, Guid Id) =>
            {
                var result = await personFacade.DeleteById(Id);
                if (result)
                {
                    return Results.Ok(result);
                }
                return Results.NotFound();
            })
            .WithName("PersonDelete");

            //-----------------------------------------------------------------//
            //----------------------         POST        ----------------------//
            //-----------------------------------------------------------------//

            people.MapPost("/PersonPost", async (IPersonFacade personFacade, PersonCreateModel model) =>
            {
                try
                {
                    var result = await personFacade.Create(model);
                    var response = new { Id = result, Name = model };
                    return Results.Created($"/PersonGetById/{result}", response);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("PersonPost");

            //-----------------------------------------------------------------//
            //---------------------         UPDATE        ---------------------//
            //-----------------------------------------------------------------//

            people.MapPut("/PersonPut", async (IPersonFacade personFacade, PersonUpdateModel model) =>
            {
                try
                {
                    await personFacade.Update(model);
                    return Results.Created($"/PersonGetById/", model);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("PersonPut");

            people.MapPut("/PersonPutActivity", async (IPersonFacade personFacade, [FromQuery] Guid PersonId, [FromQuery] Guid activityId) =>
            {
                try
                {
                    await personFacade.AddActivity(PersonId, activityId);
                    return Results.Created();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("PersonPutActivity");

            people.MapPut("/PersonPutProject", async (IPersonFacade personFacade, [FromQuery] Guid PersonId, [FromQuery] Guid projectId) =>
            {
                try
                {
                    await personFacade.AddProject(PersonId, projectId);
                    return Results.Created();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("PersonPutProject");

            /////////////////////////////////////////////////////////////////////
            ////////////////////////       PROJECTS      ////////////////////////
            /////////////////////////////////////////////////////////////////////

            var projects = app.MapGroup("/").WithTags("Projects");

            //-----------------------------------------------------------------//
            //----------------------          GET        ----------------------//
            //-----------------------------------------------------------------//

            projects.MapGet("/ProjectsGetAll", (IProjectFacade projectFacade) =>
            {
                var result = projectFacade.GetAll();
                return Results.Ok(result);
            })
            .WithName("ProjectsGetAll");

            projects.MapGet("/ProjectsGetById/{Id}", async (IProjectFacade projectFacade, Guid Id) =>
            {
                var result = await projectFacade.GetById(Id);
                if (result is not null)
                {
                    return Results.Ok(result);
                }
                return Results.NotFound("Nenalezeno");
            })
            .WithName("ProjectsGetById");

            //-----------------------------------------------------------------//
            //----------------------        DELETE       ----------------------//
            //-----------------------------------------------------------------//

            projects.MapDelete("/ProjectsDelete", async (IProjectFacade projectFacade, Guid Id) =>
            {
                var result = await projectFacade.DeleteById(Id);
                if (result)
                {
                    return Results.Ok(result);
                }
                return Results.NotFound();
            })
            .WithName("ProjectsDelete");

            //-----------------------------------------------------------------//
            //----------------------         POST        ----------------------//
            //-----------------------------------------------------------------//

            projects.MapPost("/ProjectsPost", async (IProjectFacade projectFacade, ProjectCreateModel model) =>
            {
                try
                {

                    var result = await projectFacade.Create(model);
                    var response = new { Id = result, Name = model };
                    return Results.Created($"/ProjectsGetById/{result}", response);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("ProjectsPost");

            //-----------------------------------------------------------------//
            //---------------------         UPDATE        ---------------------//
            //-----------------------------------------------------------------//

            projects.MapPut("/ProjectsPut", async (IProjectFacade projectFacade, ProjectUpdateModel model) =>
            {
                try
                {
                    await projectFacade.Update(model);
                    return Results.Created($"/ProjectsGetById/", model);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("ProjectsPut");

            projects.MapPut("/ProjectPutActivity", async (IProjectFacade projectFacade, [FromQuery] Guid projectId, [FromQuery] Guid activityId) =>
            {
                try
                {
                    await projectFacade.AddActivityToProject2(projectId, activityId);
                    return Results.Created();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("ProjectPutActivity");

            app.Run();
        }
    }
}
