namespace FloralWisdom.Services.ViewModels
{
	public class PlantViewModel
	{
		public string Id { get; set; } = null!;

		public string Name { get; set; } = null!;

		public string ScientificName { get; set; } = null!;

		public string Description { get; set; } = null!;

		public int WateringFrequency { get; set; }

		public string SunlightRequirement { get; set; } = null!;
	}
}
