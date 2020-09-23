using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWT.Libs.BBS.Areas.Forums.Services
{
    public class BBSService : IBBSService, IDisposable
    {
        protected DataConnection DataConnection { get; set; }
        public BBSService()
        {
            DataConnection = this.GetDB();
        }
        public void Dispose()
        {
            DataConnection.Dispose();
        }
    }
}
