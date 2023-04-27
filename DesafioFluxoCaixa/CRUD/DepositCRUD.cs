using DesafioFluxoCaixa.Models.Transacoes;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFluxoCaixa.CRUD
{
    public class DepositCRUD
    {
        private ISession _session;
        public DepositCRUD(ISession session) => _session = session;
        public async Task Add(Deposit item)
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

        public IEnumerable<Deposit> FindAll() =>
                        _session.Query<Deposit>().ToArray();

        public IEnumerable<Deposit> FindAllByDates(FiltroPesquisaTransacoes datas)
        {
            return _session.Query<Deposit>().Where(x => (x.Date.Date >= datas.DataInicial && x.Date.Date <= datas.DataFinal) && x.Conta.Id == datas.UserId);
        }

        public IEnumerable<Deposit> FindAllLast7Days(int id)
        {
            IEnumerable<Deposit> deposits = _session.Query<Deposit>().Where(x => (x.Date >= DateTime.Now.Date.AddDays(-7) && x.Date <= DateTime.Now) && (x.Conta.Id == id)).ToArray();

            return deposits;
        }

        public IEnumerable<Deposit> FindAllLast15Days(int id)
        {
            IEnumerable<Deposit> deposits = _session.Query<Deposit>().Where(x => (x.Date >= DateTime.Now.Date.AddDays(-15) && x.Date <= DateTime.Now) && (x.Conta.Id == id)).ToArray();

            return deposits;
        }

        public IEnumerable<Deposit> FindAllLast30Days(int id)
        {
            IEnumerable<Deposit> deposits = _session.Query<Deposit>().Where(x => (x.Date >= DateTime.Now.Date.AddDays(-30) && x.Date <= DateTime.Now) && (x.Conta.Id == id)).ToArray();

            return deposits;
        }

        public IEnumerable<Deposit> FindAllByAccount(int id) =>
                _session.Query<Deposit>().Where(x => x.Conta.Id == id);

        public async Task<Deposit> FindByID(int id) =>
                        await _session.GetAsync<Deposit>(id);

        public async Task Remove(int id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Deposit>(id);
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
                var item = this.FindAllByForeignId(id).ToList();

                foreach(var deposit in item)
                {
                    await _session.DeleteAsync(deposit);
                }

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

        public async Task Update(Deposit item)
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

