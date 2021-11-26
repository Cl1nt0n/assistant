using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tariff.model.Tools;

namespace Tariff.model
{
    [Serializable]
    class TariffData
    {
        private List<Tariff> _tariffs;

        public event Action UpdatedTariff;
        public event Action RemovedTariff;
        public event Action AddedTariff;
        public event Action<string, string> GettingAccess;

        public TariffData()
        {
            Loader loader = new Loader();

            _tariffs = loader.LoadTariffs();
        }

        public void Save()
        {
            Saver saver = new Saver();
            saver.Save(this);
        }

        public void AddTariff(int gygabytes, int minutes, int messages, int price, string name)
        {
            _tariffs.Add(new Tariff(name, minutes, gygabytes, messages, price));
            AddedTariff?.Invoke();
        }

        public void RemoveTariff(int id)
        {
            foreach (var item in _tariffs)
            {
                if (id == item.Id)
                {
                    _tariffs.Remove(item);
                    RemovedTariff?.Invoke();
                    return;
                }
            }
        }

        public void UpdateTariff(int incomeID, IEnumerable<IReadOnlyTariff> tariffs, int sum = -1, int categoryId = -1)
        {
            int index = FindTariffIndexById(incomeID, (List<Tariff>)tariffs);

            if (index != -1)
            {
                //tariffs.ToList()[index].Update(sum, category);
            }
        }

        private int FindTariffIndexById(int id, IEnumerable<IReadOnlyTariff> tariffs)
        {
            IReadOnlyList<IReadOnlyTariff> tariffList = tariffs.ToList();

            for (int i = 0; i < tariffList.Count; i++)
                if (tariffList[i].Id == id)
                    return i;

            return -1;
        }

        public void AccessAdminData(string login, string password)
        {
            StreamReader streamReaderPassword = new StreamReader("Password.txt");
            StreamReader streamReaderLogin = new StreamReader("Login.txt");
            if (streamReaderPassword.ReadLine() == password && streamReaderLogin.ReadLine() == login)
            {
                GettingAccess?.Invoke(login, password);
            }
        }

        public IReadOnlyList<IReadOnlyTariff> GetTariffs() => _tariffs;
    }
}
