#region Usings

using FloralWisdom.Data.Repositories;
using FloralWisdom.Services.Implementations;
using FloralWisdom.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FloralWisdom.Models.Entities;

#endregion

namespace FloralWisdom.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection RegisterRepositories(
		this IServiceCollection services)
	{
		services.AddScoped<IRepository<Plant, int>, ProfRepository<Plant, int>>();
		services.AddScoped<IRepository<CareReminder, int>, ProfRepository<CareReminder, int>>();
		services.AddScoped<IRepository<DiseaseReport, int>, ProfRepository<DiseaseReport, int>>();
		services.AddScoped<IRepository<User, int>, ProfRepository<User, int>>();
		services.AddScoped<IRepository<UserPlant, object>, ProfRepository<UserPlant, object>>();
		services.AddScoped<IRepository<UserRequest, int>, ProfRepository<UserRequest, int>>();

		return services;
	}

	public static IServiceCollection RegisterUserDefinedServices(
		this IServiceCollection services)
	{
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IPlantService, PlantService>();
		services.AddScoped<ICareReminderService, CareReminderService>();
		services.AddScoped<IDiseaseReportService, DiseaseReportService>();
		services.AddScoped<IUserRequestService, UserRequestService>();

		return services;
	}
}