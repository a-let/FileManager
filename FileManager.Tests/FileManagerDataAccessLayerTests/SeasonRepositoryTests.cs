﻿using FileManager.DataAccessLayer;
using FileManager.DataAccessLayer.Repositories;
using FileManager.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace FileManager.Tests.FileManagerDataAccessLayerTests
{
    [Collection("Database collection")]
    public class SeasonRepositoryTests
    {
        private readonly FileManagerContext _context;

        public SeasonRepositoryTests(DatabaseFixture dbFixture)
        {
            _context = dbFixture.Context;
        }

        [Fact]
        public async Task GetSeasonById_GivenValidId_ThenSeasonIsReturned()
        {
            // Arrange
            var id = 1;

            var seasonRepo = new SeasonRepository(_context);

            // Act
            var season = await seasonRepo.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, season.SeasonId);
        }

        [Fact]
        public void GetSeasons_ThenReturnsSeasonList()
        {
            // Arrange
            var seasonRepo = new SeasonRepository(_context);

            // Act
            var seasons = seasonRepo.Get();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Season>>(seasons);
        }

        [Fact]
        public async Task SaveSeason_GivenValidNewSeason_ThenReturnsSeasonId()
        {
            // Arrange
            var season = new Season
            {
                SeasonId = 0,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            var seasonRepo = new SeasonRepository(_context);

            // Act
            await seasonRepo.SaveAsync(season);

            // Assert
            Assert.True(season.SeasonId > 0);
        }

        [Fact]
        public async Task SaveSeason_GivenValidExistingSeason_ThenSeasonIdIsEqual()
        {
            // Arrange
            var season = new Season
            {
                SeasonId = 1,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            var seasonRepo = new SeasonRepository(_context);

            // Act
            season.Path = "Updated Path";

            await seasonRepo.SaveAsync(season);

            // Assert
            Assert.Equal(1, season.SeasonId);
        }

        [Fact]
        public async Task SaveSeason_GivenNonExistingSeason_ThenThrowsArgumentNullException()
        {
            // Arrange
            var season = new Season
            {
                SeasonId = 10,
                ShowId = 1,
                SeasonNumber = 1,
                Path = "Test"
            };

            var seasonRepo = new SeasonRepository(_context);

            // Act
            var exception = await Record.ExceptionAsync(async () => await seasonRepo.SaveAsync(season));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}