using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace lab1SisProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            exampleComboBox.SelectedIndex = 0;
            ExampleTKO();
        }

        readonly int columnsCount = 4;
        bool prepareFlag = false;
        int typeAdr = 0;
        FSPass fsp;
        DataCheck dC;
        int currentRow;

        //помещаем ТКО в динамический массив
        private string[,] operationCodeArray;
        //помещаем исходный код в динамический массив
        private string[,] sourceCodeArray;


        private void start_Click(object sender, EventArgs e)
        {
            if (!Begin(2))
                Stop();
        }

        private void step_Click(object sender, EventArgs e)
        {
            if (!Begin(0))
                Stop();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            if (!Begin(1))
                Stop();
            Clear();
        }

        private void Stop()
        {
            UnProtectSourceData();
            fsp = null;
            prepareFlag = false;
            currentRow = 0;
        }

        private bool Begin(int param)
        {
            if (!prepareFlag)
            {
                firstPassErrorTextBox.Text = "";
                if (Prepare())
                {
                    prepareFlag = true;
                    ProtectSourceData();
                    firstPassErrorTextBox.Text = "";
                }
                else
                {
                    return false;
                }
            }

            switch (param)
            {
                case 0:
                    {
                        if (currentRow + 1 <= sourceCodeArray.GetLength(0))
                        {

                            if (!fsp.DoPass(sourceCodeArray, operationCodeArray, symbolTableDataGrid, binaryCodeTextBox, currentRow, tuneTableDT))
                            {
                                writeError(firstPassErrorTextBox, fsp.errorText);
                                return false;
                            }

                            if (fsp.flagEnd)
                            {
                                Stop();
                                return true;
                            }

                            currentRow++;
                        }
                        else
                        {
                            if (!fsp.flagEnd)
                            {
                                writeError(firstPassErrorTextBox, "Ошибка: Не найдена директива END");
                                return false;
                            }
                        }
                        break;
                    }

                case 1:
                    {
                        Stop();
                        break;
                    }

                case 2:
                    {
                        for (int i = currentRow; i <= sourceCodeArray.GetLength(0); i++)
                        {
                            if (i + 1 <= sourceCodeArray.GetLength(0))
                            {
                                if (!fsp.DoPass(sourceCodeArray, operationCodeArray, symbolTableDataGrid, binaryCodeTextBox, i, tuneTableDT))
                                {
                                    writeError(firstPassErrorTextBox, fsp.errorText);
                                    return false;
                                }

                                if (fsp.flagEnd)
                                {
                                    Stop();
                                    return true;
                                }
                            }
                            else
                            {
                                if (!fsp.flagEnd)
                                {
                                    writeError(firstPassErrorTextBox, "Ошибка: не найдена директива END");
                                    return false;
                                }
                            }
                        }
                        break;
                    }
            }
            return true;
        }

        private void ProtectSourceData()
        {
            operationCodeDataGrid.ReadOnly = true;
            sourceCodeTextBox.ReadOnly = true;
        }

        private void UnProtectSourceData()
        {
            operationCodeDataGrid.ReadOnly = false;
            sourceCodeTextBox.ReadOnly = false;
        }

        private bool Prepare()
        {
            fsp = new FSPass();
            currentRow = 0;
            Clear();
            DeleteEmptyRows(operationCodeDataGrid);
            if (Parser())
            {

                if (fsp.CheckOperationCode(operationCodeArray))
                {
                    return true;
                }
                else
                {
                    writeError(firstPassErrorTextBox, fsp.errorText);
                    return false;
                }
            }
            else
                return false;
        }

        private void writeError(TextBox textBox, string str)
        {
            textBox.Text += str + "\r\n";
        }

        private void Clear()
        {
            symbolTableDataGrid.Rows.Clear();
            firstPassErrorTextBox.Clear();
            binaryCodeTextBox.Rows.Clear();
            tuneTableDT.Rows.Clear();
        }

        private void SourceCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void OperationCodeDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Clear();
        }

        public void DeleteEmptyRows(DataGridView DBGrid_source_code)
        {
            for (int i = 0; i < DBGrid_source_code.Rows.Count - 1; i++)
            {
                bool empty = true;
                for (int j = 0; j < DBGrid_source_code.Rows[i].Cells.Count; j++)
                    if ((DBGrid_source_code.Rows[i].Cells[j].Value != null) && (DBGrid_source_code.Rows[i].Cells[j].Value.ToString() != ""))
                        empty = false;

                if (empty)
                {
                    DBGrid_source_code.Rows.Remove(DBGrid_source_code.Rows[i]);
                }
            }
        }

        private bool Parser()
        {
            Clear();
            DeleteEmptyRows(operationCodeDataGrid);

            fsp = new FSPass();
            dC = new DataCheck();

            List<string> marks = new List<string>();

            operationCodeArray = new string[operationCodeDataGrid.RowCount - 1, operationCodeDataGrid.ColumnCount];

            for (int i = 0; i < operationCodeDataGrid.RowCount - 1; i++)
                for (int j = 0; j < operationCodeDataGrid.ColumnCount; j++)
                    operationCodeArray[i, j] = Convert.ToString(operationCodeDataGrid.Rows[i].Cells[j].Value).ToUpper();


            string[] str = sourceCodeTextBox.Text.Split('\n');

            for (int i = 0; i < str.Length; i++)
                str[i] = Convert.ToString(str[i]).Replace("\r", "");

            str = str.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            sourceCodeArray = new string[str.Length, columnsCount];

            for (int i = 0; i < str.Length; i++)
                for (int j = 0; j < columnsCount; j++)
                    sourceCodeArray[i, j] = "";

            for (int i = 0; i < str.Length; i++)
            {
                str[i] = str[i].Trim();
                string[] temp = str[i].Split(' ');
                if (temp.Length >= 3)
                {
                    if (temp[1].IndexOf('"') == 1 && temp[temp.Length - 1].LastIndexOf('"') == temp[temp.Length - 1].Length - 1)
                    {
                        for (int j = 2; j < temp.Length; j++)
                        {
                            temp[1] += " " + temp[j];
                            temp[j] = "";
                        }
                    }
                    else if (temp[2].IndexOf('"') == 1 && temp[temp.Length - 1].LastIndexOf('"') == temp[temp.Length - 1].Length - 1)
                    {
                        for (int j = 3; j < temp.Length; j++)
                        {
                            temp[2] += " " + temp[j];
                            temp[j] = "";
                        }
                    }
                }
                temp = temp.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                temp[temp.Length - 1] = temp[temp.Length - 1].Replace("\r", "");

                if (temp[0] == "END" && i + 1 != str.Length)
                {
                    MessageBox.Show($"Замечены строки после END.", "Внимание!");
                    return false;
                }

                if (temp.Length <= 4)
                {
                    if (temp.Length == 1)
                    {
                        if (dC.CheckDirective(temp[0]) || fsp.FindCode(temp[0], operationCodeArray) != -1)
                            sourceCodeArray[i, 1] = temp[0];
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }
                    else if (temp.Length == 2)
                    {

                        if ((temp[0] == "EXTREF") || (temp[0] == "EXTDEF"))
                        {
                            marks.Add(temp[1]);
                        }

                        if (dC.CheckDirective(temp[0]) || fsp.FindCode(temp[0], operationCodeArray) != -1)
                        {
                            if (fsp.FindCode(temp[0], operationCodeArray) != -1 && typeAdr == 0 && (temp[1].Contains('[') || temp[1].Contains(']')) && !marks.Contains(temp[1]))
                            {
                                MessageBox.Show($"Относительная адресация в {i + 1} строке. Выбрана прямая адресация.", "Внимание!");
                                return false;
                            }
                            if (fsp.FindCode(temp[0], operationCodeArray) != -1 && typeAdr == 1 && (!temp[1].Contains('[') || !temp[1].Contains(']')) && !marks.Contains(temp[1]) && !int.TryParse(temp[1], out int p))
                            {
                                MessageBox.Show($"Прямая адресация в {i + 1} строке. Выбрана относительная адресация.", "Внимание!");
                                return false;
                            }
                            sourceCodeArray[i, 1] = temp[0];
                            sourceCodeArray[i, 2] = temp[1];
                        }
                        else if (dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1)
                        {
                            sourceCodeArray[i, 0] = temp[0];
                            sourceCodeArray[i, 1] = temp[1];
                        }
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }
                    else if (temp.Length == 3)
                    {
                        if (dC.CheckDirective(temp[0]) || fsp.FindCode(temp[0], operationCodeArray) != -1)
                        {
                            if (temp[2].IndexOf('"') == 1 && temp[3].IndexOf('"') == temp[3].Length - 1)
                            {
                                sourceCodeArray[i, 1] = temp[0];
                                sourceCodeArray[i, 2] = temp[1] + " " + temp[2];
                                sourceCodeArray[i, 3] = "";
                            }
                            else
                            {
                                sourceCodeArray[i, 1] = temp[0];
                                sourceCodeArray[i, 2] = temp[1];
                                sourceCodeArray[i, 3] = temp[2];
                            }
                        }
                        else if (dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1)
                        {
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 0 && (temp[2].Contains('[') || temp[2].Contains(']')) && !marks.Contains(temp[2]))
                            {
                                MessageBox.Show($"Относительная адресация в {i + 1} строке. Выбрана прямая адресация.", "Внимание!");
                                return false;
                            }
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 1 && (!temp[2].Contains('[') || !temp[2].Contains(']')) && !marks.Contains(temp[2]) && !int.TryParse(temp[1], out int p))
                            {
                                MessageBox.Show($"Прямая адресация в {i + 1} строке. Выбрана относительная адресация.", "Внимание!");
                                return false;
                            }
                            sourceCodeArray[i, 0] = temp[0];
                            sourceCodeArray[i, 1] = temp[1];
                            sourceCodeArray[i, 2] = temp[2];
                        }
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }
                    else if (temp.Length == 4)
                    {
                        if (dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1)
                        {
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 0 && (temp[2].Contains('[') || temp[2].Contains(']')) && !marks.Contains(temp[2]))
                            {
                                MessageBox.Show($"Относительная адресация в {i + 1} строке. Выбрана прямая адресация.", "Внимание!");
                                return false;
                            }
                            if (fsp.FindCode(temp[1], operationCodeArray) != -1 && typeAdr == 1 && (!temp[2].Contains('[') || !temp[2].Contains(']')) && !marks.Contains(temp[2]) && !int.TryParse(temp[1], out int p))
                            {
                                MessageBox.Show($"Прямая адресация в {i + 1} строке. Выбрана относительная адресация.", "Внимание!");
                                return false;
                            }
                            if (temp[2].IndexOf('"') == 1 && temp[3].IndexOf('"') == temp[3].Length - 1)
                            {
                                sourceCodeArray[i, 0] = temp[0];
                                sourceCodeArray[i, 1] = temp[1];
                                sourceCodeArray[i, 2] = temp[2] + " " + temp[3];
                                sourceCodeArray[i, 3] = "";
                            }
                            else
                            {
                                sourceCodeArray[i, 0] = temp[0];
                                sourceCodeArray[i, 1] = temp[1];
                                sourceCodeArray[i, 2] = temp[2];
                                sourceCodeArray[i, 3] = temp[3];
                            }
                        }
                        else if (!(dC.CheckDirective(temp[1]) || fsp.FindCode(temp[1], operationCodeArray) != -1))
                        {
                            MessageBox.Show($"Синтаксическая ошибка в {i + 1} строке.", "Внимание!");
                            return false;
                        }
                        else
                        {
                            for (int j = 0; j < temp.Length; j++)
                                sourceCodeArray[i, j] = temp[j];
                        }
                    }

                }
                else
                {
                    MessageBox.Show($"Синтаксическая ошибка в {i + 1} строке. Элементов в строке больше 4", "Внимание!");
                    return false;
                }
            }

            for (int i = 0; i < sourceCodeArray.GetLength(0); i++)
                sourceCodeArray[i, 1] = Convert.ToString(sourceCodeArray[i, 1]).ToUpper();

            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                    Console.Write(sourceCodeArray[i, j]);
                Console.WriteLine();
            }

            return true;
        }

        private void ExampleTKO()
        {
            operationCodeDataGrid.Rows.Add("JMP", "01", "4");
            operationCodeDataGrid.Rows.Add("LOADR1", "02", "4");
            operationCodeDataGrid.Rows.Add("LOADR2", "03", "4");
            operationCodeDataGrid.Rows.Add("ADD", "04", "2");
            operationCodeDataGrid.Rows.Add("SAVER1", "05", "4");
            operationCodeDataGrid.Rows.Add("NOP", "06", "1");
        }

        private void ExampleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            if (exampleComboBox.SelectedIndex == 0)
            {
                sourceCodeTextBox.Text = 
                    "prog start 0\r\n" +
                    "jmp L1\r\n" +
                    "A1 RESB 10\r\n" +
                    "A2 RESW 20\r\n" +
                    "B1 WORD 4096\r\n" +
                    "LOADR1 L1\r\n" +
                    "B2 BYTE X\"2F4C008A\"\r\n" +
                    "B3 BYTE C\"Hello!\"\r\n" +
                    "B4 BYTE 128\r\n" +
                    "L1 LOADR1 B1\r\n" +
                    "LOADR2 B4\r\n" +
                    "ADD R1 R2\r\n" +
                    "SAVER1 L1\r\n" +
                    "NOP\r\n" +
                    "END 0";
                typeAdr = 0;
            }
            if (exampleComboBox.SelectedIndex == 1)
            {
                typeAdr = 1;
                sourceCodeTextBox.Text =
                    "prog start 0\r\n" +
                    "jmp [L1]\r\n" +
                    "A1 RESB 10\r\n" +
                    "A2 RESW 20\r\n" +
                    "B1 WORD 4096\r\n" +
                    "LOADR1 [L1]\r\n" +
                    "B2 BYTE X\"2F4C008A\"\r\n" +
                    "B3 BYTE C\"Hello!\"\r\n" +
                    "B4 BYTE 128\r\n" +
                    "L1 LOADR1 [B1]\r\n" +
                    "LOADR2 [B4]\r\n" +
                    "ADD R1 R2\r\n" +
                    "SAVER1 [L1]\r\n" +
                    "NOP\r\n" +
                    "END 0";
            }
            if (exampleComboBox.SelectedIndex == 2)
            {
                typeAdr = 2;
                sourceCodeTextBox.Text =
                    "prog start 0\r\n" +
                    "jmp L1\r\n" +
                    "A1 RESB 10\r\n" +
                    "A2 RESW 20\r\n" +
                    "B1 WORD 4096\r\n" +
                    "LOADR1 L1\r\n" +
                    "B2 BYTE X\"2F4C008A\"\r\n" +
                    "B3 BYTE C\"Hello!\"\r\n" +
                    "B4 BYTE 128\r\n" +
                    "L1 LOADR1 B1\r\n" +
                    "LOADR2 [B4]\r\n" +
                    "ADD R1 R2\r\n" +
                    "SAVER1 [L1]\r\n" +
                    "NOP\r\n" +
                    "END 0";
            }
        }


    }
}
