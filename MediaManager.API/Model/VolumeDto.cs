namespace MediaManager.API.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="VolumeDto.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    using System;

    public class VolumeDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public string Moniker { get; set; } = String.Empty;

        public DateTimeOffset Created { get; set; } = DateTime.MinValue;

        public DateTimeOffset LastModified { get; set; } = DateTime.MinValue;

        public ICollection<M3uFileDto> M3uFiles { get; set; } = new List<M3uFileDto>();

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
