// Подключаем библиотеку System
using System;

// Главный класс приложения
class Program
{
    // Точка входа в программу
    static void Main()
    {
        // Просим выбрать режим работы — шифрование или расшифровка
        Console.Write("Выберите режим (1 — шифровать, 2 — расшифровать): ");
        int mode = int.Parse(Console.ReadLine());

        // Запрашиваем текст, который надо преобразовать
        Console.Write("Введите текст: ");
        string text = Console.ReadLine();

        // Запрашиваем величину сдвига (число)
        Console.Write("Сдвиг: ");
        int shift = int.Parse(Console.ReadLine());

        // Если выбрана расшифровка — меняем направление сдвига на обратное
        if (mode == 2)
            shift = -shift;

        // Переменная для результата
        string result = "";

        // Перебираем каждый символ введённой строки
        foreach (char c in text)
        {
            // Проверяем: если это буква английского алфавита
            if (char.IsLetter(c))
            {
                // Определяем, заглавная это буква или строчная
                char baseChar = char.IsUpper(c) ? 'A' : 'a';

                // Вычисляем позицию буквы в алфавите (0–25)
                int offset = (c - baseChar + shift) % 26;

                // Если значение отрицательное — корректируем, добавляя 26
                if (offset < 0)
                    offset += 26;

                // Добавляем в результат новую букву, сдвинутую на shift
                result += (char)(baseChar + offset);
            }
            else
            {
                // Все остальные символы (пробелы, цифры, знаки) не меняем
                result += c;
            }
        }

        // Выводим результат работы программы
        Console.WriteLine("Результат: " + result);
    }
}
