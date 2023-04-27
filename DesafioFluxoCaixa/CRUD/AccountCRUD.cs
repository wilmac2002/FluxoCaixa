using DesafioFluxoCaixa.Models.Contas;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFluxoCaixa.CRUD
{
    public class AccountCRUD
    {
        private ISession _session;
        public AccountCRUD(ISession session) => _session = session;
        public async Task Add(Account item)
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

        public IEnumerable<Account> FindAll() =>
                        _session.Query<Account>().ToList();

        //public async Task<Account> FindByID(int id) =>
        //                await _session.GetAsync<Account>(id);

        public async Task<Account> FindByID(int id) =>
                await _session.GetAsync<Account>(id);

        public Account FindByForeignId(int id) =>
                        _session.Query<Account>().Where(x => x.Person.Id == id).FirstOrDefault();

        public async Task Remove(int id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Account>(id);
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

        public async Task RemoveByForeignId(int id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = this.FindByForeignId(id);
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

        public async Task Update(Account item)
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

