using System;

namespace _2._8.GAME
{
    public class Field
    {
        private int width;
        private int height;

        private Field(int fieldWidth, int fieldHeight)
        {
            if (fieldWidth < 0)
            {
                throw new Exception("Ширина поля не может быть меньше нуля!");
            }

            if (fieldHeight < 0)
            {
                throw new Exception("Высота поля не может быть меньше нуля!");
            }

            width = fieldWidth;
            height = fieldHeight;
        }
    }
}
