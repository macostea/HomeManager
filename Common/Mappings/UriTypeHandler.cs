using System;
using System.Data;
using Dapper;

namespace Common.Mappings
{
    public class UriTypeHandler : SqlMapper.TypeHandler<Uri>
    {
        public override void SetValue(IDbDataParameter parameter, Uri value)
        {
            parameter.Value = value.ToString();
        }

        public override Uri Parse(object value)
        {
            return new Uri(value.ToString());
        }
    }
}
