using System;
using System.Collections.Generic;

namespace lab1SisProg
{
    public class Pass
    {
        public string errorText = "";
        public string nameProg;
        public int startAddress = 0;
        public int endAddress = 0;
        public int countAddress = 0;
        public const int memmoryMax = 33554432;


        public List<List<string>> supportTable = new List<List<string>>();
        public List<List<string>> symbolTable = new List<List<string>>();
        public List<List<string>> exitTable = new List<List<string>>();
        public List<string> endSection = new List<string>();


        public int FindMark(string mark)
        {
            for (int i = 0; i < symbolTable[0].Count; i++)
                if (mark == symbolTable[0][i])
                    return i;
            return -1;
        }

        public void AddToBinary(string mark, string OC, string OP1, string OP2)
        {
            exitTable[0].Add(mark);
            exitTable[1].Add(OC);
            exitTable[2].Add(OP1);
            exitTable[3].Add(OP2);
        }

        public void AddToSymbolTable(string OP1, string OP2, string nameProg, string str)
        {
            symbolTable[0].Add(OP1);
            symbolTable[1].Add(OP2);
            symbolTable[2].Add(nameProg);
            symbolTable[3].Add(str);
        }

        public bool CheckMemmory()
        {
            if (countAddress < 0 || countAddress > memmoryMax)
            {
                errorText = $"Ошибка. Выход за пределы доступной памяти";
                return false;
            }
            return true;
        }

        public int FindCode(string mark, string[,] operationCode)
        {
            for (int i = 0; i < operationCode.GetLength(0); i++)
            {
                if (Convert.ToString(mark).ToUpper() == operationCode[i, 0])
                    return i;
            }
            return -1;
        }

        public int FindMarkInMarkTable(string mark, ref string addressName, ref string addressTune)
        {
            if (symbolTable.Count > 0)
            {
                for (int i = 0; i < symbolTable[0].Count; i++)
                {
                    if (symbolTable[0][i].ToUpper() == mark.ToUpper())
                    {
                        addressName = symbolTable[1][i];
                        addressTune = symbolTable[2][i];
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
