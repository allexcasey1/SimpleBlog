namespace Domain
{
    public class Section
    {
        public Guid Id { get; set; }
        public string SectionHeader { get; set; } = string.Empty;
        public string SectionText { get; set; } = string.Empty;
        public Guid ContentId { get; set; }
        public Content? Content { get; set; }
    }
}