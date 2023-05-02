namespace MediaManager.API.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="FileEntryDto.cs" company="IRANYE">
    //   Copyright (c) IRANYE. All rights reserved.
    // </copyright>
    // --------------------------------------------------------------------------------------------------------------------
    public class FileEntryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
