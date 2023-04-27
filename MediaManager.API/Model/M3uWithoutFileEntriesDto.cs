﻿namespace MediaManager.API.Model
{
    public class M3uWithoutFileEntriesDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public string? VolumeMoniker { get; set; }

        public int TotalMegaBytes { get; set; } = 42;

        public DateTime Created { get; set; } = DateTime.MinValue;

        public DateTime LastModified { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return Title;
        }
    }
}
