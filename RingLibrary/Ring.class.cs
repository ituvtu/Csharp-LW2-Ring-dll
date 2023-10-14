using System;
using System.Text;

namespace RingLibrary
{
	public class Ring<T> : IRing<T>
	{
		#region Constructors

		public Ring()
		{
			this.Length = 0;
			CurrentNode = null;
		}

		public Ring(T data)
		{
			CurrentNode = new Node { Data = data };
			CurrentNode.Next = CurrentNode;
			this.Length = 1;
		}

		public Ring(Ring<T> otherRing)
		{
			if (otherRing != null && otherRing.CurrentNode != null)
			{
				this.Length = otherRing.Length;
				CurrentNode = new Node(otherRing.CurrentNode.Data);
				Node sourceNode = otherRing.CurrentNode.Next;
				Node targetNode = CurrentNode;

				while (sourceNode != otherRing.CurrentNode)
				{
					targetNode.Next = new Node(sourceNode.Data);
					sourceNode = sourceNode.Next;
					targetNode = targetNode.Next;
				}

				targetNode.Next = CurrentNode;
			}
			else
			{
				CurrentNode = null;
			}
		}

		public Ring(T[] dataArray)
		{
			if (dataArray == null || dataArray.Length == 0)
			{
				CurrentNode = null;
				throw new ArgumentException("Invalid input array");
			}

			CurrentNode = new Node { Data = dataArray[0] };
			Node node = CurrentNode;
			Length = dataArray.Length;
			for (int i = 1; i < dataArray.Length; i++)
			{
				node.Next = new Node { Data = dataArray[i] };
				node = node.Next;
			}

			node.Next = CurrentNode;
		}

		#endregion

		#region Equals and GetHashcode overloads
		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Ring<T> otherRing = (Ring<T>)obj;

			// Compare the lengths of the rings first
			if (Length != otherRing.Length)
			{
				return false;
			}

			// Compare the elements in the rings
			Node thisNode = CurrentNode;
			Node otherNode = otherRing.CurrentNode;

			for (int i = 0; i < Length; i++)
			{
				if (!thisNode.Data.Equals(otherNode.Data))
				{
					return false;
				}

				thisNode = thisNode.Next;
				otherNode = otherNode.Next;
			}

			return true;
		}
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				Node node = CurrentNode;

				for (int i = 0; i < Length; i++)
				{
					hash = hash * 31 + node.Data.GetHashCode();
					node = node.Next;
				}

				return hash;
			}
		}
		#endregion

		#region InputFromConsole Function

		public void InputFromConsole()
		{
			Console.Write("Введите новые элементы через пробел: ");
			string input = Console.ReadLine();
			string[] values = input.Split(' ');

			foreach (string valueStr in values)
			{
				try
				{
					T value = (T)Convert.ChangeType(valueStr, typeof(T));
					Node newNode = new Node(value);
					if (CurrentNode == null)
					{
						newNode.Next = newNode;
						CurrentNode = newNode;
					}
					else
					{
						newNode.Next = CurrentNode.Next;
						CurrentNode.Next = newNode;
						CurrentNode = newNode;
					}
					Length++;
				}
				catch (FormatException)
				{
					Console.WriteLine($"Ошибка: Неверный формат значения '{valueStr}'. Пропущен.");
					throw; // Повторное генерирование исключения
				}
				catch (InvalidCastException)
				{
					Console.WriteLine($"Ошибка: Неверный тип данных для значения '{valueStr}'. Пропущен.");
				}
				catch (ArgumentNullException)
				{
					Console.WriteLine("Ошибка: Значение не может быть пустым. Пропущено.");
				}
				catch (System.Exception)
				{
					Console.WriteLine($"Ошибка: Не удалось добавить значение '{valueStr}'. Пропущено.");
				}
			}
		}

		#endregion

		#region ToString Method

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Length == 0)
			{
				sb.Append("Порожньо");
			}
			else
			{
				Node startNode = CurrentNode;
				Node current = startNode;
				do
				{
					sb.Append(current.Data);
					if (current.Next != startNode)
					{
						sb.Append(" -> ");
					}
					current = current.Next;
				} while (current != startNode);
			}
			return sb.ToString();
		}

		#endregion

		#region Properties

		public int Length { get; private set; }

		public class Node
		{
			public T Data { get; set; }
			public Node Next { get; set; }

			public Node(T data) { Data = data; }
			public Node() { }
		}

		public Node CurrentNode;

		#endregion

		#region Read element method
		public T Read()
		{
			if (CurrentNode == null)
				throw new InvalidOperationException("The ring is empty");

			return CurrentNode.Data;
		}
		#endregion

		#region Operator Overloading

		public static Ring<T> operator --(Ring<T> ring)
		{
			if (ring.CurrentNode == null)
				throw new InvalidOperationException("The ring is empty");

			Node previous = ring.CurrentNode;
			while (ring.CurrentNode.Next != previous)
			{
				previous = previous.Next;
			}
			ring.CurrentNode = previous;
			return ring;
		}

		public static Ring<T> operator ++(Ring<T> ring)
		{
			if (ring.CurrentNode == null)
				throw new InvalidOperationException("The ring is empty");
			ring.CurrentNode = ring.CurrentNode.Next;
			return ring;
		}

		public static Ring<T> operator <(Ring<T> ring, T value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value), "Значение не может быть пустым.");

			Node newNode = new Node(value);
			if (ring.Length == 0)
			{
				newNode.Next = newNode;
				ring.CurrentNode = newNode;
			}
			else
			{
				newNode.Next = ring.CurrentNode.Next;
				ring.CurrentNode.Next = newNode;
				ring.CurrentNode = newNode;
			}
			ring.Length++;
			return ring;
		}

		public static Ring<T> operator >(Ring<T> ring, T value)
		{
			if (ring.CurrentNode == null)
				throw new InvalidOperationException("Кільце порожнє");
			ring.CurrentNode.Next = ring.CurrentNode.Next.Next;
			ring.Length--;
			return ring;
		}

		#endregion

		#region True and False Overloads

		public static bool operator true(Ring<T> ring)
		{
			return ring.Length > 0;
		}

		public static bool operator false(Ring<T> ring)
		{
			return ring.Length == 0;
		}

		#endregion

		#region Explicit and Implicit Type Conversion

		public static explicit operator Ring<T>(T[] array)
		{
			return new Ring<T>(array);
		}

		public static implicit operator T[](Ring<T> ring)
		{
			T[] dataArray = new T[ring.Length];
			for (int i = 0; i < ring.Length; i++)
			{
				dataArray[i] = ring[i];
			}
			return dataArray;
		}

		#endregion

		#region Indexers

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
					throw new IndexOutOfRangeException("Index is out of range");

				Node node = CurrentNode;
				for (int i = 0; i < index; i++)
				{
					node = node.Next;
				}
				return node.Data;
			}
			set
			{
				if (index < 0 || index >= Length)
					throw new IndexOutOfRangeException("Index is out of range");

				Node node = CurrentNode;
				for (int i = 0; i < index; i++)
				{
					node = node.Next;
				}
				node.Data = value;
			}
		}

		#endregion

		#region Equality and Inequality Overloads

		public static bool operator ==(Ring<T> ring1, Ring<T> ring2)
		{
			if (ReferenceEquals(ring1, ring2))
				return true;

			if (ring1 is null || ring2 is null)
				return false;

			return ring1.Equals(ring2);
		}

		public static bool operator !=(Ring<T> ring1, Ring<T> ring2)
		{
			return !(ring1 == ring2);
		}

		#endregion
	}
}
