using FileManager.Web.Services.Interfaces;
using System;

namespace FileManager.Tests.Mocks
{
    public class MockTokenGenerator : ITokenGenerator
    {
        public string GenerateToken(string userName) => Guid.NewGuid().ToString();
    }
}