using Log.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Log.Repository
{
    public class LoggerDataBase
    {
        public void RecordLogWS(LogDto log) {
            var _parametrosDoMetodo = new List<SqlParameter>();
            
            new DataBaseRepository().NonQuery("<Command here>",
                _parametrosDoMetodo.ToArray(), System.Data.CommandType.StoredProcedure);

        }
    }
}
