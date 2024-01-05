
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public class Parallelism
{
    static int ParallelReadFiles(string folderPath)
    {
        int totalNumberSpaces = 0;
        String[] files = Directory.GetFiles(folderPath);
        Parallel.For(0, files.Length,
                     index => {
                         FileInfo fi = new FileInfo(files[index]);
                         int numberSpace = GetFileWhitespaceCount(fi.ToString(), ' ');
                         Interlocked.Add(ref totalNumberSpaces, numberSpace);
                     });
        return totalNumberSpaces;
    }
    public static void Main(string[] args)
    {
        
        string folderPath = @"C:\Users\123\source\repos\ReadFiles\";
        Stopwatch stopWatch = new Stopwatch();

        stopWatch.Start();
        int resultTotalNumberSpaces = ParallelReadFiles(folderPath);
        stopWatch.Stop();

        Console.WriteLine("Количество пробелов в файлах в папке {0} равно {1}", folderPath, resultTotalNumberSpaces);
        Console.WriteLine("Время выполнения = {0}, мс", stopWatch.ElapsedMilliseconds);
    }
    static int GetFileWhitespaceCount(string path, char ch)
    {
        return File.ReadAllText(path).Count(c => c == ch);
    }
}


