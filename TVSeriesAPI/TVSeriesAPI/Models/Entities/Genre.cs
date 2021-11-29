﻿namespace TVSeriesAPI.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVSerie> TVSeries { get; set; }
    }
}
