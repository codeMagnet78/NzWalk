namespace NzWalk.API.Models.DTO
{
    public class RegionDto
    {
        // Basically what ever you want to expose from Domain Regions model we paste it here
        // This looks repeat code but this is best way to do it.
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; } //This can be null so we have put ?
    }
}
