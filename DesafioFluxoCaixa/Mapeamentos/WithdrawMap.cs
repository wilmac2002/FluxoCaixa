using DesafioFluxoCaixa.Models.Transacoes;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DesafioFluxoCaixa.Mapeamentos
{
    public class WithdrawMap : ClassMapping<Withdraw>
    {
        public WithdrawMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Increment);
                x.Type(NHibernateUtil.Int32);
                x.Column("SAQ_Id");
            });

            ManyToOne(x => x.Conta, x =>
            {
                x.Column("CON_Id");
            });

            Property(b => b.Value, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.NotNullable(true);
                x.Column("SAQ_Valor");
            });

            Property(b => b.Date, x =>
            {
                x.Type(NHibernateUtil.DateTime);
                x.NotNullable(true);
                x.Column("SAQ_Data");
            });

            Property(b => b.Description, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.Column("SAQ_Descricao");
            });

            Table("Saques");
        }
    }

}