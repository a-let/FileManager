using System;
using System.Data;
using FileManager.BusinessLayer.Interfaces;

namespace FileManager.Tests.Mocks
{
    public class MockFileManagerDb : IFileManagerDb
    {
        private readonly Type _type;

        public MockFileManagerDb(Type type)
        {
            _type = type;
        }

        public IDbCommand CreateCommand()
        {
            return new MockDbCommand(_type);
        }

        public IDbConnection CreateConnection()
        {
            return new MockDbConnection();
        }

        public IDbDataParameter CreateParameter(string name, object value)
        {
            return new MockDbDataParameter();
        }
    }
}