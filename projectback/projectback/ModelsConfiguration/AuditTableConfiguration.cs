using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectback.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectback.ModelsConfiguration
{
    public class AuditTableConfiguration
    {
        public AuditTableConfiguration(EntityTypeBuilder<AuditTableDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("AuditLog").HasKey(t => t.Id);
                entity.Property(t => t.DateTime).IsRequired();
                entity.Property(t => t.KeyValues).IsRequired();

                entity.Property(t => t.OldValues);
                entity.Property(t => t.NewValues).IsRequired();

                entity.Property(t => t.UsuarioId).IsRequired();
                entity.Property(t => t.TableName).IsRequired();

            }
        }
    }
}
