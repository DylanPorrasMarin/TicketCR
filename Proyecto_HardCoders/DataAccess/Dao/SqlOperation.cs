using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public Dictionary<string, object> OutputParameters { get; private set; }
        public List<SqlParameter> parameters;

        public SqlOperation()
        {
            parameters = new List<SqlParameter>();
        }

        public void AddVarcharParam(string parameterName, string paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }

        public void AddIntegerParam(string parameterName, int paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }

        public void AddFloatParam(string parameterName, float paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }

        public void AddBooleanParam(string parameterName, bool paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }


        public void AddDateTimeParam(string parameterName, DateTime paramValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, paramValue));
        }
        public void AddOutputParam(string parameterName, SqlDbType sqlDbType)
        {
            SqlParameter outputParam = new SqlParameter("@" + parameterName, sqlDbType);
            outputParam.Direction = ParameterDirection.Output;
            parameters.Add(outputParam);
        }

        public object GetParameterValue(string parameterName)
        {
            SqlParameter parameter = parameters.FirstOrDefault(p => p.ParameterName == "@" + parameterName);
            if (parameter != null)
            {
                return parameter.Value;
            }
            return null;
        }

        public void AddStructuredParam(string parameterName, string typeName, DataTable value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, SqlDbType.Structured);
            parameter.TypeName = typeName;
            parameter.Value = value;
            parameters.Add(parameter);
        }

        public void AddBitParam(string paramName, bool value, bool isOutput = false)
        {
            SqlParameter param = new SqlParameter
            {
                ParameterName = paramName,
                SqlDbType = SqlDbType.Bit,
                Value = value
            };

            if (isOutput)
            {
                param.Direction = ParameterDirection.Output;
            }

            parameters.Add(param);
        }


    }
}
