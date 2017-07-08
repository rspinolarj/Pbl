using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pbl.Models
{
    public static class Singletone
    {
        private static FamervEntities db;

        public static FamervEntities InstanceFamerv
        {
            get
            {
                if (db == null)
                {
                    db = new FamervEntities();
                }
                return db;
            }
        }


        public static FamervEntities Refresh
        {
            get
            {
                db.Dispose();
                db = new FamervEntities();
                return db;
            }
        }
    }
}