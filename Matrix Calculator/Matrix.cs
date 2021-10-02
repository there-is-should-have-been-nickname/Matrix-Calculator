using System;

namespace Matrix_Calculator
{
    public class Matrix
    {
        public int rows { get; }
        public int columns { get; }

        public int[,] nums;

        public Matrix()
        {

        }

        public Matrix(int rows, int columns, int[,] nums)
        {
            if (rows > 0 && columns > 0)
            {
                this.rows = rows;
                this.columns = columns;

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

        public Matrix addition(Matrix matrix2)
        {
            if (rows == matrix2.rows && columns == matrix2.columns)
            {
                int [,]additionMas = new int[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        additionMas[i, p] = nums[i, p] + matrix2.nums[i, p];
                    }
                }

                return new Matrix(rows, columns, additionMas);
            } else
            {
                throw new ArgumentException("Dimension of the matrices is different");
            }
        }
        public Matrix substraction(Matrix matrix2)
        {
            if (rows == matrix2.rows && columns == matrix2.columns)
            {
                int[,] substractionMas = new int[rows, columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < columns; ++p)
                    {
                        substractionMas[i, p] = nums[i, p] - matrix2.nums[i, p];
                    }
                }

                return new Matrix(rows, matrix2.columns, substractionMas);
            }
            else
            {
                throw new ArgumentException("Dimension of the matrices is different");
            }
        }
        public Matrix multiplication(Matrix matrix2)
        {
            if (columns == matrix2.rows)
            {
                int[,] multiplicationMas = new int[rows, matrix2.columns];

                for (int i = 0; i < rows; ++i)
                {
                    for (int p = 0; p < matrix2.columns; ++p)
                    {
                        int num = multiplicationRowOnColumn(columns, nums, matrix2.nums, i, p);

                        multiplicationMas[i, p] = num;
                    }
                }

                return new Matrix(rows, matrix2.columns, multiplicationMas);
            }
            else
            {
                throw new ArgumentException("Number of columns of one matrix is not equal to the number of rows of second matrix");
            }
        }

        public Matrix multiplicationOnNumber(int number)
        {
            int[,] multiplicationOnNumberMas = new int[rows, columns];

            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    multiplicationOnNumberMas[i, p] = nums[i, p] * number;
                }
            }

            return new Matrix(rows, columns, multiplicationOnNumberMas);
        }

        public Matrix transposition() {
            int[,] transpositionMas = new int[columns, rows];

            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
                {
                    transpositionMas[p, i] = nums[i, p];
                }
            }

            return new Matrix(columns, rows, transpositionMas);
        }

        private int multiplicationRowOnColumn(int columns1, int[,] mas1, int[,] mas2, int indRow, int indColumn)
        {
            int result = 0;
            for (int i = 0; i < columns1; ++i)
            {
                result += mas1[indRow, i] * mas2[i, indColumn];
            }
            return result;
        }

        public int getDeterminant()
        {
            if (rows == columns)
            {
                int order = rows;

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

                    for (int i = 0; i < rows; ++i)
                    {
                        int indexRow = i, indexColumn = 0;
                        var newMatrix = getReducedMatrix(indexRow, indexColumn);
                        int minor = newMatrix.getDeterminant();
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

        private Matrix getReducedMatrix(int indDeleteRow, int indDeleteColumn)
        {
            int newSize = rows - 1;
            int[,] newMas = new int[newSize, newSize];

            for (int i = 0; i < rows; ++i)
            {
                for (int p = 0; p < columns; ++p)
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
