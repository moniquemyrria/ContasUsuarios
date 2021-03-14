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
    public class EnderecoConfiguration
    {
        public EnderecoConfiguration(EntityTypeBuilder<EnderecoDTO> entity)
        {
            if (entity != null)
            {
                entity.ToTable("Enderecos").HasKey(t => t.id);

                entity.Property(t => t.cep).HasMaxLength(10).IsRequired();
                
                entity.Property(t => t.logradouro).HasMaxLength(80).IsRequired();
                
                entity.Property(t => t.bairro).HasMaxLength(80).IsRequired();
                
                entity.Property(t => t.localidade).HasMaxLength(50).IsRequired();

                entity.Property(t => t.uf).HasMaxLength(2).IsRequired();

                entity.Property(t => t.tipo).HasMaxLength(20).IsRequired();

                entity.Property(t => t.numero).HasMaxLength(5).IsRequired();
                
                entity.Property(t => t.complemento).HasMaxLength(50);

                //Usuario - FK
                entity.HasOne(t => t.UsuarioDTO).WithMany(t => t.Endereco).HasForeignKey(t => t.idUsuario).HasPrincipalKey(t => t.id);


            }
        }
    }
}
