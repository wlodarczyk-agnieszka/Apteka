using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Apteka
{
    abstract class ActiveRecord
    {
        public int ID { get; protected set; }

        protected SqlConnection _connection = new SqlConnection("Initial Catalog=Apteka;" +
                                                                "Data Source=192.168.29.128;" +
                                                                "User id=sa;" + "Password=Test2010;");
        protected SqlCommand _command = new SqlCommand();

        public abstract void Save();
        public abstract void Reload();
        public abstract void Remove();

        protected virtual void Open()
        {
            //_connection.ConnectionString = "Initial Catalog=Apteka;" +
            //                               "Data Source=192.168.29.128;" +
            //                               "User id=sa;" + "Password=Test2010;";
            _connection.Open();
            _command.Connection = _connection;
        }

        protected virtual void Close()
        {
            _connection.Close();
        }
    }
}
