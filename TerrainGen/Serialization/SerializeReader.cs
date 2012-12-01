using System;

namespace TerrainGen.Serialization
{
    public abstract class SerializeReader : ISerializer
    {
        protected readonly dynamic Reader;

        /// <summary>
        /// �������������� ����� ������������� <see
        /// cref="T:TerrainGen.Serialization.SerializeReader"/>
        /// </summary>
        /// <remarks>
        /// �� ���� <b>Reader</b> ��������, � �������, <see
        /// cref="T:System.IO.BinaryReader"/>���� <see cref="T:System.IO.StreamReader"/>.
        /// ������ �� ������, ���������� ���������� ������ ������� ������ �� ����������
        /// ������ ���������������, ������� ������� ����������� ���
        /// </remarks>
        /// <param name="reader">����� �������, ����������� ������ ������</param>
        protected SerializeReader(dynamic reader)
        {
            Reader = reader;
        }

        public abstract bool Open();
        public abstract bool Close();

        /// <summary>
        /// ������������ ����� ������ ��������� ���������������
        /// </summary>
        /// <param name="str">������, �������� � ������� ����� �������� ��� ��������������</param>
        /// <returns>
        /// <para><b>true</b>, ���� ������� �������� ������, �
        /// <b>false</b> � ��������� ������</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref string str);

        /// <summary>
        /// ������������ ����� ������� ���� Int32 ��������� ���������������
        /// </summary>
        /// <param name="i">����� �������������� ����, �������� � ������� �����
        /// �������� ��� ��������������</param>
        /// <returns>
        /// <para><b>true</b>, ���� ������� ��������� � ��������������� �������� ����� ��� �������, ������� ����� �������� � ����� �������������� ����, �
        /// <b>false</b> � ��������� ������</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref int i);

        /// <summary>
        /// ������������ ����� ������� ���� Double ��������� ���������������
        /// </summary>
        /// <param name="d">����� � ��������� ������ ������� ��������, �������� � ������� ����� 
        /// �������� ��� ��������������</param>
        /// <returns>
        /// <para><b>true</b>, ���� ������� ��������� � ��������������� �������� ����� ��� �������, ������� ����� �������� � ����� ���� Double, �
        /// <b>false</b> � ��������� ������</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref double d);

        /// <summary>
        /// ������������ ����� ������� ���� Single ��������� ���������������
        /// </summary>
        /// <param name="f">����� � ��������� ������, �������� � ������� �����
        /// �������� ��� ��������������</param>
        /// <returns>
        /// <para><b>true</b>, ���� ������� ��������� � ��������������� �������� ����� ��� �������, ������� ����� �������� � ����� ���� Single, �
        /// <b>false</b> � ��������� ������</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref float f);

        /// <summary>
        /// ������������ ����� ������ �������� ��������� ���������������
        /// </summary>
        /// <param name="b">���������� ���� bool, �������� ������� �����
        /// �������� ��� ��������������</param>
        /// <returns>
        /// <para><b>true</b>, ���� ������� ��������� � ��������������� �������� ������ ��������, ���� ������, �������� ������� ����� ��������������� ��� ������ ��������, �
        /// <b>false</b> � ��������� ������</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(TerrainGen.Serialization.SpecialChars)"/>
        public abstract bool InOut(ref bool b);

        /// <summary>
        /// ������������ ����� ����������� �������� ��������� ���������������
        /// </summary>
        /// <param name="ch">������, ���������� ��������� ������������ <see cref="SpecialChars"/>, �������� � ������� ����� 
        /// �������� ��� ��������������</param>
        /// <returns>
        /// <para><b>true</b>, ���� ������� ��������� � ��������������� �������� ��������� (� �� �����) ������, �
        /// <b>false</b> � ��������� ������</para>
        /// </returns>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)"/>
        /// <seealso cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)"/>
        public bool InOut(SpecialChars ch)
        {
            return InOut(ch, true);
        }

        /// <summary>
        /// ������������ ����� ����������� �������� ���������
        /// ���������������
        /// </summary>
        /// <param name="ch">������, ���������� ��������� ������������ <see
        /// cref="SpecialChars"/>, �������� � ������� ����� 
        /// �������� ��� ��������������</param>
        /// <param name="delete">���� <b>true </b>(�� ���������), �� ������� ����� ������
        /// ����� ������. ����� �� ��� ��������� ��������� �� ������ � ���������� �������
        /// ������ ����� ����� ��� �� ����� �������</param>
        /// <returns>
        /// <para><b>true</b>, ���� ������� ��������� � ���������������
        /// �������� ��������� (� �� �����) ������, � <b>false</b> � ��������� ������</para>
        /// </returns>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Boolean@)">Boolean@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Double@)">Double@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Single@)">Single@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.String@)">String@)</seealso>
        /// <seealso
        /// cref="M:TerrainGen.Serialization.ISerializer.InOut(System.Int32@)">Int32@)</seealso>
        public abstract bool InOut(SpecialChars ch, bool delete = true);
        /// <summary>
        /// ���������� ������� ����������� ������
        /// </summary>
        public abstract SpecialChars TestCurrentSpecialChar();
    }
}