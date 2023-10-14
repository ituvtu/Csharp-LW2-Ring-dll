using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingLibrary
{
	public interface IRing<T>
	{
		int Length { get; }  // Довжина кільця.
		T Read();  // Повернути поточний елемент.
		void InputFromConsole();  // Зчитати елементи з консолі та додати їх до кільця.
	}
}
