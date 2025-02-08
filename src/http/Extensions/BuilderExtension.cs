using System.Text.Json.Serialization;
using ClassManager.Domain.Libs.MassTransit.Events;
using ClassManager.Domain.Libs.MassTransit.Publish;
using Data.Contexts.Foods.Repositories;
using Data.Database;
using Domain;
using Domain.Contexts.Foods.Handlers;
using Domain.Contexts.Foods.Repositories.Contracts;
using Domain.Contexts.Foods.Services;
using Domain.Contexts.Foods.Services.Contracts;
using Domain.Contexts.Seeds;
using Domain.Jobs;
using Domain.Libs.MassTransit.Publish;
using Domain.Services.Contracts;
using Http.Contexts.Shared.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Http.Extensions;
public static class BuilderExtension
{
  public static void AddConfiguration(this WebApplicationBuilder builder)
  {
    Configuration.Database.ConnectionString =
        builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
  }

  public static void AddDatabase(this WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<AppDbContext>(x =>
        x.UseSqlServer(
            Configuration.Database.ConnectionString
        ));
  }

  public static void AddControllers(this WebApplicationBuilder builder)
  {
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
      options.SuppressModelStateInvalidFilter = true;
    }).AddJsonOptions(x =>
    {
      x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });
  }



  public static void AddServices(this WebApplicationBuilder builder)
  {
    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);
    builder.Services.Configure<CheckFoodsOptions>(builder.Configuration.GetSection(CheckFoodsOptions.CheckFoodsOptionsKey));

    builder.Services.AddTransient<
      IFoodRepository,
      FoodRepository>();

    builder.Services.AddTransient<
      IGroupRepository,
      GroupRepository>();

    builder.Services.AddTransient<
      IWrapperService,
      WrapperService>();

    builder.Services.AddTransient<
   IFoodService,
   FoodService>();

    builder.Services.AddTransient<
      IPublishBus,
      PublishBus>();

    builder.Services.AddTransient<ListFoodsHandler>();
    builder.Services.AddTransient<GenerateGroupSeed>();
    /*     builder.Services.AddTransient<GetFoodProfileHandler>(); */
  }

  public static void AddRabbitMqService(this WebApplicationBuilder builder)
  {
    builder.Services.AddMassTransit(bussConfigurator =>
    {
      bussConfigurator.AddConsumer<GenerateFoodsEventConsumer>();

      bussConfigurator.UsingRabbitMq((ctx, config) =>
      {
        config.Host(new Uri(Configuration.RabbitMq.Uri), host =>
        {
          host.Username(Configuration.RabbitMq.Username);
          host.Password(Configuration.RabbitMq.Password);
        });

        config.ConfigureEndpoints(ctx);
      });
    });
  }

  public static void AddQuartz(this WebApplicationBuilder builder)
  {
    builder.Services.AddQuartz(opt =>
    {
      opt.UseMicrosoftDependencyInjectionJobFactory();
      var checkFoodsJob = new JobKey("CheckFoodsJob");
      opt.AddJob<CheckFoodsJob>(options => options.WithIdentity(checkFoodsJob));
      opt.AddTrigger(options =>
      {
        options
        .ForJob(checkFoodsJob)
        .WithIdentity("CheckFoodsJob-trigger")
        .WithCronSchedule(builder.Configuration.GetSection("CheckFoodsJob:CronSchedule").Value ?? "0 */5 * ? * *");
      });
    });

    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
  }
}