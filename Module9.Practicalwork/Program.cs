using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module9.Practicalwork
{
        abstract class Storage
        {
            protected string name;
            protected string model;

            public Storage(string name, string model)
            {
                this.name = name;
                this.model = model;
            }

            // Абстрактные методы
            public abstract double GetMemory();
            public abstract void CopyData(double dataSize);
            public abstract double GetFreeSpace();
            public abstract void DisplayInfo();
        }

        // Класс "Flash(память)", производный от "Носитель информации"
        class Flash : Storage
        {
            private double usbSpeed;
            private double memorySize;

            public Flash(string name, string model, double usbSpeed, double memorySize)
                : base(name, model)
            {
                this.usbSpeed = usbSpeed;
                this.memorySize = memorySize;
            }

            public override double GetMemory()
            {
                return memorySize;
            }

            public override void CopyData(double dataSize)
            {
                Console.WriteLine($"Copying data to Flash. Speed: {usbSpeed} MB/s");
            }

            public override double GetFreeSpace()
            {
                return memorySize;
            }

            public override void DisplayInfo()
            {
                Console.WriteLine($"Flash: {name}, Model: {model}, USB Speed: {usbSpeed} MB/s, Memory: {memorySize} GB");
            }
        }

        // Класс "DVD(диск)", производный от "Носитель информации"
        class DVD : Storage
        {
            private double readWriteSpeed;
            private string type;

            public DVD(string name, string model, double readWriteSpeed, string type)
                : base(name, model)
            {
                this.readWriteSpeed = readWriteSpeed;
                this.type = type;
            }

            public override double GetMemory()
            {
                return (type == "single-sided") ? 4.7 : 9;
            }

            public override void CopyData(double dataSize)
            {
                Console.WriteLine($"Copying data to DVD. Speed: {readWriteSpeed} MB/s");
            }

            public override double GetFreeSpace()
            {

                return GetMemory();
            }

            public override void DisplayInfo()
            {
                Console.WriteLine($"DVD: {name}, Model: {model}, Read/Write Speed: {readWriteSpeed} MB/s, Type: {type}");
            }
        }

        // Класс "HDD", производный от "Носитель информации"
        class HDD : Storage
        {
            private double usbSpeed;
            private int partitions;
            private double partitionSize;

            public HDD(string name, string model, double usbSpeed, int partitions, double partitionSize)
                : base(name, model)
            {
                this.usbSpeed = usbSpeed;
                this.partitions = partitions;
                this.partitionSize = partitionSize;
            }

            public override double GetMemory()
            {
                return partitions * partitionSize;
            }

            public override void CopyData(double dataSize)
            {
                Console.WriteLine($"Copying data to HDD. Speed: {usbSpeed} MB/s");
            }

            public override double GetFreeSpace()
            {
                return GetMemory();
            }

            public override void DisplayInfo()
            {
                Console.WriteLine($"HDD: {name}, Model: {model}, USB Speed: {usbSpeed} MB/s, Partitions: {partitions}, Partition Size: {partitionSize} GB");
            }
        }
        class Program
        {
            static void Main()
            {
                // Создание объектов соответствующих классов
                Storage[] devices = new Storage[]
                {
            new Flash("Flash1", "Kingston", 150, 64),
            new DVD("DVD1", "Sony", 16, "single-sided"),
            new HDD("HDD1", "Seagate", 120, 2, 500)
                };

                // Расчет общего количества памяти всех устройств
                double totalMemory = CalculateTotalMemory(devices);
                Console.WriteLine($"Total Memory: {totalMemory} GB");

                // Копирование информации на устройства
                double dataSize = 565; // Размер данных для копирования в ГБ
                CopyDataToDevices(devices, dataSize);

                // Расчет времени необходимого для копирования
                double copyTime = CalculateCopyTime(devices, dataSize);
                Console.WriteLine($"Time required to copy data: {copyTime} seconds");

                // Расчет необходимого количества носителей информации для переноса информации
                int requiredDevices = CalculateRequiredDevices(devices, dataSize);
                Console.WriteLine($"Required number of devices: {requiredDevices}");
            }

            static double CalculateTotalMemory(Storage[] devices)
            {
                double totalMemory = 0;
                foreach (var device in devices)
                {
                    totalMemory += device.GetMemory();
                }
                return totalMemory;
            }

            static void CopyDataToDevices(Storage[] devices, double dataSize)
            {
                foreach (var device in devices)
                {
                    device.CopyData(dataSize);
                }
            }

            static double CalculateCopyTime(Storage[] devices, double dataSize)
            {
                double totalSpeed = 0;
                foreach (var device in devices)
                {
                    totalSpeed += device.GetFreeSpace() / device.GetMemory();
                }
                return dataSize / totalSpeed;
            }

            static int CalculateRequiredDevices(Storage[] devices, double dataSize)
            {
                int requiredDevices = 0;
                double remainingData = dataSize;
                foreach (var device in devices)
                {
                    double deviceMemory = device.GetMemory();
                    if (remainingData > deviceMemory)
                    {
                        requiredDevices++;
                        remainingData -= deviceMemory;
                    }
                    else
                    {
                        requiredDevices++;
                        break;
                    }
                }
                return requiredDevices;
            }
        }
    

}
