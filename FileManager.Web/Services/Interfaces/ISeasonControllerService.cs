﻿using System.Collections.Generic;
using FileManager.Models;

namespace FileManager.Web.Services.Interfaces
{
    public interface ISeasonControllerService
    {
        Season GetSeasonById(int id);
        IEnumerable<Season> GetSeasons();
        IEnumerable<Season> GetSeasonsByShowId(int showId);
        int SaveSeason(Season season);
    }
}