using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CallOfDuty
{
    public class MainMenu
    {
        private void GetVictims(int index, SelectDuty todayDuty)
        {
            foreach (var student in todayDuty.Students)
                Console.WriteLine($"#{index++} {student.Name} {student.Info}");
        }
        public void SelectVictims(int index, SelectDuty todayDuty)
        {
            while (todayDuty.CountApproved < 2)
            {
                GetVictims(index, todayDuty);
                Console.WriteLine("Укажите индекс студента и через пробел знак + или - для подтверждения или отмены участия студента в святом дежурстве");

                var answer = Console.ReadLine();
                var cols = answer.Split();
                if (cols.Length != 2)
                    continue;
                if (!int.TryParse(cols[0], out index))
                {
                    Console.WriteLine("Неверно указан индекс студента. Укажите число первым в строке");
                    continue;
                }
                string action = cols[1];
                if (action != "+" && action != "-")
                {
                    Console.WriteLine("Действие должно обозначаться как + или -");
                    continue;
                }
                index--;
                if (cols.Length > index && index >= 0)
                {
                    if (action == "+")
                        todayDuty.Approve(todayDuty.Students[index]);
                    else
                        todayDuty.RejectAndGetAnotherStudent(todayDuty.Students[index]);
                }
            }
            SaveVictims(todayDuty);
        }
        private void SaveVictims(SelectDuty todayDuty)
        {
            todayDuty.Save();
            Console.WriteLine("Дежурные сегодня:");
            foreach (var student in todayDuty.Students)
                Console.WriteLine($"{student.Name} {student.Info}");
        }
        public void AddVictim()
        {
            bool Add = false;
            while (!Add)
            {
                try
                {
                    Console.WriteLine("Впишите имя студента");
                    string name = Console.ReadLine();
                    Console.WriteLine("Впишите фамилию студента");
                    string surName = Console.ReadLine();

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surName))
                    {
                        string newStudent = $"{name};{surName};";
                        using (StreamWriter writer = new StreamWriter("C:\\Users\\User\\source\\repos\\CallOfDutyHelp\\CallOfDuty\\Students.txt", true))
                        {
                            writer.WriteLine(newStudent);
                        }

                        Console.WriteLine("Студент добавлен");
                        Add = true;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода данных");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении студента: {ex.Message}");
                }
            }
        }
        public void DeleteVictim()
        {
            try
            {
                string[] readText = File.ReadAllLines("C:\\Users\\User\\source\\repos\\CallOfDutyHelp\\CallOfDuty\\Students.txt");
                int num = 1;
                Console.Clear();
                foreach (string line in readText)
                {
                    Console.WriteLine($"{num} {line}");
                    num++;
                }
                Console.WriteLine("Выберите номер студента, которого необходимо удалить");
                int indexToRemove = int.Parse(Console.ReadLine()) - 1;

                string[] newLines = new string[readText.Length - 1];
                for (int i = 0, j = 0; i < readText.Length; i++)
                {
                    if (i == indexToRemove)
                    {
                        continue;
                    }
                    newLines[j++] = readText[i];
                }

                File.WriteAllLines("C:\\Users\\User\\source\\repos\\CallOfDutyHelp\\CallOfDuty\\Students.txt", newLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении студента: {ex.Message}");
            }
        }
    }
}
