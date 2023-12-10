using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LabsLibrary
{
    struct Segment
    {
        public int Begin;
        public int End;
        public int Price;
    }

    public class Lab2
    {
        private string inputFilename;
        private string outputFilename;

        public Lab2(string inputFilename, string outputFilename)
        {
            this.inputFilename = inputFilename;
            this.outputFilename = outputFilename;
        }

        public static string Run(string inputFilename, string outputFilename)
        {
            try
            {
                StringBuilder result = new StringBuilder();

                using (StreamReader reader = new StreamReader(inputFilename))
                {
                    int n = int.Parse(reader.ReadLine());
                    int b, e;
                    string[] startEnd = reader.ReadLine().Split();
                    b = int.Parse(startEnd[0]);
                    e = int.Parse(startEnd[1]);

                    List<Segment> segments = new List<Segment>();
                    for (int i = 0; i < n; i++)
                    {
                        string[] line = reader.ReadLine().Split();
                        int bi = int.Parse(line[0]);
                        int ei = int.Parse(line[1]);
                        int si = int.Parse(line[2]);
                        if (ei > b && bi < e)
                        {
                            segments.Add(new Segment { Begin = Math.Max(bi, b), End = Math.Min(ei, e), Price = si });
                        }
                    }
                    n = segments.Count;
                    segments = segments.OrderBy(seg => seg.End).ToList();
                    List<int> es = segments.Select(seg => seg.End).ToList();
                    List<int> min = Enumerable.Repeat(INF, 2 * n).ToList();
                    int ans = INF;

                    for (int i = 0; i < n; i++)
                    {
                        if (segments[i].Begin == b)
                        {
                            SetValue(min, i, segments[i].Price);
                            if (segments[i].End == e)
                            {
                                ans = Math.Min(ans, segments[i].Price);
                            }
                        }
                        else
                        {
                            int from = Array.BinarySearch(es.Take(i).ToArray(), segments[i].Begin);
                            from = (from < 0) ? ~from : from;

                            if (from == i)
                            {
                                continue;
                            }

                            int minBefore = GetRangeMin(min, from, i - 1);

                            if (minBefore == INF)
                            {
                                continue;
                            }

                            SetValue(min, i, minBefore + segments[i].Price);

                            if (segments[i].End == e)
                            {
                                ans = Math.Min(ans, minBefore + segments[i].Price);
                            }
                        }
                    }

                    if (ans < INF)
                    {
                        // Додавання результату до StringBuilder
                        result.AppendLine($"Result: {ans}");

                        // Запис результату в файл
                        using (StreamWriter writer = new StreamWriter(outputFilename))
                        {
                            writer.Write(ans);
                        }
                    }
                }

                // Повернення результату як рядок
                return result.ToString();
            }
            catch (Exception ex)
            {
                // Повернення повідомлення про помилку як рядок
                return $"Error: {ex.Message}";
            }
        }

        private static int INF = 2000000001;

        private static void SetValue(List<int> min, int index, int value)
        {
            index += min.Count >> 1;
            min[index] = value;
            while (index > 0)
            {
                index >>= 1;
                min[index] = Math.Min(min[index * 2], min[index * 2 + 1]);
            }
        }

        private static int GetRangeMin(List<int> min, int left, int right)
        {
            int answer = INF;
            left += min.Count >> 1;
            right += min.Count >> 1;
            while (left <= right)
            {
                if ((left & 1) == 1)
                {
                    answer = Math.Min(answer, min[left]);
                    left++;
                }
                left >>= 1;
                if ((right & 1) == 0)
                {
                    answer = Math.Min(answer, min[right]);
                    right--;
                }
                right >>= 1;
            }
            return answer;
        }
    }
}
