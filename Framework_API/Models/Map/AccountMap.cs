using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Models.Map
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Definindo as configurações das propriedades para o banco de dados
            builder.HasKey(a => a.UserId);

            // Ao definir uma chave estrangeira 1-1 é necessário especificar a entidade dependente no HasForeignKey (https://docs.microsoft.com/pt-br/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#one-to-one)            
            builder.HasOne(a => a.User).WithOne(a => a.Account).HasForeignKey<Account>(a => a.UserId);
        }
    }
}