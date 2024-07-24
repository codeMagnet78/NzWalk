namespace NzWalk.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; } //This can be null so we have put ?
    }
}
