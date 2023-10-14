using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingLibrary;
using System;

namespace RingUnitTest
{
	[TestClass]
	public class RingTests
	{
		[TestMethod]
		public void TestRingAddElement()
		{
			Ring<int> ring = new Ring<int>();

			ring= ring<1;
			ring = ring < 2;
			ring = ring < 3;

			Assert.AreEqual(3, ring.Length);
		}

		[TestMethod]
		public void TestRingRead()
		{
			Ring<string> ring = new Ring<string>();
			string value="Hello";
			ring = ring < value; 
			string result = ring.Read();

			Assert.AreEqual("Hello", result);
		}

		[TestMethod]
		public void TestRingReadAndRemove()
		{
			Ring<int> ring = new Ring<int>();
			ring=ring < 42;
			Assert.AreEqual(42, ring.CurrentNode.Data);
			ring= ring >42;
			Assert.AreEqual(0, ring.Length);
		}

		[TestMethod]
		public void TestRingMoveNext()
		{
			Ring<char> ring = new Ring<char>();
			ring=ring<'A';
			ring=ring<'B';
			ring++;
			Assert.AreEqual('A', ring.Read());
		}

		[TestMethod]
		public void TestRingMovePrevious()
		{
			Ring<string> ring = new Ring<string>();
			ring=ring<"One";
			ring = ring < "Two";

			ring--;

			Assert.AreEqual("One", ring.Read());
		}
		[TestMethod]
		public void TestRingConstructorWithNoArguments()
		{
			Ring<int> ring = new Ring<int>();
			Assert.AreEqual(0, ring.Length);
			Assert.IsNull(ring.CurrentNode);
		}

		[TestMethod]
		public void TestRingConstructorWithDataArgument()
		{
			Ring<int> ring = new Ring<int>(42);
			Assert.AreEqual(1, ring.Length);
			Assert.AreEqual(42, ring[0]);
			Assert.IsNotNull(ring.CurrentNode);
		}

		[TestMethod]
		public void TestRingConstructorWithRingArgument()
		{
			Ring<int> originalRing = new Ring<int>(new int[] { 1, 2, 3 });
			Ring<int> ring = new Ring<int>(originalRing);
			Assert.AreEqual(originalRing.Length, ring.Length);
			Assert.IsTrue(originalRing == ring);
		}

		[TestMethod]
		public void TestRingConstructorWithArrayArgument()
		{
			var dataArray = new int[] { 1, 2, 3 };
			Ring<int> ring = new Ring<int>(dataArray);
			Assert.AreEqual(dataArray.Length, ring.Length);
			for (int i = 0; i < dataArray.Length; i++)
			{
				Assert.AreEqual(dataArray[i], ring[i]);
			}
		}

		[TestMethod]
		public void TestAddElement()
		{
			Ring<int> ring = new Ring<int>();
			ring = ring < 42;
			Assert.AreEqual(1, ring.Length);
			Assert.AreEqual(42, ring[0]);
		}


		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestReadEmptyRing()
		{
			Ring<int> ring = new Ring<int>();
			_ = ring.Read();
		}



		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestReadAndRemoveEmptyRing()
		{
			Ring<int> ring = new Ring<int>();
			_ = ring>0;
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestMoveNextEmptyRing()
		{
			Ring<int> ring = new Ring<int>();
			ring++;
		}


		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestMovePreviousEmptyRing()
		{
			Ring<int> ring = new Ring<int>();
			ring--;
		}

		[TestMethod]
		public void TestMovePreviousOnSingleElementRing()
		{
			Ring<int> ring = new Ring<int>(42);
			ring--;
			Assert.IsTrue(42 == ring.CurrentNode.Data);
		}

		[TestMethod]
		public void TestReadAndRemoveOnSingleElementRing()
		{
			Ring<int> ring = new Ring<int>(42);
			_=ring>0;
			Assert.IsTrue(ring.Length == 0);
		}

		[TestMethod]
		public void TestCopy()
		{
			Ring<int> originalRing = new Ring<int>(new int[] { 1, 2, 3 });
			Ring<int> copiedRing = new Ring<int>(originalRing);
			Assert.AreEqual(originalRing.Length, copiedRing.Length);
			for (int i = 0; i < originalRing.Length; i++)
			{
				Assert.AreEqual(originalRing[i], copiedRing[i]);
			}
		}

		[TestMethod]
		public void TestEquals()
		{
			Ring<int> ring1 = new Ring<int>(new int[] { 1, 2, 3 });
			Ring<int> ring2 = new Ring<int>(new int[] { 1, 2, 3 });
			Assert.IsTrue(ring1.Equals(ring2));
		}

		[TestMethod]
		public void TestNotEquals()
		{
			Ring<int> ring1 = new Ring<int>(new int[] { 1, 2, 3 });
			Ring<int> ring2 = new Ring<int>(new int[] { 1, 2, 4 });
			Assert.IsFalse(ring1.Equals(ring2));
		}

		[TestMethod]
		[ExpectedException(typeof(System.NullReferenceException))]
		public void TestInputFromConsoleWithInvalidInput()
		{
			Ring<int> ring = new Ring<int>();
			Console.SetIn(new System.IO.StringReader("")); // Simulate empty input
			ring.InputFromConsole();
		}


		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void TestInputFromConsoleWithInvalidElementFormat()
		{
			Ring<int> ring = new Ring<int>();
			Console.SetIn(new System.IO.StringReader("abc")); // Simulate invalid input
			ring.InputFromConsole();
		}

		[TestMethod]
		public void TestValidInputFromConsole()
		{
			Ring<int> ring = new Ring<int>();
			Console.SetIn(new System.IO.StringReader("1 2 3")); // Valid input
			ring.InputFromConsole();
			Assert.AreEqual(3, ring.Length);
		}

		[TestMethod]
		public void TestToString()
		{
			Ring<int> ring = new Ring<int>(new int[] { 1, 2, 3 });
			string expected = "1 -> 2 -> 3";
			string result = ring.ToString();
			Assert.AreEqual(expected, result);
		}
	}
}
