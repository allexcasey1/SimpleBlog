namespace Domain
{
    public class Content
    {
        public Guid Id { get; set; }
        public List<Section> Sections { get; set; } = new List<Section>();
    }
}