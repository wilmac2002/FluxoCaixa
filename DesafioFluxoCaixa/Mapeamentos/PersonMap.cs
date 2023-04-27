using DesafioFluxoCaixa.Models.Pessoas;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DesafioFluxoCaixa.Mapeamentos
{
    public class PersonMap : ClassMapping<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Identity);
                x.Type(NHibernateUtil.Int32);
                x.Column("PES_Id");
            });

            OneToOne(x => x.Conta, x =>
            {
                x.Cascade(Cascade.All);
            });

            Property(x => x.Password, x =>
            {
                x.Length(10);
                x.Type(NHibernateUtil.String);
                x.Column("PES_Senha");
                x.NotNullable(true);
            });

            Property(b => b.Name, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
                x.Column("PES_Nome");
            });

            Property(b => b.Email, x =>
            {
                x.Length(30);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
                x.Column("PES_Email");
            });

            Property(b => b.Salario, x =>
            {
                x.Type(NHibernateUtil.Double);
                x.Scale(2);
                x.Precision(15);
                x.Column("PES_Salario");
            });

            Table("Pessoas");
        }
    }

}
