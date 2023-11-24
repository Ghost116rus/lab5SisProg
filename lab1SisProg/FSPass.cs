using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lab1SisProg
{
    public class FSPass : Pass
    {
        readonly DataCheck dC = new DataCheck();

        public bool flagEnd;
        public bool flagStart;

        List<string> extNames = new List<string>();
        List<string> tuneTable = new List<string>();
        public FSPass()
        {
            symbolTable.Add(new List<string>());
            symbolTable.Add(new List<string>());
            symbolTable.Add(new List<string>());
            exitTable.Add(new List<string>());
            exitTable.Add(new List<string>());
            exitTable.Add(new List<string>());
            exitTable.Add(new List<string>());
        }

        //Проверка ТКО
        public bool CheckOperationCode(string[,] OCA)
        {
            int rows = OCA.GetLength(0);

            for (int i = 0; i < rows; i++)
            {

                if (OCA[i, 0] == "" || OCA[i, 1] == "" || OCA[i, 2] == "")
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустима пустая ячейка в ТКО";
                    return false;
                }

                if (OCA[i, 0].Length > 6 || OCA[i, 1].Length > 2 || OCA[i, 2].Length > 1)
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимый размер строки в ТКО. Команда - от 1 до 6. Код - от 1 до 2. Длина - не более одного";
                    return false;
                }

                if (!dC.CheckLettersAndNumbers(OCA[i, 0]))
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимый символ в поле команды";
                    return false;
                }

                if (dC.CheckNumbers(OCA[i, 0]))
                {
                    errorText = $"В строке {i + 1} ошибка. Некорректный МКО";
                    return false;
                }

                if (!dC.CheckLettersAndNumbers(OCA[i, 0]))
                {
                    errorText = $"В строке {i + 1} ошибка. В поле команды недопустимый символ";
                }

                //Проверка на 16чные цифры
                if (dC.CheckAdress(OCA[i, 1]))
                {
                    if (dC.CheckRegisters(OCA[i, 0]) || dC.CheckDirective(OCA[i, 0]))
                    {
                        errorText = $"В строке {i + 1} ошибка. Код команды является зарезервированным словом";
                        return false;
                    }

                    if (Converter.ConvertHexToDec(OCA[i, 1]) > 63)
                    {
                        errorText = $"В строке {i + 1} ошибка. Код команды не должен превышать 3F";
                        return false;
                    }
                    else
                    {
                        if (OCA[i, 1].Length == 1)
                            OCA[i, 1] = Converter.ConvertToTwoChars(OCA[i, 1]);
                    }
                }
                else
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимые символы в поле кода";
                    return false;
                }

                if (dC.CheckNumbers(OCA[i, 2]))
                {
                    int res = int.Parse(OCA[i, 2]);

                    if (res <= 0 || res > 4 || res == 3)
                    {
                        errorText = $"В строке {i + 1} ошибка. Недопустимый размер команды. Должен быть 1, 2 или 4";
                        return false;
                    }
                }
                else
                {
                    errorText = $"В строке {i + 1} ошибка. Недопустимые символы в поле размера операции";
                    return false;
                }


                for (int k = i + 1; k < rows; k++)
                {
                    string str1 = OCA[i, 0];
                    string str2 = OCA[k, 0];
                    if (Equals(str1, str2))
                    {
                        errorText = $"В строке {i + 1} ошибка. В поле команда найдены совпадения";
                        return false;
                    }
                }


                for (int k = i + 1; k < rows; k++)
                {
                    string str1 = Converter.ConvertHexToDec(OCA[i, 1]).ToString();
                    string str2 = Converter.ConvertHexToDec(OCA[k, 1]).ToString();
                    if (Equals(str1, str2))
                    {
                        errorText = $"В строке {i + 1} ошибка. В поле кода операции найдены совпадения";
                        return false;
                    }
                }
            }

            return true;
        }

        public bool DoPass(string[,] sourceCode, string[,] operationCode, DataGridView symbolDataGrid, DataGridView binary, int i, DataGridView tuneDataGrid)
        {
            int oldAddressCount = 0;
            int countStart = 0;
            bool flagReplace = false;

            if (flagStart)
            {
                if (!CheckMemmory())
                    return false;
            }

            if (!dC.CheckRow(sourceCode, i, out string mark, out string OC, out string OP1, out string OP2, nameProg))
            {
                errorText = $"В строке {i + 1} синтаксическая ошибка.";
                return false;
            }

            for (int j = 0; j < operationCode.GetUpperBound(0); j++)
            {
                if (Equals(mark.ToUpper(), operationCode[j, 0].ToUpper()))
                {
                    errorText = $"В строке {i + 1} ошибка. Символическое имя не может совпадать с названием команды";
                    return false;
                }
            }

            foreach (var item in extNames)
            {
                if (Equals(mark.ToUpper(), item.ToUpper()))
                {
                    errorText = $"В строке {i + 1} ошибка. Символическое имя не может совпадать с названием программы";
                    return false;
                }
            }

            string addressName = "";
            string addressTune = "";

            int markRow = FindMarkInMarkTable(mark, ref addressName, ref addressTune);

            if (mark.Length > 0 && OC.ToUpper() != "START")
            {
                if (markRow == -1)
                {
                    symbolTable[0].Add(mark.ToUpper());
                    symbolTable[1].Add(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                    symbolTable[2].Add("");
                }
                else
                {
                    if (addressName == "")
                    {
                        flagReplace = true;
                        oldAddressCount = countAddress;
                    }
                    else
                    {
                        errorText = $"В строке {i + 1} ошибка. Повторение символьных имен недопустимо";
                        return false;
                    }
                }
            }

            if (dC.CheckDirective(OC))
            {
                switch (OC)
                {
                    case "START":
                        {
                            countStart++;
                            if (i == 0 && !flagStart)
                            {
                                flagStart = true;

                                if (dC.CheckAdress(OP1) || OP1 == "")
                                {
                                    OP1 = OP1.TrimStart('0');

                                    if (OP1 == "")
                                        OP1 = "0";

                                    countAddress = Converter.ConvertHexToDec(OP1);

                                    startAddress = countAddress;

                                    if (countAddress != 0)
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Адрес начала программы должен быть равен нулю";
                                        return false;
                                    }

                                    if (countAddress > memmoryMax || countAddress < 0)
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Неправильный адрес загрузки";
                                        return false;
                                    }

                                    if (mark == "")
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Не задано имя программы";
                                        return false;
                                    }

                                    if (mark.Length > 10)
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Первышена длина имени программы\n Имя программы должно быть не больше 10 символов";
                                        return false;
                                    }

                                    for (int j = 0; j <= operationCode.GetUpperBound(0); j++)
                                    {
                                        if (Equals(mark.ToUpper(), operationCode[j, 0].ToUpper()))
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Имя программы не может совпадать с названием программы";
                                            return false;
                                        }
                                    }

                                    AddToBinary("H", mark, Converter.ConvertToSixChars(OP1), "");
                                    nameProg = mark;
                                    extNames.Add(mark);

                                    if (OP2.Length > 0)
                                    {
                                        errorText = $"В строке {i + 1} второй операнд директивы START не рассматривается. Устраните и повторите заново.";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Неверный адрес начала программы";
                                    return false;
                                }
                            }
                            else
                            {
                                errorText = $"В строке {i + 1} ошибка. Повторное использование директивы START";
                                return false;
                            }
                        }
                        break;

                    case "WORD":
                        {
                            if (flagStart)
                            {
                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb >= 0 && numb <= memmoryMax)
                                    {
                                        if (!AddCheckError(i, 3, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), "06", Converter.ConvertToSixChars(Converter.ConvertDecToHex(numb))))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы WORD не рассматривается. Устраните иповторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                    return false;
                                }
                            }
                            else
                            {
                                errorText = $"В строке {i + 1} ошибка. Не найдена директива START";
                                return false;
                            }
                        }
                        break;

                    case "BYTE":
                        {
                            if (flagStart)
                            {
                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb >= 0 && numb <= 255)
                                    {
                                        if (!AddCheckError(i, 1, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), "02", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(numb))))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы BYTE не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }
                                else
                                {
                                    string symb = dC.CheckAndGetString(OP1);

                                    if (symb != "")
                                    {
                                        if (symb.Length > 60)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Превышена длина строки";
                                            return false;
                                        }

                                        string res = CheckOP(OP1, out bool er, out bool label, -1);
                                        if (er)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Ошибка в операнде, код отсутсвтует в ТСИ";
                                            return false;
                                        }

                                        if (!AddCheckError(i, symb.Length, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), Converter.ConvertToTwoChars(Converter.ConvertDecToHex(res.Length)), res))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы BYTE не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }

                                    var symb1 = dC.CheckAndGetByteString(OP1);

                                    if (symb1 != "")
                                    {
                                        if (symb1.Length % 2 == 0)
                                        {
                                            if (symb1.Length > 60)
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Превышена длина строки";
                                                return false;
                                            }

                                            string res = CheckOP(OP1, out bool er, out bool label, -1);
                                            if (er)
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Ошибка в операнде, код отсутсвтует в ТСИ";
                                                return false;
                                            }

                                            if (!AddCheckError(i, symb1.Length / 2, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), Converter.ConvertToTwoChars(Converter.ConvertDecToHex(symb1.Length)), res))
                                                return false;

                                            if (OP2.Length > 0)
                                            {
                                                errorText = $"В строке {i + 1} второй операнд директивы BYTE не рассматривается. Устраните и повторите заново.";
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Невозможно преобразовать BYTE нечетное количество символов";
                                            return false;
                                        }
                                    }

                                    if (symb == "" && symb1 == "")
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Неверный формат строки {OP1}";
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                errorText = $"В строке {i + 1} ошибка. Не найдена директива START";
                                return false;
                            }
                        }
                        break;

                    case "RESB":
                        {
                            if (flagStart)
                            {
                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb > 0)
                                    {
                                        if (countAddress > memmoryMax)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Переполнение памяти";
                                            return false;
                                        }
                                        else
                                        {
                                            if (!AddCheckError(i, numb, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), Converter.ConvertDecToHex(numb), ""))
                                                return false;

                                            if (OP2.Length > 0)
                                            {
                                                errorText = $"В строке {i + 1} второй операнд директивы RESB не рассматривается. Устраните иповторите заново.";
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Количество байт равно нулю или меньше нуля";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Невозможно выполнить преобразование {OP1}";
                                    return false;
                                }
                            }
                            else
                            {
                                errorText = $"В строке {i + 1} ошибка. Не найдена директива START";
                                return false;
                            }
                        }
                        break;

                    case "RESW":
                        {
                            if (flagStart)
                            {
                                if (int.TryParse(OP1, out int numb))
                                {
                                    if (numb > 0)
                                    {
                                        if (!AddCheckError(i, numb * 3, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), Converter.ConvertDecToHex(numb * 3), ""))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд директивы RESW не рассматривается. Устраните и повторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Количество байт равно нулю или меньше нуля";
                                        return false;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Невозможно выполнить преобразование {OP1}";
                                    return false;
                                }
                            }
                            else
                            {
                                errorText = $"В строке {i + 1} ошибка. Не найдена директива START";
                                return false;
                            }
                        }
                        break;

                    case "END":
                        {
                            if(mark.Length > 0)
                            {
                                errorText = $"В строке {i + 1} метка. Устраните и повторите заново.";
                                return false;
                            }

                            if(OP2 != "")
                            {
                                errorText = $"В строке {i + 1} второй операнд директивы END не рассматривается. Устраните и повторите заново.";
                                return false;
                            }

                            if (flagStart && !flagEnd)
                            {
                                flagEnd = true;
                                if (OP1.Length == 0)
                                {
                                    endAddress = startAddress;
                                    AddToBinary("E", Converter.ConvertToSixChars(Converter.ConvertDecToHex(endAddress)), "", "");

                                    int head = FindHead("H");
                                    if (head > -1)
                                        exitTable[3][head] = Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress - startAddress));

                                    if (OP2.Length > 0)
                                    {
                                        errorText = $"В строке {i + 1} второй операнд директивы END не рассматривается. Устраните и повторите заново.";
                                        return false;
                                    }

                                    break;

                                }
                                else
                                {
                                    if (dC.CheckAdress(OP1))
                                    {
                                        endAddress = Converter.ConvertHexToDec(OP1);
                                        if (endAddress >= startAddress && endAddress <= countAddress)
                                        {
                                            int head = FindHead("H");
                                            if (head > -1)
                                                exitTable[3][head] = Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress - startAddress));

                                            if (flagReplace)
                                            {
                                                flagReplace = false;
                                                ReplaceMark(mark, Converter.ConvertToSixChars(Converter.ConvertDecToHex(oldAddressCount)), Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                            }

                                            if (!dC.CheckEmptyAddress(symbolTable, out int num))
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Найдено неопределенное внешнее имя {symbolTable[0][num]}";
                                                return false;
                                            }

                                            symbolDataGrid.Rows.Clear();
                                            for (int j = 0; j < symbolTable[1].Count; j++)
                                            {
                                                symbolDataGrid.Rows.Add();
                                                symbolDataGrid.Rows[j].Cells[0].Value = symbolTable[0][j];
                                                symbolDataGrid.Rows[j].Cells[1].Value = symbolTable[1][j];
                                                symbolDataGrid.Rows[j].Cells[2].Value = symbolTable[2][j];
                                            }

                                            if(tuneTable.Count > 0)
                                            {
                                                for (int j = 0; j < tuneTable.Count; j++)
                                                    AddToBinary("M", tuneTable[j], "", "");
                                            }

                                            AddToBinary("E", Converter.ConvertToSixChars(Converter.ConvertDecToHex(endAddress)), "", "");
                                            break;
                                        }
                                        else
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Неверный адрес входа в программу";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Неверный адрес входа в программу";
                                        return false;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                if (flagStart)
                {
                    if (OC.Length > 0)
                    {
                        int numb = FindCode(OC, operationCode);
                        if (numb > -1)
                        {
                            if (operationCode[numb, 2] == "1")
                            {
                                string res = CheckOP(OP1, out bool er, out bool label, -1);
                                if (er)
                                {
                                    errorText = $"В строке {i + 1} ошибка. Ошибка в операнде, код отсутсвтует в ТСИ";
                                    return false;
                                }

                                string str = Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertHexToDec(operationCode[numb, 1]) * 4));

                                if (!AddCheckError(i, 1, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), Converter.ConvertToTwoChars(Converter.ConvertDecToHex(str.Length)), str))
                                    return false;

                                if (OP1.Length > 0 || OP2.Length > 0)
                                {
                                    errorText = $"В строке {i + 1} операнды не рассматривается в команде {operationCode[numb, 0]}. Устраните иповторите заново.";
                                    return false;
                                }
                            }

                            else if (operationCode[numb, 2] == "2")
                            {
                                if (int.TryParse(OP1, out int number))
                                {

                                    if (number >= 0 && number <= 255)
                                    {
                                        string res = CheckOP(OP1, out bool er, out bool label, -1);
                                        if (er)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Ошибка в операнде, код отсутсвтует в ТСИ";
                                            return false;
                                        }

                                        string str = Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Convert.ToInt32(operationCode[numb, 2]) + res.Length));

                                        if (!AddCheckError(i, 2, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), str, 
                                            Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertHexToDec(operationCode[numb, 1])*4)) + Converter.ConvertToTwoChars(Converter.ConvertDecToHex(number))))
                                            return false;

                                        if (OP2.Length > 0)
                                        {
                                            errorText = $"В строке {i + 1} второй операнд не рассматривается в команде {operationCode[numb, 0]}.  Устраните иповторите заново.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Отрицательное число либо превышено максимальное значение числа";
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (dC.CheckRegisters(OP1) && dC.CheckRegisters(OP2))
                                    {
                                        string res1 = CheckOP(OP1, out bool er, out bool label, -1);
                                        if (er)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Ошибка в операнде, код отсутсвтует в ТСИ";
                                            return false;
                                        }

                                        string res2 = CheckOP(OP2, out er, out label, -1);
                                        if (er)
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Ошибка в операнде, код отсутсвтует в ТСИ";
                                            return false;
                                        }

                                        string str = Converter.ConvertToTwoChars(Converter.ConvertDecToHex(2 + res1.Length + res2.Length));

                                        if (!AddCheckError(i, 2, "T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), str, 
                                            Converter.ConvertToTwoChars(Converter.ConvertDecToHex(Converter.ConvertHexToDec(operationCode[numb, 1]) * 4)) + res1 + res2))
                                            return false;
                                    }
                                    else
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Ошибка в команде {operationCode[numb, 0]}";
                                        return false;
                                    }
                                }
                            }

                            else if (operationCode[numb, 2] == "4")
                            {
                                if (countAddress + 4 > memmoryMax)
                                {
                                    errorText = $"В строке {i + 1} ошибка. Произошло переполнение";
                                    return false;
                                }

                                int adrType;
                                int tpp;

                                if (OP1.Length > 0)
                                {
                                    if (OP1[0] == '[' && OP1[OP1.Length - 1] == ']')
                                    {
                                        OP1 = OP1.Substring(1, OP1.Length - 2);
                                        if(OP1.Length > 0)
                                        {
                                            if(dC.CheckLettersAndNumbers(OP1) && dC.CheckLetters(Convert.ToString(OP1[0]))){}
                                            else
                                            {
                                                errorText = $"В строке {i + 1} ошибка. Ошибка в символическом имени";
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            errorText = $"В строке {i + 1} ошибка. Не все символические имена имеют адрес";
                                            return false;
                                        }
                                        adrType = Converter.ConvertHexToDec(operationCode[numb, 1]) * 4 + 2;
                                        tpp = 2;
                                    }
                                    else
                                    {
                                        if(dC.CheckLettersAndNumbers(OP1) && dC.CheckLetters(Convert.ToString(OP1[0]))) 
                                            tuneTable.Add(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                        else
                                        {
                                            errorText = $"Ошибка в символическом имени";
                                            return false;
                                        }

                                        adrType = Converter.ConvertHexToDec(operationCode[numb, 1]) * 4 + 1;
                                        tpp = 1;
                                    }
                                }
                                else
                                {
                                    errorText = $"В строке {i + 1} ошибка. Не найден операнд";
                                    return false;
                                }

                                for (int j = 0; j <= operationCode.GetUpperBound(0) - 1; j++)
                                {
                                    if(Equals(OP1.ToUpper(), operationCode[j, 0].ToUpper()))
                                    {
                                        errorText = $"В строке {j + 1} ошибка. Символическое имя не может совпадать с названием команды";
                                        return false;
                                    }
                                }

                                foreach (var item in extNames.Select((value, index) => new {value, index}))
                                {
                                    if(Equals(OP1.ToUpper(), item.value.ToUpper()))
                                    {
                                        errorText = $"В строке {i + 1} ошибка. Символическое имя не может совпадать с названием программы";
                                        return false;
                                    }
                                }

                                if(dC.CheckDirective(OP1) || dC.CheckRegisters(OP1))
                                {
                                    errorText = $"В строке {i + 1} ошибка. Символическое имя не может совпадать с системным именем";
                                    return false;
                                }

                                string opAdr = "";
                                string tuneAdr = "";
                                int finded = FindMarkInMarkTable(OP1, ref opAdr, ref tuneAdr);

                                if(finded > -1)
                                {
                                    if(opAdr != "")
                                    {
                                        if(tpp == 1)
                                        {
                                            string tmp = Converter.ConvertToTwoChars(Converter.ConvertDecToHex(adrType)) + opAdr;
                                            AddToBinary("T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), Converter.ConvertToTwoChars(Converter.ConvertDecToHex(tmp.Length)), tmp);
                                        }
                                        else if(tpp == 2)
                                        {
                                            if (!CheckMemmory())
                                                return false;

                                            string str = Converter.ConvertToTwoChars(Converter.ConvertDecToHex(adrType)) + Converter.ConvertSubHex(symbolTable[1][finded], Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress + 4)));
                                            AddToBinary("T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), Converter.ConvertToTwoChars(Converter.ConvertDecToHex(str.Length)), str);
                                        }
                                        
                                    }
                                    else
                                    {
                                        symbolTable[0].Insert(finded + 1, OP1.ToUpper());
                                        symbolTable[1].Insert(finded + 1, "");
                                        symbolTable[2].Insert(finded + 1, Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                        AddToBinary("T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), "", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(adrType)) + "#" + OP1 + "#");
                                    }
                                }
                                else
                                {
                                    symbolTable[0].Add(OP1.ToUpper());
                                    symbolTable[1].Add("");
                                    symbolTable[2].Add(Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));
                                    AddToBinary("T", Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)), "", Converter.ConvertToTwoChars(Converter.ConvertDecToHex(adrType)) + "#" + OP1 + "#");
                                }

                                countAddress += 4;

                                if (!CheckMemmory())
                                    return false;

                                if (OP2.Length > 0)
                                {
                                    errorText = $"В строке {i + 1} второй операнд не рассматривается в команде {operationCode[numb, 0]}.  Устраните и повторите заново.";
                                    return false;
                                }
                            }
                            else
                            {
                                errorText = $"В строке {i + 1} размер команды больше установленного";
                                return false;
                            }
                        }
                        else
                        {
                            errorText = $"В строке {i + 1} ошибка. В ТКО не найдено {OC}";
                            return false;
                        }
                    }
                    else
                    {
                        errorText = $"В строке {i + 1} ошибка. Синтаксическая ошибка";
                        return false;
                    }
                }
                else
                {
                    errorText = $"В строке {i + 1} ошибка. Не найдена директива START";
                    return false;
                }
            }
            if (flagReplace)
                ReplaceMark(mark, Converter.ConvertToSixChars(Converter.ConvertDecToHex(oldAddressCount)), Converter.ConvertToSixChars(Converter.ConvertDecToHex(countAddress)));

            binary.Rows.Clear();
            tuneDataGrid.Rows.Clear();

            if(tuneTable.Count > 0)
            {
                for (int j = 0; j < tuneTable.Count; j++)
                {
                    tuneDataGrid.Rows.Add();
                    tuneDataGrid.Rows[j].Cells[0].Value = tuneTable[j];
                }
            }

            if (exitTable[0].Count > 0)
            {
                for (int j = 0; j < exitTable[0].Count; j++)
                {
                    binary.Rows.Add();
                    binary.Rows[j].Cells[0].Value = exitTable[0][j];
                    binary.Rows[j].Cells[1].Value = exitTable[1][j];
                    binary.Rows[j].Cells[2].Value = exitTable[2][j];
                    binary.Rows[j].Cells[3].Value = exitTable[3][j];
                }
            }

            symbolDataGrid.Rows.Clear();

            for (int j = 0; j < symbolTable[1].Count; j++)
            {
                symbolDataGrid.Rows.Add();
                symbolDataGrid.Rows[j].Cells[0].Value = symbolTable[0][j];
                symbolDataGrid.Rows[j].Cells[1].Value = symbolTable[1][j];
                symbolDataGrid.Rows[j].Cells[2].Value = symbolTable[2][j];
            }

            return true;
        }

        private int ReplaceMark(string mark, string markAdr, string currentAdr)
        {
            List<List<string>> marks = new List<List<string>>();

            marks.Add(new List<string>());
            marks.Add(new List<string>());
            marks.Add(new List<string>());

            List<string> adrCounter = new List<string>();

            for (int j = 0; j < exitTable[0].Count; j++)
                adrCounter.Add(exitTable[1][j]);
            adrCounter.Add(currentAdr);

            string n1 = "";
            string n2 = "";

            for (int i = 0; i < symbolTable[0].Count; i++)
            {
                if (mark == symbolTable[0][i] && symbolTable[1][i] == "")
                {
                    symbolTable[1][i] = markAdr;

                    n1 = symbolTable[0][i];
                    n2 = symbolTable[1][i];

                    for (int j = 0; j < exitTable[0].Count; j++)
                    {
                        if (symbolTable[2][i] == exitTable[1][j])
                        {
                            if (exitTable[3][j].Length > 0)
                            {
                                string type = exitTable[3][j].Substring(0, 2);
                                int typeOfAdr = (byte)Converter.ConvertHexToDec(type) & 0x03;

                                int index = exitTable[3][j].IndexOf("#" + mark + "#");
                                if (index > -1)
                                {
                                    switch (typeOfAdr)
                                    {
                                        case 1:
                                            exitTable[3][j] = exitTable[3][j].Replace("#" + mark + "#", markAdr);
                                            break;
                                        case 2:
                                            exitTable[3][j] = exitTable[3][j].Replace("#" + mark + "#", Converter.ConvertSubHex(n2, adrCounter[j + 1]));
                                            break;
                                    }

                                    exitTable[2][j] = Converter.ConvertToTwoChars(Converter.ConvertDecToHex(exitTable[3][j].Length));
                                }
                            }
                        }
                    }
                }
                else
                {
                    marks[0].Add(symbolTable[0][i]);
                    marks[1].Add(symbolTable[1][i]);
                    marks[2].Add(symbolTable[2][i]);
                }
            }

            symbolTable = marks;
            symbolTable[0].Insert(0, n1);
            symbolTable[1].Insert(0, n2);
            symbolTable[2].Insert(0, "");

            return -1;
        }

        private int FindHead(string head)
        {
            if (exitTable[0].Count > 0)
            {
                for (int i = 0; i < exitTable[0].Count; i++)
                {
                    if (head == exitTable[0][i])
                    {
                        return i;
                    }

                }
            }
            return -1;
        }

        private bool AddCheckError(int i, int numbToAdd, string type, string count, string numb, string OP1)
        {
            if (countAddress + numbToAdd > memmoryMax)
            {
                errorText = $"В строке {i + 1} ошибка. Произошло переполнение";
                return false;
            }

            AddToBinary(type, count, numb, OP1);

            countAddress += numbToAdd;

            if (!CheckMemmory())
                return false;

            return true;
        }

        public string CheckOP(string OP, out bool er, out bool operandLabel, int ind)
        {
            er = false;
            operandLabel = false;
            if (OP != "")
            {
                int reg = dC.GetRegisters(OP);
                if (reg > -1)
                    return Converter.ConvertDecToHex(reg);
                else if (dC.CheckNumbers(OP))
                    return Converter.ConvertDecToHex(Convert.ToInt32(OP));
                else
                {
                    string str = dC.CheckAndGetString(OP);
                    if (str != "")
                        return Converter.ConvertASCII(str);

                    str = dC.CheckAndGetByteString(OP);

                    if (str != "")
                        return str;

                    er = true;
                }
            }
            return "";
        }

    }
}
