using System;

namespace _2._8.GAME
{
    public class Field
    {
        private int width;
        private int height;

        public Field(int width, int height)
        {
            if (width < 0)
            {
                throw new Exception("Ширина поля не может быть меньше нуля!");
            }

            if (height < 0)
            {
                throw new Exception("Высота поля не может быть меньше нуля!");
            }

            this.width = width;
            this.height = height;
        }
    }
}
