

namespace Lab1
{
	class Project
    {
		const int MAXINT = 100;
		static void Divider()
		{
			Console.WriteLine("------------------------------------------------------------------------------");
		}
		static double EnterDouble(string message)
		{
			double result;
			do
			{
				Console.WriteLine(message);
			} while (!Double.TryParse(Console.ReadLine(), out result));

			return result;
		}
		static int EnterInt(string message)
		{
			int result;
			do
			{
				Console.WriteLine(message);
			} while (!Int32.TryParse(Console.ReadLine(), out result));

			return result;
		}
		static int[] RandomArray(int size)
		{
			int[] array = new int[size];
			Random seed = new Random();

			for (int i = 0; i < size; i++) array[i] = seed.Next(MAXINT);
			return array;
		}
		static int[][] RandomMatrix(int height, int length)
		{
			int[][] matrix = new int[height][];
			Random seed = new Random();
			for (int i = 0; i < height; i++) matrix[i] = new int[length];

			for (int i = 0; i < height * length; i++) matrix[i / length][i % length] = seed.Next(MAXINT);

			return matrix;
		}
		static void OutputArray(int[] array, string message)
		{
			Console.WriteLine(message);
			foreach (int element in array) Console.Write(element + " ");
			Console.WriteLine("");
		}
		static int[] EnterArray(int size)
		{
			int[] array = new int[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = EnterInt("Enter element number " + i + ":");
			}
			return array;
		}
		static int[] ExtendArray(int[] array, int size, int extensionsize)
        {
			int[] result = new int[size + extensionsize];
			for (int i = 0; i < size; i++) result[i]=array[i];
			for (int i = size; i < size + extensionsize; i++) result[i] = 0;
			return result;
        }
		static void MergeSort(int[] array, int lo, int hi)
		{
			if (lo >= hi) return;
			int pivot = (lo + hi) / 2;
			MergeSort(array, lo, pivot);
			MergeSort(array, pivot + 1, hi);

			int[] temp = new int[(hi - lo + 1)];
			int loi = lo, hii = pivot + 1, cycles = 0;

			while (cycles < (hi - lo + 1))
			{
				if (loi == pivot + 1)
				{
					temp[cycles] = array[hii];
					hii++;
				}
				else if (hii == hi + 1)
				{
					temp[cycles] = array[loi];
					loi++;
				}
				else if (array[loi] < array[hii])
				{
					temp[cycles] = array[loi];
					loi++;
				}
				else
				{
					temp[cycles] = array[hii];
					hii++;
				}
				cycles++;
			}
			//OutputArray(temp,"");
			for (int i = lo; i <= hi; i++)
			{
				array[i] = temp[i - lo];
			}
			return;
		}
		static int LinearSearch(int[] array,int size, int x)
        {
			for (int i = 0; i < size; i++)
			{
				if (x == array[i])
				{
					return i;
				}
			}
			return size;
		}
		static int BarrierSearch(int[] array, int size, int x)
		{
			array = ExtendArray(array, size, 1);
			array[size] = x;
			int i = 0;
			while (x != array[i])
			{
				i++;
			}
			return i;
		}
		static int BinarySearchCustom(int[] array, int lo, int hi, int x,int[] depth)
		{
			int guess = (lo + hi) / 2;
			depth[0]++;

			if (hi - lo < 2 && array[lo] != x && array[hi] != x) return -1;
			if (array[guess] == x) return guess;
			else if (array[guess] > x) return BinarySearchCustom(array, lo, guess - 1, x, depth);
			else return BinarySearchCustom(array, guess + 1, hi, x, depth);
		}
		static int BinarySearchGolden(int[] array, int lo, int hi, int x, int[] depth)
		{
			double goldenratio = 1.61803398875;
			int guess = (int)((lo * goldenratio + hi) / (goldenratio + 1));
			depth[0]++;

			if (hi - lo < 2 && array[lo] != x && array[hi] != x) return -1;
			if (array[guess] == x) return guess;
			else if (array[guess] > x) return BinarySearchGolden(array, lo, guess - 1, x, depth);
			else return BinarySearchGolden(array, guess + 1, hi, x, depth);
		}
		static void Task1()
        {
			int n = EnterInt("Enter array size:");
			int t = EnterInt("Randomize array?(1 for yes):");
			int[] array = new int[n];
			if (t == 1) 
			{ 
				array = RandomArray(n);
				OutputArray(array,"Genereted an array:");
			}
			else { array = EnterArray(n); }

			int x = EnterInt("Enter an element to search for:");
			int result = LinearSearch(array, n, x);

			if (result<n)Console.WriteLine("Found the element at index " + result + '\n' + "Comparisons made: " + (result + 1));
			else Console.WriteLine("Element not found" + '\n' + "Comparisons made: " + n);

		}
		static void Task2()
		{
			int n = EnterInt("Enter array size:");
			int t = EnterInt("Randomize array?(1 for yes):");
			int[] array = new int[n];
			if (t == 1)
			{
				array = RandomArray(n);
				OutputArray(array, "Genereted an array:");
			}
			else { array = EnterArray(n);}
			
			int x = EnterInt("Enter an element to search for:");

			int result = BarrierSearch(array, n, x);

			if (result == n) Console.WriteLine("Element not found" + '\n' + "Comparisons made: " + n);
			else Console.WriteLine("Element found at index " + result + '\n' + "Comparisons made: " + (result + 1));
		}
		static void Task3()
		{
			int n = EnterInt("Enter array size:");
			int t = EnterInt("Randomize array?(1 for yes):");
			int[] array = new int[n + 1];
			if (t == 1)
			{
				array = RandomArray(n);
				OutputArray(array, "Genereted an array:");
			}
			else { array = EnterArray(n); }

			MergeSort(array, 0, n - 1);
			OutputArray(array, "Sorted the array:");

			int x = EnterInt("Enter an element to search for:");
			int[] depth = new int[1];

			int result = BinarySearchCustom(array, 0, n - 1, x,depth);

			if (result == -1)Console.WriteLine("Element not found" + '\n' + "Comparisons made: " + depth[0]);
			else Console.WriteLine("Element found at index " + result + '\n' + "Comparisons made: " + depth[0]);

		}
		static void Task4()
		{
			int n = EnterInt("Enter array size:");
			int t = EnterInt("Randomize array?(1 for yes):");
			int[] array = new int[n + 1];
			if (t == 1)
			{
				array = RandomArray(n);
				OutputArray(array, "Genereted an array:");
			}
			else { array = EnterArray(n); }

			MergeSort(array, 0, n - 1);
			OutputArray(array, "Sorted the array:");

			int x = EnterInt("Enter an element to search for:");
			int[] depth = new int[1];

			int result = BinarySearchGolden(array, 0, n - 1, x,depth);

			if (result == -1) Console.WriteLine("Element not found" + '\n' + "Comparisons made: " + depth[0]);
			else Console.WriteLine("Element found at index " + result +'\n' + "Comparisons made: " + depth[0]);
		}
		static void Menu()
		{
			Console.WriteLine("Lab #1 by Danyil Akulich, IPZ-11");
			int t;
			while (true)
			{
				t = EnterInt("Enter task number(1-4)or 5 to exit");
				switch (t)
				{
					case 1: Task1(); break;
					case 2: Task2(); break;
					case 3: Task3(); break;
					case 4: Task4(); break;
					case 5: return;
				}
				Divider();
			}
		}
		static void Main()
        {
			Menu();
        }

	}
}



