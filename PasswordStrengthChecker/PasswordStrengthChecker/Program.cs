// Библиотека System для работы с консолью
using System;

// Главный класс программы
class Program
{
    // Основной метод программы
    static void Main()
    {
        while (true)
        {
            // Просим пользователя ввести пароль
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            // Флаг наличия хотя бы одной цифры в пароле
            bool hasDigit = false;

            // Флаг наличия хотя бы одной заглавной буквы
            bool hasUpper = false;

            // Перебираем каждый символ введённого пароля
            foreach (char c in password)
            {
                // Проверяем, является ли символ цифрой
                if (char.IsDigit(c))
                    hasDigit = true;

                // Проверяем, является ли символ заглавной латинской буквой
                if (char.IsUpper(c))
                    hasUpper = true;
            }

            // Проверяем выполнение всех условий надёжности
            if (password.Length >= 8 && hasDigit && hasUpper)
            {
                // Если все три условия выполнены — пароль надёжный
                Console.WriteLine("Пароль надёжный");
                break;
            }
            else
            {
                // Если хотя бы одно условие не выполнено — слабый
                Console.WriteLine("Пароль слабый");
            }
        }
    }
}
