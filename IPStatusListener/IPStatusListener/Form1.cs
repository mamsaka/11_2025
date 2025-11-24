using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPStatusListener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await MonitorServers(); // запуск мониторинга
        }

        private void saveIP_Click(object sender, EventArgs e)
        {
            string text = enterIP.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Поле ввода пустое!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckType(text) == "Unknown")
            {
                MessageBox.Show("Невалидный ввод!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Добавляем в ListBox
            IPList.Items.Add(text);

            enterIP.Clear();
            enterIP.Focus();
        }

        private void IPList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IPList.SelectedIndex == -1)
                return;

            string item = IPList.SelectedItem.ToString();

            if (MessageBox.Show($"Удалить элемент: \"{item}\"?",
                "Удаление",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IPList.Items.RemoveAt(IPList.SelectedIndex);
            }
        }

        // -----------------------
        // Проверка IP или домена
        // -----------------------

        static string CheckType(string input)
        {
            if (IPAddress.TryParse(input, out _))
                return "IP";

            if (IsValidDomain(input))
                return "Domain";

            return "Unknown";
        }

        static bool IsValidDomain(string input)
        {
            input = input.Replace("http://", "").Replace("https://", "");

            string pattern = @"^(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$";
            return Regex.IsMatch(input, pattern);
        }

        // -----------------------
        // Проверка статуса
        // -----------------------

        private string CheckServerStatus(string address)
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(address, 1000);

                if (reply.Status == IPStatus.Success)
                    return "online server";

                return "offline server";
            }
            catch
            {
                return "unable to ping";
            }
        }

        // -----------------------
        // Мониторинг серверов
        // -----------------------

        private async Task MonitorServers()
        {
            while (true)
            {
                // Берём копию, чтобы не падало при удалении элементов во время цикла
                List<string> items = IPList.Items.Cast<string>().ToList();

                foreach (var item in items)
                {
                    string IP = item
                        .Replace(": online server", "")
                        .Replace(": offline server", "")
                        .Replace(": unable to ping", "");

                    string status = await Task.Run(() => CheckServerStatus(IP));
                    string newStatus = $"{IP}: {status}";

                    Invoke(new Action(() =>
                    {
                        int index = IPList.Items.IndexOf(item);
                        if (index != -1)
                            IPList.Items[index] = newStatus;
                    }));
                }

                await Task.Delay(1000);
            }
        }
    }
}
