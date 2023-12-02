using System;

namespace CarApp
{
    public class Car
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Number { get; private set; }
        public string Color { get; private set; }

        private bool isEngineRunning;
        private int speed;
        private int gear;

        public Car(string brand, string model, string number, string color)
        {
            Brand = brand;
            Model = model;
            Number = number;
            Color = color;
            isEngineRunning = false;
            speed = 0;
            gear = 0;
        }

        public string StartEngine()
        {
            if (gear == 0 || gear == 1)
            {
                isEngineRunning = true;
                return $"Машина {Brand} {Model} завелась.";
            }
            else
            {
                return "Невозможно завести машину: неподходящая передача.";
            }
        }

        public string StopEngine()
        {
            isEngineRunning = false;
            return $"Машина {Brand} {Model} заглушена.";
        }

        public string Accelerate()
        {
            if (isEngineRunning && gear > 0)
            {
                speed += 10;
                return $"Машина {Brand} {Model} ускорилась до {speed} км/ч.";
            }
            else
            {
                return "Невозможно ускориться: машина заглушена или на нейтральной передаче.";
            }
        }

        public string Brake()
        {
            if (speed > 0)
            {
                speed -= 10;
                return $"Текущая скорость {Brand} {Model}: {speed} км/ч.";
            }
            else
            {
                return $"Машина {Brand} {Model} уже остановлена.";
            }
        }

        public string ChangeGear(int newGear)
        {
            if ((newGear == 0 || IsSpeedValidForGear(newGear)) && isEngineRunning)
            {
                gear = newGear;
                return $"Передача изменена на {newGear}.";
            }
            else
            {
                StopEngine();
                return "Неверная скорость для выбранной передачи: машина заглохла.";
            }
        }

        private bool IsSpeedValidForGear(int newGear)
        {
            switch (newGear)
            {
                case 1: return speed >= 0 && speed <= 30;
                case 2: return speed >= 20 && speed <= 50;
                case 3: return speed >= 40 && speed <= 70;
                case 4: return speed >= 60 && speed <= 90;
                case 5: return speed >= 80 && speed <= 120;
                default: return false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car[] cars = new Car[]
            {
                new Car("Toyota", "300", "A123BC", "Green"),
                new Car("Mercedes", "Benz", "B234CD", "Blue"),
                new Car("BMW", "X5", "C345DE", "Black")
            };

            Console.WriteLine("Выберите машину:");
            for (int i = 0; i < cars.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {cars[i].Brand} {cars[i].Model}");
            }

            int choice = Convert.ToInt32(Console.ReadLine()) - 1;
            Car selectedCar = cars[choice];

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Завести машину");
                Console.WriteLine("2. Заглушить машину");
                Console.WriteLine("3. Газануть");
                Console.WriteLine("4. Притормозить");
                Console.WriteLine("5. Переключить передачу");
                Console.WriteLine("6. Выход");

                int action = Convert.ToInt32(Console.ReadLine());
                string result = "";

                switch (action)
                {
                    case 1:
                        result = selectedCar.StartEngine();
                        break;
                    case 2:
                        result = selectedCar.StopEngine();
                        break;
                    case 3:
                        result = selectedCar.Accelerate();
                        break;
                    case 4:
                        result = selectedCar.Brake();
                        break;
                    case 5:
                        Console.WriteLine("Введите номер передачи:");
                        int gear = Convert.ToInt32(Console.ReadLine());
                        result = selectedCar.ChangeGear(gear);
                        break;
                    case 6:
                        return;
                    default:
                        result = "Неизвестное действие.";
                        break;
                }

                Console.WriteLine(result);
            }
        }
    }
}
