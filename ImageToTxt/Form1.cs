using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ImageToTxt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //Считывание изображения
            Bitmap image = new Bitmap(@"D:\work\ImageToTxt\ImageToTxt\Resources\1.png");
            //Bitmap image = new Bitmap(@"D:\work\ImageToTxt\PNG2.png");
            
            //Создается массив, в который будет записан конвертируемый файл
            char[,] image_array_of_symbols = new char[image.Width, image.Height];

            /*Основная часть программы, которая проходит по длине и высоте картинки 
             * и получает цвет пикселя, что позволяет в зависимости от цвета пикселя
             * указать каким символом он будем заменем для записи в файл
            */
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {             
                    Color pixelColor = Color.FromArgb(image.GetPixel(x, y).ToArgb());

                    if (Math.Abs(pixelColor.A - pixelColor.B) < 10 && Math.Abs(pixelColor.A - pixelColor.G) < 10 && Math.Abs(pixelColor.B - pixelColor.G) < 10 && pixelColor.A > 200)
                    {
                        image_array_of_symbols[x, y] = ' ';
                    }
                    else if (Math.Abs(pixelColor.A - pixelColor.B) < 10 && Math.Abs(pixelColor.A - pixelColor.G) < 10 && Math.Abs(pixelColor.B - pixelColor.G) < 10 && pixelColor.A < 100)
                    {
                        image_array_of_symbols[x, y] = '.';
                    }
                    else
                    {
                        if (pixelColor.A - pixelColor.B > 70 && pixelColor.A - pixelColor.G > 70)
                        {
                            image_array_of_symbols[x, y] = 'a';
                        }
                        else if (pixelColor.B - pixelColor.A > 70 && pixelColor.B - pixelColor.G > 70)
                        {
                            image_array_of_symbols[x, y] = 'y';
                        }
                        else
                        {
                            image_array_of_symbols[x, y] = 'x';
                        }
                    }
                }
            }
            //Вывод массива в текстовый файл, в котором можно просмотреть полученый результат
            using (StreamWriter outputFile = new StreamWriter("Output.txt"))
            {                
                    for (int i = 0; i < image.Height; i++)
                    {
                        for (int j = 0; j < image.Width; j++)
                        {
                            outputFile.Write(image_array_of_symbols[j, i]);
                        }
                    outputFile.Write("\n");
                    }
            }
            
            Application.Exit();
        }

        
    }
}
