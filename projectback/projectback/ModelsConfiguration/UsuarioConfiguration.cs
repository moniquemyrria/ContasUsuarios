using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using projectback.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectback.ModelsConfiguration
{
    public class UsuarioConfiguration
    {
        public UsuarioConfiguration(EntityTypeBuilder<UsuarioDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Usuarios").HasKey(t => t.id);

                entity.Property(t => t.nome).HasMaxLength(100).IsRequired();
                
                entity.Property(t => t.email).HasMaxLength(50).IsRequired();
                
                entity.Property(t => t.senha).HasMaxLength(50).IsRequired();

                entity.Property(t => t.dataNascimento);

                entity.Property(t => t.cpf).HasMaxLength(13);

                entity.Property(t => t.deletado).HasMaxLength(1);

                entity.Property(t => t.dataCadastro);


            }
        }
    }
}
