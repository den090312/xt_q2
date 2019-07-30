using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sp1 = new SPoint();
            sp1.x = 5;
            sp1.y = 6;

            //создание независимой копии элемента
            SPoint sp2 = sp1;

            sp1.x = 7;

            Console.WriteLine(sp2.x); //5

            var cp1 = new CPoint();
            cp1.x = 5;
            cp1.y = 6;

            //ссылка указывает на два объекта
            //меняем один - меняется и второй
            CPoint cp2 = cp1;

            cp2.x = 7;

            Console.WriteLine(cp2.x); //7

            //преобразование базовых типов
            byte a = 4;
            byte b = (byte)(a + 70);
        }

        struct SPoint
        {
            public int x;
            public int y;
        }

        class CPoint
        {
            public int x;
            public int y;
        }
    }
}
