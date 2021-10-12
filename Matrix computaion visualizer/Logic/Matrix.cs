using System;
namespace Matrix_Calculator
{
    public class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }

        public int[,] nums;

        public Matrix()
        {

        }

        public Matrix(int rows, int columns, int[,] nums)
        {
            if (rows > 0 && columns > 0)
            {
                Rows = rows;
                Columns = columns;

                this.nums = new int[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        this.nums[i, p] = nums[i, p];
                    }
                }
            } else
            {
                throw new ArgumentException("One of arguments or both is unacceptable");
            }

        }

        public Matrix Addition(Matrix matrix2)
        {
            if (Rows == matrix2.Rows && Columns == matrix2.Columns)
            {
                int[,] additionMas = new int[Rows, Columns];

                for (int i = 0; i < Rows; ++i)
                {
                    for (int p = 0; p < Columns; ++p)
                    {
                        additionMas[i, p] = nums[i, p] + matrix2.nums[i, p];
                    }
                }

                return new Matrix(Rows, Columns, additionMas);
            } else
            {
                throw new ArgumentException("Dimension of the matrices is different");
            }
        }
        public Matrix Substraction(Matrix matrix2)
        {
            if (Rows == matrix2.Rows && Columns == matrix2.Columns)
            {
                int[,] substractionMas = new int[Rows, Columns];

                for (int i = 0; i < Rows; ++i)
                {
                    for (int p = 0; p < Columns; ++p)
                    {
                        substractionMas[i, p] = nums[i, p] - matrix2.nums[i, p];
                    }
                }

                return new Matrix(Rows, matrix2.Columns, substractionMas);
            }
            else
            {
                throw new ArgumentException("Dimension of the matrices is different");
            }
        }
        public Matrix Multiplication(Matrix matrix2)
        {
            if (Columns == matrix2.Rows)
            {
                int[,] multiplicationMas = new int[Rows, matrix2.Columns];

                for (int i = 0; i < Rows; ++i)
                {
                    for (int p = 0; p < matrix2.Columns; ++p)
                    {
                        int num = MultiplicationRowOnColumn(Columns, nums, matrix2.nums, i, p);

                        multiplicationMas[i, p] = num;
                    }
                }

                return new Matrix(Rows, matrix2.Columns, multiplicationMas);
            }
            else
            {
                throw new ArgumentException("Number of columns of one matrix is not equal to the number of rows of second matrix");
            }
        }

        public Matrix MultiplicationOnNumber(int number)
        {
            int[,] multiplicationOnNumberMas = new int[Rows, Columns];

            for (int i = 0; i < Rows; ++i)
            {
                for (int p = 0; p < Columns; ++p)
                {
                    multiplicationOnNumberMas[i, p] = nums[i, p] * number;
                }
            }

            return new Matrix(Rows, Columns, multiplicationOnNumberMas);
        }

        public Matrix Transposition() {
            int[,] transpositionMas = new int[Columns, Rows];

            for (int i = 0; i < Rows; ++i)
            {
                for (int p = 0; p < Columns; ++p)
                {
                    transpositionMas[p, i] = nums[i, p];
                }
            }

            return new Matrix(Columns, Rows, transpositionMas);
        }

        private int MultiplicationRowOnColumn(int columns1, int[,] mas1, int[,] mas2, int indRow, int indColumn)
        {
            int result = 0;
            for (int i = 0; i < columns1; ++i)
            {
                result += mas1[indRow, i] * mas2[i, indColumn];
            }
            return result;
        }

        public int GetDeterminant()
        {
            if (Rows == Columns)
            {
                int order = Rows;

                if (order == 1)
                {
                    return nums[0,0];
                } else if (order == 2)
                {
                    return nums[0, 0] * nums[1, 1] - nums[0, 1] * nums[1, 0]; 
                } else if (order == 3)
                {
                    int mainDiagonal = nums[0, 0] * nums[1, 1] * nums[2, 2] + nums[0, 1] * nums[1, 2] * nums[2, 0] + nums[0, 2] * nums[1, 0] * nums[2, 1];
                    int secondaryDiagonal = nums[0, 2] * nums[1, 1] * nums[2, 0] + nums[0, 1] * nums[1, 0] * nums[2, 2] + nums[0, 0] * nums[2, 1] * nums[1, 2];
                    return mainDiagonal - secondaryDiagonal;
                } else
                {
                    int sum = 0;

                    for (int i = 0; i < Rows; ++i)
                    {
                        int indexRow = i, indexColumn = 0;
                        var newMatrix = GetReducedMatrix(indexRow, indexColumn);
                        int minor = newMatrix.GetDeterminant();
                        int algebraicComplement = Convert.ToInt32(Math.Pow(-1, indexRow + indexColumn)) * minor;

                        sum += nums[indexRow, indexColumn] * algebraicComplement;
                    }

                    return sum;
                }
            } else
            {
                throw new ArgumentException("Number of columns is not equal to the number of rows");
            }
        }

        private Matrix GetReducedMatrix(int indDeleteRow, int indDeleteColumn)
        {
            int newSize = Rows - 1;
            int[,] newMas = new int[newSize, newSize];

            for (int i = 0; i < Rows; ++i)
            {
                for (int p = 0; p < Columns; ++p)
                {
                    if (i < indDeleteRow && p < indDeleteColumn)
                    {
                        newMas[i, p] = nums[i, p];
                    } else if (i < indDeleteRow && p > indDeleteColumn)
                    {
                        newMas[i, p - 1] = nums[i, p];
                    } else if (i > indDeleteRow && p < indDeleteColumn)
                    {
                        newMas[i - 1, p] = nums[i, p];
                    } else if (i > indDeleteRow && p > indDeleteColumn)
                    {
                        newMas[i - 1, p - 1] = nums[i, p];
                    }

                }
            }

            var newMatrix = new Matrix(newSize, newSize, newMas);
            return newMatrix;
        }

    }
}
