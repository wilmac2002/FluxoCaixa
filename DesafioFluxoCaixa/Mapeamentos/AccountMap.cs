using DesafioFluxoCaixa.Models.Contas;
using DesafioFluxoCaixa.Models.Pessoas;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace DesafioFluxoCaixa.Mapeamentos
{
    public class AccountMap : ClassMapping<Account>
    {
        public AccountMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Foreign<Account>(Account => Account.Person));
                x.Type(NHibernateUtil.Int32);
            });

            OneToOne(x => x.Person, x =>
             {
                 x.Constrained(true);
             });

            Bag(conta => conta.Saques, map => 
            {
                map.Key(k => k.Column("CON_Id"));
                map.Cascade(Cascade.DeleteOrphans);
            }, 
            rel => rel.OneToMany());

            Bag(conta => conta.Depositos, map =>
            {
                map.Key(k => k.Column("CON_Id"));
                map.Cascade(Cascade.DeleteOrphans);
            },
            rel => rel.OneToMany());

            Property(b => b.Balance, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.Column("CON_Saldo");
            });

            Property(b => b.Limit, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.Column("CON_Limite");
            });

            Property(b => b.MinimalBalance, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.Column("CON_Minimo");
            });

            Table("Contas");
        }
    }

}