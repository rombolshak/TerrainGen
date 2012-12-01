using System;
using Tao.OpenGl;

namespace TerrainGen
{

    /// <summary>
    /// Содержит все необходимые методы для управления движением камеры
    /// </summary>
    /// <example>
    /// Следующий пример показывает принципы работы с камерой. 
    /// <code lang="C#"><![CDATA[
    /// var cam = new Camera();
    /// cam.PositionCamera(0, 100, 0, 10, 40, 30, 0, 1, 0); // установили камеру в
    /// начало координат на высоту 100 и направили в точку (10;30) на высоту 40
    /// cam.MoveCamera(10f); // передвинулись вперед по направлению взгляда на 10 единиц
    /// cam.MoveUpDown(-8f); // спустились вниз на 8 единиц
    /// cam.RotateView(15f); // повернулись на 15 единиц
    /// cam.Update(); // без этого вызова движение вбок не будет работать корректно
    /// cam.Strafe(20f); // сдвинулись вбок
    /// cam.Look(); //зафиксировали все изменения, теперь и только теперь они
    /// отобразятся на экране
    /// ]]></code>
    /// </example>
    internal class Camera
    {
        private Vector3D _mPos; //Вектор позиции камеры
        private Vector3D _mStrafe; //Вектор для стрейфа (движения влево и вправо) камеры.
        private Vector3D _mUp; //Вектор верхнего направления
        private Vector3D _mView; //Куда смотрит камера

        private static Vector3D Cross(Vector3D vV1, Vector3D vV2, Vector3D vVector2)
        {
            Vector3D vNormal;
            Vector3D vVector1;
            vVector1.X = vV1.X - vV2.X;
            vVector1.Y = vV1.Y - vV2.Y;
            vVector1.Z = vV1.Z - vV2.Z;

            // Если у нас есть 2 вектора (вектор взгляда и вертикальный вектор), 
            // у нас есть плоскость, от которой мы можем вычислить угол в 90 градусов.
            vNormal.X = ((vVector1.Y*vVector2.Z) - (vVector1.Z*vVector2.Y));

            vNormal.Y = ((vVector1.Z*vVector2.X) - (vVector1.X*vVector2.Z));

            vNormal.Z = ((vVector1.X*vVector2.Y) - (vVector1.Y*vVector2.X));

            // Итак, зачем всё это? Нам нужно найти ось, вокруг которой вращаться. Вращение камеры
            // влево и вправо простое - вертикальная ось всегда (0,1,0). 
            // Вращение камеры вверх и вниз отличается, так как оно происходит вне 
            // глобальных осей

            return vNormal;
        }

        private static float Magnitude(Vector3D vNormal)
        {
            return (float) Math.Sqrt((vNormal.X*vNormal.X) +
                                     (vNormal.Y*vNormal.Y) +
                                     (vNormal.Z*vNormal.Z));
        }

        private static Vector3D Normalize(Vector3D vVector)
        {
            var magnitude = Magnitude(vVector);
            vVector.X = vVector.X/magnitude;
            vVector.Y = vVector.Y/magnitude;
            vVector.Z = vVector.Z/magnitude;

            return vVector;
        }

        /// <summary>
        /// Устанавливает позицию камеры на заданном месте
        /// </summary>
        /// <remarks>
        /// Предполагается, что последние три параметры будут равны 0, 1, 0
        /// </remarks>
        /// <param name="posX">Позиция камеры по координате X</param>
        /// <param name="posY">Позиция камеры по координате Y (высота)</param>
        /// <param name="posZ">Позиция камеры по координате Z (глубина)</param>
        /// <param name="viewX">Координата Х точки взгляда камеры</param>
        /// <param name="viewY">Координата Y точки взгляда камеры (высота)</param>
        /// <param name="viewZ">Координата Z точки взгляда камеры (глубина)</param>
        /// <param name="upX">Х-компонента вектора высоты</param>
        /// <param name="upY">Y-компонента вектора высоты</param>
        /// <param name="upZ">Z-компонента вектора высоты</param>
        public void PositionCamera(float posX, float posY, float posZ,
                                   float viewX, float viewY, float viewZ,
                                   float upX, float upY, float upZ)
        {
            _mPos.X = posX; //Позиция камеры
            _mPos.Y = posY; //
            _mPos.Z = posZ; //
            _mView.X = viewX; //Куда смотрит, т.е. взгляд
            _mView.Y = viewY; //
            _mView.Z = viewZ; //
            _mUp.X = upX; //Вертикальный вектор камеры
            _mUp.Y = upY; //
            _mUp.Z = upZ; //
        }


