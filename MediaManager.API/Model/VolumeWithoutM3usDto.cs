namespace MediaManager.API.Model
{
    public class VolumeWithoutM3usDto
    {
        public string Title { get; set; } = String.Empty;

        public string Moniker { get; set; } = String.Empty;

        public DateTime Created { get; set; } = DateTime.MinValue;

        public DateTime LastModified { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
