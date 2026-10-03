namespace VideoGames.BLL.Dtos
{
    public class DeveloperDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Country { get; set; }
        public int Year { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<string> Games { get; set; } = [];
    }
}
