using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tariff
{
    public partial class AdminForm : Form
    {
        public event Action<int, int, int, int, string> AddingTariff;
        public event Action<int> RemovingTariff;
        public event Action<int, int, int, int, int, string> UpdatingTariff;

        public event Func<IReadOnlyList<IReadOnlyTariff>> GettingAllTariffs;

        public AdminForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddOrUpdateTariff(AddingTariff);
        }

        public void RefreshListBox()
        {
            listBoxTariffs.DataSource = null;
            listBoxTariffs.DataSource = GettingAllTariffs?.Invoke();
            listBoxTariffs.SelectedIndex = -1;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxTariffs.SelectedIndex != -1)
            {
                RemovingTariff?.Invoke(listBoxTariffs.SelectedIndex);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxTariffs.SelectedIndex != -1)
            {
                //UpdatingTariff?.Invoke(int.Parse(maskedTextBoxGygabytes.Text),
                //                         int.Parse(maskedTextBoxMinutes.Text),
                //                         int.Parse(maskedTextBoxMessages.Text),
                //                         int.Parse(maskedTextBoxPrice.Text),
                //                         textBoxName.Text);
            }
        }

        private void AddOrUpdateTariff(Action<int, int, int, int, string> AddingOrUpdating)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text) == false
                            && string.IsNullOrWhiteSpace(maskedTextBoxGygabytes.Text) == false
                            && string.IsNullOrWhiteSpace(maskedTextBoxMinutes.Text) == false
                            && string.IsNullOrWhiteSpace(maskedTextBoxMessages.Text) == false
                            && string.IsNullOrWhiteSpace(maskedTextBoxPrice.Text) == false)
            {
                AddingOrUpdating?.Invoke(int.Parse(maskedTextBoxGygabytes.Text),
                                         int.Parse(maskedTextBoxMinutes.Text),
                                         int.Parse(maskedTextBoxMessages.Text),
                                         int.Parse(maskedTextBoxPrice.Text),
                                         textBoxName.Text);
            }
        }
    }
}