        /// <summary>
        /// Поворачивает камеру с указанной скоростью
        /// </summary>
        /// <remarks>
        /// Поворот происходит в горизонтальной плоскости. Для поворота вверх/вниз
        /// используйте <see cref="M:TerrainGen.Camera.UpDown(System.Single)"/>
        /// </remarks>
        /// <param name="speed">Скорость, с которой происходит поворот</param>
        public void RotateView(float speed)
        {
            Vector3D vVector; // Полчим вектор взгляда
            vVector.X = _mView.X - _mPos.X;
            vVector.Y = _mView.Y - _mPos.Y;
            vVector.Z = _mView.Z - _mPos.Z;


            _mView.Z = (float) (_mPos.Z + Math.Sin(speed)*vVector.X + Math.Cos(speed)*vVector.Z);
            _mView.X = (float) (_mPos.X + Math.Cos(speed)*vVector.X - Math.Sin(speed)*vVector.Z);
        }

        /// <summary>
        /// Двигает камеру в направлении взгляда с указанной скоростью
        /// </summary>
        /// <param name="speed">Скорость, с которой происходит движение. Если значение
        /// отрицательно, происходит движение назад</param>
        public void MoveCamera(float speed)
        {
            Vector3D vVector; //Получаем вектор взгляда
            vVector.X = _mView.X - _mPos.X;
            vVector.Y = _mView.Y - _mPos.Y;
            vVector.Z = _mView.Z - _mPos.Z;

            vVector = Normalize(vVector);

            _mPos.X += vVector.X*speed;
            _mPos.Y += vVector.Y*speed;
            _mPos.Z += vVector.Z*speed;
            _mView.X += vVector.X*speed;
            _mView.Y += vVector.Y*speed;
            _mView.Z += vVector.Z*speed;
        }

        /// <summary>
        /// Двигает камеру вбок с указанной скоростью
        /// </summary>
        /// <param name="speed">Скорость движения камеры. Если значение положительно, то
        /// происходит движение вправо, иначе влево</param>
        public void Strafe(float speed)
        {
            // добавим вектор стрейфа к позиции
            _mPos.X += _mStrafe.X*speed;
            _mPos.Z += _mStrafe.Z*speed;

            // Добавим теперь к взгляду
            _mView.X += _mStrafe.X*speed;
            _mView.Z += _mStrafe.Z*speed;
        }

        /// <summary>
        /// Обновляет камеру. Необходимо вызывать этот метод каждый раз при отрисовке сцены
        /// </summary>
        public void Update()
        {
            var vCross = Cross(_mView, _mPos, _mUp);
            _mStrafe = Normalize(vCross);
        }


        /// <summary>
        /// Задает поворот камеры вверх/вниз
        /// </summary>
        /// <param name="speed">Скорость, с которой происходит вращение. При отрицательном
        /// значении поворот происходит вверх, иначе вниз</param>
        public void UpDown(float speed)
        {
            //mPos.y += speed;
            _mView.Y -= speed;
        }

        /// <summary>
        /// Двигает камеру с указанной скоростью в вертикальной плоскости
        /// </summary>
        /// <param name="speed">Скорость движения. Если положительна, движение происходит
        /// вверх, иначе вниз</param>
        public void MoveUpDown(float speed)
        {
            _mPos.Y += speed;
            _mView.Y += speed;
        }

        /// <summary>
        /// Применяет все ранее сделанные модификации положения камеры. До вызова этого метода никакой другой не возымеет эффекта
        /// </summary>
        public void Look()
        {
            Glu.gluLookAt(_mPos.X, _mPos.Y, _mPos.Z,
                          _mView.X, _mView.Y, _mView.Z,
                          _mUp.X, _mUp.Y, _mUp.Z);
        }

        /// <summary>
        /// Возвращает позицию камеры по Х
        /// </summary>
        public double GetPosX()
        {
            return _mPos.X;
        }

        /// <summary>
        /// Возвращает позицию камеры по Y
        /// </summary>
        public double GetPosY()
        {
            return _mPos.Y;
        }

        /// <summary>
        /// Возвращает позицию камеры по Z
        /// </summary>
        public double GetPosZ()
        {
            return _mPos.Z;
        }

        /// <summary>
        /// Возвращает позицию взгляда по Х
        /// </summary>
        public double GetViewX()
        {
            return _mView.X;
        }

        /// <summary>
        /// Возвращает позицию взгляда по Y
        /// </summary>
        public double GetViewY()
        {
            return _mView.Y;
        }

        /// <summary>
        /// Возвращает позицию взгляда по Z
        /// </summary>
        public double GetViewZ()
        {
            return _mView.Z;
        }

        #region Nested type: Vector3D

        private struct Vector3D
        {
            public float X, Y, Z;
        };

        #endregion
    }
}