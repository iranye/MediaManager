namespace MediaManager.API.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="M3uFileDto.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------

    public class M3uFileDto
    {
        public int Id { get; set; }

        public int TotalMegaBytes { get; set; } = 42;

        public string Title { get; set; } = String.Empty;

        public DateTime Created { get; set; } = DateTime.MinValue;

        public DateTime LastModified { get; set; } = DateTime.MinValue;

        public ICollection<FileEntryDto> FilesInM3U { get; set; } = new List<FileEntryDto>();

        public override string ToString()
        {
            return Title;
        }
    }
}
