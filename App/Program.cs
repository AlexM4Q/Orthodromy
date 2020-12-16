using System;

namespace App
{
    internal static class Program
    {
        private const double L = 111.1;

        private static int Main()
        {
            var line = Console.ReadLine();
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("No input");
                return 1;
            }

            line = line.Replace('.', ',');

            var parts = line.Split();
            if (parts.Length != 4)
            {
                Console.WriteLine("Numbers count < 4");
                return 1;
            }

            if (!(double.TryParse(parts[0], out var long1)
                  && double.TryParse(parts[1], out var lat1)
                  && double.TryParse(parts[2], out var long2)
                  && double.TryParse(parts[3], out var lat2)
                ))
            {
                Console.WriteLine("Any of numbers has incorrect format");
                return 1;
            }

            var result = Calculate(
                ToRadians(long1),
                ToRadians(lat1),
                ToRadians(long2),
                ToRadians(lat2)
            );

            Console.WriteLine($"Distance: {result} km");
            Console.WriteLine("Press any button");
            Console.ReadKey();
            return 0;
        }

        private static double ToRadians(this double angle) => angle * Math.PI / 180;
        private static double ToDegrees(this double angle) => angle * 180 / Math.PI;

        private static double Calculate(double l1, double f1, double l2, double f2)
        {
            var dl = l2 - l1;
            var dlCos = Math.Cos(dl);
            var dlSin = Math.Sin(dl);
            var f1Cos = Math.Cos(f1);
            var f2Cos = Math.Cos(f2);
            var f1Sin = Math.Sin(f1);
            var f2Sin = Math.Sin(f2);

            var part1 = f2Cos * dlSin;
            var part2 = f1Cos * f2Sin - f1Sin * f2Cos * dlCos;
            var part3 = Math.Sqrt(part1 * part1 + part2 * part2);
            var part4 = f1Sin * f2Sin + f1Cos * f2Cos * dlCos;

            return L * Math.Atan2(part3, part4).ToDegrees();
        }
    }
}
