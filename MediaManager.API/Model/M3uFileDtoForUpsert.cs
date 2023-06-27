namespace MediaManager.API.Model
{
    public class M3uFileDtoForUpsert
    {
        public string Title { get; set; } = String.Empty;

        public ICollection<FileEntryDto> FilesInM3U { get; set; } = new List<FileEntryDto>();

        public override string ToString()
        {
            return Title;
        }
    }
}
