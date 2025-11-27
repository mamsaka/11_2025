using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CheckListCreator
{
    // Конвертер цвета прогресса
    public class ProgressColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int progress)
            {
                return progress switch
                {
                    100 => new SolidColorBrush(Colors.Green),
                    >= 75 => new SolidColorBrush(Colors.DarkGreen),
                    >= 50 => new SolidColorBrush(Colors.Orange),
                    >= 25 => new SolidColorBrush(Colors.OrangeRed),
                    _ => new SolidColorBrush(Colors.Red)
                };
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Конвертер иконки статуса
    public class StatusIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int progress)
            {
                return progress switch
                {
                    100 => "✅",
                    0 => "❎",
                    _ => "⏳"
                };
            }
            return "❓";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Конвертер статистики задач
    public class TaskStatsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Checklist checklist && parameter is string type)
            {
                var completedTasks = checklist.Tasks.Count(t => t.CompleteFlag);
                return type == "completed" ? completedTasks : checklist.Tasks.Count;
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}