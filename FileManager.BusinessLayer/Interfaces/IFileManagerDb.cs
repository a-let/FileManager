﻿using System.Data.SqlClient;

namespace FileManager.BusinessLayer.Interfaces
{
    public interface IFileManagerDb
    {
        SqlConnection CreateConnection();
    }
}