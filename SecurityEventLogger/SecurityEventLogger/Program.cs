// Подключаем необходимые библиотеки для работы с коллекциями, консолью и задачами
using System;
using System.Collections.Generic;

// Главный класс программы
class Program
{
    // Точка входа в программу
    static void Main(string[] args)
    {
        // Списки для хранения зарегистрированных логинов, паролей и логов событий
        List<string> logins = new List<string>();
        List<string> passwords = new List<string>();
        List<string> log = new List<string>();

        // Переменные для временного хранения введённых данных пользователя
        string login = null;
        string password = null;
        string Date = null;

        // Бесконечный цикл для отображения меню и обработки команд
        while (true)
        {
            // Вывод меню доступных действий
            Console.WriteLine("Выберите действие: ");
            Console.WriteLine("1 -- Регистрация");
            Console.WriteLine("2 -- Логин");
            Console.WriteLine("3 -- Вывести лог");

            // Чтение выбранной команды от пользователя
            string command = Console.ReadLine();

            // Обработка выбранной команды с помощью switch
            switch (command)
            {
                case "1":
                    // Регистрация нового пользователя
                    Console.Write("Логин: ");
                    login = Console.ReadLine();

                    Console.Write("Пароль: ");
                    password = Console.ReadLine();

                    // Получение текущей даты и времени в формате строки
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                    // Проверка, существует ли уже такой логин в списке
                    if (logins.Contains(login))
                    {
                        // Логирование неудачной попытки регистрации
                        log.Add($"[{Date}] Попытка регистрации: пользователь {login} -- неудача");
                        Console.WriteLine("\n");
                        break;
                    }

                    // Логирование успешной регистрации
                    log.Add($"[{Date}] Попытка регистрации: пользователь {login} -- успех");

                    // Добавление нового логина и пароля в соответствующие списки
                    logins.Add(login);
                    passwords.Add(password);

                    Console.WriteLine("\n");
                    break;

                case "2":
                    // Вход в систему (логин)
                    Console.Write("Логин: ");
                    login = Console.ReadLine();

                    Console.Write("Пароль: ");
                    password = Console.ReadLine();

                    // Получение текущей даты и времени
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                    // Проверка, существует ли введённый логин
                    if (logins.Contains(login))
                    {
                        // Получение индекса логина в списке
                        int index = logins.IndexOf(login);
                        if (index != -1)
                        {
                            // Проверка совпадения введённого пароля с сохранённым
                            if (passwords[index] == password)
                            {
                                // Логирование успешного входа
                                log.Add($"[{Date}] Попытка входа: пользователь {login} -- успех");
                                Console.WriteLine("\n");
                                break;
                            }
                        }

                        // Логирование неудачного входа
                        log.Add($"[{Date}] Попытка входа: пользователь {login} -- неудача");
                        Console.WriteLine("\n");
                        break;
                    }

                    // Логирование неудачного входа для несуществующего логина
                    log.Add($"[{Date}] Попытка входа: пользователь {login} -- неудача");
                    Console.WriteLine("\n");
                    break;

                case "3":
                    // Вывод всех записей из лога событий
                    foreach (string item in log)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine("\n");
                    break;
            }
        }
    }
}