using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace FileManager.Tests.Mocks
{
    public class MockConfiguration : IConfiguration
    {
        public string this[string key]
        {
            get => "Test";
            set { value = "Test"; }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            return new MockConfigurationSection();
        }
    }

    public class MockConfigurationSection : IConfigurationSection
    {
        public string this[string key]
        {
            get => "Test";
            set { value = "Test"; }
        }

        public string Key => throw new NotImplementedException();

        public string Path => throw new NotImplementedException();

        public string Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            return new MockConfigurationSection();
        }
    }

}
