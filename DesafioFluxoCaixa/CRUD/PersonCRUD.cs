using DesafioFluxoCaixa.Models.Pessoas;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFluxoCaixa.CRUD
{
    public class PersonCRUD
    {
        private ISession _session;
        public PersonCRUD(ISession session) => _session = session;
        public async Task Add(Person item)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }

        }

        public IEnumerable<Person> FindAll() =>
                        _session.Query<Person>().ToList();

        public async Task<Person> FindByID(int id) =>
                        await _session.GetAsync<Person>(id);

        public Person FindByEmail(string email) =>
                _session.Query<Person>().Where(x => x.Email == email).FirstOrDefault();

        public async Task Remove(int id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Person>(id);
                await _session.DeleteAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task Update(Person item)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                await _session.UpdateAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }
}
