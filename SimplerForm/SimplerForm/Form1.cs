using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimplerForm
{
    public partial class Form1 : Form
    {
        // Коллекция для хранения данных
        private List<string> items = new List<string>();
        private string savePath = "items.txt";

        public Form1()
        {
            InitializeComponent();
        }

        // --- Кнопка "Добавить" ---
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string text = textEnter.Text.Trim();

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Поле ввода пустое!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Добавляем в коллекцию
            items.Add(text);

            // Добавляем в ListView
            viewList.Items.Add(text);

            // Очищаем поле ввода
            textEnter.Clear();
            textEnter.Focus();
        }

        // --- Удаление элемента по двойному клику ---
        private void viewList_MouseDoubleClick(object sender, EventArgs e)
        {
            if (viewList.SelectedItems.Count == 0)
                return;

            var item = viewList.SelectedItems[0];

            // Подтверждение удаления (не обязательно)
            if (MessageBox.Show($"Удалить элемент: \"{item.Text}\"?",
                "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Удаляем из коллекции
                items.Remove(item.Text);

                // Удаляем из ListView
                viewList.Items.Remove(item);
            }
        }

        // --- Кнопка "Выход" ---
        private void buttonExit_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllLines(savePath, items);
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(savePath))
            {
                var lines = System.IO.File.ReadAllLines(savePath);

                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        items.Add(line);
                        viewList.Items.Add(line);
                    }
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.File.WriteAllLines(savePath, items);
        }
    }
}
