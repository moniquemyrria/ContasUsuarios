using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectback.ModelsData
{
    public class AuditTableDTO
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public long UsuarioId { get; set; }
    }
}
