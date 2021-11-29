using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.Models.Entities;
using TVSeriesAPI.Models.Enums;

namespace TVSeriesAPI.DAL
{
    public static class DataSeeder
    {
        public static void SeedData(this ModelBuilder builder)
        {
            Genre genre1 = new Genre()
            {
                Id = 1,
                Name = "Comedy"
            };
            Genre genre2 = new Genre()
            {
                Id = 2,
                Name = "Animated"
            };
            Genre genre3 = new Genre()
            {
                Id = 3,
                Name = "Drama"
            };
            Genre genre4 = new Genre()
            {
                Id = 4,
                Name = "Criminal"
            };
            Genre genre5 = new Genre()
            {
                Id = 5,
                Name = "Documentary"
            };
            
            CastMember member1 = new CastMember()
            {
                Id=1,
                Name = "Steve Carell",
                Position = CastPosition.Actor,
            };
            CastMember member2 = new CastMember()
            {
                Id = 2,
                Name = "Justin Roiland",
                Position = CastPosition.Director,
            };
            CastMember member3 = new CastMember()
            {
                Id = 3,
                Name = "Bob Odenkirk",
                Position = CastPosition.Actor,
            };
            CastMember member4 = new CastMember()
            {
                Id = 4,
                Name = "Pedro Pascal",
                Position = CastPosition.Actor,
            };
            CastMember member5 = new CastMember()
            {
                Id = 5,
                Name = "Steven Avery",
                Position = CastPosition.Actor,
            };

            TVSerie serie1 = new TVSerie()
            {
                Id = 1,
                GenreId = genre1.Id,
                Title = "The Office (US)",
                ReleaseYear = 2005
            };

            TVSerie serie2 = new TVSerie()
            {
                Id = 2,
                GenreId = genre2.Id,
                Title = "Rick and Morty",
                ReleaseYear = 2013
            };

            TVSerie serie3 = new TVSerie()
            {
                Id = 3,
                GenreId = genre3.Id,
                Title = "Better Call Saul",
                ReleaseYear = 2015
            };

            TVSerie serie4 = new TVSerie()
            {
                Id = 4,
                GenreId = genre4.Id,
                Title = "Narcos",
                ReleaseYear = 2015
            };

            TVSerie serie5 = new TVSerie()
            {
                Id = 5,
                GenreId = genre5.Id,
                Title = "Making a Murderer",
                ReleaseYear = 2015
            };

            Season season1 = new Season()
            {
                Id = 1,
                TVSerieId = serie1.Id,
                Number = 1
            };

            Season season2 = new Season()
            {
                Id = 2,
                TVSerieId = serie2.Id,
                Number = 1
            };

            Season season3 = new Season()
            {
                Id = 3,
                TVSerieId = serie3.Id,
                Number = 1
            };

            Season season4 = new Season()
            {
                Id = 4,
                TVSerieId = serie4.Id,
                Number = 1
            };

            Season season5 = new Season()
            {
                Id = 5,
                TVSerieId = serie5.Id,
                Number = 1
            };

            Episode episode1 = new Episode()
            {
                Id = 1,
                Number = 1,
                SeasonId = season1.Id,
                Title = "The American Office"
            };

            Episode episode2 = new Episode()
            {
                Id = 2,
                Number = 1,
                SeasonId = season2.Id,
                Title = "Pilot"
            };

            Episode episode3 = new Episode()
            {
                Id = 3,
                Number = 1,
                SeasonId = season3.Id,
                Title = "Uno"
            };

            Episode episode4 = new Episode()
            {
                Id = 4,
                Number = 1,
                SeasonId = season4.Id,
                Title = "Descenso"
            };

            Episode episode5 = new Episode()
            {
                Id = 5,
                Number = 1,
                SeasonId = season5.Id,
                Title = "Eighteen Years Lost"
            };

            EpisodeCast cast1 = new EpisodeCast()
            {
                Id = 1,
                CastMemberId = member1.Id,
                EpisodeId = episode1.Id,
            };

            EpisodeCast cast2 = new EpisodeCast()
            {
                Id = 2,
                CastMemberId = member2.Id,
                EpisodeId = episode2.Id,
            };

            EpisodeCast cast3 = new EpisodeCast()
            {
                Id = 3,
                CastMemberId = member3.Id,
                EpisodeId = episode3.Id,
            };

            EpisodeCast cast4 = new EpisodeCast()
            {
                Id = 4,
                CastMemberId = member4.Id,
                EpisodeId = episode4.Id,
            };

            EpisodeCast cast5 = new EpisodeCast()
            {
                Id = 5,
                CastMemberId = member5.Id,
                EpisodeId = episode5.Id,
            };

            builder.Entity<Episode>().HasData(new List<Episode>() {
            episode1,
            episode2,
            episode3,
            episode4,
            episode5});

            builder.Entity<EpisodeCast>().HasData(new List<EpisodeCast>() {
            cast1,
            cast2,
            cast3,
            cast4,
            cast5});

            builder.Entity<CastMember>().HasData(new List<CastMember>() {
            member1,
            member2,
            member3,
            member4,
            member5});

            builder.Entity<Genre>().HasData(new List<Genre>() {
            genre1,
            genre2,
            genre3,
            genre4,
            genre5});

            builder.Entity<Season>().HasData(new List<Season>() {
            season1,
            season2,
            season3,
            season4,
            season5});

            builder.Entity<TVSerie>().HasData(new List<TVSerie>() {
            serie1,
            serie2,
            serie3,
            serie4,
            serie5});
        }
    }
}