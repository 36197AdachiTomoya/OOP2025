﻿namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var numbers = new List<int> { 12, 87, 94, 14, 53, 20, 40, 35, 76, 91, 31, 17, 48 };

            // 3.1.1
            Exercise1(numbers);
            Console.WriteLine("-----");

            // 3.1.2
            Exercise2(numbers);
            Console.WriteLine("-----");

            // 3.1.3
            Exercise3(numbers);
            Console.WriteLine("-----");

            // 3.1.4
            Exercise4(numbers);
        }

        private static void Exercise1(List<int> numbers) {
            Console.WriteLine(numbers.Exists(n => n % 8 == 0
            || n % 9 == 0) ? "存在します" : "存在しません");
            //var exists = numbers.Exists(n => n % 8 == 0 || n % 9 == 0);
            //if(exists) {
            //    Console.WriteLine("存在します");
            //} else {
            //    Console.WriteLine("存在しません");
            //}
        }
        private static void Exercise2(List<int> numbers) {
            numbers.ForEach(s => Console.WriteLine(s / 2.0));
        }

        private static void Exercise3(List<int> numbers) {
            numbers.Where(s => s >= 50).ToList().ForEach(s => Console.WriteLine(s));
            //var num = numbers.Where(s => s >= 50);
            //foreach (var s in num) {
            //    Console.WriteLine(s);
            //}
        }

        private static void Exercise4(List<int> numbers) {
            numbers.Select(s => s * 2).ToList().ForEach(s => Console.WriteLine(s));
            //var num = numbers.Select(s => s * 2).ToList();
            //foreach (var s in num) {
            //    Console.WriteLine(s);
            //}
        }
    }
}
