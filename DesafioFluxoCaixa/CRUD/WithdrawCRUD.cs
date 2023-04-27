using DesafioFluxoCaixa.Models.Transacoes;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFluxoCaixa.CRUD
{
    public class WithdrawCRUD
    {
        private ISession _session;
        public WithdrawCRUD(ISession session) => _session = session;

        public async Task Add(Withdraw item)
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

        public IEnumerable<Withdraw> FindAllByAccount(int id) =>
                _session.Query<Withdraw>().Where(x => x.Conta.Id == id);

        public IEnumerable<Withdraw> FindAllByDates(FiltroPesquisaTransacoes datas)
        {
            return _session.Query<Withdraw>().Where(x => (x.Date.Date >= datas.DataInicial && x.Date.Date <= datas.DataFinal) && x.Conta.Id == datas.UserId);
        }

        public IEnumerable<Withdraw> FindAllLast7Days(int id)
        {
            IEnumerable<Withdraw> saques = _session.Query<Withdraw>().Where(x => (x.Date >= DateTime.Now.Date.AddDays(-7) && x.Date <= DateTime.Now) && (x.Conta.Id == id)).ToArray();

            return saques;
        }

        public IEnumerable<Withdraw> FindAllLast15Days(int id)
        {
            IEnumerable<Withdraw> saques = _session.Query<Withdraw>().Where(x => (x.Date >= DateTime.Now.Date.AddDays(-15) && x.Date <= DateTime.Now) && (x.Conta.Id == id)).ToArray();

            return saques;
        }

        public IEnumerable<Withdraw> FindAllLast30Days(int id)
        {
            IEnumerable<Withdraw> saques = _session.Query<Withdraw>().Where(x => (x.Date >= DateTime.Now.Date.AddDays(-30) && x.Date <= DateTime.Now) && (x.Conta.Id == id)).ToArray();

            return saques;
        }

        public async Task<Withdraw> FindByID(int id) =>
                        await _session.GetAsync<Withdraw>(id);

        public async Task Remove(int id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Withdraw>(id);
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
                var item = this.FindAllByAccount(id).ToList();

                foreach (var saque in item)
                {
                    await _session.DeleteAsync(saque);
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

        public async Task Update(Withdraw item)
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

