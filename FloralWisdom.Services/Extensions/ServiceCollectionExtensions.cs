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
		services.AddScoped<IRepository<Plant, string>, ProfRepository<Plant, string>>();
		services.AddScoped<IRepository<CareReminder, string>, ProfRepository<CareReminder, string>>();
		services.AddScoped<IRepository<DiseaseReport, string>, ProfRepository<DiseaseReport, string>>();
		services.AddScoped<IRepository<User, string>, ProfRepository<User, string>>();
		services.AddScoped<IRepository<UserPlant, object>, ProfRepository<UserPlant, object>>();
		services.AddScoped<IRepository<UserRequest, string>, ProfRepository<UserRequest, string>>();

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