// Подключаем базовую библиотеку System, чтобы использовать консоль и другие функции
using System;

// Главный класс приложения
class Program
{
    // Точка входа в программу
    static void Main()
    {
        // Заранее заданный правильный логин (константа — нельзя изменить)
        const string correctLogin = "admin";

        // Заранее заданный правильный пароль
        const string correctPassword = "secret";

        // Количество попыток для пользователя
        int attempts = 3;

        // Цикл будет работать, пока число попыток больше нуля
        while (attempts > 0)
        {
            // Запрос ввода логина
            Console.Write("Логин: ");
            string login = Console.ReadLine();

            // Запрос ввода пароля
            Console.Write("Пароль: ");
            string password = Console.ReadLine();

            // Проверяем совпадают ли введённые значения с правильными
            if (login == correctLogin && password == correctPassword)
            {
                // Успех, доступ разрешён
                Console.WriteLine("Доступ разрешён");

                // Завершаем выполнение программы
                return;
            }
            else
            {
                // Уменьшаем количество оставшихся попыток
                attempts--;

                // Если ещё остались попытки
                if (attempts > 0)
                {
                    Console.WriteLine($"Неверные данные. Осталось попыток: {attempts}");
                }
                else
                {
                    // Если попытки закончились — доступ блокируется
                    Console.WriteLine("Доступ запрещён");
                    Console.ReadLine();
                }
            }
        }
    }
}
