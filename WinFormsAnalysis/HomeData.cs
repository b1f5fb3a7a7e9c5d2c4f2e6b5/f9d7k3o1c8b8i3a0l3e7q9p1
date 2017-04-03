using System;
using System.Linq;
using System.Collections.Generic;

namespace WinFormsMappingDecoder
{
    internal class HomeData
    {
        private Dictionary<string, double> Data { get; set; } = new Dictionary<string, double>();

        internal string GetData()
        {
            if (Data == null) throw new Exception("Необходимо выполнить метод [SetData(string text)]");
            return GetAnalysis();
        }

        public void Analysis_1(string text)
        {
            Data.Clear();
            foreach (var number in "0123456789")
            {
                Data.Add(number.ToString(), 0);
            }

            foreach (var symbol in text)
            {
                if (Data.ContainsKey(symbol.ToString()))
                    Data[symbol.ToString()] = Data[symbol.ToString()] + 1;
            }

            //var sum = Data.Values.Sum();
            //for (var i = 0; i < Data.Count; i++)
            //{
            //    Data[Data.Keys.ToArray()[i]] =
            //        Data[Data.Keys.ToArray()[i]] * 10000 / sum;
            //}

            //Data = Data.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public void Analysis_2(string sentence)
        {
            Data.Clear();

            var text = sentence.Replace("\n", "");

            for (var i = 0; i < 10; i++)
            {
                var subText = text.Substring(0, i + 1);
                Data[i.ToString()] = (text.Length - text.Replace(subText, "").Length) / subText.Length;
            }
        }

        public void Analysis_3(string sentence)
        {
            Data.Clear();

            var text = sentence.Split('\n');

            for (var i = 0; i < 10; i++)
            {
                var subText = text[i];
                Data[subText] = text.Count(s => s == subText);
            }

            //var sum = Data.Values.Sum();
            //for (var i = 0; i < Data.Count; i++)
            //{
            //    Data[Data.Keys.ToArray()[i]] =
            //        Data[Data.Keys.ToArray()[i]] * 1000 / sum;
            //}

            //Data = Data.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public void Analysis_4(string sentence)
        {
            Data.Clear();

            var text = sentence.Split('\n');

            var n = text.Length;
            const int count = 10; // Для скольких первых чисел делаем проверку корреляции

            // Находим математическое ожидание и дисперсию
            double sumM = 0;
            double sumD = 0;

            for (var i = 0; i < n; i++)
            {
                sumM += int.Parse(text[i]);
                sumD += Math.Pow(int.Parse(text[i]), 2);
            }

            var mx = sumM / n; //Математическое ожидание
            var dx = (sumD / n) - Math.Pow(mx, 2); //Дисперсия

            for (var j = 0; j < count; j++)
            {
                double numerator = 0;
                double leftDenominator = 0;
                double rightDenominator = 0;
                for (var i = 1; i < (n - count); i++)
                {
                    numerator += (int.Parse(text[i]) - mx) * (int.Parse(text[i + j]) - mx);
                    leftDenominator += Math.Pow(int.Parse(text[i]) - mx, 2);
                    rightDenominator += Math.Pow(int.Parse(text[i + j]) - mx, 2);
                }
                Data[j.ToString()] = Math.Round(numerator / Math.Sqrt(leftDenominator * rightDenominator), 4);
            }

            //var sum = Data.Values.Sum();
            //for (var i = 0; i < Data.Count; i++)
            //{
            //    Data[Data.Keys.ToArray()[i]] =
            //        Data[Data.Keys.ToArray()[i]] * 1000 / sum;
            //}

            //Data = Data.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        private string GetAnalysis()
        {
            return Data.Aggregate("------\n", (current, d) => current + $"key: {d.Key}\t - value: {d.Value}\n");
        }
    }
}