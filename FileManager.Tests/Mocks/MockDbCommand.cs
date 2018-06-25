using System;
using System.Data;

namespace FileManager.Tests.Mocks
{
    public class MockDbCommand : IDbCommand
    {
        private readonly Type _type;

        public MockDbCommand(Type type)
        {
            _type = type;
        }

        public string CommandText { get; set; }
        public int CommandTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CommandType CommandType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDbConnection Connection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDataParameterCollection Parameters => new MockDataParameterCollection();

        public IDbTransaction Transaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UpdateRowSource UpdatedRowSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public int ExecuteNonQuery()
        {
            return 1;
        }

        public IDataReader ExecuteReader()
        {
            return new MockDataReader(_type);
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }
    }
}